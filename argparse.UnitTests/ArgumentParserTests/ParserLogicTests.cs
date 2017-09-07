using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        // User for this case types in: (c# removes the "" around value 2)
        // app.exe --string=value1,"value 2" 
        [InlineData("--string=value1,value 2", "value1", "value 2")]
        [InlineData("--string=value1,value 2,value3", "value1", "value 2", "value3")]
        [InlineData("--string=value1,value2,value3", "value1", "value2", "value3")]
        [InlineData("-s=value1,value 2", "value1", "value 2")]
        [InlineData("-s=value1,value 2,value3", "value1", "value 2", "value3")]
        [InlineData("-s=value1,value2,value3", "value1", "value2", "value3")]
        [InlineData("/string=value1,value 2", "value1", "value 2")]
        [InlineData("/string=value1,value 2,value3", "value1", "value 2", "value3")]
        [InlineData("/string=value1,value2,value3", "value1", "value2", "value3")]
        [InlineData("/s=value1,value 2", "value1", "value 2")]
        [InlineData("/s=value1,value 2,value3", "value1", "value 2", "value3")]
        [InlineData("/s=value1,value2,value3", "value1", "value2", "value3")]
        public void ParserCanHandleCommaSeperatedValuesOnDeliminatedArgument(string argumentWithCommaSeperatedValues, params string[] values)
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<MultiOptions>()
                    .WithMultiArgument(x => x.String)
                        .Flag('s');

            parser.Parse(argumentWithCommaSeperatedValues);

            ImmutableArray<string> parsedValues = parser.GetArgumentCatagory<MultiOptions>().String;
            
            Assert.Equal(values.Length, parsedValues.Length);

            for (int i = 0; i < values.Length; i++)
            {
                Assert.Equal(values[i], parsedValues[i]);
            }
        }

        [Theory]
        // User for this case types in: (c# removes the "" around value 2)
        // app.exe --string value1,"value 2" 
        [InlineData(new[] { "--string", "value1,value2", "--string", "value3" }, "value1,value2", "value3")]
        [InlineData(new[] { "--string", "value1,value 2", "--string", "value3" }, "value1,value 2", "value3")]
        [InlineData(new[] { "--string", "value1,value 2,value3", "--string", "value4" }, "value1,value 2,value3", "value4")]
        [InlineData(new[] { "-s", "value1,value2", "-s", "value3" }, "value1,value2", "value3")]
        [InlineData(new[] { "-s", "value1,value 2", "-s", "value3" }, "value1,value 2", "value3")]
        [InlineData(new[] { "-s", "value1,value 2,value3", "-s", "value4" }, "value1,value 2,value3", "value4")]
        [InlineData(new[] { "/string", "value1,value2", "/string", "value3" }, "value1,value2", "value3")]
        [InlineData(new[] { "/string", "value1,value 2", "/string", "value3" }, "value1,value 2", "value3")]
        [InlineData(new[] { "/string", "value1,value 2,value3", "/string", "value4" }, "value1,value 2,value3", "value4")]
        [InlineData(new[] { "/s", "value1,value2", "/s", "value3" }, "value1,value2", "value3")]
        [InlineData(new[] { "/s", "value1,value 2", "/s", "value3" }, "value1,value 2", "value3")]
        [InlineData(new[] { "/s", "value1,value 2,value3", "/s", "value4" }, "value1,value 2,value3", "value4")]
        public void ParserTreatsArgumentsSeperateFromNameWithCommasAsSingleValue(string[] argumentWithCommaSeperatedValues, params string[] values)
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<MultiOptions>()
                    .WithMultiArgument(x => x.String)
                        .Flag('s');

            parser.Parse(argumentWithCommaSeperatedValues);

            ImmutableArray<string> parsedValues = parser.GetArgumentCatagory<MultiOptions>().String;

            Assert.Equal(values.Length, parsedValues.Length);

            for (int i = 0; i < values.Length; i++)
            {
                Assert.Equal(values[i], parsedValues[i]);
            }
        }

        [Theory]
        [InlineData(new[] { "--flaggable-enum", "mars" }, FlaggableEnum.Mars)]
        [InlineData(new[] { "--flaggable-enum", "earth,mars,jupiter" }, FlaggableEnum.Earth | FlaggableEnum.Mars | FlaggableEnum.Jupiter)]
        [InlineData(new[] { "--flaggable-enum=earth,mars,jupiter" }, FlaggableEnum.Earth | FlaggableEnum.Mars | FlaggableEnum.Jupiter)]
        [InlineData(new[] { "-f", "earth,mars,jupiter" }, FlaggableEnum.Earth | FlaggableEnum.Mars | FlaggableEnum.Jupiter)]
        [InlineData(new[] { "-f=earth,mars,jupiter" }, FlaggableEnum.Earth | FlaggableEnum.Mars | FlaggableEnum.Jupiter)]
        [InlineData(new[] { "/flaggable-enum", "earth,mars,jupiter" }, FlaggableEnum.Earth | FlaggableEnum.Mars | FlaggableEnum.Jupiter)]
        [InlineData(new[] { "/flaggable-enum=earth,mars,jupiter" }, FlaggableEnum.Earth | FlaggableEnum.Mars | FlaggableEnum.Jupiter)]
        [InlineData(new[] { "/f", "earth,mars,jupiter" }, FlaggableEnum.Earth | FlaggableEnum.Mars | FlaggableEnum.Jupiter)]
        [InlineData(new[] { "/f=earth,mars,jupiter" }, FlaggableEnum.Earth | FlaggableEnum.Mars | FlaggableEnum.Jupiter)]
        public void ParserCanHandleCommaSeperatedValuesOnFlaggableEnums(string[] argumentWithCommaSeperatedValues, FlaggableEnum value)
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                    .WithArgument(x => x.FlaggableEnum)
                        .Flag('f');

            parser.Parse(argumentWithCommaSeperatedValues);
            
            Assert.Equal(value, parser.GetArgumentCatagory<BasicOptions>().FlaggableEnum);
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

            ImmutableArray<string> values = parser.GetArgumentCatagory<MultiOptions>().String;

            Assert.Equal(3, values.Length);
            Assert.Equal("value", values[0]);
            Assert.Equal("value2", values[1]);
            Assert.Equal("val3", values[2]);
        }

        [Fact]
        public void ParserShouldSetParameterValue()
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateParameterCatagory<BasicOptions>()
                    .WithParameter(x => x.String);

            parser.Parse("value");

            Assert.Equal("value", parser.GetParameterCatagory<BasicOptions>().String);
        }

        [Fact]
        public void ParserShouldAddMultipleValuesOnMultiParamters()
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateParameterCatagory<MultiOptions>()
                    .WithMultiParameter(x => x.String);

            parser.Parse("value", "value2", "val3");

            ImmutableArray<string> values = parser.GetParameterCatagory<MultiOptions>().String;

            Assert.Equal(3, values.Length);
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

        [Theory]
        [InlineData("--enum", SciFiShows.Battlestar)]
        [InlineData("-e", SciFiShows.Battlestar)]
        public void ParserCanParseEnumType(string argument, SciFiShows enumValue)
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                    .WithArgument(x => x.Enum)
                        .Flag('e');

            parser.Parse(argument, Enum.GetName(typeof(SciFiShows), enumValue));

            Assert.Equal(enumValue, parser.GetArgumentCatagory<BasicOptions>().Enum);
        }

        [Theory]
        [InlineData("--flaggable-enum", FlaggableEnum.Mercury, FlaggableEnum.Earth)]
        [InlineData("-f", FlaggableEnum.Mercury, FlaggableEnum.Earth)]
        public void ParserCanParseFlaggableEnumType(string argument, FlaggableEnum enumValue, FlaggableEnum secondEnumValue)
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                    .WithArgument(x => x.FlaggableEnum)
                        .Flag('f');

            parser.Parse(argument, Enum.GetName(typeof(FlaggableEnum), enumValue), argument, Enum.GetName(typeof(FlaggableEnum), secondEnumValue));

            Assert.Equal(enumValue | secondEnumValue, parser.GetArgumentCatagory<BasicOptions>().FlaggableEnum);
        }

        [Theory]
        [InlineData("--enum", "Battlestar", SciFiShows.Battlestar)]
        [InlineData("--enum", "battlestar", SciFiShows.Battlestar)]
        [InlineData("--enum", "BATTLESTAR", SciFiShows.Battlestar)]
        public void ParserShouldBeCaseInsensitiveOnEnumNames(string argument, string value, SciFiShows enumValue)
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateArgumentCatagory<BasicOptions>()
                    .WithArgument(x => x.Enum);

            parser.Parse(argument, value);

            Assert.Equal(enumValue, parser.GetArgumentCatagory<BasicOptions>().Enum);
        }

        [Theory]
        [InlineData("--date-time", "9/10/2014")]
        [InlineData("--date-time", "10 august 2015")]
        [InlineData("--date-time", "1-10-2014 9pm")]
        [InlineData("--date-time", "2017-01-01 10:3:20PM")]
        [InlineData("-d", "9/10/2014")]
        [InlineData("-d", "10 august 2015")]
        [InlineData("-d", "1-10-2014 9pm")]
        [InlineData("-d", "2017-01-01 10:3:20PM")]
        public void ParserCanParseDateTimeStringType(string argument, string value)
        {
            DateTime expectedDateTime = DateTime.Parse(value);

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
            parser
                .CreateCommandCatagory<Commands>()
                    .WithCommand(x => x.FlagCommand);

            parser.Parse("flagcommand");

            Assert.True(parser.GetCommandCatagory<Commands>().FlagCommand);
        }

        [Fact]
        public void ParserShouldSetArgumentOnCommandIfCommandIsFound()
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser
                .CreateCommandCatagory<Commands>()
                    .WithCommand(
                        x => x.CommandWithArguments, 
                        (cp) => cp.CreateArgumentCatagory<BasicOptions>().WithArgument(x => x.Boolean));

            parser.Parse("commandwitharguments", "--boolean");

            Assert.True(parser.GetCommandCatagory<Commands>().CommandWithArguments.Selected);
            Assert.True(parser.GetCommandCatagory<Commands>().CommandWithArguments.GetArgumentCatagory<BasicOptions>().Boolean);
        }
    }
}
