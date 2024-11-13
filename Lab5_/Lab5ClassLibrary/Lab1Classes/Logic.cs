using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5ClassLibrary.Lab1Classes
{
    public class Logic
    {
        private Maze maze;
        private List<string> words = new List<string>();
        private char[,] maze_table;
        private bool[,] is_cross;
        private int size;
        private int number;
        public List<char> answer = new List<char>();
        public string Result { get; set; }
        public Logic(Maze maze, List<string> words)
        {
            this.maze = maze;
            this.words = words.OrderByDescending(w => w.Length).ToList();
            maze_table = maze.CreateMaze();
            size = maze_table.GetLength(0);
            is_cross = new bool[size, size];
            number = 0;
        }

        private void Helper(int i, int j, int index, string word)
        {
            if (index + 1 != word.Length)
            {
                if (i + 1 < size && maze_table[i + 1, j] == word[index + 1] && !is_cross[i + 1, j])
                {
                    is_cross[i, j] = true;
                    index++;
                    SolveMaze(i + 1, j, index);
                }
                else if (i - 1 >= 0 && maze_table[i - 1, j] == word[index + 1] && !is_cross[i - 1, j])
                {
                    is_cross[i, j] = true;
                    index++;
                    SolveMaze(i - 1, j, index);
                }
                else if (j + 1 < size && maze_table[i, j + 1] == word[index + 1] && !is_cross[i, j + 1])
                {
                    is_cross[i, j] = true;
                    index++;
                    SolveMaze(i, j + 1, index);
                }
                else if (j - 1 >= 0 && maze_table[i, j - 1] == word[index + 1] && !is_cross[i, j - 1])
                {
                    is_cross[i, j] = true;
                    index++;
                    SolveMaze(i, j - 1, index);
                }
            }
            else
            {
                is_cross[i, j] = true;
                number = CheckWordNumber(number, index, word);
                SolveMaze(0, 0, 0);
            }
        }

        private int CheckWordNumber(int number, int index, string word)
        {
            if (index == word.Length - 1 && number + 1 < words.Count)
                number++;
            return number;
        }

        private void SolveMaze(int row, int col, int index)
        {
            string word = words[number];
            for (int i = row; i < size; i++)
            {
                for (int j = col; j < size; j++)
                {
                    if (maze_table[i, j] == word[index] && !is_cross[i, j])
                    {
                        Helper(i, j, index, word);
                    }
                }
            }
        }

        public List<char> GetResult(int row, int col, int index)
        {
            SolveMaze(row, col, index);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (!is_cross[i, j])
                    {
                        answer.Add(maze_table[i, j]);
                    }
                }
            }
            return answer.OrderBy(i => i).ToList();
        }

        public void ShowResult(int row, int col, int index)
        {
            var sorted_answer = GetResult(row, col, index);
            var write_to_file = new WriteToFile();

            string result = "";
            foreach (var letter in sorted_answer)
                result += letter;
            Result = result;
            //write_to_file.Write(write_path, result);
            foreach (var item in sorted_answer)
                Console.Write(item);
            Console.WriteLine();
        }
    }
}
