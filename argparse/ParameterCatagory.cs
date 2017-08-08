using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace argparse
{
    internal class ParameterCatagory<TArgumentOptions> : IParameterCatagory<TArgumentOptions>
        where TArgumentOptions : new()
    {
        private ICreateParameterCatagory _catagoryCreator;
        private List<IParameter> _parameters = new List<IParameter>();

        public IEnumerable<IParameter> Parameters => _parameters;

        public string CatagoryName { get; private set; }

        public object CatagoryInstance { get; } = new TArgumentOptions();

        public ParameterCatagory(ICreateParameterCatagory catagoryCreator, string name)
        {
            _catagoryCreator = catagoryCreator;
            CatagoryName = ArgumentHelper.FormatModuleName(name);
        }

        public IParameterCatagory<TArgumentOptions> Name(string name)
        {
            CatagoryName = name;

            return this;
        }

        public IParameter<TArgumentOptions, TArgument> WithParameter<TArgument>(Expression<Func<TArgumentOptions, TArgument>> argument)
        {
            PropertyInfo property = (argument.Body as MemberExpression).Member as PropertyInfo;

            if (_parameters.Any(a => a.Property == property))
            {
                throw new ArgumentException($"Property '{property.Name}' has already been added to the catagory '{typeof(TArgumentOptions).Name}' and cannot be set twice.");
            }

            int position = _parameters.Count;
            var arg = new Parameter<TArgumentOptions, TArgument>(_catagoryCreator, this, property, (uint)position);

            if (arg.IsMultiple && _parameters.Any(p => p.IsMultiple))
            {
                throw new ArgumentException($"{argument.Name} is set to be a multi parameter but there is already one defined. You cannot have two multi-parameters in one catagory.", nameof(argument));
            }

            _parameters.Add(arg);

            return arg;
        }
    }
}
