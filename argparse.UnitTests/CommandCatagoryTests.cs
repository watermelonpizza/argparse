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
            Assert.Equal("Options In Pascal Case", ArgumentParser.Create("app").CreateCommandCatagory<OptionsInPascalCase>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryDefaultName_LowerCase()
        {
            Assert.Equal("optionsinlowercase", ArgumentParser.Create("app").CreateCommandCatagory<optionsinlowercase>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryDefaultName_UpperCase()
        {
            Assert.Equal("OPTIONSINUPPERCASE", ArgumentParser.Create("app").CreateCommandCatagory<OPTIONSINUPPERCASE>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryDefaultName_CamelCase()
        {
            Assert.Equal("options In Camel Case", ArgumentParser.Create("app").CreateCommandCatagory<optionsInCamelCase>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryDefaultName_EndingTLA()
        {
            Assert.Equal("Options With Ending TLA", ArgumentParser.Create("app").CreateCommandCatagory<OptionsWithEndingTLA>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryDefaultName_TLA()
        {
            Assert.Equal("Options With TLA In It", ArgumentParser.Create("app").CreateCommandCatagory<OptionsWithTLAInIt>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryDefaultName_OnlyTLA()
        {
            Assert.Equal("TLA", ArgumentParser.Create("app").CreateCommandCatagory<TLA>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryDefaultName_Underscores()
        {
            Assert.Equal("Options With Underscores", ArgumentParser.Create("app").CreateCommandCatagory<Options_With_Underscores>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryDefaultName_LowerCaseUnderscores()
        {
            Assert.Equal("options in lowercase with underscores", ArgumentParser.Create("app").CreateCommandCatagory<options_in_lowercase_with_underscores>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryDefaultName_UpperCaseWithUnderscores()
        {
            Assert.Equal("OPTIONS IN UPPERCASE WITH UNDERSCORES", ArgumentParser.Create("app").CreateCommandCatagory<OPTIONS_IN_UPPERCASE_WITH_UNDERSCORES>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryDefaultName_NumberInIt()
        {
            Assert.Equal("Options With Number 1 In It", ArgumentParser.Create("app").CreateCommandCatagory<OptionsWithNumber1InIt>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryDefaultName_NumberEnding()
        {
            Assert.Equal("Options With Number Ending 1", ArgumentParser.Create("app").CreateCommandCatagory<OptionsWithNumberEnding1>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryDefaultName_TLAAndNumber()
        {
            Assert.Equal("Options With TLA 1 Number", ArgumentParser.Create("app").CreateCommandCatagory<OptionsWithTLA1Number>().CatagoryName);
        }

        [Fact]
        public void CommandCatagoryNameMethodSetsName()
        {
            ICommandCatagory<NoOptions> cat = ArgumentParser.Create("app").CreateCommandCatagory<NoOptions>();
            cat.Name("CustomName");

            Assert.Equal("CustomName", cat.CatagoryName);
        }

        [Fact]
        public void CommandCatagoryNameMethodSetsAnything()
        {
            Assert.Equal("CustomName", ArgumentParser.Create("app").CreateCommandCatagory<NoOptions>().Name("CustomName").CatagoryName);
            Assert.Equal("MY fun N!!", ArgumentParser.Create("app").CreateCommandCatagory<NoOptions>().Name("MY fun N!!").CatagoryName);
            Assert.Equal("12345!@#$%^&*()_+", ArgumentParser.Create("app").CreateCommandCatagory<NoOptions>().Name("12345!@#$%^&*()_+").CatagoryName);
            Assert.Equal("🍉 watermelon options", ArgumentParser.Create("app").CreateCommandCatagory<NoOptions>().Name("🍉 watermelon options").CatagoryName);
        }

        [Fact]
        public void CommandCatagoryWithCommandNotNull()
        {
            ICommandCatagory<Commands> catagory = ArgumentParser.Create("app").CreateCommandCatagory<Commands>();

            Assert.NotNull(catagory.WithCommand(x => x.FlagCommand));
        }

        [Fact]
        public void CommandCatagoryWithArgumentIsTypeIArgument()
        {
            ICommandCatagory<Commands> catagory = ArgumentParser.Create("app").CreateCommandCatagory<Commands>();

            Assert.IsAssignableFrom<ICommand>(catagory.WithCommand(x => x.FlagCommand));
        }

        [Fact]
        public void CommandCatagoryWithArgumentIsTypeIArgument_T_()
        {
            ICommandCatagory<Commands> catagory = ArgumentParser.Create("app").CreateCommandCatagory<Commands>();

            Assert.IsAssignableFrom<ICommand<Commands>>(catagory.WithCommand(x => x.FlagCommand));
        }

        [Fact]
        public void CommandCatagoryWithCatagoryOnSamePropertyThrowArgumentException()
        {
            ICommandCatagory<Commands> catagory = ArgumentParser.Create("app").CreateCommandCatagory<Commands>();

            Assert.Throws<ArgumentException>(() => catagory.WithCommand(x => x.FlagCommand).WithCommand(x => x.FlagCommand));
            Assert.Throws<ArgumentException>(() => catagory.WithCommand(x => x.CommandWithArguments, (sp)=> { }).WithCommand(x => x.CommandWithArguments, (sp) => { }));
        }
    }
}