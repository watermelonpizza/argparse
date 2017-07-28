using System;
using System.Collections.Generic;
using System.Text;

namespace argparse
{
    public interface IArgument<TOptions, TArgument> : IWithArgument<TOptions>, ICreateArgumentCatagory
    {
        IArgument<TOptions, TArgument> Name(string name);

        IArgument<TOptions, TArgument> Flag(char flag);

        IArgument<TOptions, TArgument> Required();

        IArgument<TOptions, TArgument> Countable();

        IArgument<TOptions, TArgument> DefaultValue(TArgument value);

        IArgument<TOptions, TArgument> Help(string help);
    }
}
