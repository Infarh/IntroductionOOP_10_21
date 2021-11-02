using System;

namespace Utilities.Extensions
{
    public static class ArrayExtensions
    {
        public static void Deconstruct<T>(this T[] array, out T v1, out T v2)
        {
            if (array.Length < 2) throw new ArgumentException("Длина массива меньше 2");
            v1 = array[0];
            v2 = array[1];
        }

        public static void Deconstruct<T>(this T[] array, out T v1, out T v2, out T v3)
        {
            if (array.Length < 3) throw new ArgumentException("Длина массива меньше 3");
            v1 = array[0];
            v2 = array[1];
            v3 = array[2];
        }
    }
}
