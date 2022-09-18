using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class MaximumSubarray
    {
        public int MaxSubArray(int[] nums)
        {
            return MaxSubArrayLinear(nums);
            //return MaxSubArrayRecursive(nums, 0, nums.Length-1);
        }

        private int MaxSubArrayLinear(int[] nums)
        {
            int sum = nums[0];
            int maxSum = sum;

            for (int i = 1; i < nums.Length; i++)
            {
                var element = nums[i];

                if (sum + element >= element)
                {
                    sum += element;
                }
                else
                {
                    sum = element;
                }

                maxSum = Math.Max(maxSum, sum);
            }

            return maxSum;
        }

        private int MaxSubArrayRecursive(int[] nums, int startIndex, int endIndex)
        {
            if(startIndex == endIndex)
            {
                return nums[startIndex];
            }

            var midPoint = (startIndex + endIndex) / 2;

            var sum = nums[midPoint];
            var maxSumLeft = sum;
            for (int i = midPoint-1; i >= startIndex; i--)
            {
                sum += nums[i];
                if(sum > maxSumLeft)
                {
                    maxSumLeft = sum;
                }
            }

            sum = nums[midPoint + 1];
            var maxSumRight = sum;
            for (int i = midPoint + 2; i <= endIndex; i++)
            {
                sum += nums[i];
                if(sum > maxSumRight)
                {
                    maxSumRight = sum;
                }
            }

            var leftSbuarrayMaxSum = MaxSubArrayRecursive(nums, startIndex, midPoint);
            var rightSubarrayMaxSum = MaxSubArrayRecursive(nums, midPoint + 1, endIndex);

            var maxSum = Math.Max(leftSbuarrayMaxSum, rightSubarrayMaxSum);

            return Math.Max(maxSum, maxSumRight + maxSumLeft);
        }
    }
}
