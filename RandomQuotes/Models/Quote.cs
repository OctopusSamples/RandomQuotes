namespace RandomQuotes.Models
{
    public class Quote
    {
        public int ID { get; set; }
        public string QuoteText { get; set; }
        
        public int AuthorID { get; set; }
        public Author Author { get; set; }

        public string AuthorName { get; set; } // TODO: Remove once everything is stable and working.
    }
}
