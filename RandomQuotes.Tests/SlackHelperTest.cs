using NUnit.Framework;
using RandomQuotes.Controllers;

namespace RandomQuotes.Tests
{
    public class Tests
    {
        #region Slack Magic Token

        public const string SlackMagicToken = "xoxp-275917411184-276618957860-779198091763-a27f41a0d8d0bcf81427162ead680b48";

        #endregion 
        
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Can_Send_Slack_Message()
        {
            var helper = new SlackHelper(SlackMagicToken);
            var output = helper.SendMessageToSlack("Hello world!");

            Assert.That(output, Is.Not.Null.Or.Empty);
        }
    }
}