using Leetcode.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class BinTreeLowestCommonAncestor
    {
        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            var stack = new Stack<TreeNode>();
            var nodePathStack = new Stack<Stack<TreeNode>>();

            stack.Push(root);
            Stack<TreeNode> nodePath = new Stack<TreeNode>();
            nodePath.Push(root);
            nodePathStack.Push(nodePath);
            //dfs for any of the two nodes
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                nodePath = nodePathStack.Pop();

                if (node == p || node == q)
                {
                    stack.Clear();
                    break;
                }

                if(node.left != null)
                {
                    stack.Push(node.left);

                    var npr = CloneStack(nodePath);
                    npr.Push(node.left);
                    nodePathStack.Push(npr);
                }
                if(node.right != null)
                {
                    stack.Push(node.right);

                    var npl = CloneStack(nodePath);
                    npl.Push(node.right);
                    nodePathStack.Push(npl);
                }
            }

            TreeNode prevAncestor = null;
            var targetNode = nodePath.Peek() == p ? q : p;

            //for each ancestor of the found node, start dfs on the "opposite" half
            //for the node also check its children
            while (nodePath.Count > 0)
            {
                var foundNodeAncestor = nodePath.Pop();

                if(foundNodeAncestor == root) //guaranteed to be in the list by definition
                {
                    return foundNodeAncestor;
                }

                if(prevAncestor != null && foundNodeAncestor.left == prevAncestor)
                {
                    if (foundNodeAncestor.right != null)
                        stack.Push(foundNodeAncestor.right);
                }
                else if (prevAncestor != null && foundNodeAncestor.right == prevAncestor)
                {
                    if (foundNodeAncestor.left != null)
                        stack.Push(foundNodeAncestor.left);
                }
                else //the top node in the stack is one of the two, which was found previously
                {
                    if(foundNodeAncestor.right != null)
                        stack.Push(foundNodeAncestor.right);
                    if (foundNodeAncestor.left != null)
                        stack.Push(foundNodeAncestor.left);
                }

                //dfs to find the remaining node
                while (stack.Count > 0)
                {
                    var node = stack.Pop();

                    if (node == targetNode)
                    {
                        return foundNodeAncestor;
                    }

                    if (node.left != null)
                    {
                        stack.Push(node.left);
                    }
                    if (node.right != null)
                    {
                        stack.Push(node.right);
                    }
                }

                prevAncestor = foundNodeAncestor;
            }

            return null;
        }
        public Stack<T> CloneStack<T>(Stack<T> original)
        {
            var arr = new T[original.Count];
            original.CopyTo(arr, 0);
            Array.Reverse(arr);
            return new Stack<T>(arr);
        }
    }
}
