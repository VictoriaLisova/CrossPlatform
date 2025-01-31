using Lab3;

namespace Lab3Test
{
    public class WriteToFileTest
    {
        [Fact]
        public void WriteToFileWriteTest()
        {
            string tempPath = Path.Combine(Path.GetTempPath(), "output.txt");
            var writrToFile = new WriteToFile(tempPath);
            writrToFile.Write(3);

            try
            {
                var streamReader = new StreamReader(tempPath);
                string? actual = streamReader.ReadLine();
                streamReader.Close();
                string expected = "Answer = 3";

                Assert.Equal(actual, expected);
            }
            finally
            {
                if (File.Exists(tempPath))
                    File.Delete(tempPath);
            }
        }
    }
}