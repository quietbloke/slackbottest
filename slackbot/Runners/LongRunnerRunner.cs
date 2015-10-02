using SlackAPI.WebSocketMessages;
using System;
using System.Diagnostics;
using System.Text;

namespace slackbot.Runners
{
    class LongRunnerRunner : IRunner
    {
        private Request _request;
        private Action<Request, string> _callback;
        private NewMessage _message;
        StringBuilder output = new StringBuilder();
        private bool _finished = false;
        private Process process;
        private string _seconds;

        public LongRunnerRunner(Request request, Action<Request, string> callback, string seconds)
        {
            _request = request;
            _callback = callback;
            _seconds = seconds;

            Reply();
        }

        private void Reply()
        {
            string filename = "..\\..\\..\\LongRunner\\bin\\Debug\\LongRunner.exe";
            string arguments = _seconds;
            process = new Process();
            ProcessStartInfo psi = new ProcessStartInfo(filename);
            psi.Arguments = arguments;
            psi.CreateNoWindow = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.UseShellExecute = false;
            process.StartInfo = psi;
            process.EnableRaisingEvents = true;

            process.OutputDataReceived += (sender, e) => { output.AppendLine(e.Data); };
            process.ErrorDataReceived += (sender, e) => { output.AppendLine(e.Data); };

            process.Exited += LongRunnerDone;

            // run the process
            process.Start();

            // start reading output to events
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            _callback(_request, "I'm all over it. I'll let you know the results");
        }

        private void LongRunnerDone(object sender, EventArgs e)
        {
            _callback(_request, "LongRunner is finished. Here's what happened.");
            _finished = true;
            _callback(_request, "```" + output.ToString().Trim() + "```");
        }

        public bool IsFinished()
        {
            return _finished;
        }
    }
}
