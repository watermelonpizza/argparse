using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace argparse
{
    /// <summary>
    /// Standard options for the argument parser
    /// </summary>
    public class ArgumentParserOptions
    {
        public string ApplicationName { get; set; } = null;

        public string ApplicationDescription { get; set; } = null;

        public string Preamble { get; set; } = null;

        public ConsoleLogLevel ConsoleLogLevel { get; set; } = ConsoleLogLevel.Warn;

        public bool StrictParsing { get; set; } = true;

        public bool WriteBasicHelpOnEmptyArguments { get; set; } = false;

        public TextWriter StdOut { get; set; } = Console.Out;

        public TextWriter StdErr { get; set; } = Console.Error;
    }
}
