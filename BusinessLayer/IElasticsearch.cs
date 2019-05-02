using Nest;

namespace BusinessLayer
{
    public interface IElasticsearch
    {
        bool IndexToNestElasticsearch(IElasticClient client);
        void NestIndexSearch();
        void NestIndexSearch2();
    }
}
