using System;
using System.Collections.Generic;
using System.Text;

namespace argparse
{
    public interface IBasicArgumentParser
    {
        IBasicArgumentParser Catagory(string catagoryName);

        void AddArguments<T>();
        void AddPositionalArgument<TArgumentType>(string description, string help = "", uint position = 0);
        void AddPositionalArgument<TArgumentType, IEnumerable<>>(string description, string help = "", uint position = 0);
        
        GetPositionalArgument<>
    }
}
