using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace RandomQuotes.Controllers
{
    /// <summary>
    /// Slack Helper
    /// Code copy/pasted from StackOverflow: https://stackoverflow.com/questions/53192395/how-to-post-a-message-via-slack-app-from-c-as-a-user-not-app-with-attachment
    /// </summary>
    public class SlackHelper
    {
        private readonly string slackMagicToken;

        public SlackHelper(string token)
        {
            slackMagicToken = token;
        }
        
        public string SendMessageToSlack(string message)
        {        
            var data = new NameValueCollection();
            data["token"] = slackMagicToken;
            data["channel"] = "general";        
            data["as_user"] = "true"; 
            data["text"] = message;
            data["attachments"] = "[{\"fallback\":\"dummy\", \"text\":\"this is an attachment\"}]";

            var client = new WebClient();
            var response = client.UploadValues("https://slack.com/api/chat.postMessage", "POST", data);
            string responseInString = Encoding.UTF8.GetString(response);
            Console.WriteLine(responseInString);
            return responseInString;
        }
    }
}