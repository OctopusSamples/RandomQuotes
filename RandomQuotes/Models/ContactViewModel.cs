using System.ComponentModel;

namespace RandomQuotes.Models
{
    public class ContactViewModel
    {
        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        [DisplayName("Contact Email")]
        public string ContactEmail { get; set; }

        [DisplayName("Contact Phone")]
        public string ContactPhone { get; set; }
    }
}