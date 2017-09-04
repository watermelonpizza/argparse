using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace argparse
{
    public interface ICommand
    {
        string CommandName { get; }

        string CommandHelp { get; }

        string CommandSummary { get; }

        PropertyInfo Property { get; }
    }

    public interface ICommand<TOptions> : ICommand, IWithCommand<TOptions>, ICreateCommandCatagory
        where TOptions : class, new()
    {
        ICommand<TOptions> Name(string name);

        ICommand<TOptions> Help(string help);

        ICommand<TOptions> Summary(string summary);
    }
}
