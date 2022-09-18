using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.DataTypes
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;

        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
              this.val = val;
              this.left = left;
              this.right = right;
        }

        public override string ToString()
        {
            if (right != null && left != null)
                return $"[{left}-{val}-{right}]";
            else if(right != null)
                return $"[{val}-{right}]";
            else if (left != null)
                return $"[{left}-{val}]";
            return $"[{val}]";
        }
    }
}
