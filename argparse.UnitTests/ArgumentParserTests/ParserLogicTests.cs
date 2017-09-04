using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace argparse.UnitTests.ArgumentParserTests
{
    public class ParserLogicTests
    {
        [Fact]
        public void ParserCanHandleNames()
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                .WithArgument(x => x.Boolean);

            parser.Parse("--boolean");

            Assert.True(parser.GetArgumentCatagory<BasicOptions>().Boolean);
        }

        [Fact]
        public void ParserCanHandleFlags()
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                .WithArgument(x => x.Boolean)
                    .Flag('b');

            parser.Parse("-b");

            Assert.True(parser.GetArgumentCatagory<BasicOptions>().Boolean);
        }

        [Theory]
        [InlineData("/boolean")]
        [InlineData("/b")]
        public void ParserCanHandleWindowsPrefix(string argumentWithWindowsPrefix)
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                .WithArgument(x => x.Boolean)
                    .Flag('b');

            parser.Parse(argumentWithWindowsPrefix);

            Assert.True(parser.GetArgumentCatagory<BasicOptions>().Boolean);
        }

        [Theory]
        [InlineData("--boolean")]
        [InlineData("-b")]
        public void ParserCanHandleStandardPrefix(string argumentWithStandardPrefix)
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                .WithArgument(x => x.Boolean)
                    .Flag('b');

            parser.Parse(argumentWithStandardPrefix);

            Assert.True(parser.GetArgumentCatagory<BasicOptions>().Boolean);
        }

        [Theory]
        [InlineData("--string:value", "value")]
        [InlineData("-s:value", "value")]
        [InlineData("/string:value", "value")]
        [InlineData("/s:value", "value")]
        public void ParserCanHandleWindowsDelimiter(string argumentUsingWindowsDelimiterWithValue, string value)
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                .WithArgument(x => x.String)
                    .Flag('s');

            parser.Parse(argumentUsingWindowsDelimiterWithValue);

            Assert.Equal(value, parser.GetArgumentCatagory<BasicOptions>().String);
        }

        [Theory]
        [InlineData("--string=value", "value")]
        [InlineData("-s=value", "value")]
        [InlineData("/string=value", "value")]
        [InlineData("/s=value", "value")]
        public void ParserCanHandleEqualsDelimiter(string argumentUsingEqualsDelimiterWithValue, string value)
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                .WithArgument(x => x.String)
                    .Flag('s');

            parser.Parse(argumentUsingEqualsDelimiterWithValue);

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
        public void ParserShouldBeCaseInsensitiveOnNames(string argumentName)
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser.CreateArgumentCatagory<BasicOptions>().WithArgument(x => x.Boolean);

            parser.Parse(argumentName);

            Assert.True(parser.GetArgumentCatagory<BasicOptions>().Boolean);
        }

        [Fact]
        public void ParserShouldBeCaseSensitiveOnFlags()
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                .WithArgument(x => x.Boolean)
                    .Flag('b')
                .WithArgument(x => x.Byte)
                    .Flag('B');

            parser.Parse("-B", "10");

            Assert.Equal(10, parser.GetArgumentCatagory<BasicOptions>().Byte);
            Assert.False(parser.GetArgumentCatagory<BasicOptions>().Boolean);
        }

        [Theory]
        [InlineData("--string", "value")]
        [InlineData("-s", "value")]
        [InlineData("/string", "value")]
        [InlineData("/s", "value")]
        public void ParserShouldPickupAValueOnNextArgument(string argument, string value)
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                .WithArgument(x => x.String)
                    .Flag('s');

            parser.Parse(argument, value);

            Assert.Equal(value, parser.GetArgumentCatagory<BasicOptions>().String);
        }

        [Fact]
        public void ParserShouldAddMultipleValuesOnMultiArguments()
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<MultiOptions>()
                .WithMultiArgument(x => x.String)
                    .Flag('s');

            parser.Parse("--string", "value", "--string",  "value2", "-s", "val3"); 

            List<string> values = parser.GetArgumentCatagory<MultiOptions>().String.ToList();

            Assert.Equal(3, values.Count);
            Assert.Equal("value", values[0]);
            Assert.Equal("value2", values[1]);
            Assert.Equal("val3", values[2]);
        }

        [Theory]
        [InlineData(new[] { "" }, 0)]
        [InlineData(new[] { "--integer" }, 1)]
        [InlineData(new[] { "--integer", "--integer" }, 2)]
        [InlineData(new[] { "-i" }, 1)]
        [InlineData(new[] { "-i", "-i" }, 2)]
        [InlineData(new[] { "-ii" }, 2)]
        public void ParserShouldCountArgumentsOnArgumentsSetToBeCountable(string[] argument, int expectedCount)
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                .WithArgument(x => x.Integer)
                    .Flag('i')
                    .Countable();

            parser.Parse(argument);

            Assert.Equal(expectedCount, parser.GetArgumentCatagory<BasicOptions>().Integer);
        }

        #region Conversion to supported type tests

        [Theory]
        [InlineData("--boolean")]
        [InlineData("-b")]
        public void ParserCanParseBooleanType(string argument)
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                .WithArgument(x => x.Boolean)
                    .Flag('b');

            parser.Parse(argument);

            Assert.True(parser.GetArgumentCatagory<BasicOptions>().Boolean);
        }

        [Theory]
        [InlineData("--character", "a", 'a')]
        [InlineData("--character", "1", '1')]
        [InlineData("--character", "&", '&')]
        [InlineData("--character", ";", ';')]
        [InlineData("--character", "~", '~')]
        [InlineData("--character", "\u0058", '\u0058')]
        [InlineData("-c", "a", 'a')]
        [InlineData("-c", "1", '1')]
        [InlineData("-c", "&", '&')]
        [InlineData("-c", ";", ';')]
        [InlineData("-c", "~", '~')]
        [InlineData("-c", "\u0058", '\u0058')]
        public void ParserCanParseCharacterType(string argument, string character, char expectedValue)
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                .WithArgument(x => x.Character)
                    .Flag('c');

            parser.Parse(argument, character);

            Assert.Equal(expectedValue, parser.GetArgumentCatagory<BasicOptions>().Character);
        }

        [Theory]
        [InlineData("--string", "value")]
        [InlineData("--string", "value with spaces")]
        [InlineData("--string", "🍉emoji")]
        [InlineData("-s", "value")]
        [InlineData("-s", "value with spaces")]
        [InlineData("-s", "🍉emoji")]
        public void ParserCanParseStringType(string argument, string value)
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                .WithArgument(x => x.String)
                    .Flag('s');

            parser.Parse(argument, value);

            Assert.Equal(value, parser.GetArgumentCatagory<BasicOptions>().String);
        }

        // TODO: Write tests for enum support, ensure enum set, multi enums set, flags set to all values
        //[Theory]
        //[InlineData("--enum", SciFiShows.Battlestar)]
        //[InlineData("-e", SciFiShows.Battlestar)]
        //public void ParserCanParseEnumType(string argument, SciFiShows enumValue)
        //{
        //    ArgumentParser parser = ArgumentParser.Create("app");
        //    parser
        //        .CreateArgumentCatagory<BasicOptions>()
        //        .WithArgument(x => x.Enum)
        //            .Flag('e');

        //    parser.Parse(argument, Enum.GetName(typeof(SciFiShows), enumValue));

        //    Assert.Equal(enumValue, parser.GetArgumentCatagory<BasicOptions>().Enum);
        //}

        [Theory]
        [InlineData("--date-time", "9/10/2014", 2014, 10, 9)]
        [InlineData("--date-time", "10 august 2015", 2015, 8, 10)]
        [InlineData("--date-time", "1-10-2014 9pm", 2014, 10, 1, 21)]
        [InlineData("--date-time", "2017-01-01 10:3:20PM", 2017, 1, 1, 22, 3, 20)]
        [InlineData("-d", "9/10/2014", 2014, 10, 9)]
        [InlineData("-d", "10 august 2015", 2015, 8, 10)]
        [InlineData("-d", "1-10-2014 9pm", 2014, 10, 1, 21)]
        [InlineData("-d", "2017-01-01 10:3:20PM", 2017, 1, 1, 22, 3, 20)]
        public void ParserCanParseDateTimeStringType(string argument, string value, int year, int month, int day, int hour = 0, int minute = 0, int second = 0)
        {
            DateTime expectedDateTime = new DateTime(year, month, day, hour, minute, second);

            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                .WithArgument(x => x.DateTime)
                    .Flag('d');

            parser.Parse(argument, value);

            Assert.Equal(expectedDateTime, parser.GetArgumentCatagory<BasicOptions>().DateTime);
        }

        [Theory]
        [InlineData("--date-time", "0", 0)]
        [InlineData("--date-time", "618199776000000000", 618199776000000000)]
        [InlineData("--date-time", "3155378975999999999", 3155378975999999999)]
        [InlineData("-d", "0", 0)]
        [InlineData("-d", "618199776000000000", 618199776000000000)]
        [InlineData("-d", "3155378975999999999", 3155378975999999999)]
        public void ParserCanParseDateTimeTicksType(string argument, string value, long ticks)
        {
            DateTime expectedDateTime = new DateTime(ticks);

            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                .WithArgument(x => x.DateTime)
                    .Flag('d');

            parser.Parse(argument, value);

            Assert.Equal(expectedDateTime, parser.GetArgumentCatagory<BasicOptions>().DateTime);
        }

        [Theory]
        [InlineData("--byte", "0", byte.MinValue)]
        [InlineData("--byte", "2", 2)]
        [InlineData("--byte", "255", byte.MaxValue)]
        [InlineData("-b", "0", byte.MinValue)]
        [InlineData("-b", "2", 2)]
        [InlineData("-b", "255", byte.MaxValue)]
        public void ParserCanParseByte(string argument, string value, byte expectedValue)
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                .WithArgument(x => x.Byte)
                    .Flag('b');

            parser.Parse(argument, value);

            Assert.Equal(expectedValue, parser.GetArgumentCatagory<BasicOptions>().Byte);
        }

        [Theory]
        [InlineData("--signed-byte", "-128", sbyte.MinValue)]
        [InlineData("--signed-byte", "0", 0)]
        [InlineData("--signed-byte", "2", 2)]
        [InlineData("--signed-byte", "127", sbyte.MaxValue)]
        [InlineData("-s", "-128", sbyte.MinValue)]
        [InlineData("-s", "0", 0)]
        [InlineData("-s", "2", 2)]
        [InlineData("-s", "127", sbyte.MaxValue)]
        public void ParserCanParseSByte(string argument, string value, sbyte expectedValue)
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                .WithArgument(x => x.SignedByte)
                    .Flag('s');

            parser.Parse(argument, value);

            Assert.Equal(expectedValue, parser.GetArgumentCatagory<BasicOptions>().SignedByte);
        }

        [Theory]
        [InlineData("--short-value", "-32768", short.MinValue)]
        [InlineData("--short-value", "0", 0)]
        [InlineData("--short-value", "2", 2)]
        [InlineData("--short-value", "32767", short.MaxValue)]
        [InlineData("-s", "-32768", short.MinValue)]
        [InlineData("-s", "0", 0)]
        [InlineData("-s", "2", 2)]
        [InlineData("-s", "32767", short.MaxValue)]
        public void ParserCanParseShort(string argument, string value, short expectedValue)
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                .WithArgument(x => x.ShortValue)
                    .Flag('s');

            parser.Parse(argument, value);

            Assert.Equal(expectedValue, parser.GetArgumentCatagory<BasicOptions>().ShortValue);
        }

        [Theory]
        [InlineData("--integer", "-2147483648", int.MinValue)]
        [InlineData("--integer", "0", 0)]
        [InlineData("--integer", "2", 2)]
        [InlineData("--integer", "2147483647", int.MaxValue)]
        [InlineData("-i", "-2147483648", int.MinValue)]
        [InlineData("-i", "0", 0)]
        [InlineData("-i", "2", 2)]
        [InlineData("-i", "2147483647", int.MaxValue)]
        public void ParserCanParseInt(string argument, string value, int expectedValue)
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                .WithArgument(x => x.Integer)
                    .Flag('i');

            parser.Parse(argument, value);

            Assert.Equal(expectedValue, parser.GetArgumentCatagory<BasicOptions>().Integer);
        }

        [Theory]
        [InlineData("--unsigned-integer", "0", uint.MinValue)]
        [InlineData("--unsigned-integer", "2", 2)]
        [InlineData("--unsigned-integer", "4294967295", uint.MaxValue)]
        [InlineData("-u", "0", uint.MinValue)]
        [InlineData("-u", "2", 2)]
        [InlineData("-u", "4294967295", uint.MaxValue)]
        public void ParserCanParseUInt(string argument, string value, uint expectedValue)
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                .WithArgument(x => x.UnsignedInteger)
                    .Flag('u');

            parser.Parse(argument, value);

            Assert.Equal(expectedValue, parser.GetArgumentCatagory<BasicOptions>().UnsignedInteger);
        }

        [Theory]
        [InlineData("--long", "-9223372036854775808", long.MinValue)]
        [InlineData("--long", "0", 0)]
        [InlineData("--long", "2", 2)]
        [InlineData("--long", "9223372036854775807", long.MaxValue)]
        [InlineData("-l", "-9223372036854775808", long.MinValue)]
        [InlineData("-l", "0", 0)]
        [InlineData("-l", "2", 2)]
        [InlineData("-l", "9223372036854775807", long.MaxValue)]
        public void ParserCanParseLong(string argument, string value, long expectedValue)
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                .WithArgument(x => x.Long)
                    .Flag('l');

            parser.Parse(argument, value);

            Assert.Equal(expectedValue, parser.GetArgumentCatagory<BasicOptions>().Long);
        }

        [Theory]
        [InlineData("--unsigned-long", "0", ulong.MinValue)]
        [InlineData("--unsigned-long", "2", 2)]
        [InlineData("--unsigned-long", "18446744073709551615", ulong.MaxValue)]
        [InlineData("-u", "0", ulong.MinValue)]
        [InlineData("-u", "2", 2)]
        [InlineData("-u", "18446744073709551615", ulong.MaxValue)]
        public void ParserCanParseULong(string argument, string value, ulong expectedValue)
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                .WithArgument(x => x.UnsignedLong)
                    .Flag('u');

            parser.Parse(argument, value);

            Assert.Equal(expectedValue, parser.GetArgumentCatagory<BasicOptions>().UnsignedLong);
        }

        [Theory]
        [InlineData("--float", "-123.4442", -123.4442F)]
        [InlineData("--float", "0", 0)]
        [InlineData("--float", "2.1", 2.1)]
        [InlineData("--float", "10E+4", 10E+4F)]
        [InlineData("-f", "-123.4442", -123.4442F)]
        [InlineData("-f", "0", 0)]
        [InlineData("-f", "2.1", 2.1)]
        [InlineData("-f", "10E+4", 10E+4F)]
        public void ParserCanParseFloat(string argument, string value, float expectedValue)
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                .WithArgument(x => x.Float)
                    .Flag('f');

            parser.Parse(argument, value);

            Assert.Equal(expectedValue, parser.GetArgumentCatagory<BasicOptions>().Float);
        }

        [Theory]
        [InlineData("--double", "-1.7976931348623157E+308", double.MinValue)]
        [InlineData("--double", "0", 0)]
        [InlineData("--double", "0.00034", 0.00034d)]
        [InlineData("--double", "2", 2)]
        [InlineData("--double", "1.7976931348623157E+308", double.MaxValue)]
        [InlineData("-d", "-1.7976931348623157E+308", double.MinValue)]
        [InlineData("-d", "0", 0)]
        [InlineData("-d", "0.00034", 0.00034d)]
        [InlineData("-d", "2", 2)]
        [InlineData("-d", "1.7976931348623157E+308", double.MaxValue)]
        public void ParserCanParseDouble(string argument, string value, double expectedValue)
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                .WithArgument(x => x.Double)
                    .Flag('d');

            parser.Parse(argument, value);

            Assert.Equal(expectedValue, parser.GetArgumentCatagory<BasicOptions>().Double);
        }
        
        [Fact] // Has to be fact because decimal isn't CLR :(
        public void ParserCanParseDecimal()
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                .WithArgument(x => x.Decimal)
                    .Flag('d');

            parser.Parse("--decimal", "-79228162514264337593543950335");
            Assert.Equal(decimal.MinValue, parser.GetArgumentCatagory<BasicOptions>().Decimal);

            parser.Parse("--decimal", "-1");
            Assert.Equal(decimal.MinusOne, parser.GetArgumentCatagory<BasicOptions>().Decimal);

            parser.Parse("--decimal", "0");
            Assert.Equal(decimal.Zero, parser.GetArgumentCatagory<BasicOptions>().Decimal);

            parser.Parse("--decimal", "1");
            Assert.Equal(decimal.One, parser.GetArgumentCatagory<BasicOptions>().Decimal);

            parser.Parse("--decimal", "4112.4242");
            Assert.Equal((decimal)4112.4242, parser.GetArgumentCatagory<BasicOptions>().Decimal);

            parser.Parse("--decimal", "79228162514264337593543950335");
            Assert.Equal(decimal.MaxValue, parser.GetArgumentCatagory<BasicOptions>().Decimal);

            parser.Parse("-d", "-79228162514264337593543950335");
            Assert.Equal(decimal.MinValue, parser.GetArgumentCatagory<BasicOptions>().Decimal);

            parser.Parse("-d", "-1");
            Assert.Equal(decimal.MinusOne, parser.GetArgumentCatagory<BasicOptions>().Decimal);

            parser.Parse("-d", "0");
            Assert.Equal(decimal.Zero, parser.GetArgumentCatagory<BasicOptions>().Decimal);

            parser.Parse("-d", "1");
            Assert.Equal(decimal.One, parser.GetArgumentCatagory<BasicOptions>().Decimal);

            parser.Parse("-d", "4112.4242");
            Assert.Equal((decimal)4112.4242, parser.GetArgumentCatagory<BasicOptions>().Decimal);

            parser.Parse("-d", "79228162514264337593543950335");
            Assert.Equal(decimal.MaxValue, parser.GetArgumentCatagory<BasicOptions>().Decimal);
        }


        #endregion

        [Fact]
        public void ParserShouldSetCommandToTrueIfCommandIsFound()
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser.CreateCommandCatagory<Commands>()
                .WithCommand(x => x.FlagCommand);

            parser.Parse("flagcommand");

            Assert.True(parser.GetCommandCatagory<Commands>().FlagCommand);
        }

        [Fact]
        public void ParserShouldSetArgumentOnCommandIfCommandIsFound()
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser.CreateCommandCatagory<Commands>()
                .WithCommand(x => x.CommandWithArguments, (cp) => cp.CreateArgumentCatagory<BasicOptions>().WithArgument(x => x.Boolean));

            parser.Parse("commandwitharguments", "--boolean");

            Assert.True(parser.GetCommandCatagory<Commands>().CommandWithArguments.Selected);
            Assert.True(parser.GetCommandCatagory<Commands>().CommandWithArguments.GetArgumentCatagory<BasicOptions>().Boolean);
        }
    }
}
