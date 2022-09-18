using Leetcode.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class BinaryTreeMaximumPathSum
    {
        private const int minValue = -1000;

        public int MaxPathSum(TreeNode root)
        {
            if (root == null)
                return minValue;

            return MaxPathSumAndPath(root).Item1;
        }


        public (int,int) MaxPathSumAndPath(TreeNode node)
        {
            if (node == null)
                return (minValue, minValue);

            var leftSumAndPath = MaxPathSumAndPath(node.left);
            var rightSumAndPath = MaxPathSumAndPath(node.right);
            var leftMaxSum = leftSumAndPath.Item1;
            var rightMaxSum = rightSumAndPath.Item1;
            var leftPath = leftSumAndPath.Item2;
            var rightPath = rightSumAndPath.Item2;

            var maxPathVal = node.val;
            if (node.left != null)
            {
                maxPathVal = Math.Max(maxPathVal, node.val + leftPath);
            }
            if (node.right != null)
            {
                maxPathVal = Math.Max(maxPathVal, node.val + rightPath);
            }

            var maxPathSum = Math.Max(leftMaxSum, rightMaxSum);
            if (node.left != null && node.right != null )
            {
                maxPathSum = Math.Max(maxPathSum, node.val + leftPath + rightPath);
            }

            return (Math.Max(maxPathSum, maxPathVal), maxPathVal);
        }
    }
}
