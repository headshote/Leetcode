using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class LongestValidParentheses
    {
        private readonly char openingParens = '(';
        private readonly char closingParens = ')';

        public int LongestValidParenthesesLen(string s)
        {
            var lastOpeningChars = new List<(char, int)>();
            var subStringLen = 0;
            var maxSubstringLen = 0;
            var prevSubStringStart = 0;

            for (int i = 0; i < s.Length;)
            {
                var c = s[i];

                if (openingParens == c)
                {
                    lastOpeningChars.Add((c, i));

                    subStringLen++;
                    i++;
                }
                else if (closingParens == c)
                {
                    if (lastOpeningChars.Count > 0)
                    {
                        lastOpeningChars.RemoveAt(lastOpeningChars.Count - 1);

                        subStringLen++;
                        i++;
                    }
                    else
                    {
                        maxSubstringLen = Math.Max(maxSubstringLen, subStringLen);

                        subStringLen = 0;
                        lastOpeningChars.Clear();
                        prevSubStringStart = ++i;
                    }
                }

                if(i >= s.Length && lastOpeningChars.Count > 0)
                {
                    if (prevSubStringStart < s.Length-1)
                    {
                        maxSubstringLen = Math.Max(maxSubstringLen,  lastOpeningChars.First().Item2 - prevSubStringStart);

                        subStringLen = 0;
                        prevSubStringStart = lastOpeningChars.First().Item2 + 1;
                        i = prevSubStringStart;
                        lastOpeningChars.Clear();
                    }
                    else
                    {
                        subStringLen = 0;
                        break;
                    }
                }
            }

            return Math.Max(maxSubstringLen, subStringLen);
        }
    }
}
