using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace StrukturData
{
    public class TreeNode<T> where T : IComparable<T>
    {
        public T Data { get; set; }
        public TreeNode<T> Left { get; set; }
        public TreeNode<T> Right { get; set; }
        
        public TreeNode(T data)
        {
            Data = data;
            Left = null;
            Right = null;
        }
    }
    
    // Binary Tree 
    public class BinaryTree<T> where T : IComparable<T>
    {
        private TreeNode<T> root;
        
        public BinaryTree()
        {
            root = null;
        }
        
        public void Insert(T data)
        {
            root = InsertRec(root, data);
        }
        
        private TreeNode<T> InsertRec(TreeNode<T> root, T data)
        {
            if (root == null)
            {
                root = new TreeNode<T>(data);
                return root;
            }
            
            if (data.CompareTo(root.Data) < 0)
                root.Left = InsertRec(root.Left, data);
            else if (data.CompareTo(root.Data) > 0)
                root.Right = InsertRec(root.Right, data);
            
            return root;
        }
        
        public bool Search(T data)
        {
            return SearchRec(root, data);
        }
        
        private bool SearchRec(TreeNode<T> root, T data)
        {
            if (root == null)
                return false;
            
            if (data.CompareTo(root.Data) == 0)
                return true;
            
            if (data.CompareTo(root.Data) < 0)
                return SearchRec(root.Left, data);
            
            return SearchRec(root.Right, data);
        }
        
        public void Delete(T data)
        {
            root = DeleteRec(root, data);
        }
        
        private TreeNode<T> DeleteRec(TreeNode<T> root, T data)
        {
            if (root == null)
                return root;
            
            if (data.CompareTo(root.Data) < 0)
                root.Left = DeleteRec(root.Left, data);
            else if (data.CompareTo(root.Data) > 0)
                root.Right = DeleteRec(root.Right, data);
            else
            {
                if (root.Left == null)
                    return root.Right;
                else if (root.Right == null)
                    return root.Left;
                
                root.Data = MinValue(root.Right);
                root.Right = DeleteRec(root.Right, root.Data);
            }
            
            return root;
        }
        
        private T MinValue(TreeNode<T> root)
        {
            T minValue = root.Data;
            while (root.Left != null)
            {
                minValue = root.Left.Data;
                root = root.Left;
            }
            return minValue;
        }
        
        public void InorderTraversal()
        {
            Console.WriteLine("\nInorder Traversal:");
            InorderRec(root);
            Console.WriteLine();
        }
        
        private void InorderRec(TreeNode<T> root)
        {
            if (root != null)
            {
                InorderRec(root.Left);
                Console.Write($"{root.Data} ");
                InorderRec(root.Right);
            }
        }
        
        public void PreorderTraversal()
        {
            Console.WriteLine("\nPreorder Traversal:");
            PreorderRec(root);
            Console.WriteLine();
        }
        
        private void PreorderRec(TreeNode<T> root)
        {
            if (root != null)
            {
                Console.Write($"{root.Data} ");
                PreorderRec(root.Left);
                PreorderRec(root.Right);
            }
        }
        
        public void PostorderTraversal()
        {
            Console.WriteLine("\nPostorder Traversal:");
            PostorderRec(root);
            Console.WriteLine();
        }
        
        private void PostorderRec(TreeNode<T> root)
        {
            if (root != null)
            {
                PostorderRec(root.Left);
                PostorderRec(root.Right);
                Console.Write($"{root.Data} ");
            }
        }
        
        public void Display()
        {
            if (root == null)
            {
                Console.WriteLine("Tree is empty");
                return;
            }
            
            DisplayTree(root, "", true);
        }
        
        private void DisplayTree(TreeNode<T> node, string prefix, bool isLast)
        {
            if (node != null)
            {
                Console.WriteLine($"{prefix}{(isLast ? "└── " : "├── ")}{node.Data}");
                
                var children = new List<TreeNode<T>>();
                if (node.Left != null) children.Add(node.Left);
                if (node.Right != null) children.Add(node.Right);
                
                for (int i = 0; i < children.Count; i++)
                {
                    bool isLastChild = i == children.Count - 1;
                    string childPrefix = prefix + (isLast ? "    " : "│   ");
                    
                    if (children[i] == node.Left)
                        DisplayTree(children[i], childPrefix, children.Count == 1 || node.Right == null);
                    else
                        DisplayTree(children[i], childPrefix, true);
                }
            }
        }
        
        public List<T> GetInorderList()
        {
            var result = new List<T>();
            GetInorderRec(root, result);
            return result;
        }
        
        private void GetInorderRec(TreeNode<T> root, List<T> result)
        {
            if (root != null)
            {
                GetInorderRec(root.Left, result);
                result.Add(root.Data);
                GetInorderRec(root.Right, result);
            }
        }
        
        public void LoadFromList(List<T> data)
        {
            root = null;
            foreach (var item in data)
            {
                Insert(item);
            }
        }
        
        public bool IsEmpty()
        {
            return root == null;
        }
    }
}