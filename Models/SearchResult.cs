using System;
using System.Collections.Generic;

namespace Models
{
    public class SearchResult
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public DateTime LastActivityDate { get; set; }
        public int AnswerCount { get; set; }
        public IList<string> Tags { get; set; }
        public Owner Owner { get; set; }
    }
}
