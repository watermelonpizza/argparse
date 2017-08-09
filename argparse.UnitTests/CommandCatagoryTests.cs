using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace argparse.UnitTests
{
        public class CommandCatagoryTests
    {
        [Fact]
        public void CommandCatagoryDefaultName_PascalCase()
        {
            Assert.Equal("Options In Pascal Case", new ArgumentParser().CreateCommandCatagory<OptionsInPascalCase>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryDefaultName_LowerCase()
        {
            Assert.Equal("optionsinlowercase", new ArgumentParser().CreateCommandCatagory<optionsinlowercase>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryDefaultName_UpperCase()
        {
            Assert.Equal("OPTIONSINUPPERCASE", new ArgumentParser().CreateCommandCatagory<OPTIONSINUPPERCASE>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryDefaultName_CamelCase()
        {
            Assert.Equal("options In Camel Case", new ArgumentParser().CreateCommandCatagory<optionsInCamelCase>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryDefaultName_EndingTLA()
        {
            Assert.Equal("Options With Ending TLA", new ArgumentParser().CreateCommandCatagory<OptionsWithEndingTLA>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryDefaultName_TLA()
        {
            Assert.Equal("Options With TLA In It", new ArgumentParser().CreateCommandCatagory<OptionsWithTLAInIt>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryDefaultName_OnlyTLA()
        {
            Assert.Equal("TLA", new ArgumentParser().CreateCommandCatagory<TLA>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryDefaultName_Underscores()
        {
            Assert.Equal("Options With Underscores", new ArgumentParser().CreateCommandCatagory<Options_With_Underscores>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryDefaultName_LowerCaseUnderscores()
        {
            Assert.Equal("options in lowercase with underscores", new ArgumentParser().CreateCommandCatagory<options_in_lowercase_with_underscores>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryDefaultName_UpperCaseWithUnderscores()
        {
            Assert.Equal("OPTIONS IN UPPERCASE WITH UNDERSCORES", new ArgumentParser().CreateCommandCatagory<OPTIONS_IN_UPPERCASE_WITH_UNDERSCORES>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryDefaultName_NumberInIt()
        {
            Assert.Equal("Options With Number 1 In It", new ArgumentParser().CreateCommandCatagory<OptionsWithNumber1InIt>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryDefaultName_NumberEnding()
        {
            Assert.Equal("Options With Number Ending 1", new ArgumentParser().CreateCommandCatagory<OptionsWithNumberEnding1>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryDefaultName_TLAAndNumber()
        {
            Assert.Equal("Options With TLA 1 Number", new ArgumentParser().CreateCommandCatagory<OptionsWithTLA1Number>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryNameMethodSetsName()
        {
            ICommandCatagory<NoOptions> cat = new ArgumentParser().CreateCommandCatagory<NoOptions>();
            cat.Name("CustomName");

            Assert.Equal("CustomName", cat.CatagoryName);
        }

        [Fact]
        public void CommandCatagoryNameMethodSetsAnything()
        {
            Assert.Equal("CustomName", new ArgumentParser().CreateCommandCatagory<NoOptions>().Name("CustomName").CatagoryName);
            Assert.Equal("MY fun N!!", new ArgumentParser().CreateCommandCatagory<NoOptions>().Name("MY fun N!!").CatagoryName);
            Assert.Equal("12345!@#$%^&*()_+", new ArgumentParser().CreateCommandCatagory<NoOptions>().Name("12345!@#$%^&*()_+").CatagoryName);
            Assert.Equal("🍉 watermelon options", new ArgumentParser().CreateCommandCatagory<NoOptions>().Name("🍉 watermelon options").CatagoryName);
        }

        [Fact]
        public void CommandCatagoryWithCommandNotNull()
        {
            ICommandCatagory<Commands> catagory = new ArgumentParser().CreateCommandCatagory<Commands>();

            Assert.NotNull(catagory.WithCommand(x => x.FlagCommand));
        }

        [Fact]
        public void CommandCatagoryWithArgumentIsTypeIArgument()
        {
            ICommandCatagory<Commands> catagory = new ArgumentParser().CreateCommandCatagory<Commands>();

            Assert.IsType<ICommand>(catagory.WithCommand(x => x.FlagCommand));
        }

        [Fact]
        public void CommandCatagoryWithArgumentIsTypeIArgument_T_()
        {
            ICommandCatagory<Commands> catagory = new ArgumentParser().CreateCommandCatagory<Commands>();

            Assert.IsType<ICommand<Commands>>(catagory.WithCommand(x => x.FlagCommand));
        }
    }
}
