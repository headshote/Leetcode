using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class RottenOrangesGridDuration
    {
        public int OrangesRotting(int[][] grid)
        {
            var adjacentToRotten = new List<(int row, int col)>();
            var n = grid.Length;
            var m = grid[0].Length;
            var nonRotens = 0;

            var minutes = -1;
            do
            {
                nonRotens = 0;
                adjacentToRotten.Clear();
                for (int i = 0; i < n; ++i)
                {
                    var row = grid[i];
                    for (int j = 0; j < m; ++j)
                    {
                        var element = row[j];
                        if (element == 2)
                        {
                            adjacentToRotten.AddRange(FindAdjacentsToRotten(grid, i, j, n, m));
                        }
                        else if(element == 1)
                        {
                            nonRotens++;
                        }
                    }
                }

                for (int i = 0; i < adjacentToRotten.Count; ++i)
                {
                    var indices = adjacentToRotten[i];
                    if(grid[indices.row][indices.col] == 1)
                    {
                        grid[indices.row][indices.col] = 2;
                        nonRotens--;
                    }
                }
                minutes++;
            } while (adjacentToRotten.Count > 0);

            return nonRotens < 1 ? minutes : -1;
        }

        private IList<(int row, int col)> FindAdjacentsToRotten(int[][] grid, int i, int j, int n, int m)
        {
            var adjacentToRotten = new List<(int row, int col)>();

            if(i > 0)
            {
                int e = grid[i - 1][j];
                if (e == 1)
                {
                    adjacentToRotten.Add((i - 1, j));
                }
            }
            if(j > 0)
            {
                int e = grid[i][j - 1];
                if (e == 1)
                {
                    adjacentToRotten.Add((i, j - 1));
                }
            }
            if (i < n - 1)
            {
                int e = grid[i + 1][j];
                if (e == 1)
                {
                    adjacentToRotten.Add((i + 1, j));
                }
            }
            if (j < m - 1)
            {
                int e = grid[i ][j + 1];
                if (e == 1)
                {
                    adjacentToRotten.Add((i, j + 1));
                }
            }

            return adjacentToRotten;
        }
    }
}
