using System;
using System.Collections.Generic;

namespace argparse.UnitTests
{
        public class ArgumentAttributeTests
    {
//        [Fact]
//        public void NullNameShouldThrowArgumentException()
//        {
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute(null, typeof(string)));
//        }

//        [Fact]
//        public void EmptyNameShouldThrowArgumentException()
//        {
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute(string.Empty, typeof(string)));
//        }

//        [Fact]
//        public void SingleCharacterNameShouldThrowArgumentException()
//        {
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("A", typeof(string)));
//        }

//        [Fact]
//        public void LowercaseNameShouldPass()
//        {
//            string name = "flag";
//            Assert.Equal(name, new ArgumentAttribute(name, typeof(string)).Name);
//        }

//        [Fact]
//        public void MixedCaseNameShouldPass()
//        {
//            string name = "Flag";
//            Assert.Equal(name, new ArgumentAttribute(name, typeof(string)).Name);
//        }

//        [Fact]
//        public void MixedCaseNameWithNumberShouldPass()
//        {
//            string name = "Flag10";
//            Assert.Equal(name, new ArgumentAttribute(name, typeof(string)).Name);
//        }

//        [Fact]
//        public void NameWithHyphenShouldPass()
//        {
//            string name = "my-flag";
//            Assert.Equal(name, new ArgumentAttribute(name, typeof(string)).Name);
//        }

//        [Fact]
//        public void NameWithUnderscoreShouldThrowArgumentException()
//        {
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("my_flag", typeof(string)));
//        }

//        [Fact]
//        public void NameWithSpecialCharactersShouldThrowArgumentException()
//        {
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag$", typeof(string)));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag#", typeof(string)));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag(", typeof(string)));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag+", typeof(string)));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag=", typeof(string)));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag|", typeof(string)));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag\\", typeof(string)));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag/", typeof(string)));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("$flag", typeof(string)));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("#flag", typeof(string)));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("(flag", typeof(string)));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("+flag", typeof(string)));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("=flag", typeof(string)));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("|flag", typeof(string)));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("\\flag", typeof(string)));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("/flag", typeof(string)));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("fl$ag", typeof(string)));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("fl#ag", typeof(string)));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("fl(ag", typeof(string)));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("fl+ag", typeof(string)));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("fl=ag", typeof(string)));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("fl|ag", typeof(string)));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("fl\\ag", typeof(string)));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("fl/ag", typeof(string)));
//        }

//        [Fact]
//        public void StartingLowerLetterNameShouldPass()
//        {
//            string name = "flag";
//            Assert.Equal(name, new ArgumentAttribute(name, typeof(string)).Name);
//        }

//        [Fact]
//        public void StartingUppercaseLetterNameShouldPass()
//        {
//            string name = "Flag";
//            Assert.Equal(name, new ArgumentAttribute(name, typeof(string)).Name);
//        }

//        [Fact]
//        public void StartingNumberNameShouldThrowArgumentException()
//        {
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("1flag", typeof(string)));
//        }

//        [Fact]
//        public void StartingHyphenNameShouldThrowArgumentException()
//        {
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("-flag", typeof(string)));
//        }

//        [Fact]
//        public void SupportedTypesShouldPass()
//        {
//            foreach (Type type in ArgumentParser.SupportedTypes)
//            {
//                Assert.Equal(type, new ArgumentAttribute("flag", type).Type);
//            }
//        }

//        [Fact]
//        public void UnknownTypesShouldThrowArgumentException()
//        {
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag", typeof(object)));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag", typeof(ArgumentAttributeTests)));
//        }

//        [Fact]
//        public void DefaultFlagShouldBe_charMinValue()
//        {
//            Assert.Equal(char.MinValue, new ArgumentAttribute("flag", typeof(string)).Flag);
//        }

//        [Fact]
//        public void LowercaseFlagShouldPass()
//        {
//            Assert.Equal('f', new ArgumentAttribute("flag", typeof(string), flag: 'f').Flag);
//        }

//        [Fact]
//        public void UppercaseFlagShouldPass()
//        {
//            Assert.Equal('F', new ArgumentAttribute("flag", typeof(string), flag: 'F').Flag);
//        }

//        [Fact]
//        public void NumberFlagShouldPass()
//        {
//            Assert.Equal('1', new ArgumentAttribute("flag", typeof(string), flag: '1').Flag);
//        }

//        [Fact]
//        public void SpecialCharacterFlagShouldThrowArgumentException()
//        {
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag", typeof(string), flag: '$'));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag", typeof(string), flag: '@'));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag", typeof(string), flag: '%'));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag", typeof(string), flag: '-'));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag", typeof(string), flag: '='));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag", typeof(string), flag: '\\'));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag", typeof(string), flag: '/'));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag", typeof(string), flag: '.'));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag", typeof(string), flag: ','));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag", typeof(string), flag: '+'));
//        }

