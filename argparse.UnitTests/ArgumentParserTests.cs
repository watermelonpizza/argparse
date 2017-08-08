using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace argparse.UnitTests
{
    [TestClass]
    public class ArgumentParserTests
    {
        [TestMethod]
        public void ArgumentParserStaticDefaultInstanceIsInstantiated()
        {
            Assert.IsNotNull(ArgumentParser.Default);
            Assert.IsInstanceOfType(ArgumentParser.Default, typeof(ArgumentParser));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_bool()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(bool)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_byte()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(byte)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_sbyte()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(sbyte)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_short()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(short)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_int()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(int)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_uint()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(uint)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_long()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(long)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_ulong()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(ulong)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_float()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(float)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_double()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(double)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_decimal()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(decimal)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_char()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(char)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_string()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(string)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_DateTime()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(DateTime)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_Enum()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(Enum)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_IEnumerable_bool()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<bool>)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_IEnumerable_byte()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<byte>)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_IEnumerable_sbyte()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<sbyte>)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_IEnumerable_short()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<short>)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_IEnumerable_int()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<int>)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_IEnumerable_uint()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<uint>)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_IEnumerable_long()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<long>)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_IEnumerable_ulong()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<ulong>)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_IEnumerable_float()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<float>)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_IEnumerable_double()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<double>)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_IEnumerable_decimal()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<decimal>)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_IEnumerable_char()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<char>)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_IEnumerable_string()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<string>)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_IEnumerable_DateTime()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<DateTime>)));
        }

        [TestMethod]
        public void ArgumentParserSupportsType_IEnumerable_Enum()
        {
            Assert.IsTrue(ArgumentParser.SupportedTypes.Contains(typeof(IEnumerable<Enum>)));
        }

        [TestMethod]
        public void ArgumentParserArgumentCreateCatagoryIsNotNull()
        {
            Assert.IsNotNull(new ArgumentParser().CreateArgumentCatagory<NoOptions>());
        }

        [TestMethod]
        public void ArgumentParserCreateArgumentCatagoryIsOfTypeIArgumentCatagory()
        {
            Assert.IsInstanceOfType(new ArgumentParser().CreateArgumentCatagory<NoOptions>(), typeof(IArgumentCatagory));
        }

        [TestMethod]
        public void ArgumentParserCreateArgumentCatagoryIsOfTypeIArgumentCatagory_T_()
        {
            Assert.IsInstanceOfType(new ArgumentParser().CreateArgumentCatagory<NoOptions>(), typeof(IArgumentCatagory<NoOptions>));
        }

        [TestMethod]
        public void ArgumentParserGetArgumentCatagoryIsNotNull()
        {
            ArgumentParser parser = new ArgumentParser();
            parser.CreateArgumentCatagory<NoOptions>();
            Assert.IsNotNull(parser.GetArgumentCatagory<NoOptions>());
        }

        [TestMethod]
        public void ArgumentParserGetArgumentCatagoryIsSameTypeAs_TOptions()
        {
            ArgumentParser parser = new ArgumentParser();
            parser.CreateArgumentCatagory<NoOptions>();
            Assert.IsInstanceOfType(parser.GetArgumentCatagory<NoOptions>(), typeof(NoOptions));
        }

        [TestMethod]
        public void ArgumentParserCreateCommandCatagoryIsNotNull()
        {
            Assert.IsNotNull(new ArgumentParser().CreateCommandCatagory<NoOptions>());
        }

        [TestMethod]
        public void ArgumentParserCreateCommandCatagoryIsOfTypeICommandCatagory()
        {
            Assert.IsInstanceOfType(new ArgumentParser().CreateCommandCatagory<NoOptions>(), typeof(ICommandCatagory));
        }

        [TestMethod]
        public void ArgumentParserCreateCommandCatagoryIsOfTypeICommandCatagory_T_()
        {
            Assert.IsInstanceOfType(new ArgumentParser().CreateCommandCatagory<NoOptions>(), typeof(ICommandCatagory<NoOptions>));
        }

        [TestMethod]
        public void ArgumentParserGetCommandCatagoryIsNotNull()
        {
            ArgumentParser parser = new ArgumentParser();
            parser.CreateCommandCatagory<NoOptions>();
            Assert.IsNotNull(parser.GetCommandCatagory<NoOptions>());
        }

        [TestMethod]
        public void ArgumentParserGetCommandCatagoryIsSameTypeAs_TOptions()
        {
            ArgumentParser parser = new ArgumentParser();
            parser.CreateCommandCatagory<NoOptions>();
            Assert.IsInstanceOfType(parser.GetCommandCatagory<NoOptions>(), typeof(NoOptions));
        }

        [TestMethod]
        public void ArgumentParserCreateParameterCatagoryIsNotNull()
        {
            Assert.IsNotNull(new ArgumentParser().CreateParameterCatagory<NoOptions>());
        }

        [TestMethod]
        public void ArgumentParserCreateParameterCatagoryIsOfTypeIParameterCatagory()
        {
            Assert.IsInstanceOfType(new ArgumentParser().CreateParameterCatagory<NoOptions>(), typeof(IParameterCatagory));
        }

        [TestMethod]
        public void ArgumentParserCreateParameterCatagoryIsOfTypeIParameterCatagory_T_()
        {
            Assert.IsInstanceOfType(new ArgumentParser().CreateParameterCatagory<NoOptions>(), typeof(IParameterCatagory<NoOptions>));
        }

        [TestMethod]
        public void ArgumentParserGetParameterCatagoryIsNotNull()
        {
            ArgumentParser parser = new ArgumentParser();
            parser.CreateParameterCatagory<NoOptions>();
            Assert.IsNotNull(parser.GetParameterCatagory<NoOptions>());
        }

        [TestMethod]
        public void ArgumentParserGetParameterCatagoryIsSameTypeAs_TOptions()
        {
            ArgumentParser parser = new ArgumentParser();
            parser.CreateParameterCatagory<NoOptions>();
            Assert.IsInstanceOfType(parser.GetParameterCatagory<NoOptions>(), typeof(NoOptions));
        }
    }
}
