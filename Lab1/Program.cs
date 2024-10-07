using System;
using System.IO;
internal class Program
{
    static void Main(string[] args)
    {
        string projectDirectory = AppContext.BaseDirectory;
        string read_path = Path.Combine(projectDirectory, "input", "input_1.txt");
        var read_file = new ReadFile(read_path);
        read_file.Read();

        var maze = new Maze(read_file);
        maze.CreateMaze();
        Logic logic = new Logic(maze, read_file.Known_words);

        string write_path = Path.Combine(projectDirectory, "output", "output.txt");

        logic.ShowResult(0, 0, 0, write_path);
    }
}
