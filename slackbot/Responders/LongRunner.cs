using System;
using System.Text;
using System.Text.RegularExpressions;
using slackbot.Runners;

namespace slackbot.Responders
{
    public class LongRunner : IResponder
    {
        private const string DEFINE_REGEX = @"longrunner\s+(?<seconds>\w+)";

        StringBuilder output = new StringBuilder();

        public bool Matched(Request request)
        {
            return Regex.IsMatch(request.message, DEFINE_REGEX, RegexOptions.IgnoreCase);
        }

        public IRunner Create(Request request, Action<Request, string> callback)
        {
            string seconds = Regex.Match(request.message, DEFINE_REGEX).Groups["seconds"].Value;
            return new LongRunnerRunner(request, callback, seconds);
        }
    }
}
