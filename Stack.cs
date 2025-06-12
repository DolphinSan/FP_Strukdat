using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace StrukturData
{
    // Stack
    public class CustomStack<T>
    {
        private List<T> items;

        public CustomStack()
        {
            items = new List<T>();
        }

        public void Push(T item)
        {
            items.Add(item);
        }

        public T Pop()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Stack is empty");

            T item = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            return item;
        }

        public T Peek()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Stack is empty");

            return items[items.Count - 1];
        }

        public bool IsEmpty()
        {
            return items.Count == 0;
        }

        public int Size()
        {
            return items.Count;
        }

        public void Display()
        {
            Console.WriteLine("\n=== Stack  ===");
            if (IsEmpty())
            {
                Console.WriteLine("Stack is empty");
                return;
            }

            Console.WriteLine("Top");
            Console.WriteLine(" ↓");
            for (int i = items.Count - 1; i >= 0; i--)
            {
                Console.WriteLine($"[{items[i]}]");
            }
            Console.WriteLine("Bottom");
        }

        public List<T> GetItems()
        {
            return new List<T>(items);
        }

        public void LoadFromList(List<T> data)
        {
            items = new List<T>(data);
        }
    }
}