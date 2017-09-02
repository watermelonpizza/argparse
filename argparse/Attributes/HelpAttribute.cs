using System;
using System.Collections.Generic;
using System.Text;

namespace argparse.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class HelpAttribute : Attribute
    {
        public readonly string Help;

        public HelpAttribute(string helpText)
        {
            Help = helpText;
        }
    }
}
