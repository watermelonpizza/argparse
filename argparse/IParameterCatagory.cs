using System;
using System.Collections.Generic;
using System.Text;

namespace argparse
{
    public interface IParameterCatagory
    {
        IEnumerable<IParameter> Parameters { get; }

        uint PositionStart { get; }

        string CatagoryName { get; }
    }

    public interface IParameterCatagory<TArgumentOptions> : IParameterCatagory, IWithParameter<TArgumentOptions>
    {
        IParameterCatagory<TArgumentOptions> Name(string name);
    }
}
