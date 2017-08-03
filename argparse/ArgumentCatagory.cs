using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace argparse
{
    internal class ArgumentCatagory<TOptions> : IArgumentCatagory<TOptions>, ICatagoryInstance
        where TOptions : new()
    {
        private ICreateArgumentCatagory _catagoryCreator;
        private List<IArgument> _arguments = new List<IArgument>();

        public IEnumerable<IArgument> Arguments => _arguments;

        public string CatagoryName { get; private set; }

        public object CatagoryInstance { get; } = new TOptions();

        public ArgumentCatagory(ICreateArgumentCatagory catagoryCreator, string name)
        {
            _catagoryCreator = catagoryCreator;
            CatagoryName = name;
        }

        public IArgumentCatagory<TOptions> Name(string name)
        {
            CatagoryName = name;

            return this;
        }

        public IArgument<TOptions, TArgument> WithArgument<TArgument>(Expression<Func<TOptions, TArgument>> argument)
        {
            PropertyInfo property = (argument.Body as MemberExpression).Member as PropertyInfo;

            var arg = new Argument<TOptions, TArgument>(_catagoryCreator, this, property);
            _arguments.Add(arg);

            return arg;
        }
    }
}
