using System;
using System.Collections.Generic;
using System.Text;

namespace argparse
{
    public interface ICreateCommandCatagory
    {
        ICommandCatagory<TOptions> CreateCommandCatagory<TOptions>() where TOptions : class, new();

        TOptions GetCommandCatagory<TOptions>() where TOptions : class, new();
    }
}
