using argparse.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace argparse
{
    public class ArgumentParser : IArgumentParser
    {
        public string[] Passthrough { get; private set; }

        public bool HelpCalled { get; private set; }

        internal ArgumentParserOptions Options { get; }

        internal ConsoleHelper ConsoleHelper { get; }

        private List<IArgumentCatagory> _argumentCatagories = new List<IArgumentCatagory>();
        private List<IParameterCatagory> _parameterCatagories = new List<IParameterCatagory>();
        private List<ICommandCatagory> _commandCatagories = new List<ICommandCatagory>();

        private ArgumentParser _parentArgumentParser;
        private ICommand _commandScope;

        protected ArgumentParser(ArgumentParserOptions argumentParserOptions, ConsoleHelper consoleHelper)
        {
            if (argumentParserOptions.ApplicationName == null)
                throw new Exception("Application name cannot be null");

            Options = argumentParserOptions;
            ConsoleHelper = consoleHelper;
        }

        protected ArgumentParser(ArgumentParser parentArgumentParser, ICommand commandScope)
            : this(parentArgumentParser.Options, parentArgumentParser.ConsoleHelper)
        {
            _parentArgumentParser = parentArgumentParser;
            _commandScope = commandScope;
        }

        public static ArgumentParser Create(string applicationName)
        {
            ArgumentParserOptions options = new ArgumentParserOptions
            {
                ApplicationName = applicationName
            };

            return new ArgumentParser(options, new ConsoleHelper(options));
        }

        public static ArgumentParser Create(ArgumentParserOptions argumentParserOptions)
        {
            return new ArgumentParser(argumentParserOptions, new ConsoleHelper(argumentParserOptions));
        }

        public static ArgumentParser Create(Action<ArgumentParserOptions> argumentParserOptions)
        {
            ArgumentParserOptions options = new ArgumentParserOptions();
            argumentParserOptions(options);

            return new ArgumentParser(options, new ConsoleHelper(options));
        }

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
                = new ParameterCatagory<TOptions>(this, typeof(TOptions).Name, (uint)(_parameterCatagories.Count + 1) * 1000);

            _parameterCatagories.Add(parameterCatagory);
            return parameterCatagory;
        }

        public TOptions GetParameterCatagory<TOptions>()
            where TOptions : class, new()
        {
            return (TOptions)
                (_parameterCatagories.SingleOrDefault(pac => pac is ParameterCatagory<TOptions>) as ParameterCatagory<TOptions>)
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

        /// <summary>
        /// Parse the arguments and return true if successfull
        /// </summary>
        /// <param name="args">The args to be parsed, usually from the <c>static void Main(string[] args)</c> entry point function</param>
        /// <returns><c>true</c> if no errors were encountered and all required arguments were filled, <c>false</c> if any problems were encountered</returns>
        public bool Parse(params string[] args)
        {
            if ((args == null || !args.Any()) && Options.WriteBasicHelpOnEmptyArguments)
            {
                HelpCalled = true;
                WriteHelp(HelpDisplayMode.Compact, ArgumentHelper.FlagPrefix, ArgumentHelper.NamePrefix);
                return true;
            }

            // Keep a record of the last parameter used so far
            // so we can use it to determine the next parameter to use
            IParameter lastParameterUsed = null;

            string flagPrefixUsed = null;
            string namePrefixUsed = null;

            // First check for static help argument of -h, --help, /help etc.
            // And ensure anything before help argumnt is not a command.
            // If it is call help on the command.
            string helpArg;
            if ((helpArg = args.SingleOrDefault(arg => ArgumentHelper.HelpArguments.Contains(arg.ToLowerInvariant()))) != null)
            {
                HelpCalled = true;

                // Get the index of the help call to ensure that there isn't
                // a command call before it
                int index = args.ToList().IndexOf(helpArg);
                IEnumerable<string> preArgs = args.Take(index);
                string nextArg = index + 1 < args.Length ? args[index + 1] : null;

                ICommand command =
                    _commandCatagories
                        .SelectMany(cc => cc.Commands)
                        .SingleOrDefault(c => preArgs.Contains(c.CommandName.ToLowerInvariant()));

                if (command != null)
                {
                    IProperty property = command as IProperty;

                    // Now set the sub argument parser to parse the rest of the arguments
                    // Or if it is a bool type then set it to true and we are done
                    if (typeof(IArgumentParser).GetTypeInfo().IsAssignableFrom(property.Property.PropertyType.GetTypeInfo()))
                    {
                        ArgumentParser commandsArgumentParser = property.GetValue() as ArgumentParser;

                        return commandsArgumentParser.Parse(helpArg);
                    }
                    else if (property.Property.PropertyType == typeof(bool))
                    {
                        // TODO: Print help for just command if it is bool
                    }
                    else
                    {
                        throw new Exception($"Command '{command.CommandName}' has property type '{command.Property.Name}' which shouldn't be possible. Something wasn't set up correctly.");
                    }
                }
                else
                {
                    var split = ArgumentHelper.StripArgument(helpArg);


                    if (nextArg == "full")
                        WriteHelp(HelpDisplayMode.Full, flagPrefixUsed, namePrefixUsed);
                    else
                        WriteHelp(HelpDisplayMode.Expanded, flagPrefixUsed, namePrefixUsed);
                }

                return true;
            }

            bool success = true;

            // TODO: Add positions to all argument errors i.e. Argument was expecting value but none found at position 'x'
            // if possible add indicatior? e.g.
            // args: --my-arg value -s --something
            // err!: Argument was expecting value but none found at position 3 'g value -s<--[ERROR]'

            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                string nextArg = i + 1 < args.Length ? args[i + 1] : null;
                string[] nextAllArgs = args.Skip(i + 1).ToArray();

                var strippedArgument = ArgumentHelper.StripArgument(arg);

                // Set the prefix that was used by the user to tailor the help messages
                // to something more friendly that they will be used to
                if (flagPrefixUsed == null || namePrefixUsed == null)
                {
                    (flagPrefixUsed, namePrefixUsed) = GetPrefixUsed(strippedArgument.prefix);
                    if (strippedArgument.prefix == ArgumentHelper.FlagPrefix || strippedArgument.prefix == ArgumentHelper.NamePrefix)
                    {
                        flagPrefixUsed = ArgumentHelper.FlagPrefix;
                        namePrefixUsed = ArgumentHelper.NamePrefix;
                    }
                    else
                    {
                        flagPrefixUsed = ArgumentHelper.WindowsArgumentPrefix;
                        namePrefixUsed = ArgumentHelper.WindowsArgumentPrefix;
                    }
                }

                // If it is an argument, get the argument if exists
                // Attempt to convert argument to target type and set value
                // or count/multiple if the case may be
                if (ArgumentHelper.IsArgument(arg))
                {
                    if (strippedArgument.prefix == ArgumentHelper.WindowsArgumentPrefix)
                    {
                        var findNameResult = FindNameAndSetProperty(strippedArgument, nextArg);

                        if (findNameResult.success)
                        {
                            if (findNameResult.nextArgumentUsed)
                                i++;
                        }
                        else
                        {
                            var findFlagResult = FindFlagAndSetProperty(strippedArgument, nextArg);

                            if (findFlagResult.success && findFlagResult.nextArgumentUsed)
                            {
                                i++;
                            }
                            else if (!findFlagResult.success)
                            {
                                if (Options.StrictParsing)
                                {
                                    ConsoleHelper.WriteError($"no such option: {arg}");
                                    success = false;
                                }
                                else
                                {
                                    ConsoleHelper.WriteWarning($"no such option: '{arg}', ignoring");
                                }
                            }
                        }
                    }
                    // Passthrough argument has been defined. Anything after this isn't for us.
                    // Set the rest of the arguments to the gloabal Passthrough variable and return.
                    // The user can check the Passthrough property for the command (if we are in one)
                    // or something needs external arguments
                    else if (strippedArgument.prefix == ArgumentHelper.NamePrefix && string.IsNullOrEmpty(strippedArgument.argument))
                    {
                        Passthrough = nextAllArgs;
                        return success;
                    }
                    else if (strippedArgument.prefix == ArgumentHelper.NamePrefix)
                    {
                        var findNameResult = FindNameAndSetProperty(strippedArgument, nextArg);

                        if (findNameResult.success && findNameResult.nextArgumentUsed)
                        {
                            i++;
                        }
                        else if (!findNameResult.success)
                        {
                            if (Options.StrictParsing)
                            {
                                ConsoleHelper.WriteError($"no such option: {arg}");
                                success = false;
                            }
                            else
                            {
                                ConsoleHelper.WriteWarning($"no such option: '{arg}', ignoring");
                            }
                        }
                    }
                    else if (strippedArgument.prefix == ArgumentHelper.FlagPrefix)
                    {
                        var findFlagResult = FindFlagAndSetProperty(strippedArgument, nextArg);

                        if (findFlagResult.success && findFlagResult.nextArgumentUsed)
                        {
                            i++;
                        }
                        else if (!findFlagResult.success)
                        {
                            if (Options.StrictParsing)
                            {
                                ConsoleHelper.WriteError($"no such option: {arg}");
                                success = false;
                            }
                            else
                            {
                                ConsoleHelper.WriteWarning($"no such option: '{arg}', ignoring");
                            }
                        }
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
                        if (property.Property.PropertyType == typeof(ICommandArgumentParser))
                        {
                            CommandArgumentParser commandsArgumentParser = property.GetValue() as CommandArgumentParser;
                            commandsArgumentParser.Selected = true;

                            return success && commandsArgumentParser.Parse(nextAllArgs);
                        }
                        else if (property.Property.PropertyType == typeof(bool))
                        {
                            property.SetValue(true);

                            if (nextAllArgs.Any())
                            {
                                ConsoleHelper.WriteWarning($"arguments '{string.Join(" ", nextAllArgs)}' will be ignored because command '{arg}' does not accept arguments or parameters");

                                return false;
                            }

                            return true;
                        }
                        else
                        {
                            throw new Exception($"Command '{command.CommandName}' has property type '{command.Property.Name}' which shouldn't be possible. Something wasn't set up correctly.");
                        }
                    }
                    // Must be a parameter
                    else
                    {
                        var parameters = _parameterCatagories.SelectMany(pc => pc.Parameters);

                        // If there are any parameters, look for the position we are at
                        if (parameters.Any())
                        {
                            // Get the lowest positioned parameter based on the last parameter used up
                            IParameter parameter = lastParameterUsed ?? parameters.Min();

                            if (lastParameterUsed == null)
                            {
                                parameter = parameters.Min();
                            }
                            else
                            {
                                if (!parameter.IsMultiple)
                                {
                                    parameter = parameters.Where(p => p.Position > parameter.Position).Min();
                                }
                            }

                            // We found it so set that parameter
                            if (parameter != null)
                            {
                                lastParameterUsed = parameter;

                                IProperty property = parameter as IProperty;

                                if (parameter.IsMultiple)
                                {
                                    try
                                    {
                                        // Call the internal add method to add to the property
                                        var convertedType = ConversionHelper.Convert(strippedArgument.argument, parameter.ParameterType);
                                        ((IMultiProperty)property).AddValue(convertedType);
                                    }
                                    catch (Exception)
                                    {
                                        ConsoleHelper.WriteError($"argument '{arg}' could not be added to parameter '{parameter.ParameterName.ToUpperInvariant()}' as it is expecting a '{parameter.ParameterType.Name}' type");
                                        success = false;
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        var convertedType = ConversionHelper.Convert(strippedArgument.argument, parameter.ParameterType);
                                        property.SetValue(convertedType);
                                    }
                                    catch (Exception)
                                    {
                                        ConsoleHelper.WriteError($"argument '{arg}' could not be assigned to parameter '{parameter.ParameterName.ToUpperInvariant()}' as it is expecting a '{parameter.ParameterType.Name}' type");
                                        success = false;
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
                                        var convertedType = ConversionHelper.Convert(strippedArgument.argument, parameter.ParameterType);
                                        ((IMultiProperty)property).AddValue(convertedType);
                                    }
                                    catch (Exception)
                                    {
                                        ConsoleHelper.WriteError($"argument '{arg}' could not be added to parameter '{parameter.ParameterName.ToUpperInvariant()}' as it is expecting a '{parameter.ParameterType.Name}' type");
                                        success = false;
                                    }
                                }
                                else
                                {
                                    if (Options.StrictParsing)
                                    {

                                        ConsoleHelper.WriteError($"parameter '{arg}' does not fit any known parameters at this position");
                                        success = false;
                                    }
                                    else
                                    {
                                        ConsoleHelper.WriteWarning($"parameter '{arg}' was ignored as it does not fit any known parameters at this position");
                                    }
                                }
                            }
                        }
                    }
                }
            }

            flagPrefixUsed = flagPrefixUsed ?? ArgumentHelper.FlagPrefix;
            namePrefixUsed = namePrefixUsed ?? ArgumentHelper.NamePrefix;

            if (!HelpCalled)
            {
                // Ensure that all required arguments/parameters are set
                foreach (var argument in
                    _argumentCatagories
                        .SelectMany(ac => ac.Arguments)
                        .Where(a => a.IsRequired)
                        .OfType<IProperty>()
                        .Where(p => !p.ValueSet)
                        .OfType<IArgument>())
                {
                    ConsoleHelper.WriteError($"argument '{namePrefixUsed}{argument.ArgumentName}' is required");
                    success = false;
                }

                foreach (var parameter in
                    _parameterCatagories
                        .SelectMany(pc => pc.Parameters)
                        .Where(p => p.IsRequired)
                        .OfType<IProperty>()
                        .Where(p => !p.ValueSet)
                        .OfType<IParameter>())
                {
                    ConsoleHelper.WriteError($"parameter '{parameter.ParameterName}' is required");
                    success = false;
                }

                // Set all the arguments that have default values which haven't been set yet
                foreach (var argumentProperty in
                    _argumentCatagories
                    .SelectMany(ac => ac.Arguments)
                    .Where(a => a.ArgumentDefaultSet)
                    .OfType<IProperty>()
                    .Where(p => !p.ValueSet))
                {
                    IArgument argument = argumentProperty as IArgument;

                    if (!argumentProperty.ValueSet)
                        argumentProperty.SetValue(argument.ArgumentDefaultValue);
                }

                if (success && (args == null || !args.Any()))
                {
                    WriteHelp(HelpDisplayMode.Compact, ArgumentHelper.FlagPrefix, ArgumentHelper.NamePrefix);
                }
                else
                {
                    ConsoleHelper.WriteLine();
                    WriteHelp(HelpDisplayMode.JustUsage, ArgumentHelper.FlagPrefix, ArgumentHelper.NamePrefix);
                }
            }

            return success;
        }

        internal void WriteHelp(HelpDisplayMode displayMode, string flagPrefixUsed, string namePrefixUsed)
        {
            StringBuilder sb = new StringBuilder();

            // Write out the preable (copyright or such details)
            if (!string.IsNullOrWhiteSpace(Options.Preamble))
            {
                sb.Append(Options.Preamble);
                sb.AppendLine();
                sb.AppendLine();
            }

            // Write out the usage of the application
            sb.Append($"Usage: {Options.ApplicationName}");

            // If the main argument parser has arguements ensure to include the options part before the command
            if (_parentArgumentParser != null &&
                _parentArgumentParser._argumentCatagories.Any())
                sb.Append(" [OPTIONS]");

            // or in the case of this being within a command, write out the usage for the command
            if (_commandScope != null)
                sb.Append($" {_commandScope.CommandName}");

            if (_argumentCatagories.Any())
                sb.Append(" [OPTIONS]");

            // Loop over all the parameters and print them out in order
            foreach (var parameter in _parameterCatagories.SelectMany(pc => pc.Parameters).OrderBy(p => p.Position))
            {
                sb.Append(" ");

                if (parameter.IsMultiple && parameter.IsRequired)
                {
                    sb.Append($"{parameter.ParameterName.ToUpperInvariant()} [{parameter.ParameterName.ToUpperInvariant()}...]");
                }
                else if (parameter.IsMultiple)
                {
                    sb.Append($"[{parameter.ParameterName.ToUpperInvariant()}...]");
                }
                else if (parameter.IsRequired)
                {
                    sb.Append(parameter.ParameterName.ToUpperInvariant());
                }
            }

            if (_commandCatagories.Any())
                sb.Append(" COMMAND");

            sb.AppendLine();

            if (displayMode != HelpDisplayMode.JustUsage)
            {
                // If we are within a command print out its description
                if (!string.IsNullOrWhiteSpace(_commandScope?.CommandSummary))
                {
                    sb.AppendLine(_commandScope.CommandSummary);
                }
                // Otherwise print out the applications description
                else if (!string.IsNullOrWhiteSpace(Options.ApplicationDescription))
                {
                    sb.AppendLine(Options.ApplicationDescription);
                }

                // If we are in compact mode (i.e. no argument called, just the application was called without any args)
                if (displayMode == HelpDisplayMode.Compact)
                {
                    sb.AppendLine();

                    sb.AppendLine($"Run '{Options.ApplicationName} --help' or '{Options.ApplicationName} --help full' to see a list of all options and more information.");
                }
                // Otherwise prepair to print off all the help documentation
                else
                {
                    // Next few lines determine what the longest possible length for all
                    // of the commands/argument/parameters to align the help correctly
                    // e.g.
                    // Options:
                    //   --arg1             |
                    //   --arg10            |
                    //                      |
                    // Parameters:           |
                    //   somelongparameter  |<- Help starts on that column 2 off the longest.
                    //

                    int argumentStartLength = _argumentCatagories.Count == 1 ? 2 : 4;
                    int commandStartLength = _commandCatagories.Count == 1 ? 2 : 4;
                    int parameterStartLength = _parameterCatagories.Count == 1 ? 2 : 4;

                    int helpIndentLength =
                        _argumentCatagories
                            .SelectMany(ac => ac.Arguments)
                            .MaxOrDefault(a => GetArgumentLength(argumentStartLength, a, displayMode == HelpDisplayMode.Full));

                    helpIndentLength =
                        Math.Max(
                            helpIndentLength,
                            _commandCatagories
                                .SelectMany(cc => cc.Commands)
                                .MaxOrDefault(c => GetCommandLength(commandStartLength, c)));

                    helpIndentLength =
                        Math.Max(
                            helpIndentLength,
                            _parameterCatagories
                                .SelectMany(pc => pc.Parameters)
                                .MaxOrDefault(p => GetParamterLength(parameterStartLength, p)));

                    helpIndentLength += 2;

                    // For when the help wraps over multiple lines, prepend this blank string
                    string helpPreSpacing = string.Join("", Enumerable.Repeat(" ", helpIndentLength));

                    // However the available width will bottom out at 20 charaters
                    // If the console is too small, it will just text wrap. Can't do anything about it.
                    int availableWidth = Math.Max(20, Console.WindowWidth - helpIndentLength);

                    // If there any any arguments loop over them all and print out the doco.
                    if (_argumentCatagories.Any())
                    {
                        string argumentPreSpacing = string.Join("", Enumerable.Repeat(" ", argumentStartLength));
                        string enumPreSpacing = string.Join("", Enumerable.Repeat(" ", argumentStartLength + 3));

                        sb.AppendLine();
                        sb.AppendLine("Options:");

                        foreach (var argCatagory in _argumentCatagories)
                        {
                            if (_argumentCatagories.Count > 1)
                            {
                                if (argCatagory != _argumentCatagories.First())
                                {
                                    sb.AppendLine();
                                }

                                sb.AppendLine($"  {argCatagory.CatagoryName}:");
                            }

                            foreach (var argument in argCatagory.Arguments)
                            {
                                string argumentHelp = "";

                                if (argument.ArgumentFlag != ArgumentHelper.NoFlag)
                                {
                                    argumentHelp += $"{flagPrefixUsed}{argument.ArgumentFlag}, ";
                                }

                                argumentHelp += $"{namePrefixUsed}{argument.ArgumentName}";

                                sb.Append((argumentPreSpacing + argumentHelp).PadRight(helpIndentLength));

                                string helpText = argument.ArgumentHelp;

                                if (argument.IsEnum && displayMode != HelpDisplayMode.Full)
                                {
                                    if (helpText.Length != 0)
                                        helpText += " ";

                                    helpText += $"(values {string.Join(", ", Enum.GetNames(argument.ArgumentType)).ToLowerInvariant()})";
                                }

                                if (argument.ArgumentDefaultSet)
                                {
                                    if (helpText.Length != 0)
                                        helpText += " ";

                                    helpText += $"(default {argument.ArgumentDefaultValue.ToString().ToLowerInvariant()})";
                                }

                                if (helpText.Length > availableWidth)
                                {
                                    IEnumerable<string> split = SplitToLines(helpText, availableWidth);
                                    foreach (var line in split)
                                    {
                                        if (line == split.First())
                                            sb.Append(line);
                                        else
                                            sb.Append(helpPreSpacing + line);

                                        if ((helpPreSpacing + line).Length != Console.WindowWidth)
                                            sb.AppendLine();
                                    }
                                }
                                else
                                {
                                    sb.AppendLine(helpText);
                                }

                                // If the display is in full mode write out the enum values as a list
                                // with their help text if they have any
                                if (argument.IsEnum && displayMode == HelpDisplayMode.Full)
                                {
                                    // Has to filter out value__ because GetRuntimeFields() also includes the internal
                                    // enum value field. Better methods don't exist at netstandard1.3
                                    foreach (var enumValue in argument.ArgumentType.GetRuntimeFields().Where(f => f.Name != "value__"))
                                    {
                                        sb.Append((enumPreSpacing + enumValue.Name.ToLowerInvariant()).PadRight(helpIndentLength));

                                        var helpAttribute = enumValue.GetCustomAttribute<HelpAttribute>();

                                        if (helpAttribute != null)
                                        {
                                            if (helpAttribute.Help.Length > availableWidth)
                                            {
                                                IEnumerable<string> split = SplitToLines(helpAttribute.Help, availableWidth);
                                                foreach (var line in split)
                                                {
                                                    if (line == split.First())
                                                        sb.Append(line);
                                                    else
                                                        sb.Append(helpPreSpacing + line);

                                                    if ((helpPreSpacing + line).Length != Console.WindowWidth)
                                                        sb.AppendLine();
                                                }
                                            }
                                            else
                                            {
                                                sb.AppendLine(helpAttribute.Help);
                                            }
                                        }
                                        else
                                        {
                                            sb.AppendLine();
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // If there are any commands, loop over em' and print like above
                    if (_commandCatagories.Any())
                    {
                        string commandPreSpacing = string.Join("", Enumerable.Repeat(" ", commandStartLength));

                        sb.AppendLine();
                        sb.AppendLine("Commands:");

                        foreach (var commandCatagory in _commandCatagories)
                        {
                            if (_commandCatagories.Count > 1)
                            {
                                if (commandCatagory != _commandCatagories.First())
                                {
                                    sb.AppendLine();
                                }

                                sb.AppendLine($"  {commandCatagory.CatagoryName}:");
                            }

                            foreach (var command in commandCatagory.Commands)
                            {
                                string commandHelp = "";

                                commandHelp += $"{command.CommandName}";

                                sb.Append((commandPreSpacing + commandHelp).PadRight(helpIndentLength));

                                string helpText = command.CommandHelp;

                                if (helpText.Length > availableWidth)
                                {
                                    IEnumerable<string> split = SplitToLines(helpText, availableWidth);
                                    foreach (var line in split)
                                    {
                                        if (line == split.First())
                                            sb.Append(line);
                                        else
                                            sb.Append(helpPreSpacing + line);

                                        if ((helpPreSpacing + line).Length != Console.WindowWidth)
                                            sb.AppendLine();
                                    }
                                }
                                else
                                {
                                    sb.AppendLine(helpText);
                                }
                            }
                        }
                    }

                    // Finally any paramters will be printed if there are any
                    if (_parameterCatagories.Any())
                    {
                        string parameterPreSpacing = string.Join("", Enumerable.Repeat(" ", parameterStartLength));

                        sb.AppendLine();
                        sb.AppendLine("Paramters:");

                        foreach (var parameterCatagory in _parameterCatagories)
                        {
                            if (_parameterCatagories.Count > 1)
                            {
                                if (parameterCatagory != _parameterCatagories.First())
                                {
                                    sb.AppendLine();
                                }

                                sb.AppendLine($"  {parameterCatagory.CatagoryName}:");
                            }

                            foreach (var parameter in parameterCatagory.Parameters)
                            {
                                string parameterHelp = "";

                                parameterHelp += $"{parameter.ParameterName}";

                                sb.Append((parameterPreSpacing + parameterHelp).PadRight(helpIndentLength));

                                string helpText = parameter.ParameterHelp;

                                if (helpText.Length > availableWidth)
                                {
                                    IEnumerable<string> split = SplitToLines(helpText, availableWidth);
                                    foreach (var line in split)
                                    {
                                        if (line == split.First())
                                            sb.Append(line);
                                        else
                                            sb.Append(helpPreSpacing + line);

                                        if ((helpPreSpacing + line).Length != Console.WindowWidth)
                                            sb.AppendLine();
                                    }
                                }
                                else
                                {
                                    sb.AppendLine(helpText);
                                }
                            }
                        }
                    }
                }
            }

            ConsoleHelper.Write(sb.ToString());
        }

        private (string flagPrefixUsed, string namePrefixUsed) GetPrefixUsed(string prefix)
        {
            string flagPrefixUsed;
            string namePrefixUsed;

            // Set the prefix that was used by the user to tailor the help messages
            // to something more friendly that they will be used to
            if (prefix == ArgumentHelper.FlagPrefix || prefix == ArgumentHelper.NamePrefix)
            {
                flagPrefixUsed = ArgumentHelper.FlagPrefix;
                namePrefixUsed = ArgumentHelper.NamePrefix;
            }
            else
            {
                flagPrefixUsed = ArgumentHelper.WindowsArgumentPrefix;
                namePrefixUsed = ArgumentHelper.WindowsArgumentPrefix;
            }

            return (flagPrefixUsed, namePrefixUsed);
        }

        // https://stackoverflow.com/a/22368809
        private IEnumerable<string> SplitToLines(string stringToSplit, int maximumLineLength)
        {
            var words = stringToSplit.Split(' ').Concat(new[] { "" });

            return words
                .Skip(1)
                .Aggregate(
                    words.Take(1).ToList(),
                    (a, w) =>
                    {
                        var last = a.Last();
                        while (last.Length > maximumLineLength)
                        {
                            a[a.Count() - 1] = last.Substring(0, maximumLineLength);
                            last = last.Substring(maximumLineLength);
                            a.Add(last);
                        }
                        var test = last + " " + w;
                        if (test.Length > maximumLineLength)
                        {
                            a.Add(w);
                        }
                        else
                        {
                            a[a.Count() - 1] = test;
                        }
                        return a;
                    });
        }

        private int GetArgumentLength(int startLength, IArgument argument, bool includeEnum)
        {
            int length = startLength;

            if (argument.ArgumentFlag != ArgumentHelper.NoFlag)
            {
                length += length;
            }

            length += ("--" + argument.ArgumentName).Length;

            if (includeEnum && argument.IsEnum)
            {
                length = Math.Max(length, startLength + 2 + Enum.GetNames(argument.ArgumentType).Max(n => n.Length));
            }

            return length;
        }

        private int GetCommandLength(int startLength, ICommand command)
        {
            int length = startLength;

            return length + command.CommandName.Length;
        }

        private int GetParamterLength(int startLength, IParameter parameter)
        {
            int length = startLength;

            return length + parameter.ParameterName.Length;
        }

        private (bool success, bool nextArgumentUsed) FindNameAndSetProperty((string prefix, string argument) arg, string nextArg)
        {
            // TODO: Parse multi values as comma seperated lists like: --files file1.txt,file2.txt,"file 3.txt"
            // TODO: Parse DateTime values as possible ticks

            IArgument argument =
                _argumentCatagories
                    .SelectMany(ac => ac.Arguments)
                    .SingleOrDefault(a => arg.argument.ToLowerInvariant().StartsWith(a.ArgumentName.ToLowerInvariant()));

            if (argument != null)
            {
                IProperty property = argument as IProperty;

                // If the argument requires a value, i.e. it isn't a flaggable argument and it's multiple or countable
                // then this argument will require a value
                bool argumentRequiresValue = !argument.IsCountable && argument.ArgumentType != typeof(bool);

                // Check if the argument has a parameter or not
                // If the length of the argument is the same as the name,
                // there isn't any argument value here, might be on nextArg if allowed
                if (arg.argument.Length == argument.ArgumentName.Length)
                {
                    if (argument.IsCountable)
                    {
                        int count = Convert.ToInt32(property.GetValue()) + 1;
                        var convertedType = ConversionHelper.Convert(count, argument.ArgumentType);
                        property.SetValue(convertedType);

                        return (success: true, nextArgumentUsed: false);
                    }
                    // If the argument is multiple and the next argument isn't null
                    // Then the next argument is a value for this argument
                    else if (argument.IsMultiple && nextArg != null)
                    {
                        try
                        {
                            // Call the internal add method to add to the property
                            var convertedType = ConversionHelper.Convert(nextArg, argument.ArgumentType);
                            ((IMultiProperty)property).AddValue(convertedType);

                            // We need to leave here as let the parent know we have used the next argument as well as this one
                            return (success: true, nextArgumentUsed: true);
                        }
                        catch (Exception)
                        {
                            ConsoleHelper.WriteError($"Argument value '{nextArg}' could not be added to argument '{argument.ArgumentName.ToLowerInvariant()}' as it is expecting a '{argument.ArgumentType.Name}' type");
                        }
                    }
                    else if (argument.IsMultiple && nextArg == null)
                    {
                        ConsoleHelper.WriteError($"Argument '{argument.ArgumentName.ToLowerInvariant()}' is expecting a value but none was supplied");
                    }
                    // The next argument isn't an argument and this isn't a flag (not bool) so set this argument to the nextArg
                    else if (argumentRequiresValue && nextArg != null)
                    {
                        try
                        {
                            // Call the internal add method to add to the property
                            var convertedType = ConversionHelper.Convert(nextArg, argument.ArgumentType);
                            property.SetValue(convertedType);

                            // We need to leave here as let the parent know we have used the next argument as well as this one
                            return (success: true, nextArgumentUsed: true);
                        }
                        catch (Exception)
                        {
                            ConsoleHelper.WriteError($"Argument value '{nextArg}' could not be assigned to argument '{argument.ArgumentName.ToLowerInvariant()}' as it is expecting a '{argument.ArgumentType.Name}' type");
                        }
                    }
                    else if (argumentRequiresValue && nextArg == null)
                    {
                        ConsoleHelper.WriteError($"Argument '{argument.ArgumentName.ToLowerInvariant()}' is expecting a value but none was supplied");
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
                    string argumentValue = arg.argument.Substring(argument.ArgumentName.Length + 1);

                    if (argument.IsCountable)
                    {
                        ConsoleHelper.WriteError($"Argument '{argument.ArgumentName.ToLowerInvariant()}' isn't expecting a value but '{argumentValue}' was used");
                    }

                    // If it's multiple we need to get the instance of the enumerable, cast the to type
                    // and add to that enumerable
                    else if (argument.IsMultiple)
                    {
                        try
                        {
                            string[] commaSeperatedValues = argumentValue.Split(',');

                            foreach (var value in commaSeperatedValues)
                            {
                                // Call the internal add method to add to the property
                                var convertedType = ConversionHelper.Convert(value, argument.ArgumentType);
                                ((IMultiProperty)property).AddValue(convertedType);
                            }

                            return (success: true, nextArgumentUsed: false);
                        }
                        catch (Exception)
                        {
                            ConsoleHelper.WriteError($"Argument value '{argumentValue}' could not be added to argument '{argument.ArgumentName.ToLowerInvariant()}' as it is expecting a '{argument.ArgumentType.Name}' type");
                        }
                    }
                    else
                    {
                        try
                        {
                            var convertedType = ConversionHelper.Convert(argumentValue, argument.ArgumentType);
                            property.SetValue(convertedType);

                            return (success: true, nextArgumentUsed: false);
                        }
                        catch (Exception)
                        {
                            ConsoleHelper.WriteError($"Argument value '{argumentValue}' could not be assigned to argument '{argument.ArgumentName.ToLowerInvariant()}' as it is expecting a '{argument.ArgumentType.Name}' type");
                        }
                    }
                }
            }

            return (false, false);
        }

        private (bool success, bool nextArgumentUsed) FindFlagAndSetProperty((string prefix, string argument) arg, string nextArg)
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

                    // If the argument requires a value, i.e. it isn't a flaggable argument and it's multiple or countable
                    // then this argument will require a value
                    bool argumentRequiresValue = !argument.IsCountable && argument.ArgumentType != typeof(bool);

                    if (argument.IsCountable)
                    {
                        int count = Convert.ToInt32(property.GetValue()) + 1;
                        property.SetValue(count);

                        return (success: true, nextArgumentUsed: false);
                    }
                    // If the argument is multiple and the next argument isn't an null
                    // Then the next argument is a value for this argument
                    else if (argument.IsMultiple && nextArg != null)
                    {
                        try
                        {
                            // Call the internal add method to add to the property
                            var convertedType = ConversionHelper.Convert(nextArg, argument.ArgumentType);
                            ((IMultiProperty)property).AddValue(convertedType);

                            // We need to leave here as let the parent know we have used the next argument as well as this one
                            return (success: true, nextArgumentUsed: true);
                        }
                        catch (Exception)
                        {
                            ConsoleHelper.WriteError($"Argument value '{nextArg}' could not be added to argument '{argument.ArgumentName.ToLowerInvariant()}' as it is expecting a '{argument.ArgumentType.Name}' type");
                        }
                    }
                    else if (argument.IsMultiple && nextArg == null)
                    {
                        ConsoleHelper.WriteError($"Argument '{argument.ArgumentName.ToLowerInvariant()}' is expecting a value but none was supplied");
                    }
                    // The next argument isn't an argument and this isn't a flag (not bool) so set this argument to the nextArg
                    else if (argumentRequiresValue && nextArg != null)
                    {
                        try
                        {
                            // Call the internal add method to add to the property
                            var convertedType = ConversionHelper.Convert(nextArg, argument.ArgumentType);
                            property.SetValue(convertedType);

                            // We need to leave here as let the parent know we have used the next argument as well as this one
                            return (success: true, nextArgumentUsed: true);
                        }
                        catch (Exception)
                        {
                            // TODO: Catch exception and let users know of why failed to cast.
                        }
                    }
                    else if (argumentRequiresValue && nextArg == null)
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
                if (arg.prefix != ArgumentHelper.WindowsArgumentPrefix && Regex.IsMatch(arg.argument, ArgumentHelper.MultiFlagMatchPattern))
                {
                    bool success = true;

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
                            }
                            else if (argument.IsMultiple)
                            {
                                // TODO: Throw exception, this is a flag collection and cannot support multiple yet
                            }
                            // It's a flag so set to true
                            else if (argument.ArgumentType == typeof(bool))
                            {
                                property.SetValue(true);
                            }
                            else
                            {
                                // TODO: Throw excpetion, cannot use value required argument in flag list
                            }
                        }
                        else
                        {
                            // TODO: Throw exception, flag not found
                        }
                    }

                    return (success: success, nextArgumentUsed: false);
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
                                string[] commaSeperatedValues = argumentValue.Split(',');

                                foreach (var value in commaSeperatedValues)
                                {
                                    // Call the internal add method to add to the property
                                    var convertedType = ConversionHelper.Convert(value, argument.ArgumentType);
                                    ((IMultiProperty)property).AddValue(convertedType);
                                }

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
                                var convertedType = ConversionHelper.Convert(argumentValue, argument.ArgumentType);
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

        /// <summary>
        /// A list of supported types that are allowed for arguments or parameters.
        ///
        /// This includes:
        /// - bool
        /// - byte
        /// - sbyte
        /// - short
        /// - int
        /// - uint
        /// - float
        /// - double
        /// - decimal
        /// - char
        /// - string
        /// - DateTime
        /// - Enum
        /// - ImmutableArray{T} (of the above)
        ///
        /// * Some of the types are not available on all situations,
        /// e.g. you cannot use DateTime on a countable argument.
        /// </summary>
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
            typeof(ImmutableArray<byte>),
            typeof(ImmutableArray<sbyte>),
            typeof(ImmutableArray<short>),
            typeof(ImmutableArray<int>),
            typeof(ImmutableArray<uint>),
            typeof(ImmutableArray<long>),
            typeof(ImmutableArray<ulong>),
            typeof(ImmutableArray<float>),
            typeof(ImmutableArray<double>),
            typeof(ImmutableArray<decimal>),
            typeof(ImmutableArray<char>),
            typeof(ImmutableArray<string>),
            typeof(ImmutableArray<DateTime>),
            typeof(ImmutableArray<Enum>)
        };

        #endregion
    }
}
