using Lab2;
using Xunit;

namespace Lab2Test
{
    public class ReadFileTest
    {
        private ReadFile GetReadFileResults()
        {
            string projectDirectory = AppContext.BaseDirectory;
            string read_path = Path.Combine(projectDirectory, "input", "input_2.txt");

            var readFile = new ReadFile(read_path);
            readFile.Read();
            return readFile;
        }
        public static IEnumerable<object[]> GetReadedTestData()
        {
            var readFile = new ReadFileTest().GetReadFileResults();
            yield return new object[] { readFile.N, 2 };
            yield return new object[] { readFile.K, 17 };
            yield return new object[] { readFile.T, 10 };
        }

        static IEnumerable<object[]> GetReadedcollectionsTestData()
        {
            var readFile = new ReadFileTest().GetReadFileResults();
            yield return new object[] { readFile.GansterComeMomments, new List<int>() { 5, 5 } };
            yield return new object[] { readFile.Money, new List<int>() { 3, 3 } };
            yield return new object[] { readFile.DoorOpeness, new List<int>() { 6, 1 } };
        }

        [Theory]
        [MemberData(nameof(GetReadedTestData))]
        public void ReadFileReadReturnsTest(int actual, int expected)
        {
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(GetReadedcollectionsTestData))]
        public void ReadFileReadReturnsCollectionsTest(List<int> actual, List<int> expected)
        {
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void ReadFileFileNotFoundTest()
        {
            string projectDirectory = AppContext.BaseDirectory;
            string read_path = Path.Combine(projectDirectory, "input", "input_4.txt");
            var readFile = new ReadFile(read_path);

            var exception = Assert.Throws<FileNotFoundException>(() => readFile.Read());

            Assert.Contains(read_path, exception.Message);
        }
    }
}