using Leetcode.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class MergeKSortedLists
    {
        public ListNode MergeKLists(ListNode[] lists)
        {
            return SolveMergingListsByHeads(lists);
        }

        private ListNode SolveMergingListsByHeads(ListNode[] lists)
        {
            ListNode mergedNodes = null;
            ListNode mergedNodesTail = null;

            while(true)
            {
                int minHeadValue = int.MaxValue;
                int minHeadIndex = 0;
                for (var i = 0; i < lists.Length; i++)
                {
                    var head = lists[i];

                    if(head == null)
                    {
                        continue;
                    }    

                    if (head.val < minHeadValue)
                    {
                        minHeadValue = head.val;
                        minHeadIndex = i;
                    }
                }

                if(minHeadValue == int.MaxValue)
                {
                    break;
                }

                lists[minHeadIndex] = lists[minHeadIndex].next;

                if (mergedNodes == null)
                {
                    mergedNodes = new ListNode() { val = minHeadValue };
                    mergedNodesTail = mergedNodes;
                }
                else
                {
                    mergedNodesTail.next = new ListNode() { val = minHeadValue };
                    mergedNodesTail = mergedNodesTail.next;
                }
            }

            return mergedNodes;
        }

        private ListNode SolveMergingListsRecursive(ListNode[] lists)
        {
            ListNode mergedNodes = null;
            var i = 0;

            for (i = 0; i < lists.Length; i++)
            {
                var list = lists[i];

                if (list == null)
                {
                    continue;
                }

                if (mergedNodes == null)
                {
                    mergedNodes = MergeKListsRecursive(list.next, new ListNode() { val = list.val });
                }
                else
                {
                    mergedNodes = MergeKListsRecursive(list, mergedNodes);
                }
            }

            return mergedNodes;
        }

        private ListNode MergeKListsRecursive(ListNode list, ListNode mergedNodes)
        {
            if(mergedNodes == null)
            {
                if (list.next == null)
                {
                    return new ListNode()
                    {
                        val = list.val
                    };
                }
                else
                {
                    return new ListNode()
                    {
                        val = list.val,
                        next = MergeKListsRecursive(list.next, null)
                    };
                }
            }

            if(list == null)
            {
                return mergedNodes;
            }

            if(list.val > mergedNodes.val)
            {
                if (mergedNodes.next != null)
                {
                    mergedNodes.next = MergeKListsRecursive(list, mergedNodes.next);
                }
                else
                {
                    if(list.next != null)
                    {
                        mergedNodes.next = new ListNode() { val = list.val, next = MergeKListsRecursive(list.next, null) };
                    }
                    else
                    {
                        mergedNodes.next = new ListNode() { val = list.val };
                    }
                }
            }
            else
            {
                if(list.next != null)
                {
                    mergedNodes = new ListNode() 
                    { 
                        val = list.val, 
                        next = MergeKListsRecursive(list.next, mergedNodes) 
                    };
                }
                else
                {
                    mergedNodes = new ListNode() 
                    { 
                        val = list.val, 
                        next = mergedNodes 
                    };
                }
            }

            return mergedNodes;
        }
    }
}
