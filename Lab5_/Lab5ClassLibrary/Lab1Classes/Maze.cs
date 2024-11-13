using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5ClassLibrary.Lab1Classes
{
    public class Maze
    {
        private ReadFile readFile;
        private char[,] maze;
        public Maze(ReadFile readFile)
        {
            this.readFile = readFile;
        }

        private char GetLetter(string word, int index)
        {
            return word[index];
        }

        public char[,] CreateMaze()
        {
            maze = new char[readFile.Words_maze_count, readFile.Words_maze_count];
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    maze[i, j] = GetLetter(readFile.Maze_words[i], j);
                }
            }
            return maze;
        }
    }
}
