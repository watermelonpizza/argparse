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

    public class OptionsWithEndingTLA { }

    public class OptionsWithTLAInIt { }

    public class TLA { }

    public class Options_With_Underscores { }

    public class options_in_lowercase_with_underscores { }

    public class OPTIONS_IN_UPPERCASE_WITH_UNDERSCORES { }

    public class OptionsWithNumber1InIt { }

    public class OptionsWithNumberEnding1 { }

    public class OptionsWithTLA1Number { }

    public class NameOptions
    {
        public bool lowercaseproperty { get; set; }
        public bool UPPERCASEPROPERTY { get; set; }
        public bool PascalCaseProperty { get; set; }
        public bool camelCaseProperty { get; set; }
        public bool PropertyWithEndingTLA { get; set; }
        public bool PropertyWithTLAInIt { get; set; }
        public bool TLA { get; set; }
        public bool Property_With_Underscores { get; set; }
        public bool property_in_lowercase_with_underscores { get; set; }
        public bool PROPERTY_IN_UPPERCASE_WITH_UNDERSCORES { get; set; }
        public bool PropertyWithNumber1InIt { get; set; }
        public bool PropertyWithNumberEnding1 { get; set; }
        public bool PropertyWithTLA1Number { get; set; }

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

    public class MultiOptionsEnumerableType
    {
        public IEnumerable<int> IEnumerable { get; set; }
        public ICollection<int> ICollection { get; set; }
        public IReadOnlyCollection<int> IReadOnlyCollection { get; set; }
        public IList<int> IList { get; set; }
        public List<int> List { get; set; }
        public IDictionary<int, int> IDictionary { get; set; }
        public Dictionary<int, int> Dictionary { get; set; }
    }

    public class PropertyTypes
    {
        public bool GetAndSet { get; set; }
        public bool GetOnly { get; }
        public bool SetOnly { set { } }
        public bool GetPrivateSet { get; private set; }
        public bool PrivateGetSet { private get; set; }
    }

    public class Commands
    {
        public bool FlagCommand { get; set; }
        public bool SecondFlagCommand { get; set; }
        public ICommandArgumentParser CommandWithArguments { get; set; }
        public ICommandArgumentParser SecondCommandWithArguments { get; set; }
    }
}