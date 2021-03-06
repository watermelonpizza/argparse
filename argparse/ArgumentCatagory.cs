﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
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

        public ImmutableArray<IArgument> Arguments => _arguments.ToImmutableArray();

        public string CatagoryName { get; private set; }

        public object CatagoryInstance { get; } = new TOptions();

        public ArgumentCatagory(ICreateArgumentCatagory catagoryCreator, string name)
        {
            _catagoryCreator = catagoryCreator;
            CatagoryName = ArgumentHelper.FormatModuleName(name);
        }

        public IArgumentCatagory<TOptions> Name(string name)
        {
            CatagoryName = name;

            return this;
        }

        public IArgument<TOptions, TArgument> WithArgument<TArgument>(Expression<Func<TOptions, TArgument>> argument)
        {
            PropertyInfo property = (argument.Body as MemberExpression).Member as PropertyInfo;

            if (_arguments.Any(a => a.Property == property))
            {
                throw new ArgumentException($"Property '{property.Name}' has already been added to the catagory '{typeof(TOptions).Name}' and cannot be set twice.");
            }

            var arg = new Argument<TOptions, TArgument>(_catagoryCreator, this, property);
            _arguments.Add(arg);

            return arg;
        }

        public IArgument<TOptions, TArgument> WithMultiArgument<TArgument>(Expression<Func<TOptions, ImmutableArray<TArgument>>> argument)
        {
            PropertyInfo property = (argument.Body as MemberExpression).Member as PropertyInfo;

            if (_arguments.Any(a => a.Property == property))
            {
                throw new ArgumentException($"Property '{property.Name}' has already been added to the catagory '{typeof(TOptions).Name}' and cannot be set twice.");
            }

            var arg = new MultiArgument<TOptions, TArgument>(_catagoryCreator, this, property);
            _arguments.Add(arg);

            return arg;
        }
    }
}
