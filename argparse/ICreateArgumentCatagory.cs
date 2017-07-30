using System;
using System.Collections.Generic;
using System.Text;

namespace argparse
{
    public interface ICreateArgumentCatagory
    {
        IArgumentCatagory<TOptions> CreateArgumentCatagory<TOptions>() where TOptions : class, new();

        TOptions GetArgumentCatagory<TOptions>() where TOptions : class, new();
    }
}
