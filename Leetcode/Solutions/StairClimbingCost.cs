using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class StairClimbingCost
    {
        public int MinCostClimbingStairs(int[] cost)
        {
            //_sumMap.Clear();
            //return Math.Min(SumAtStepRecursive(cost, 0, 0), SumAtStepRecursive(cost, 1, 0));

            var minBackWardCost = new int[cost.Length];
            var n = cost.Length;

            minBackWardCost[n-1] = cost[n-1];
            minBackWardCost[n-2] = cost[n-2];
            for (int i = n-3; i >= 0; i--)
            {
                minBackWardCost[i] = Math.Min(minBackWardCost[i+1], minBackWardCost[i + 2]) + cost[i];
            }

            return Math.Min(minBackWardCost[0], minBackWardCost[1]);
        }

        public int MinCostClimbingStairsBruteforce(int[] cost)
        {
            var minSum = int.MaxValue;

            var stack = new Stack<int>();
            var sumsStack = new Stack<int>();
            stack.Push(0);
            stack.Push(1);
            sumsStack.Push(0);
            sumsStack.Push(0);
            while (stack.Count > 0)
            {
                var i = stack.Pop();
                var stepPrice = cost[i];
                var sumAtStep = sumsStack.Pop();
                sumAtStep += stepPrice;

                if (i < cost.Length-1)
                {
                    stack.Push(i+1);
                    sumsStack.Push(sumAtStep);
                }
                else
                {
                    if(sumAtStep < minSum)
                    {
                        minSum = sumAtStep;
                    }
                }

                if(i < cost.Length-2)
                {
                    stack.Push(i + 2);
                    sumsStack.Push(sumAtStep);
                }
                else
                {
                    if (sumAtStep < minSum)
                    {
                        minSum = sumAtStep;
                    }
                }
            }

            return minSum;
        }


        private Dictionary<(int,int), int> _sumMap = new Dictionary<(int,int), int>();
        private int SumAtStepRecursive(int[] cost, int stepIndex, int sumSoFar)
        {
            if (stepIndex > cost.Length - 1)
                return sumSoFar;
            var stepPrice = cost[stepIndex];

            var si1 = stepIndex + 1;
            var si2 = stepIndex + 2;
            sumSoFar += stepPrice;
            var si1k = (si1, sumSoFar);
            if (!_sumMap.ContainsKey(si1k))
            {
                _sumMap[si1k] = SumAtStepRecursive(cost, si1, sumSoFar);
            }
            var sum1 = _sumMap[si1k];
            var si2k = (si2, sumSoFar);
            if (!_sumMap.ContainsKey(si2k))
            {
                _sumMap[si2k] = SumAtStepRecursive(cost, si2, sumSoFar);
            }
            var sum2 = _sumMap[si2k];

            return Math.Min(sum1, sum2);
        }
    }
}
