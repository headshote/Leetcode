using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class DiagonalMartix
    {
        public int[][] GenerateMatrix(int n)
        {
            int xDir = 1;
            int yDir = 0;
            var filledElements = 0;
            var totalElemtns = n * n;
            var r = 0;
            var c = 0;

            int[][] matrix = new int[n][];
            for(int i = 0; i < n; i++)
            {
                matrix[i] = new int[n];
            }

            while(filledElements < totalElemtns)
            {
                var el = filledElements + 1;
                matrix[r][c] = el;
                filledElements++;

                if (xDir > 0)
                {
                    if (c == n-1 || matrix[r][c + 1] != 0)
                    {
                        xDir = 0;
                        yDir = 1;
                    }
                }
                else if(xDir < 0)
                {
                    if (c == 0 || matrix[r][c - 1] != 0)
                    {
                        xDir = 0;
                        yDir = -1;
                    }
                }
                else if(yDir > 0)
                {
                    if (r == n-1 || matrix[r+1][c] != 0)
                    {
                        xDir = -1;
                        yDir = 0;
                    }
                }
                else if (yDir < 0)
                {
                    if (r == 0 || matrix[r - 1][c] != 0)
                    {
                        xDir = 1;
                        yDir = 0;
                    }
                }

                c += xDir;
                r += yDir;
            }

            return matrix;
        }
    }
}
