using System;

namespace Models
{
    public class SearchInput
    {
        public string order { get; set; }
        public string sort { get; set; }
        public string options { get; set; }
        public string query { get; set; }
        public DateTime? creationDate { get; set; }
    }
}
