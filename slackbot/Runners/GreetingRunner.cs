using System;

namespace slackbot.Runners
{
    public class GreetingRunner : IRunner
    {
        private Request _request;
        private Action<Request,string> _callback;

        public GreetingRunner(Request request, Action<Request,string> callback)
        {
            _request = request;
            _callback = callback;

            Reply();
        }

        private void Reply()
        {
            _callback( _request, "Hello " + _request.from);
        }

        public bool IsFinished()
        {
            return true;
        }
    }
}
