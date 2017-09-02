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
            Assert.Equal("Options In Pascal Case", ArgumentParser.Create("app").CreateArgumentCatagory<OptionsInPascalCase>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryDefaultName_LowerCase()
        {
            Assert.Equal("optionsinlowercase", ArgumentParser.Create("app").CreateArgumentCatagory<optionsinlowercase>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryDefaultName_UpperCase()
        {
            Assert.Equal("OPTIONSINUPPERCASE", ArgumentParser.Create("app").CreateArgumentCatagory<OPTIONSINUPPERCASE>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryDefaultName_CamelCase()
        {
            Assert.Equal("options In Camel Case", ArgumentParser.Create("app").CreateArgumentCatagory<optionsInCamelCase>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryDefaultName_EndingTLA()
        {
            Assert.Equal("Options With Ending TLA", ArgumentParser.Create("app").CreateArgumentCatagory<OptionsWithEndingTLA>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryDefaultName_TLA()
        {
            Assert.Equal("Options With TLA In It", ArgumentParser.Create("app").CreateArgumentCatagory<OptionsWithTLAInIt>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryDefaultName_OnlyTLA()
        {
            Assert.Equal("TLA", ArgumentParser.Create("app").CreateArgumentCatagory<TLA>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryDefaultName_Underscores()
        {
            Assert.Equal("Options With Underscores", ArgumentParser.Create("app").CreateArgumentCatagory<Options_With_Underscores>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryDefaultName_LowerCaseUnderscores()
        {
            Assert.Equal("options in lowercase with underscores", ArgumentParser.Create("app").CreateArgumentCatagory<options_in_lowercase_with_underscores>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryDefaultName_UpperCaseWithUnderscores()
        {
            Assert.Equal("OPTIONS IN UPPERCASE WITH UNDERSCORES", ArgumentParser.Create("app").CreateArgumentCatagory<OPTIONS_IN_UPPERCASE_WITH_UNDERSCORES>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryDefaultName_NumberInIt()
        {
            Assert.Equal("Options With Number 1 In It", ArgumentParser.Create("app").CreateArgumentCatagory<OptionsWithNumber1InIt>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryDefaultName_NumberEnding()
        {
            Assert.Equal("Options With Number Ending 1", ArgumentParser.Create("app").CreateArgumentCatagory<OptionsWithNumberEnding1>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryDefaultName_TLAAndNumber()
        {
            Assert.Equal("Options With TLA 1 Number", ArgumentParser.Create("app").CreateArgumentCatagory<OptionsWithTLA1Number>().CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryNameMethodSetsName()
        {
            IArgumentCatagory<NoOptions> cat = ArgumentParser.Create("app").CreateArgumentCatagory<NoOptions>();
            cat.Name("CustomName");

            Assert.Equal("CustomName", cat.CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryNameMethodSetsAnything()
        {
            Assert.Equal("CustomName", ArgumentParser.Create("app").CreateArgumentCatagory<NoOptions>().Name("CustomName").CatagoryName);
            Assert.Equal("MY fun N!!", ArgumentParser.Create("app").CreateArgumentCatagory<NoOptions>().Name("MY fun N!!").CatagoryName);
            Assert.Equal("12345!@#$%^&*()_+", ArgumentParser.Create("app").CreateArgumentCatagory<NoOptions>().Name("12345!@#$%^&*()_+").CatagoryName);
            Assert.Equal("🍉 watermelon options", ArgumentParser.Create("app").CreateArgumentCatagory<NoOptions>().Name("🍉 watermelon options").CatagoryName);
        }

        [Fact]
        public void ArgumentCatagoryWithArgumentNotNull()
        {
            IArgumentCatagory<BasicOptions> catagory = ArgumentParser.Create("app").CreateArgumentCatagory<BasicOptions>();

            Assert.NotNull(catagory.WithArgument(x => x.Boolean));
        }

        [Fact]
        public void ArgumentCatagoryWithArgumentIsTypeIArgument()
        {
            IArgumentCatagory<BasicOptions> catagory = ArgumentParser.Create("app").CreateArgumentCatagory<BasicOptions>();

            Assert.IsAssignableFrom<IArgument>(catagory.WithArgument(x => x.Boolean));
        }

        [Fact]
        public void ArgumentCatagoryWithArgumentIsTypeIArgument_T_()
        {
            IArgumentCatagory<BasicOptions> catagory = ArgumentParser.Create("app").CreateArgumentCatagory<BasicOptions>();

            Assert.IsAssignableFrom<IArgument<BasicOptions, bool>>(catagory.WithArgument(x => x.Boolean));
        }

        [Fact]
        public void ArgumentCatagoryWithArgumentOnSamePropertyThrowArgumentException()
        {
            IArgumentCatagory<BasicOptions> catagory = ArgumentParser.Create("app").CreateArgumentCatagory<BasicOptions>();

            Assert.Throws<ArgumentException>(() => catagory.WithArgument(x => x.Boolean).WithArgument(x => x.Boolean));
        }

        [Fact]
        public void ArgumentCatagoryWithMultiArgumentOnSamePropertyThrowArgumentException()
        {
            IArgumentCatagory<MultiOptions> catagory = ArgumentParser.Create("app").CreateArgumentCatagory<MultiOptions>();

            Assert.Throws<ArgumentException>(() => catagory.WithMultiArgument(x => x.Boolean).WithMultiArgument(x => x.Boolean));
        }
    }
}