using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace argparse.UnitTests
{
    public class CommandTests
    {
        [Fact]
        public void CommandDefaultName_PascalCase()
        {
            ICommand command = ArgumentParser.Create("app").CreateCommandCatagory<NameOptions>().WithCommand(x => x.PascalCaseProperty);
            Assert.Equal("pascalcaseproperty", command.CommandName);
        }

        [Fact]
        public void CommandDefaultName_LowerCase()
        {
            ICommand command = ArgumentParser.Create("app").CreateCommandCatagory<NameOptions>().WithCommand(x => x.lowercaseproperty);
            Assert.Equal("lowercaseproperty", command.CommandName);
        }

        [Fact]
        public void CommandDefaultName_UpperCase()
        {
            ICommand command = ArgumentParser.Create("app").CreateCommandCatagory<NameOptions>().WithCommand(x => x.UPPERCASEPROPERTY);
            Assert.Equal("uppercaseproperty", command.CommandName);
        }

        [Fact]
        public void CommandDefaultName_CamelCase()
        {
            ICommand command = ArgumentParser.Create("app").CreateCommandCatagory<NameOptions>().WithCommand(x => x.camelCaseProperty);
            Assert.Equal("camelcaseproperty", command.CommandName);
        }

        [Fact]
        public void CommandDefaultName_EndingTLA()
        {
            ICommand command = ArgumentParser.Create("app").CreateCommandCatagory<NameOptions>().WithCommand(x => x.PropertyWithEndingTLA);
            Assert.Equal("propertywithendingtla", command.CommandName);
        }

        [Fact]
        public void CommandDefaultName_TLAInIt()
        {
            ICommand command = ArgumentParser.Create("app").CreateCommandCatagory<NameOptions>().WithCommand(x => x.PropertyWithTLAInIt);
            Assert.Equal("propertywithtlainit", command.CommandName);
        }

        [Fact]
        public void CommandDefaultName_OnlyTLA()
        {
            ICommand command = ArgumentParser.Create("app").CreateCommandCatagory<NameOptions>().WithCommand(x => x.TLA);
            Assert.Equal("tla", command.CommandName);
        }

        [Fact]
        public void CommandDefaultName_UnderscoresThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => ArgumentParser.Create("app").CreateCommandCatagory<NameOptions>().WithCommand(x => x.Property_With_Underscores));
        }

        [Fact]
        public void CommandDefaultName_LowerCaseUnderscoresThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => ArgumentParser.Create("app").CreateCommandCatagory<NameOptions>().WithCommand(x => x.property_in_lowercase_with_underscores));
        }

        [Fact]
        public void CommandDefaultName_UppercaseUnderscoresThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => ArgumentParser.Create("app").CreateCommandCatagory<NameOptions>().WithCommand(x => x.PROPERTY_IN_UPPERCASE_WITH_UNDERSCORES));
        }

        [Fact]
        public void CommandDefaultName_NumberInIt()
        {
            ICommand command = ArgumentParser.Create("app").CreateCommandCatagory<NameOptions>().WithCommand(x => x.PropertyWithNumber1InIt);
            Assert.Equal("propertywithnumber1init", command.CommandName);
        }

        [Fact]
        public void CommandDefaultName_EndingNumber()
        {
            ICommand command = ArgumentParser.Create("app").CreateCommandCatagory<NameOptions>().WithCommand(x => x.PropertyWithNumberEnding1);
            Assert.Equal("propertywithnumberending1", command.CommandName);
        }

        [Fact]
        public void CommandDefaultName_Number()
        {
            ICommand command = ArgumentParser.Create("app").CreateCommandCatagory<NameOptions>().WithCommand(x => x.PropertyWithTLA1Number);
            Assert.Equal("propertywithtla1number", command.CommandName);
        }

        [Theory]
        [InlineData("config")]
        [InlineData("a")]
        [InlineData("ipv4")]
        [InlineData("4")]
        [InlineData("1234")]
        [InlineData("12command")]
        [InlineData("command12")]
        public void CommandNameMethodAllowedNames(string commandName)
        {
            ICommand<NameOptions> command =
                ArgumentParser.Create("app").CreateCommandCatagory<NameOptions>().WithCommand(x => x.PascalCaseProperty).Name(commandName);

            Assert.Equal(commandName, command.CommandName);
        }

        [Theory]
        [InlineData("under_score")]
        [InlineData("symbol!")]
        [InlineData("#")]
        [InlineData("a)")]
        [InlineData("🍉")]
        [InlineData("!a")]
        [InlineData("🍉-myname")]
        [InlineData("-abc")]
        [InlineData("abc bbb")]
        [InlineData("-abc bbb")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void CommandNameMethodDissallowedNamesShouldThrowCommandException(string CommandName)
        {
            ICommand<NameOptions> command =
                ArgumentParser.Create("app").CreateCommandCatagory<NameOptions>().WithCommand(x => x.PascalCaseProperty);

            Assert.Throws<ArgumentException>(() => command.Name(CommandName));
        }

        [Fact]
        public void CommandWithCommandReturnsANewCommand()
        {
            ICommand<Commands> command =
                ArgumentParser.Create("app").CreateCommandCatagory<Commands>().WithCommand(x => x.FlagCommand);

            Assert.NotSame(command, command.WithCommand(x => x.SecondFlagCommand));
        }

        [Fact]
        public void CommandGetCatagoryReturnsCatagory()
        {
            ICommand<BasicOptions> command =
                ArgumentParser.Create("app").CreateCommandCatagory<BasicOptions>().WithCommand(x => x.Boolean);

            Assert.IsType<BasicOptions>(command.GetCommandCatagory<BasicOptions>());
        }

        [Fact]
        public void CommandGetCatagoryNotNull()
        {
            ICommand<BasicOptions> command =
                ArgumentParser.Create("app").CreateCommandCatagory<BasicOptions>().WithCommand(x => x.Boolean);

            Assert.NotNull(command.GetCommandCatagory<BasicOptions>());
        }

        [Fact]
        public void CommandPropertyWithGetAndSetAccessorsPasses()
        {
            ArgumentParser.Create("app").CreateCommandCatagory<PropertyTypes>().WithCommand(x => x.GetAndSet);
        }

        [Fact]
        public void CommandWithoutGetOrSetShouldThrowCommandException()
        {
            Assert.Throws<ArgumentException>(() =>
                ArgumentParser.Create("app").CreateCommandCatagory<PropertyTypes>().WithCommand(x => x.GetOnly));
            // Build time error. Cannot test anyway
            //Assert.Throws<CommandException>(() =>
            //    parser.CreateCommandCatagory<PropertyTypes>().WithCommand(x => x.SetOnly));
        }

        [Fact]
        public void CommandArgumentsAreSparateFromBaseArgumentParser()
        {
            ArgumentParser parser = ArgumentParser.Create("app");
            parser.CreateArgumentCatagory<BasicOptions>().WithArgument(x => x.String).Name("basename");
            parser.CreateCommandCatagory<Commands>().WithCommand(x => x.CommandWithArguments, (cp) => cp.CreateArgumentCatagory<BasicOptions>().WithArgument(x => x.String).Name("basename"));

            Assert.NotSame(parser.GetCommandCatagory<Commands>().CommandWithArguments, parser);
            Assert.NotSame(parser.GetArgumentCatagory<BasicOptions>(), parser.GetCommandCatagory<Commands>().CommandWithArguments.GetCommandCatagory<BasicOptions>());
        }
    }
}
