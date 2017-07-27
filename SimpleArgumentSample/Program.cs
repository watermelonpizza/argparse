using argparse;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SimpleArgumentSample
{
    class Program
    {
        static void Main(string[] args)
        {
            dynamic argparser = null; // BasicArgumentParser

            argparser.AddArguments<GlobalScopeCommands>(scope: "*", description: "Options");
            argparser.AddArguments<GlobalScopeCommands>(scope: "*", description: "General Options");
            argparser.AddPositionalArgument<string>(description: "files", position: 2, multiple: true);
        }
    }

    class GlobalScopeCommands
    {
        [Argument(flag: 'f', name: "flag", type: typeof(bool), help: "help doco for this thing", required: true)]
        public bool Flag { get; set; }
        
        public string File { get; set; }
    }
}