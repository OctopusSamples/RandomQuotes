using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace RandomQuotes.Models
{
    public class Quote
    {
        public static List<string> Quotes = new List<string>();
        public static List<string> Authors = new List<string>();

        public static void Initialize()
        {
            var quoteFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{Path.DirectorySeparatorChar}data{Path.DirectorySeparatorChar}quotes.txt");
            Quotes = File.Exists(quoteFilePath) ? File.ReadAllLines(quoteFilePath).Select(System.Net.WebUtility.HtmlDecode).ToList() : new List<string>();
            var authorFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{Path.DirectorySeparatorChar}data{Path.DirectorySeparatorChar}authors.txt");
            Authors = File.Exists(authorFilePath) ? File.ReadAllLines(authorFilePath).Select(System.Net.WebUtility.HtmlDecode).ToList() : new List<string>();
        }

        private Quote(string text, string author)
        {
            QuoteText = text;
            Author = author;
        }

        public static Quote GetRandomQuote()
        {
            var random = new Random();
            var index = random.Next(Quotes.Count);

            var randomQuote = Quotes.ElementAtOrDefault(index);
            var randomAuthor = Authors.ElementAtOrDefault(index);

            if (string.IsNullOrEmpty(randomQuote) | string.IsNullOrEmpty(randomAuthor))
            {
                return BuildQuote("Something went wrong", "System");
            }

            return BuildQuote(randomQuote, randomAuthor);
        }

        public static Quote BuildQuote(string quote, string author)
        {
            return new Quote(quote, author);
        }

        public string QuoteText { get; set; }
        public string Author { get; set; }
    }
}
