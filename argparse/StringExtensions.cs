using System;
using System.Collections.Generic;
using System.Text;

namespace argparse
{
    public static class StringExtensions
    {
        public static int Count(this string str, char character)
        {
            int count = 0;
            int length = str.Length;
            for (int n = length - 1; n >= 0; n--)
            {
                if (str[n] == character)
                    count++;
            }

            return count;
        }

        public static int Count(this string str, string match)
        {
            char c = match[0]; 
            int count = 0;
            int length = str.Length;
            for (int n = length - 1; n >= 0; n--)
            {
                if (str[n] == c)
                    count++;
            }

            return count;
        }
    }
}
