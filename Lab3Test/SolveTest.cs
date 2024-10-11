using ClasslibLab3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3Test
{
    public class SolveTest
    {
        private Solve GetSolveObject()
        {
            var solve = new Solve(4);
            solve.Add(0, 1, 2);
            solve.Add(0, 2, 6);
            solve.Add(1, 3, 5);
            solve.Add(2, 3, 8);
            solve.Add(0, 3, 3);
            return solve;
        }

        public static IEnumerable<object[]> GetData()
        {
            var solve = new SolveTest().GetSolveObject();
            yield return new object[] { solve.FindPath(0, 1), 2 };
            yield return new object[] { solve.FindPath(0, 2), 6 };
            yield return new object[] { solve.FindPath(0, 3), 3 };
            yield return new object[] { solve.FindPath(0, 4), -1 };
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void SolveFindPathTest(int actual, int expected)
        {
            Assert.Equal(actual, expected);
        }
    }
}
