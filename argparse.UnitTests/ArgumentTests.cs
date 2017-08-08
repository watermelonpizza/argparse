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
            IArgument argument = new ArgumentParser().CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty);
            Assert.AreEqual("pascal-case-property", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentDefaultName_LowerCase()
        {
            IArgument argument = new ArgumentParser().CreateArgumentCatagory<NameOptions>().WithArgument(x => x.lowercaseproperty);
            Assert.AreEqual("lowercaseproperty", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentDefaultName_UpperCase()
        {
            IArgument argument = new ArgumentParser().CreateArgumentCatagory<NameOptions>().WithArgument(x => x.UPPERCASEPROPERTY);
            Assert.AreEqual("uppercaseproperty", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentDefaultName_CamelCase()
        {
            IArgument argument = new ArgumentParser().CreateArgumentCatagory<NameOptions>().WithArgument(x => x.camelCaseProperty);
            Assert.AreEqual("camel-case-property", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentDefaultName_EndingTLA()
        {
            IArgument argument = new ArgumentParser().CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PropertyWithEndingTLA);
            Assert.AreEqual("property-with-ending-tla", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentDefaultName_TLAInIt()
        {
            IArgument argument = new ArgumentParser().CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PropertyWithTLAInIt);
            Assert.AreEqual("property-with-tla-in-it", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentDefaultName_OnlyTLA()
        {
            IArgument argument = new ArgumentParser().CreateArgumentCatagory<NameOptions>().WithArgument(x => x.TLA);
            Assert.AreEqual("tla", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentDefaultName_Underscores()
        {
            IArgument argument = new ArgumentParser().CreateArgumentCatagory<NameOptions>().WithArgument(x => x.Property_With_Underscores);
            Assert.AreEqual("property-with-underscores", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentDefaultName_LowerCaseUnderscores()
        {
            IArgument argument = new ArgumentParser().CreateArgumentCatagory<NameOptions>().WithArgument(x => x.property_in_lowercase_with_underscores);
            Assert.AreEqual("property-in-lowercase-with-underscores", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentDefaultName_UppercaseUnderscores()
        {
            IArgument argument = new ArgumentParser().CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PROPERTY_IN_UPPERCASE_WITH_UNDERSCORES);
            Assert.AreEqual("property-in-uppercase-with-underscores", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentDefaultName_NumberInIt()
        {
            IArgument argument = new ArgumentParser().CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PropertyWithNumber1InIt);
            Assert.AreEqual("property-with-number-1-in-it", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentDefaultName_EndingNumber()
        {
            IArgument argument = new ArgumentParser().CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PropertyWithNumberEnding1);
            Assert.AreEqual("property-with-number-ending-1", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentDefaultName_Number()
        {
            IArgument argument = new ArgumentParser().CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PropertyWithTLA1Number);
            Assert.AreEqual("property-with-tla-1-number", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentNameMethodSetsName()
        {
            IArgument<NameOptions, int> argument =
                new ArgumentParser().CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty).Name("custom-name");

            Assert.AreEqual("custom-name", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentNameMethodSingleCharacterShouldThrowArgumentException()
        {
            IArgument<NameOptions, int> argument =
                new ArgumentParser().CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty);

            Assert.ThrowsException<ArgumentException>(() => argument.Name("a"));
        }

        [TestMethod]
        public void ArgumentNameMethodUpperCaseValuesShouldThrowArgumentException()
        {
            IArgument<NameOptions, int> argument =
                new ArgumentParser().CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty);

            Assert.ThrowsException<ArgumentException>(() => argument.Name("UP"));
        }

        [TestMethod]
        public void ArgumentNameMethodSymbolsShouldThrowArgumentException()
        {
            IArgument<NameOptions, int> argument =
                new ArgumentParser().CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty);

            Assert.ThrowsException<ArgumentException>(() => argument.Name("a!"));
            Assert.ThrowsException<ArgumentException>(() => argument.Name("a#"));
            Assert.ThrowsException<ArgumentException>(() => argument.Name("a)"));
            Assert.ThrowsException<ArgumentException>(() => argument.Name("a%"));
            Assert.ThrowsException<ArgumentException>(() => argument.Name("a'"));
            Assert.ThrowsException<ArgumentException>(() => argument.Name("a>"));
            Assert.ThrowsException<ArgumentException>(() => argument.Name("a🍉"));
        }

        [TestMethod]
        public void ArgumentNameMethodOnlyNumbersPasses()
        {
            IArgument argument = new ArgumentParser().CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty).Name("1234");
            Assert.AreEqual("1234", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentNameMethodStartsWithNumberPasses()
        {
            IArgument argument = new ArgumentParser().CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty).Name("1abc");
            Assert.AreEqual("1abc", argument.ArgumentName);
        }

        [TestMethod]
        public void ArgumentNameMethodStartsWithHyphenShouldThrowArgumentException()
        {
            IArgument<NameOptions, int> argument =
                new ArgumentParser().CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty);

            Assert.ThrowsException<ArgumentException>(() => argument.Name("-abc"));
        }

        [TestMethod]
        public void ArgumentNameMethodSpaceShouldThrowArgumentException()
        {
            IArgument<NameOptions, int> argument =
                new ArgumentParser().CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty);

            Assert.ThrowsException<ArgumentException>(() => argument.Name("aaa bbb"));
        }

        [TestMethod]
        public void ArgumentFlagMethodSetsFlag()
        {
            IArgument<NameOptions, int> argument =
                new ArgumentParser().CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty).Flag('a');

            Assert.AreEqual('a', argument.ArgumentFlag);
        }

        [TestMethod]
        public void ArgumentFlagCanSupportUpperCase()
        {
            IArgument<NameOptions, int> argument =
                new ArgumentParser().CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty).Flag('A');

            Assert.AreEqual('A', argument.ArgumentFlag);
        }

        [TestMethod]
        public void ArgumentFlagCanSupportNumbers()
        {
            IArgument<NameOptions, int> argument =
                new ArgumentParser().CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty).Flag('1');

            Assert.AreEqual('1', argument.ArgumentFlag);
        }

        [TestMethod]
        public void ArgumentFlagSymbolShouldThrowArgumentException()
        {
            IArgument<NameOptions, int> argument =
                new ArgumentParser().CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty);

            Assert.ThrowsException<ArgumentException>(() => argument.Flag('!'));
            Assert.ThrowsException<ArgumentException>(() => argument.Flag('~'));
            Assert.ThrowsException<ArgumentException>(() => argument.Flag(';'));
            Assert.ThrowsException<ArgumentException>(() => argument.Flag('"'));
            Assert.ThrowsException<ArgumentException>(() => argument.Flag('<'));
            Assert.ThrowsException<ArgumentException>(() => argument.Flag('.'));
            Assert.ThrowsException<ArgumentException>(() => argument.Flag('-'));
        }

        [TestMethod]
        public void ArgumentRequiredMethodsSetsArgumentToRequired()
        {
            IArgument<BasicOptions, bool> argument =
                new ArgumentParser().CreateArgumentCatagory<BasicOptions>().WithArgument(x => x.Boolean).Required();

            Assert.IsTrue(argument.IsRequired);
        }

        [TestMethod]
        public void ArgumentWithArgumentReturnsANewArgument()
        {
            IArgument<BasicOptions, bool> argument =
                new ArgumentParser().CreateArgumentCatagory<BasicOptions>().WithArgument(x => x.Boolean);

            Assert.AreNotSame(argument, argument.WithArgument(x => x.Byte));
        }

        [TestMethod]
        public void ArgumentGetCatagoryReturnsCatagory()
        {
            IArgument<BasicOptions, bool> argument =
                new ArgumentParser().CreateArgumentCatagory<BasicOptions>().WithArgument(x => x.Boolean);

            Assert.IsInstanceOfType(argument.GetArgumentCatagory<BasicOptions>(), typeof(BasicOptions));
        }

        [TestMethod]
        public void ArgumentGetCatagoryIsNotNull()
        {
            IArgument<BasicOptions, bool> argument =
                new ArgumentParser().CreateArgumentCatagory<BasicOptions>().WithArgument(x => x.Boolean);

            Assert.IsNotNull(argument.GetArgumentCatagory<BasicOptions>());
        }

        [TestMethod]
        public void ArgumentBasicArgumentTypeIsNotMultiple()
        {
            IArgument<BasicOptions, bool> argument =
                new ArgumentParser().CreateArgumentCatagory<BasicOptions>().WithArgument(x => x.Boolean);

            Assert.IsFalse(argument.IsMultiple);
        }

        [TestMethod]
        public void ArgumentEnumerableArgumentTypeIsMultiple()
        {
            IArgument<MultiOptions, IEnumerable<bool>> argument =
                new ArgumentParser().CreateArgumentCatagory<MultiOptions>().WithArgument(x => x.Boolean);

            Assert.IsTrue(argument.IsMultiple);
        }

        [TestMethod]
        public void ArgumentEnumerableTypesNotSupportedExceptIEnumerable()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new ArgumentParser().CreateArgumentCatagory<MultiOptionsEnumerableType>().WithArgument(x => x.ICollection));
            Assert.ThrowsException<ArgumentException>(() =>
                new ArgumentParser().CreateArgumentCatagory<MultiOptionsEnumerableType>().WithArgument(x => x.IReadOnlyCollection));
            Assert.ThrowsException<ArgumentException>(() =>
                new ArgumentParser().CreateArgumentCatagory<MultiOptionsEnumerableType>().WithArgument(x => x.IDictionary));
            Assert.ThrowsException<ArgumentException>(() =>
                new ArgumentParser().CreateArgumentCatagory<MultiOptionsEnumerableType>().WithArgument(x => x.Dictionary));
            Assert.ThrowsException<ArgumentException>(() =>
                new ArgumentParser().CreateArgumentCatagory<MultiOptionsEnumerableType>().WithArgument(x => x.IList));
            Assert.ThrowsException<ArgumentException>(() =>
                new ArgumentParser().CreateArgumentCatagory<MultiOptionsEnumerableType>().WithArgument(x => x.List));
        }

        [TestMethod]
        public void ArgumentPropertyWithGetAndSetAccessorsPasses()
        {
            new ArgumentParser().CreateArgumentCatagory<PropertyTypes>().WithArgument(x => x.GetAndSet);
        }

        [TestMethod]
        public void ArgumentWithoutGetOrSetShouldThrowArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                new ArgumentParser().CreateArgumentCatagory<PropertyTypes>().WithArgument(x => x.GetOnly));
            // Build time error. Cannot test anyway
            //Assert.ThrowsException<ArgumentException>(() =>
            //    parser.CreateArgumentCatagory<PropertyTypes>().WithArgument(x => x.SetOnly));
        }
    }
}
