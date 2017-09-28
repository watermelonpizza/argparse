using System;
using System.Collections.Generic;
using System.Text;

namespace argparse
{
    public class ConsoleHelper
    {
        ArgumentParserOptions _options;

        internal ConsoleHelper(ArgumentParserOptions options)
        {
            _options = options;
        }

        public void Write(string str)
        {
            _options.StdOut.Write(str);
        }

        public void WriteLine()
        {
            _options.StdOut.WriteLine();
        }

        public void WriteLine(string str)
        {
            _options.StdOut.WriteLine(str);
        }

        public void WriteVerbose(string verbose)
        {
            if (_options.ConsoleLogLevel <= ConsoleLogLevel.Verbose)
            {
                PrefixLine(ConsoleLogLevel.Verbose, false);
                _options.StdOut.WriteLine(verbose);
                Console.ResetColor();
            }
        }

        public void WriteInfo(string info)
        {
            if (_options.ConsoleLogLevel <= ConsoleLogLevel.Info)
            {
                PrefixLine(ConsoleLogLevel.Info);
                _options.StdOut.WriteLine(info);
            }
        }

        public void WriteWarning(string warning)
        {
            if (_options.ConsoleLogLevel <= ConsoleLogLevel.Warn)
            {
                PrefixLine(ConsoleLogLevel.Warn);
                _options.StdOut.WriteLine(warning);
            }
        }

        public void WriteError(string error)
        {
            if (_options.ConsoleLogLevel <= ConsoleLogLevel.Error)
            {
                PrefixLine(ConsoleLogLevel.Error);
                _options.StdErr.WriteLine(error);
            }
        }

        public void WriteFatal(string fatal)
        {
            if (_options.ConsoleLogLevel <= ConsoleLogLevel.Fatal)
            {
                PrefixLine(ConsoleLogLevel.Fatal);
                _options.StdErr.WriteLine(fatal);
            }
        }

        private void PrefixLine(ConsoleLogLevel level, bool reset = true)
        {
            switch (level)
            {
                case ConsoleLogLevel.Verbose:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    _options.StdOut.Write($"{_options.ApplicationName}: verbose: ");
                    break;
                case ConsoleLogLevel.Info:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    _options.StdOut.Write($"{_options.ApplicationName}: info: ");
                    break;
                case ConsoleLogLevel.Warn:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    _options.StdOut.Write($"{_options.ApplicationName}: warning: ");
                    break;
                case ConsoleLogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    _options.StdErr.Write($"{_options.ApplicationName}: error: ");
                    break;
                case ConsoleLogLevel.Fatal:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Red;
                    _options.StdErr.Write($"{_options.ApplicationName}: fatal: ");
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
