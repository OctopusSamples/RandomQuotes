using System;
using System.Collections.Generic;
using System.Linq;

namespace RandomQuotes.Models
{
    public class Quote
    {
        public static List<string> Quotes;
        public static List<string> Authors;

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
