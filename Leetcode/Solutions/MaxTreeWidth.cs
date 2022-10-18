using Leetcode.DataTypes;
using System;
using System.Collections.Generic;

namespace Leetcode.Solutions
{
    public class MaxTreeWidth
    {
        public int WidthOfBinaryTree(TreeNode root)
        {
            var leftIndices = new Dictionary<long, long>();
            var rightIndices = new Dictionary<long, long>();

            ParseLeftMostIndices(root, leftIndices, 0, 0);
            ParseRightMostIndices(root, rightIndices, 0, 0);

            long maxWidth = 1;
            foreach(long level in leftIndices.Keys)
            {
                if(rightIndices.ContainsKey(level))
                {
                    maxWidth = Math.Max(maxWidth, rightIndices[level] - leftIndices[level] + 1);
                }
            }

            return (int)maxWidth;
        }

        private void ParseLeftMostIndices(TreeNode node, Dictionary<long, long> leftIndices, long level, long startIndex)
        {
            if (node == null)
            {
                return;
            }

            if (node.left != null)
            {
                UpdateLeftIndices(leftIndices, level, startIndex * 2);
            }
            else if (node.right != null)
            {
                UpdateLeftIndices(leftIndices, level, startIndex * 2 + 1);
            }

            ParseLeftMostIndices(node.left, leftIndices, level + 1, startIndex * 2);
            ParseLeftMostIndices(node.right, leftIndices, level + 1, startIndex * 2 + 1);
        }

        private void UpdateLeftIndices(Dictionary<long, long> leftIndices, long level, long startIndex)
        {
            if (leftIndices.ContainsKey(level + 1))
            {
                leftIndices[level + 1] = Math.Min(startIndex, leftIndices[level + 1]);
            }
            else
            {
                leftIndices[level + 1] = startIndex;
            }
        }

        private void ParseRightMostIndices(TreeNode node, Dictionary<long, long> rightIndices, long level, long startIndex)
        {
            if (node == null)
            {
                return;
            }

            if (node.right != null)
            {
                UpdatRightIndices(rightIndices, level, startIndex * 2 + 1);
            }
            else if (node.left != null)
            {
                UpdatRightIndices(rightIndices, level, startIndex * 2);
            }

            ParseRightMostIndices(node.left, rightIndices, level + 1, startIndex * 2);
            ParseRightMostIndices(node.right, rightIndices, level + 1, startIndex * 2 + 1);
        }

        private void UpdatRightIndices(Dictionary<long, long> rightIndices, long level, long startIndex)
        {
            if (rightIndices.ContainsKey(level + 1))
            {
                rightIndices[level + 1] = Math.Max(startIndex, rightIndices[level + 1]);
            }
            else
            {
                rightIndices[level + 1] = startIndex;
            }
        }
    }
}
