using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace argparse
{
    public interface IBasicArgumentCatagory<TOptions> : IWithArgument<TOptions>
    {
        IBasicArgumentCatagory<TOptions> CatagoryName(string name);
    }
}