//        [Fact]
//        public void NullHelpTextSetsToEmptyString()
//        {
//            Assert.Equal(string.Empty, new ArgumentAttribute("flag", typeof(string), help: null).Help);
//        }

//        [Fact]
//        public void HelpTextWithEmptyStringShouldPass()
//        {
//            Assert.Equal(string.Empty, new ArgumentAttribute("flag", typeof(string), help: string.Empty).Help);
//            Assert.Equal("", new ArgumentAttribute("flag", typeof(string), help: "").Help);
//        }

//        [Fact]
//        public void HelpTextIsNotManipulated()
//        {
//            string help = "This is a help string";

//            Assert.Equal(help, new ArgumentAttribute("flag", typeof(string), help: help).Help);
//        }

//        [Fact]
//        public void HelpTextCanBeMultiline()
//        {
//            string help = 
//@"This is a help string
//With multiple
//Lines.";

//            Assert.Equal(help, new ArgumentAttribute("flag", typeof(string), help: help).Help);
//        }

//        [Fact]
//        public void HelpTextCanHaveNewlineAndEscapeCharacters()
//        {
//            string help =
//@"This is a help string\n
//\t\tWith multiple\r\n
//Lines and \ escape charaters";

//            Assert.Equal(help, new ArgumentAttribute("flag", typeof(string), help: help).Help);
//        }

//        [Fact]
//        public void CountShouldPassOnType_short()
//        {
//            Assert.True(new ArgumentAttribute("countableflag", typeof(short), count: true).Count);
//        }

//        [Fact]
//        public void CountShouldPassOnType_int()
//        {
//            Assert.True(new ArgumentAttribute("countableflag", typeof(int), count: true).Count);
//        }

//        [Fact]
//        public void CountShouldPassOnType_uint()
//        {
//            Assert.True(new ArgumentAttribute("countableflag", typeof(uint), count: true).Count);
//        }

//        [Fact]
//        public void CountShouldPassOnType_long()
//        {
//            Assert.True(new ArgumentAttribute("countableflag", typeof(long), count: true).Count);
//        }

//        [Fact]
//        public void CountShouldPassOnType_ulong()
//        {
//            Assert.True(new ArgumentAttribute("countableflag", typeof(ulong), count: true).Count);
//        }

//        [Fact]
//        public void CountWithNonNumberTypeShouldThrowArgumentException()
//        {
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag", typeof(string), count: true));
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag", typeof(Enum), count: true));
//        }

//        [Fact]
//        public void NullDefaultValueShouldPass()
//        {
//            Assert.IsNull(new ArgumentAttribute("flag", typeof(string), defaultValue: null).DefaultValue);
//        }

//        [Fact]
//        public void DefaultValueOfSameTypeAsTypeShouldPass()
//        {
//            Assert.Equal("default", new ArgumentAttribute("flag", typeof(string), defaultValue: "default").DefaultValue);
//            Assert.Equal(10, new ArgumentAttribute("flag", typeof(int), defaultValue: 10).DefaultValue);
//        }

//        [Fact]
//        public void DefaultValueWithDifferentTypeAsTypeShouldThrowArgumentException()
//        {
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag", typeof(int), defaultValue: "default"));
//        }

//        [Fact]
//        public void RequiredWithDefaultParametersShouldPass()
//        {
//            Assert.True(new ArgumentAttribute("flag", typeof(string), required: true).Required);
//        }

//        [Fact]
//        public void RequiredAndFallThroughSilentlyBothSetShouldThrowArgumentException()
//        {
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag", typeof(int), required: true, fallThroughSilentlyOnError: true));
//        }

//        [Fact]
//        public void FallThroughSilentlyWithDefaultParametersShouldThrowArgumentException()
//        {
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag", typeof(int), fallThroughSilentlyOnError: true));
//        }

//        [Fact]
//        public void FallThroughSilentlyAndNullDefaultValueShouldThrowArgumentException()
//        {
//            Assert.Throws<ArgumentException>(() => new ArgumentAttribute("flag", typeof(int), defaultValue: null, fallThroughSilentlyOnError: true));
//        }

//        [Fact]
//        public void FallThroughSilentlyAndDefaultValueSetShouldPass()
//        {
//            Assert.True(new ArgumentAttribute("flag", typeof(int), defaultValue: 1, fallThroughSilentlyOnError: true).FallThroughSilentlyOnError);
//        }
    }
}
