using System;
using System.Collections.Generic;
using System.Text;

namespace argparse
{
    public enum HelpDisplayMode
    {
        /// <summary>
        /// Just the usage and preable
        /// </summary>
        Compact,
        /// <summary>
        /// The main help documentation that is displayed with --help
        /// </summary>
        Expanded,
        /// <summary>
        /// Includes the description on enum values and other such things
        /// </summary>
        Full
    }
}
