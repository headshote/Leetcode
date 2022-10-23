using System;
using System.Text;

namespace Leetcode.Solutions
{
    public class HammingDistanceCalc
    {
        public int HammingDistance(int x, int y)
        {
            int result = x ^ y;
            int distance = 0;
            while(result != 0)
            {
                if (result % 2 == 1)
                {
                    distance++;
                }
                result = result >> 1;
            }

            return distance;
        }

        private int StringBased(int x, int y)
        {
            string sx = Convert.ToString(x, 2);
            string sy = Convert.ToString(y, 2);

            if (sx.Length < sy.Length)
            {
                sx = AddLeadingZeroes(sx, sy.Length - sx.Length);
            }
            else if (sx.Length > sy.Length)
            {
                sy = AddLeadingZeroes(sy, sx.Length - sy.Length);
            }

            int distance = 0;

            for (int i = 0; i < sx.Length; ++i)
            {
                char xBit = sx[i];
                char yBit = sy[i];
                if (xBit != yBit)
                {
                    distance++;
                }
            }

            return distance;
        }

        private string AddLeadingZeroes(string s, int nZeroes)
        {
            var b = new StringBuilder(nZeroes);
            b.Append('0', nZeroes);
            return b.ToString() + s;
        }
    }
}
