using System;
using System.Diagnostics;

namespace Utilities.Extensions
{
    public static class RefListEx
    {
        [Conditional("IncludeInternal")]
        public static void PrintToConsole<T>(this RefList<T> list, string Separator = ",")
        {
            Console.WriteLine(list.ToSeparateString(Separator));
        }
    }
}
