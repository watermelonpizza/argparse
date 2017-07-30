using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace argparse
{
    public interface IWithCommand<TOptions>
        where TOptions : class, new()
    {
        ICommand<TOptions> WithCommand(Expression<Func<TOptions, bool>> command);

        ICommand<TOptions> WithCommand(Expression<Func<TOptions, IArgumentParser>> command, Action<IArgumentParser> parser);
    }
}
