using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    /*
     * Longest Substring Without Repeating Characters
     */
    public class LongestSubstring
    {
        public int LengthOfLongestSubstring(string s)
        {
            var usedChars = new HashSet<char>();
            var subStringLen = 0;
            var maxSubstringLen = 0;
            var prevSubStringStart = 0;

            for( int i = 0; i < s.Length; )
            {
                var c = s[i];

                if (!usedChars.Contains(c))
                {
                    usedChars.Add(c);
                    subStringLen++;
                    i++;
                }
                else
                {
                    maxSubstringLen = Math.Max(maxSubstringLen, subStringLen);

                    subStringLen = 0;
                    usedChars.Clear();

                    i = ++prevSubStringStart;
                }
            }

            return Math.Max(maxSubstringLen, subStringLen);
        }
    }
}
