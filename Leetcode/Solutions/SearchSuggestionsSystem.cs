using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class SearchSuggestionsSystem
    {
        public IList<IList<string>> SuggestedProducts(string[] products, string searchWord)
        {
            var suggestions = new List<IList<string>>();
            var prefix = string.Empty;

            Array.Sort(products);

            foreach (var letter in searchWord)
            {
                var prefixSuggestion = new List<string>();
                prefix += letter;

                (var prefixWord, var prefixIndex) =
                        BinaryFindSmallestWordByPrefix(products, prefix, 0, products.Count() - 1);

                while (prefixWord != null && prefixSuggestion.Count() < 3 && prefixIndex < products.Count())
                {
                    prefixWord = products[prefixIndex++];
                    if (prefixWord != null && prefixWord.StartsWith(prefix))
                    {
                        prefixSuggestion.Add(prefixWord);
                    }
                }

                suggestions.Add(prefixSuggestion);
            }

            return suggestions;
        }

        private (string, int) BinaryFindSmallestWordByPrefix(string[] products, string searchPrefix, int start, int end)
        {
            if (end < start)
            {
                return (null, -1);
            }

            var searchIndex = (start + end) / 2;
            var product = products[searchIndex];
            var productPrefix = product.Substring(0, Math.Min(searchPrefix.Length, product.Length));
            string prefixWord = null;

            var prodPrefixComparedToSearchPrefix = productPrefix.CompareTo(searchPrefix);

            if (prodPrefixComparedToSearchPrefix == 0)
            {
                prefixWord = products[searchIndex];

                if(searchIndex > 0)
                {
                    var lresult = BinaryFindSmallestWordByPrefix(products, searchPrefix, start, searchIndex - 1);
                    if (!string.IsNullOrEmpty(lresult.Item1) && lresult.Item1.CompareTo(prefixWord) < 0)
                    {
                        prefixWord = lresult.Item1;
                        searchIndex = lresult.Item2;
                    }
                }
            }
            else if(end == start)
            {
                return (prefixWord, searchIndex);
            }

            if (prodPrefixComparedToSearchPrefix > 0)
            {
                return BinaryFindSmallestWordByPrefix(products, searchPrefix, start, searchIndex - 1);
            }
            else if(prodPrefixComparedToSearchPrefix < 0)
            {
                return BinaryFindSmallestWordByPrefix(products, searchPrefix, searchIndex + 1, end);
            }

            return (prefixWord, searchIndex);
        }
    }
}
