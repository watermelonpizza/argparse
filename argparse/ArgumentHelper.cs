using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace argparse
{
    internal enum ArgumentType
    {
        None,
        Flag,
        FlagWithArgument,
        Flags,
        FlagsWithUnknown,
        Name,
        NameWithArgument,
        Command,
        Passthrough
    }

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
        internal const string NameMatchPattern = "^[a-zA-Z][a-zA-Z0-9-]+$";
        internal const char NoFlag = char.MinValue;

        internal const string WindowsArgumentPrefix = "/";
        internal const string FlagPrefix = "-";
        internal const string NamePrefix = "--";

        internal const char WindowsDeliminator = ':';
        internal const char Deliminator = '=';

        private static readonly string[] ArgumentPrefixes = new string[] { WindowsArgumentPrefix, FlagPrefix, NamePrefix };

        public static string[] StripApplication(params string[] args)
        {
            return args.Skip(1).ToArray();
        }

        public static ArgumentType GetArgumentType(string arg, IEnumerable<string> argNames, IEnumerable<char> argFlags, IEnumerable<string> commands)
        {
            if (IsArgument(arg))
            {
                var strippedArgument = StripArgument(arg);

                if (strippedArgument.prefix == WindowsArgumentPrefix)
                {
                    return GetWindowsArgumentType(strippedArgument.argument, argNames, argFlags);
                }
                else if (strippedArgument.prefix == NamePrefix && string.IsNullOrEmpty(strippedArgument.argument))
                {
                    return ArgumentType.Passthrough;
                }
                else if (strippedArgument.prefix == NamePrefix)
                {
                    return GetNameType(strippedArgument.argument, argNames);
                }
                else if (strippedArgument.prefix == FlagPrefix)
                {
                    return GetFlagType(strippedArgument.argument, argFlags);
                }
            }
            else if (commands.Contains(arg))
            {
                return ArgumentType.Command;
            }

            return ArgumentType.None;
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

        public static ArgumentType GetWindowsArgumentType(string arg, IEnumerable<string> argNames, IEnumerable<char> argFlags)
        {
            ArgumentType result = ArgumentType.None;

            result = GetNameType(arg, argNames);

            if (result == ArgumentType.None)
                result = GetFlagType(arg, argFlags);

            return result;
        }

        public static ArgumentType GetNameType(string arg, IEnumerable<string> argNames)
        {
            foreach (string name in argNames)
            {
                if (arg.StartsWith(name))
                {
                    if (arg.Length == name.Length)
                    {
                        return ArgumentType.Name;
                    }
                    else if (arg[name.Length] == WindowsDeliminator ||
                        arg[name.Length] == Deliminator)
                    {
                        return ArgumentType.NameWithArgument;
                    }
                }
            }

            return ArgumentType.None;
        }

        public static ArgumentType GetFlagType(string arg, IEnumerable<char> argFlags)
        {
            if (arg.Length > 1)
            {
                if (Regex.IsMatch(arg, FlagMatchPattern))
                {
                    ArgumentType type = ArgumentType.Flags;

                    foreach (char flag in arg)
                    {
                        if (!argFlags.Contains(flag))
                            type = ArgumentType.FlagsWithUnknown;
                    }

                    return type;
                }
                else
                {
                    char flag = arg[0];

                    if (argFlags.Contains(flag))
                    {
                        if (arg[1] == WindowsDeliminator ||
                                arg[1] == Deliminator)
                        {
                            return ArgumentType.FlagWithArgument;
                        }
                    }
                }
            }
            else if (arg.Length == 1)
            {
                char flag = arg[0];

                if (argFlags.Contains(flag))
                {
                    return ArgumentType.Flag;
                }
            }

            return ArgumentType.None;
        }
    }
}
