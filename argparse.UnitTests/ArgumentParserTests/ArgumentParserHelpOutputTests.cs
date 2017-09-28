using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace argparse.UnitTests.ArgumentParserTests
{
    public class ArgumentParserHelpOutputTests
    {
        ConsoleOutputHook _consoleOutputHook;
        string nl = Environment.NewLine;

        public ArgumentParserHelpOutputTests()
        {
            _consoleOutputHook = new ConsoleOutputHook();
        }

        [Fact]
        public void BasicHelpOutput()
        {
            CreateArgumentParser(new ArgumentParserOptions { ApplicationName = "app" }).Parse();

            Assert.Equal($"Usage: app{nl}{nl}Run 'app --help' or 'app --help full' to see a list of all options and more information.{nl}", _consoleOutputHook.RawString.ToString());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void BasicHelpOutputWithEmptyPreambleIsExcluded(string preamble)
        {
            CreateArgumentParser(new ArgumentParserOptions { ApplicationName = "app", Preamble = preamble }).Parse();

            Assert.Equal($"Usage: app{nl}{nl}Run 'app --help' or 'app --help full' to see a list of all options and more information.{nl}", _consoleOutputHook.RawString.ToString());
        }

        [Theory]
        [InlineData("Some preamble")]
        [InlineData(@"Some multi
line
preable")]
        [InlineData(@"Inline\nFormatted\nPreamble")]
        public void BasicHelpOutputWithPreamble(string preamble)
        {
            CreateArgumentParser(new ArgumentParserOptions { ApplicationName = "app", Preamble = preamble }).Parse();

            Assert.Equal($"{preamble}{nl}{nl}Usage: app{nl}{nl}Run 'app --help' or 'app --help full' to see a list of all options and more information.{nl}", _consoleOutputHook.RawString.ToString());
        }

        [Fact]
        public void RequiredParameterShouldThrowErrorOnScreen()
        {
            ArgumentParser parser = CreateArgumentParser(new ArgumentParserOptions { ApplicationName = "app" });
            parser
                .CreateParameterCatagory<BasicOptions>()
                        .WithParameter(x => x.String)
                            .Required();

            parser.Parse();

            Assert.Equal($"app: error: parameter 'STRING' is required{nl}{nl}Usage: app STRING{nl}", _consoleOutputHook.RawString.ToString());
        }

        [Fact]
        public void RequiredArgumentShouldThrowErrorOnScreen()
        {
            ArgumentParser parser = CreateArgumentParser(new ArgumentParserOptions { ApplicationName = "app" });
            parser
                .CreateArgumentCatagory<BasicOptions>()
                        .WithArgument(x => x.String)
                            .Required();

            parser.Parse();

            Assert.Equal($"app: error: argument '--string' is required{nl}{nl}Usage: app [OPTIONS]{nl}", _consoleOutputHook.RawString.ToString());
        }

        [Fact]
        public void ParserShouldHaveNoOutputUponSuccess()
        {
            ArgumentParser parser = CreateArgumentParser(new ArgumentParserOptions { ApplicationName = "app" });
            parser
                .CreateArgumentCatagory<BasicOptions>()
                    .WithArgument(x => x.Boolean);

            parser.Parse("--boolean");

            Assert.Equal(string.Empty, _consoleOutputHook.RawString.ToString());
        }

        private ArgumentParser CreateArgumentParser(ArgumentParserOptions options)
        {
            options.StdOut = _consoleOutputHook;
            options.StdErr = _consoleOutputHook;

            return ArgumentParser.Create(options);
        }
    }
}
