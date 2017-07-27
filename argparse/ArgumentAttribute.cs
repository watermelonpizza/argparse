using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace argparse
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class ArgumentAttribute : Attribute
    {
        private const string FlagMatchPattern = "^[a-zA-Z0-9]$";
        private const string NameMatchPattern = "^[a-zA-Z][a-zA-Z0-9-]+$";
        private const char NoFlag = char.MinValue;

        /// <summary>
        /// A single alphanumeric character used as a reprisentation for the argument. E.g. -h
        /// Does not require a prefix (i.e -, -- or / etc.).
        /// <c>\0</c> = no flag for this argument.
        /// </summary>
        public char Flag { get; }

        /// <summary>
        /// The fully-named argument. E.g. --help
        /// Does not require a prefix (i.e -, -- or / etc.).
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The type of the argument that will be parsed. Must be the same type as the property.
        /// See <see cref="ArgumentParser.SupportedTypes"/> for a list of currently supported argument types.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// The help documentation for the argument
        /// </summary>
        public string Help { get; }

        /// <summary>
        /// Sets the arguments to be a counter for the number of times the argument is used.
        /// Arguments property MUST be of type <see cref="short"/>, <see cref="int"/>, <see cref="uint"/>, <see cref="long"/> or <see cref="ulong"/>.
        /// </summary>
        public bool Count { get; }

        /// <summary>
        /// The default value for the argument if argument is not defined/supplied by the user.
        /// If set it must match the type of <see cref="Type"/>.
        /// <c>null</c> stops this from happening. Does not set the argument property to <see cref="DefaultValue"/> if the argument is not supplied.
        /// </summary>
        public object DefaultValue { get; }

        /// <summary>
        /// Whether the argument is required to be specified or not.
        /// Cannot be used with <see cref="FallThroughSilentlyOnError"/> set to <c>true</c>.
        /// </summary>
        public bool Required { get; }

        /// <summary>
        /// If <c>true</c> and the argument is malformed or unable to be read correctly 
        /// the parser will set the argument to <see cref="DefaultValue"/> and move on to the next argument.
        /// Requires <see cref="DefaultValue"/> to be defined and cannot be null.
        /// Cannot be used with <see cref="Required"/> set to <c>true</c>.
        /// </summary>
        public bool FallThroughSilentlyOnError { get; }

        /// <summary>
        /// Marks this property as an argument for the parser to use
        /// </summary>
        /// <param name="name">
        /// The fully-named argument. E.g. --help
        /// Does not require a prefix (i.e -, -- or / etc.).
        /// </param>
        /// <param name="type">
        /// The type of the argument that will be parsed. Must be the same type as the property.
        /// See <see cref="ArgumentParser.SupportedTypes"/> for a list of currently supported argument types.
        /// </param>
        /// <param name="help">The help documentation for the argument</param>
        /// <param name="count">
        /// Sets the arguments to be a counter for the number of times the argument is used.
        /// Arguments property MUST be of type <see cref="short"/>, <see cref="int"/>, <see cref="uint"/>, <see cref="long"/> or <see cref="ulong"/>.
        /// </param>
        /// <param name="flag">
        /// A single alphanumeric character used as a reprisentation for the argument. E.g. -h
        /// Does not require a prefix (i.e -, -- or / etc.).
        /// <c>null</c> = no flag for this argument.
        /// </param>
        /// <param name="defaultValue">
        /// The default value for the argument if argument is not defined/supplied by the user.
        /// If set it must match the type of <paramref name="type"/>.
        /// <c>null</c> stops this from happening. Does not set the argument property to <paramref name="defaultValue"/> if the argument is not supplied.
        /// <param name="required">
        /// Whether the argument is required to be specified or not.
        /// Cannot be used with <paramref name="fallThroughSilentlyOnError"/> set to <c>true</c>.
        /// </param>
        /// <param name="fallThroughSilentlyOnError">
        /// If <c>true</c> and the argument is malformed or unable to be read correctly 
        /// the parser will set the argument to <paramref name="defaultValue"/> and move on to the next argument.
        /// Requires <paramref name="defaultValue"/> to be defined and cannot be null.
        /// Cannot be used with <paramref name="required"/> set to <c>true</c>.
        /// </param>
        public ArgumentAttribute(
            string name,
            Type type,
            char flag = NoFlag,
            string help = "",
            bool count = false,
            object defaultValue = null,
            bool required = false,
            bool fallThroughSilentlyOnError = false)
        {
            if (name == null || !Regex.IsMatch(name, NameMatchPattern))
            {
                throw new ArgumentException(
                    $"{nameof(name)} must only be compromised of letters, numbers or hyphens, must be at least two characters and must start with a letter. Match pattern: {NameMatchPattern}",
                    nameof(name));
            }
            
            Name = name;

            if (!ArgumentParser.SupportedTypes.Contains(type))
            {
                throw new ArgumentException(
                    $"{type.Name} is not supported as an argument type for {name}. Please use only one of the supported types as found in {nameof(ArgumentParser.SupportedTypes)}",
                    nameof(type));
            }

            Type = type;

            if (flag != NoFlag && !Regex.IsMatch(flag.ToString(), FlagMatchPattern))
            {
                throw new ArgumentException($"{nameof(flag)} must only be an alphanumeric charater. Match pattern: {FlagMatchPattern}", nameof(flag));
            }

            Flag = flag;

            Help = help ?? string.Empty;

            if (count &&
                type != typeof(short) &&
                type != typeof(int) &&
                type != typeof(uint) &&
                type != typeof(long) &&
                type != typeof(ulong))
            {
                throw new ArgumentException(
                    $"{nameof(count)} cannot be set to true if the underlying argument type is not one of the supported interger types. See summary on {nameof(Count)} for more information",
                    nameof(count));
            }

            Count = count;

            if (defaultValue != null && defaultValue.GetType() != type)
            {
                throw new ArgumentException($"The type of the {nameof(defaultValue)} and the {nameof(type)} must match.", nameof(defaultValue));
            }

            DefaultValue = defaultValue;
            Required = required;

            if (required && fallThroughSilentlyOnError)
            {
                throw new ArgumentException($"{nameof(fallThroughSilentlyOnError)} cannot be set if {nameof(required)} is set", nameof(fallThroughSilentlyOnError));
            }

            if (fallThroughSilentlyOnError && defaultValue == null)
            {
                throw new ArgumentException($"{nameof(fallThroughSilentlyOnError)} cannot be set if {nameof(defaultValue)} is null", nameof(fallThroughSilentlyOnError));
            }

            FallThroughSilentlyOnError = fallThroughSilentlyOnError;
        }
    }
}
