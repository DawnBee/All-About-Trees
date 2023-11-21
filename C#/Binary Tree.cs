using System;
using System.Linq;
using System.Collections.Generic;

namespace Binary_Tree_Practice
{
    class Program
    {
        static void Main(string[] args)
        {
            //  BINARY TREE STRUCTURE
            /*
                    5
                   / \
                  6   8
                   \   \
                    9   11
                   / \
                  14  15 
            */

            //Main Root
            TreeNode root = new TreeNode(5);

            //1st Level
            root.Left = new TreeNode(6);
            root.Right = new TreeNode(8);

            //2nd Level
            root.Left.Right = new TreeNode(9);
            root.Right.Right = new TreeNode(11);

            //3rd Level
            root.Left.Right.Left = new TreeNode(14);
            root.Left.Right.Right = new TreeNode(15);


            Console.WriteLine("**BINARY TREE**");
            //Inorder Traversals
            string treeInOrder = string.Join(",", root.InorderTraversal(root));
            Console.WriteLine($"InOrder Recursive: [{treeInOrder}]");
            //  (left --> root --> right)
            string treeInorderIter = string.Join(",", root.IterativeInorderTraversal(root));
            Console.WriteLine($"InOrder Iterative: [{treeInorderIter}]");

            Console.WriteLine("\r");

            //PreOrder Traversals
            string treePreOrder = string.Join(",", root.PreOrderTraversal(root));
            Console.WriteLine($"PreOrder Recursive: [{treePreOrder}]");
            // (root --> left --> right)
            string treePreOrderIter = string.Join(",", root.IterativePreOrderTraversal(root));
            Console.WriteLine($"PreOrder Iterative: [{treePreOrderIter}]");

            Console.WriteLine("\r");

            //PostOrder Traversals
            string treePostOrder = string.Join(",", root.PostOrderTraversal(root));
            Console.WriteLine($"PostOrder Recursive: [{treePostOrder}]");
            // (left --> right --> root)
            string treePostOrderIter = string.Join(",", root.IterativePostOrderTraversal(root));
            Console.WriteLine($"PostOrder Iterative: [{treePostOrderIter}]");

            Console.WriteLine("\n");

            // Comparisons
            // P - Tree
            TreeNode pRoot = new TreeNode(1);
            pRoot.Left = new TreeNode(2);
            pRoot.Right = new TreeNode(3);

            // Q - Tree
            TreeNode qRoot = new TreeNode(1);
            qRoot.Left = new TreeNode(2);
            qRoot.Right = new TreeNode(3);
            Console.WriteLine(pRoot.IsSameTree(pRoot, qRoot));


            // Is Symmetric?
            /*
                           1
                 		 /   \
                 	    2     2
                      /  \   /  \
                     3   4  4    3             
            */

            // Main Root
            TreeNode symmRoot = new TreeNode(1);

            // 1st Level
            symmRoot.Left = new TreeNode(2);
            symmRoot.Right = new TreeNode(2);

            // 2nd Level
            symmRoot.Left.Left = new TreeNode(3);
            symmRoot.Left.Right = new TreeNode(4);
            symmRoot.Right.Right = new TreeNode(3);
            symmRoot.Right.Left = new TreeNode(4);

            Console.WriteLine($"Symmetric: {symmRoot.IsSymmetric(symmRoot)}");
            Console.WriteLine($"IsBalanced: {symmRoot.IsBalanced(symmRoot)}");
            Console.WriteLine($"Minimum Depth: {symmRoot.MinDepth(symmRoot)}");


            // Path Sum
            /*
                        5
                       / \
                      4   8
                     /   / \
                    11  13  4
                   /  \      \
                  7    2      1            
            */

            // Main Root
            TreeNode rootSum = new TreeNode(5);

            // 1st Level
            rootSum.Left = new TreeNode(4);
            rootSum.Right = new TreeNode(8);

            // 2nd Level
            rootSum.Left.Left = new TreeNode(11);
            rootSum.Right.Left = new TreeNode(13);
            rootSum.Right.Right = new TreeNode(4);

            // 3rd Level
            rootSum.Left.Left.Left = new TreeNode(7);
            rootSum.Left.Left.Right = new TreeNode(2);
            rootSum.Right.Right.Right = new TreeNode(1);

            Console.WriteLine(rootSum.HasPathSum(rootSum, 22));

            string combinedLargest = string.Join(",",rootSum.LargestValues(rootSum));
            Console.WriteLine($"[{combinedLargest}]");

            string combinedPaths = string.Join(", ", rootSum.BinaryTreePaths(rootSum));
            Console.WriteLine($"[{combinedPaths}]");


            //  Complete Tree
            /*
                      1
                     / \
            	 	2 	3
            	   /\  /
             	  4  5 6
            */

            // Main Root
            TreeNode completeRoot = new TreeNode(1);

            // 1st Level
            completeRoot.Left = new TreeNode(2);
            completeRoot.Right = new TreeNode(3);

            // 2nd Level
            completeRoot.Left.Left = new TreeNode(4);
            completeRoot.Left.Right = new TreeNode(5);
            completeRoot.Right.Left = new TreeNode(6);

            // Count Nodes of a Complete Tree
            Console.WriteLine($"Nodes: {completeRoot.CountNodes(completeRoot)}");

            // Sum of all Left leaf nodes
            Console.WriteLine($"Left Leaves Sum: {completeRoot.SumOfLeftLeaves(completeRoot)}");

            // Level Order
            List<List<int>> levelOrderResult = completeRoot.LevelOrder(completeRoot);
            List<string> resultString = new List<string>();
            
            foreach (var level in levelOrderResult)
            {
                resultString.Add($"[{string.Join(", ", level)}]");
            }
            string combinedLevels = string.Join(", ", resultString);
            Console.WriteLine($"Level Order Traversal: [{combinedLevels}]");
        }
    }

