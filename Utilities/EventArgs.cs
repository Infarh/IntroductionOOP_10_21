namespace Utilities
{
    public class EventArgs<T>
    {
        public T Arg { get; set; }

        public EventArgs()
        {
            
        }

        public EventArgs(T arg)
        {
            Arg = arg;
        }

        public static implicit operator T(EventArgs<T> e) => e.Arg;

        public static implicit operator EventArgs<T>(T value) => new(value);
    }
}
