using SlackAPI;
using slackbot.Responders;
using System;

namespace slackbot
{
    class Program
    {
        static void Main(string[] args)
        {
            const string APIToken = "";

            var bot = new TJBot(new SlackSocketClient(APIToken));
            bot.AddResponder(new Greeting());
            bot.AddResponder(new LongRunner());
            bot.Connect();

            Console.ReadKey();
        }
    }
}
