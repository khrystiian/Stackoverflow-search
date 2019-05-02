using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public class StackoverflowReader
    {
        private readonly string baseUrl = "https://api.stackexchange.com/2.2/";
        private readonly string tags = "&tags/"; //for the related search
        private readonly string related = "/related?"; //for the related search

        private readonly string advancedSearch = "search/advanced";
        private readonly string fromDate = "&fromdate="; //long format of date
        private readonly string tagged = "&tagged="; //for the main search
        private readonly string sort = "&sort="; //activity, relevance
        private readonly string order = "&order="; //asc, desc
        private readonly string page = "?page="; //number
        private readonly string urlTail = "site=stackoverflow";

        //baseUrl + tag(user input) + from date/to date/order/sort/page/size(for page read until items list is empty)/related/advanced

        public async Task InputRead(string inputOrder, string inputSort, string query, DateTime fromdate, string option)
        {
            using (var httpClient = new HttpClient())
            {
                var mainSeachQueryJson = await httpClient.GetStringAsync(
                    baseUrl+
                    advancedSearch+
                    page+
                    sort+inputSort+
                    order+inputOrder+
                    fromDate+fromdate+
                    tagged+query+
                    urlTail
                    );

                // Now parse with JSON.Net
            }
        }
    }
}
