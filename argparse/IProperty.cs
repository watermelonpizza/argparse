using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace argparse
{
    internal interface IProperty
    {
        PropertyInfo Property { get; }

        void AddIfMultiple(object obj);

        void SetValue(object obj);

        object GetValue();
    }
}
