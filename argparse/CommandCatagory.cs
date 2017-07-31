﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace argparse
{
    public class CommandCatagory<TOptions> : ICommandCatagory<TOptions>
        where TOptions : class, new()
    {
        private ICreateCommandCatagory _catagoryCreator;
        private List<ICommand> _commands = new List<ICommand>();

        public IEnumerable<ICommand> Commands => _commands;

        public string CommandCatagoryName { get; private set; }

        internal TOptions CatagoryInstance { get; } = new TOptions();

        public CommandCatagory(ICreateCommandCatagory catagoryCreator, string name)
        {
            _catagoryCreator = catagoryCreator;
            CommandCatagoryName = name;
        }

        public ICommandCatagory<TOptions> Name(string name)
        {
            CommandCatagoryName = name;

            return this;
        }

        public ICommand<TOptions> WithCommand(Expression<Func<TOptions, bool>> command)
        {
            PropertyInfo property = (command.Body as MemberExpression).Member as PropertyInfo;

            var cmd = new Command<TOptions>(_catagoryCreator, this, property);
            _commands.Add(cmd);

            return cmd;
        }

        public ICommand<TOptions> WithCommand(Expression<Func<TOptions, IArgumentParser>> command, Action<IArgumentParser> parser)
        {
            PropertyInfo property = (command.Body as MemberExpression).Member as PropertyInfo;

            var cmd = new Command<TOptions>(_catagoryCreator, this, property);
            _commands.Add(cmd);

            IArgumentParser commandArgParser = new ArgumentParser();
            parser(commandArgParser);

            property.SetValue(CatagoryInstance, commandArgParser);

            return cmd;
        }
    }
}