using System.Collections.Generic;

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
