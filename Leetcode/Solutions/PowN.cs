using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class PowN
    {
        private Dictionary<(double, int), double> powers = new Dictionary<(double, int), double>();

        public double MyPow(double x, int n)
        {
            if(n == 0 || x == 1.0)
                return 1.0;
            if (x == -1.0)
                return n % 2 == 0 ? 1.0 : -1.0;
            if (n == 1)
                return x;

            int absN;
            var overflowLeftover = 0;
            if (n > int.MinValue)
            {
                absN = n < 0 ? -n : n;
            }
            else
            {
                absN = n < 0 ? -(n + 1) : n;
                overflowLeftover = 1;
            }

            var xPowNPair = (x, n);
            if (powers.ContainsKey(xPowNPair))
            {
                return powers[xPowNPair];
            }

            var result = MyPow(x, (absN / 2) + overflowLeftover) * MyPow(x, (absN % 2 != 0 ? absN / 2 + 1 : absN / 2) + overflowLeftover);
            powers[xPowNPair] = n > 0 ? result : 1 / result;

            return powers[xPowNPair];
        }
    }
}
