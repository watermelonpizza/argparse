using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
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

                    // If the property is an ImmutableArray
                    if (Property.GetValue(instance.CatagoryInstance) is ImmutableArray<TArgument> propValue)
                    {
                        // If it's not default, add the new value and set it back
                        if (propValue.IsDefault)
                        {
                            Property.SetValue(
                                instance.CatagoryInstance,
                                ImmutableArray.Create((TArgument)obj));

                            ValueSet = true;

                        }
                        // Otherwise create a new list and set the property
                        else
                        {
                            ImmutableArray<TArgument> newPropertyArray = propValue.Add((TArgument)obj);
                            Property.SetValue(instance.CatagoryInstance, newPropertyArray);

                            ValueSet = true;
                        }
                    }
                    else
                    {
                        // TODO: Can't be not ImmutableArray<TArgument> (can't get here?)
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                throw new Exception($"Tried to add a value to the MultiArgument '{ArgumentName}' but the IsMultiple flag wasn't set.");
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
