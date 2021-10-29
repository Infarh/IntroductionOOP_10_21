using System;
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


            RefList<int>.ListCreated += (s, e) => Console.WriteLine(string.Join(",", e.Arg));
            //RefList<string>.ListCreated += (s, e) => Console.WriteLine(string.Join(",", e.Arg));

            //RefList<int> list = new(Enumerable.Range(1, 10));
            var list = RefList<int>.CreateIntRange(1, 10);



            var list_str = string.Join(", ", list);

            var target = list.First.Next.Next;

            var after_node = list.AddAfter(target, 0);
            var list_str2 = string.Join(", ", list);

            var before_node = list.AddBefore(target, -1);
            var list_str3 = string.Join(", ", list);

            //var int_ref_list = new IntList();

           
        }
    }

    //class IntList : RefList<int>
    //{
    //    private readonly List<int> _AddedLastValues = new();

    //    public sealed override Node AddLast(int value)
    //    {
    //        var node = base.AddLast(value);
    //        _AddedLastValues.Add(value);
    //        return node;
    //    }
    //}

    //internal class IntList2 : IntList
    //{
    //    public new Node AddLast(int value) { return base.AddLast(value); }
    //}
}
