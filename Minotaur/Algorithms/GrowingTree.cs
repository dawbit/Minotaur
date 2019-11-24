using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minotaur.Algorithms
{
    class GrowingTree
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

            List<Cell> firstVisit = new List<Cell>();
            List<Cell> secondVisit = new List<Cell>();

            //grid[i, j].Walls[x] = false;// liczby przy grid to współrzędne pola przy walls są współrzędne ściany
            Random random = new Random();
            int x = random.Next(0, w);
            int y = random.Next(0, h); // wybieramy randomowe współrzędne potrzebne do startu gnerowania labiryntu
            firstVisit.Add(grid[x, y]);
            List<Cell> neighbours = new List<Cell>();


            do
            {
                x = firstVisit[firstVisit.Count - 1].X;
                y = firstVisit[firstVisit.Count - 1].Y;
                neighbours.Clear();

                if (y - 1 >= 0 && !firstVisit.Contains(grid[x, y - 1]) && !secondVisit.Contains(grid[x, y - 1]))
                {
                    neighbours.Add(grid[x, y - 1]);
                } // do góry
                if (x + 1 < w && !firstVisit.Contains(grid[x + 1, y]) && !secondVisit.Contains(grid[x + 1, y]))
                {
                    neighbours.Add(grid[x + 1, y]);
                } //w prawo
                if (y + 1 < h && !firstVisit.Contains(grid[x, y + 1]) && !secondVisit.Contains(grid[x, y + 1]))
                {
                    neighbours.Add(grid[x, y + 1]);
                } // nadół
                if (x - 1 >= 0 && !firstVisit.Contains(grid[x - 1, y]) && !secondVisit.Contains(grid[x - 1, y]))
                {
                    neighbours.Add(grid[x - 1, y]);
                }// lewo



                if (neighbours.Count == 0)
                {
                    int temp = firstVisit.Count;
                    secondVisit.Add(firstVisit[temp - 1]);
                    firstVisit.RemoveAt(temp - 1);
                }
                else
                {
                    int temp = random.Next(0, neighbours.Count);
                    firstVisit.Add(neighbours[temp]);

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
                    if (nx == x && ny > y) // dół
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

            } while (secondVisit.Count < w * h);


            string json = JsonConvert.SerializeObject(grid);
            string path = Variables.Instance.path + "\\" + DateTime.Now.ToString("MM-dd-yyyy_h-mm-ss") + ".json";

            using (var tw = new StreamWriter(path, true))
            {
                tw.WriteLine(json.ToString());
                tw.Close();
            }
        }
    }


}

