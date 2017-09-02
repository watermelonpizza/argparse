using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace argparse
{
    internal class Parameter<TArgumentOptions, TArgument> : IParameter<TArgumentOptions, TArgument>, IProperty
    {
        private IParameterCatagory<TArgumentOptions> _currentCatagory;
        private ICreateParameterCatagory _catagoryCreator;

        public string ParameterName { get; private set; }

        public Type ParameterType { get; }

        public uint Position { get; }

        public bool IsRequired { get; private set; }

        public bool IsMultiple { get; }

        public string ParamterHelp { get; private set; }

        public PropertyInfo Property { get; }

        public Parameter(ICreateParameterCatagory catagoryCreator, IParameterCatagory<TArgumentOptions> currentCatagory, PropertyInfo property, uint position)
        {
            if (!ArgumentParser.SupportedTypes.Contains(typeof(TArgument)))
            {
                throw new ArgumentException(
                    $"{typeof(TArgument).Name} is not supported as a parameter type for {property.Name}. Please use only one of the supported types as found in {nameof(ArgumentParser.SupportedTypes)}",
                    nameof(TArgument));
            }

            if (property.GetMethod == null || property.SetMethod == null)
            {
                throw new ArgumentException(
                    $"Property '{property.Name}' must have both a get and set accessor on catagory '{typeof(TArgumentOptions).Name}'.",
                    nameof(property));
            }

            _catagoryCreator = catagoryCreator;
            _currentCatagory = currentCatagory;
            Property = property;

            Name(property.Name.ToUpperInvariant());

            ParameterType = typeof(TArgument);

            Position = position;
        }

        public void SetValue(object obj)
        {
            if (obj?.GetType() != typeof(TArgument)) { } // TODO: Throw exception if different types

            ICatagoryInstance instance = _currentCatagory as ICatagoryInstance;
            Property.SetValue(instance.CatagoryInstance, obj);
        }

        public object GetValue()
        {
            ICatagoryInstance instance = _currentCatagory as ICatagoryInstance;
            return Property.GetValue(instance.CatagoryInstance);
        }

        public IParameter<TArgumentOptions, TArgument> Help(string help)
        {
            ParamterHelp = help;

            return this;
        }

        public IParameter<TArgumentOptions, TArgument> Name(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"{Property.Name} names cannot be empty or null.", nameof(name));
            }

            ParameterName = name;

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

        public IParameter<TArgumentOptions, TArgument1> WithMultiParameter<TArgument1>(Expression<Func<TArgumentOptions, IEnumerable<TArgument1>>> argument)
        {
            return _currentCatagory.WithMultiParameter(argument);
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