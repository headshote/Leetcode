using System.Collections.Generic;

namespace Leetcode.Solutions
{
    public  class ValidParentheses
    {
        private readonly List<char> openingParens = new List<char>() { '(', '{', '[' };
        private readonly List<char> closingParens = new List<char>() { ')', '}', ']' };

        public bool IsValid(string s)
        {
            var lastOpeningChars = new List<char>();
            foreach(var c in s)
            {
                if(openingParens.Contains(c))
                {
                    lastOpeningChars.Add(c);
                }
                else if(closingParens.Contains(c))
                {
                    if(lastOpeningChars.Count > 0 && openingParens.IndexOf(lastOpeningChars[lastOpeningChars.Count - 1]) == closingParens.IndexOf(c))
                    {
                        lastOpeningChars.RemoveAt(lastOpeningChars.Count - 1);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return lastOpeningChars.Count == 0;
        }
    }
}
