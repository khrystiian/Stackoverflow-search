using System.Collections.Generic;

namespace Models
{
    public class StackResultModel
    {
        public List<Items> items { get; set; }
        public bool has_more { get; set; }
        public int quota_max { get; set; }
        public int quota_remaining { get; set; }
    }
}
