using argparse;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SimpleArgumentSample
{
    class Program
    {
        //new BasicArgumentParser();
        // No such thing as scoped commands. Cannot have multiple 'multiple' positional arguments.
        // Last positional argument can be the only one with 'multiple' set to true
        // Can mix and match options throughout the command

        // Usage: app [OPTIONS] FILE [FILE...]
        //        app [OPTIONS] SOURCE DESTINATION
        //        app [OPTIONS] SOURCE DESTINATION [DESTINATION...]
        // app [opt] [source ] [ dest  ] [                            opt                      ] [  dest  ]
        // app -f -v file1.txt file3.txt -vvpoD --long-name-argument --argument-with-value value file46.txt

        //new ScopedArgumentParser();
        // Only scoped arguments allowed. First positional must be global scope arguments, second positional must be scope name.
        // After scope name all arguments are arguments for that scope.
        // Cannot mix and match scope-name/global arguments.
        // Can mix and match scope arguments. (After scope name is declared arguments after scope is a BasicArgumentParser)

        // Usage: app [OPTIONS] SCOPE [BasicArgumentParser]
        static void Main(string[] args)
        {
            //ArgumentParser
            //    .Default
            //    .CreateArgumentCatagory<GeneralOptions>()
            //        .WithArgument(x => x.Flag)
            //            .Required()
            //            .Name("blah")
            //        .WithArgument(x => x.CountMe)
            //    .CreateArgumentCatagory<NetworkOptions>()
            //        .WithArgument(x => x.Integer)
            //            .Name("intplx")
            //            .Flag('i');

            //ArgumentParser
            //    .Default
            //    .CreateParameterCatagory<PositionalOptions>()
            //        .WithParameter(x => x.Source)
            //            .Name("string")
            //        .WithParameter(x => x.Destination)
            //            .Name("dest");

            //ArgumentParser
            //    .Default
            //    .CreateCommandCatagory<Commands>()
            //        .Name("comANDS")
            //        .WithCommand(x => x.Config, SetupConfigCommand)
            //            .Help("THis is config")
            //    .CreateCommandCatagory<SimpleCommands>()
            //        .WithCommand(x => x.Config);

            //ArgumentParser.Default.Parse(args);

            //GeneralOptions go = ArgumentParser.Default.GetArgumentCatagory<GeneralOptions>();
            //NetworkOptions no = ArgumentParser.Default.GetArgumentCatagory<NetworkOptions>();
            //PositionalOptions po = ArgumentParser.Default.GetParameterCatagory<PositionalOptions>();

            //Commands c = ArgumentParser.Default.GetCommandCatagory<Commands>();
            //SimpleCommands sc = ArgumentParser.Default.GetCommandCatagory<SimpleCommands>();

            //ConfigCommands cc = c.Config.GetArgumentCatagory<ConfigCommands>();

            ArgumentParser.Default.CreateArgumentCatagory<GeneralOptions>().WithArgument(x => x.String);
            ArgumentParser.Default.Parse(args);

        }

        static void SetupConfigCommand(IArgumentParser argParser)
        {
            argParser
                .CreateArgumentCatagory<ConfigCommands>()
                    .Name("Options")
                    .WithArgument(x => x.Basic);
        }
    }

    class GeneralOptions
    {
        public bool Flag { get; set; }

        public NetworkOptions Network { get; set; }
        public DateTime DateTime { get; set; }
        public TypeCode MyEnum { get; set; }
        public IEnumerable<string> List { get; set; }
        public string String { get; set; }

        public uint CountMe { get; set; }
    }

    class NetworkOptions
    {
        public int Integer { get; set; }
    }

    class ConfigCommands
    {
        public string Basic { get; set; }
    }

    class Commands
    {
        public IArgumentParser Config { get; set; }
    }

    class SimpleCommands
    {
        public bool Config { get; set; }
    }

    class PositionalOptions
    {
        public string Source { get; set; }
        public IEnumerable<string> Destination { get; set; }
    }
}