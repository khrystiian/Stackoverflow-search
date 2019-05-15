using System.Collections.Generic;

namespace Models
{
    public class ResultModel
    {
        public Hits hits { get; set; }
    }

    public class Total
    {
        public int value { get; set; }
    }

    public class Hits
    {
        public Total total { get; set; }
        public IList<Hit> hits { get; set; }
    }

    public class Hit
    {
        public Items _source { get; set; }
    }
}
