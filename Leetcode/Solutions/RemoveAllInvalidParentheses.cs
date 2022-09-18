using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class RemoveAllInvalidParentheses
    {
        public IList<string> RemoveInvalidParentheses(string s)
        {
            (int leftPars, int rightPars) cs = CountBrokenParens(s);

            var correctStrings = GetCorrectStringsRecursive(s, cs.leftPars, cs.rightPars, new StringBuilder(s.Length), 0, 0);
            return correctStrings.ToList();
        }

        private HashSet<string> GetCorrectStringsRecursive(string s, int leftParensToRemove, int rightParensToRemove, StringBuilder createdSting, int startChar, int openedParenthesesCount)
        {
            var correctStrings = new HashSet<string>();
            var newString = createdSting;

            for (int j = startChar; j < s.Length; j++)
            {
                var cj = s[j];

                if (cj == '(')
                {
                    if (leftParensToRemove > 0)
                    {
                        //skip char
                        correctStrings.UnionWith(GetCorrectStringsRecursive(s, leftParensToRemove - 1, rightParensToRemove, new StringBuilder(newString.ToString()), j+1, openedParenthesesCount));
                    }

                    openedParenthesesCount++;
                    newString.Append(cj);
                }
                else if (cj == ')')
                {
                    if (openedParenthesesCount > 0)
                    {
                        if (rightParensToRemove > 0)
                        {
                            //skip char
                            correctStrings.UnionWith(GetCorrectStringsRecursive(s, leftParensToRemove, rightParensToRemove - 1, new StringBuilder(newString.ToString()), j + 1, openedParenthesesCount));
                        }

                        openedParenthesesCount--;
                        newString.Append(cj);
                    }
                    else   // unpaired closing paren = broken string
                    {
                        if (rightParensToRemove > 0)
                        {
                            //skip char
                            correctStrings.UnionWith(GetCorrectStringsRecursive(s, leftParensToRemove, rightParensToRemove - 1, new StringBuilder(newString.ToString()), j + 1, openedParenthesesCount));
                        }
                        else
                        {
                            //we've ended up with a broken string
                            newString = null;
                            break;
                        }
                    }
                }
                else //non-parenthesis
                {
                    newString.Append(cj);
                }
            }

            if(newString != null && openedParenthesesCount < 1)
            {
                correctStrings.Add(newString.ToString());
            }

            return correctStrings;
        }

        private (int leftPars, int rightPars) CountBrokenParens(string s)
        {
            (int leftPars, int rightPars) cs = (0, 0);

            int ops = 0;
            for (int i = 0; i < s.Length; ++i)
            {
                var c = s[i];

                if (c == '(')
                {
                    ops++;
                    cs.leftPars++;
                }
                else if (c == ')')
                {
                    if (ops > 0)
                    {
                        ops--;
                        cs.leftPars--;
                    }
                    else
                    {
                        cs.rightPars++;
                    }
                }
            }

            return cs;
        }
    }
}
