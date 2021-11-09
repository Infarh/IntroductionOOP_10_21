using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Utilities
{
    [DebuggerDisplay("RefList[items:{Count}]")]
    public /*sealed*/ class RefList<T> : IEnumerable<T>
    {
        //static RefList()
        //{
        //    __Default = new();
        //}

        private static /*readonly*/ volatile RefList<T> __Default;
        private static readonly object __DefaultLock = new();

        public RefList<T> Default
        {
            get
            {
                if (__Default != null) return __Default;
                lock (__DefaultLock)
                {
                    if (__Default != null) return __Default;
                    __Default = new();
                    return __Default;
                }
            }
        }

        public static event EventHandler<EventArgs<RefList<T>>> ListCreated;

        public static RefList<T> Create() => new();

        public static RefList<T> Create(IEnumerable<T> items) => new(items);

        public static RefList<T> Create(T item, int RepeatCount) => new(Enumerable.Repeat(item, RepeatCount));

        public static RefList<int> CreateIntRange(int Start, int Count) => new(Enumerable.Range(Start, Count));

        [DebuggerDisplay("Node = {Value}")]
        public class Node
        {
            internal RefList<T> _List;

            public T Value { get; set; }

            public Node Next { get; internal set; }

            public Node Prev { get; internal set; }

            internal Node(RefList<T> List, T Value)
            {
                _List = List;
                this.Value = Value;
            }

            //public override string ToString() => Value?.ToString();
        }

        private int _Count;

        public Node First { get; private set; }

        public Node Last { get; private set; }

        public int Count => _Count;

        private RefList()
        {
            ListCreated?.Invoke(null, this);
        }

        private RefList(IEnumerable<T> items)
        {
            foreach (var item in items)
                AddLast(item);

            ListCreated?.Invoke(null, this);
        }

        public virtual Node AddFirst(T value)
        {
            var node = new Node(this, value);

            _Count++;

            if (First is null)
            {
                First = node;
                Last = node;
                return node;
            }

            node.Next = First;
            First.Prev = node;
            First = node;

            return node;
        }

        public virtual Node AddLast(T value)
        {
            var node = new Node(this, value);

            _Count++;

            if (Last is null)
            {
                First = node;
                Last = node;
                return node;
            }

            node.Prev = Last;
            Last.Next = node;
            Last = node;

            return node;
        }

        public virtual Node AddAfter(Node Position, T value)
        {
            if (!ReferenceEquals(Position._List, this))
                throw new InvalidOperationException("Произведена попытка добавления элемента в список после узла, принадлежащего другому списку");

            if (ReferenceEquals(Position, Last))
                return AddLast(value);

            var node = new Node(this, value)
            {
                Prev = Position,
                Next = Position.Next,
            };

            Position.Next = node;
            node.Next.Prev = node;

            return node;
        }

        public virtual Node AddBefore(Node Position, T value)
        {
            if (!ReferenceEquals(Position._List, this))
                throw new InvalidOperationException("Произведена попытка добавления элемента в список до узла, принадлежащего другому списку");

            if (ReferenceEquals(Position, Last))
                return AddLast(value);

            var node = new Node(this, value)
            {
                Next = Position,
                Prev = Position.Prev,
            };

            Position.Prev = node;
            node.Prev.Next = node;

            return node;
        }

        public virtual T Remove(Node node)
        {
            if (!ReferenceEquals(node._List, this))
                throw new InvalidOperationException("Произведена попытка удаления узла, принадлежащего другому списку");

            node._List = null;

            if (_Count == 1)
            {
                First = null;
                Last = null;
            }
            else if (ReferenceEquals(First, node))
            {
                First = node.Next;
                First.Prev = null;
            }
            else if (ReferenceEquals(Last, node))
            {
                Last = node.Prev;
                Last.Next = null;
            }
            else
            {
                node.Prev.Next = node.Next;
                node.Next.Prev = node.Prev;
            }

            node.Next = null;
            node.Prev = null;

            _Count--;
            return node.Value;
        }

        [Conditional("IncludeInternal")]
        public virtual void Clear()
        {
            Console.WriteLine("Clear list");

            if (_Count == 0) return;
            var node = First;
            while (node != null)
            {
                node._List = null;
                node.Prev = null;
                var tmp = node;
                node = node.Next;
                tmp.Next = null;
            }

            First = null;
            Last = null;
            _Count = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (_Count == 0) yield break;
            var node = First;
            while (node != null)
            {
                yield return node.Value;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
