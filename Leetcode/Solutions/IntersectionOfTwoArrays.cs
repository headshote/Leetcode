using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class IntersectionOfTwoArrays
    {
        public int[] Intersection(int[] nums1, int[] nums2)
        {
            int[] result;

            Array.Sort(nums1);
            Array.Sort(nums2);

            if (nums1.Length < nums2.Length)
            {
                result = ScanInterSections(nums1, nums2);
            }
            else
            {
                result = ScanInterSections(nums2, nums1);
            }

            return result;
        }

        private int[] ScanInterSections(int[] smaller, int[] larger)
        {
            var result = new List<int>();
            int prevNum = 0;
            for (int i = 0; i < smaller.Length; i++)
            {
                var num = smaller[i];

                if (i == 0)
                {
                    if (BinarySearch(larger, num))
                    {
                        result.Add(num);
                    }
                }
                else
                {
                    if (num != prevNum)
                    {
                        if (BinarySearch(larger, num))
                        {
                            result.Add(num);
                        }
                    }
                }

                prevNum = num;
            }

            return result.ToArray();
        }

        private bool BinarySearch(int[] nums, int num)
        {
            int i = 0, j = nums.Length - 1;

            while(i < j)
            {
                var mid = (i + j) / 2;
                var ni = nums[mid];

                if (ni == num)
                {
                    return true;
                }
                else if (num > ni)
                {
                    i = mid + 1;
                }
                else
                {
                    j = mid - 1;
                }

            }

            return nums[(i + j) / 2] == num;
        }
    }
}
