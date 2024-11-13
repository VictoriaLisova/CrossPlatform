namespace Lab5ClassLibrary
{
    public class Lab1 : ILabNumber
    {
        public string? PathToInputFile { get; set; }
        //public string? PathToOutputFile { get; set; }
        public string Run()
        {
            string result = "";
            try
            {
                var read_file = new Lab1Classes.ReadFile(PathToInputFile);
                read_file.Read();

                var maze = new Lab1Classes.Maze(read_file);
                maze.CreateMaze();
                Lab1Classes.Logic logic = new Lab1Classes.Logic(maze, read_file.Known_words);
                //string write_path = PathToOutputFile;
                logic.ShowResult(0, 0, 0);
                result = logic.Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}