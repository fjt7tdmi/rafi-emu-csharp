using System;
using System.Collections.Generic;

namespace Rafi
{
    internal enum LogLevel: int
    {
        None = 0,
        Info = 1,
        Trace = 2,
    }

    internal class Logger
    {
        private static IDictionary<string, LogLevel> logLevels = new Dictionary<string, LogLevel>();

        private readonly string module;

        static Logger()
        {
            logLevels.Add("cpu", LogLevel.Info);
            logLevels.Add("gdb", LogLevel.Trace);
        }

        public Logger(string module)
        {
            this.module = module;
        }

        private LogLevel GetLogLevel()
        {
            if (logLevels.ContainsKey(module))
            {
                return logLevels[module];
            }

            return LogLevel.None;
        }
        
        private void Print(string message)
        {
            Console.WriteLine($"[{module}] {message}");
        }

        public void Info(string message)
        {
            if (GetLogLevel() >= LogLevel.Info)
            {
                Print(message);
            }
        }

        public void Trace(string message)
        {
            if (GetLogLevel() >= LogLevel.Trace)
            {
                Print(message);
            }
        }
    }
}
