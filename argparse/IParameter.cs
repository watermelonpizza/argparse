﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace argparse
{
    public interface IParameter : IComparable<IParameter>
    {
        string ParameterName { get; }

        uint Position { get; }

        Type ParameterType { get; }

        bool IsRequired { get; }

        bool IsMultiple { get; }

        string ParamterHelp { get; }

        PropertyInfo Property { get; }
    }

    public interface IParameter<TArgumentOptions, TArgument> : IParameter, IWithParameter<TArgumentOptions>, ICreateParameterCatagory
    {
        IParameter<TArgumentOptions, TArgument> Name(string name);

        IParameter<TArgumentOptions, TArgument> Required();

        IParameter<TArgumentOptions, TArgument> Help(string help);
    }
}