    public class TreeNode
    {
        public int Data { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }

        public TreeNode(int data)
        {
            Data = data;
            Left = null;
            Right = null;
        }

        //Recursive
        public int[] InorderTraversal(TreeNode root)
        {
            List<int> result = new List<int>();

            if (root == null)
            {
                return result.ToArray();
            }

            if (root.Left != null)
            {
                result.AddRange(InorderTraversal(root.Left));
            }

            result.Add(root.Data);

            if (root.Right != null)
            {
                result.AddRange(InorderTraversal(root.Right));
            }

            return result.ToArray();
        }

        //Iterative
        public int[] IterativeInorderTraversal(TreeNode root)
        {
            List<int> result = new List<int>();

            if (root == null)
            {
                return result.ToArray();
            }

            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode current = root;

            while (current != null || stack.Count > 0)
            {
                while (current != null)
                {
                    stack.Push(current);
                    current = current.Left;
                }

                current = stack.Pop();
                result.Add(current.Data);
                current = current.Right;
            }
            return result.ToArray();
        }

        //Recursive
        public int[] PreOrderTraversal(TreeNode root)
        {
            List<int> result = new List<int>();
            if (root == null)
            {
                return result.ToArray();
            }

            result.Add(root.Data);

            if (root.Left != null)
            {
                result.AddRange(PreOrderTraversal(root.Left));
            }

            if (root.Right != null)
            {
                result.AddRange(PreOrderTraversal(root.Right));
            }

            return result.ToArray();
        }

        //Iterative
        public int[] IterativePreOrderTraversal(TreeNode root)
        {
            List<int> result = new List<int>();

            if (root == null)
            {
                return result.ToArray();
            }

            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode current = root;

            while (current != null || stack.Count > 0)
            {
                while (current != null)
                {
                    result.Add(current.Data);
                    stack.Push(current);
                    current = current.Left;
                }

                current = stack.Pop();
                current = current.Right;
            }
            return result.ToArray();
        }

        //Recursive
        public int[] PostOrderTraversal(TreeNode root)
        {
            List<int> result = new List<int>();

            if (root == null)
            {
                return result.ToArray();
            }

            if (root.Left != null)
            {
                result.AddRange(PostOrderTraversal(root.Left));
            }

            if (root.Right != null)
            {
                result.AddRange(PostOrderTraversal(root.Right));
            }

            result.Add(root.Data);

            return result.ToArray();
        }

        //Iterative
        public int[] IterativePostOrderTraversal(TreeNode root)
        {
            List<int> result = new List<int>();

            if (root == null)
            {
                return result.ToArray();
            }

            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode current = root;
            TreeNode prev = null;

            while (current != null || stack.Count > 0)
            {
                while (current != null)
                {
                    stack.Push(current);
                    current = current.Left;
                }

                current = stack.Peek();

                if (current.Right == null || current.Right == prev)
                {
                    result.Add(current.Data);
                    stack.Pop();
                    prev = current;
                    current = null;
                }
                else
                {
                    current = current.Right;
                }
            }
            return result.ToArray();
        }

        public bool IsSameTree(TreeNode p, TreeNode q)
        {
            if (p == null && q == null)
            {
                return true;
            }

            if (p == null || q == null)
            {
                return false;
            }
            return p.Data == q.Data && IsSameTree(p.Left, q.Left) && IsSameTree(p.Right, q.Right);
        }

        public bool IsSymmetric(TreeNode root)
        {
            bool IsMirror(TreeNode left, TreeNode right)
            {
                if (left == null && right == null)
                {
                    return true;
                }
                if (left == null || right == null)
                {
                    return false;
                }
                return (left.Data == right.Data) && IsMirror(left.Left, right.Right) && IsMirror(left.Right, right.Left);
            }
            if (root == null)
            {
                return true;
            }
            return IsMirror(root.Left, root.Right);
        }

