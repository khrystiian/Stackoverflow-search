using Models;
using Nest;
using System.Collections.Generic;

namespace BusinessLayer
{
    public interface IElasticsearch
    {
        bool IndexToNestElasticsearch(IElasticClient client, IList<Items> stackResults);
        ResultModel SearchOnELK(SearchModel searchModel);
    }
}
