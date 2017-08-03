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
            Assert.AreEqual("Options In Pascal Case", ArgumentParser.Default.CreateCommandCatagory<OptionsInPascalCase>().CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryDefaultName_LowerCase()
        {
            Assert.AreEqual("optionsinlowercase", ArgumentParser.Default.CreateCommandCatagory<optionsinlowercase>().CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryDefaultName_UpperCase()
        {
            Assert.AreEqual("OPTIONSINUPPERCASE", ArgumentParser.Default.CreateCommandCatagory<OPTIONSINUPPERCASE>().CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryDefaultName_CamelCase()
        {
            Assert.AreEqual("options In Camel Case", ArgumentParser.Default.CreateCommandCatagory<optionsInCamelCase>().CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryDefaultName_TLA()
        {
            Assert.AreEqual("Options With TLA", ArgumentParser.Default.CreateCommandCatagory<OptionsWithTLA>().CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryDefaultName_OnlyTLA()
        {
            Assert.AreEqual("TLA", ArgumentParser.Default.CreateCommandCatagory<TLA>().CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryDefaultName_Underscores()
        {
            Assert.AreEqual("Options With Underscores", ArgumentParser.Default.CreateCommandCatagory<Options_With_Underscores>().CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryDefaultName_LowerCaseUnderscores()
        {
            Assert.AreEqual("options in lowercase with underscores", ArgumentParser.Default.CreateCommandCatagory<options_in_lowercase_with_underscores>().CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryDefaultName_UpperCaseWithUnderscores()
        {
            Assert.AreEqual("OPTIONS IN UPPERCASE WITH UNDERSCORES", ArgumentParser.Default.CreateCommandCatagory<OPTIONS_IN_UPPERCASE_WITH_UNDERSCORES>().CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryNameMethodSetsName()
        {
            ICommandCatagory<NoOptions> cat = ArgumentParser.Default.CreateCommandCatagory<NoOptions>();
            cat.Name("CustomName");

            Assert.AreEqual("CustomName", cat.CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryNameMethodSetsAnything()
        {
            Assert.AreEqual("CustomName", ArgumentParser.Default.CreateCommandCatagory<NoOptions>().Name("CustomName").CatagoryName);
            Assert.AreEqual("MY fun N!!", ArgumentParser.Default.CreateCommandCatagory<NoOptions>().Name("MY fun N!!").CatagoryName);
            Assert.AreEqual("12345!@#$%^&*()_+", ArgumentParser.Default.CreateCommandCatagory<NoOptions>().Name("12345@#$%^&()_+").CatagoryName);
            Assert.AreEqual("🍉 watermelon options", ArgumentParser.Default.CreateCommandCatagory<NoOptions>().Name("🍉 watermelon options").CatagoryName);
        }

        [TestMethod]
        public void CommandCatagoryWithCommandIsNotNull()
        {
            ICommandCatagory<Commands> catagory = ArgumentParser.Default.CreateCommandCatagory<Commands>();

            Assert.IsNotNull(catagory.WithCommand(x => x.FlagCommand));
        }

        [TestMethod]
        public void CommandCatagoryWithArgumentIsTypeIArgument()
        {
            ICommandCatagory<Commands> catagory = ArgumentParser.Default.CreateCommandCatagory<Commands>();

            Assert.IsInstanceOfType(catagory.WithCommand(x => x.FlagCommand), typeof(ICommand));
        }

        [TestMethod]
        public void CommandCatagoryWithArgumentIsTypeIArgument_T_()
        {
            ICommandCatagory<Commands> catagory = ArgumentParser.Default.CreateCommandCatagory<Commands>();

            Assert.IsInstanceOfType(catagory.WithCommand(x => x.FlagCommand), typeof(ICommand<Commands>));
        }
    }
}
