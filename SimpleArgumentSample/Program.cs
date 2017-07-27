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

            ///
            /// Basic
            ///

            ArgumentParser.ParserMode = ParserMode.Basic;

            ArgumentParser
                .ArgumentCatagory(name: "General Options", position: 1)
                    .AddArgument<GeneralOptions>(property: x => x.Flag, flag: 'f', name: "flag", type: typeof(bool), help: "help doco for this thing", required: true);

            ArgumentParser
                .ArgumentCatagory(name: "Network Options", position: 2)
                    .AddArguments<NetworkOptions>();

            ArgumentParser.AddPositionalArgument<string>(description: "source", position: 1, multiple: true);
            ArgumentParser.AddPositionalArgument<string>(description: "dest", position: 2, multiple: true);

            ArgumentParser.Parse(args);

            AddArgument
            AddMultiArgument
            AddCountArgument

            ///
            /// Scoped
            ///

            ArgumentParser.ParserMode = ParserMode.Scoped;

            ArgumentParser.AddArguments<Options>(name: "Options");

            ArgumentParser
                .CommandCatagory(name: "Management Commands")
                    .AddCommand(name: "config", help: "The config of the thing", argumentParser: new BasicArgumentParser())
                    .AddCommand(name: "image", help: "Gets images", argumentParser: new BasicArgumentParser());

            ArgumentParser
                .CommandCatagory(name: "Commands")
                    .AddCommand(name: "ps", help: "List stuff", argumentParser: new BasicArgumentParser());
            
            ArgumentParser.AddArguments<Options>(name: "Options");
            ArgumentParser.CommandCatagory(name: "Commands")

            argparser.AddArguments<GeneralOptions>(scope: "*", description: "Options");
            GeneralOptions go = argparser.GetArguments<GeneralOptions>();
            argparser.AddArguments<GeneralOptions>(scope: "*", description: "General Options");

            var options =  argparser.Catagory("Options");

            options.Catagory("General Options").AddArguments<GeneralOptions>();
            options.Catagory("Network Options").AddArguments<GeneralOptions>();
        }
    }

    class GeneralOptions
    {
        [Argument(flag: 'f', name: "flag", type: typeof(bool), help: "help doco for this thing", required: true)]
        public bool Flag { get; set; }

        public NetworkOptions Network { get; set; }
        public DateTime DateTime { get; set; }
        public TypeCode MyEnum { get; set; }
        public IEnumerable<string> List { get; set; }
        public string Str { get; set; }
    }

    class NetworkOptions
    {
        [Argument(flag: 'i', name: 'countme', type: typeof(int), count: true, help: "This will count number of times you use it")]
        public int Integer { get; set; }
    }
}