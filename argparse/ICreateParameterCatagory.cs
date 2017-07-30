using System;
using System.Collections.Generic;
using System.Text;

namespace argparse
{
    public interface ICreateParameterCatagory
    {
        IParameterCatagory<TOptions> CreateParameterCatagory<TOptions>() where TOptions : class, new();

        TOptions GetParameterCatagory<TOptions>() where TOptions : class, new();
    }
}
