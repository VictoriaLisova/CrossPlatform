namespace TestProject1
{
    public class ReadFileTests
    {
        private ReadFile GeInstance(string file_name)
        {
            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string read_path = Path.Combine(projectDirectory, "input", "input_1.txt");
            var read_file = new ReadFile(read_path);
            return read_file;
        }
        [Fact]
        public void Lab1_ReadFile_ReturnWordsMazeCount()
        {
            ReadFile read_file = GeInstance("input_1.txt");
            read_file.Read();
            int expected = 5;
            Assert.Equal(read_file.Words_maze_count, expected);
        }

        [Fact]
        public void Lab1_ReadFile_ReturnKnownWordsCount()
        {
            ReadFile read_file = GeInstance("input_1.txt");
            read_file.Read();
            int expected = 3;
            Assert.Equal(read_file.Words_find_count, expected);
        }

        [Fact]
        public void Lab1_ReadFile_ReturnWordMaze()
        {
            ReadFile read_file = GeInstance("input_1.txt");
            read_file.Read();
            List<string> expected = new List<string>() { "POLTE", "RWYMS", "OAIPT", "BDANR", "LEMES" };
            Assert.Equal(read_file.Maze_words, expected);
        }

        [Fact]
        public void Lab1_ReadFile_ReturnKnownListOfWords()
        {
            ReadFile read_file = GeInstance("input_1.txt");
            read_file.Read();
            List<string> expected = new List<string>() { "OLYMPIAD", "PROBLEM", "TEST" };
            Assert.Equal(read_file.Known_words, expected);
        }
        [Fact]
        public void Lab1_ReadFile_ReturnFileNotFoundExeption()
        {
            ReadFile read_file = GeInstance("input_3.txt");
            var excpected = new FieldAccessException().Message;
            try
            {
                read_file.Read();
            }
            catch (Exception ex)
            {
                Assert.IsType<FileNotFoundException>(ex);
                Assert.Equal(ex.Message, excpected);
            }
        }
    }
}