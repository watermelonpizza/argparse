using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace argparse.UnitTests.ArgumentParserTests
{
    public class ArgumentParserClassTests
    {
        [Fact]
        public void ArgumentParserSupportsType_bool()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(bool)));
        }

        [Fact]
        public void ArgumentParserSupportsType_byte()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(byte)));
        }

        [Fact]
        public void ArgumentParserSupportsType_sbyte()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(sbyte)));
        }

        [Fact]
        public void ArgumentParserSupportsType_short()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(short)));
        }

        [Fact]
        public void ArgumentParserSupportsType_int()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(int)));
        }

        [Fact]
        public void ArgumentParserSupportsType_uint()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(uint)));
        }

        [Fact]
        public void ArgumentParserSupportsType_long()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(long)));
        }

        [Fact]
        public void ArgumentParserSupportsType_ulong()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(ulong)));
        }

        [Fact]
        public void ArgumentParserSupportsType_float()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(float)));
        }

        [Fact]
        public void ArgumentParserSupportsType_double()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(double)));
        }

        [Fact]
        public void ArgumentParserSupportsType_decimal()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(decimal)));
        }

        [Fact]
        public void ArgumentParserSupportsType_char()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(char)));
        }

        [Fact]
        public void ArgumentParserSupportsType_string()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(string)));
        }

        [Fact]
        public void ArgumentParserSupportsType_DateTime()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(DateTime)));
        }

        [Fact]
        public void ArgumentParserSupportsType_Enum()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(Enum)));
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_byte()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<byte>)));
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_sbyte()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<sbyte>)));
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_short()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<short>)));
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_int()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<int>)));
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_uint()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<uint>)));
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_long()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<long>)));
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_ulong()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<ulong>)));
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_float()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<float>)));
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_double()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<double>)));
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_decimal()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<decimal>)));
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_char()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<char>)));
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_string()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<string>)));
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_DateTime()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<DateTime>)));
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_Enum()
        {
            Assert.True(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<Enum>)));
        }

        [Fact]
        public void ArgumentParserArgumentCreateCatagoryNotNull()
        {
            Assert.NotNull(ArgumentParser.Create("app").CreateArgumentCatagory<NoOptions>());
        }

        [Fact]
        public void ArgumentParserCreateArgumentCatagoryIsOfTypeIArgumentCatagory()
        {
            Assert.IsAssignableFrom<IArgumentCatagory>(ArgumentParser.Create("app").CreateArgumentCatagory<NoOptions>());
        }

        [Fact]
        public void ArgumentParserCreateArgumentCatagoryIsOfTypeIArgumentCatagory_T_()
        {
            Assert.IsAssignableFrom<IArgumentCatagory<NoOptions>>(ArgumentParser.Create("app").CreateArgumentCatagory<NoOptions>());
        }

        [Fact]
        public void ArgumentParserGetArgumentCatagoryNotNull()
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser.CreateArgumentCatagory<NoOptions>();
            Assert.NotNull(parser.GetArgumentCatagory<NoOptions>());
        }

        [Fact]
        public void ArgumentParserGetArgumentCatagoryIsSameTypeAs_TOptions()
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser.CreateArgumentCatagory<NoOptions>();
            Assert.IsType<NoOptions>(parser.GetArgumentCatagory<NoOptions>());
        }

        [Fact]
        public void ArgumentParserCreateCommandCatagoryNotNull()
        {
            Assert.NotNull(ArgumentParser.Create("app").CreateCommandCatagory<NoOptions>());
        }

        [Fact]
        public void ArgumentParserCreateCommandCatagoryIsOfTypeICommandCatagory()
        {
            Assert.IsAssignableFrom<ICommandCatagory>(ArgumentParser.Create("app").CreateCommandCatagory<NoOptions>());
        }

        [Fact]
        public void ArgumentParserCreateCommandCatagoryIsOfTypeICommandCatagory_T_()
        {
            Assert.IsAssignableFrom<ICommandCatagory<NoOptions>>(ArgumentParser.Create("app").CreateCommandCatagory<NoOptions>());
        }

        [Fact]
        public void ArgumentParserGetCommandCatagoryNotNull()
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser.CreateCommandCatagory<NoOptions>();
            Assert.NotNull(parser.GetCommandCatagory<NoOptions>());
        }

        [Fact]
        public void ArgumentParserGetCommandCatagoryIsSameTypeAs_TOptions()
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser.CreateCommandCatagory<NoOptions>();
            Assert.IsType<NoOptions>(parser.GetCommandCatagory<NoOptions>());
        }

        [Fact]
        public void ArgumentParserCreateParameterCatagoryNotNull()
        {
            Assert.NotNull(ArgumentParser.Create("app").CreateParameterCatagory<NoOptions>());
        }

        [Fact]
        public void ArgumentParserCreateParameterCatagoryIsOfTypeIParameterCatagory()
        {
            Assert.IsAssignableFrom<IParameterCatagory>(ArgumentParser.Create("app").CreateParameterCatagory<NoOptions>());
        }

        [Fact]
        public void ArgumentParserCreateParameterCatagoryIsOfTypeIParameterCatagory_T_()
        {
            Assert.IsAssignableFrom<IParameterCatagory<NoOptions>>(ArgumentParser.Create("app").CreateParameterCatagory<NoOptions>());
        }

        [Fact]
        public void ArgumentParserGetParameterCatagoryNotNull()
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser.CreateParameterCatagory<NoOptions>();
            Assert.NotNull(parser.GetParameterCatagory<NoOptions>());
        }

        [Fact]
        public void ArgumentParserGetParameterCatagoryIsSameTypeAs_TOptions()
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser.CreateParameterCatagory<NoOptions>();
            Assert.IsType<NoOptions>(parser.GetParameterCatagory<NoOptions>());
        }
    }
}