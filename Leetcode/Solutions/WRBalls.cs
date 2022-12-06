namespace Leetcode.Solutions
{
    public class WRBalls
    {
        public int Solution(string S)
        {
            int moves = 0;
            int rTrainCount = 0;
            int lTrainCount = 0;
            int leftIndex = S.IndexOf('R');
            int rightIndex = S.LastIndexOf('R');

            if(leftIndex == rightIndex)
                return 0;

            while (leftIndex <= rightIndex)
            {
                char lChr = S[leftIndex];
                char rChr = S[rightIndex];
                int lDist = 0;
                int rDist = 0;

                if(lChr == 'R')
                {
                    lTrainCount++;
                    leftIndex++;
                }
                else if (lChr == 'W')
                {
                    int seekIndex = leftIndex;
                    while (S[++seekIndex] != 'R' && seekIndex < rightIndex)
                    {
                    }
                    lDist = seekIndex - leftIndex;
                }

                if(rChr == 'R')
                {
                    rTrainCount++;
                    rightIndex--;
                }
                else if (rChr == 'W')
                {
                    int seekIndex = rightIndex;
                    while (S[--seekIndex] != 'R' && seekIndex > leftIndex)
                    {
                    }
                    rDist = rightIndex - seekIndex;
                }

                if(lDist * lTrainCount < rDist * rTrainCount || (lDist > 0 && rDist < 1))
                {
                    if (int.MaxValue - lDist * lTrainCount <= moves)
                        return -1;
                    moves += lDist * lTrainCount;
                    leftIndex += lDist;
                }
                else
                {
                    if (int.MaxValue - rDist * rTrainCount <= moves)
                        return -1;
                    moves += rDist * rTrainCount;
                    rightIndex -= rDist;
                }
            }            

            return moves;
        }
    }
}
