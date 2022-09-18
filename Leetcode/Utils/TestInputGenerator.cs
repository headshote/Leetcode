using Leetcode.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Utils
{
    public class TestInputGenerator
    {
        public static string GenerateHugeStringOfRandomChars(int size, char iLow, char iHigh)
        {
            var b = new StringBuilder();
            var prg = new Random();

            for (int i = 0; i < size; i++)
            {
                b.Append((char)prg.Next(iLow, iHigh));
            }

            return b.ToString();
        }

        public static string GenerateHugeStringOfSameChars(int size, char c)
        {
            var b = new StringBuilder();

            for (int i = 0; i < size; i++)
            {
                b.Append(c);
            }

            return b.ToString();
        }

        public static int[] GenerateHugeArrayOfRandomInts(int size, int iLow, int iHigh)
        {
            var randomInts = new int[size];
            var prg = new Random();
            for (int i = 0; i < size; i++)
            {
                randomInts[i] = prg.Next(iLow, iHigh);
            }
            return randomInts;
        }

        public static (TreeNode, TreeNode, TreeNode) GenerateLeftHandMatrix(int start, int end)
        {
            var root = new TreeNode(start);
            var secondToLast = root;
            var last = root;

            var node = root;
            for (int i = start+1; i < end; i++)
            {
                node.left = new TreeNode(i);
                node = node.left;

                if (i == end - 2)
                    secondToLast = node;
                if (i == end - 1)
                    last = node;
            }

            return (root, secondToLast, last);
        }

        public static (TreeNode, TreeNode, TreeNode) GenerateRightHandMatrix(int start, int end)
        {
            var root = new TreeNode(start);
            var secondToLast = root;
            var last = root;

            var node = root;
            for (int i = start+1; i < end; i++)
            {
                node.right = new TreeNode(i);
                node = node.right;

                if (i == end - 2)
                    secondToLast = node;
                if (i == end - 1)
                    last = node;
            }

            return (root, secondToLast, last);
        }
    }
}
