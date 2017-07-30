using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace argparse
{
    public class Parameter<TArgumentOptions, TArgument> : IParameter<TArgumentOptions, TArgument>
    {
        private IParameterCatagory<TArgumentOptions> _currentCatagory;
        private ICreateParameterCatagory _catagoryCreator;

        public string ParameterName { get; private set; }

        public uint ParameterPosition { get; private set; }

        public bool IsRequired { get; private set; }

        public bool IsMultiple { get; private set; }

        public string ParamterHelp { get; private set; }

        public PropertyInfo Property { get; }

        public Parameter(ICreateParameterCatagory catagoryCreator, IParameterCatagory<TArgumentOptions> currentCatagory, PropertyInfo property)
        {
            if (!ArgumentParser.SupportedTypes.Contains(typeof(TArgument)))
            {
                throw new ArgumentException(
                    $"{typeof(TArgument).Name} is not supported as a parameter type for {property.Name}. Please use only one of the supported types as found in {nameof(ArgumentParser.SupportedTypes)}",
                    nameof(TArgument));
            }

            _catagoryCreator = catagoryCreator;
            _currentCatagory = currentCatagory;
            Property = property;

            Name(property.Name);
        }

        public IParameter<TArgumentOptions, TArgument> Help(string help)
        {
            ParamterHelp = help;

            return this;
        }

        public IParameter<TArgumentOptions, TArgument> Name(string name)
        {
            ParameterName = name;

            return this;
        }

        public IParameter<TArgumentOptions, TArgument> Position(uint position)
        {
            ParameterPosition = position;

            return this;
        }

        public IParameter<TArgumentOptions, TArgument> Required()
        {
            IsRequired = true;

            return this;
        }

        public IParameter<TArgumentOptions, TArgument1> WithParameter<TArgument1>(Expression<Func<TArgumentOptions, TArgument1>> argument)
        {
            return _currentCatagory.WithParameter(argument);
        }

        public IParameterCatagory<TOptions> CreateParameterCatagory<TOptions>()
            where TOptions : class, new()
        {
            return _catagoryCreator.CreateParameterCatagory<TOptions>();
        }

        public TOptions GetParameterCatagory<TOptions>()
            where TOptions : class, new()
        {
            return _catagoryCreator.GetParameterCatagory<TOptions>();
        }
    }
}
