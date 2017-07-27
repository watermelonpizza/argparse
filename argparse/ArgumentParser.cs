using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace argparse
{
    public class ArgumentParser
    {
        //new BasicArgumentParser();
        // No such thing as scoped commands. Cannot have multiple 'multiple' positional arguments.
        // Last positional argument can be the only one with 'multiple' set to true
        // Can mix and match options throughout the command

        // Usage: app [OPTIONS] FILE [FILE...]
        //        app [OPTIONS] SOURCE DESTINATION
        //        app [OPTIONS] SOURCE DESTINATION [DESTINATION...]
        // app [opt] [source ] [ dest  ] [                            opt                      ] [  dest  ]
        // app -f -v file1.txt file3.txt -vvpoD --long-name-argument --argument-with-value value file46.txt

        //new ScopedArgumentParser();
        // Only scoped arguments allowed. First positional must be global scope arguments, second positional must be scope name.
        // After scope name all arguments are arguments for that scope.
        // Cannot mix and match scope-name/global arguments.
        // Can mix and match scope arguments. (After scope name is declared arguments after scope is a BasicArgumentParser)

        // Usage: app [OPTIONS] SCOPE [BasicArgumentParser]

        /// 

        // All 

        public static readonly IReadOnlyCollection<Type> SupportedTypes = new Type[]
        {
            typeof(bool),
            typeof(byte),
            typeof(sbyte),
            typeof(short),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(char),
            typeof(string),
            typeof(DateTime),
            typeof(Enum),
            typeof(IEnumerable<bool>),
            typeof(IEnumerable<byte>),
            typeof(IEnumerable<sbyte>),
            typeof(IEnumerable<short>),
            typeof(IEnumerable<int>),
            typeof(IEnumerable<uint>),
            typeof(IEnumerable<long>),
            typeof(IEnumerable<ulong>),
            typeof(IEnumerable<float>),
            typeof(IEnumerable<double>),
            typeof(IEnumerable<decimal>),
            typeof(IEnumerable<char>),
            typeof(IEnumerable<string>),
            typeof(IEnumerable<DateTime>),
            typeof(IEnumerable<Enum>)
        };
    }
}
