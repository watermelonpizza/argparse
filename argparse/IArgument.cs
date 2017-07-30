using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace argparse
{
    public interface IArgument
    {
        string ArgumentName { get; }

        char ArgumentFlag { get; }

        bool IsRequired { get; }

        bool IsCountable { get; }

        bool IsMultiple { get; }

        object ArgumentDefaultValue { get; }

        string ArgumentHelp { get; }

        PropertyInfo Property { get; }
    }

    public interface IArgument<TOptions, TArgument> : IArgument, IWithArgument<TOptions>, ICreateArgumentCatagory
    {
        IArgument<TOptions, TArgument> Name(string name);

        IArgument<TOptions, TArgument> Flag(char flag);

        IArgument<TOptions, TArgument> Required();

        IArgument<TOptions, TArgument> Countable();

        IArgument<TOptions, TArgument> DefaultValue(TArgument value);

        IArgument<TOptions, TArgument> Help(string help);
    }
}
