using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class FindDuplicateNumberBetween1AndN
    {
        public int FindDuplicate(int[] nums)
        {
            var duplicate = -1;
            for(int i = 0; i < nums.Length; i++)
            {
                var num = Math.Abs(nums[i]);

                if(nums[num] < 0 )
                {
                    duplicate = num;
                }
                nums[num] *= -1;
            }

            for (int i = 0; i < nums.Length; i++)
            {
                if(nums[i] < 0)
                    nums[i] *= -1;
            }

            return duplicate;
        }
    }
}
