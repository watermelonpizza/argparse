using System;
using System.Collections.Generic;
using System.Text;

namespace argparse
{
    public interface IArgumentParser : ICreateParameterCatagory, ICreateArgumentCatagory, ICreateCommandCatagory
    {
        string[] Passthrough { get; }
    }
}
