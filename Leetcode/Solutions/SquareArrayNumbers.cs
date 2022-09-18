using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class SquareArrayNumbers
    {
        public int[] SortedSquares(int[] nums)
        {
            var n = nums.Length;

            var squares = new int[n];

            if (nums[0] < 0 && nums[n - 1] > 0)
            {
                var nonNegStartIndex = n - 1;
                for (int ind = 1; ind < n; ++ind)
                {
                    if (nums[ind] > -1)
                    {
                        nonNegStartIndex = ind;
                        break;
                    }
                }

                int i = nonNegStartIndex;
                int j = nonNegStartIndex - 1;
                int k = 0;
                for (; j > -1 && i < n;)
                {
                    var numPos = nums[i];
                    var numNeg = nums[j];

                    var nPSquared = numPos * numPos;
                    var nNSquared = numNeg * numNeg;

                    if (nNSquared < nPSquared)
                    {
                        squares[k++] = nNSquared;
                        j--;
                    }
                    else if (nNSquared > nPSquared)
                    {
                        squares[k++] = nPSquared;
                        i++;
                    }
                    else
                    {
                        squares[k++] = nNSquared;
                        squares[k++] = nPSquared;
                        j--;
                        i++;
                    }
                }
                while (j > -1)
                {
                    squares[k++] = nums[j] * nums[j];
                    j--;
                }
                while (i < n)
                {
                    squares[k++] = nums[i] * nums[i];
                    i++;
                }
            }
            else if (nums[0] < 0 && nums[n - 1] <= 0)
            {
                for (int i = 0; i < n; ++i)
                {
                    var num = nums[i];
                    squares[n - 1 - i] = num * num;
                }
            }
            else
            {
                for (int i = 0; i < n; ++i)
                {
                    var num = nums[i];
                    squares[i] = num * num;
                }
            }

            return squares;
        }
    }
}
