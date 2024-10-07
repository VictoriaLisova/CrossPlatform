using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    public class LogicTests
    {
        [Fact]
        public void Lab1_Logic_ReturnResult()
        {
            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string read_path = Path.Combine(projectDirectory, "input", "input_1.txt");
            var read_file = new ReadFile(read_path);
            read_file.Read();
            var maze = new Maze(read_file);
            maze.CreateMaze();
            var logic = new Logic(maze, read_file.Known_words);
            var expected = new List<char>() { 'A', 'E', 'N', 'R', 'S', 'W' };
            Assert.Equal(logic.GetResult(0, 0, 0), expected);
        }
    }
}
