using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace argparse
{
    public class Argument<TOptions, TArgument> : IArgument<TOptions, TArgument>
    {
        private IArgumentCatagory<TOptions> _currentCatagory;
        private ICreateArgumentCatagory _catagoryCreator;

        /// <summary>
        /// A single alphanumeric character used as a reprisentation for the argument. E.g. -h
        /// Does not require a prefix (i.e -, -- or / etc.).
        /// <c>\0</c> = no flag for this argument.
        /// </summary>
        public char ArgumentFlag { get; private set; }

        /// <summary>
        /// The fully-named argument. E.g. --help
        /// Does not require a prefix (i.e -, -- or / etc.).
        /// </summary>
        public string ArgumentName { get; private set; }

        /// <summary>
        /// The help documentation for the argument
        /// </summary>
        public string ArgumentHelp { get; private set; }

        /// <summary>
        /// Sets the arguments to be a counter for the number of times the argument is used.
        /// Arguments property MUST be of type <see cref="short"/>, <see cref="int"/>, <see cref="uint"/>, <see cref="long"/> or <see cref="ulong"/>.
        /// </summary>
        public bool IsCountable { get; private set; }

        /// <summary>
        /// The default value for the argument if argument is not defined/supplied by the user.
        /// If set it must match the type of <see cref="Type"/>.
        /// <c>null</c> stops this from happening. Does not set the argument property to <see cref="DefaultValue"/> if the argument is not supplied.
        /// </summary>
        public object ArgumentDefaultValue { get; private set; }

        /// <summary>
        /// Whether the argument is required to be specified or not.
        /// </summary>
        public bool IsRequired { get; private set; }

        /// <summary>
        /// Supports the ability to have multiple arguments with the same name/flag with different argumetn parameters.
        /// The argument type <see cref="TArgument"/> must be <see cref="IEnumerable{T}"/>.
        /// This is set implicitlely based on the type. You cannot set this manually. 
        /// </summary>
        public bool IsMultiple { get; private set; }
        
        /// <summary>
        /// The property info of the argument
        /// </summary>
        public PropertyInfo Property { get; private set; }

        public Argument(ICreateArgumentCatagory catagoryCreator, IArgumentCatagory<TOptions> currentCatagory, PropertyInfo property)
        {
            if (!ArgumentParser.SupportedTypes.Contains(typeof(TArgument)))
            {
                throw new ArgumentException(
                    $"{typeof(TArgument).Name} is not supported as an argument type for {property.Name}. Please use only one of the supported types as found in {nameof(ArgumentParser.SupportedTypes)}",
                    nameof(TArgument));
            }

            _catagoryCreator = catagoryCreator;
            _currentCatagory = currentCatagory;
            Property = property;

            Name(property.Name);
        }

        public IArgument<TOptions, TArgument> Countable()
        {
            if (typeof(TArgument) != typeof(short) &&
                typeof(TArgument) != typeof(int) &&
                typeof(TArgument) != typeof(uint) &&
                typeof(TArgument) != typeof(long) &&
                typeof(TArgument) != typeof(ulong))
            {
                throw new Exception(
                    $"This argument cannot be set to countable if the underlying argument type is not one of the supported interger types. See summary on {nameof(IsCountable)} for more information");
            }

            IsCountable = true;

            return this;
        }

        public IArgumentCatagory<TOptions1> CreateArgumentCatagory<TOptions1>()
            where TOptions1 : class, new()
        {
            return _catagoryCreator.CreateArgumentCatagory<TOptions1>();
        }

        public IArgument<TOptions, TArgument> DefaultValue(TArgument value)
        {
            ArgumentDefaultValue = value;

            return this;
        }

        public IArgument<TOptions, TArgument> Flag(char flag)
        {
            if (flag != ArgumentHelper.NoFlag && !Regex.IsMatch(flag.ToString(), ArgumentHelper.FlagMatchPattern))
            {
                throw new ArgumentException($"{nameof(flag)} must only be an alphanumeric charater. Match pattern: { ArgumentHelper.FlagMatchPattern }", nameof(flag));
            }

            ArgumentFlag = flag;

            return this;
        }

        public TOptions1 GetArgumentCatagory<TOptions1>()
            where TOptions1 : class, new()
        {
            return _catagoryCreator.GetArgumentCatagory<TOptions1>();
        }

        public IArgument<TOptions, TArgument> Help(string help)
        {
            ArgumentHelp = help ?? string.Empty;

            return this;
        }

        public IArgument<TOptions, TArgument> Name(string name)
        {
            if (name == null || !Regex.IsMatch(name, ArgumentHelper.NameMatchPattern))
            {
                throw new ArgumentException(
                    $"{nameof(name)} must only be compromised of letters, numbers or hyphens, must be at least two characters and must start with a letter. Match pattern: { ArgumentHelper.NameMatchPattern }",
                    nameof(name));
            }

            ArgumentName = name;

            return this;
        }

        public IArgument<TOptions, TArgument> Required()
        {
            IsRequired = true;

            return this;
        }

        public IArgument<TOptions, TArgument1> WithArgument<TArgument1>(Expression<Func<TOptions, TArgument1>> argument)
        {
            return _currentCatagory.WithArgument(argument);
        }
    }
}
