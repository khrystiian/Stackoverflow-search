using Models;
using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BusinessLayer
{
    public class StackoverflowReader : IStackoverflowReader
    {
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
        
        /// <summary>
        /// Search for result in Stackoverflow website using API.
        /// Plays the role of database for the elasticsearch engine.
        /// In real life scenario, data should already exist in the Elasticsearch, and the search should be performed only on ELK search engine.
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        public IList<Items> InputRead(SearchModel userInput)
        {
            IList<Items> items = new List<Items>();
            ResultModel queryResult = new ResultModel();
            string url = baseUrl+advancedSearch+page+
                sort+userInput.sortResults+
                order+userInput.orderResults+
                fromDate+userInput.creationDate+
                tagged+userInput.userInput+
                urlTail;       

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
                    items = JsonConvert.DeserializeObject<StackResultModel>(result).items;
                }
            }
            catch (Exception e)
            {
                Log.Information("Error in Stackoverflow API "+e);
                Debug.WriteLine(e);
            }

            return items;
        }
    }
}
