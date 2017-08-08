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
        internal const string FlagMatchPattern = "^[A-Za-z0-9]$";
        internal const string NameMatchPattern = "^[a-z0-9][a-z0-9-]+$";
        internal const string CommandMatchPattern = "^[A-Za-z0-9]+$";
        internal const string DefaultModuleMatchPattern = "^[A-Za-z0-9_]+$";
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

        public static string FormatModuleName(string moduleName)
        {
            if (!Regex.IsMatch(moduleName, DefaultModuleMatchPattern))
            {
                throw new ArgumentException($"{nameof(moduleName)} must only be an alphanumeric and underscore charaters. To set module to custom name use the Name function. Match pattern: {ArgumentHelper.DefaultModuleMatchPattern}", nameof(moduleName));
            }

            // https://stackoverflow.com/a/3103795
            Regex r = new Regex(@"(?<=[A-Z])(?=[A-Z][a-z])|(?<=[^A-Z])(?=[A-Z])|(?<=[A-Za-z])(?=[^A-Za-z])");

            // Remove any '_' characters
            return string.Join(" ", r.Split(moduleName).Where(x => x != "_").Select(x => x.Trim(' ', '_')));
        }

        public static string FormatCommandName(string commandName)
        {
            if (!Regex.IsMatch(commandName, CommandMatchPattern))
            {
                throw new ArgumentException($"{nameof(commandName)} can only ever be alphanumeric charaters. Match pattern: {CommandMatchPattern}", nameof(commandName));
            }

            return commandName.ToLowerInvariant();
        }

        public static string DefaultCatagoryToString(string catagoryName)
        {
            return FormatModuleName(catagoryName);
        }

        public static string DefaultParameterToString(string parameterName)
        {
            return FormatModuleName(parameterName).ToUpperInvariant();
        }

        public static string DefaultArgumentToString(string argumentName)
        {
            return FormatModuleName(argumentName).Replace(" ", "-").ToLowerInvariant();
        }
    }
}
