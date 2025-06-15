using System.Collections.Generic;
using System;
using System.Linq;

namespace StrukturData
{
    public class Node
    {
        public int Value { get; set; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }

        public Node(int value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

    public class BinarySearchTree
    {
        public Node? Root { get; set; }

        public BinarySearchTree()
        {
            Root = null;
        }

        public static Node InsertNode(Node? node, int value)
        {
            if (node == null)
            {
                return new Node(value);
            }

            if (value < node.Value)
            {
                node.Left = InsertNode(node.Left, value);
            }
            else if (value > node.Value) 
            {
                node.Right = InsertNode(node.Right, value);
            }

            return node;
        }

        public static void InOrderTraversal(Node? node)
        {
            if (node == null)
            {
                return;
            }

            if (node.Left != null)
            {
                InOrderTraversal(node.Left);
            }
            Console.Write(node.Value + " ");
            if (node.Right != null)
            {
                InOrderTraversal(node.Right);
            }
        }

        public static Node? GetInOrderSuccessor(Node? node)
        {
            if (node == null) return null;

            Node tmp = node;
            while (tmp.Left != null)
            {
                tmp = tmp.Left;
            }
            return tmp;
        }

        public static Node? DeleteNode(Node? node, int value)
        {
            if (node == null)
            {
                return node;
            }

            if (node.Value > value)
            {
                node.Left = DeleteNode(node.Left, value);
                return node;
            }
            else if (node.Value < value)
            {
                node.Right = DeleteNode(node.Right, value);
                return node;
            }
            else
            {
                if (node.Left == null)
                {
                    return node.Right;
                }
                else if (node.Right == null)
                {
                    return node.Left;
                }
                else
                {
                    Node? inOrderSuccessor = GetInOrderSuccessor(node.Right);
                    if (inOrderSuccessor != null)
                    {
                        node.Value = inOrderSuccessor.Value;
                        node.Right = DeleteNode(node.Right, inOrderSuccessor.Value);
                    }
                    return node;
                }
            }
        }

        public static bool Search(Node? node, int value)
        {
            if (node == null)
                return false;

            if (value == node.Value)
                return true;
            else if (value < node.Value)
                return Search(node.Left, value);
            else
                return Search(node.Right, value);
        }

        public static void DisplayTree(Node? node, string prefix = "", bool isLast = true)
        {
            if (node != null)
            {
                Console.WriteLine($"{prefix}{(isLast ? "└── " : "├── ")}{node.Value}");
                
                if (node.Left != null || node.Right != null)
                {
                    if (node.Left != null)
                        DisplayTree(node.Left, prefix + (isLast ? "    " : "│   "), node.Right == null);
                    
                    if (node.Right != null)
                        DisplayTree(node.Right, prefix + (isLast ? "    " : "│   "), true);
                }
            }
        }
    }
}