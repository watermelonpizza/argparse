using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace argparse.UnitTests
{
    [TestClass]
    public class ArgumentTests
    {
        [TestMethod]
        public void ArgumentDefaultName_PascalCase()
        {
            IArgument argument = ArgumentParser.Default.CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty);
            Assert.AreEqual("pascal-case-proprty", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentDefaultName_LowerCase()
        {
            IArgument argument = ArgumentParser.Default.CreateArgumentCatagory<NameOptions>().WithArgument(x => x.lowercaseproperty);
            Assert.AreEqual("lowercaseproperty", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentDefaultName_UpperCase()
        {
            IArgument argument = ArgumentParser.Default.CreateArgumentCatagory<NameOptions>().WithArgument(x => x.UPPERCASEPROPERTY);
            Assert.AreEqual("uppercaseproperty", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentDefaultName_CamelCase()
        {
            IArgument argument = ArgumentParser.Default.CreateArgumentCatagory<NameOptions>().WithArgument(x => x.camelCaseProperty);
            Assert.AreEqual("camel-case-property", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentDefaultName_TLA()
        {
            IArgument argument = ArgumentParser.Default.CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PropertyWithTLA);
            Assert.AreEqual("property-with-tla", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentDefaultName_OnlyTLA()
        {
            IArgument argument = ArgumentParser.Default.CreateArgumentCatagory<NameOptions>().WithArgument(x => x.TLA);
            Assert.AreEqual("tla", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentDefaultName_Underscores()
        {
            IArgument argument = ArgumentParser.Default.CreateArgumentCatagory<NameOptions>().WithArgument(x => x.TLA);
            Assert.AreEqual("tla", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentDefaultName_LowerCaseUnderscores()
        {
            IArgument argument = ArgumentParser.Default.CreateArgumentCatagory<NameOptions>().WithArgument(x => x.property_in_lowercase_with_underscores);
            Assert.AreEqual("property-in-lowercase-with-underscores", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentDefaultName_UppercaseUnderscores()
        {
            IArgument argument = ArgumentParser.Default.CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PROPERTY_IN_UPPERCASE_WITH_UNDERSCORES);
            Assert.AreEqual("property-in-uppercase-with-underscores", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentDefaultName_Number()
        {
            IArgument argument = ArgumentParser.Default.CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PropertyWithNumber1);
            Assert.AreEqual("property-with-number-1", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentNameMethodSetsName()
        {
            IArgument<NameOptions, int> argument = 
                ArgumentParser.Default.CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty).Name("custom-name");

            Assert.AreEqual("custom-name", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentNameMethodSingleCharacterShouldThrowArgumentException()
        {
            IArgument<NameOptions, int> argument =
                ArgumentParser.Default.CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty);

            Assert.ThrowsException<ArgumentException>(() => argument.Name("a"));
        }

        [TestMethod]
        public void ArgumentNameMethodUpperCaseValuesShouldThrowArgumentException()
        {
            IArgument<NameOptions, int> argument = 
                ArgumentParser.Default.CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty);

            Assert.ThrowsException<ArgumentException>(() => argument.Name("UP"));
        }

        [TestMethod]
        public void ArgumentNameMethodSymbolsShouldThrowArgumentException()
        {
            IArgument<NameOptions, int> argument =
                ArgumentParser.Default.CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty);

            Assert.ThrowsException<ArgumentException>(() => argument.Name("a!"));
            Assert.ThrowsException<ArgumentException>(() => argument.Name("a#"));
            Assert.ThrowsException<ArgumentException>(() => argument.Name("a)"));
            Assert.ThrowsException<ArgumentException>(() => argument.Name("a%"));
            Assert.ThrowsException<ArgumentException>(() => argument.Name("a'"));
            Assert.ThrowsException<ArgumentException>(() => argument.Name("a>"));
            Assert.ThrowsException<ArgumentException>(() => argument.Name("a🍉"));
        }

        [TestMethod]
        public void ArgumentNameMethodOnlyNumbersShouldThrowArgumentException()
        {
            IArgument<NameOptions, int> argument =
                ArgumentParser.Default.CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty);

            Assert.ThrowsException<ArgumentException>(() => argument.Name("1234"));
        }

        [TestMethod]
        public void ArgumentNameMethodStartsWithNumberShouldThrowArgumentException()
        {
            IArgument<NameOptions, int> argument =
                ArgumentParser.Default.CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty);

            Assert.ThrowsException<ArgumentException>(() => argument.Name("1abc"));
        }

        [TestMethod]
        public void ArgumentNameMethodStartsWithHyphenShouldThrowArgumentException()
        {
            IArgument<NameOptions, int> argument =
                ArgumentParser.Default.CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty);

            Assert.ThrowsException<ArgumentException>(() => argument.Name("-abc"));
        }

        [TestMethod]
        public void ArgumentNameMethodSpaceShouldThrowArgumentException()
        {
            IArgument<NameOptions, int> argument =
                ArgumentParser.Default.CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty);

            Assert.ThrowsException<ArgumentException>(() => argument.Name("aaa bbb"));
        }
    }
}
