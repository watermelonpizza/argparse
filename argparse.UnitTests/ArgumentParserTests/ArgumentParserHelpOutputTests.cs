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
            Console.SetOut(_consoleOutputHook);
            Console.SetError(_consoleOutputHook);
        }

        [Fact]
        public void BasicHelpOutput()
        {
            ArgumentParser.Create("app").Parse();

            Assert.Equal($"Usage: app{nl}{nl}Run 'app --help' to see a list of all options and more information.{nl}", _consoleOutputHook.RawString.ToString());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void BasicHelpOutputWithEmptyPreambleIsExcluded(string preamble)
        {
            ArgumentParser.Create("app", preamble).Parse();

            Assert.Equal($"Usage: app{nl}{nl}Run 'app --help' to see a list of all options and more information.{nl}", _consoleOutputHook.RawString.ToString());
        }

        [Theory]
        [InlineData("Some preamble")]
        [InlineData(@"Some multi
line
preable")]
        [InlineData(@"Badly\nFormatted\nPreamble")]
        public void BasicHelpOutputWithPreamble(string preamble)
        {
            ArgumentParser.Create("app", preamble).Parse();

            Assert.Equal($"{preamble}{nl}{nl}Usage: app{nl}{nl}Run 'app --help' to see a list of all options and more information.{nl}", _consoleOutputHook.RawString.ToString());
        }
    }
}
