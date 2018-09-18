namespace RandomQuotes
{
    public class AppSettings
    {
        public string AppVersion { get; set; }
        public string EnvironmentName { get; set; }
        public string TenantName { get; set; }
        public QuoteRecord[] CustomQuotes { get; set; }
    }

    public class QuoteRecord
    {
        public string Quote { get; set; }
        public string Author { get; set; }
    }
}
