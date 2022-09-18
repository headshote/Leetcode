using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class MaxPointsOnLine
    {
        //will be used to identify lines, slopes are unique for each line
        private (int x, int y) GetSlopeAsCoprimes(int x1, int y1, int x2, int y2)
        {
            var dx = x1 - x2;
            var dy = y1 - y2;

            //treat horizontal/vertical lines differently
            if(dx == 0)
            {
                return (x1, 0); //mark a constant x line (horizontal) with value x1
            }
            if(dy == 0)
            {
                return (0, y1); //mark a constant y line (vertical) with value y1
            }

            if(dx < 0)  //count opposite direction slope as equivalent and put them under one key as one slope
            {
                dx = -dx;
                dy = -dy;
            }

            var gcd = (int) BigInteger.GreatestCommonDivisor(dx, dy);

            return (dx / gcd, dy / gcd);
        }

        public int MaxPoints(int[][] points)
        {
            if(points.Length < 2) //don't waste my time
            {
                return points.Length;
            }

            var maxLinePoints = 2;
            var linePoints = new Dictionary<(int x, int y), int>();
            for (var i = 0; i < points.Length-1; ++i)
            {
                linePoints.Clear(); //clear for each new point pair search

                var pointA = points[i];
                for (var j = i+1; j < points.Length; ++j)
                {
                    var pointB = points[j];

                    var slope = GetSlopeAsCoprimes(pointA[0], pointA[1], pointB[0], pointB[1]);
                    linePoints[slope] = linePoints.ContainsKey(slope) ? linePoints[slope] + 1 : 2;

                    maxLinePoints = Math.Max(maxLinePoints, linePoints[slope]);
                }
            }

            return maxLinePoints;
        }
    }
}
