using Elasticsearch.Net;
using System;

namespace BusinessLayer
{
    public class ElasticsearchSingleConn
    {
        private static readonly string _uri = "http://localhost:9200";
        private static readonly object LowLevelLock = new object();
        private static readonly object ConnectionLock = new object();
        private static volatile SingleNodeConnectionPool _singleNodeConnection;
        private static volatile ConnectionConfiguration _connectionConfiguration;
        private static volatile ElasticLowLevelClient _lowlevelClient;

        public static SingleNodeConnectionPool SingleNodeConn
        {
            get
            {
                if (_singleNodeConnection != null) { return _singleNodeConnection; }
                lock (ConnectionLock)
                {
                    if (_singleNodeConnection != null) { return _singleNodeConnection; }

                    var uri = new Uri(_uri);
                    _singleNodeConnection = new SingleNodeConnectionPool(uri);
                }
                return _singleNodeConnection;
            }
        }

        public static ElasticLowLevelClient LowLevelClient
        {
            get
            {
                if (_lowlevelClient != null) { return _lowlevelClient; }
                lock (LowLevelLock)
                {
                    if (_lowlevelClient != null) { return _lowlevelClient; }

                    _connectionConfiguration = new ConnectionConfiguration(SingleNodeConn)
                        .RequestTimeout(TimeSpan.FromMinutes(2))
                        .ThrowExceptions();
                    _lowlevelClient = new ElasticLowLevelClient(_connectionConfiguration);
                }
                return _lowlevelClient;
            }
        }
    }
}
