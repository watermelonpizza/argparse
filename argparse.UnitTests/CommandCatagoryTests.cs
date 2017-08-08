using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace argparse.UnitTests
{
    [TestClass]
    public class CommandCatagoryTests
    {
        [TestMethod]
        public void CommandCatagoryDefaultName_PascalCase()
        {
            Assert.AreEqual("Options In Pascal Case", new ArgumentParser().CreateCommandCatagory<OptionsInPascalCase>().CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryDefaultName_LowerCase()
        {
            Assert.AreEqual("optionsinlowercase", new ArgumentParser().CreateCommandCatagory<optionsinlowercase>().CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryDefaultName_UpperCase()
        {
            Assert.AreEqual("OPTIONSINUPPERCASE", new ArgumentParser().CreateCommandCatagory<OPTIONSINUPPERCASE>().CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryDefaultName_CamelCase()
        {
            Assert.AreEqual("options In Camel Case", new ArgumentParser().CreateCommandCatagory<optionsInCamelCase>().CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryDefaultName_EndingTLA()
        {
            Assert.AreEqual("Options With Ending TLA", new ArgumentParser().CreateCommandCatagory<OptionsWithEndingTLA>().CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryDefaultName_TLA()
        {
            Assert.AreEqual("Options With TLA In It", new ArgumentParser().CreateCommandCatagory<OptionsWithTLAInIt>().CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryDefaultName_OnlyTLA()
        {
            Assert.AreEqual("TLA", new ArgumentParser().CreateCommandCatagory<TLA>().CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryDefaultName_Underscores()
        {
            Assert.AreEqual("Options With Underscores", new ArgumentParser().CreateCommandCatagory<Options_With_Underscores>().CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryDefaultName_LowerCaseUnderscores()
        {
            Assert.AreEqual("options in lowercase with underscores", new ArgumentParser().CreateCommandCatagory<options_in_lowercase_with_underscores>().CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryDefaultName_UpperCaseWithUnderscores()
        {
            Assert.AreEqual("OPTIONS IN UPPERCASE WITH UNDERSCORES", new ArgumentParser().CreateCommandCatagory<OPTIONS_IN_UPPERCASE_WITH_UNDERSCORES>().CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryDefaultName_NumberInIt()
        {
            Assert.AreEqual("Options With Number 1 In It", new ArgumentParser().CreateCommandCatagory<OptionsWithNumber1InIt>().CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryDefaultName_NumberEnding()
        {
            Assert.AreEqual("Options With Number Ending 1", new ArgumentParser().CreateCommandCatagory<OptionsWithNumberEnding1>().CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryDefaultName_TLAAndNumber()
        {
            Assert.AreEqual("Options With TLA 1 Number", new ArgumentParser().CreateCommandCatagory<OptionsWithTLA1Number>().CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryNameMethodSetsName()
        {
            ICommandCatagory<NoOptions> cat = new ArgumentParser().CreateCommandCatagory<NoOptions>();
            cat.Name("CustomName");

            Assert.AreEqual("CustomName", cat.CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryNameMethodSetsAnything()
        {
            Assert.AreEqual("CustomName", new ArgumentParser().CreateCommandCatagory<NoOptions>().Name("CustomName").CatagoryName);
            Assert.AreEqual("MY fun N!!", new ArgumentParser().CreateCommandCatagory<NoOptions>().Name("MY fun N!!").CatagoryName);
            Assert.AreEqual("12345!@#$%^&*()_+", new ArgumentParser().CreateCommandCatagory<NoOptions>().Name("12345!@#$%^&*()_+").CatagoryName);
            Assert.AreEqual("🍉 watermelon options", new ArgumentParser().CreateCommandCatagory<NoOptions>().Name("🍉 watermelon options").CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryWithCommandIsNotNull()
        {
            ICommandCatagory<Commands> catagory = new ArgumentParser().CreateCommandCatagory<Commands>();

            Assert.IsNotNull(catagory.WithCommand(x => x.FlagCommand));
        }

        [TestMethod]
        public void CommandCatagoryWithArgumentIsTypeIArgument()
        {
            ICommandCatagory<Commands> catagory = new ArgumentParser().CreateCommandCatagory<Commands>();

            Assert.IsInstanceOfType(catagory.WithCommand(x => x.FlagCommand), typeof(ICommand));
        }

        [TestMethod]
        public void CommandCatagoryWithArgumentIsTypeIArgument_T_()
        {
            ICommandCatagory<Commands> catagory = new ArgumentParser().CreateCommandCatagory<Commands>();

            Assert.IsInstanceOfType(catagory.WithCommand(x => x.FlagCommand), typeof(ICommand<Commands>));
        }
    }
}
