using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StrukturData
{
    public class Node
    {
        public string Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(string value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

    public class Binary1
    {
        public Node Root { get; set; }

        public Binary1()
        {
            Root = null;
        }

        public void Insert(string value)
        {
            Root = InsertNode(Root, value);
        }

        private Node InsertNode(Node node, string value)
        {
            if (node == null)
                return new Node(value);

            if (string.Compare(value, node.Value) < 0)
                node.Left = InsertNode(node.Left, value);
            else if (string.Compare(value, node.Value) > 0)
                node.Right = InsertNode(node.Right, value);

            return node;
        }

        public void InOrderTraversal()
        {
            InOrderTraversal(Root);
        }

        private void InOrderTraversal(Node node)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left);
                Console.WriteLine(node.Value);
                InOrderTraversal(node.Right);
            }
        }
    }
}
