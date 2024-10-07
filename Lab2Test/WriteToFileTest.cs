using Lab2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Test
{
    public class WriteToFileTest
    {
        private string path1;
        private string path2;
        private WriteToFile writeToFile;
        public WriteToFileTest()
        {
            var baseDir = AppContext.BaseDirectory;
            this.path1 = Path.Combine(baseDir, "output", "output.txt");
            this.path2 = Path.Combine(baseDir, "output_data", "output.txt");
            writeToFile = new WriteToFile();
        }

        [Fact]
        public void WriteToFileWritePathExeption()
        {
            Assert.Throws<DirectoryNotFoundException>(() => writeToFile.WriteData(path2, 26));
        }

        [Fact]
        public void WriteToFileWriteCorrectPath()
        {
            writeToFile.WriteData(path1, 26);
            var srteamReader = new StreamReader(path1);
            string ? actual = srteamReader.ReadLine();

            string expect = "Result = 26";

            Assert.Equal(actual, expect);
        }
    }
}
