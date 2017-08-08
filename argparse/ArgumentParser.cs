using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace argparse
{
    public class ArgumentParser : IArgumentParser
    {
        public static ArgumentParser Default { get; } = new ArgumentParser();

        public string[] Passthrough { get; private set; }

        private List<IArgumentCatagory> _argumentCatagories = new List<IArgumentCatagory>();
        private List<IParameterCatagory> _paramterCatagories = new List<IParameterCatagory>();
        private List<ICommandCatagory> _commandCatagories = new List<ICommandCatagory>();

        public IArgumentCatagory<TOptions> CreateArgumentCatagory<TOptions>()
            where TOptions : class, new()
        {
            var argumentCatagory = new ArgumentCatagory<TOptions>(this, typeof(TOptions).Name);
            _argumentCatagories.Add(argumentCatagory);

            return argumentCatagory;
        }

        public TOptions GetArgumentCatagory<TOptions>()
            where TOptions : class, new()
        {
            try
            {
                var cat = _argumentCatagories.OfType<ArgumentCatagory<TOptions>>().SingleOrDefault();

                return (TOptions)cat.CatagoryInstance;
            }
            catch (InvalidOperationException)
            {

                throw;
            }
        }

        public IParameterCatagory<TOptions> CreateParameterCatagory<TOptions>()
            where TOptions : class, new()
        {
            IParameterCatagory<TOptions> parameterCatagory
                = new ParameterCatagory<TOptions>(this, typeof(TOptions).Name);

            _paramterCatagories.Add(parameterCatagory);
            return parameterCatagory;
        }

        public TOptions GetParameterCatagory<TOptions>()
            where TOptions : class, new()
        {
            return (TOptions)
                (_paramterCatagories.SingleOrDefault(pac => pac is ParameterCatagory<TOptions>) as ParameterCatagory<TOptions>)
                    ?.CatagoryInstance;
        }

        public ICommandCatagory<TOptions> CreateCommandCatagory<TOptions>()
            where TOptions : class, new()
        {
            ICommandCatagory<TOptions> commandCatagory = new CommandCatagory<TOptions>(this, typeof(TOptions).Name);

            _commandCatagories.Add(commandCatagory);
            return commandCatagory;
        }

        public TOptions GetCommandCatagory<TOptions>()
            where TOptions : class, new()
        {
            return (TOptions)
                (_commandCatagories.SingleOrDefault(cc => cc is CommandCatagory<TOptions>) as CommandCatagory<TOptions>)
                    ?.CatagoryInstance;
        }

        public void WriteHelp()
        {
            // TODO: Help writing logic here. 
            // Probably need helper console class here for formatting
        }

        public void Parse(params string[] args)
        {
            args = ArgumentHelper.StripApplication(args);

            // Keep a record of the parameters found so far 
            // so we can add them to the correct parameter position
            List<string> parameterFound = new List<string>();

            // First check for static help argument of -h, --help, /help etc.
            // And ensure anything before help argumnt is not a command. 
            // If it is call help on the command.
            if (args.Any(arg => ArgumentHelper.HelpArguments.Contains(arg)))
            {
                // TODO: Check for help postition and call on command if nessasary

                return;
            }

            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                string nextArg = i + 1 < args.Length ? args[i + 1] : null;
                string[] nextAllArgs = args.Skip(i + 1).ToArray();

                bool nextArgIsArgument = nextArg != null ? ArgumentHelper.IsArgument(nextArg) : false;

                var strippedArgument = ArgumentHelper.StripArgument(arg);

                // If it is an argument, get the argument if exists
                // Attempt to convert argument to target type and set value
                // or count/multiple if the case may be
                if (ArgumentHelper.IsArgument(arg))
                {
                    var strip = ArgumentHelper.StripArgument(arg);

                    if (strip.prefix == ArgumentHelper.WindowsArgumentPrefix)
                    {
                        var findNameResult = FindNameAndSetProperty(strip, nextArg, nextArgIsArgument);

                        if (findNameResult.success)
                        {
                            if (findNameResult.nextArgumentUsed)
                                i++;
                        }
                        else
                        {
                            var findFlagResult = FindFlagAndSetProperty(strip, nextArg, nextArgIsArgument);

                            if (findFlagResult.success && findFlagResult.nextArgumentUsed)
                                i++;
                        }
                    }
                    // Passthrough argument has been defined. Anything after this isn't for us.
                    // Set the rest of the arguments to the gloabal Passthrough variable and return.
                    // The user can check the Passthrough property if the command (if we are in one)
                    // or something needs external arguments
                    else if (strip.prefix == ArgumentHelper.NamePrefix && string.IsNullOrEmpty(strip.argument))
                    {
                        Passthrough = nextAllArgs;
                        return;
                    }
                    else if (strip.prefix == ArgumentHelper.NamePrefix)
                    {
                        var findNameResult = FindNameAndSetProperty(strip, nextArg, nextArgIsArgument);

                        if (findNameResult.success && findNameResult.nextArgumentUsed)
                            i++;
                    }
                    else if (strip.prefix == ArgumentHelper.FlagPrefix)
                    {
                        var findFlagResult = FindFlagAndSetProperty(strip, nextArg, nextArgIsArgument);

                        if (findFlagResult.success && findFlagResult.nextArgumentUsed)
                            i++;
                    }
                }
                // This is either a command or a parameter 
                else
                {
                    ICommand command =
                        _commandCatagories
                            .SelectMany(cc => cc.Commands)
                            .SingleOrDefault(c => c.CommandName == arg);

                    // If it's a command, look for the command and parse arguments on that command
                    if (command != null)
                    {
                        IProperty property = command as IProperty;

                        // Now set the sub argument parser to parse the rest of the arguments
                        // Or if it is a bool type then set it to true and we are done
                        if (property.Property.PropertyType == typeof(IArgumentParser))
                        {
                            ArgumentParser commandParser = new ArgumentParser();
                            property.SetValue(commandParser);

                            commandParser.Parse(nextAllArgs);
                            return;
                        }
                        else if (property.Property.PropertyType == typeof(bool))
                        {
                            property.SetValue(true);

                            // TODO: Warn user about rest of argument if there are any there 
                            // that they are not being read because this command is a bool type

                            return;
                        }
                        else
                        {
                            // TODO: Throw exception, commands cannot be some other type than bool or IArgumentParser
                        }
                    }
                    // Must be a parameter
                    else
                    {
                        var parameters = _paramterCatagories.SelectMany(pc => pc.Parameters);

                        // If there are any parameters, look for the position we are at
                        if (parameters.Any())
                        {
                            // Get the current positioned paramter based on how many we have found
                            IParameter parameter = parameters.SingleOrDefault(p => p?.Position == parameterFound.Count);

                            // We found it so set that parameter
                            if (parameter != null)
                            {
                                IProperty property = parameter as IProperty;

                                if (parameter.IsMultiple)
                                {
                                    try
                                    {
                                        // Call the internal add method to add to the property
                                        var convertedType = Convert.ChangeType(strippedArgument.argument, parameter.ParameterType);
                                        property.AddIfMultiple(convertedType);
                                    }
                                    catch (Exception)
                                    {
                                        // TODO: Catch exception and let users know of why failed to cast.
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        var convertedType = Convert.ChangeType(strippedArgument.argument, parameter.ParameterType);
                                        property.SetValue(convertedType);
                                    }
                                    catch (Exception)
                                    {
                                        // TODO: Catch exception and let users know of why failed to cast.
                                    }
                                }
                            }
                            // We didn't find the parameter at the required position
                            else
                            {
                                // Check if the last positioned parameter is multi, then we can add to that one
                                parameter = parameters.Single(p => p.Position == parameters.Max(par => par.Position));

                                IProperty property = parameter as IProperty;

                                if (parameter.IsMultiple)
                                {
                                    try
                                    {
                                        // Call the internal add method to add to the property
                                        var convertedType = Convert.ChangeType(strippedArgument.argument, parameter.ParameterType);
                                        property.AddIfMultiple(convertedType);
                                    }
                                    catch (Exception)
                                    {
                                        // TODO: Catch exception and let users know of why failed to cast.
                                    }
                                }
                                else
                                {
                                    // TODO: Can't set last parameter as it is not multiple/no parameter found at position parametersFound.Count
                                }
                            }
                        }
                    }
                }
            }
        }

        private (bool success, bool nextArgumentUsed) FindNameAndSetProperty((string prefix, string argument) arg, string nextArg, bool nextArgIsArgument)
        {
            IArgument argument =
                 _argumentCatagories
                     .SelectMany(ac => ac.Arguments)
                     .SingleOrDefault(a => a.ArgumentName.StartsWith(arg.argument));

            if (argument != null)
            {
                IProperty property = argument as IProperty;

                // Found the argument.
                if (argument.ArgumentName.StartsWith(arg.argument))
                {
                    // Check if the argument has a parameter or not
                    // If the length of the argument is the same as the name, 
                    // there isn't any argument value here, might be on nextArg if allowed
                    if (arg.argument.Length == argument.ArgumentName.Length)
                    {
                        if (argument.IsCountable)
                        {
                            int count = Convert.ToInt32(property.GetValue()) + 1;
                            property.SetValue(count);

                            return (success: true, nextArgumentUsed: false);
                        }
                        // If the argument is multiple and the next argument isn't an argument
                        // Then the next argument is a value for this argument
                        else if (argument.IsMultiple && !nextArgIsArgument)
                        {
                            try
                            {
                                // Call the internal add method to add to the property
                                var convertedType = Convert.ChangeType(nextArg, argument.ArgumentType);
                                property.AddIfMultiple(convertedType);

                                // We need to leave here as let the parent know we have used the next argument as well as this one
                                return (success: true, nextArgumentUsed: true);
                            }
                            catch (Exception)
                            {
                                // TODO: Catch exception and let users know of why failed to cast.
                            }
                        }
                        else if (argument.IsMultiple && nextArgIsArgument)
                        {
                            // TODO: Throw exception, cannot have multiple without an argument parameter
                        }
                        // The next argument isn't an argument and this isn't a flag (not bool) so set this argument to the nextArg
                        else if (argument.ArgumentType != typeof(bool) && !nextArgIsArgument)
                        {
                            try
                            {
                                // Call the internal add method to add to the property
                                var convertedType = Convert.ChangeType(nextArg, argument.ArgumentType);
                                property.SetValue(convertedType);

                                // We need to leave here as let the parent know we have used the next argument as well as this one
                                return (success: true, nextArgumentUsed: true);
                            }
                            catch (Exception)
                            {
                                // TODO: Catch exception and let users know of why failed to cast.
                            }
                        }
                        else if (argument.ArgumentType != typeof(bool) && nextArgIsArgument)
                        {
                            // TODO: Throw exception, cannot have non bool argument without value
                        }
                        // Otherwise after all that it must be a flag so set to true
                        else
                        {
                            property.SetValue(true);
                            return (success: true, nextArgumentUsed: false);
                        }
                    }
                    // If the next character of the argument is a deliminator: 
                    // substring on the delim and set to property of the argument
                    else if (arg.argument[argument.ArgumentName.Length] == ArgumentHelper.WindowsDeliminator || arg.argument[argument.ArgumentName.Length] == ArgumentHelper.Deliminator)
                    {
                        string argumentValue = arg.argument.Substring(argument.ArgumentName.Length);

                        if (argument.IsCountable)
                        {
                            // TODO: Throw exception, cannot have argument on countable
                        }
                        // If it's multiple we need to get the instance of the enumerable, cast the to type
                        // and add to that enumerable
                        else if (argument.IsMultiple)
                        {
                            try
                            {
                                // Call the internal add method to add to the property
                                var convertedType = Convert.ChangeType(argumentValue, argument.ArgumentType);
                                property.AddIfMultiple(convertedType);

                                return (success: true, nextArgumentUsed: false);
                            }
                            catch (Exception)
                            {
                                // TODO: Catch exception and let users know of why failed to cast.
                            }
                        }
                        else
                        {
                            try
                            {
                                var convertedType = Convert.ChangeType(argumentValue, argument.ArgumentType);
                                property.SetValue(convertedType);

                                return (success: true, nextArgumentUsed: false);
                            }
                            catch (Exception)
                            {
                                // TODO: Catch exception and let users know of why failed to cast.
                            }
                        }
                    }
                }
            }
            else
            {
                // TODO: Throw exception, argument name not found
            }

            return (false, false);
        }

        private (bool success, bool nextArgumentUsed) FindFlagAndSetProperty((string prefix, string argument) arg, string nextArg, bool nextArgIsArgument)
        {
            // If the argument is only one long then the only charater must be a flag
            // and the next argument may be the argument value
            if (arg.argument.Length == 1)
            {
                char flag = arg.argument[0];

                // Find the flag and if it exists then set the property accordingly
                IArgument argument = _argumentCatagories
                    .SelectMany(ac => ac.Arguments)
                    .SingleOrDefault(a => a.ArgumentFlag == flag);

                if (argument != null)
                {
                    IProperty property = argument as IProperty;

                    if (argument.IsCountable)
                    {
                        int count = Convert.ToInt32(property.GetValue()) + 1;
                        property.SetValue(count);

                        return (success: true, nextArgumentUsed: false);
                    }
                    // If the argument is multiple and the next argument isn't an argument
                    // Then the next argument is a value for this argument
                    else if (argument.IsMultiple && !nextArgIsArgument)
                    {
                        try
                        {
                            // Call the internal add method to add to the property
                            var convertedType = Convert.ChangeType(nextArg, argument.ArgumentType);
                            property.AddIfMultiple(convertedType);

                            // We need to leave here as let the parent know we have used the next argument as well as this one
                            return (success: true, nextArgumentUsed: true);
                        }
                        catch (Exception)
                        {
                            // TODO: Catch exception and let users know of why failed to cast.
                        }
                    }
                    else if (argument.IsMultiple && nextArgIsArgument)
                    {
                        // TODO: Throw exception, cannot have multiple without an argument parameter
                    }
                    // The next argument isn't an argument and this isn't a flag (not bool) so set this argument to the nextArg
                    else if (argument.ArgumentType != typeof(bool) && !nextArgIsArgument)
                    {
                        try
                        {
                            // Call the internal add method to add to the property
                            var convertedType = Convert.ChangeType(nextArg, argument.ArgumentType);
                            property.SetValue(convertedType);

                            // We need to leave here as let the parent know we have used the next argument as well as this one
                            return (success: true, nextArgumentUsed: true);
                        }
                        catch (Exception)
                        {
                            // TODO: Catch exception and let users know of why failed to cast.
                        }
                    }
                    else if (argument.ArgumentType != typeof(bool) && nextArgIsArgument)
                    {
                        // TODO: Throw exception, cannot have non bool argument without value
                    }
                    // Otherwise after all that it must be a flag so set to true
                    else
                    {
                        property.SetValue(true);
                        return (success: true, nextArgumentUsed: false);
                    }
                }
                else
                {
                    // TODO: Throw exception, flag not found
                }
            }
            // If the argument is longer than one, the argument could be a collection of flags
            // or a flag with an argument to it (only support -c:value or -abc not -abc:value (yet))
            else if (arg.argument.Length > 1)
            {
                // Check if the argument is just a collection of flags (can't have argument value without delim)
                // Can't work on windows prefix system (cannot tell between collection of flags and name)
                if (arg.prefix != ArgumentHelper.WindowsArgumentPrefix && Regex.IsMatch(arg.argument, ArgumentHelper.FlagMatchPattern))
                {
                    foreach (char flag in arg.argument)
                    {
                        // Find the flag and if it exists then set the property accordingly
                        IArgument argument = _argumentCatagories
                            .SelectMany(ac => ac.Arguments)
                            .SingleOrDefault(a => a.ArgumentFlag == flag);

                        if (argument != null)
                        {
                            IProperty property = argument as IProperty;

                            if (argument.IsCountable)
                            {
                                int count = Convert.ToInt32(property.GetValue()) + 1;
                                property.SetValue(count);

                                return (success: true, nextArgumentUsed: false);
                            }
                            else if (argument.IsMultiple)
                            {
                                // TODO: Throw exception, this is a flag collection and cannot support multiple yet
                            }
                            // It's a flag so set to true
                            else
                            {
                                property.SetValue(true);

                                return (success: true, nextArgumentUsed: false);
                            }
                        }
                        else
                        {
                            // TODO: Throw exception, flag not found
                        }
                    }
                }
                else if (arg.argument[1] == ArgumentHelper.WindowsDeliminator || arg.argument[1] == ArgumentHelper.Deliminator)
                {
                    char flag = arg.argument[0];

                    // Find the flag and if it exists then set the property accordingly
                    IArgument argument = _argumentCatagories
                        .SelectMany(ac => ac.Arguments)
                        .SingleOrDefault(a => a.ArgumentFlag == flag);

                    if (argument != null)
                    {
                        IProperty property = argument as IProperty;
                        string argumentValue = arg.argument.Substring(2);

                        if (argument.IsCountable)
                        {
                            // TODO: Throw exception, cannot have argument value on countable
                        }
                        // If it's multiple we need to get the instance of the enumerable, cast the to type
                        // and add to that enumerable
                        else if (argument.IsMultiple)
                        {
                            try
                            {
                                // Call the internal add method to add to the property
                                var convertedType = Convert.ChangeType(argumentValue, argument.ArgumentType);
                                property.AddIfMultiple(convertedType);

                                return (success: true, nextArgumentUsed: false);
                            }
                            catch (Exception)
                            {
                                // TODO: Catch exception and let users know of why failed to cast.
                            }
                        }
                        // Normal argument with argument value
                        else
                        {
                            try
                            {
                                var convertedType = Convert.ChangeType(argumentValue, argument.ArgumentType);
                                property.SetValue(convertedType);

                                return (success: true, nextArgumentUsed: false);
                            }
                            catch (Exception)
                            {
                                // TODO: Catch exception and let users know of why failed to cast.
                            }
                        }
                    }
                    else
                    {
                        // TODO: Throw exception, flag not found
                    }
                }
            }
            else
            {
                // TODO: Throw exception, cannot have empty flag prefix with no argument
            }

            return (false, false);
        }

        #region Static Stuff

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

        #endregion
    }
}
