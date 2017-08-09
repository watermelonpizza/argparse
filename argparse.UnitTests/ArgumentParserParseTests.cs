using Xunit;

namespace argparse.UnitTests
{
    public class ArgumentParserParseTests
    {
        [Fact]
        public void NameParserCanHandleWindowsPrefix()
        {
            ArgumentParser parser = new ArgumentParser();
            parser.CreateArgumentCatagory<BasicOptions>().WithArgument(x => x.Boolean);

            parser.Parse("/boolean");

            Assert.True(parser.GetArgumentCatagory<BasicOptions>().Boolean);
        }

        [Fact]
        public void NameParserCanHandleDoubleDashPrefix()
        {
            ArgumentParser parser = new ArgumentParser();
            parser.CreateArgumentCatagory<BasicOptions>().WithArgument(x => x.Boolean);

            parser.Parse("--boolean");

            Assert.True(parser.GetArgumentCatagory<BasicOptions>().Boolean);
        }

        [Theory]
        [InlineData("--string:value", "value")]
        [InlineData("/string:value", "value")]
        public void NameParserCanHandleWindowsDelimiter(string argumentNameWithValue, string value)
        {
            ArgumentParser parser = new ArgumentParser();
            parser.CreateArgumentCatagory<BasicOptions>().WithArgument(x => x.String);

            parser.Parse(argumentNameWithValue);

            Assert.Equal(value, parser.GetArgumentCatagory<BasicOptions>().String);
        }

        [Theory]
        [InlineData("--string=value", "value")]
        [InlineData("/string=value", "value")]
        public void NameParserCanHandleEqualsDelimiter(string argumentNameWithValue, string value)
        {
            ArgumentParser parser = new ArgumentParser();
            parser.CreateArgumentCatagory<BasicOptions>().WithArgument(x => x.String);

            parser.Parse(argumentNameWithValue);

            Assert.Equal(value, parser.GetArgumentCatagory<BasicOptions>().String);
        }

        [Theory]
        [InlineData("--boolean")]
        [InlineData("--Boolean")]
        [InlineData("--BOOLEAN")]
        [InlineData("--boolEAN")]
        [InlineData("/boolean")]
        [InlineData("/Boolean")]
        [InlineData("/BOOLEAN")]
        [InlineData("/boolEAN")]
        public void NameParserShouldBeCaseInsensitive(string argumentName)
        {
            ArgumentParser parser = new ArgumentParser();
            parser.CreateArgumentCatagory<BasicOptions>().WithArgument(x => x.Boolean);

            parser.Parse(argumentName);

            Assert.True(parser.GetArgumentCatagory<BasicOptions>().Boolean);
        }
    }
}