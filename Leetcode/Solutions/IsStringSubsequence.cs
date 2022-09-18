using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class IsStringSubsequence
    {
        public bool IsSubsequence(string s, string t)
        {
            int j = 0;
            for (int i = 0; j < s.Length && i < t.Length; i++)
            {
                var ct = t[i];
                var cs = s[j];
                
                if(ct == cs)
                {
                    j++;
                }                
            }

            return j == s.Length;
        }

        private bool IsSubsequenceWithHashTable(string s, string t)
        {
            var charIndexes = new Dictionary<char, List<int>>();

            for(int i = 0; i < t.Length; i++)
            {
                var c = t[i];

                if(charIndexes.ContainsKey(c))
                {
                    charIndexes[c].Add(i);
                }
                else
                {
                    charIndexes[c] = new List<int> { i };
                }
            }

            var prevMinCharIndex = -1;
            foreach (var c in s)
            {
                if (charIndexes.ContainsKey(c))
                {
                    var charIndicesInT = charIndexes[c];

                    if (charIndicesInT.Count < 1)
                        return false;

                    var minCharIndex = -1;
                    for (int j = 0; minCharIndex == -1 && j < charIndicesInT.Count; j++)
                    {
                        if(charIndicesInT[j] > prevMinCharIndex)
                        {
                            minCharIndex = charIndicesInT[j];
                            charIndicesInT.RemoveAt(j);
                        }
                    }
                    if(minCharIndex == -1)
                        return false;

                    var maxCharIndex = minCharIndex;
                    if(charIndicesInT.Count > 0)
                    {
                        maxCharIndex = Math.Max(maxCharIndex, charIndicesInT[charIndicesInT.Count - 1]);
                    }

                    if(maxCharIndex > prevMinCharIndex)
                    {
                        prevMinCharIndex = minCharIndex;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}
