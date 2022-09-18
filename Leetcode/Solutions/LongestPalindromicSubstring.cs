using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class LongestPalindromicSubstring
    {
        public string LongestPalindrome(string s)
        {
            var maxPal = "";

            var iPoint = s.Length / 2;
            while (iPoint >= 0 && iPoint > maxPal.Length / 2 - 1)
            {
                maxPal = GetLargetPalidromeAroundPoint(s, iPoint, maxPal);

                iPoint--;
            }

            iPoint = s.Length / 2 + 1;
            while (iPoint < s.Length && ( (s.Length - iPoint) > (maxPal.Length / 2 - 1) ))
            {
                maxPal = GetLargetPalidromeAroundPoint(s, iPoint, maxPal);
                iPoint++;
            }

            return maxPal;
        }

        private string GetLargetPalidromeAroundPoint(string s, int point, string prevMaxPal)
        {
            int i = point - 1;
            int j = point + 1;

            var pal = new StringBuilder();

            var pointChar = s[point];
            pal.Append(pointChar);

            if (i < 0 || j > s.Length)
            {
                if (pal.Length > prevMaxPal.Length)
                {
                    prevMaxPal = pal.ToString();
                }
                return prevMaxPal;
            }

            while (i >= 0 && s[i] == pointChar)
            {
                i--;
                pal.Append(pointChar);
            }
            while (j < s.Length && s[j] == pointChar)
            {
                j++;
                pal.Append(pointChar);
            }

            if (pal.Length > prevMaxPal.Length)
            {
                prevMaxPal = pal.ToString();
            }

            while (i >= 0 && j < s.Length)
            {
                var si = s[i--];
                var sj = s[j++];
                if (si == sj)
                {
                    pal.Insert(0, si);
                    pal.Append(sj); 
                    
                    if (pal.Length > prevMaxPal.Length)
                    {
                        prevMaxPal = pal.ToString();
                    }
                }
                else
                {                    
                    break;
                }
            }

            return prevMaxPal;
        }
    }
}
