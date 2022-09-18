using Leetcode.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class CopyListWRandomPointer
    {
        public Node CopyRandomList(Node head)
        {
            Dictionary<Node, Node> oldNewMapping = new Dictionary<Node, Node>();

            Node newHead = null;
            var newTail = newHead;

            var pointer = head;

            while(pointer != null)
            {
                var newPointer = oldNewMapping.ContainsKey(pointer) ? oldNewMapping[pointer] : new Node(pointer.val);

                if(newHead == null)
                {
                    newTail = newHead = newPointer;
                }
                else
                {
                    newTail.next = newPointer;
                    newTail = newTail.next;
                }

                oldNewMapping[pointer] = newTail;

                var randPointer = pointer.random;

                if(randPointer != null)
                {
                    if(oldNewMapping.ContainsKey(randPointer))
                    {
                        newTail.random = oldNewMapping[randPointer];
                    }
                    else
                    {
                        newTail.random = new Node(randPointer.val);
                        oldNewMapping[randPointer] = newTail.random;
                    }
                }

                pointer = pointer.next;
            }

            return newHead;
        }
    }
}
