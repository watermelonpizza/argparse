using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace argparse.UnitTests
{
    public class ConsoleOutputHook : TextWriter
    {
        public List<string> Output { get; } = new List<string>();
        public StringBuilder RawString { get; } = new StringBuilder();

        public override Encoding Encoding => Encoding.Unicode;

        public override void Write(char value)
        {
            throw new Exception("Don't use write char");
        }

        public override void Write(string value)
        {
            RawString.Append(value);
            AppendToLast(value);
        }

        public override void WriteLine()
        {
            RawString.AppendLine();
            Output.Add(string.Empty);
        }

        public override void WriteLine(string value)
        {
            RawString.AppendLine(value);
            Output.AddRange(value.Split(new string[] { NewLine }, StringSplitOptions.None));
        }

        private void AppendToLast(string append)
        {
            if (Output.Any())
            {
                Output[Output.Count - 1] = Output.Last() + append;
            }
            else
            {
                Output.Add(append);
            }
        }
    }
}
