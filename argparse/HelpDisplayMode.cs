using System;
using System.Collections.Generic;
using System.Text;

namespace argparse
{
    public enum HelpDisplayMode
    {
        /// <summary>
        /// Just the usage and preable.
        /// Set when the application was called without any command or arguments.
        /// </summary>
        Compact,
        /// <summary>
        /// The main help documentation that is displayed with any of the help arguments
        /// </summary>
        Expanded,
        /// <summary>
        /// Includes the description on enum values and other such things 
        /// which required a more detailed output.
        /// This is called when 'full' is passed after the help argument.
        /// </summary>
        Full
    }
}
