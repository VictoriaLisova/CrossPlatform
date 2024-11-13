using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5ClassLibrary
{
    public class WriteToFile
    {
        public void Write(string path, object answer)
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
