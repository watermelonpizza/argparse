using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace argparse
{
    internal class CommandCatagory<TOptions> : ICommandCatagory<TOptions>, ICatagoryInstance
        where TOptions : class, new()
    {
        private ICreateCommandCatagory _catagoryCreator;
        private List<ICommand> _commands = new List<ICommand>();

        public IEnumerable<ICommand> Commands => _commands;

        public string CatagoryName { get; private set; }

        public object CatagoryInstance { get; } = new TOptions();

        public CommandCatagory(ICreateCommandCatagory catagoryCreator, string name)
        {
            _catagoryCreator = catagoryCreator;
            CatagoryName = ArgumentHelper.FormatModuleName(name);
        }

        public ICommandCatagory<TOptions> Name(string name)
        {
            CatagoryName = name;

            return this;
        }

        public ICommand<TOptions> WithCommand(Expression<Func<TOptions, bool>> command)
        {
            PropertyInfo property = (command.Body as MemberExpression).Member as PropertyInfo;

            if (_commands.Any(a => a.Property == property))
            {
                throw new ArgumentException($"Command '{property.Name}' has already been added to the catagory '{typeof(TOptions).Name}' and cannot be set twice.");
            }

            var cmd = new Command<TOptions>(_catagoryCreator, this, property);
            _commands.Add(cmd);

            return cmd;
        }

        public ICommand<TOptions> WithCommand(Expression<Func<TOptions, ICommandArgumentParser>> command, Action<IArgumentParser> parser)
        {
            PropertyInfo property = (command.Body as MemberExpression).Member as PropertyInfo;

            var cmd = new Command<TOptions>(_catagoryCreator, this, property);
            _commands.Add(cmd);

            ICommandArgumentParser commandArgParser = new CommandArgumentParser();
            parser(commandArgParser);

            property.SetValue(CatagoryInstance, commandArgParser);

            return cmd;
        }
    }
}
