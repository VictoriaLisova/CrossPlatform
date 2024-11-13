using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5ClassLibrary
{
    public class Lab2 : ILabNumber
    {
        public string? PathToInputFile { get; set; }
        //public string? PathToOutputFile { get; set; }
        private bool CheckCondition(int value, int conditionOne, int contionTwo)
        {
            return (value >= conditionOne && value <= contionTwo);
        }

        public string Run()
        {
            string projectDirectory = AppContext.BaseDirectory;
            string result = "";
            try
            {
                Lab2Classes.ReadFile readFile = new Lab2Classes.ReadFile(PathToInputFile);
                readFile.Read();

                bool checkN = CheckCondition(readFile.N, 1, 100);
                bool checkK = CheckCondition(readFile.K, 1, 100);
                bool checkT = CheckCondition(readFile.T, 1, 30000);
                bool checkTi = CheckCondition(readFile.GansterComeMomments.Count, 0, readFile.T);
                bool checkPi = CheckCondition(readFile.Money.Count, 1, 300);
                bool checkSi = CheckCondition(readFile.DoorOpeness.Count, 1, readFile.K);

                if (checkN && checkK && checkT && checkTi && checkPi && checkSi)
                {
                    Lab2Classes.SolveLogic logic = new Lab2Classes.SolveLogic(readFile);
                    int answer = logic.Solve();
                    Console.WriteLine("Result = " + answer);
                    result = answer.ToString();
                    //WriteToFile writeToFile = new WriteToFile();
                    //writeToFile.Write(PathToOutputFile, answer);
                }
                else
                {
                    Console.WriteLine("Incorrect data, check cinditions:" +
                        " 1 <= N <= 100, 1 <= K <= 100, 1 <= T <= 30 000, 0 <= Ti <= T, 1 <= Pi <= 300, 1 <= Si <= K");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
