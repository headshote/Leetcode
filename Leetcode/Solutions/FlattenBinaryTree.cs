using Leetcode.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class FlattenBinaryTree
    {
        public void Flatten(TreeNode root)
        {
            if(root == null)
                return;

            FlattenRecursive(root);
        }

        private TreeNode FlattenRecursive(TreeNode node)
        {
            if(node.left != null)
            {
                var right = node.right;
                var left = node.left;

                node.right = left;
                node.left = null;
                FlattenRecursive(left).right = right;
            }
            if(node.right != null)
            {
                return FlattenRecursive(node.right);
            }
            return node;
        }
    }
}
