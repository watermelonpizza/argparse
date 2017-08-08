using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace argparse.UnitTests
{
    [TestClass]
    public class ParameterCatagoryTests
    {
        [TestMethod]
        public void ParameterCatagoryDefaultName_PascalCase()
        {
            Assert.AreEqual("Options In Pascal Case", new ArgumentParser().CreateParameterCatagory<OptionsInPascalCase>().CatagoryName);
        }

        [TestMethod]
        public void ParameterCatagoryDefaultName_LowerCase()
        {
            Assert.AreEqual("optionsinlowercase", new ArgumentParser().CreateParameterCatagory<optionsinlowercase>().CatagoryName);
        }

        [TestMethod]
        public void ParameterCatagoryDefaultName_UpperCase()
        {
            Assert.AreEqual("OPTIONSINUPPERCASE", new ArgumentParser().CreateParameterCatagory<OPTIONSINUPPERCASE>().CatagoryName);
        }

        [TestMethod]
        public void ParameterCatagoryDefaultName_CamelCase()
        {
            Assert.AreEqual("options In Camel Case", new ArgumentParser().CreateParameterCatagory<optionsInCamelCase>().CatagoryName);
        }

        [TestMethod]
        public void ParameterCatagoryDefaultName_EndingTLA()
        {
            Assert.AreEqual("Options With Ending TLA", new ArgumentParser().CreateParameterCatagory<OptionsWithEndingTLA>().CatagoryName);
        }

        [TestMethod]
        public void ParameterCatagoryDefaultName_TLA()
        {
            Assert.AreEqual("Options With TLA In It", new ArgumentParser().CreateParameterCatagory<OptionsWithTLAInIt>().CatagoryName);
        }

        [TestMethod]
        public void ParameterCatagoryDefaultName_OnlyTLA()
        {
            Assert.AreEqual("TLA", new ArgumentParser().CreateParameterCatagory<TLA>().CatagoryName);
        }

        [TestMethod]
        public void ParameterCatagoryDefaultName_Underscores()
        {
            Assert.AreEqual("Options With Underscores", new ArgumentParser().CreateParameterCatagory<Options_With_Underscores>().CatagoryName);
        }

        [TestMethod]
        public void ParameterCatagoryDefaultName_LowerCaseUnderscores()
        {
            Assert.AreEqual("options in lowercase with underscores", new ArgumentParser().CreateParameterCatagory<options_in_lowercase_with_underscores>().CatagoryName);
        }

        [TestMethod]
        public void ParameterCatagoryDefaultName_UpperCaseWithUnderscores()
        {
            Assert.AreEqual("OPTIONS IN UPPERCASE WITH UNDERSCORES", new ArgumentParser().CreateParameterCatagory<OPTIONS_IN_UPPERCASE_WITH_UNDERSCORES>().CatagoryName);
        }

        [TestMethod]
        public void ParameterCatagoryDefaultName_NumberInIt()
        {
            Assert.AreEqual("Options With Number 1 In It", new ArgumentParser().CreateParameterCatagory<OptionsWithNumber1InIt>().CatagoryName);
        }

        [TestMethod]
        public void ParameterCatagoryDefaultName_NumberEnding()
        {
            Assert.AreEqual("Options With Number Ending 1", new ArgumentParser().CreateParameterCatagory<OptionsWithNumberEnding1>().CatagoryName);
        }

        [TestMethod]
        public void ParameterCatagoryDefaultName_TLAAndNumber()
        {
            Assert.AreEqual("Options With TLA 1 Number", new ArgumentParser().CreateParameterCatagory<OptionsWithTLA1Number>().CatagoryName);
        }

        [TestMethod]
        public void ParameterCatagoryNameMethodSetsName()
        {
            IParameterCatagory<NoOptions> cat = new ArgumentParser().CreateParameterCatagory<NoOptions>();
            cat.Name("CustomName");

            Assert.AreEqual("CustomName", cat.CatagoryName);
        }

        [TestMethod]
        public void ParameterCatagoryNameMethodSetsAnything()
        {
            Assert.AreEqual("CustomName", new ArgumentParser().CreateParameterCatagory<NoOptions>().Name("CustomName").CatagoryName);
            Assert.AreEqual("MY fun N!!", new ArgumentParser().CreateParameterCatagory<NoOptions>().Name("MY fun N!!").CatagoryName);
            Assert.AreEqual("12345!@#$%^&*()_+", new ArgumentParser().CreateParameterCatagory<NoOptions>().Name("12345!@#$%^&*()_+").CatagoryName);
            Assert.AreEqual("🍉 watermelon options", new ArgumentParser().CreateParameterCatagory<NoOptions>().Name("🍉 watermelon options").CatagoryName);
        }

        [TestMethod]
        public void ParameterCatagoryWithCatagoryIsNotNull()
        {
            IParameterCatagory<BasicOptions> catagory = new ArgumentParser().CreateParameterCatagory<BasicOptions>();

            Assert.IsNotNull(catagory.WithParameter(x => x.Boolean));
        }

        [TestMethod]
        public void ParameterCatagoryWithCatagoryIsTypeIPatameter()
        {
            IParameterCatagory<BasicOptions> catagory = new ArgumentParser().CreateParameterCatagory<BasicOptions>();

            Assert.IsInstanceOfType(catagory.WithParameter(x => x.Boolean), typeof(IParameter));
        }

        [TestMethod]
        public void ParameterCatagoryWithCatagoryIsTypeIParameter_T_()
        {
            IParameterCatagory<BasicOptions> catagory = new ArgumentParser().CreateParameterCatagory<BasicOptions>();

            Assert.IsInstanceOfType(catagory.WithParameter(x => x.Boolean), typeof(IParameter<BasicOptions, bool>));
        }

        [TestMethod]
        public void ParameterCatagoryWithCatagoryOnSamePropertyThrowArgumentException()
        {
            IParameterCatagory<BasicOptions> catagory = new ArgumentParser().CreateParameterCatagory<BasicOptions>();

            Assert.ThrowsException<ArgumentException>(() => catagory.WithParameter(x => x.Boolean).WithParameter(x => x.Boolean));
        }
    }
}
