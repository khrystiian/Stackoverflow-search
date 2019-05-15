using System;

namespace Models
{
    public class SearchModel
    {
        public string orderResults { get; set; }
        public string sortResults { get; set; }
        public string searchOption { get; set; }
        public string userInput { get; set; }
        public DateTime? creationDate { get; set; }
    }
}
