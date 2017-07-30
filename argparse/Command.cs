using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace argparse
{
    public class Command<TOptions> : ICommand<TOptions>
        where TOptions : class, new()
    {
        private ICommandCatagory<TOptions> _currentCatagory;
        private ICreateCommandCatagory _catagoryCreator;

        public string CommandName { get; set; }

        public string CommandHelp { get; set; }

        public PropertyInfo Property { get; }

        public Command(ICreateCommandCatagory catagoryCreator, ICommandCatagory<TOptions> currentCatagory, PropertyInfo property)
        {
            _catagoryCreator = catagoryCreator;
            _currentCatagory = currentCatagory;
            Property = property;

            Name(property.Name);
        }

        public ICommand<TOptions> Help(string help)
        {
            CommandHelp = help;

            return this;
        }

        public ICommand<TOptions> Name(string name)
        {
            CommandName = name;

            return this;
        }

        public ICommand<TOptions> WithCommand(Expression<Func<TOptions, bool>> command)
        {
            return _currentCatagory.WithCommand(command);
        }

        public ICommand<TOptions> WithCommand(Expression<Func<TOptions, IArgumentParser>> command, Action<IArgumentParser> parser)
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
