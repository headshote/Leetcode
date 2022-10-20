using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Designs
{
    /*
     * Q with 2 stacks
     */
    public class MyQueue
    {
        private Stack<int> stack;
        private Stack<int> tmp;

        public MyQueue()
        {
            stack = new Stack<int>();
            tmp = new Stack<int>();
        }

        public void Push(int x)
        {
            if (!Empty())
            {
                SwapStacks();
            }
            tmp.Push(x);
            SwapStacks();
        }

        public int Pop()
        {
            return stack.Pop();
        }

        public int Peek()
        {
            return stack.Peek();
        }

        public bool Empty()
        {
            return stack.Count == 0;
        }

        private void SwapStacks()
        {
            while (!Empty())
            {
                var item = stack.Pop();
                tmp.Push(item);
            }

            var swapVar = stack;
            stack = tmp;
            tmp = swapVar;
        }
    }
}
