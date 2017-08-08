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

            _catagoryCreator = catagoryCreator;
            _currentCatagory = currentCatagory;
            Property = property;

            Name(property.Name);

            if (typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(typeof(TArgument).GetTypeInfo()))
            {
                ParameterType = typeof(TArgument).GenericTypeArguments[0];
                IsMultiple = true;
            }
            else
            {
                ParameterType = typeof(TArgument);
            }

            Position = position;
        }

        public void AddIfMultiple(object obj)
        {
            if (obj?.GetType() != typeof(TArgument)) { } // TODO: Throw exception if different types

            if (IsMultiple)
            {
                try
                {
                    ICatagoryInstance instance = _currentCatagory as ICatagoryInstance;

                    // If the property is enumerable and it's not null cast to a list, add the new value and set it back
                    if (Property.GetValue(instance.CatagoryInstance) is IEnumerable<TArgument> propValue)
                    {
                        if (propValue != null)
                        {
                            IList<TArgument> propListValue = propValue.ToList();
                            propListValue.Add((TArgument)obj);
                            Property.SetValue(instance.CatagoryInstance, propListValue);
                        }
                        // Otherwise create a new list and set the property
                        else
                        {
                            Property.SetValue(
                                instance.CatagoryInstance,
                                new List<TArgument>
                                {
                                     (TArgument)obj
                                });
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                // TODO: Throw exception why cannot add to non multiple
            }
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