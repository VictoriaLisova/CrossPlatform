using Lab2;

internal class Program
{
    static bool CheckCondition(int value, int conditionOne, int contionTwo)
    {
        return (value >= conditionOne && value <= contionTwo);
    }
    static void Main(string[] args)
    {
        string projectDirectory = AppContext.BaseDirectory;
        string read_path = Path.Combine(projectDirectory, "input", "input_1.txt");

        ReadFile readFile = new ReadFile(read_path);
        readFile.Read();

        bool checkN = CheckCondition(readFile.N, 1, 100);
        bool checkK = CheckCondition(readFile.K, 1, 100);
        bool checkT = CheckCondition(readFile.T, 1, 30000);
        bool checkTi = CheckCondition(readFile.GansterComeMomments.Count, 0, readFile.T);
        bool checkPi = CheckCondition(readFile.Money.Count, 1, 300);
        bool checkSi = CheckCondition(readFile.DoorOpeness.Count, 1, readFile.K);

        if (checkN && checkK && checkT && checkTi && checkPi && checkSi)
        {
            SolveLogic logic = new SolveLogic(readFile);
            int answer = logic.Solve();
            Console.WriteLine("Result = " + answer);

            string path_to_save = Path.Combine(projectDirectory, "output", "output.txt");
            WriteToFile writeToFile = new WriteToFile();
            writeToFile.WriteData(path_to_save, answer);
        }
        else
        {
            Console.WriteLine("Incorrect data, check cinditions:" +
                " 1 <= N <= 100, 1 <= K <= 100, 1 <= T <= 30 000, 0 <= Ti <= T, 1 <= Pi <= 300, 1 <= Si <= K");
        }
    }
}
