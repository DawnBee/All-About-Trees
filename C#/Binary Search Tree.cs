using System;
using System.Linq;
using System.Collections.Generic;


namespace Binary_Search_Tree_Practice
{
    class Program
    {
        static void Main(string[] args)
        {
            // Binary Search Tree
            /* 
             
                          5
             			 / \
            			3   6
             		   / \   \
              		  2   4   8
             		 /		  /\
             		1   	 7	9	 
            */


            // Main Root
            BSTree root = new BSTree(5);

            // 1st Level
            root.Left = new BSTree(3);
            root.Right = new BSTree(6);

            // 2nd Level
            root.Left.Left = new BSTree(2);
            root.Left.Right = new BSTree(4);
            root.Right.Right = new BSTree(8);

            // 3rd Level
            root.Left.Left.Left = new BSTree(1);
            root.Right.Right.Left = new BSTree(7);
            root.Right.Right.Right = new BSTree(9);

            // Aligns all nodes to right (a.k.a LinkedList)
            Console.WriteLine(root.IncreasingBST(root));


            /*   Basic Search 
                   
                        9
                       / \
                      5   14
                     / \    \
                    3   7    18
                             / \
                            16  25
            */

            // Main Root
            BSTree findRoot = new BSTree(9);

            // 1st Level
            findRoot.Left = new BSTree(5);
            findRoot.Right = new BSTree(14);

            // 2nd Level
            findRoot.Left.Left = new BSTree(3);
            findRoot.Left.Right = new BSTree(7);
            findRoot.Right.Right = new BSTree(18);

            // 3rd Level
            findRoot.Right.Right.Left = new BSTree(16);
            findRoot.Right.Right.Right = new BSTree(25);

            BSTree newTree = findRoot.SearchBST(findRoot, 18);
            newTree.DisplayTree();


            /*      Find Modes
                
                         6
                        / \
                       4   9
                      / \   \
                     4   5   9
                    /    /    \
                   4    3      9
            */

            // Main Root
            BSTree rootModes = new BSTree(6);

            // 1st Level
            rootModes.Left = new BSTree(4);
            rootModes.Right = new BSTree(9);

            // 2nd Level
            rootModes.Left.Left = new BSTree(4);
            rootModes.Left.Right = new BSTree(5);
            rootModes.Right.Right = new BSTree(9);

            // 3rd Level
            rootModes.Left.Left.Left = new BSTree(4);
            rootModes.Left.Right.Left = new BSTree(3);
            rootModes.Right.Right.Right = new BSTree(9);

            // Returns nodes that appeared most
            Dictionary<int, int> freqCount = new Dictionary<int, int>();
            int maxFrequency = 0;
            string combinedModes = string.Join(",", rootModes.FindMode(freqCount, maxFrequency));
            Console.WriteLine($"Tree Modes: [{combinedModes}]");

            // Minimum Difference
            Console.WriteLine($"Min Diff: {rootModes.GetMinimumDiff(rootModes)}");

            // Lowest Common Ancestor
            BSTree p = rootModes.Left.Left.Left;
            BSTree q = rootModes.Left.Right.Left;
            Console.WriteLine($"LCA: {rootModes.LowestCommonAncestor(rootModes, p, q)}");
            Console.WriteLine("\r");

            /*      Delete a Node
      
                         5
                        / \
                       3   6
                      / \   \
                     2   4   7  
            */

            // Main Root
            BSTree rootDel = new BSTree(5);

            // 1st Level
            rootDel.Left = new BSTree(3);
            rootDel.Right = new BSTree(6);

            // 2nd Level
            rootDel.Left.Left = new BSTree(2);
            rootDel.Left.Right = new BSTree(4);
            rootDel.Right.Right = new BSTree(7);

            // Node Deletion
            Console.WriteLine("Before Deletion:");
            rootDel.DisplayTree();
            Console.WriteLine("\r");

            rootDel.DeleteNode(rootDel, 3);

            Console.WriteLine("After Deletion:");
            rootDel.DisplayTree();
            Console.WriteLine("\r");

            // Converts Sorted Array to BST
            int[] nums = { -10, -3, 0, 5, 9 };
            string combinedNums = string.Join(",", nums);
            BSTree numsRoot = SortedArrayToBST(nums);
            Console.WriteLine($"From Sorted Array: [{combinedNums}]");
            numsRoot.DisplayTree();

        }

        public static BSTree SortedArrayToBST(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return null;
            }

            BSTree ConstructBST(int left, int right)
            {
                if (left > right)
                {
                    return null;
                }

                int mid = left + (right - left) / 2;
                BSTree root = new BSTree(nums[mid]);

                root.Left = ConstructBST(left, mid - 1);
                root.Right = ConstructBST(mid + 1, right);

                return root;
            }

