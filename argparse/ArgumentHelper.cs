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

    internal struct ArgumentDetails
    {
        public ArgumentType Type;
        public string Name;
        public char Flag;
        public string Argument;

        public ArgumentDetails(
            ArgumentType type, 
            string name = null,
            char flag = ArgumentHelper.NoFlag,
            string argument = null)
        {
            Type = type;
            Name = name;
            Flag = flag;
            Argument = argument;
        }
    }

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


        public static IEnumerable<ArgumentDetails> ParseArgument(string arg, string nextArg, IEnumerable<string> argNames, IEnumerable<char> argFlags, IEnumerable<string> commands)
        {
            ArgumentType type = GetArgumentType(arg, argNames, argFlags, commands);

            switch (type)
            {
                case ArgumentType.Flag:
                    return new List<ArgumentDetails> { new ArgumentDetails(type, flag: arg[0])}
                case ArgumentType.FlagWithArgument:
                    break;
                case ArgumentType.Flags:
                    break;
                case ArgumentType.FlagsWithUnknown:
                    break;
                case ArgumentType.Name:
                    break;
                case ArgumentType.NameWithArgument:
                    break;
                case ArgumentType.Command:
                    break;
                case ArgumentType.Passthrough:
                    break;
                default:
                case ArgumentType.None:
                    break;
            }
        }

        public static ArgumentType GetArgumentType(string arg, IEnumerable<string> argNames, IEnumerable<char> argFlags, IEnumerable<string> commands)
        {
            if (IsArgument(arg))
            {
                if (arg.StartsWith(WindowsArgumentPrefix))
                {
                    return GetWindowsArgumentType(arg, argNames, argFlags);
                }
                else if (arg.StartsWith(NamePrefix))
                {
                    return GetNameType(arg, argNames);
                }
                else if (arg.StartsWith(FlagPrefix))
                {
                    return GetFlagType(arg, argFlags);
                }
            }
            else if (commands.Contains(arg))
            {
                return ArgumentType.Command;
            }

            return ArgumentType.None;
        }

        public static bool IsArgument(string arg) => ArgumentPrefixes.Any(prefix => arg.StartsWith(prefix));

        public static ArgumentType GetWindowsArgumentType(string arg, IEnumerable<string> argNames, IEnumerable<char> argFlags)
        {
            ArgumentType result = ArgumentType.None;
            string strippedArgument = arg.Substring(WindowsArgumentPrefix.Length);

            result = GetNameType(arg, argNames);

            if (result == ArgumentType.None)
                result = GetFlagType(arg, argFlags);

            return result;
        }

        public static ArgumentType GetNameType(string arg, IEnumerable<string> argNames)
        {
            if (arg == NamePrefix)
                return ArgumentType.Passthrough;

            string strippedArgument = arg.Substring(NamePrefix.Length);

            foreach (string name in argNames)
            {
                if (strippedArgument.StartsWith(name))
                {
                    if (strippedArgument.Length == name.Length)
                    {
                        return ArgumentType.Name;
                    }
                    else if (strippedArgument[name.Length] == WindowsDeliminator ||
                        strippedArgument[name.Length] == Deliminator)
                    {
                        return ArgumentType.NameWithArgument;
                    }
                }
            }

            return ArgumentType.None;
        }

        public static ArgumentType GetFlagType(string arg, IEnumerable<char> argFlags)
        {
            string strippedArgument = arg.Substring(FlagPrefix.Length);

            if (strippedArgument.Length > 1)
            {
                if (Regex.IsMatch(strippedArgument, FlagMatchPattern))
                {
                    ArgumentType type = ArgumentType.Flags;

                    foreach (char flag in strippedArgument)
                    {
                        if (!argFlags.Contains(flag))
                            type = ArgumentType.FlagsWithUnknown;
                    }

                    return type;
                }
                else
                {
                    char flag = strippedArgument[0];

                    if (argFlags.Contains(flag))
                    {
                        if (strippedArgument[1] == WindowsDeliminator ||
                                strippedArgument[1] == Deliminator)
                        {
                            return ArgumentType.FlagWithArgument;
                        }
                    }
                }
            }
            else if (strippedArgument.Length == 1)
            {
                char flag = strippedArgument[0];

                if (argFlags.Contains(flag))
                {
                    return ArgumentType.Flag;
                }
            }

            return ArgumentType.None;
        }
    }
}
