using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace argparse
{
    public interface ICommandCatagory
    {
        IEnumerable<ICommand> Commands { get; }

        string CommandCatagoryName { get; }

        object CatagoryInstance { get; }
    }

    public interface ICommandCatagory<TOptions> : ICommandCatagory, IWithCommand<TOptions>
        where TOptions : class, new()
    {
        ICommandCatagory<TOptions> Name(string name);
    }
}
