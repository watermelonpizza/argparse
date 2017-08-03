using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace argparse.UnitTests
{
    [TestClass]
    public class ArgumentAttributeTests
    {
//        [TestMethod]
//        public void NullNameShouldThrowArgumentException()
//        {
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute(null, typeof(string)));
//        }

//        [TestMethod]
//        public void EmptyNameShouldThrowArgumentException()
//        {
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute(string.Empty, typeof(string)));
//        }

//        [TestMethod]
//        public void SingleCharacterNameShouldThrowArgumentException()
//        {
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("A", typeof(string)));
//        }

//        [TestMethod]
//        public void LowercaseNameShouldPass()
//        {
//            string name = "flag";
//            Assert.AreEqual(name, new ArgumentAttribute(name, typeof(string)).Name);
//        }

//        [TestMethod]
//        public void MixedCaseNameShouldPass()
//        {
//            string name = "Flag";
//            Assert.AreEqual(name, new ArgumentAttribute(name, typeof(string)).Name);
//        }

//        [TestMethod]
//        public void MixedCaseNameWithNumberShouldPass()
//        {
//            string name = "Flag10";
//            Assert.AreEqual(name, new ArgumentAttribute(name, typeof(string)).Name);
//        }

//        [TestMethod]
//        public void NameWithHyphenShouldPass()
//        {
//            string name = "my-flag";
//            Assert.AreEqual(name, new ArgumentAttribute(name, typeof(string)).Name);
//        }

//        [TestMethod]
//        public void NameWithUnderscoreShouldThrowArgumentException()
//        {
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("my_flag", typeof(string)));
//        }

//        [TestMethod]
//        public void NameWithSpecialCharactersShouldThrowArgumentException()
//        {
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag$", typeof(string)));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag#", typeof(string)));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag(", typeof(string)));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag+", typeof(string)));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag=", typeof(string)));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag|", typeof(string)));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag\\", typeof(string)));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag/", typeof(string)));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("$flag", typeof(string)));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("#flag", typeof(string)));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("(flag", typeof(string)));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("+flag", typeof(string)));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("=flag", typeof(string)));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("|flag", typeof(string)));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("\\flag", typeof(string)));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("/flag", typeof(string)));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("fl$ag", typeof(string)));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("fl#ag", typeof(string)));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("fl(ag", typeof(string)));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("fl+ag", typeof(string)));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("fl=ag", typeof(string)));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("fl|ag", typeof(string)));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("fl\\ag", typeof(string)));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("fl/ag", typeof(string)));
//        }

//        [TestMethod]
//        public void StartingLowerLetterNameShouldPass()
//        {
//            string name = "flag";
//            Assert.AreEqual(name, new ArgumentAttribute(name, typeof(string)).Name);
//        }

//        [TestMethod]
//        public void StartingUppercaseLetterNameShouldPass()
//        {
//            string name = "Flag";
//            Assert.AreEqual(name, new ArgumentAttribute(name, typeof(string)).Name);
//        }

//        [TestMethod]
//        public void StartingNumberNameShouldThrowArgumentException()
//        {
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("1flag", typeof(string)));
//        }

//        [TestMethod]
//        public void StartingHyphenNameShouldThrowArgumentException()
//        {
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("-flag", typeof(string)));
//        }

//        [TestMethod]
//        public void SupportedTypesShouldPass()
//        {
//            foreach (Type type in ArgumentParser.SupportedTypes)
//            {
//                Assert.AreEqual(type, new ArgumentAttribute("flag", type).Type);
//            }
//        }

//        [TestMethod]
//        public void UnknownTypesShouldThrowArgumentException()
//        {
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag", typeof(object)));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag", typeof(ArgumentAttributeTests)));
//        }

//        [TestMethod]
//        public void DefaultFlagShouldBe_charMinValue()
//        {
//            Assert.AreEqual(char.MinValue, new ArgumentAttribute("flag", typeof(string)).Flag);
//        }

//        [TestMethod]
//        public void LowercaseFlagShouldPass()
//        {
//            Assert.AreEqual('f', new ArgumentAttribute("flag", typeof(string), flag: 'f').Flag);
//        }

//        [TestMethod]
//        public void UppercaseFlagShouldPass()
//        {
//            Assert.AreEqual('F', new ArgumentAttribute("flag", typeof(string), flag: 'F').Flag);
//        }

//        [TestMethod]
//        public void NumberFlagShouldPass()
//        {
//            Assert.AreEqual('1', new ArgumentAttribute("flag", typeof(string), flag: '1').Flag);
//        }

//        [TestMethod]
//        public void SpecialCharacterFlagShouldThrowArgumentException()
//        {
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag", typeof(string), flag: '$'));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag", typeof(string), flag: '@'));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag", typeof(string), flag: '%'));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag", typeof(string), flag: '-'));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag", typeof(string), flag: '='));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag", typeof(string), flag: '\\'));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag", typeof(string), flag: '/'));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag", typeof(string), flag: '.'));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag", typeof(string), flag: ','));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag", typeof(string), flag: '+'));
//        }

