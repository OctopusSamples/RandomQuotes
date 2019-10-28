using NUnit.Framework;
using RandomQuotes.Models;

namespace RandomQuotes.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Quote.Initialize();
        }

        [Test]
        public void Test1()
        {
            Assert.True(Quote.GetRandomQuote().QuoteText != "Something went wrong");
        }
    }
}