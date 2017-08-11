using System;
using System.Collections.Generic;
using System.Text;

namespace argparse
{
    public interface ICommandArgumentParser : IArgumentParser
    {
        /// <summary>
        /// Whether or not the command was called
        /// </summary>
        bool Selected { get; }
    }
}
