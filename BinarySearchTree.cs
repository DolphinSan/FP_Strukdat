using System.Collections.Generic;
using System;
using System.Linq;
namespace StrukturData
{
    // Binary Search Tree

   

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
            else
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
            InOrderTraversal(node.Right);
        }


        public static Node GetInOrderSuccessor(Node node)
        {

            Node tmp = node;
            while (tmp.Left != null)
            {
                tmp = tmp.Left;
            }
            return tmp;

        }

        public static Node? DeleteNode(Node? node, int value)
        {
            //cari node yang akan dihapus
            //hapus node sesuai dengan konsep tree


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
                    //cari inorder successor dari substree sebelah kanan node
                    Node? InOrderSuccessor = GetInOrderSuccessor(node.Right);
                    node.Value = InOrderSuccessor.Value;
                    node.Right = DeleteNode(node.Right, InOrderSuccessor.Value);
                    return node;



                    //kita cari inorder sucessor 
                    //kita rubah nilai node yang akan kita hapus dengan nilai inorder sucessor
                    //kita hapus inorder successornya


                }
            }
        }


    }
}