        public bool IsBalanced(TreeNode root)
        {
            Tuple<bool, int> CheckBalance(TreeNode node)
            {
                // An empty tree is balanced and has a height of 0.
                if (node == null)
                {
                    return new Tuple<bool, int>(true, 0);
                }

                // Check if the left subtree is balanced and get its height.
                bool isLeftBalanced = CheckBalance(node.Left).Item1;
                int leftHeight = CheckBalance(node.Left).Item2;

                if (isLeftBalanced == false)
                {
                    return new Tuple<bool, int>(false, 0);
                    // If the left subtree is not balanced, the whole tree is unbalanced.
                }

                bool isRightBalanced = CheckBalance(node.Right).Item1;
                int rightHeight = CheckBalance(node.Right).Item2;

                if (isRightBalanced == false)
                {
                    return new Tuple<bool, int>(false, 0);
                }

                bool isCurrentBalanced = Math.Abs(leftHeight - rightHeight) <= 1;
                int currentHeight = Math.Max(leftHeight, rightHeight) + 1;

                return new Tuple<bool, int> (isCurrentBalanced, currentHeight);
            }

            bool isBalanced = CheckBalance(root).Item1;
            return isBalanced;
        }

        // MinDepth till we reach a Leaf Node
        public int MinDepth (TreeNode root)
        {
            // Breadth-First Search
            if (root == null)
            {
                return 0;
            }
            Queue<Tuple<TreeNode, int>> queue = new Queue<Tuple<TreeNode, int>>();
            queue.Enqueue(new Tuple<TreeNode, int>(root, 1));

            while (queue.Count != 0)
            {
                var current = queue.Dequeue();
                TreeNode node = current.Item1;
                int depth = current.Item2;

                if (node.Left == null && node.Right == null)
                {
                    return depth;
                }

                if (node.Left != null)
                {
                    queue.Enqueue(new Tuple<TreeNode, int>(node.Left, depth + 1));
                }

                if (node.Right != null)
                {
                    queue.Enqueue(new Tuple<TreeNode, int>(node.Right, depth + 1));
                }
            }
            return 0;
        }

        public bool HasPathSum(TreeNode root, int targetSum)
        {
            if (root == null)
            {
                return false;
            }

            targetSum -= root.Data;

            if (root.Left == null && root.Right == null)
            {
                return targetSum == 0;
            }
            return HasPathSum(root.Left, targetSum) || HasPathSum(root.Right, targetSum);
        }

        public int[] LargestValues(TreeNode root)
        {
            List<int> result = new List<int>();

            if (root == null)
            {
                return result.ToArray();
            }

            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                float levelMax = float.NegativeInfinity;
                int levelSize = queue.Count;

                for (int i = 0; i < levelSize; i++)
                {
                    TreeNode node = queue.Dequeue();
                    levelMax = Math.Max(levelMax, node.Data);

                    if (node.Left != null)
                    {
                        queue.Enqueue(node.Left);
                    }

                    if (node.Right != null) 
                    {
                        queue.Enqueue(node.Right);
                    }
                }
                result.Add((int)levelMax);
            }
            return result.ToArray();
        }

        public string[] BinaryTreePaths(TreeNode root)
        {
            List<string> result = new List<string>();

            void dfs(TreeNode node, List<string> currentPath)
            {
                if (node == null)
                {
                    return;
                }

                currentPath.Add(node.Data.ToString());

                if (node.Left == null && node.Right == null)
                {
                    result.Add(string.Join("-> ", currentPath));
                }

                dfs(node.Left, currentPath);
                dfs(node.Right, currentPath);

                currentPath.RemoveAt(currentPath.Count - 1);
            }

            List<string> current = new List<string>();
            dfs(root, current);

            return result.ToArray();
        }

        public int CountNodes(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            int depthLeft = 0;
            int depthRight = 0;
            TreeNode left = root;
            TreeNode right = root;

            while (left != null)
            {
                left = left.Left;
                depthLeft++;
            }

            while (right != null)
            {
                right = right.Right;
                depthRight++;
            }

            if (depthLeft == depthRight)
            {
                return (int)(Math.Pow(2, depthLeft) - 1);
            }
            else
            {
                int leftCount;
                if (root.Left != null) {
                    leftCount = CountNodes(root.Left);
                } else {
                    leftCount = 0;
                }

                int rightCount;
                if (root.Right != null) {
                    rightCount = CountNodes(root.Right);
                } else {
                    rightCount = 0;
                }
                return 1 + leftCount + rightCount;
            }
        }

        public int SumOfLeftLeaves(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            int leftSum = 0;

            if (root.Left != null && root.Left.Left == null && root.Left.Right == null)
            {
                leftSum += root.Left.Data;
            }

            if (root.Left != null) {
                leftSum += SumOfLeftLeaves(root.Left);
            } else {
                leftSum += 0;
            }

            if (root.Right != null)
            {
                leftSum += SumOfLeftLeaves(root.Right);
            } else {
                leftSum += 0;
            }

            return leftSum;
        }

        public List<List<int>> LevelOrder(TreeNode root)
        {
            List<List<int>> result = new List<List<int>>();
            if (root == null)
            {
                return result;
            }

            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                List<int> level = new List<int>();
                int levelSize = queue.Count;

                for (int _ = 0; _ < levelSize; _++)
                {
                    TreeNode node = queue.Dequeue();
                    level.Add(node.Data);

                    if (node.Left != null)
                    {
                        queue.Enqueue(node.Left);
                    }

                    if (node.Right != null)
                    {
                        queue.Enqueue(node.Right);
                    }
                }
                result.Add(level);
            }
            return result;
        }


    }
}
