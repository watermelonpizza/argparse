using System;
using System.Collections.Generic;
using System.Text;

namespace argparse
{
    public class CommandArgumentParser : ArgumentParser, ICommandArgumentParser
    {
        internal CommandArgumentParser(ArgumentParser parentArgumentParser, ICommand command, ConsoleHelper consoleHelper)
            : base(parentArgumentParser, command, consoleHelper)
        {

        }

        public bool Selected { get; internal set; }
    }
}
