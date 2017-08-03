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
            Assert.AreEqual("Options In Pascal Case", ArgumentParser.Default.CreateArgumentCatagory<OptionsInPascalCase>().CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryDefaultName_LowerCase()
        {
            Assert.AreEqual("optionsinlowercase", ArgumentParser.Default.CreateArgumentCatagory<optionsinlowercase>().CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryDefaultName_UpperCase()
        {
            Assert.AreEqual("OPTIONSINUPPERCASE", ArgumentParser.Default.CreateArgumentCatagory<OPTIONSINUPPERCASE>().CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryDefaultName_CamelCase()
        {
            Assert.AreEqual("options In Camel Case", ArgumentParser.Default.CreateArgumentCatagory<optionsInCamelCase>().CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryDefaultName_TLA()
        {
            Assert.AreEqual("Options With TLA", ArgumentParser.Default.CreateArgumentCatagory<OptionsWithTLA>().CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryDefaultName_OnlyTLA()
        {
            Assert.AreEqual("TLA", ArgumentParser.Default.CreateArgumentCatagory<TLA>().CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryDefaultName_Underscores()
        {
            Assert.AreEqual("Options With Underscores", ArgumentParser.Default.CreateArgumentCatagory<Options_With_Underscores>().CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryDefaultName_LowerCaseUnderscores()
        {
            Assert.AreEqual("options in lowercase with underscores", ArgumentParser.Default.CreateArgumentCatagory<options_in_lowercase_with_underscores>().CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryDefaultName_UpperCaseWithUnderscores()
        {
            Assert.AreEqual("OPTIONS IN UPPERCASE WITH UNDERSCORES", ArgumentParser.Default.CreateArgumentCatagory<OPTIONS_IN_UPPERCASE_WITH_UNDERSCORES>().CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryNameMethodSetsName()
        {
            IArgumentCatagory<NoOptions> cat = ArgumentParser.Default.CreateArgumentCatagory<NoOptions>();
            cat.Name("CustomName");

            Assert.AreEqual("CustomName", cat.CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryNameMethodSetsAnything()
        {
            Assert.AreEqual("CustomName", ArgumentParser.Default.CreateArgumentCatagory<NoOptions>().Name("CustomName").CatagoryName);
            Assert.AreEqual("MY fun N!!", ArgumentParser.Default.CreateArgumentCatagory<NoOptions>().Name("MY fun N!!").CatagoryName);
            Assert.AreEqual("12345!@#$%^&*()_+", ArgumentParser.Default.CreateArgumentCatagory<NoOptions>().Name("12345@#$%^&()_+").CatagoryName);
            Assert.AreEqual("🍉 watermelon options", ArgumentParser.Default.CreateArgumentCatagory<NoOptions>().Name("🍉 watermelon options").CatagoryName);
        }

        [TestMethod]
        public void ArgumentCatagoryWithArgumentIsNotNull()
        {
            IArgumentCatagory<BasicOptions> catagory = ArgumentParser.Default.CreateArgumentCatagory<BasicOptions>();

            Assert.IsNotNull(catagory.WithArgument(x => x.Boolean));
        }

        [TestMethod]
        public void ArgumentCatagoryWithArgumentIsTypeIArgument()
        {
            IArgumentCatagory<BasicOptions> catagory = ArgumentParser.Default.CreateArgumentCatagory<BasicOptions>();

            Assert.IsInstanceOfType(catagory.WithArgument(x => x.Boolean), typeof(IArgument));
        }

        [TestMethod]
        public void ArgumentCatagoryWithArgumentIsTypeIArgument_T_()
        {
            IArgumentCatagory<BasicOptions> catagory = ArgumentParser.Default.CreateArgumentCatagory<BasicOptions>();

            Assert.IsInstanceOfType(catagory.WithArgument(x => x.Boolean), typeof(IArgument<BasicOptions, bool>));
        }

        [TestMethod]
        public void ArgumentCatagoryWithArgumentOnSamePropertyThrowArgumentException()
        {
            IArgumentCatagory<BasicOptions> catagory = ArgumentParser.Default.CreateArgumentCatagory<BasicOptions>();

            Assert.ThrowsException<ArgumentException>(() => catagory.WithArgument(x => x.Boolean).WithArgument(x => x.Boolean));
        }
    }
}
