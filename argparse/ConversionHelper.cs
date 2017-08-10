using System;
using System.Collections.Generic;
using System.Text;

namespace argparse
{
    public static class ConversionHelper
    {
        public static object Convert(this string str, Type type)
        {
            try
            {
                if (type == typeof(DateTime))
                {
                    if (DateTime.TryParse(str, out DateTime datetime))
                        return datetime;
                    else if (long.TryParse(str, out long ticks))
                        return new DateTime(ticks);
                    else
                        throw new Exception();
                }
                else
                {
                    return System.Convert.ChangeType(str, type);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
