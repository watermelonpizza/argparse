using System;
using System.Collections.Generic;
using System.Text;

namespace argparse.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class NameAttribute : Attribute
    {
        public readonly string Name;
        
        public NameAttribute(string name)
        {
            Name = name;
        }
    }
}
