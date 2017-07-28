using System;
using System.Collections.Generic;
using System.Text;

namespace argparse
{
    public class Argument<T> : IArgument<T>
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
        public T Type { get; }

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
    }
}
