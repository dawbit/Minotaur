using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minotaur.Algorithms
{
    static class Test2
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
            
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    if(i == 4 && j > 0 && j < h - 1)
                    {
                        grid[i, j].Walls[0] = false;
                        grid[i, j].Walls[2] = false;
                    }
                }
            }

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
