using Elasticsearch.Net;
using Models;
using Nest;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class ElasticsearchImpl : IElasticsearch
    {
        #region Elasticsearch NEST indexing
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
        #endregion


        #region Elasticsearch.Net search
        public void SearchResult(IList<Items> stackResults)
        {
            IElasticLowLevelClient lowlevelclient = ElasticsearchSingleConn.LowLevelClient;
            QueryResult resultExist = QuerySearch(lowlevelclient);
            RootJson result = new RootJson();

            if (resultExist.hits.total.value == 0)
            {
                var client = ElasticsearchMultiNodesConn.ElasticClient;
                var successful = IndexToNestElasticsearch(client, stackResults);
                if (successful)
                {
                  //result = QuerySearch(lowlevelclient); 
                }
            }
                var q = result;
        }


        private QueryResult QuerySearch(IElasticLowLevelClient client)
        {
            QueryResult qResult = new QueryResult();
            if (client != null)
            {
                var searchResponse = client.Search<BytesResponse>("stackindex", "stacktype", PostData.Serializable(new
                {
                    from = 0,
                    size = 5,
                    //analyzer ="autocomplete",
                    query = new
                    {
                        match = new
                        {
                            tags = new
                            {
                                query = "angular"
                            }
                        },
                    }
                }));
                var responseBytes = searchResponse.Body;
               qResult = JsonConvert.DeserializeObject<QueryResult>(Encoding.UTF8.GetString(responseBytes));
            }

            return qResult;
        }
            #endregion
    }
}
