using slackbot.Runners;
using System;
using System.Text.RegularExpressions;

namespace slackbot.Responders
{
    public class Greeting : IResponder
    {
        public bool Matched(Request request)
        {
            return Regex.IsMatch(request.message, @"\b(hi|hey|hello)\b", RegexOptions.IgnoreCase);
        }

        public IRunner Create(Request request, Action<Request, string> callback)
        {
            return new GreetingRunner(request,callback);
        }
    }
}
