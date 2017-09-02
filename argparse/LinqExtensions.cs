using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace argparse
{
    public static class LinqExtensions
    {
        public static void Each<T>(this IEnumerable<T> collection, Action<T> func)
        {
            foreach (var obj in collection)
            {
                func(obj);
            }
        }
    }
}
