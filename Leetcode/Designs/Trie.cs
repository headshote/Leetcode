using System.Collections.Generic;

namespace Leetcode.Designs
{
    public class Trie
    {
        private NTreeNode root;

        public Trie()
        {
            root = new NTreeNode();
        }

        public void Insert(string word)
        {
            var node = root;
            foreach(var chr in word)
            {
                if(!node.leafs.ContainsKey(chr))
                {
                    node.leafs[chr] = new NTreeNode();
                }
                node = node.leafs[chr];
            }

            node.terminal = true;
        }

        public bool Search(string word)
        {
            var node = root;
            foreach (var chr in word)
            {
                if (node.leafs.ContainsKey(chr))
                {
                    node = node.leafs[chr];
                }
                else
                {
                    return false;
                }
            }

            return node.terminal;
        }

        public bool StartsWith(string prefix)
        {
            var node = root;
            foreach (var chr in prefix)
            {
                if (node.leafs.ContainsKey(chr))
                {
                    node = node.leafs[chr];
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }

    internal class NTreeNode
    {
        public Dictionary<char, NTreeNode> leafs;
        public bool terminal;

        public NTreeNode(Dictionary<char, NTreeNode> leafs = null)
        {
            this.leafs = leafs != null ? leafs : new Dictionary<char, NTreeNode>();
            this.terminal = false;
        }
    }
}
