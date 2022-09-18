using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    //TODO:
    public class CountOneSubmatrices
    {
        public int NumSubmat(int[][] mat)
        {
            var n = mat.Length;
            var m = mat[0].Length;
            var total = 0;
            for (int i = 1; i <= n; ++i)
            {
                for (int j = 1; j <= m; ++j)
                {
                    total += GetNMatricesIxJ(mat, i, j, n, m);
                }
            }

            return total;
        }

        //TODO: fast algo
        public int NumSubmatFast(int[][] mat)
        {
            var n = mat.Length;
            var m = mat[0].Length;
            var total = 0;
            for (int i = 0; i < n; ++i)
            {
                var row = mat[i];
                for (int j = 0; j < m; ++j)
                {
                    var elt = row[j];

                    if(elt == 1)
                    {
                        ++total;
                        
                        //Find 1-len, 1-hight matrices
                        var ii = i+1;
                        while(ii < n && mat[ii][j] == 1)
                        {
                            ++total;
                            ++ii;
                        }
                        var jj = j + 1;
                        while (jj < m && mat[i][jj] == 1)
                        {
                            ++total;
                            ++jj;
                        }

                        //find how large is 1-el i x j matrix starting from this element (by searchingto closest 0 down and to the left)
                        ii = i;
                        var minIi = int.MaxValue;
                        var minJj = int.MaxValue;
                        while (ii < n)
                        {
                            jj = j;
                            while (jj < m)
                            {
                                if( mat[ii][jj] != 1)
                                {
                                    minIi = Math.Min(minIi, ii);
                                    minJj = Math.Min(minJj, jj);
                                }
                                ++jj;
                            }

                            ++ii;
                        }

                        //calculate all the possible submatrix combinations this i x j 1-only element matrix can have using math
                    }
                }
            }

            return total;
        }

        private int GetNMatricesIxJ(int[][] mat, int i, int j, int n, int m)
        {
            var counter = 0;
            for (int row = 0; row < n - (i - 1); ++row)
            {
                var mRow = mat[row];
                for (int col = 0; col < m - (j - 1); ++col)
                {
                    if (CheckIxJMatrix(mat, row, col, row + i - 1, col + j - 1))
                    {
                        ++counter;
                    }
                }
            }

            return counter;
        }

        private bool CheckIxJMatrix(int[][] mat, int r1, int c1, int r2, int c2)
        {
            for (int row = r1; row <= r2; ++row)
            {
                var mRow = mat[row];
                for (int col = c1; col <= c2; ++col)
                {
                    if (mRow[col] == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
