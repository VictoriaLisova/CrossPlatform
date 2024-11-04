using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4Classlib
{
    public class Lab1 : ILabNumber
    {
        public string? PathToInputFile { get; set; }
        public string? PathToOutputFile { get; set; } 
        public void Run()
        {
            try
            {
                //string projectDirectory = AppContext.BaseDirectory;
                //string read_path = Path.Combine(projectDirectory, "input", "input_1.txt");
                var read_file = new Lab1Classes.ReadFile(PathToInputFile);
                read_file.Read();

                var maze = new Lab1Classes.Maze(read_file);
                maze.CreateMaze();
                Lab1Classes.Logic logic = new Lab1Classes.Logic(maze, read_file.Known_words);

                //string write_path = Path.Combine(projectDirectory, "output", "output.txt");
                string write_path = PathToOutputFile;

                logic.ShowResult(0, 0, 0, write_path);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
