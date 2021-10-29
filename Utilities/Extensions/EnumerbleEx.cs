using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities.Extensions
{
    public static class EnumerbleEx
    {
        public static string ToSeparateString<T>(this IEnumerable<T> items, string Separator = ", ")
        {
            return string.Join(Separator, items);
        }
    }
}
