using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace argparse
{
    public interface IWithParameter<TArgumentOptions>
    {
        IParameter<TArgumentOptions, TArgument> WithParameter<TArgument>(Expression<Func<TArgumentOptions, TArgument>> argument);
    }
}
