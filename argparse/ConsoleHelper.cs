using System;
using System.Collections.Generic;
using System.Text;

namespace argparse
{
    public static class ConsoleHelper
    {
        public static void WriteVerbose(string verbose)
        {
            PrefixLevel(Level.Verbose, false);
            Console.WriteLine(verbose);
            Console.ResetColor();
        }

        public static void WriteInfo(string info)
        {
            PrefixLevel(Level.Info);
            Console.WriteLine(info);
        }

        public static void WriteWarning(string warning)
        {
            PrefixLevel(Level.Warn);
            Console.WriteLine(warning);
        }

        public static void WriteError(string error)
        {
            PrefixLevel(Level.Error);
            Console.Error.WriteLine(error);
        }

        public static void WriteFatal(string fatal)
        {
            PrefixLevel(Level.Fatal);
            Console.Error.WriteLine(fatal);
        }

        private static void PrefixLevel(Level level, bool reset = true)
        {
            switch (level)
            {
                case Level.Verbose:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("verb: ");
                    break;
                case Level.Info:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("info: ");
                    break;
                case Level.Warn:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("warn: ");
                    break;
                case Level.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.Write("err!: ");
                    break;
                case Level.Fatal:
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

        private enum Level
        {
            Verbose,
            Info,
            Warn,
            Error,
            Fatal
        }
    }
}
