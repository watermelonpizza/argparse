using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace argparse
{
    public interface IArgumentCatagory
    {
        IEnumerable<IArgument> Arguments { get; }

        string CatagoryName { get; }

        object CatagoryInstance { get; }
    }

    public interface IArgumentCatagory<TOptions> : IArgumentCatagory, IWithArgument<TOptions>
    {
        IArgumentCatagory<TOptions> Name(string name);
    }
}
