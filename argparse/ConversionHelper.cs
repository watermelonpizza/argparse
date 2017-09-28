using System;
using System.Collections.Immutable;
using System.Reflection;

namespace argparse
{
    internal static class ConversionHelper
    {
        internal static object Convert(this string str, Type type) => Convert(str as object, type);
        internal static object Convert(this object obj, Type type)
        {
            try
            {
                if (type == typeof(DateTime))
                {
                    if (DateTime.TryParse(obj as string, out DateTime datetime))
                        return datetime;
                    else if (long.TryParse(obj as string, out long ticks))
                        return new DateTime(ticks);
                    else
                        throw new Exception();
                }
                else if (type.GetTypeInfo().IsEnum)
                {
                    return Enum.Parse(type, obj as string, true);
                }
                else
                {
                    return System.Convert.ChangeType(obj, type);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
