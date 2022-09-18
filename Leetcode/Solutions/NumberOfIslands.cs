using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class NumberOfIslands
    {
        private const char land = '1';
        private const char water = '0';

        public int NumIslands(char[][] grid)
        {
            var islandStartNodes = 0;

            for(int row = 0; row < grid.Length; row++)
            {
                for(int col = 0; col < grid[row].Length; col++)
                {
                    if(grid[row][col] == land)
                    {
                        FindAndMarkConnectedIslandNodes(grid, row, col);
                        islandStartNodes++;
                    }                    
                }
            }

            return islandStartNodes;
        }

        private void FindAndMarkConnectedIslandNodes(char[][] grid, int islandNodeRow, int islandNodeCol)
        {
            if(grid[islandNodeRow][islandNodeCol] == water)
            {
                return;
            }

            grid[islandNodeRow][islandNodeCol] = water;

            if (islandNodeRow != 0)
            {
                FindAndMarkConnectedIslandNodes(grid, islandNodeRow - 1, islandNodeCol);
            }
            if (islandNodeCol != 0)
            {
                FindAndMarkConnectedIslandNodes(grid, islandNodeRow, islandNodeCol - 1);
            }
            if (islandNodeRow != grid.Length - 1)
            {
                FindAndMarkConnectedIslandNodes(grid, islandNodeRow + 1, islandNodeCol);
            }
            if (islandNodeCol != grid[islandNodeRow].Length - 1)
            {
                FindAndMarkConnectedIslandNodes(grid, islandNodeRow, islandNodeCol + 1);
            }
        }
    }
}
