using System;
using System.Linq;
using System.Collections.Generic;

namespace N_Ary_Tree_Practice
{
    class Program
    {
        static void Main(string[] args)
        {
            //   N-ARY TREE STRUCTURE
            /*
                        1
                      / | \
                     3  2  4
                    / \     \
                   5   6     8
                  /          /\
                 9         10  11
            */

            //Main Root
            N_AryTree nRoot = new N_AryTree(1);

            //1st Level
            N_AryTree nodeA = new N_AryTree(3);
            N_AryTree nodeB = new N_AryTree(2);
            N_AryTree nodeC = new N_AryTree(4);

            nRoot.Children = new List<N_AryTree> { nodeA, nodeB, nodeC };

            //2nd Level
            N_AryTree nodeD = new N_AryTree(5);
            N_AryTree nodeE = new N_AryTree(6);
            N_AryTree nodeF = new N_AryTree(8);

            nodeA.Children = new List<N_AryTree> { nodeD, nodeE };
            nodeC.Children = new List<N_AryTree> { nodeF };

            //3rd Level
            N_AryTree nodeG = new N_AryTree(9);
            N_AryTree nodeH = new N_AryTree(10);
            N_AryTree nodeI = new N_AryTree(11);

            nodeD.Children = new List<N_AryTree> { nodeG };
            nodeF.Children = new List<N_AryTree> { nodeH, nodeI };

            Console.WriteLine("**N-ARY TREE**");
            //PreOrder Traversals
            string NTreePreOrder = string.Join(",", nRoot.PreOrderTraversal(nRoot));
            Console.WriteLine($"PreOrder Recursive: [{NTreePreOrder}]");
            //(root --> children)
            string NTreePreOrderIter = string.Join(",", nRoot.IterativePreOrderTraversal(nRoot));
            Console.WriteLine($"PreOrder Iterative: [{NTreePreOrderIter}]");

            Console.WriteLine("\r");

            //PostOrder Traversals
            string NTreePostOrder = string.Join(",", nRoot.PostOrderTraversal(nRoot));
            Console.WriteLine($"PostOrder Recursive: [{NTreePostOrder}]");
            //(children --> root)
            string NTreePostOrderIter = string.Join(",", nRoot.IterativePostOrderTraversal(nRoot));
            Console.WriteLine($"PostOrder Iterative: [{NTreePostOrderIter}]");

            Console.WriteLine("\r");

            // Maximum Depth
            Console.WriteLine($"Max Depth: {nRoot.MaxDepth(nRoot)}");

            // Level Order
            List<List<int>> levelOrderResult = nRoot.LevelOrder(nRoot);
            List<string> resultString = new List<string>();

            foreach (var level in levelOrderResult)
            {
                resultString.Add($"[{string.Join(", ", level)}]");
            }
            string combinedLevels = string.Join(", ", resultString);
            Console.WriteLine($"Level Order: [{combinedLevels}]");

            /*
                                   7
                                / | | \
                              10 20 30 40
                              /   \
                            50    60
                           / |      \
                          90 100    70
                                      \
                                      110 
            */

            // Main Root
            N_AryTree nRoot2 = new N_AryTree(7);

            // 1st Level
            N_AryTree node1 = new N_AryTree(10);
            N_AryTree node2 = new N_AryTree(20);
            N_AryTree node3 = new N_AryTree(30);
            N_AryTree node4 = new N_AryTree(40);

            nRoot2.Children = new List<N_AryTree> { node1, node2, node3, node4 };

            // 2nd Level
            N_AryTree node5 = new N_AryTree(50);
            N_AryTree node6 = new N_AryTree(60);

            node1.Children = new List<N_AryTree> { node5};

            // 3rd Level
            N_AryTree node7 = new N_AryTree(90);
            N_AryTree node8 = new N_AryTree(100);
            N_AryTree node9 = new N_AryTree(70);

            node5.Children = new List<N_AryTree> { node7, node8 };
            node2.Children = new List<N_AryTree> { node6 };
            node6.Children = new List<N_AryTree> { node9 };
        }
    }

    public class N_AryTree
    {
        public int Data { get; set; }
        public List<N_AryTree> Children { get; set; }

        public N_AryTree(int data)
        {
            Data = data;
            Children = new List<N_AryTree>();
        }

        //Recursive
        public int[] PreOrderTraversal(N_AryTree root)
        {

            List<int> result = new List<int>();
            if (root == null)
            {
                return result.ToArray();
            }

            result.Add(root.Data);

            if (root.Children != null)
            {
                foreach (var child in root.Children)
                {
                    result.AddRange(PreOrderTraversal(child));
                }
            }
            return result.ToArray();
        }

        //Iterative
        public int[] IterativePreOrderTraversal(N_AryTree root)
        {
            List<int> result = new List<int>();

            if (root == null)
            {
                return result.ToArray();
            }

            Stack<N_AryTree> stack = new Stack<N_AryTree>();
            stack.Push(root);

            while (stack.Count > 0)
            {
                N_AryTree current = stack.Pop();
                result.Add(current.Data);

                for (int i = current.Children.Count - 1; i >= 0; i--)
                {
                    var child = current.Children[i];
                    stack.Push(child);
                }
            }

            return result.ToArray();
        }

        //Recursive
        public int[] PostOrderTraversal(N_AryTree root)
        {
            List<int> result = new List<int>();

            if (root == null)
            {
                return result.ToArray();
            }

            if (root.Children != null)
            {
                foreach (var child in root.Children)
                {
                    result.AddRange(PostOrderTraversal(child));
                }
                result.Add(root.Data);
            }
            return result.ToArray();
        }

        //Iterative
        public int[] IterativePostOrderTraversal(N_AryTree root)
        {
            List<int> result = new List<int>();

            if (root == null)
            {
                return result.ToArray();
            }

            Stack<N_AryTree> stack = new Stack<N_AryTree>();
            stack.Push(root);

            while (stack.Count > 0)
            {
                N_AryTree current = stack.Pop();
                result.Add(current.Data);

                foreach (var child in current.Children)
                {
                    stack.Push(child);
                }
            }
            result.Reverse();
            return result.ToArray();
        }

        public int MaxDepth(N_AryTree root)
        {
            if (root == null)
            {
                return 0;
            }

            if (root.Children == null || root.Children.Count == 0)
            {
                return 1;
            }

            int maxChildDepth = 0;
            foreach (var child in root.Children)
            {
                int childDepth = MaxDepth(child);
                maxChildDepth = Math.Max(maxChildDepth, childDepth);
            }

            return maxChildDepth + 1;
        }

        public List<List<int>> LevelOrder(N_AryTree root)
        {
            if (root == null)
            {
                return new List<List<int>>();
            }

            List<List<int>> result = new List<List<int>>();
            Queue<N_AryTree> queue = new Queue<N_AryTree>();

            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                List<int> levelNodes = new List<int>();
                int levelSize = queue.Count;

                for (int i = 0; i < levelSize; i++)
                {
                    N_AryTree node = queue.Dequeue();
                    levelNodes.Add(node.Data);

                    if (node.Children != null)
                    {
                        foreach (var child in node.Children)
                        {
                            queue.Enqueue(child);
                        }
                    }
                }
                result.Add(levelNodes);
            }

            return result;
        }
    }
}
