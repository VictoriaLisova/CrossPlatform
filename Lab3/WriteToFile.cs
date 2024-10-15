using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class WriteToFile
    {
        private string path;
        public WriteToFile(string path)
        {
            this.path = path;
        }

        public void Write(int answer)
        {
            try
            {
                var streamWriter = new StreamWriter(path);
                streamWriter.Write($"Answer = {answer}");
                Console.WriteLine($"The data is written by: {path}");
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
