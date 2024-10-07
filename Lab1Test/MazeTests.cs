using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    public class MazeTests
    {
        [Fact]
        public void Lab1_Maze_CreateMaze_ReturnMaze()
        {
            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string read_path = Path.Combine(projectDirectory, "input", "input_1.txt");
            var read_file = new ReadFile(read_path);
            read_file.Read();
            var maze = new Maze(read_file);
            char[,] expected = new char[5, 5] {
                { 'P', 'O', 'L', 'T', 'E'},
                { 'R', 'W', 'Y', 'M', 'S' },
                { 'O', 'A', 'I', 'P', 'T' },
                { 'B', 'D', 'A', 'N', 'R' },
                { 'L', 'E', 'M', 'E', 'S' } };
            Assert.Equal(maze.CreateMaze(), expected);
        }
    }
}
