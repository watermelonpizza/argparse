using System;
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
            return (_argumentCatagories.SingleOrDefault(ac => ac is ArgumentCatagory<TOptions>) as ArgumentCatagory<TOptions>)
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


            }
            
        }

        internal void CleanParse()
        {

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
