using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class PartitionUniqueLetterLabels
    {
        public IList<int> PartitionLabels(string s)
        {
            var indices = new List<int>();
            var occurences = new Dictionary<char, (int, int)>();
            for (int i = 0; i < s.Length; ++i)
            {
                var c = s[i];
                if (!occurences.ContainsKey(c))
                {
                    occurences[c] = (i, i);
                }
                else
                {
                    occurences[c] = (occurences[c].Item1, i);
                }
            }

            var occStart = 0;
            var occEnd = -1;
            foreach (var kv in occurences)
            {
                var chr = kv.Key;
                var ind = kv.Value;
                if (occEnd == -1)
                {
                    occEnd = ind.Item2;
                }
                if (ind.Item1 < occEnd && ind.Item2 > occEnd)
                {
                    occEnd = ind.Item2;
                }
                else if (ind.Item1 > occEnd && ind.Item2 > occEnd)
                {
                    indices.Add(occEnd - occStart + 1);
                    occStart = ind.Item1;
                    occEnd = ind.Item2;
                }
            }
            indices.Add(occEnd - occStart + 1);

            return indices;
        }
    }
}
