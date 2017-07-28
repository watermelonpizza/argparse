using System;
using System.Collections.Generic;
using System.Text;

namespace argparse
{
    public interface ICreateArgumentCatagory
    {
        IBasicArgumentCatagory<TOptions> CreateArgumentCatagory<TOptions>(TOptions options);
    }
}
