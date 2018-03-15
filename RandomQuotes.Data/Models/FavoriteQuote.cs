using System.ComponentModel;

namespace RandomQuotes.Data.Models
{
    public class FavoriteQuoteViewModel 
    {
        [DisplayName("Quote Text")]
        public string QuoteText { get; set; }
        [DisplayName("Author")]
        public string AuthorName { get; set; }

        [DisplayName("Your Name")]
        public string SharingContactName { get; set; }
        [DisplayName("Your Email Address")]
        public string SharingContactEmail { get; set; }
    }
}
