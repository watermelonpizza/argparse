using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace argparse
{
    public static class ArgumentParser
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

        /// <summary>
        /// Sets the mode for this argument parser
        /// </summary>
        public static ParserMode ParserMode { get; set; }

        public static void AddArguments<T>()
        {

        }

        public static void Add<TOptions, TArgument>(Expression<Func<TOptions, TArgument>> exp)
            where TOptions : class, new()
            where TArgument : struct
        {
            IList<string> s;
        }

        public static void AddPostiionalArgument<>

        public static IBasicArgumentParser ForScope(string scope)
        {

        }

        public static 

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
