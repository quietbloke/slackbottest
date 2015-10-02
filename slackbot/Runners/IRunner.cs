using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace slackbot.Runners
{
    public interface IRunner
    {
        bool IsFinished();
    }
}
