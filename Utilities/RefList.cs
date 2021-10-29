using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Utilities
{
    [DebuggerDisplay("RefList[items:{Count}]")]
    public class RefList<T>
    {
        [DebuggerDisplay("Node = {Value}")]
        public class Node
        {
            internal RefList<T> _List;

            public T Value { get; set; }

            public Node Next { get; internal set; }

            public Node Prev { get; internal set; }

            internal Node(RefList<T> List,T Value)
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

        public RefList() { }

        public RefList(IEnumerable<T> items)
        {
            foreach (var item in items)
                AddLast(item);
        }

        public Node AddFirst(T value)
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

        public Node AddLast(T value)
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

        public Node AddAfter(Node Position, T value)
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

        public Node AddBefore(Node Position, T value)
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

        public T Remove(Node node)
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

        public void Clear()
        {
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
        }
    }
}
