using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class ReadFile
    {
        private string path;
        public int N { get; set; }
        public int K { get; set; }
        public int T { get; set; }
        public List<int> Money { get; set; } = new List<int>(); //Pi - 3 line
        public List<int> DoorOpeness { get; set; } = new List<int>(); // Si - 4 line
        public List<int> GansterComeMomments { get; set; } = new List<int>(); // Ti - 2 line
        public ReadFile(string path)
        {
            this.path = path;
        }

        private List<int> ParseToNumber(string[] variables)
        {
            var result = new List<int>();
            for (int i = 0; i < variables.Length; i++)
                result.Add(int.Parse(variables[i]));
            return result;
        }

        private void ParseLine(string line, int line_index)
        {
            var variables = line.Split(' ');
            if (variables.Length == N)
            {
                var result_variables = ParseToNumber(variables);
                if (line_index == 1)
                {
                    GansterComeMomments.AddRange(result_variables);
                }
                else if (line_index == 2)
                {
                    Money.AddRange(result_variables);
                }
                else if (line_index == 3)
                {
                    DoorOpeness.AddRange(result_variables);
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        private void ParseFirstLine(string line)
        {
            if (line != null)
            {
                var variables = line.Split(' ');
                if (variables.Length == 3)
                {
                    N = int.Parse(variables[0]);
                    K = int.Parse(variables[1]);
                    T = int.Parse(variables[2]);
                }
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public void Read()
        {
            try
            {
                var stream_reader = new StreamReader(path);
                string line = stream_reader.ReadLine();
                ParseFirstLine(line);
                int line_index = 1;
                while (line != null && line_index < 4)
                {
                    line = stream_reader.ReadLine();
                    ParseLine(line, line_index);
                    line_index++;
                }
                stream_reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
