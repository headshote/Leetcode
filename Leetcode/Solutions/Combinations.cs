using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class Combinations
    {
        public IList<IList<int>> Combine(int n, int k)
        {
            return GetCombinationsRecursive(n, k, 1, new List<int>());
        }

        private IList<IList<int>> GetCombinationsRecursive(int n, int size, int startInteger, List<int> prevCombination)
        {
            List<IList<int>> combinations = new List<IList<int>>();

            for (int i = startInteger; i <= n; i++)
            {
                var integer = i;
                List<int> newCombination = new List<int>(prevCombination);
                newCombination.Add(integer);
                if (newCombination.Count == size)
                {
                    combinations.Add(newCombination);
                }
                if (newCombination.Count < size)
                    combinations.AddRange(GetCombinationsRecursive(n, size, i + 1, newCombination));
            }

            return combinations;
        }
    }
}
