using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace argparse
{
    internal interface IProperty
    {
        PropertyInfo Property { get; }

        void SetValue(object obj);

        object GetValue();
    }

    internal interface IMultiProperty : IProperty
    {
        void AddValue(object obj);
    }
}
