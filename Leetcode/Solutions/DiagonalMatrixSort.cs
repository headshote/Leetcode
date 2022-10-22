using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class DiagonalMatrixSort
    {
        public int[][] DiagonalSort(int[][] mat)
        {
            int nRows = mat.Length;
            int mCols = mat[0].Length;

            for (int i = 0; i < nRows - 1; ++i)
            {
                SortDiagonal(mat, nRows, mCols, i, 0);
            }
            for (int i = 0; i < mCols - 1; ++i)
            {
                SortDiagonal(mat, nRows, mCols, 0, i);
            }
            return mat;
        }

        private void SortDiagonal(int[][] mat, int nRows, int mCols, int jj, int kk)
        {
            bool founOutOfOrder = false;
            do
            {
                int j = jj;
                int k = kk;
                founOutOfOrder = false;
                while (j < nRows - 1 && k < mCols - 1)
                {
                    var curCel = mat[j][k];
                    var nextDiagCel = mat[j + 1][k + 1];
                    if (curCel > nextDiagCel)
                    {
                        mat[j][k] = nextDiagCel;
                        mat[j + 1][k + 1] = curCel;
                        founOutOfOrder = true;
                    }
                    j++;
                    k++;
                }
            } while (founOutOfOrder == true);
        }
    }
}
