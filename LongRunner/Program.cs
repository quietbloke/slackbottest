using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LongRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("LongRunner");
            var waittime = 5;

            if (args.Length == 1)
            {
                waittime = Convert.ToInt32(args[0]);
            }

            Console.WriteLine("Waiting for : {0:D} seconds", waittime);

            for (var tick = 0; tick < waittime; tick++)
            {
                Thread.Sleep(1000);
                Console.Write(".", tick+1);
            }
            Console.WriteLine("");
            Console.WriteLine("TotalTime spent : {0:D}", waittime);
        }
    }
}
