using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class TrappingRainWater
    {
        public double Trap(int[] height)
        {
            //return TrapWith2Pointers(height);
            return TrapWithBacktracking(height);
        }

        public double TrapWith2Pointers(int[] height)
        {
            var left = 0;
            var right = height.Length-1;
            var totalWaterHeight = 0.0;
            var maxLeftHeight = 0;
            var maxRightHeight = 0;

            while (left < right)
            {
                var leftHeight = height[left];
                var rightHeight = height[right];

                if(leftHeight < rightHeight)
                {
                    if(maxLeftHeight <= leftHeight)
                    {
                        maxLeftHeight = leftHeight;
                    }
                    else
                    {
                        totalWaterHeight += maxLeftHeight - leftHeight;
                    }
                    left++;
                }
                else
                {
                    if(maxRightHeight <= rightHeight)
                    {
                        maxRightHeight = rightHeight;
                    }
                    else
                    {
                        totalWaterHeight += maxRightHeight - rightHeight;
                    }
                    right--;
                }
            }
            return totalWaterHeight;
        }

        public double TrapWithBacktracking(int[] height)
        {
            var totalWaterHeight = 0.0;

            for (int i = 0; i < height.Length;)
            {
                var blockHeight = height[i];

                if (blockHeight > 0)
                {
                    var closingBlockHeight = 0;
                    var closingBlockIndex = 0;

                    //find the highest "closing" block to catch water
                    var j = i;
                    while(++j < height.Length && blockHeight > closingBlockHeight)
                    {
                        var forwardHeight = height[j];
                        if(closingBlockHeight <= forwardHeight)
                        {
                            closingBlockHeight = forwardHeight;
                            closingBlockIndex = j;
                        }
                    }

                    //if it exists, backtrack to origin, calculating water height trapped between the start block and closing block
                    //then move the next search to the closing block
                    if(closingBlockHeight != 0)
                    {
                        var minHeight = Math.Min(blockHeight, closingBlockHeight);
                        j = closingBlockIndex-1;
                        while (j > i)
                        {
                            totalWaterHeight += minHeight - height[j];
                            j--;
                        }
                        i = closingBlockIndex;
                    }
                    else //if closing block doesn't exist, move the cycle forward (with leap through everything, in case we never found any closing blocks beyond)
                    {
                        if (closingBlockIndex > 0)
                            i = closingBlockIndex;
                        else
                            i++;
                    }
                }
                else
                {
                    i++;
                }
            }

            return totalWaterHeight;
        }
    }
}
