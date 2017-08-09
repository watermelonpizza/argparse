using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace argparse.UnitTests
{
    public class ParameterCatagoryTests
    {
        [Fact]
        public void ParameterCatagoryDefaultName_PascalCase()
        {
            Assert.Equal("Options In Pascal Case", new ArgumentParser().CreateParameterCatagory<OptionsInPascalCase>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryDefaultName_LowerCase()
        {
            Assert.Equal("optionsinlowercase", new ArgumentParser().CreateParameterCatagory<optionsinlowercase>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryDefaultName_UpperCase()
        {
            Assert.Equal("OPTIONSINUPPERCASE", new ArgumentParser().CreateParameterCatagory<OPTIONSINUPPERCASE>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryDefaultName_CamelCase()
        {
            Assert.Equal("options In Camel Case", new ArgumentParser().CreateParameterCatagory<optionsInCamelCase>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryDefaultName_EndingTLA()
        {
            Assert.Equal("Options With Ending TLA", new ArgumentParser().CreateParameterCatagory<OptionsWithEndingTLA>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryDefaultName_TLA()
        {
            Assert.Equal("Options With TLA In It", new ArgumentParser().CreateParameterCatagory<OptionsWithTLAInIt>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryDefaultName_OnlyTLA()
        {
            Assert.Equal("TLA", new ArgumentParser().CreateParameterCatagory<TLA>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryDefaultName_Underscores()
        {
            Assert.Equal("Options With Underscores", new ArgumentParser().CreateParameterCatagory<Options_With_Underscores>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryDefaultName_LowerCaseUnderscores()
        {
            Assert.Equal("options in lowercase with underscores", new ArgumentParser().CreateParameterCatagory<options_in_lowercase_with_underscores>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryDefaultName_UpperCaseWithUnderscores()
        {
            Assert.Equal("OPTIONS IN UPPERCASE WITH UNDERSCORES", new ArgumentParser().CreateParameterCatagory<OPTIONS_IN_UPPERCASE_WITH_UNDERSCORES>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryDefaultName_NumberInIt()
        {
            Assert.Equal("Options With Number 1 In It", new ArgumentParser().CreateParameterCatagory<OptionsWithNumber1InIt>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryDefaultName_NumberEnding()
        {
            Assert.Equal("Options With Number Ending 1", new ArgumentParser().CreateParameterCatagory<OptionsWithNumberEnding1>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryDefaultName_TLAAndNumber()
        {
            Assert.Equal("Options With TLA 1 Number", new ArgumentParser().CreateParameterCatagory<OptionsWithTLA1Number>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryNameMethodSetsName()
        {
            IParameterCatagory<NoOptions> cat = new ArgumentParser().CreateParameterCatagory<NoOptions>();
            cat.Name("CustomName");

            Assert.Equal("CustomName", cat.CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryNameMethodSetsAnything()
        {
            Assert.Equal("CustomName", new ArgumentParser().CreateParameterCatagory<NoOptions>().Name("CustomName").CatagoryName);
            Assert.Equal("MY fun N!!", new ArgumentParser().CreateParameterCatagory<NoOptions>().Name("MY fun N!!").CatagoryName);
            Assert.Equal("12345!@#$%^&*()_+", new ArgumentParser().CreateParameterCatagory<NoOptions>().Name("12345!@#$%^&*()_+").CatagoryName);
            Assert.Equal("🍉 watermelon options", new ArgumentParser().CreateParameterCatagory<NoOptions>().Name("🍉 watermelon options").CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryWithCatagoryNotNull()
        {
            IParameterCatagory<BasicOptions> catagory = new ArgumentParser().CreateParameterCatagory<BasicOptions>();

            Assert.NotNull(catagory.WithParameter(x => x.Boolean));
        }

        [Fact]
        public void ParameterCatagoryWithCatagoryIsTypeIPatameter()
        {
            IParameterCatagory<BasicOptions> catagory = new ArgumentParser().CreateParameterCatagory<BasicOptions>();

            Assert.IsType<IParameter>(catagory.WithParameter(x => x.Boolean));
        }

        [Fact]
        public void ParameterCatagoryWithCatagoryIsTypeIParameter_T_()
        {
            IParameterCatagory<BasicOptions> catagory = new ArgumentParser().CreateParameterCatagory<BasicOptions>();

            Assert.IsType<IParameter<BasicOptions, bool>>(catagory.WithParameter(x => x.Boolean));
        }

        [Fact]
        public void ParameterCatagoryWithCatagoryOnSamePropertyThrowArgumentException()
        {
            IParameterCatagory<BasicOptions> catagory = new ArgumentParser().CreateParameterCatagory<BasicOptions>();

            Assert.Throws<ArgumentException>(() => catagory.WithParameter(x => x.Boolean).WithParameter(x => x.Boolean));
        }
    }
}