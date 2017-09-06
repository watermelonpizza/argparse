using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Text;

namespace argparse
{
    public interface IWithArgument<TOptions>
    {
        IArgument<TOptions, TArgument> WithArgument<TArgument>(Expression<Func<TOptions, TArgument>> argument);
        IArgument<TOptions, TArgument> WithMultiArgument<TArgument>(Expression<Func<TOptions, ImmutableArray<TArgument>>> argument);
    }
}
