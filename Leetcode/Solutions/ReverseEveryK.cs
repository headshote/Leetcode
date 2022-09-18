using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class ReverseEveryK
    {
        public string ReverseStr(string s, int k)
        {
            int i = 0;
            StringBuilder str = new StringBuilder(s.Length);
            var n = s.Length;
            while (i < n)
            {
                int reversingStart = Math.Min(i + k - 1, n - 1);
                for (int j = reversingStart; j >= i; --j)
                {
                    var chr = s[j];
                    str.Append(chr);
                }

                var subStringStart = i + k;
                if(subStringStart <= n)
                {
                    var remainingSubstringLen = Math.Min(n - subStringStart, k);
                    var remainingSubstring = s.Substring(subStringStart, remainingSubstringLen);
                    str.Append(remainingSubstring);
                }
                else if(reversingStart + 1< n)
                {
                    var remainingSubstring = s.Substring(reversingStart+1, n- reversingStart);
                    str.Append(remainingSubstring);
                }

                i += k*2;
            }

            return str.ToString();
        }
    }
}
