using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class Permutations
    {
        public IList<IList<int>> Permute(int[] nums)
        {
            return GetPermutationsRecursive(nums.ToList(), 0, new List<int>());
        }

        private IList<IList<int>> GetPermutationsRecursive(List<int> lst, int recursionPosition, List<int> prevPermutation)
        {
            List<IList<int>> permutations = new List<IList<int>>();

            for (int i = 0; i < lst.Count && recursionPosition < lst.Count; i++)
            {
                var integer = lst[i];
                List<int> newPermutation = new List<int>(prevPermutation);
                if (newPermutation.IndexOf(integer) < 0)
                {
                    newPermutation.Add(integer);
                }
                else
                {
                    continue;
                }
                if (newPermutation.Count == lst.Count)
                {
                    permutations.Add(newPermutation);
                }
                permutations.AddRange(GetPermutationsRecursive(lst, recursionPosition + 1, newPermutation));
            }

            return permutations;
        }
    }
}
