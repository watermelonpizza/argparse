using System;
using System.Collections.Generic;
using System.Text;

namespace argparse
{
    public class CommandArgumentParser : ArgumentParser, ICommandArgumentParser
    {
        public bool Selected { get; internal set; }
    }
}
