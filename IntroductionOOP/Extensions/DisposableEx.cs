namespace IntroductionOOP.Extensions
{
    public static class DisposableEx
    {
        public static void DisposeAfter<T>(this T value, Action<T> action) where T : IDisposable
        {
            using (value)
                action(value);
        }

        public static TResult DisposeAfter<T, TResult>(this T value, Func<T, TResult> selector) where T : IDisposable
        {
            using (value)
                return selector(value);
        }
    }
}
