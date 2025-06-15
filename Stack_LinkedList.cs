using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace StrukturData
{
    public class StackNode
    {
        public string data;
        public StackNode next;

        public StackNode(string data)
        {
            this.data = data;
            this.next = null;
        }
    }

    public class Stack1
    {
        private StackNode top;

        public Stack1()
        {
            this.top = null;
        }

        public void Push(string item)
        {
            StackNode newNode = new StackNode(item);
            newNode.next = top;
            top = newNode;
        }

        public string Pop()
        {
            if (top == null)
            {
                Console.WriteLine("Stack kosong");
                return null;
            }
            string item = top.data;
            top = top.next;
            return item;
        }

        public bool IsEmpty()
        {
            return top == null;
        }

        public void PrintAll()
        {
            StackNode current = top;
            while (current != null)
            {
                Console.WriteLine(current.data);
                current = current.next;
            }
        }

        public void Clear()
        {
            top = null;
        }
    }
}
