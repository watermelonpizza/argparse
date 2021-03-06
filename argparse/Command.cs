﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace argparse
{
    internal class Command<TOptions> : ICommand<TOptions>, IProperty
        where TOptions : class, new()
    {
        private ICommandCatagory<TOptions> _currentCatagory;
        private ICreateCommandCatagory _catagoryCreator;

        public string CommandName { get; set; } = string.Empty;

        public string CommandHelp { get; set; } = string.Empty;

        public string CommandSummary { get; set; } = string.Empty;

        public PropertyInfo Property { get; }

        public bool ValueSet { get; private set; }

        public Command(ICreateCommandCatagory catagoryCreator, ICommandCatagory<TOptions> currentCatagory, PropertyInfo property)
        {
            if (property.GetMethod == null || property.SetMethod == null)
            {
                throw new ArgumentException(
                    $"Property '{property.Name}' must have both a get and set accessor on catagory '{typeof(TOptions).Name}'.",
                    nameof(property));
            }

            _catagoryCreator = catagoryCreator;
            _currentCatagory = currentCatagory;
            Property = property;

            Name(property.Name);
        }

        public void AddIfMultiple(object obj)
        {
            throw new InvalidOperationException("How did you get here? This shouldn't happen. Commands can only be bool or IArgumentParser type");
        }

        public void SetValue(object obj)
        {
            if (obj?.GetType() != Property.PropertyType) { } // TODO: Throw exception if different types

            ICatagoryInstance instance = _currentCatagory as ICatagoryInstance;
            Property.SetValue(instance.CatagoryInstance, obj);

            ValueSet = true;
        }

        public object GetValue()
        {
            ICatagoryInstance instance = _currentCatagory as ICatagoryInstance;
            return Property.GetValue(instance.CatagoryInstance);
        }

        public ICommand<TOptions> Help(string help)
        {
            CommandHelp = help;

            return this;
        }

        public ICommand<TOptions> Summary(string summary)
        {
            CommandSummary = summary;

            return this;
        }

        public ICommand<TOptions> Name(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"{Property.Name} names cannot be empty or null.", nameof(name));
            }

            CommandName = ArgumentHelper.FormatCommandName(name);

            return this;
        }

        public ICommand<TOptions> WithCommand(Expression<Func<TOptions, bool>> command)
        {
            return _currentCatagory.WithCommand(command);
        }

        public ICommand<TOptions> WithCommand(Expression<Func<TOptions, ICommandArgumentParser>> command, Action<IArgumentParser> parser)
        {
            return _currentCatagory.WithCommand(command, parser);
        }

        public ICommandCatagory<TOptions1> CreateCommandCatagory<TOptions1>()
            where TOptions1 : class, new()
        {
            return _catagoryCreator.CreateCommandCatagory<TOptions1>();
        }

        public TOptions1 GetCommandCatagory<TOptions1>()
            where TOptions1 : class, new()
        {
            return _catagoryCreator.GetCommandCatagory<TOptions1>();
        }
    }
}