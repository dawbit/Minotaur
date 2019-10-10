using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minotaur.Algorithms
{
    static class Prims
    {
        static public void Generate(int w, int h)
        {
            int size = Variables.Instance.size;
            Cell[,] maze = new Cell[w, h];
            bool[,] visited = new bool[w, h];
            List<Cell> frontier = new List<Cell>();
            var r = new Random();

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

            visited[x, y] = true;

            if (x - 1 >= 0)
                frontier.Add(maze[x - 1, y]);
            if (x + 1 < w)
                frontier.Add(maze[x + 1, y]);
            if (y - 1 >= 0)
                frontier.Add(maze[x, y - 1]);
            if (y + 1 < h)
                frontier.Add(maze[x, y + 1]);

            List<Cell> neighbours = new List<Cell>();
            int index, x2, y2;

            while(frontier.Count > 0)
            {
                index = r.Next(frontier.Count);

                x = frontier[index].X;
                y = frontier[index].Y;
                visited[x, y] = true;
                frontier.RemoveAt(index);

                if(x - 1 >= 0) //left
                {
                    if(visited[x - 1, y])
                    {
                        neighbours.Add(maze[x - 1, y]);
                    }
                    else if(!frontier.Contains(maze[x - 1, y]))
                    {
                        frontier.Add(maze[x - 1, y]);
                    }
                }

                if (x + 1 < w) //right
                {
                    if (visited[x + 1, y])
                    {
                        neighbours.Add(maze[x + 1, y]);
                    }
                    else if (!frontier.Contains(maze[x + 1, y]))
                    {
                        frontier.Add(maze[x + 1, y]);
                    }
                }

                if (y - 1 >= 0) //top
                {
                    if (visited[x, y - 1])
                    {
                        neighbours.Add(maze[x, y - 1]);
                    }
                    else if (!frontier.Contains(maze[x, y - 1]))
                    {
                        frontier.Add(maze[x, y - 1]);
                    }
                }

                if(y + 1 < h) //bottom
                {
                    if (visited[x, y + 1])
                    {
                        neighbours.Add(maze[x, y + 1]);
                    }
                    else if (!frontier.Contains(maze[x, y + 1]))
                    {
                        frontier.Add(maze[x, y + 1]);
                    }
                }

                index = r.Next(neighbours.Count);

                x2 = neighbours[index].X;
                y2 = neighbours[index].Y;

                if(x2 == x - 1)
                {
                    maze[x - 1, y].Walls[1] = false;
                    maze[x, y].Walls[3] = false;
                }
                else if(x2 == x + 1)
                {
                    maze[x + 1, y].Walls[3] = false;
                    maze[x, y].Walls[1] = false;
                }
                else if(y2 == y - 1)
                {
                    maze[x, y - 1].Walls[2] = false;
                    maze[x, y].Walls[0] = false;
                }
                else if(y2 == y + 1)
                {
                    maze[x, y + 1].Walls[0] = false;
                    maze[x, y].Walls[2] = false;
                }

                neighbours.Clear();
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
