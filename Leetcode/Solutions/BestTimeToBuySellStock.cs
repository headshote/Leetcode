using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode.Solutions
{
    public class BestTimeToBuySellStock
    {
        public int MaxProfit(int[] prices)
        {
            return MaxProfitStateMachine(prices);
            var profit = 0;

            var (bottomIndices, topIndices) = EnumerateAllLocalBottomsAndTops(prices);

            var bottomIndicesList = bottomIndices.ToList();
            var topIndicesList = topIndices.ToList();

            var independentClusters = EnumerateIndependentPriceClusters(prices, bottomIndicesList, topIndicesList);

            foreach (var cluster in independentClusters)
            {
                var clusterStart = cluster.Item1;
                var clusterEnd = cluster.Item2;

                Dictionary<(int, int, int?), int> clusterProfitsAtPoint = new Dictionary<(int, int, int?), int>();
                var maxClusterProfit = GetInterferingPriceClusterMaxProfit(prices, clusterStart, clusterEnd, bottomIndices, topIndices,
                    0, null, clusterProfitsAtPoint);

                profit += maxClusterProfit;
            }

            return profit;
        }

        /*
         * The state machine
         *                         Rest
         *                      _________
         *                      |        |
         *                   (State A)<--
         *                 ^/^        \
         *             Rest/           \Buy     Rest
         *                /             \    __________
         *               /              ,\,  |        |
         *           (State C)<-------(State B)<-------  
         *                       Sell     
         * 
         * */
        public int MaxProfitStateMachine(int[] prices)
        {
            var n = prices.Length;

            var statesA = new int[n];
            var statesB = new int[n];
            var statesC = new int[n];

            //initial states at the first step
            statesA[0] = 0;
            statesB[0] = -prices[0];
            statesC[0] = 0;

            for (int i = 1; i < n; i++)
            {
                var price = prices[i];
                statesA[i] = Math.Max(statesA[i-1], statesC[i-1]);  //either prevoius state (rest action), or result of a previous sale
                statesB[i] = Math.Max(statesB[i-1], statesA[i - 1] - price);    //either prevois state (rest), or buy now (spend money)
                statesC[i] = statesB[i-1] + price;  //sell - buy profit (-buyPrice+sellPrice)
            }

            return Math.Max(statesA[n - 1], statesC[n - 1]);
        }

        private int GetMaxRecursiveClusterProfit(int[] prices, int i, int clusterEnd,
            HashSet<int> bottomIndices, HashSet<int> topIndices,
            int profit, int? buyPrice, Dictionary<(int, int, int?), int> clusterProfitsAtPoint)
        {
            var key = (i, profit, buyPrice);
            if (!clusterProfitsAtPoint.ContainsKey(key))
            {
                clusterProfitsAtPoint[key] = GetInterferingPriceClusterMaxProfit(prices, i, clusterEnd, bottomIndices, topIndices, profit, buyPrice, clusterProfitsAtPoint);
            }
            
            return clusterProfitsAtPoint[key];
        }

        private int GetInterferingPriceClusterMaxProfit(int[] prices, int clusterStart, int clusterEnd,
            HashSet<int> bottomIndices, HashSet<int> topIndices,
            int profitThusFar, int? buyPrice, Dictionary<(int, int, int?), int> clusterProfitsAtPoint)
        {
            var profits = new List<int>();
            var profit = profitThusFar;

            for (int i = clusterStart; i <= clusterEnd; i++)
            {
                var price = prices[i];

                if (bottomIndices.Contains(i))
                {
                    //skip it
                    profits.Add(GetMaxRecursiveClusterProfit(prices, i + 1, clusterEnd, bottomIndices, topIndices, profit, buyPrice, clusterProfitsAtPoint));

                    //buy it 
                    buyPrice = price;

                }
                else if (topIndices.Contains(i))
                {
                    if(buyPrice.HasValue)
                    {
                        //skip it if sellable
                        profits.Add(GetMaxRecursiveClusterProfit(prices, i + 1, clusterEnd, bottomIndices, topIndices, profit, buyPrice, clusterProfitsAtPoint));

                        //sell it
                        profit += price - buyPrice.Value;
                        i++;    //skip after selling, cooldown effect
                        buyPrice = null;
                    }

                }
                else //point on a trend
                {
                    //buy it if follows the bottom
                    if(bottomIndices.Contains(i-1))
                    {
                        profits.Add(GetMaxRecursiveClusterProfit(prices, i + 1, clusterEnd, bottomIndices, topIndices, profit, price, clusterProfitsAtPoint));
                    }

                    if (topIndices.Contains(i + 1) && buyPrice.HasValue)
                    {
                        //skip it if sellable
                        profits.Add(GetMaxRecursiveClusterProfit(prices, i + 1, clusterEnd, bottomIndices, topIndices, profit, buyPrice, clusterProfitsAtPoint));

                        //sell it
                        profit += price - buyPrice.Value;
                        i++;    //skip after selling, cooldown effect
                        buyPrice = null;
                    }
                }
            }

            var maxProfit = 0;
            foreach (var item in profits)
            {
                maxProfit = Math.Max(maxProfit, item);
            }

            return Math.Max(maxProfit, profit);
        }

        private List<(int, int)> EnumerateIndependentPriceClusters(int[] prices, IList<int> bottomIndicesList, IList<int> topIndicesList)
        {
            var independentClusters = new List<(int, int)>();

            var nb = bottomIndicesList.Count;
            var nt = topIndicesList.Count;
            var startIndex = nb > 0 ? bottomIndicesList[0] : 0;
            var prevTopIndex = prices.Length;
            for (int i = 0, j = 0; i < nb && j < nt; i++, j++)
            {
                var bottomIndex = bottomIndicesList[i];
                var topIndex = topIndicesList[j];

                if (bottomIndex - prevTopIndex > 1)
                {
                    independentClusters.Add((startIndex, prevTopIndex));
                    startIndex = bottomIndex;
                }

                if (j == nt - 1)
                {
                    independentClusters.Add((startIndex, topIndex));
                }

                prevTopIndex = topIndex;
            }

            return independentClusters;
        }

        private (HashSet<int>, HashSet<int>) EnumerateAllLocalBottomsAndTops(int[] prices)
        {
            var bottomIndices = new HashSet<int>();
            var topIndices = new HashSet<int>();

            var n = prices.Length;
            for (int i = 0; i < n; i++)
            {
                var price = prices[i];

                var priceBefore = new int?();
                if (i > 0)
                {
                    priceBefore = prices[i - 1];
                }
                var priceAfter = new int?();
                if (i < prices.Length - 1)
                {
                    priceAfter = prices[i + 1];
                }

                if (priceBefore < price &&
                    (!priceAfter.HasValue || priceAfter <= price))
                {
                    topIndices.Add(i);
                }
                else if ((!priceBefore.HasValue || priceBefore >= price) &&
                    priceAfter > price)
                {
                    bottomIndices.Add(i);
                }
            }

            return (bottomIndices, topIndices);
        }
    }
}
