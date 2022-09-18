using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    // TODO: finish
    public class NetworkNodesReceiveSignals
    {
        public int NetworkDelayTime(int[][] times, int n, int k)
        {
            var maxDist = 0;

            var kTimes = GetKNodeTimes(times, k);

            var visitedNodes = new HashSet<int>();
            var markedNodes = new HashSet<int>();
            markedNodes.Add(k);
            var que = new Queue<(int node, int pathSum, int[] nodeTime)>();
            var signalDistAtNode = new int[n+1];

            foreach (var kTime in kTimes)
            {
                que.Enqueue((k, 0, kTime));
            }

            while(que.Count > 0)
            {
                var nodeData = que.Dequeue();

                var node = nodeData.node;
                visitedNodes.Add(node);
                var pathSum = nodeData.pathSum;
                var nodeTime = nodeData.nodeTime;

                var nextNode = nodeTime[1];
                if(!visitedNodes.Contains(nextNode))
                {
                    pathSum += nodeTime[2];

                    if(pathSum < signalDistAtNode[nextNode] || signalDistAtNode[nextNode] == 0)
                    {
                        var nodeTimes = GetKNodeTimes(times, nextNode);                        

                        signalDistAtNode[nextNode] = pathSum;
                        markedNodes.Add(nextNode);

                        foreach (var nextNodeTime in nodeTimes)
                        {
                            que.Enqueue((nextNode, pathSum, nextNodeTime));
                        }
                    }
                }
            }

            for(int i =1; i < signalDistAtNode.Length; i++)
            {
                maxDist = Math.Max(signalDistAtNode[i], maxDist);
            }

            return markedNodes.Count == n ? maxDist : -1;
        }

        private IList<int[]> GetKNodeTimes(int[][] times, int k)
        {
            return times.Where(x => x[0] == k).ToList();
        }
    }
}
