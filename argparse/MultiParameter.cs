using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace argparse
{
    internal class MultiParameter<TArgumentOptions, TArgument> : Parameter<TArgumentOptions, TArgument>, IMultiProperty
    {
        public MultiParameter(ICreateParameterCatagory catagoryCreator, IParameterCatagory<TArgumentOptions> currentCatagory, PropertyInfo property, uint position)
            : base(catagoryCreator, currentCatagory, property, position)
        {
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
                throw new Exception($"Tried to add a value to the MultiParamter '{ParameterName}' but the IsMultiple flag wasn't set.");
            }
        }
    }
}
