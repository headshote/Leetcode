using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public  class CombinationsFromArrayCandidatesToTarget
    {
        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            return SolveFast(candidates, target);
            //return SolveIterative(candidates, target);
        }

        private IList<IList<int>> SolveFast(int[] candidates, int target)
        {
            var candsList = candidates.ToList();
            var combinations = new List<IList<int>>();

            for (int i = 0; i < candidates.Length; i++)
            {
                var candidate = candidates[i];

                if(candidate == target)
                {
                    combinations.Add(new List<int>{ candidate });
                }
                else
                {
                    combinations.AddRange(BuildCombinationRecursive(candsList, target, candidate, i, new List<int> { candidate }));
                }
            }

            return combinations;
        }

        private IList<IList<int>> BuildCombinationRecursive(IList<int> candidates, int target, int sumSoFar, int startSearchIndex, IList<int> combinationBuilt)
        {
            var combinations = new List<IList<int>>();

            var usableCandidates = FindLessThanOrEqualCandidates(candidates, target, sumSoFar, startSearchIndex);

            for (int j = 0; j < usableCandidates.Count; ++j)
            {
                var uc = usableCandidates[j];

                var newSum = uc + sumSoFar;
                if(newSum == target)
                {
                    var newCombination = new List<int>(combinationBuilt);
                    newCombination.Add(uc);
                    combinations.Add(newCombination);
                }
                else if(newSum < target)
                {
                    var newCombination = new List<int>(combinationBuilt);
                    newCombination.Add(uc);
                    combinations.AddRange(BuildCombinationRecursive(usableCandidates, target, newSum, j, newCombination));
                }
            }

            return combinations;
        }

        private IList<int> FindLessThanOrEqualCandidates(IList<int> candidates, int target, int sumSofar, int startSearchIndex)
        {
            var usableCandidates = new List<int>();
            for (int i = startSearchIndex; i < candidates.Count; i++)
            {
                var candidate2 = candidates[i];
                if(sumSofar + candidate2 <= target)
                {
                    usableCandidates.Add(candidate2);
                }
            }
            return usableCandidates;
        }

        private IList<IList<int>> SolveIterative(int[] candidates, int target)
        {
            var combinations = new List<IList<int>>();

            var que = new Queue<(int sum, List<int> combination)>();
            que.Enqueue((0, new List<int> { }));

            while (que.Count > 0)
            {
                var data = que.Dequeue();

                var sum = data.sum;
                var combination = data.combination;

                for (int i = 0; i < candidates.Length; ++i)
                {
                    var num = candidates[i];
                    var newSum = sum + num;
                    if (newSum == target)
                    {
                        var newCombo = new List<int>(combination);
                        newCombo.Add(num);
                        combinations.Add(newCombo);
                    }
                    else if (newSum < target)
                    {
                        var newCombo = new List<int>(combination);
                        newCombo.Add(num);
                        que.Enqueue((newSum, newCombo));
                    }                
                }
            }

            return combinations;
        }

        private IList<IList<int>> SolveRescursive(int[] candidates, int target)
        {
            var combinations = new List<IList<int>>();

            for (int i = 0; i < candidates.Length; ++i)
            {
                var num = candidates[i];
                combinations.AddRange(BuidlCombinationRecirsive(candidates, target, num, new List<int> { num }));
            }

            for (int i = 0; i < combinations.Count; ++i)
            {
                var combination = combinations[i] as List<int>;
                combination.Sort();
            }

            return combinations.Distinct(new CompareLists()).ToList();
        }

        private IList<IList<int>> BuidlCombinationRecirsive(int[] candidates, int target, int sumSoFar, IList<int> combinationBuilt)
        {

            if (sumSoFar == target)
                return new List<IList<int>>() { combinationBuilt };
            else if (sumSoFar > target)
                return new List<IList<int>>();

            var combinations = new List<IList<int>>();

            for (int i = 0; i < candidates.Length; ++i)
            {
                var num = candidates[i];
                if (sumSoFar + num <= target)
                {
                    var newCombo = new List<int>(combinationBuilt);
                    newCombo.Add(num);
                    combinations.AddRange(BuidlCombinationRecirsive(candidates, target, sumSoFar + num, newCombo));
                }
            }

            return combinations;
        }
    }
    public class CompareLists : IEqualityComparer<IList<int>>
    {
        public bool Equals(IList<int> x, IList<int> y)
        {
            if (x.Count != y.Count)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < x.Count; i++)
                {
                    if (x[i] != y[i])
                        return false;
                }
                return true;
            }
        }
        public int GetHashCode(IList<int> codeh)
        {
            return 0;
        }
    }
}
