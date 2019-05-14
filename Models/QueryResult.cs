namespace Models
{
    public class QueryResult
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
    }
}