            return ConstructBST(0, nums.Length - 1);
        }
    }

    public class BSTree
    {
        public int Data { get; set; }
        public BSTree Left { get; set; }
        public BSTree Right { get; set; }

        public BSTree (int data)
        {
            Data = data;
            Left = null;
            Right = null;
        }

        public void DisplayTree(int level = 0, string prefix = "Root: ")
        {
            if (this != null)
            {
                if (level == 0)
                {
                    Console.WriteLine($"{prefix}{this.Data}");
                }
                else
                {
                    string line = new string(' ', level * 4) + "|-- ";
                    if (prefix.StartsWith("L: "))
                    {
                        line = new string(' ', level * 4) + "|--L: ";
                    }
                    else if (prefix.StartsWith("R: "))
                    {
                        line = new string(' ', level * 4) + "|--R: ";
                    }
                    Console.WriteLine(line + this.Data);
                }

                if (this.Left != null)
                {
                    this.Left.DisplayTree(level + 1, "L: ");
                }
                if (this.Right != null)
                {
                    this.Right.DisplayTree(level + 1, "R: ");
                }
            }
        }

        public BSTree IncreasingBST(BSTree root)
        {
            List<BSTree> InOrderTraversal(BSTree node)
            {
                List<BSTree> result = new List<BSTree>();

                if (node == null)
                {
                    return result;
                }

                result.AddRange(InOrderTraversal(node.Left));
                result.Add(node);
                result.AddRange(InOrderTraversal(node.Right));

                return result;
            }
            List<BSTree> sortedNodes = InOrderTraversal(root);

            BSTree newRoot = new BSTree(sortedNodes[0].Data);
            BSTree currentNode = newRoot;

            for (int i = 1; i < sortedNodes.Count; i++)
            {
                currentNode.Right = new BSTree(sortedNodes[i].Data);
                currentNode = currentNode.Right;
            }

            while (newRoot != null)
            {
                if (newRoot.Right == null)
                {
                    Console.WriteLine(newRoot.Data);
                }
                else
                {
                    Console.Write($"{newRoot.Data} --> ");
                }
                newRoot = newRoot.Right;
            }
            return null;
        }

        public BSTree SearchBST(BSTree root, int data)
        {
            if (root == null)
            {
                return root;
            }

            if (root.Data == data)
            {
                return root;
            }
            else if (data < root.Data) 
            {
                return SearchBST(root.Left, data);
            }
            else
            {
                return SearchBST(root.Right, data);
            }
        }

        public List<int> FindMode(Dictionary<int, int> freqCount, int maxFrequency)
        {
            List<int> modes = new List<int>();

            void InOrderTraversal(BSTree node)
            {
                if (node == null)
                {
                    return;
                }

                InOrderTraversal(node.Left);

                int value = node.Data;
                freqCount[value] = freqCount.TryGetValue(value, out int count) ? count + 1 : 1;

                maxFrequency = Math.Max(maxFrequency, freqCount[value]);

                InOrderTraversal(node.Right);
            }

            InOrderTraversal(this);

            foreach (var kv in freqCount)
            {
                if (kv.Value == maxFrequency)
                {
                    modes.Add(kv.Key);
                }
            }

            return modes;
        }

        public int GetMinimumDiff(BSTree root)
        {
            void InOrderTraversal(BSTree node, ref int prev, ref int minDiff)
            {
                if (node == null)
                {
                    return;
                }

                InOrderTraversal(node.Left, ref prev, ref minDiff);

                if (prev != int.MinValue) // Check for initial value
                {
                    minDiff = Math.Min(minDiff, Math.Abs(node.Data - prev));
                }

                prev = node.Data;
                InOrderTraversal(node.Right, ref prev, ref minDiff);
            }

            int prev = int.MinValue; // Use a sentinel value instead of null
            int minDiff = int.MaxValue;
            InOrderTraversal(this, ref prev, ref minDiff);

            return minDiff;
        }

        public int LowestCommonAncestor(BSTree root, BSTree p, BSTree q)
        {
            while (root != null)
            {
                if (p.Data < root.Data && q.Data < root.Data)
                {
                    root =  root.Left;
                }
                else if (p.Data > root.Data && q.Data > root.Data) 
                {
                    root = root.Right;
                }
                else
                {
                    return root.Data;
                }
            }
            return 0;
        }

        public BSTree FindMin(BSTree node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }

        public BSTree DeleteNode(BSTree root, int key)
        {
            if (root == null)
            {
                return root;
            }

            if (key < root.Data)
            {
                root.Left = DeleteNode(root.Left, key);
            }
            else if (key > root.Data)
            {
                root.Right = DeleteNode(root.Right, key);
            }
            else
            {
                if (root.Left == null)
                {
                    return root.Right;
                }
                else if (root.Right == null)
                {
                    return root.Left;
                }

                BSTree minNode = FindMin(root.Right);
                root.Data = minNode.Data;
                root.Right = DeleteNode(root.Right, minNode.Data);
            }
            return root;
        }
    }
}
