using System;
using System.Collections.Generic;

namespace Models
{
    public class Items
    {
        public string title { get; set; }
        public string link { get; set; }
        public int last_activity_date { get; set; }
        public int answer_count { get; set; }
        public IList<string> tags { get; set; }
        public Owner Owner { get; set; }
    }
}
