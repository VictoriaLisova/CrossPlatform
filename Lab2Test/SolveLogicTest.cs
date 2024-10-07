using Lab2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Test
{
    public class SolveLogicTest
    {
        private SolveLogic solve;
        public SolveLogicTest()
        {
            string baseDirectory = AppContext.BaseDirectory;
            string read_path = Path.Combine(baseDirectory, "input", "input_1.txt");
            var readFile = new ReadFile(read_path);

            readFile.N = 2;
            readFile.K = 17;
            readFile.T = 10;
            readFile.GansterComeMomments = new List<int> { 5, 5 };
            readFile.Money = new List<int> { 3, 3 };
            readFile.DoorOpeness = new List<int> { 6, 1 };

            this.solve = new SolveLogic(readFile);
        }

        [Fact]
        public void SolveLogicSolveReturnsTest()
        {
            Assert.Equal(0, solve.Solve());
        }
    }
}
