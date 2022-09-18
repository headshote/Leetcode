using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class MedianOfTwoSortedArrays
    {
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            return FindMedianSortedArraysBinarySearch(nums1, nums2);
        }

        private double FindMedianSortedArraysBinarySearch(int[] nums1, int[] nums2)
        {
            int n = nums1.Length;
            int m = nums2.Length;
            if (n > m) //first array has to be the smaller one
                return FindMedianSortedArraysBinarySearch(nums2, nums1);

            int start = 0;
            int end = n;
            int totalMedianIndex = (n + m + 1) / 2;

            while (start <= end)
            {
                int mid = (start + end) / 2;
                int leftAsize = mid;
                int leftBsize = totalMedianIndex - mid;
                int left1 = (leftAsize > 0)  ? nums1[leftAsize - 1] : Int32.MinValue;
                int left2  = (leftBsize > 0) ? nums2[leftBsize - 1] : Int32.MinValue;
                int right1 = (leftAsize < n) ? nums1[leftAsize] : Int32.MaxValue;
                int right2 = (leftBsize < m) ? nums2[leftBsize] : Int32.MaxValue;

                // move as binary search until subarrays start overlapping
                if (left1 <= right2 && left2 <= right1)
                {
                    if ((m + n) % 2 == 0)
                        return (Math.Max(left1, left2) + Math.Min(right1, right2)) / 2.0;
                    return Math.Max(left1, left2);
                }
                else if (left1 > right2)
                {
                    end = mid - 1;
                }
                else
                {
                    start = mid + 1;
                }
            }
            return 0.0;
        }

        private double FindMedianSortedArraysLinear(int[] nums1, int[] nums2)
        {
            var totalOrder = new double[nums1.Length + nums2.Length];
            int i = 0, j = 0, k = 0;
            for (; i < nums1.Length && j < nums2.Length; k++)
            {
                var n1 = nums1[i];
                var n2 = nums2[j];

                if (n1 < n2)
                {
                    totalOrder[k] = n1;
                    i++;
                }
                else
                {
                    totalOrder[k] = n2;
                    j++;
                }
            }
            for (; i < nums1.Length; i++)
            {
                totalOrder[k++] = nums1[i];
            }
            for (; j < nums2.Length; j++)
            {
                totalOrder[k++] = nums2[j];
            }

            if (totalOrder.Length % 2 == 0)
            {
                return (totalOrder[totalOrder.Length / 2] + totalOrder[totalOrder.Length / 2 - 1]) / 2;
            }
            else
            {
                return totalOrder[totalOrder.Length / 2];
            }
        }
    }
}
