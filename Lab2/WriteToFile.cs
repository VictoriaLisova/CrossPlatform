using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class WriteToFile
    {
        public void WriteData(string path, int answer)
        {
            try
            {
                var stream_writer = new StreamWriter(path);
                stream_writer.Write("Result = " + answer);
                stream_writer.Close();
                Console.WriteLine($"Data has been written to a file: {path}");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
