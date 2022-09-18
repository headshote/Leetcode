using System.Collections.Generic;

namespace Leetcode.Solutions
{
    public class TheGameOfLife
    {
        Dictionary<(int, int), int> boardCellNeighbors = new Dictionary<(int, int), int>();

        public void GameOfLife(int[][] board)
        {
            boardCellNeighbors = new Dictionary<(int, int), int>();
            var n = board.Length;

            for (int i = 0; i < n; i++)
            {
                var m = board[i].Length;
                for (int j = 0; j < m; j++)
                    boardCellNeighbors[(i,j)] = 0;
            }

            for (int i = 0 ; i < n; i++)
            {
                var row = board[i];
                var m = row.Length;
                for( int j = 0 ; j < m ; j++)
                {
                    var cellValue = row[j];

                    CalculateNeighbors(i, j, cellValue, n, m, board);
                }
            }

            for(int i = 0 ; i < n; i++)
            {
                var row = board[i];
                var m = row.Length;
                for( int j = 0 ; j < m ; j++)
                {
                    var cell = (i, j);
                    var cellValue = row[j];
                    var cellsNeighborCount = boardCellNeighbors[cell];

                    if(cellsNeighborCount < 2)
                    {
                        row[j] = 0;
                    }
                    else if (cellsNeighborCount > 3)
                    {
                        row[j] = 0;
                    }
                    else
                    {
                        if(cellsNeighborCount == 3 && cellValue == 0)
                        {
                            row[j] = 1;
                        }
                    }    
                }
            }
        }

        private void CalculateNeighbors(int i, int j, int cellValue, int n, int m, int[][] board)
        {
            var cell = (i, j);

            if (i < n - 1)
            {
                UpdateCellNeighborData(cell, cellValue, i+1, j, board);
                if (j > 0)
                {
                    UpdateCellNeighborData(cell, cellValue, i + 1, j - 1, board);
                }
                if (j < m - 1)
                {
                    UpdateCellNeighborData(cell, cellValue, i + 1, j + 1, board);
                }
            }
            if(j < m - 1)
            {
                UpdateCellNeighborData(cell, cellValue, i, j + 1, board);
            }
        }

        private void UpdateCellNeighborData((int, int) cell, int cellValue, int neighborCelli, int neighborCellj, int[][] board)
        {
            if (board[neighborCelli][neighborCellj] == 1)
            {
                boardCellNeighbors[cell] += 1;
            }

            if (cellValue == 1)
            {
                var neighborCell = (neighborCelli, neighborCellj);

                boardCellNeighbors[neighborCell] += 1;
            }
        }
    }
}
