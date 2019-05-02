using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace API
{
    public interface IElasticsearch
    {
        bool IndexToNestElasticsearch(IElasticClient client);
        void NestIndexSearch();
        void NestIndexSearch2();
    }
}
