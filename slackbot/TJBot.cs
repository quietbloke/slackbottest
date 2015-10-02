using SlackAPI;
using SlackAPI.WebSocketMessages;
using slackbot.Responders;
using slackbot.Runners;
using System;
using System.Collections.Generic;

namespace slackbot
{
    class TJBot
    {
        SlackSocketClient _client = null;
        List<IResponder> Responders;
        List<IRunner> runners;

        public TJBot(SlackSocketClient client)
        {
            _client = client;
            Responders = new List<IResponder>();
            runners = new List<IRunner>();
        }

        public void AddResponder(IResponder responder)
        {
            Responders.Add(responder);
        }

        public void Connect()
        {
            if ( _client.IsConnected)
                _client.CloseSocket();

            _client.OnHello += UpAndRunning;

            _client.Connect((connected) => {
                //This is called once the client has emitted the RTM start command
                Console.WriteLine("client emmited RTM Start command");
            }, () => {
                //This is called once the RTM client has connected to the end point
                Console.WriteLine("RTM client connected to end point");
            });
        }

        private void UpAndRunning()
        {
            _client.OnMessageReceived += Receieved;

//            var general = client.Channels.Where(r => r.name == "general").First();
//            client.SendMessage(msg, general.id, "Hi, Im a bot of sorts.");
        }

        private void Receieved(NewMessage message)
        {
            Console.WriteLine("Got message " + message.channel + " : " + message.text);

            var request = new Request {
                message = message.text,
                from = _client.UserLookup[message.user].name,
                channel = message.channel
            };

            foreach ( var responder in Responders )
            {
                if (responder.Matched(request))
                {
                    var runner = responder.Create(request, RunnerOutput);
                    if ( !runner.IsFinished())
                        runners.Add(runner);
                    return;
                }
            }

            _client.SendMessage(SendMessage, message.channel, "You said : " + message.text);
        }

        public void RunnerOutput(Request request, string result)
        {
            _client.SendMessage(SendMessage, request.channel, result);
            Console.WriteLine("Got runner result : " + result);
            
            // while we are here remove any runner that is now finished
            runners.RemoveAll(r => r.IsFinished() == true);
        }

        private void SendMessage(MessageReceived obj)
        {
            Console.WriteLine("Wrote message");
        }
    }
}
