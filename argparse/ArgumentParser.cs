using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace argparse
{
    public class ArgumentParser : IArgumentParser
    {
        public static ArgumentParser Default { get; } = new ArgumentParser();

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
            return (TOptions)(_argumentCatagories.SingleOrDefault(ac => ac is ArgumentCatagory<TOptions>) as ArgumentCatagory<TOptions>)
                ?.CatagoryInstance;
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
            return (_paramterCatagories.SingleOrDefault(pac => pac is ParameterCatagory<TOptions>) as ParameterCatagory<TOptions>)
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
            return (_commandCatagories.SingleOrDefault(cc => cc is CommandCatagory<TOptions>) as CommandCatagory<TOptions>)
                ?.CatagoryInstance;
        }

        public void Parse(params string[] args)
        {
            args = ArgumentHelper.StripApplication(args);

            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                string nextArg = i + 1 < args.Length ? args[i + 1] : null;

                ArgumentType type = ArgumentHelper.GetArgumentType(arg, AllArgumentNames(), AllArgumentFlags(), AllCommands());

                var strippedArgument = ArgumentHelper.StripArgument(arg);

                switch (type)
                {
                    case ArgumentType.Flag:
                        foreach (IArgumentCatagory cat in _argumentCatagories)
                        {
                            IArgument flag = cat.Arguments.SingleOrDefault(a => a.ArgumentFlag == strippedArgument.argument[0]);

                            if (flag != null)
                            {
                                if (flag.IsCountable)
                                {
                                    uint count = Convert.ToUInt32(flag.Property.GetValue(cat.CatagoryInstance)) + 1;

                                    flag.Property.SetValue(cat.CatagoryInstance, Convert.ChangeType(count, flag.Property.PropertyType));
                                    break;
                                }
                                else if (flag.IsMultiple)
                                {
                                    // TODO: Throw exception. Need argument.
                                }
                                else
                                {
                                    flag.Property.SetValue(cat.CatagoryInstance, true);
                                    break;
                                }
                            }
                        }
                        
                        break;
                    case ArgumentType.FlagWithArgument:
                        foreach (IArgumentCatagory cat in _argumentCatagories)
                        {
                            IArgument flag = cat.Arguments.SingleOrDefault(a => a.ArgumentFlag == strippedArgument.argument[0]);

                            if (flag != null)
                            {
                                if (flag.IsCountable)
                                {
                                    uint count = Convert.ToUInt32(flag.Property.GetValue(cat.CatagoryInstance)) + 1;

                                    flag.Property.SetValue(cat.CatagoryInstance, Convert.ChangeType(count, flag.Property.PropertyType));
                                    break;
                                }
                                else if (flag.IsMultiple)
                                {
                                    // TODO: Throw exception. Need argument.
                                }
                                else
                                {
                                    flag.Property.SetValue(cat.CatagoryInstance, strippedArgument.argument);
                                    break;
                                }
                            }
                        }

                        break;

                        new List<ArgumentDetails> { new ArgumentDetails(type, flag: strippedArgument.argument[0], argument: strippedArgument.argument.Substring(2)) };
                    case ArgumentType.Flags:
                        List<ArgumentDetails> flags = new List<ArgumentDetails>();
                        foreach (char flag in strippedArgument.argument)
                        {
                            flags.Add(new ArgumentDetails(type, flag: flag));
                        }

                        return flags;
                    case ArgumentType.FlagsWithUnknown:
                        List<ArgumentDetails> flagsWithUnknown = new List<ArgumentDetails>();
                        foreach (char flag in strippedArgument.argument)
                        {
                            flagsWithUnknown.Add(new ArgumentDetails(type, flag: flag));
                        }

                        return flagsWithUnknown;
                    case ArgumentType.Name:
                        return new List<ArgumentDetails> { new ArgumentDetails(type, name: strippedArgument.argument) };
                    case ArgumentType.NameWithArgument:
                        string argName = argNames.Single(name => strippedArgument.argument.StartsWith(name));

                        return new List<ArgumentDetails>
                    {
                        new ArgumentDetails(type, name: strippedArgument.argument.Substring(0, argName.Length), argument: strippedArgument.argument.Substring(argName.Length + 1))
                    };
                    case ArgumentType.Command:
                        return new List<ArgumentDetails> { new ArgumentDetails(type, name: strippedArgument.argument) };
                    case ArgumentType.Passthrough:
                        return new List<ArgumentDetails> { new ArgumentDetails(type, argument: strippedArgument.argument) };
                    case ArgumentType.None:
                    default:
                        return new List<ArgumentDetails>();
                }
            }
            
        }

        IEnumerable<string> AllArgumentNames()
        {
            foreach (IArgumentCatagory cat in _argumentCatagories)
            {
                foreach (IArgument arg in cat.Arguments)
                {
                    yield return arg.ArgumentName;
                }
            }
        }

        IEnumerable<char> AllArgumentFlags()
        {
            foreach (IArgumentCatagory cat in _argumentCatagories)
            {
                foreach (IArgument arg in cat.Arguments)
                {
                    yield return arg.ArgumentFlag;
                }
            }
        }

        IEnumerable<string> AllCommands()
        {
            foreach (ICommandCatagory cat in _commandCatagories)
            {
                foreach (ICommand command in cat.Commands)
                {
                    yield return command.CommandName;
                }
            }
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
