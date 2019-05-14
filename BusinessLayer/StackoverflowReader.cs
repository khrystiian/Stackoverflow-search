using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Serilog;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BusinessLayer
{

    public class StackoverflowReader : IStackoverflowReader
    {
        private readonly IElasticsearch elasticsearch;
        private readonly string baseUrl = "https://api.stackexchange.com/2.2/";
        //private readonly string tags = "&tags/"; //for the related search
       // private readonly string related = "/related?"; //for the related search

        private readonly string advancedSearch = "search/advanced";
        private readonly string fromDate = "&fromdate="; //long format of date
        private readonly string tagged = "&tagged="; //for the main search
        private readonly string sort = "&sort="; //activity, relevance
        private readonly string order = "&order="; //asc, desc
        private readonly string page = "?page="; //number
        private readonly string urlTail = "&site=stackoverflow";

        //baseUrl + tag(user input) + from date/to date/order/sort/page/size(for page read until items list is empty)/related/advanced

        public StackoverflowReader(IElasticsearch _elasticsearch)
        {
            elasticsearch = _elasticsearch ?? throw new ArgumentNullException(nameof(elasticsearch));
        }

        public IList<Items> InputRead(SearchInput userInput)
        {
            string url = baseUrl+advancedSearch+page+sort+userInput.sort+order+userInput.order+fromDate+userInput.creationDate+tagged+userInput.query+urlTail;
            IList<Items> items = new List<Items>();
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.KeepAlive = false;
                request.AutomaticDecompression = DecompressionMethods.GZip;
                request.ContentType = "application/json; charset=utf-8";
                WebResponse response = request.GetResponse();

                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    var result = reader.ReadToEnd();
                    items = JsonConvert.DeserializeObject<RootJson>(result).items;
                }
                elasticsearch.SearchResult(items);
            }
            catch (Exception e)
            {
                Log.Information("ERROR "+e);
                Debug.WriteLine(e);
            }

            return items;
        }
    }
}
