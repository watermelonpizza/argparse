using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace argparse.UnitTests
{
    [TestClass]
    public class ArgumentCatagoryTests
    {
        [TestMethod]
        public void ArgumentCatagoryDefaultName_PascalCase()
        {
            Assert.AreEqual("Options In Pascal Case", new ArgumentParser().CreateArgumentCatagory<OptionsInPascalCase>().CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryDefaultName_LowerCase()
        {
            Assert.AreEqual("optionsinlowercase", new ArgumentParser().CreateArgumentCatagory<optionsinlowercase>().CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryDefaultName_UpperCase()
        {
            Assert.AreEqual("OPTIONSINUPPERCASE", new ArgumentParser().CreateArgumentCatagory<OPTIONSINUPPERCASE>().CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryDefaultName_CamelCase()
        {
            Assert.AreEqual("options In Camel Case", new ArgumentParser().CreateArgumentCatagory<optionsInCamelCase>().CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryDefaultName_EndingTLA()
        {
            Assert.AreEqual("Options With Ending TLA", new ArgumentParser().CreateArgumentCatagory<OptionsWithEndingTLA>().CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryDefaultName_TLA()
        {
            Assert.AreEqual("Options With TLA In It", new ArgumentParser().CreateArgumentCatagory<OptionsWithTLAInIt>().CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryDefaultName_OnlyTLA()
        {
            Assert.AreEqual("TLA", new ArgumentParser().CreateArgumentCatagory<TLA>().CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryDefaultName_Underscores()
        {
            Assert.AreEqual("Options With Underscores", new ArgumentParser().CreateArgumentCatagory<Options_With_Underscores>().CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryDefaultName_LowerCaseUnderscores()
        {
            Assert.AreEqual("options in lowercase with underscores", new ArgumentParser().CreateArgumentCatagory<options_in_lowercase_with_underscores>().CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryDefaultName_UpperCaseWithUnderscores()
        {
            Assert.AreEqual("OPTIONS IN UPPERCASE WITH UNDERSCORES", new ArgumentParser().CreateArgumentCatagory<OPTIONS_IN_UPPERCASE_WITH_UNDERSCORES>().CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryDefaultName_NumberInIt()
        {
            Assert.AreEqual("Options With Number 1 In It", new ArgumentParser().CreateArgumentCatagory<OptionsWithNumber1InIt>().CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryDefaultName_NumberEnding()
        {
            Assert.AreEqual("Options With Number Ending 1", new ArgumentParser().CreateArgumentCatagory<OptionsWithNumberEnding1>().CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryDefaultName_TLAAndNumber()
        {
            Assert.AreEqual("Options With TLA 1 Number", new ArgumentParser().CreateArgumentCatagory<OptionsWithTLA1Number>().CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryNameMethodSetsName()
        {
            IArgumentCatagory<NoOptions> cat = new ArgumentParser().CreateArgumentCatagory<NoOptions>();
            cat.Name("CustomName");

            Assert.AreEqual("CustomName", cat.CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryNameMethodSetsAnything()
        {
            Assert.AreEqual("CustomName", new ArgumentParser().CreateArgumentCatagory<NoOptions>().Name("CustomName").CatagoryName);
            Assert.AreEqual("MY fun N!!", new ArgumentParser().CreateArgumentCatagory<NoOptions>().Name("MY fun N!!").CatagoryName);
            Assert.AreEqual("12345!@#$%^&*()_+", new ArgumentParser().CreateArgumentCatagory<NoOptions>().Name("12345!@#$%^&*()_+").CatagoryName);
            Assert.AreEqual("🍉 watermelon options", new ArgumentParser().CreateArgumentCatagory<NoOptions>().Name("🍉 watermelon options").CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryWithArgumentIsNotNull()
        {
            IArgumentCatagory<BasicOptions> catagory = new ArgumentParser().CreateArgumentCatagory<BasicOptions>();

            Assert.IsNotNull(catagory.WithArgument(x => x.Boolean));
        }

        [TestMethod]
        public void ArgumentCatagoryWithArgumentIsTypeIArgument()
        {
            IArgumentCatagory<BasicOptions> catagory = new ArgumentParser().CreateArgumentCatagory<BasicOptions>();

            Assert.IsInstanceOfType(catagory.WithArgument(x => x.Boolean), typeof(IArgument));
        }

        [TestMethod]
        public void ArgumentCatagoryWithArgumentIsTypeIArgument_T_()
        {
            IArgumentCatagory<BasicOptions> catagory = new ArgumentParser().CreateArgumentCatagory<BasicOptions>();

            Assert.IsInstanceOfType(catagory.WithArgument(x => x.Boolean), typeof(IArgument<BasicOptions, bool>));
        }

        [TestMethod]
        public void ArgumentCatagoryWithArgumentOnSamePropertyThrowArgumentException()
        {
            IArgumentCatagory<BasicOptions> catagory = new ArgumentParser().CreateArgumentCatagory<BasicOptions>();

            Assert.ThrowsException<ArgumentException>(() => catagory.WithArgument(x => x.Boolean).WithArgument(x => x.Boolean));
        }
    }
}
