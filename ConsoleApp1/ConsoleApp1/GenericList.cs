using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class GenericList<T>
    {
        public void Add(T input) {
            Console.WriteLine(input);
        }
        private class Node
        {
            // T used in non-generic constructor.
            public Node(T t)
            {
                Next = null;
                Data = t;
            }

            public Node Next { get; set; }

            // T as return type of property.
            public T Data { get; set; }
        }

        private Node head;

        // constructor
        public GenericList()
        {
            head = null;
        }

        // T as method parameter type:
        public void AddHead(T t)
        {
            Node n = new Node(t);
            n.Next = head;
            head = n;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node current = head;

            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
    }
}
