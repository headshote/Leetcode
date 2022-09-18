using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Utils
{
    public class PrintUtils
    {
        static public string ShortString4Print(string rawString)
        {
            return $"{rawString.Substring(0, Math.Min(50, rawString.Length))}{(rawString.Length > 50 ? $"...[{rawString.Length}]" : "")}";
        }
    }
}
