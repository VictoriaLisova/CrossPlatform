using ClasslibLab3;

internal class Program
{
    static bool isCorrectTimeFormat(List<Trip> trips)
    {
        int startLength = trips.Count;
        return trips.Where(t=>t.StartTime >= 0 && t.StartTime <= 10000 && t.EndTime >= 0 && t.EndTime <= 10000).Count() == startLength;
    }
    static void Main(string[] args)
    {
        string baseDirectory = AppContext.BaseDirectory;
        string pathToFile = Path.Combine(baseDirectory, "input", "input.txt");
        var readFile = new ReadFile(pathToFile);

        readFile.Read();

        if(readFile.NumberOfVillages >= 1 && readFile.NumberOfVillages <= 100 
            && readFile.NumberOfBusTrips >= 0 && readFile.NumberOfBusTrips <= 10000 && isCorrectTimeFormat(readFile.Trips))
        {
            var solve = new Solve(readFile.NumberOfVillages);
            foreach (var item in readFile.Trips)
            {
                solve.Add(item.StartVillageNumber - 1, item.EndVillageNumber - 1, Math.Abs(item.EndTime - item.StartTime));
            }

            int answer = solve.FindPath(readFile.StartVillageNumber - 1, readFile.EndVillageNumber - 1);
            Console.WriteLine(answer);

            string pathToWrite = Path.Combine(baseDirectory, "output", "output.txt");
            var writeFile = new WriteToFile(pathToWrite);
            writeFile.Write(answer);
        }
        else
        {
            Console.WriteLine("Please check data. They must be: 1 <= N <= 100, 0 <= R <= 10 000, 0 <= time <= 10 000");
        }
    }
}