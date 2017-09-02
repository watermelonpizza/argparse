using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace argparse.UnitTests
{
    public class ParameterTests
    {
        [Fact]
        public void ParameterDefaultName_PascalCase()
        {
            IParameter parameter = ArgumentParser.Create("app").CreateParameterCatagory<NameOptions>().WithParameter(x => x.PascalCaseProperty);
            Assert.Equal("PASCALCASEPROPERTY", parameter.ParameterName);
        }

        [Fact]
        public void ParameterDefaultName_LowerCase()
        {
            IParameter parameter = ArgumentParser.Create("app").CreateParameterCatagory<NameOptions>().WithParameter(x => x.lowercaseproperty);
            Assert.Equal("LOWERCASEPROPERTY", parameter.ParameterName);
        }

        [Fact]
        public void ParameterDefaultName_UpperCase()
        {
            IParameter parameter = ArgumentParser.Create("app").CreateParameterCatagory<NameOptions>().WithParameter(x => x.UPPERCASEPROPERTY);
            Assert.Equal("UPPERCASEPROPERTY", parameter.ParameterName);
        }

        [Fact]
        public void ParameterDefaultName_CamelCase()
        {
            IParameter parameter = ArgumentParser.Create("app").CreateParameterCatagory<NameOptions>().WithParameter(x => x.camelCaseProperty);
            Assert.Equal("CAMELCASEPROPERTY", parameter.ParameterName);
        }

        [Fact]
        public void ParameterDefaultName_EndingTLA()
        {
            IParameter parameter = ArgumentParser.Create("app").CreateParameterCatagory<NameOptions>().WithParameter(x => x.PropertyWithEndingTLA);
            Assert.Equal("PROPERTYWITHENDINGTLA", parameter.ParameterName);
        }

        [Fact]
        public void ParameterDefaultName_TLAInIt()
        {
            IParameter parameter = ArgumentParser.Create("app").CreateParameterCatagory<NameOptions>().WithParameter(x => x.PropertyWithTLAInIt);
            Assert.Equal("PROPERTYWITHTLAINIT", parameter.ParameterName);
        }

        [Fact]
        public void ParameterDefaultName_OnlyTLA()
        {
            IParameter parameter = ArgumentParser.Create("app").CreateParameterCatagory<NameOptions>().WithParameter(x => x.TLA);
            Assert.Equal("TLA", parameter.ParameterName);
        }

        [Fact]
        public void ParameterDefaultName_Underscores()
        {
            IParameter parameter = ArgumentParser.Create("app").CreateParameterCatagory<NameOptions>().WithParameter(x => x.Property_With_Underscores);
            Assert.Equal("PROPERTY_WITH_UNDERSCORES", parameter.ParameterName);
        }

        [Fact]
        public void ParameterDefaultName_LowerCaseUnderscores()
        {
            IParameter parameter = ArgumentParser.Create("app").CreateParameterCatagory<NameOptions>().WithParameter(x => x.property_in_lowercase_with_underscores);
            Assert.Equal("PROPERTY_IN_LOWERCASE_WITH_UNDERSCORES", parameter.ParameterName);
        }

        [Fact]
        public void ParameterDefaultName_UppercaseUnderscores()
        {
            IParameter parameter = ArgumentParser.Create("app").CreateParameterCatagory<NameOptions>().WithParameter(x => x.PROPERTY_IN_UPPERCASE_WITH_UNDERSCORES);
            Assert.Equal("PROPERTY_IN_UPPERCASE_WITH_UNDERSCORES", parameter.ParameterName);
        }

        [Fact]
        public void ParameterDefaultName_NumberInIt()
        {
            IParameter parameter = ArgumentParser.Create("app").CreateParameterCatagory<NameOptions>().WithParameter(x => x.PropertyWithNumber1InIt);
            Assert.Equal("PROPERTYWITHNUMBER1INIT", parameter.ParameterName);
        }

        [Fact]
        public void ParameterDefaultName_EndingNumber()
        {
            IParameter parameter = ArgumentParser.Create("app").CreateParameterCatagory<NameOptions>().WithParameter(x => x.PropertyWithNumberEnding1);
            Assert.Equal("PROPERTYWITHNUMBERENDING1", parameter.ParameterName);
        }

        [Fact]
        public void ParameterDefaultName_Number()
        {
            IParameter parameter = ArgumentParser.Create("app").CreateParameterCatagory<NameOptions>().WithParameter(x => x.PropertyWithTLA1Number);
            Assert.Equal("PROPERTYWITHTLA1NUMBER", parameter.ParameterName);
        }

        [Theory]
        [InlineData("name")]
        [InlineData("NAME")]
        [InlineData("custom-name")]
        [InlineData("ipv4")]
        [InlineData("a sentance")]
        [InlineData("1234")]
        [InlineData("my application is FUN!")]
        [InlineData("a")]
        [InlineData("UP")]
        [InlineData("a!")]
        [InlineData("a#")]
        [InlineData("a)")]
        [InlineData("a%")]
        [InlineData("a'")]
        [InlineData("a>")]
        [InlineData("a🍉")]
        [InlineData("!a")]
        [InlineData("🍉-myname")]
        [InlineData("-abc")]
        [InlineData("abc bbb")]
        [InlineData("-abc bbb")]
        public void ParameterNameMethodAllowedNames(string parameterName)
        {
            IParameter<NameOptions, bool> parameter =
                ArgumentParser.Create("app").CreateParameterCatagory<NameOptions>().WithParameter(x => x.PascalCaseProperty).Name(parameterName);

            Assert.Equal(parameterName, parameter.ParameterName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ParameterNameMethodDissallowedNamesShouldThrowArgumentException(string parameterName)
        {
            IParameter<NameOptions, bool> parameter =
                ArgumentParser.Create("app").CreateParameterCatagory<NameOptions>().WithParameter(x => x.PascalCaseProperty);

            Assert.Throws<ArgumentException>(() => parameter.Name(parameterName));
        }

        [Fact]
        public void ParameterRequiredMethodsSetsArgumentToRequired()
        {
            IParameter<BasicOptions, bool> parameter =
                ArgumentParser.Create("app").CreateParameterCatagory<BasicOptions>().WithParameter(x => x.Boolean).Required();

            Assert.True(parameter.IsRequired);
        }

        [Fact]
        public void ParameterWithParameterReturnsANewArgument()
        {
            IParameter<BasicOptions, bool> parameter =
                ArgumentParser.Create("app").CreateParameterCatagory<BasicOptions>().WithParameter(x => x.Boolean);

            Assert.NotSame(parameter, parameter.WithParameter(x => x.Byte));
        }

        [Fact]
        public void ParameterGetCatagoryReturnsCatagory()
        {
            IParameter<BasicOptions, bool> parameter =
                ArgumentParser.Create("app").CreateParameterCatagory<BasicOptions>().WithParameter(x => x.Boolean);

            Assert.IsType<BasicOptions>(parameter.GetParameterCatagory<BasicOptions>());
        }

        [Fact]
        public void ParameterGetCatagoryNotNull()
        {
            IParameter<BasicOptions, bool> parameter =
                ArgumentParser.Create("app").CreateParameterCatagory<BasicOptions>().WithParameter(x => x.Boolean);

            Assert.NotNull(parameter.GetParameterCatagory<BasicOptions>());
        }

        [Fact]
        public void ParameterBasicArgumentTypeIsNotMultiple()
        {
            IParameter<BasicOptions, bool> parameter =
                ArgumentParser.Create("app").CreateParameterCatagory<BasicOptions>().WithParameter(x => x.Boolean);

            Assert.False(parameter.IsMultiple);
        }

        [Fact]
        public void ParameterEnumerableArgumentTypeIsMultiple()
        {
            IParameter<MultiOptions, string> parameter =
                ArgumentParser.Create("app").CreateParameterCatagory<MultiOptions>().WithMultiParameter(x => x.String);

            Assert.True(parameter.IsMultiple);
        }

        // TODO: Create enum tests for enum, multi enum (non flags), and flags (non multi)
        // TODO: Create exception test for IsMultiple on flags

        [Fact]
        public void ParameterEnumerableTypesNotSupportedExceptIEnumerable()
        {
            Assert.NotNull(ArgumentParser.Create("app").CreateParameterCatagory<MultiOptionsEnumerableType>().WithParameter(x => x.IEnumerable));

            Assert.Throws<ArgumentException>(() =>
                ArgumentParser.Create("app").CreateParameterCatagory<MultiOptionsEnumerableType>().WithParameter(x => x.ICollection));
            Assert.Throws<ArgumentException>(() =>
                ArgumentParser.Create("app").CreateParameterCatagory<MultiOptionsEnumerableType>().WithParameter(x => x.IReadOnlyCollection));
            Assert.Throws<ArgumentException>(() =>
                ArgumentParser.Create("app").CreateParameterCatagory<MultiOptionsEnumerableType>().WithParameter(x => x.IDictionary));
            Assert.Throws<ArgumentException>(() =>
                ArgumentParser.Create("app").CreateParameterCatagory<MultiOptionsEnumerableType>().WithParameter(x => x.Dictionary));
            Assert.Throws<ArgumentException>(() =>
                ArgumentParser.Create("app").CreateParameterCatagory<MultiOptionsEnumerableType>().WithParameter(x => x.IList));
            Assert.Throws<ArgumentException>(() =>
                ArgumentParser.Create("app").CreateParameterCatagory<MultiOptionsEnumerableType>().WithParameter(x => x.List));
        }

        [Fact]
        public void ParameterPropertyWithGetAndSetAccessorsPasses()
        {
            ArgumentParser.Create("app").CreateParameterCatagory<PropertyTypes>().WithParameter(x => x.GetAndSet);
        }

        [Fact]
        public void ParameterWithoutGetOrSetShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                ArgumentParser.Create("app").CreateParameterCatagory<PropertyTypes>().WithParameter(x => x.GetOnly));
            // Build time error. Cannot test anyway
            //Assert.Throws<ArgumentException>(() =>
            //    parser.CreateArgumentCatagory<PropertyTypes>().WithArgument(x => x.SetOnly));
        }
    }
}
