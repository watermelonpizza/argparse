﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Text;

namespace argparse
{
    public interface ICommandCatagory
    {
        ImmutableArray<ICommand> Commands { get; }

        string CatagoryName { get; }
    }

    public interface ICommandCatagory<TOptions> : ICommandCatagory, IWithCommand<TOptions>
        where TOptions : class, new()
    {
        ICommandCatagory<TOptions> Name(string name);
    }
}
