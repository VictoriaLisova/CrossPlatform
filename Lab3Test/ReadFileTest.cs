using Lab3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3Test
{
    public class ReadFileTest
    {
        private ReadFile CalcData()
        {
            string baseDir = AppContext.BaseDirectory;
            string path = Path.Combine(baseDir, "input", "test.txt");
            var readFile = new ReadFile(path);
            readFile.Read();
            return readFile;
        }

        public static IEnumerable<object[]> GetData()
        {
            var readFile = new ReadFileTest().CalcData();
            yield return new object[] { readFile.NumberOfVillages, 3 };
            yield return new object[] { readFile.StartVillageNumber, 1 };
            yield return new object[] { readFile.EndVillageNumber, 3 };
            yield return new object[] { readFile.NumberOfBusTrips, 2 };
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void ReadFileReadTestReturnsValues(int actual, int expected)
        {
            Assert.Equal(actual, expected);
        }

        [Fact]
        public void ReadFileReadTestReturnsCollection()
        {
            var actual = CalcData().Trips;
            var expected = new List<Trip> { new Trip(1, 0, 2, 5), new Trip(2, 5, 1, 10) };

            Assert.Equal(actual.Count, expected.Count);
            Assert.All(expected, item => actual.Contains(item));
        }
    }
}
