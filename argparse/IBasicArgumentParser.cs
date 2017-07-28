using System;
using System.Collections.Generic;
using System.Text;

namespace argparse
{
    public interface IBasicArgumentParser : IRestrictedBasicArgumentParser
    {
        IBasicArgumentParser CreatePositionalArgument();
    }
}
