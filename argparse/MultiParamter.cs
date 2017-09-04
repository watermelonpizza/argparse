using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace argparse
{
    internal class MultiParamter<TArgumentOptions, TArgument> : Parameter<TArgumentOptions, TArgument>, IMultiProperty
    {
        public MultiParamter(ICreateParameterCatagory catagoryCreator, IParameterCatagory<TArgumentOptions> currentCatagory, PropertyInfo property, uint position)
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
    }
}
