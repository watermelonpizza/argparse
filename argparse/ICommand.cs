using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace argparse
{
    public interface ICommand
    {
        /// <summary>
        /// The name of the command
        /// </summary>
        string CommandName { get; }

        /// <summary>
        /// The commands help documentation (used as a short description to display next to the command in the list of commands)
        /// </summary>
        /// <example>
        /// $ app.exe --help
        /// Usage: app.exe [COMMAND]
        ///
        /// Commands:
        ///   mycommand  CommandHelp is displayed here
        /// </example>
        string CommandHelp { get; }

        /// <summary>
        /// The summary for the command, used for a more detailed description of the command if the help argument is called on it
        /// </summary>
        /// <example>
        /// $ app.exe mycommand --help
        /// Usage: app.exe mycommand [OPTIONS]
        ///
        /// This is the CommandDescription area
        ///
        /// Options:
        ///   --my-arg  Some help for the argument
        /// </example>
        string CommandSummary { get; }

        /// <summary>
        /// The information about the property
        /// </summary>
        PropertyInfo Property { get; }
    }

    public interface ICommand<TOptions> : ICommand, IWithCommand<TOptions>, ICreateCommandCatagory
        where TOptions : class, new()
    {
        /// <summary>
        /// Sets the name of the command
        /// </summary>
        ICommand<TOptions> Name(string name);

        /// <summary>
        /// Sets the help documentation of the command
        /// </summary>
        ICommand<TOptions> Help(string help);

        /// <summary>
        /// Sets the commands summary
        /// </summary>
        ICommand<TOptions> Summary(string summary);
    }
}
