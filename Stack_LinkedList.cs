using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace StrukturData
{



    public class Stack_LinkedList
    {
        private StackNode top;

        public Stack_LinkedList()
        {
            this.top = null;
        }

        public void Push(int item)
        {
            StackNode newNode = new StackNode(item);
            newNode.next = top;
            top = newNode;
        }

        public int Pop()
        {
            if (top == null)
            {
                Console.WriteLine("Stack kosong");
                return -1;
            }
            else
            {
                int item = top.data;
                top = top.next;
                return item;
            }
        }

        public int Peek()
        {
            if (top == null)
            {
                Console.WriteLine("Stack kosong");
                return -1;
            }
            else
            {
                return top.data;
            }
        }

        public bool IsEmpty()
        {
            return top == null;
        }
    }
}