using Elasticsearch.Net;
using Models;
using Nest;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class ElasticsearchImpl : IElasticsearch
    {
        private readonly IStackoverflowReader stack;

        public ElasticsearchImpl(IStackoverflowReader _stack) => stack = _stack ?? throw new ArgumentNullException(nameof(stack));

        /// <summary>
        /// NEST index search result from stackoverflow to elasticsearch.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="stackResults"></param>
        /// <returns></returns>
        public bool IndexToNestElasticsearch(IElasticClient client, IList<Items> stackResults)
        {
            bool _indexResponse = false;
            var indexResponse = client.IndexMany<Items>(stackResults, "stackindex", "stacktype");
            _indexResponse = indexResponse.IsValid;
            if (indexResponse.Errors)
            {
                _indexResponse = indexResponse.IsValid;
            }
            return _indexResponse;
        }


        /// <summary>
        /// Elasticsearch.NET search.
        /// </summary>
        /// <param name="stackResults"></param>
        public ResultModel SearchOnELK(SearchModel searchModel)
        {
            IElasticLowLevelClient lowlevelclient = ElasticsearchSingleConn.LowLevelClient;
            ResultModel resultExist = SearchResult(lowlevelclient, searchModel.userInput);

            if (resultExist.hits.total.value == 0)
            {
                var client = ElasticsearchMultiNodesConn.ElasticClient;
                var stackResults = stack.InputRead(searchModel);

                //Check if Stackoverflow API returns results.
                //Necessary because this method is called on user input change, which is meant for autocompletition.
                //Stackoverflow API doesn't offer autocompletition, therefore the program will return a message to the user accordingly.
                //When Stackoverflow API will return a result this will be forwarded to the ELK engine to enable autocompletition feature.
                //All this process is required since there is no database in place to feed the ELK engine, only an open API which represents the data provider.
                if (stackResults.Count > 0)
                {
                    var successful = IndexToNestElasticsearch(client, stackResults);
                    if (successful)
                    {
                        resultExist = SearchResult(lowlevelclient, searchModel.userInput);
                    }
                }

            }
            return resultExist;
        }

        /// <summary>
        /// Query the elasticsearch engine.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private ResultModel SearchResult(IElasticLowLevelClient client, string userInput)
        {
            if (client == null)
            {
                Log.Information("Error in connecting with ELK ! ");
            }

            var searchResponse = client.Search<BytesResponse>("stackoverflowindex", "item", PostData.Serializable(new
            {
                from = 0,
                size = 100,
                query = new
                {
                    match = new
                    {
                        tags = new
                        {
                            query = userInput,
                            analyzer = "autocomplete" //type-to-search 
                        }
                    },
                }
            }));
            return JsonConvert.DeserializeObject<ResultModel>(Encoding.UTF8.GetString(searchResponse.Body));
        }
    }
}
