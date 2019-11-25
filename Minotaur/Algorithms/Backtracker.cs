using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minotaur.Algorithms
{
    class Backtracker
    {
        static public void Generate(int w, int h)
        {
            int size = Variables.Instance.size;
            Cell[,] maze = new Cell[w, h];
            bool[,] visited = new bool[w, h];
            List<Cell> frontier = new List<Cell>();
            var r = new Random();

            Stack<int> stackX = new Stack<int>();
            Stack<int> stackY = new Stack<int>();


            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    maze[i, j] = new Cell(i, j);
                    visited[i, j] = false;
                }
            }

            int x = r.Next(w);
            int y = r.Next(h);

            visited[x, y] = true; //First visited cell
            stackX.Push(x); //X coord of first cell in stack
            stackY.Push(y); //Y coord of first cell in stack

            CheckForNeighbours(x, y);

            bool end = true; //Trigger to quit the while loop

            while (end)
            {
                int move = r.Next(frontier.Count); //choosing which way to go

                //Getting coordinates of the move
                int x2 = frontier[move].X;
                int y2 = frontier[move].Y;

                //Adding next cell to stack
                stackX.Push(x2);
                stackY.Push(y2);

                //Adding next cell to visited
                visited[x2, y2] = true;

                #region deleting walls

                if (x2 == x - 1)
                {
                    maze[x - 1, y].Walls[1] = false;
                    maze[x, y].Walls[3] = false;
                }
                else if (x2 == x + 1)
                {
                    maze[x + 1, y].Walls[3] = false;
                    maze[x, y].Walls[1] = false;
                }
                else if (y2 == y - 1)
                {
                    maze[x, y - 1].Walls[2] = false;
                    maze[x, y].Walls[0] = false;
                }
                else if (y2 == y + 1)
                {
                    maze[x, y + 1].Walls[0] = false;
                    maze[x, y].Walls[2] = false;
                }
                #endregion


                //Cleaning list of neighbours
                frontier.Clear();

                CheckForNeighbours(x2, y2);
                x = x2;
                y = y2;

                bool flag = false;

                foreach (var item in visited)
                {
                    if (item == false)
                    {
                        flag = true;
                    }
                }

                //If all cells were allready visited, then end the while loop
                if (!flag)
                {
                    end = false;
                }

                //Backtracking
                while ((frontier.Count == 0) && (end))
                {
                    stackX.Pop();
                    stackY.Pop();
                    if ((stackX.Count != 0) && (stackY.Count != 0))
                    {
                        x2 = stackX.Peek();
                        y2 = stackY.Peek();
                        CheckForNeighbours(x2, y2);
                        x = x2;
                        y = y2;
                    }
                }

            }

            void CheckForNeighbours(int a, int b)
            {
                if ((a - 1 >= 0) && !(visited[a - 1, b])) //Checking if there is a move left and the cell wasn't visited yet
                    frontier.Add(maze[a - 1, b]);
                if ((a + 1 < w) && !(visited[a + 1, b])) //Checking if there is a move right and the cell wasn't visited yet
                    frontier.Add(maze[a + 1, b]);
                if ((b - 1 >= 0) && !(visited[a, b - 1])) //Checking if there is a move up and the cell wasn't visited yet
                    frontier.Add(maze[a, b - 1]);
                if ((b + 1 < h) && !(visited[a, b + 1])) //Checking if there is a move down and the cell wasn't visited yet
                    frontier.Add(maze[a, b + 1]);
            }

            string json = JsonConvert.SerializeObject(maze);
            string path = Variables.Instance.path + "\\" + DateTime.Now.ToString("MM-dd-yyyy_h-mm-ss") + ".json";

            using (var tw = new StreamWriter(path, true))
            {
                tw.WriteLine(json.ToString());
                tw.Close();
            }

        }
    }
}
