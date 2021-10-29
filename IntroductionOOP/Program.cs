using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace IntroductionOOP
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<int> list1 = new();
            var node = list1.AddFirst(42);
            node.Value = 7;


            RefList<int> list = new(Enumerable.Range(1, 10));

            var target = list.First.Next.Next;

            var after_node = list.AddAfter(target, 0);
            var before_node = list.AddBefore(target, -1);
        }
    }
}
