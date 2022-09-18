using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class WordLadder
    {
        public int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {
            if(wordList.IndexOf(endWord) == -1)
            {
                return 0;
            }

            var ls = LadderCountBFS(beginWord, endWord, wordList);
            return ls.Count;
        }

        //breadth-first, graph structure traversal
        private IList<string> LadderCountBFS(string startWord, string endWord, IList<string> vocabulary)
        {
            var usedWords = new HashSet<string>();
            var ladderList = new List<string>();

            Queue<string> wordQueue = new Queue<string>();
            Queue<List<string>> ladderListQueue = new Queue<List<string>>();

            var oneLetterDiffVocabularyWords = CacheWordsWithOneLetterDiff(startWord, vocabulary);

            wordQueue.Enqueue(startWord);
            ladderListQueue.Enqueue(ladderList);
            while (wordQueue.Count > 0)
            {
                var word = wordQueue.Dequeue();
                ladderList = ladderListQueue.Dequeue();

                ladderList.Add(word);

                var oneDiffWords = oneLetterDiffVocabularyWords[word];

                foreach (var oneDiffWord in oneDiffWords)
                {
                    if (oneDiffWord == endWord)
                    {
                        ladderList.Add(oneDiffWord);
                        return ladderList;
                    }
                    if (!usedWords.Contains(oneDiffWord))
                    {
                        usedWords.Add(oneDiffWord);
                        wordQueue.Enqueue(oneDiffWord);
                        ladderListQueue.Enqueue(ladderList.GetRange(0, ladderList.Count));
                    }
                }
            }

            return new List<string>();
        }

        private Dictionary<string, IList<string>> CacheWordsWithOneLetterDiff(string startWord, IList<string> vocabulary)
        {
            var oneLetterDiffVocabularyWords = new Dictionary<string, IList<string>>();
            foreach (var word in vocabulary)
            {
                var oneDiffWords = GetWordsWithOneLetterDiff(word, vocabulary);
                oneLetterDiffVocabularyWords[word] = oneDiffWords;
            }
            var oneDiffWordsStartWord = GetWordsWithOneLetterDiff(startWord, vocabulary);
            oneLetterDiffVocabularyWords[startWord] = oneDiffWordsStartWord;

            return oneLetterDiffVocabularyWords;
        }


        private IList<string> GetWordsWithOneLetterDiff(string tWord, IList<string> vocabulary)
        {
            var oneDiffWords = new List<string>();

            foreach(var word in vocabulary)
            {
                if(LetterDifference(tWord, word) == 1)
                {
                    oneDiffWords.Add(word);
                }
            }

            return oneDiffWords;
        }

        private int LetterDifference(string word1, string word2)
        {
            if (word1.Length != word2.Length)
                return -1;

            var difference = 0;
            for (int i = 0; i < word1.Length; i++)
            {
                var c1 = word1[i];
                var c2 = word2[i];
                if(c1 != c2)
                {
                    difference++;
                }
            }

            return difference;
        }

        //Very inefficient traversal: depth-first, n-ary tree structure
        private IList<string> LadderStepRecursiveDFS(string startWord, string endWord, IList<string> vocabulary, List<string> ladderList, HashSet<string> usedWords)
        {
            //either use chache, or check for used words here, just too lazy to make a separate method now, after implementing bfs
            var oneDiffWords = GetWordsWithOneLetterDiff(startWord, vocabulary);

            if (oneDiffWords.Count == 0)
            {
                return new List<string>();
            }

            var wUsedWords = new HashSet<string>(usedWords);
            foreach (var word in oneDiffWords)
            {
                wUsedWords.Add(word);
            }

            var results = new List<IList<string>>();
            foreach (var word in oneDiffWords)
            {
                if (word == endWord)
                {
                    ladderList.Add(word);
                    results.Add(ladderList);
                }
                else if (ladderList.Count < startWord.Length * 2) //hack to constrain recursion depth, makes algo work with 50 elements arrays faster than infinity time, still very slow
                {
                    var wLadderList = ladderList.GetRange(0, ladderList.Count);
                    wLadderList.Add(word);
                    results.Add(LadderStepRecursiveDFS(word, endWord, vocabulary, wLadderList, wUsedWords));
                }
            }

            IList<string> minResult = null;
            foreach (var result in results)
            {
                if (result.Count > 0 && (minResult == null || result.Count < minResult.Count))
                {
                    minResult = result;
                }
            }

            return minResult ?? new List<string>();
        }
    }
}
