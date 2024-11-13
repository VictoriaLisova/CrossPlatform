using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5ClassLibrary.Lab1Classes
{
    public class ReadFile
    {
        private string file_path;
        public int Words_maze_count { get; set; }
        public int Words_find_count { get; set; }
        public List<string> Maze_words { get; set; } = new List<string>();
        public List<string> Known_words { get; set; } = new List<string>();
        public ReadFile(string file_path)
        {
            this.file_path = file_path;
        }

        private void ParseLine(string line)
        {
            try
            {
                Words_maze_count = int.Parse(line.Split(' ')[0]);
                Words_find_count = int.Parse(line.Split(' ')[1]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Read()
        {
            try
            {
                var stream_reader = new StreamReader(file_path);
                var line = stream_reader.ReadLine();
                ParseLine(line);
                while (line != null)
                {
                    line = stream_reader.ReadLine();
                    GetMazeWords(line);
                }
                stream_reader.Close();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void GetMazeWords(string word)
        {
            if (Maze_words.Count != Words_maze_count)
                Maze_words.Add(word);
            else
                GetKnownsWords(word);
        }

        private void GetKnownsWords(string line)
        {
            if (Known_words.Count != Words_find_count)
                Known_words.Add(line);
        }
    }
}
