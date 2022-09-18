using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class ArrayInlineRemoveElement
    {
        public int RemoveElement(int[] nums, int val)
        {
            var n = nums.Length;
            var allowedNums = 0;
            for (int i = 0; i < n;)
            {
                var num = nums[i];
                if (num == val)
                {
                    int j = i;
                    var done = false;
                    while (j < nums.Length - 1)
                    {
                        nums[j] = nums[j + 1];
                        ++j;
                        done = true;
                    }
                    --n;
                    if (!done)
                    {
                        ++i;
                    }
                }
                else
                {
                    ++allowedNums;
                    ++i;
                }
            }

            return allowedNums;
        }
    }
}
