using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace argparse
{
    internal class MultiArgument<TOptions, TArgument> : Argument<TOptions, TArgument>, IMultiProperty
    {
        public MultiArgument(ICreateArgumentCatagory catagoryCreator, IArgumentCatagory<TOptions> currentCatagory, PropertyInfo property)
            : base(catagoryCreator, currentCatagory, property)
        {
            if (typeof(TArgument) == typeof(bool))
            {
                throw new ArgumentException(
                    $"{typeof(TArgument).Name} is not supported as an argument type for '{property.Name}' on catagory '{typeof(TOptions).Name}'. Please use int type with {nameof(Countable)} function instead",
                    nameof(TArgument));
            }

            if (IsFlags)
            {
                throw new ArgumentException(
                    $"Flags enum type is not supported as a multi-argument type for '{property.Name}' on catagory '{typeof(TOptions).Name}'. Either use non flag enum or use a normal Argument",
                    nameof(TArgument));
            }

            IsMultiple = true;
        }

        public void AddValue(object obj)
        {
            if (obj?.GetType() != typeof(TArgument)) { } // TODO: Throw exception if different types

            if (IsMultiple)
            {
                try
                {
                    ICatagoryInstance instance = _currentCatagory as ICatagoryInstance;

                    // If the property is enumerable and if it's not null cast to a list, add the new value and set it back
                    if (Property.GetValue(instance.CatagoryInstance) != null)
                    {
                        if (Property.GetValue(instance.CatagoryInstance) is IEnumerable<TArgument> propValue)
                        {
                            IList<TArgument> propListValue = propValue.ToList();
                            propListValue.Add((TArgument)obj);
                            Property.SetValue(instance.CatagoryInstance, propListValue);

                            ValueSet = true;
                        }
                        else
                        {
                            // TODO: Can't be not IEnumerable<TArgument> (can't get here?)
                        }
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

                        ValueSet = true;
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
        
        public override IArgument<TOptions, TArgument> Countable()
        {
            throw new Exception($"This argument cannot be set to countable as it is a multi-argument.");
        }

        public override IArgument<TOptions, TArgument> DefaultValue(TArgument value)
        {
            throw new ArgumentException($"Default value cannot be set for multi-arguments on property '{Property.Name}', catagory '{typeof(TOptions).Name}'.", nameof(value));
        }
    }
}
