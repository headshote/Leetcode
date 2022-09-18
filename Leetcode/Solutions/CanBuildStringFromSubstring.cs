using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class CanBuildStringFromSubstring
    {
        public bool RepeatedSubstringPattern(string s)
        {
            var n = s.Length;
            for (int i = 1; i <= n / 2; ++i)
            {
                var subString = s.Substring(0, i);
                var ratio = n / subString.Length;
                var repeatedSubstrB = new StringBuilder(n);
                for (int j = 0; j < ratio; ++j)
                {
                    repeatedSubstrB.Append(subString);
                }
                var repeatedSubstr = repeatedSubstrB.ToString();

                if (repeatedSubstr == s)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
