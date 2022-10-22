using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class NumRollsToTargetWithNDice
    {
        //Slow, brute-force. Optimization idea - use math to count combinations, BigInteger for calculation
        public int NumRollsToTarget(int n, int k, int target)
        {
            return Permute(k, n, target);
        }

        private int Permute(int maxNum, int permLength, int target)
        {
            return GetPermutationsRecursive(maxNum, permLength, target, 0, 0, 0);
        }

        private int GetPermutationsRecursive(int maxNum, int permLength, int target, 
            int recursionPosition, int prevPermutationSum, int prevPermutationLen)
        {
            int permutations = 0;

            for (int i = 1; i <= maxNum && recursionPosition < permLength; i++)
            {
                if (i + prevPermutationSum <= target)
                {
                    int newPermutation = prevPermutationSum;
                    newPermutation += i;
                    if (prevPermutationLen + 1 == permLength && newPermutation == target)
                    {
                        permutations += 1;
                    }
                    permutations += GetPermutationsRecursive(maxNum, permLength, target, recursionPosition + 1, newPermutation, prevPermutationLen + 1);
                }
            }

            return permutations;
        }
    }
}
