using System;
using System.Collections.Generic;
using System.Text;

namespace argparse
{
    public class ConsoleHelper
    {
        ConsoleLogLevel _minConsoleLogLevel;

        internal ConsoleHelper(ConsoleLogLevel minimumConsoleLogLevel)
        {
            _minConsoleLogLevel = minimumConsoleLogLevel;
        }

        public void WriteVerbose(string verbose)
        {
            if (_minConsoleLogLevel <= ConsoleLogLevel.Verbose)
            {
                PrefixLevel(ConsoleLogLevel.Verbose, false);
                Console.WriteLine(verbose);
                Console.ResetColor();
            }
        }

        public void WriteInfo(string info)
        {
            if (_minConsoleLogLevel <= ConsoleLogLevel.Info)
            {
                PrefixLevel(ConsoleLogLevel.Info);
                Console.WriteLine(info);
            }
        }

        public void WriteWarning(string warning)
        {
            if (_minConsoleLogLevel <= ConsoleLogLevel.Warn)
            {
                PrefixLevel(ConsoleLogLevel.Warn);
                Console.WriteLine(warning);
            }
        }

        public void WriteError(string error)
        {
            if (_minConsoleLogLevel <= ConsoleLogLevel.Error)
            {
                PrefixLevel(ConsoleLogLevel.Error);
                Console.Error.WriteLine(error);
            }
        }

        public void WriteFatal(string fatal)
        {
            if (_minConsoleLogLevel <= ConsoleLogLevel.Fatal)
            {
                PrefixLevel(ConsoleLogLevel.Fatal);
                Console.Error.WriteLine(fatal);
            }
        }

        private void PrefixLevel(ConsoleLogLevel level, bool reset = true)
        {
            switch (level)
            {
                case ConsoleLogLevel.Verbose:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("verb: ");
                    break;
                case ConsoleLogLevel.Info:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("info: ");
                    break;
                case ConsoleLogLevel.Warn:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("warn: ");
                    break;
                case ConsoleLogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.Write("err!: ");
                    break;
                case ConsoleLogLevel.Fatal:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Error.Write("fatl: ");
                    break;
                default:
                    throw new Exception("Unknown Console Level");
            }

            if (reset)
                Console.ResetColor();
        }

    }

    public enum ConsoleLogLevel
    {
        Verbose = 0,
        Info = 1,
        Warn = 2,
        Error = 4,
        Fatal = 8,
        None = 256
    }
}
