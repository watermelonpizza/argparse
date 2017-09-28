using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace argparse
{
    /// <summary>
    /// The interface which represents an arguments core details without type specific information
    /// </summary>
    public interface IArgument
    {
        /// <summary>
        /// The fully-named argument. E.g. --help
        /// Does not require a prefix (i.e -, -- or / etc.).
        /// </summary>
        string ArgumentName { get; }

        /// <summary>
        /// A single alphanumeric character used as a reprisentation for the argument. E.g. -h
        /// Does not require a prefix (i.e -, -- or / etc.).
        /// <see cref="ArgumentHelper.NoFlag"/> = no flag for this argument.
        /// </summary>
        char ArgumentFlag { get; }

        /// <summary>
        /// The arguments type (i.e. the type of the property)
        /// </summary>
        Type ArgumentType { get; }

        /// <summary>
        /// Whether the argument is required to be specified or not.
        /// </summary>
        bool IsRequired { get; }

        /// <summary>
        /// Sets the arguments to be a counter for the number of times the argument is used.
        /// Arguments property MUST be of type <see cref="short"/>, <see cref="int"/>, <see cref="uint"/>, <see cref="long"/> or <see cref="ulong"/>.
        /// </summary>
        bool IsCountable { get; }

        /// <summary>
        /// Supports the ability to have multiple arguments with the same name/flag with different argumetn parameters.
        /// The argument type <see cref="TArgument"/> must be <see cref="ImmutableArray{T}"/>.
        /// This is set implicitlely based on the type. You cannot set this manually. 
        /// </summary>
        bool IsMultiple { get; }

        /// <summary>
        /// If the argument is an <see cref="Enum"/>
        /// </summary>
        bool IsEnum { get; }

        /// <summary>
        /// If the argument is an <see cref="Enum"/> with the <see cref="FlagsAttribute"/>.
        /// </summary>
        bool IsFlags { get; }

        /// <summary>
        /// The default value for the argument if argument is not defined/supplied by the user.
        /// If set it must match the type of <see cref="Type"/>.
        /// <c>null</c> stops this from happening.
        /// </summary>
        object ArgumentDefaultValue { get; }

        /// <summary>
        /// Whether or not the <see cref="ArgumentDefaultValue"/> was set
        /// </summary>
        bool ArgumentDefaultSet { get; }

        /// <summary>
        /// The help documentation for the argument
        /// </summary>
        string ArgumentHelp { get; }

        /// <summary>
        /// The property info of the argument
        /// </summary>
        PropertyInfo Property { get; }
    }

    /// <summary>
    /// An interface with represents an argument
    /// </summary>
    /// <typeparam name="TCatagory">The type of the argument catagory</typeparam>
    /// <typeparam name="TArgument">The type of the argument</typeparam>
    public interface IArgument<TCatagory, TArgument> : IArgument, IWithArgument<TCatagory>, ICreateArgumentCatagory
    {
        /// <summary>
        /// Sets the fully-named argument. E.g. --help
        /// Does not require a prefix (i.e -, -- or / etc.).
        /// </summary>
        /// <param name="name">The argument name (without prefix)</param>
        IArgument<TCatagory, TArgument> Name(string name);

        /// <summary>
        /// Sets the flag for the argument.
        /// A single alphanumeric character used as a reprisentation for the argument. E.g. -h
        /// Does not require a prefix (i.e -, -- or / etc.).
        /// <see cref="ArgumentHelper.NoFlag"/> = no flag for this argument.
        /// </summary>
        /// <param name="flag">The argument flag (without prefix)</param>
        /// <returns></returns>
        IArgument<TCatagory, TArgument> Flag(char flag);

        /// <summary>
        /// Sets the argument to be required
        /// </summary>
        IArgument<TCatagory, TArgument> Required();

        /// <summary>
        /// Sets the arguments to be a counter for the number of times the argument is used.
        /// Arguments property MUST be of type <see cref="short"/>, <see cref="int"/>, <see cref="uint"/>, <see cref="long"/> or <see cref="ulong"/>.
        /// </summary>
        IArgument<TCatagory, TArgument> Countable();

        /// <summary>
        /// Sets the default value for the argument if argument is not defined/supplied by the user.
        /// If set it must match the type of <see cref="Type"/>.
        /// <c>null</c> stops this from happening.
        /// </summary>
        /// <param name="value">The default value for the argument</param>
        IArgument<TCatagory, TArgument> DefaultValue(TArgument value);

        /// <summary>
        /// Sets the help documentation for the argument
        /// </summary>
        /// <param name="help">The help text</param>
        IArgument<TCatagory, TArgument> Help(string help);
    }
}
