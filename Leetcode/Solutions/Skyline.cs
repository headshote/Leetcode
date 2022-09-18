using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class Skyline
    {
        public IList<IList<int>> GetSkyline(int[][] buildings)
        {
            var maxX = 0;
            for (int i = 0; i < buildings.Length; i++)
            {
                maxX = Math.Max(maxX, buildings[i][1]);
            }

            var sPoints = new List<IList<int>>();
            IList<int> currentBuilding = null; //we consider ourselves on the roof of this building
            var x = buildings[0][0]; //we start from the leftmost building
            var nBuildings = buildings.Length;
            //search directions, search upwards first to find the first roof beginning point
            var xDir = 0;
            var yDir = 1;
            while (x < maxX)
            {
                if (xDir > 0) //seeking to the right for the first wall, or the drop - current building's roof end
                {
                    var found = false;
                    for (int i = 0; !found && i < nBuildings; i++)
                    {
                        var building = buildings[i];
                        var buildingLeftEdge = building[0];
                        var buildingRightEdge = building[1];
                        var buildingHeight = building[2];

                        if (currentBuilding != null)
                        {
                            var cBuildingRightEdge = currentBuilding[1];
                            var cBuildingHeight = currentBuilding[2];

                            if (buildingLeftEdge > x && buildingHeight > cBuildingHeight && buildingLeftEdge <= cBuildingRightEdge) //adjacet/overlapping building with a higher roof, move up
                            {
                                x = buildingLeftEdge;
                                yDir = 1;
                                xDir = 0;
                                found = true;
                            }
                            else if(cBuildingHeight == buildingHeight && cBuildingRightEdge == buildingLeftEdge) //hop onto an adjacent building to the right
                            {
                                x = currentBuilding[0];
                                yDir = 0;
                                xDir = 1;
                                currentBuilding = building;
                            }
                            else if(cBuildingHeight == buildingHeight && cBuildingRightEdge > buildingLeftEdge && cBuildingRightEdge < buildingRightEdge) //move on to the overlapping building
                            {
                                x = cBuildingRightEdge;
                                yDir = 0;
                                xDir = 1;
                                currentBuilding = building;
                            }
                        }
                        else
                        {
                            if (buildingLeftEdge > x) //we're on the ground, so take the first one to the right
                            {
                                x = buildingLeftEdge;
                                yDir = 1;
                                xDir = 0;
                                found = true;
                            }
                        }
                    }

                    if(!found) //happens when we're at the building with the rightmost wall, or no adjacent/overlapping neighbors, seek something down
                    {
                        x = currentBuilding[1];
                        yDir = -1;
                        xDir = 0;
                    }
                }
                else if (yDir > 0)  //seeking upward for the tallest building at the point x
                {
                    var maxTop = 0;
                    for (int i = 0; i < nBuildings; i++)
                    {
                        var building = buildings[i];
                        if (building[0] > x)    //buildings further away from our point - no point looking at them
                        {
                            break;
                        }
                        var bTop = building[2];
                        if (building[1] >= x && bTop > maxTop)
                        {
                            maxTop = bTop;
                            currentBuilding = building;
                        }
                    }

                    sPoints.Add(new List<int>() {x, maxTop});
                    yDir = 0;
                    xDir = 1;
                }
                else if (yDir < 0)  //looking downward for the tallest building below the current building's roof edge (or thake the roof if no buildings were found)
                {
                    var currBuildingTop = currentBuilding[2];
                    var maxTop = 0;
                    for (int i = 0; i < nBuildings; i++)
                    {
                        var building = buildings[i];
                        var bTop = building[2];

                        if (building[0] > x)    //no more overlapping/adjacent buildings, can stop searching
                        {
                            if(maxTop == 0) //no buildings found before - we're on the ground
                            {
                                currentBuilding = null;
                            }
                            break;
                        }
                        else if (building[1] > x && bTop < currBuildingTop)
                        {
                            if (bTop > maxTop)
                            {
                                maxTop = bTop;
                                currentBuilding = building;
                            }
                        }
                    }

                    sPoints.Add(new List<int>() { x, maxTop });
                    yDir = 0;
                    xDir = 1;
                }
            }

            //closing point
            sPoints.Add(new List<int>() { maxX, 0 });

            return sPoints;
        }
    }
}
