using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minotaur.Algorithms
{
    static class HuntAndKill
    {
        static public void Generate(int w, int h)
        {
            int size = Variables.Instance.size;
            Cell[,] grid = new Cell[w, h];

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    grid[i, j] = new Cell(i, j);
                }
            }


            bool[,] visited = new bool[w, h];

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    visited[i, j] = false;
                }
                    
            }


            Random random = new Random();
            int x = random.Next(0, w);
            int y = random.Next(0, h); 
            visited[x, y] = true;
            List<Cell> neighbours = new List<Cell>();
            int iterator = 0;


            do
            {
                neighbours.Clear();

                if (y - 1 >= 0 && !visited[x, y - 1])
                {
                    neighbours.Add(grid[x, y - 1]);
                } // do góry
                if (x + 1 < w && !visited[x + 1, y])
                {
                    neighbours.Add(grid[x + 1, y]);
                } //w prawo
                if (y + 1 < h && !visited[x, y + 1])
                {
                    neighbours.Add(grid[x, y + 1]);
                } // na dół
                if (x - 1 >= 0 && !visited[x - 1, y])
                {
                    neighbours.Add(grid[x - 1, y]);
                }// w lewo

                if (neighbours.Count == 0)
                {
                    bool ok = false;
                    int i = 0;
                    int j = 0;
                    while (!ok && i < w && j < h)
                    {
                        if (visited[i, j] == false)
                        {
                            neighbours.Clear();

                            if (j - 1 >= 0 && visited[i, j - 1])
                            {
                                neighbours.Add(grid[i, j - 1]);
                            } // do góry
                            if (i + 1 < w && visited[i + 1, j])
                            {
                                neighbours.Add(grid[i + 1, j]);
                            } //w prawo
                            if (j + 1 < h && visited[i, j + 1])
                            {
                                neighbours.Add(grid[i, j + 1]);
                            } // na dół
                            if (i - 1 >= 0 && visited[i - 1, j])
                            {
                                neighbours.Add(grid[i - 1, j]);
                            }// w lewo

                            if(neighbours.Count > 0)
                            {
                                ok = true;
                                x = i;
                                y = j;
                                visited[x, y] = true;

                                int temp = random.Next(0, neighbours.Count);
                                Cell neighbour = neighbours[temp];
                                int nx = neighbour.X;
                                int ny = neighbour.Y;

                                if (nx == x && ny < y) //do góry
                                {
                                    grid[nx, ny].Walls[2] = false;
                                    grid[x, y].Walls[0] = false;
                                }
                                if (nx > x && ny == y) // na prawo
                                {
                                    grid[nx, ny].Walls[3] = false;
                                    grid[x, y].Walls[1] = false;
                                }
                                if (nx == x && ny > y) // na dół
                                {
                                    grid[nx, ny].Walls[0] = false;
                                    grid[x, y].Walls[2] = false;
                                }
                                if (nx < x && ny == y) // w lewo
                                {
                                    grid[nx, ny].Walls[1] = false;
                                    grid[x, y].Walls[3] = false;
                                }
                            }
                        }

                        i++;

                        if(i == w)
                        {
                            j++;
                            i = 0;
                        }
                    }

                    iterator++;
                }
                else
                {

                    int temp = random.Next(0, neighbours.Count);

                    Cell neighbour = neighbours[temp];
                    int nx = neighbour.X;
                    int ny = neighbour.Y;
                    visited[nx, ny] = true;

                    if (nx == x && ny < y) //do góry
                    {
                        grid[nx, ny].Walls[2] = false;
                        grid[x, y].Walls[0] = false;
                    }
                    if (nx > x && ny == y) // na prawo
                    {
                        grid[nx, ny].Walls[3] = false;
                        grid[x, y].Walls[1] = false;
                    }
                    if (nx == x && ny > y) // na dół
                    {
                        grid[nx, ny].Walls[0] = false;
                        grid[x, y].Walls[2] = false;
                    }
                    if (nx < x && ny == y) // w lewo
                    {
                        grid[nx, ny].Walls[1] = false;
                        grid[x, y].Walls[3] = false;
                    }

                    x = nx;
                    y = ny;
                    iterator++;
                }
               

            } while (iterator < w * h);

            string json = JsonConvert.SerializeObject(grid);
            AdditionalMethods.SaveMazeToFile(json);
        }
    }
}