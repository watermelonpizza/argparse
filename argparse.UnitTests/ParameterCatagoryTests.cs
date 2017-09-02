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
            Assert.Equal("Options In Pascal Case", ArgumentParser.Create("app").CreateParameterCatagory<OptionsInPascalCase>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryDefaultName_LowerCase()
        {
            Assert.Equal("optionsinlowercase", ArgumentParser.Create("app").CreateParameterCatagory<optionsinlowercase>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryDefaultName_UpperCase()
        {
            Assert.Equal("OPTIONSINUPPERCASE", ArgumentParser.Create("app").CreateParameterCatagory<OPTIONSINUPPERCASE>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryDefaultName_CamelCase()
        {
            Assert.Equal("options In Camel Case", ArgumentParser.Create("app").CreateParameterCatagory<optionsInCamelCase>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryDefaultName_EndingTLA()
        {
            Assert.Equal("Options With Ending TLA", ArgumentParser.Create("app").CreateParameterCatagory<OptionsWithEndingTLA>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryDefaultName_TLA()
        {
            Assert.Equal("Options With TLA In It", ArgumentParser.Create("app").CreateParameterCatagory<OptionsWithTLAInIt>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryDefaultName_OnlyTLA()
        {
            Assert.Equal("TLA", ArgumentParser.Create("app").CreateParameterCatagory<TLA>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryDefaultName_Underscores()
        {
            Assert.Equal("Options With Underscores", ArgumentParser.Create("app").CreateParameterCatagory<Options_With_Underscores>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryDefaultName_LowerCaseUnderscores()
        {
            Assert.Equal("options in lowercase with underscores", ArgumentParser.Create("app").CreateParameterCatagory<options_in_lowercase_with_underscores>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryDefaultName_UpperCaseWithUnderscores()
        {
            Assert.Equal("OPTIONS IN UPPERCASE WITH UNDERSCORES", ArgumentParser.Create("app").CreateParameterCatagory<OPTIONS_IN_UPPERCASE_WITH_UNDERSCORES>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryDefaultName_NumberInIt()
        {
            Assert.Equal("Options With Number 1 In It", ArgumentParser.Create("app").CreateParameterCatagory<OptionsWithNumber1InIt>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryDefaultName_NumberEnding()
        {
            Assert.Equal("Options With Number Ending 1", ArgumentParser.Create("app").CreateParameterCatagory<OptionsWithNumberEnding1>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryDefaultName_TLAAndNumber()
        {
            Assert.Equal("Options With TLA 1 Number", ArgumentParser.Create("app").CreateParameterCatagory<OptionsWithTLA1Number>().CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryNameMethodSetsName()
        {
            IParameterCatagory<NoOptions> cat = ArgumentParser.Create("app").CreateParameterCatagory<NoOptions>();
            cat.Name("CustomName");

            Assert.Equal("CustomName", cat.CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryNameMethodSetsAnything()
        {
            Assert.Equal("CustomName", ArgumentParser.Create("app").CreateParameterCatagory<NoOptions>().Name("CustomName").CatagoryName);
            Assert.Equal("MY fun N!!", ArgumentParser.Create("app").CreateParameterCatagory<NoOptions>().Name("MY fun N!!").CatagoryName);
            Assert.Equal("12345!@#$%^&*()_+", ArgumentParser.Create("app").CreateParameterCatagory<NoOptions>().Name("12345!@#$%^&*()_+").CatagoryName);
            Assert.Equal("🍉 watermelon options", ArgumentParser.Create("app").CreateParameterCatagory<NoOptions>().Name("🍉 watermelon options").CatagoryName);
        }

        [Fact]
        public void ParameterCatagoryWithCatagoryNotNull()
        {
            IParameterCatagory<BasicOptions> catagory = ArgumentParser.Create("app").CreateParameterCatagory<BasicOptions>();

            Assert.NotNull(catagory.WithParameter(x => x.Boolean));
        }

        [Fact]
        public void ParameterCatagoryWithCatagoryIsTypeIPatameter()
        {
            IParameterCatagory<BasicOptions> catagory = ArgumentParser.Create("app").CreateParameterCatagory<BasicOptions>();

            Assert.IsAssignableFrom<IParameter>(catagory.WithParameter(x => x.Boolean));
        }

        [Fact]
        public void ParameterCatagoryWithCatagoryIsTypeIParameter_T_()
        {
            IParameterCatagory<BasicOptions> catagory = ArgumentParser.Create("app").CreateParameterCatagory<BasicOptions>();

            Assert.IsAssignableFrom<IParameter<BasicOptions, bool>>(catagory.WithParameter(x => x.Boolean));
        }

        [Fact]
        public void ParameterCatagoryWithCatagoryOnSamePropertyThrowArgumentException()
        {
            IParameterCatagory<BasicOptions> catagory = ArgumentParser.Create("app").CreateParameterCatagory<BasicOptions>();

            Assert.Throws<ArgumentException>(() => catagory.WithParameter(x => x.Boolean).WithParameter(x => x.Boolean));
        }

        [Fact]
        public void ParameterCatagoryWithMultiParameterOnSamePropertyThrowArgumentException()
        {
            IParameterCatagory<MultiOptions> catagory = ArgumentParser.Create("app").CreateParameterCatagory<MultiOptions>();

            Assert.Throws<ArgumentException>(() => catagory.WithMultiParameter(x => x.Boolean).WithMultiParameter(x => x.Boolean));
        }
    }
}