using System;
using Oxide.Core.Logging;

namespace Oxide.GettingOverIt.Loggers
{
    internal class ConsoleLogger : Logger
    {
        public ConsoleLogger() : base(true)
        {
        }

        protected override void ProcessMessage(LogMessage message)
        {
            string date = DateTime.Now.ToString("HH:mm:ss");
            string prefix = $"[{message.Type}] {date} ";
            Console.WriteLine(prefix + message.ConsoleMessage);
        }
    }
}
