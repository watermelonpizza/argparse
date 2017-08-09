using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace argparse.UnitTests
{
        public class ArgumentCatagoryTests
    {
        [Fact]
        public void ArgumentCatagoryDefaultName_PascalCase()
        {
            Assert.Equal("Options In Pascal Case", new ArgumentParser().CreateArgumentCatagory<OptionsInPascalCase>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryDefaultName_LowerCase()
        {
            Assert.Equal("optionsinlowercase", new ArgumentParser().CreateArgumentCatagory<optionsinlowercase>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryDefaultName_UpperCase()
        {
            Assert.Equal("OPTIONSINUPPERCASE", new ArgumentParser().CreateArgumentCatagory<OPTIONSINUPPERCASE>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryDefaultName_CamelCase()
        {
            Assert.Equal("options In Camel Case", new ArgumentParser().CreateArgumentCatagory<optionsInCamelCase>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryDefaultName_EndingTLA()
        {
            Assert.Equal("Options With Ending TLA", new ArgumentParser().CreateArgumentCatagory<OptionsWithEndingTLA>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryDefaultName_TLA()
        {
            Assert.Equal("Options With TLA In It", new ArgumentParser().CreateArgumentCatagory<OptionsWithTLAInIt>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryDefaultName_OnlyTLA()
        {
            Assert.Equal("TLA", new ArgumentParser().CreateArgumentCatagory<TLA>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryDefaultName_Underscores()
        {
            Assert.Equal("Options With Underscores", new ArgumentParser().CreateArgumentCatagory<Options_With_Underscores>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryDefaultName_LowerCaseUnderscores()
        {
            Assert.Equal("options in lowercase with underscores", new ArgumentParser().CreateArgumentCatagory<options_in_lowercase_with_underscores>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryDefaultName_UpperCaseWithUnderscores()
        {
            Assert.Equal("OPTIONS IN UPPERCASE WITH UNDERSCORES", new ArgumentParser().CreateArgumentCatagory<OPTIONS_IN_UPPERCASE_WITH_UNDERSCORES>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryDefaultName_NumberInIt()
        {
            Assert.Equal("Options With Number 1 In It", new ArgumentParser().CreateArgumentCatagory<OptionsWithNumber1InIt>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryDefaultName_NumberEnding()
        {
            Assert.Equal("Options With Number Ending 1", new ArgumentParser().CreateArgumentCatagory<OptionsWithNumberEnding1>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryDefaultName_TLAAndNumber()
        {
            Assert.Equal("Options With TLA 1 Number", new ArgumentParser().CreateArgumentCatagory<OptionsWithTLA1Number>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryNameMethodSetsName()
        {
            IArgumentCatagory<NoOptions> cat = new ArgumentParser().CreateArgumentCatagory<NoOptions>();
            cat.Name("CustomName");

            Assert.Equal("CustomName", cat.CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryNameMethodSetsAnything()
        {
            Assert.Equal("CustomName", new ArgumentParser().CreateArgumentCatagory<NoOptions>().Name("CustomName").CatagoryName);
            Assert.Equal("MY fun N!!", new ArgumentParser().CreateArgumentCatagory<NoOptions>().Name("MY fun N!!").CatagoryName);
            Assert.Equal("12345!@#$%^&*()_+", new ArgumentParser().CreateArgumentCatagory<NoOptions>().Name("12345!@#$%^&*()_+").CatagoryName);
            Assert.Equal("🍉 watermelon options", new ArgumentParser().CreateArgumentCatagory<NoOptions>().Name("🍉 watermelon options").CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryWithArgumentNotNull()
        {
            IArgumentCatagory<BasicOptions> catagory = new ArgumentParser().CreateArgumentCatagory<BasicOptions>();

            Assert.NotNull(catagory.WithArgument(x => x.Boolean));
        }

        [Fact]
        public void ArgumentCatagoryWithArgumentIsTypeIArgument()
        {
            IArgumentCatagory<BasicOptions> catagory = new ArgumentParser().CreateArgumentCatagory<BasicOptions>();

            Assert.IsType<IArgument>(catagory.WithArgument(x => x.Boolean));
        }

        [Fact]
        public void ArgumentCatagoryWithArgumentIsTypeIArgument_T_()
        {
            IArgumentCatagory<BasicOptions> catagory = new ArgumentParser().CreateArgumentCatagory<BasicOptions>();

            Assert.IsType<IArgument<BasicOptions, bool>>(catagory.WithArgument(x => x.Boolean));
        }

        [Fact]
        public void ArgumentCatagoryWithArgumentOnSamePropertyThrowArgumentException()
        {
            IArgumentCatagory<BasicOptions> catagory = new ArgumentParser().CreateArgumentCatagory<BasicOptions>();

            Assert.Throws<ArgumentException>(() => catagory.WithArgument(x => x.Boolean).WithArgument(x => x.Boolean));
        }
    }
}