//        [TestMethod]
//        public void NullHelpTextSetsToEmptyString()
//        {
//            Assert.AreEqual(string.Empty, new ArgumentAttribute("flag", typeof(string), help: null).Help);
//        }

//        [TestMethod]
//        public void HelpTextWithEmptyStringShouldPass()
//        {
//            Assert.AreEqual(string.Empty, new ArgumentAttribute("flag", typeof(string), help: string.Empty).Help);
//            Assert.AreEqual("", new ArgumentAttribute("flag", typeof(string), help: "").Help);
//        }

//        [TestMethod]
//        public void HelpTextIsNotManipulated()
//        {
//            string help = "This is a help string";

//            Assert.AreEqual(help, new ArgumentAttribute("flag", typeof(string), help: help).Help);
//        }

//        [TestMethod]
//        public void HelpTextCanBeMultiline()
//        {
//            string help = 
//@"This is a help string
//With multiple
//Lines.";

//            Assert.AreEqual(help, new ArgumentAttribute("flag", typeof(string), help: help).Help);
//        }

//        [TestMethod]
//        public void HelpTextCanHaveNewlineAndEscapeCharacters()
//        {
//            string help =
//@"This is a help string\n
//\t\tWith multiple\r\n
//Lines and \ escape charaters";

//            Assert.AreEqual(help, new ArgumentAttribute("flag", typeof(string), help: help).Help);
//        }

//        [TestMethod]
//        public void CountShouldPassOnType_short()
//        {
//            Assert.IsTrue(new ArgumentAttribute("countableflag", typeof(short), count: true).Count);
//        }

//        [TestMethod]
//        public void CountShouldPassOnType_int()
//        {
//            Assert.IsTrue(new ArgumentAttribute("countableflag", typeof(int), count: true).Count);
//        }

//        [TestMethod]
//        public void CountShouldPassOnType_uint()
//        {
//            Assert.IsTrue(new ArgumentAttribute("countableflag", typeof(uint), count: true).Count);
//        }

//        [TestMethod]
//        public void CountShouldPassOnType_long()
//        {
//            Assert.IsTrue(new ArgumentAttribute("countableflag", typeof(long), count: true).Count);
//        }

//        [TestMethod]
//        public void CountShouldPassOnType_ulong()
//        {
//            Assert.IsTrue(new ArgumentAttribute("countableflag", typeof(ulong), count: true).Count);
//        }

//        [TestMethod]
//        public void CountWithNonNumberTypeShouldThrowArgumentException()
//        {
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag", typeof(string), count: true));
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag", typeof(Enum), count: true));
//        }

//        [TestMethod]
//        public void NullDefaultValueShouldPass()
//        {
//            Assert.IsNull(new ArgumentAttribute("flag", typeof(string), defaultValue: null).DefaultValue);
//        }

//        [TestMethod]
//        public void DefaultValueOfSameTypeAsTypeShouldPass()
//        {
//            Assert.AreEqual("default", new ArgumentAttribute("flag", typeof(string), defaultValue: "default").DefaultValue);
//            Assert.AreEqual(10, new ArgumentAttribute("flag", typeof(int), defaultValue: 10).DefaultValue);
//        }

//        [TestMethod]
//        public void DefaultValueWithDifferentTypeAsTypeShouldThrowArgumentException()
//        {
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag", typeof(int), defaultValue: "default"));
//        }

//        [TestMethod]
//        public void RequiredWithDefaultParametersShouldPass()
//        {
//            Assert.IsTrue(new ArgumentAttribute("flag", typeof(string), required: true).Required);
//        }

//        [TestMethod]
//        public void RequiredAndFallThroughSilentlyBothSetShouldThrowArgumentException()
//        {
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag", typeof(int), required: true, fallThroughSilentlyOnError: true));
//        }

//        [TestMethod]
//        public void FallThroughSilentlyWithDefaultParametersShouldThrowArgumentException()
//        {
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag", typeof(int), fallThroughSilentlyOnError: true));
//        }

//        [TestMethod]
//        public void FallThroughSilentlyAndNullDefaultValueShouldThrowArgumentException()
//        {
//            Assert.ThrowsException<ArgumentException>(() => new ArgumentAttribute("flag", typeof(int), defaultValue: null, fallThroughSilentlyOnError: true));
//        }

//        [TestMethod]
//        public void FallThroughSilentlyAndDefaultValueSetShouldPass()
//        {
//            Assert.IsTrue(new ArgumentAttribute("flag", typeof(int), defaultValue: 1, fallThroughSilentlyOnError: true).FallThroughSilentlyOnError);
//        }
    }
}
