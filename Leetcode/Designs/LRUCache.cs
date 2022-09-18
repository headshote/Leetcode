using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Designs
{
    public class LRUCache
    {
        private DoubleListNode recencyHead;
        private DoubleListNode recencyTail;
        private Dictionary<int, DoubleListNode> keyValMapping;
        private int cap;

        public LRUCache(int capacity)
        {
            cap = capacity;
            keyValMapping = new Dictionary<int, DoubleListNode>(cap);
        }

        public int Get(int key)
        {
            if(keyValMapping.ContainsKey(key))
            {
                var node = keyValMapping[key];
                PushNodeToTopRecency(node);
                return node.val;
            }
            return -1;
        }

        public void Put(int key, int value)
        {
            if (keyValMapping.ContainsKey(key))
            {
                var node = keyValMapping[key];
                PushNodeToTopRecency(node);
                node.val = value;
            }
            else if (keyValMapping.Count == cap)
            {
                //remove LRU node - the tail, add a new one as a head
                var prevTail = recencyTail;
                keyValMapping.Remove(prevTail.key);
                if (prevTail.next != null)
                {
                    recencyTail = prevTail.next;
                    recencyTail.prev = null;
                    prevTail.next = null;
                }

                var prevHead = recencyHead;
                recencyHead = new DoubleListNode(value, key, prevHead);
                prevHead.next = recencyHead;
                if(prevTail == prevHead)
                {
                    recencyTail = recencyHead;
                }

                keyValMapping[key] = recencyHead;                
            }
            else
            {
                var prevHead = recencyHead;
                recencyHead = new DoubleListNode(value, key, prevHead);
                if(prevHead != null)
                {
                    prevHead.next = recencyHead;
                }
                else
                {
                    recencyTail = recencyHead;
                }

                keyValMapping[key] = recencyHead;
            }
        }

        private void PushNodeToTopRecency(DoubleListNode node)
        {
            if (recencyHead == node)
                return;

            var nodePrev = node.prev;
            var nodeNext = node.next;
            if (nodePrev != null)
            {
                if (nodeNext != null)
                {
                    nodePrev.next = nodeNext;
                }
            }

            if (nodeNext != null)
            {
                nodeNext.prev = nodePrev;
                if (recencyTail == node)
                {
                    recencyTail = nodeNext;
                }
            }

            recencyHead.next = node;
            node.prev = recencyHead;
            node.next = null;
            recencyHead = node;
        }
    }

    internal class DoubleListNode
    {
        public int val;
        public int key;
        public DoubleListNode prev;
        public DoubleListNode next;

        public DoubleListNode(int val = 0, int key = 0, DoubleListNode prev = null, DoubleListNode next = null)
        {
            this.val = val;
            this.key = key;
            this.prev = prev;
            this.next = next;
        }
    }
}
