using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace argparse
{
    public class ParameterCatagory<TArgumentOptions> : IParameterCatagory<TArgumentOptions>
        where TArgumentOptions : new()
    {
        private ICreateParameterCatagory _catagoryCreator;
        private List<IParameter> _arguments = new List<IParameter>();

        public IEnumerable<IParameter> Parameters => _arguments;

        public string ParameterCatagoryName { get; private set; }

        internal TArgumentOptions CatagoryInstance { get; } = new TArgumentOptions();

        public ParameterCatagory(ICreateParameterCatagory catagoryCreator, string name)
        {
            _catagoryCreator = catagoryCreator;
            ParameterCatagoryName = name;
        }

        public IParameterCatagory<TArgumentOptions> Name(string name)
        {
            ParameterCatagoryName = name;

            return this;
        }

        public IParameter<TArgumentOptions, TArgument> WithParameter<TArgument>(Expression<Func<TArgumentOptions, TArgument>> argument)
        {
            PropertyInfo property = (argument.Body as MemberExpression).Member as PropertyInfo;

            var arg = new Parameter<TArgumentOptions, TArgument>(_catagoryCreator, this, property);
            _arguments.Add(arg);

            return arg;
        }

        internal IParameter FindArgument(string name, char flag = ArgumentParser.NoArgumentFlag)
        {
            return _arguments.SingleOrDefault(arg => arg.ParameterName == name);
        }
    }
}
