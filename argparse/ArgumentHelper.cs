using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace argparse
{
    //internal enum ArgumentType
    //{
    //    None,
    //    Flag,
    //    FlagWithArgument,
    //    Flags,
    //    FlagsWithUnknown,
    //    Name,
    //    NameWithArgument,
    //    Command,
    //    Passthrough
    //}

    //internal struct ArgumentDetails
    //{
    //    public ArgumentType Type;
    //    public string Name;
    //    public char Flag;
    //    public string Argument;

    //    public ArgumentDetails(
    //        ArgumentType type, 
    //        string name = null,
    //        char flag = ArgumentHelper.NoFlag,
    //        string argument = null)
    //    {
    //        Type = type;
    //        Name = name;
    //        Flag = flag;
    //        Argument = argument;
    //    }
    //}

    internal static class ArgumentHelper
    {
        internal const string FlagMatchPattern = "^[a-zA-Z0-9]$";
        internal const string NameMatchPattern = "^[a-z0-9][a-z0-9-]+$";
        internal const char NoFlag = char.MinValue;

        internal const string WindowsArgumentPrefix = "/";
        internal const string FlagPrefix = "-";
        internal const string NamePrefix = "--";

        internal const char WindowsDeliminator = ':';
        internal const char Deliminator = '=';

        internal static readonly string[] HelpArguments = new string[] { "-h", "--help", "/help", "/h", "/?" };

        private static readonly string[] ArgumentPrefixes = new string[] { WindowsArgumentPrefix, FlagPrefix, NamePrefix };

        public static string[] StripApplication(params string[] args)
        {
            return args.Skip(1).ToArray();
        }

        public static bool IsArgument(string arg) => ArgumentPrefixes.Any(prefix => arg.StartsWith(prefix));

        public static (string prefix, string argument) StripArgument(string arg)
        {
            if (arg.StartsWith(WindowsArgumentPrefix))
            {
                return (WindowsArgumentPrefix, arg.Substring(WindowsArgumentPrefix.Length));
            }
            else if (arg == NamePrefix)
            {
                return (NamePrefix, string.Empty);
            }
            else if (arg.StartsWith(NamePrefix))
            {
                return (NamePrefix, arg.Substring(NamePrefix.Length));
            }
            else if (arg.StartsWith(FlagPrefix))
            {
                return (FlagPrefix, arg.Substring(FlagPrefix.Length));
            }

            return (null, arg);
        }
    }
}
