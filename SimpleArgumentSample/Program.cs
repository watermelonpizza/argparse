using argparse;
using argparse.Attributes;
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
            ArgumentParser parser = ArgumentParser.Create(
                "simpleargumentsample",
                "Simple Argument Sample Application");

            parser
                .CreateArgumentCatagory<GeneralOptions>()
                    .WithArgument(x => x.Flag)
                        .Required()
                        .Name("blah")
                        .DefaultValue(false)
                    .WithArgument(x => x.MyEnum)
                        .Help("This is the enum test. Should put the possible values after this.")
                        .DefaultValue(MyEnum.Option2)
                    .WithArgument(x => x.CountMe)
                        .Help("This is a reallllllllly long help content box that will have to wrap around and stuff. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.")
                        .DefaultValue(10)
                .CreateArgumentCatagory<NetworkOptions>()
                    .WithArgument(x => x.Integer)
                        .Name("intplx")
                        .Flag('i')
                        .Help("Some help here is good for other things and stuff");

            parser
                .CreateParameterCatagory<PositionalOptions>()
                    .WithParameter(x => x.Source)
                        .Required()
                    .WithMultiParameter(x => x.Destination)
                        .Required()
                        .Name("dest");

            parser
                .CreateCommandCatagory<Commands>()
                    .Name("comANDS")
                    .WithCommand(x => x.Config, SetupConfigCommand)
                        .Help("THis is config")
                .CreateCommandCatagory<SimpleCommands>()
                    .WithCommand(x => x.Config);

            parser.Parse(args);

            //GeneralOptions go = parser.GetArgumentCatagory<GeneralOptions>();
            //NetworkOptions no = parser.GetArgumentCatagory<NetworkOptions>();
            //PositionalOptions po = parser.GetParameterCatagory<PositionalOptions>();

            //Commands c = parser.GetCommandCatagory<Commands>();
            //SimpleCommands sc = parser.GetCommandCatagory<SimpleCommands>();

            //ConfigCommands cc = c.Config.GetArgumentCatagory<ConfigCommands>();

            //foreach (var item in args)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine();
            //parser.CreateArgumentCatagory<GeneralOptions>().WithArgument(x => x.String);
            //parser.Parse(args);

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
        [Help("The help")]
        [Name("my-enum")]
        public MyEnum MyEnum { get; set; }
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
        public ICommandArgumentParser Config { get; set; }
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
    
    [Flags]
    enum MyEnum
    {
        [Help("This is option one")]
        Option1,
        [Help("This is the second option")]
        Option2,
        Option3
    }
}