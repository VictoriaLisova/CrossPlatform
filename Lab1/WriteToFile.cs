using System;

public class WriteToFile
{
    private string result = "";
    private string path;
    public WriteToFile(List<char> answer, string path)
    {
        foreach (var letter in answer)
            result += letter;
        this.path = path;
    }

    public void Write()
    {
        try
        {
            var stream_writer = new StreamWriter(path);
            stream_writer.Write("Answer: " + result);
            stream_writer.Close();
            Console.WriteLine($"Data has been written to a file: {path}");
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
