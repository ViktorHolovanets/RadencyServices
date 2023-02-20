namespace RadencyWebApplication.Models.Entity
{
    public class BookInfo
    {
        public int id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string cover { get; set; }
        public decimal rating { get; set; }
        public decimal reviewsNumber { get; set; }
    }
}