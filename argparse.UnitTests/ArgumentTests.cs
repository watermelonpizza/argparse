﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace argparse.UnitTests
{
    public class ArgumentTests
    {
        [Fact]
        public void ArgumentDefaultName_PascalCase()
        {
            IArgument argument = ArgumentParser.Create("app").CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty);
            Assert.Equal("pascal-case-property", argument.ArgumentName);
        }

        [Fact]
        public void ArgumentDefaultName_LowerCase()
        {
            IArgument argument = ArgumentParser.Create("app").CreateArgumentCatagory<NameOptions>().WithArgument(x => x.lowercaseproperty);
            Assert.Equal("lowercaseproperty", argument.ArgumentName);
        }

        [Fact]
        public void ArgumentDefaultName_UpperCase()
        {
            IArgument argument = ArgumentParser.Create("app").CreateArgumentCatagory<NameOptions>().WithArgument(x => x.UPPERCASEPROPERTY);
            Assert.Equal("uppercaseproperty", argument.ArgumentName);
        }

        [Fact]
        public void ArgumentDefaultName_CamelCase()
        {
            IArgument argument = ArgumentParser.Create("app").CreateArgumentCatagory<NameOptions>().WithArgument(x => x.camelCaseProperty);
            Assert.Equal("camel-case-property", argument.ArgumentName);
        }

        [Fact]
        public void ArgumentDefaultName_EndingTLA()
        {
            IArgument argument = ArgumentParser.Create("app").CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PropertyWithEndingTLA);
            Assert.Equal("property-with-ending-tla", argument.ArgumentName);
        }

        [Fact]
        public void ArgumentDefaultName_TLAInIt()
        {
            IArgument argument = ArgumentParser.Create("app").CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PropertyWithTLAInIt);
            Assert.Equal("property-with-tla-in-it", argument.ArgumentName);
        }

        [Fact]
        public void ArgumentDefaultName_OnlyTLA()
        {
            IArgument argument = ArgumentParser.Create("app").CreateArgumentCatagory<NameOptions>().WithArgument(x => x.TLA);
            Assert.Equal("tla", argument.ArgumentName);
        }

        [Fact]
        public void ArgumentDefaultName_Underscores()
        {
            IArgument argument = ArgumentParser.Create("app").CreateArgumentCatagory<NameOptions>().WithArgument(x => x.Property_With_Underscores);
            Assert.Equal("property-with-underscores", argument.ArgumentName);
        }

        [Fact]
        public void ArgumentDefaultName_LowerCaseUnderscores()
        {
            IArgument argument = ArgumentParser.Create("app").CreateArgumentCatagory<NameOptions>().WithArgument(x => x.property_in_lowercase_with_underscores);
            Assert.Equal("property-in-lowercase-with-underscores", argument.ArgumentName);
        }

        [Fact]
        public void ArgumentDefaultName_UppercaseUnderscores()
        {
            IArgument argument = ArgumentParser.Create("app").CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PROPERTY_IN_UPPERCASE_WITH_UNDERSCORES);
            Assert.Equal("property-in-uppercase-with-underscores", argument.ArgumentName);
        }

        [Fact]
        public void ArgumentDefaultName_NumberInIt()
        {
            IArgument argument = ArgumentParser.Create("app").CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PropertyWithNumber1InIt);
            Assert.Equal("property-with-number-1-in-it", argument.ArgumentName);
        }

        [Fact]
        public void ArgumentDefaultName_EndingNumber()
        {
            IArgument argument = ArgumentParser.Create("app").CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PropertyWithNumberEnding1);
            Assert.Equal("property-with-number-ending-1", argument.ArgumentName);
        }

        [Fact]
        public void ArgumentDefaultName_Number()
        {
            IArgument argument = ArgumentParser.Create("app").CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PropertyWithTLA1Number);
            Assert.Equal("property-with-tla-1-number", argument.ArgumentName);
        }

        [Theory]
        [InlineData("name")]
        [InlineData("custom-name")]
        [InlineData("ipv4")]
        [InlineData("ip-v6")]
        [InlineData("1234")]
        [InlineData("12things")]
        public void ArgumentNameMethodAllowedNames(string argumentName)
        {
            IArgument<NameOptions, bool> argument =
                ArgumentParser.Create("app").CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty).Name(argumentName);

            Assert.Equal(argumentName, argument.ArgumentName);
        }

        [Theory]
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
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ArgumentNameMethodDissallowedNamesShouldThrowArgumentException(string argumentName)
        {
            IArgument<NameOptions, bool> argument =
                ArgumentParser.Create("app").CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty);

            Assert.Throws<ArgumentException>(() => argument.Name(argumentName));
        }

        [Theory]
        [InlineData('a')]
        [InlineData('A')]
        [InlineData('4')]
        public void ArgumentFlagMethodAllowedNames(char flag)
        {
            IArgument<NameOptions, bool> argument =
                ArgumentParser.Create("app").CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty).Flag(flag);

            Assert.Equal(flag, argument.ArgumentFlag);
        }


        [Theory]
        [InlineData('!')]
        [InlineData('~')]
        [InlineData(';')]
        [InlineData('"')]
        [InlineData('<')]
        [InlineData('.')]
        [InlineData('-')]
        [InlineData('?')]
        [InlineData(' ')]
        public void ArgumentFlagMethodDissallowedFlagsShouldThrowArgumentException(char flag)
        {
            IArgument<NameOptions, bool> argument =
                ArgumentParser.Create("app").CreateArgumentCatagory<NameOptions>().WithArgument(x => x.PascalCaseProperty);

            Assert.Throws<ArgumentException>(() => argument.Flag(flag));
        }

        [Fact]
        public void ArgumentRequiredMethodsSetsArgumentToRequired()
        {
            IArgument<BasicOptions, bool> argument =
                ArgumentParser.Create("app").CreateArgumentCatagory<BasicOptions>().WithArgument(x => x.Boolean).Required();

            Assert.True(argument.IsRequired);
        }

        [Fact]
        public void ArgumentWithArgumentReturnsANewArgument()
        {
            IArgument<BasicOptions, bool> argument =
                ArgumentParser.Create("app").CreateArgumentCatagory<BasicOptions>().WithArgument(x => x.Boolean);

            Assert.NotSame(argument, argument.WithArgument(x => x.Byte));
        }

        [Fact]
        public void ArgumentGetCatagoryReturnsCatagory()
        {
            IArgument<BasicOptions, bool> argument =
                ArgumentParser.Create("app").CreateArgumentCatagory<BasicOptions>().WithArgument(x => x.Boolean);

            Assert.IsType<BasicOptions>(argument.GetArgumentCatagory<BasicOptions>());
        }

        [Fact]
        public void ArgumentGetCatagoryNotNull()
        {
            IArgument<BasicOptions, bool> argument =
                ArgumentParser.Create("app").CreateArgumentCatagory<BasicOptions>().WithArgument(x => x.Boolean);

            Assert.NotNull(argument.GetArgumentCatagory<BasicOptions>());
        }

        [Fact]
        public void ArgumentBasicArgumentTypeIsNotMultiple()
        {
            IArgument<BasicOptions, bool> argument =
                ArgumentParser.Create("app").CreateArgumentCatagory<BasicOptions>().WithArgument(x => x.Boolean);

            Assert.False(argument.IsMultiple);
        }

        [Fact]
        public void ArgumentMultiArgumentTypeIsMultiple()
        {
            IArgument<MultiOptions, string> argument =
                ArgumentParser.Create("app").CreateArgumentCatagory<MultiOptions>().WithMultiArgument(x => x.String);

            Assert.True(argument.IsMultiple);
        }

        [Fact]
        public void ArgumentEnumArgumentTypeIsEnum()
        {
            IArgument<BasicOptions, SciFiShows> argument =
                ArgumentParser.Create("app").CreateArgumentCatagory<BasicOptions>().WithArgument(x => x.Enum);

            Assert.True(argument.IsEnum);
        }

        [Fact]
        public void ArgumentFlaggableEnumArgumentTypeIsFlags()
        {
            IArgument<BasicOptions, FlaggableEnum> argument =
                ArgumentParser.Create("app").CreateArgumentCatagory<BasicOptions>().WithArgument(x => x.FlaggableEnum);

            Assert.True(argument.IsFlags);
        }

        [Fact]
        public void Argument_FlaggableEnum_NotAllowedToBeMuliArgument()
        {
            Assert.Throws<ArgumentException>(() => 
                ArgumentParser.Create("app").CreateArgumentCatagory<MultiOptions>().WithMultiArgument(x => x.FlaggableEnum));
        }

        // TODO: Create enum tests for enum, multi enum (non flags), and flags (non multi)
        // TODO: Create exception test for IsMultiple on flags

        [Fact]
        public void ArgumentCollectionTypesNotSupportedExceptImmutableArray()
        {
            Assert.NotNull(ArgumentParser.Create("app").CreateArgumentCatagory<MultiOptionsEnumerableType>().WithArgument(x => x.ImmutableArray));

            Assert.Throws<ArgumentException>(() =>
                ArgumentParser.Create("app").CreateArgumentCatagory<MultiOptionsEnumerableType>().WithArgument(x => x.IEnumerable));
            Assert.Throws<ArgumentException>(() =>
                ArgumentParser.Create("app").CreateArgumentCatagory<MultiOptionsEnumerableType>().WithArgument(x => x.ICollection));
            Assert.Throws<ArgumentException>(() =>
                ArgumentParser.Create("app").CreateArgumentCatagory<MultiOptionsEnumerableType>().WithArgument(x => x.IReadOnlyCollection));
            Assert.Throws<ArgumentException>(() =>
                ArgumentParser.Create("app").CreateArgumentCatagory<MultiOptionsEnumerableType>().WithArgument(x => x.IDictionary));
            Assert.Throws<ArgumentException>(() =>
                ArgumentParser.Create("app").CreateArgumentCatagory<MultiOptionsEnumerableType>().WithArgument(x => x.Dictionary));
            Assert.Throws<ArgumentException>(() =>
                ArgumentParser.Create("app").CreateArgumentCatagory<MultiOptionsEnumerableType>().WithArgument(x => x.IList));
            Assert.Throws<ArgumentException>(() =>
                ArgumentParser.Create("app").CreateArgumentCatagory<MultiOptionsEnumerableType>().WithArgument(x => x.List));
        }

        [Fact]
        public void ArgumentPropertyWithGetAndSetAccessorsPasses()
        {
            ArgumentParser.Create("app").CreateArgumentCatagory<PropertyTypes>().WithArgument(x => x.GetAndSet);
        }

        [Fact]
        public void ArgumentHelpMethodSetsHelp()
        {
            IArgument<BasicOptions, string> parameter =
                ArgumentParser.Create("app").CreateArgumentCatagory<BasicOptions>().WithArgument(x => x.String).Help("help");

            Assert.Equal("help", parameter.ArgumentHelp);
        }

        [Fact]
        public void ArgumentWithoutGetOrSetShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                ArgumentParser.Create("app").CreateArgumentCatagory<PropertyTypes>().WithArgument(x => x.GetOnly));
            // Build time error. Cannot test anyway
            //Assert.Throws<ArgumentException>(() =>
            //    parser.CreateArgumentCatagory<PropertyTypes>().WithArgument(x => x.SetOnly));
        }
    }
}