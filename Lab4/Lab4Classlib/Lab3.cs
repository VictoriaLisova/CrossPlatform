using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4Classlib
{
    public class Lab3 : ILabNumber
    {
        public string? PathToInputFile { get; set; }
        public string? PathToOutputFile { get; set; }
        private bool isCorrectTimeFormat(List<Lab3Classes.Trip> trips)
        {
            int startLength = trips.Count;
            return trips.Where(t => t.StartTime >= 0 && t.StartTime <= 10000 && t.EndTime >= 0 && t.EndTime <= 10000).Count() == startLength;
        }

        public void Run()
        {
            string baseDirectory = AppContext.BaseDirectory;
            //string pathToFile = Path.Combine(baseDirectory, "input", "input.txt");
            try
            {
                var readFile = new Lab3Classes.ReadFile(PathToInputFile);

                readFile.Read();

                if (readFile.NumberOfVillages >= 1 && readFile.NumberOfVillages <= 100
                    && readFile.NumberOfBusTrips >= 0 && readFile.NumberOfBusTrips <= 10000 && isCorrectTimeFormat(readFile.Trips))
                {
                    var solve = new Lab3Classes.Solve(readFile.NumberOfVillages);
                    foreach (var item in readFile.Trips)
                    {
                        solve.Add(item.StartVillageNumber - 1, item.EndVillageNumber - 1, Math.Abs(item.EndTime - item.StartTime));
                    }

                    int answer = solve.FindPath(readFile.StartVillageNumber - 1, readFile.EndVillageNumber - 1);
                    Console.WriteLine(answer);

                    //string pathToWrite = Path.Combine(baseDirectory, "output", "output.txt");
                    var writeFile = new WriteToFile();
                    writeFile.Write(PathToOutputFile, answer);
                }
                else
                {
                    Console.WriteLine("Please check data. They must be: 1 <= N <= 100, 0 <= R <= 10 000, 0 <= time <= 10 000");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
