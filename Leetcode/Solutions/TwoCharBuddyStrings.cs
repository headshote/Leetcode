using System;

namespace Leetcode.Solutions
{
    public class TwoCharBuddyStrings
    {
        //Pretty slow bruteforce-ish
        public bool BuddyStrings(string s, string goal)
        {
            if( s.Length != goal.Length ) return false;

            for (int i = 0; i < s.Length; ++i)
            {
                char si = s[i];
                if (i < s.Length - 3)
                {
                    char sii = s[i + 1];
                    if(si == goal[i] && si == sii && sii == goal[i+1])
                    {
                        continue;
                    }
                }

                for (int j = i + 1; j < s.Length; ++j)
                {
                    var newS = SwapChars(s, i, j);
                    if (newS == goal)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private string SwapChars(string s, int i, int j)
        {
            char[] strChar = s.ToCharArray();
            strChar[i] = s[j];
            strChar[j] = s[i];

            return new String(strChar);
        }
    }
}
