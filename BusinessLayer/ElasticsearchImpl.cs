using Models;
using Nest;
using System;

namespace BusinessLayer
{
    public class ElasticsearchImpl : IElasticsearch
    {
        public bool IndexToNestElasticsearch(IElasticClient client)
        {
            var tweet = new
            {
                Id = 1,
                User = "kimchy",
                PostDate = new DateTime(2009, 11, 15),
                Message = "Trying out NEST, so far so good?"
            };

            var indexResponse = client.Index(tweet, idx => idx.Index("mypersonindex"));
            return indexResponse.IsValid;
            // var response = client.Get<Tweet>(1, idx => idx.Index("mytweetindex")); // returns an IGetResponse mapped 1-to-1 with the Elasticsearch JSON response
            // Tweet newTweet = response.Source; // the original document
        }

        #region NEST
        public void NestIndexSearch()
        {
            var client = ElasticsearchMultiNodesConn.ElasticClient;
            var successful = IndexToNestElasticsearch(client);
            if (successful)
            {
                var response = client.Search<SearchResult>(s => s
                                .From(0) //results to skip
                                .Size(10) //number of results
                                .Query(q => q
                                .Term(t => t.Tags, "vejle") || q     //to do
                                .Match(mq => mq
                                       .Field(f => f.Tags)
                                       .Query("vejle"))
                                       )
                                );
            }
        }

        public void NestIndexSearch2()
        {
            var client = ElasticsearchMultiNodesConn.ElasticClient;
            var succesful = IndexToNestElasticsearch(client);
            if (succesful)
            {
                var request = new SearchRequest
                {
                    From = 0,
                    Size = 10,
                    Query = new TermQuery { Field = "End_address", Value = "Bucharest" } // ||
                                                                                         //new MatchQuery { Field = "description", Query = "nest" }
                };

                var response = client.Search<SearchResult>(request);
            }
        }
            #endregion
    }
}
