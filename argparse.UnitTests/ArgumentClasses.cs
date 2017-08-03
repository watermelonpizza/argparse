using System;
using System.Collections.Generic;
using System.Text;

namespace argparse.UnitTests
{
    public enum SciFiShows
    {
        Stargate,
        Battlestar,
        Startrek,
        Sancuary
    }

#pragma warning disable IDE1006 // Naming Styles
    public class NoOptions { }

    public class optionsinlowercase { }

    public class OPTIONSINUPPERCASE { }

    public class OptionsInPascalCase { }

    public class optionsInCamelCase { }

    public class OptionsWithTLA { }

    public class TLA { }

    public class Options_With_Underscores { }

    public class options_in_lowercase_with_underscores { }

    public class OPTIONS_IN_UPPERCASE_WITH_UNDERSCORES { }

    public class NameOptions
    {
        public int lowercaseproperty { get; set; }
        public int UPPERCASEPROPERTY { get; set; }
        public int PascalCaseProperty { get; set; }
        public int camelCaseProperty { get; set; }
        public int PropertyWithTLA { get; set; }
        public int TLA { get; set; }
        public int Property_With_Underscores { get; set; }
        public int property_in_lowercase_with_underscores { get; set; }
        public int PROPERTY_IN_UPPERCASE_WITH_UNDERSCORES { get; set; }
        public int PropertyWithNumber1 { get; set; }
    }
#pragma warning restore IDE1006 // Naming Styles

    public class BasicOptions
    {
        public bool Boolean { get; set; }
        public char Character { get; set; }
        public string String { get; set; }
        public SciFiShows Enum { get; set; }
        public DateTime DateTime { get; set; }

        public byte Byte { get; set; }
        public sbyte SignedByte { get; set; }
        public short ShortValue { get; set; }
        public int Integer { get; set; }
        public uint UnsignedInteger { get; set; }
        public long Long { get; set; }
        public ulong UnsignedLong { get; set; }
        public float Float { get; set; }
        public double Double { get; set; }
        public decimal Decimal { get; set; }
    }

    public class MultiOptions
    {
        public IEnumerable<bool> Boolean { get; set; }
        public IEnumerable<char> Character { get; set; }
        public IEnumerable<string> String { get; set; }
        public IEnumerable<SciFiShows> Enum { get; set; }
        public IEnumerable<DateTime> DateTime { get; set; }

        public IEnumerable<byte> Byte { get; set; }
        public IEnumerable<sbyte> SignedByte { get; set; }
        public IEnumerable<short> ShortValue { get; set; }
        public IEnumerable<int> Integer { get; set; }
        public IEnumerable<uint> UnsignedInteger { get; set; }
        public IEnumerable<long> Long { get; set; }
        public IEnumerable<ulong> UnsignedLong { get; set; }
        public IEnumerable<float> Float { get; set; }
        public IEnumerable<double> Double { get; set; }
        public IEnumerable<decimal> Decimal { get; set; }
    }

    public class Commands
    {
        public bool FlagCommand { get; set; }
        public IArgumentParser CommandWithArguments { get; set; }
    }
}
