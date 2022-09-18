using System.Collections.Generic;

namespace Leetcode.Extensions
{
    public static class PrintExtensions
    {
        public static string TwoDimListToString<T>(this IList<IList<T>> list)
        {
            var results = new List<string>();
            foreach (var subList in list)
            {
                results.Add($"[{string.Join(",", subList)}]");
            }
            return $"\n[\n {string.Join("\n ", results)}\n]";
        }

        public static string TwoDimArrayToString<T>(this T[][] list)
        {
            var results = new List<string>();
            foreach (var subList in list)
            {
                results.Add($"[{string.Join(",", subList)}]");
            }
            return $"\n[\n {string.Join("\n ", results)}\n]";
        }
    }
}
