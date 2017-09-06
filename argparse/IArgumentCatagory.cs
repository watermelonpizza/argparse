using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Text;

namespace argparse
{
    public interface IArgumentCatagory
    {
        ImmutableArray<IArgument> Arguments { get; }

        string CatagoryName { get; }
    }

    public interface IArgumentCatagory<TOptions> : IArgumentCatagory, IWithArgument<TOptions>
    {
        IArgumentCatagory<TOptions> Name(string name);
    }
}
