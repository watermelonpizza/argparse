using Xunit;

namespace argparse.UnitTests
{
    public class ArgumentParserParseTests
    {
        [Fact]
        public void NameParserShouldBeCaseInsensitive()
        {
            ArgumentParser parser = new ArgumentParser();
            parser.CreateArgumentCatagory<BasicOptions>().WithArgument(x => x.Boolean);

            parser.Parse("--boolean");

            Assert.True(parser.GetArgumentCatagory<BasicOptions>().Boolean);
        }

        [Fact]
        public void ParseUpperCaseNameShouldSuccess()
        {
            ArgumentParser parser = new ArgumentParser();
            parser.CreateArgumentCatagory<BasicOptions>().WithArgument(x => x.Boolean);

            parser.Parse("--Boolean");

            Assert.True(parser.GetArgumentCatagory<BasicOptions>().Boolean);
        }
    }
}
