using System;
using System.Collections.Generic;
using System.Text;

namespace argparse
{
    public class CommandArgumentParser : ArgumentParser, ICommandArgumentParser
    {
        internal CommandArgumentParser(ArgumentParser parentArgumentParser, ICommand command)
            : base(parentArgumentParser, command)
        {

        }

        public bool Selected { get; internal set; }
    }
}
