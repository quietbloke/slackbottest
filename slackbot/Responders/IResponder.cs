using SlackAPI.WebSocketMessages;
using slackbot.Runners;
using System;

namespace slackbot.Responders
{
    public interface IResponder
    {
        bool Matched(Request request);
        IRunner Create(Request request, Action<Request,string> callback);
    }
}
