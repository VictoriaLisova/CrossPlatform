using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasslibLab3
{
    public class Solve
    {
        private int verticleAmount;
        private List<Tuple<int, int>>[] edges;
        private int[] shortestTime;
        public Solve(int verticleAmount)
        {
            this.verticleAmount = verticleAmount;
            edges = new List<Tuple<int, int>>[verticleAmount];
            shortestTime = new int[verticleAmount];
            for (int i = 0; i < verticleAmount; i++)
            {
                shortestTime[i] = int.MaxValue;
                edges[i] = new List<Tuple<int, int>>();
            }
            shortestTime[0] = 0;
        }

        public void Add(int verticleFrom, int verticleTo, int time)
        {
            edges[verticleFrom].Add(Tuple.Create(verticleTo, time));
            edges[verticleTo].Add(Tuple.Create(verticleFrom, time));
        }

        public int FindPath(int verticleFrom, int verticleTo)
        {
            int edgeIndex = verticleFrom;
            var queue = new PriorityQueue<int, int>();
            queue.Enqueue(verticleFrom, 0);
            bool isReached = false;
            while (queue.Count > 0)
            {
                edgeIndex = queue.Dequeue();
                foreach (var edge in edges[edgeIndex])
                {
                    if (shortestTime[edge.Item1] == int.MaxValue)
                    {
                        shortestTime[edge.Item1] = shortestTime[edgeIndex] + edge.Item2;
                        queue.Enqueue(edge.Item1, shortestTime[edge.Item1]);
                    }
                    if (edge.Item1 == verticleTo)
                    {
                        isReached = true;
                    }
                }
            }

            return isReached ? shortestTime[verticleTo] : -1;
        }
    }
}
