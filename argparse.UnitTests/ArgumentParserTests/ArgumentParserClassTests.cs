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
            Assert.Contains(typeof(bool), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_byte()
        {
            Assert.Contains(typeof(byte), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_sbyte()
        {
            Assert.Contains(typeof(sbyte), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_short()
        {
            Assert.Contains(typeof(short), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_int()
        {
            Assert.Contains(typeof(int), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_uint()
        {
            Assert.Contains(typeof(uint), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_long()
        {
            Assert.Contains(typeof(long), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_ulong()
        {
            Assert.Contains(typeof(ulong), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_float()
        {
            Assert.Contains(typeof(float), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_double()
        {
            Assert.Contains(typeof(double), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_decimal()
        {
            Assert.Contains(typeof(decimal), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_char()
        {
            Assert.Contains(typeof(char), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_string()
        {
            Assert.Contains(typeof(string), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_DateTime()
        {
            Assert.Contains(typeof(DateTime), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_Enum()
        {
            Assert.Contains(typeof(Enum), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_byte()
        {
            Assert.Contains(typeof(IEnumerable<byte>), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_sbyte()
        {
            Assert.Contains(typeof(IEnumerable<sbyte>), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_short()
        {
            Assert.Contains(typeof(IEnumerable<short>), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_int()
        {
            Assert.Contains(typeof(IEnumerable<int>), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_uint()
        {
            Assert.Contains(typeof(IEnumerable<uint>), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_long()
        {
            Assert.Contains(typeof(IEnumerable<long>), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_ulong()
        {
            Assert.Contains(typeof(IEnumerable<ulong>), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_float()
        {
            Assert.Contains(typeof(IEnumerable<float>), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_double()
        {
            Assert.Contains(typeof(IEnumerable<double>), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_decimal()
        {
            Assert.Contains(typeof(IEnumerable<decimal>), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_char()
        {
            Assert.Contains(typeof(IEnumerable<char>), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_string()
        {
            Assert.Contains(typeof(IEnumerable<string>), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_DateTime()
        {
            Assert.Contains(typeof(IEnumerable<DateTime>), ArgumentParser.SupportedTypes);
        }

        [Fact]
        public void ArgumentParserSupportsType_IEnumerable_Enum()
        {
            Assert.Contains(typeof(IEnumerable<Enum>), ArgumentParser.SupportedTypes);
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