using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class SumBigIntStrings
    {
        public string AddStrings(string num1, string num2)
        {
            var n1 = num1.Length;
            var n2 = num2.Length;

            if (n1 < n2)
            {
                return AddStrings(num2, num1);
            }

            var memSum = 0;
            var sumStr = string.Empty;
            for (int i = n1 - 1, j = n2 - 1; i > -1; --i, --j)
            {
                int integer1 = num1[i] - '0';
                if (j > -1)
                {
                    int integer2 = num2[j] - '0';
                    var sum = integer1 + integer2 + memSum;
                    if (sum > 9)
                    {
                        var cEnd = sum % 10;
                        memSum = sum / 10;
                        char cs = (char)(((char)cEnd) + '0');
                        sumStr = cs + sumStr;
                    }
                    else
                    {
                        memSum = 0;
                        char cs = (char)(((char)sum) + '0');
                        sumStr = cs + sumStr;
                    }
                }
                else
                {
                    var sum = integer1 + memSum;
                    if (sum > 9)
                    {
                        var cEnd = sum % 10;
                        memSum = sum / 10;
                        char cs = (char)(((char)cEnd) + '0');
                        sumStr = cs + sumStr;
                    }
                    else
                    {
                        memSum = 0;
                        char cs = (char)(((char)sum) + '0');
                        sumStr = cs + sumStr;
                    }
                }
            }

            if(memSum > 0)
            {
                char cs = (char)(((char)memSum) + '0');
                sumStr = cs + sumStr;
            }

            return sumStr;
        }
    }
}
