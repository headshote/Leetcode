using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class TheWordBreak
    {
        private Dictionary<(int, string), bool> wordsBuiltCache;

        public bool WordBreak(string s, IList<string> wordDict)
        {
            HashSet<string> words = new HashSet<string>(wordDict);

            wordsBuiltCache = new Dictionary<(int, string), bool>();

            return BreakWordsRecursive(s, words, 0, string.Empty);
        }

        private bool GetChachedBreakWordsRecursive(string s, HashSet<string> words, int startChar, string wordBuilt)
        {
            var key = (startChar, wordBuilt);
            if(!wordsBuiltCache.ContainsKey(key))
            {
                wordsBuiltCache[key] = BreakWordsRecursive(s, words, startChar, wordBuilt);
            }

            return wordsBuiltCache[key];
        }

        private bool BreakWordsRecursive(string s, HashSet<string> words, int startChar, string wordBuiltThusFar)
        {
            var n = s.Length;
            var wordBuilt = wordBuiltThusFar;
            for (int i = startChar; i < n; i++)
            {
                var c = s[i];
                wordBuilt = wordBuilt + c;

                if (words.Contains(wordBuilt))
                {
                    if(GetChachedBreakWordsRecursive(s, words, i+1, wordBuilt))
                    {
                        return true;
                    }
                    wordBuilt = string.Empty;
                }
                else if (wordBuilt.Length > 20 || i == n - 1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
