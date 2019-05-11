using Models;
using Nest;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class ElasticsearchImpl : IElasticsearch
    {
        private readonly IStackoverflowReader stack;

        public ElasticsearchImpl(IStackoverflowReader _stack)
        {
            stack = _stack ?? throw new ArgumentNullException(nameof(stack));
        }

        public bool IndexToNestElasticsearch(IElasticClient client, IList<Items> stackResults)
        {
            bool _indexResponse = false;
            for (int i = 0; i < stackResults.Count; i++)
            {
                var indexResponse = client.Index(stackResults[i], idx => idx.Index("mypersonindex"));
                _indexResponse = indexResponse.IsValid;

                if (!indexResponse.IsValid)
                {
                    _indexResponse = indexResponse.IsValid;
                    break;
                }
            }
            return _indexResponse;

            // var response = client.Get<Tweet>(1, idx => idx.Index("mytweetindex")); // returns an IGetResponse mapped 1-to-1 with the Elasticsearch JSON response
            // Tweet newTweet = response.Source; // the original document
        }

        #region NEST
        public void NestIndexSearch(IList<Items> stackResults)
        {
            var client = ElasticsearchMultiNodesConn.ElasticClient;
            var successful = IndexToNestElasticsearch(client, stackResults);
            if (successful)
            {
                var response = client.Search<Items>(s => s
                                .From(0) //results to skip
                                .Size(10) //number of results
                                .Query(q => q
                                .Term(t => t.tags, "signalr") || q     //to do ..
                                .Match(mq => mq
                                       .Field(f => f.tags)
                                       .Query("signalr"))
                                       )
                                );
            }
        }

        public void NestIndexSearch2()
        {
            var client = ElasticsearchMultiNodesConn.ElasticClient;
            var succesful = true; //IndexToNestElasticsearch(client);
            if (succesful)
            {
                var request = new SearchRequest
                {
                    From = 0,
                    Size = 10,
                    Query = new TermQuery { Field = "End_address", Value = "Bucharest" } // ||
                                                                                         //new MatchQuery { Field = "description", Query = "nest" }
                };

                var response = client.Search<Items>(request);
            }
        }
            #endregion
    }
}
