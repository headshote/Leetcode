using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class PrefixExcludedProducts
    {
        public int[] ProductExceptSelf(int[] nums)
        {
            var n = nums.Length;
            var leftProducts = new int[n - 1];
            var rightProducts = new int[n - 1];

            leftProducts[0] = nums[0];
            for (int i = 1; i < n - 1; ++i)
            {
                leftProducts[i] = leftProducts[i - 1] * nums[i];
            }
            rightProducts[n - 2] = nums[n - 1];
            for (int i = n - 3; i > -1; --i)
            {
                rightProducts[i] = rightProducts[i + 1] * nums[i+1];
            }

            var prefixProducts = new int[n];
            for (int i = 0; i < n; ++i)
            {
                if (i == 0)
                {
                    prefixProducts[i] = rightProducts[i];
                }
                else if (i == n - 1)
                {

                    prefixProducts[i] = leftProducts[i-1];
                }
                else
                {
                    prefixProducts[i] = leftProducts[i - 1] * rightProducts[i];
                }

            }

            return prefixProducts;
        }
    }
}
