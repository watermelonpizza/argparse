using System;
using System.Collections.Generic;
using System.Text;

namespace argparse
{
    public class BasicArgumentParser : IBasicArgumentParser
    {
        public IBasicArgumentCatagory<TOptions> CreateArgumentCatagory<TOptions>(TOptions options)
        {
            throw new NotImplementedException();
        }

        public IRestrictedBasicArgumentParser CreateMultiPositionalArgument()
        {
            throw new NotImplementedException();
        }

        public IBasicArgumentParser CreatePositionalArgument()
        {
            throw new NotImplementedException();
        }
    }
}
