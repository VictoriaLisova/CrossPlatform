using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class SolveLogic
    {
        private int n;
        private int k;
        private int t;
        private List<int> money = new List<int>();
        private List<int> doorOpeness = new List<int>();
        public List<int> gansterComeMomments = new List<int>();
        public SolveLogic(ReadFile readFile)
        {
            n = readFile.N;
            k = readFile.K;
            t = readFile.T;
            money = readFile.Money;
            doorOpeness = readFile.DoorOpeness;
            gansterComeMomments = readFile.GansterComeMomments;
        }

        public int Solve()
        {
            int[,] matrix = new int[t + 1, k + 1];
            matrix[0, 0] = 0;
            for (int i = 1; i <= t; i++)
            {
                for (int j = 1; j <= k; j++)
                {

                    // open for hour

                    if (j + 1 <= k && matrix[i, j] <= matrix[i - 1, j + 1])
                    {
                        matrix[i, j] = matrix[i - 1, j + 1];
                    }

                    // close for hour

                    if (j - 1 >= 1 && matrix[i, j] <= matrix[i - 1, j - 1])
                    {
                        matrix[i, j] = matrix[i - 1, j - 1];
                    }

                    // stay in the same state

                    if (matrix[i, j] <= matrix[i - 1, j])
                    {
                        matrix[i, j] = matrix[i - 1, j];
                    }

                    for (int g = 0; g < n; g++)
                    {
                        if (i == gansterComeMomments[g] && j == doorOpeness[g])
                        {
                            matrix[i, j] += money[g];
                        }
                    }
                }
            }
            return matrix[t, k];
        }
    }
}
