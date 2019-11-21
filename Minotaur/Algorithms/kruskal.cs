using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Minotaur.Algorithms
{
    static class Kruskal
    {

        public static void Generate(int w, int h)
        {
            int size = Variables.Instance.size;
            Cell[,] maze = new Cell[w, h];
            bool check_if_all_cells_are_connected = false;
            int number = 1; // every cell must be "named"
            int[,] cell_value = new int[w, h]; //atm only used for kruskal algorithm
            var random = new Random();
            int cell_W;
            int cell_H;
            int cell_which_wall;
            bool is_Possible = false;
            int bufor_number=1;
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    maze[i, j] = new Cell(i, j);
                    cell_value[i, j] = number;
                    number++;
                    Console.WriteLine(cell_value[i, j]);
                }
                Console.WriteLine("break");
            }

            while (check_if_all_cells_are_connected == false)
            {
                
                    cell_W = random.Next() % w;
                    cell_H = random.Next() % h;
                    while (is_Possible == false)
                    {
                        if (cell_value[cell_W, cell_H] == 1)
                        {
                            cell_W = random.Next() % w;
                            cell_H = random.Next() % h;
                        }
                        else
                        {
                            is_Possible = true;
                        }
                    }
                    is_Possible = false;

                    cell_which_wall = random.Next() % 4;
                    while (is_Possible == false)
                    {
                        if (cell_which_wall == 0 && cell_H == 0)
                        {
                            cell_which_wall = random.Next() % 4;
                        }
                        else if (cell_which_wall == 1 && cell_W == w - 1)
                        {
                            cell_which_wall = random.Next() % 4;
                        }
                        else if (cell_which_wall == 2 && cell_H == h - 1)
                        {
                            cell_which_wall = random.Next() % 4;
                        }
                        else if (cell_which_wall == 3 && cell_W == 0)
                        {
                            cell_which_wall = random.Next() % 4;
                        }
                        else { is_Possible = true; }

                    }
                
                    is_Possible = false;
                
                    Console.WriteLine(cell_value[cell_W, cell_H]);
                    switch (cell_which_wall)
                    {
                        case 0:
                            if (cell_value[cell_W, cell_H] != cell_value[cell_W, cell_H - 1])
                            {
                                maze[cell_W, cell_H].Walls[cell_which_wall] = false;
                                maze[cell_W, cell_H - 1].Walls[2] = false;
                                if (cell_value[cell_W, cell_H] > cell_value[cell_W, cell_H - 1])
                                {
                                bufor_number = cell_value[cell_W, cell_H];
                                    cell_value[cell_W, cell_H] = cell_value[cell_W, cell_H - 1];
                                }
                                else
                                {
                                bufor_number = cell_value[cell_W, cell_H - 1];
                                cell_value[cell_W, cell_H - 1] = cell_value[cell_W, cell_H];
                                }
                                Console.WriteLine("cell W: " + cell_W + " , cell H: " + cell_H + " , cell number: " + cell_value[cell_W, cell_H]);
                                Console.WriteLine("cell W: " + cell_W + " , cell H: " + (cell_H - 1) + " , cell number: " + cell_value[cell_W, cell_H - 1]);
                            is_Possible = true;
                        }
                            break;
                        case 1:
                            if (cell_value[cell_W, cell_H] != cell_value[cell_W + 1, cell_H])
                            {
                                maze[cell_W, cell_H].Walls[1] = false;
                                maze[cell_W + 1, cell_H].Walls[3] = false;
                                if (cell_value[cell_W, cell_H] > cell_value[cell_W + 1, cell_H])
                                {
                                bufor_number = cell_value[cell_W, cell_H];
                                cell_value[cell_W, cell_H] = cell_value[cell_W + 1, cell_H];
                                }
                                else
                                {
                                bufor_number = cell_value[cell_W + 1, cell_H];
                                cell_value[cell_W + 1, cell_H] = cell_value[cell_W, cell_H];
                                }
                                Console.WriteLine("cell W: " + cell_W + " , cell H: " + cell_H + " , cell number: " + cell_value[cell_W, cell_H]);
                                Console.WriteLine("cell W: " + (cell_W + 1) + " , cell H: " + cell_H + " , cell number: " + cell_value[cell_W + 1, cell_H]);
                            is_Possible = true;
                        }
                            break;
                        case 2:
                            if (cell_value[cell_W, cell_H] != cell_value[cell_W, cell_H + 1])
                            {
                                maze[cell_W, cell_H].Walls[2] = false;
                                maze[cell_W, cell_H + 1].Walls[0] = false;
                                if (cell_value[cell_W, cell_H] > cell_value[cell_W, cell_H + 1])
                                {
                                bufor_number = cell_value[cell_W, cell_H];
                                cell_value[cell_W, cell_H] = cell_value[cell_W, cell_H + 1];
                                }
                                else
                                {
                                bufor_number = cell_value[cell_W, cell_H +1];
                                cell_value[cell_W, cell_H + 1] = cell_value[cell_W, cell_H];
                                }
                                Console.WriteLine("cell W: " + cell_W + " , cell H: " + cell_H + " , cell number: " + cell_value[cell_W, cell_H]);
                                Console.WriteLine("cell W: " + cell_W + " , cell H: " + (cell_H + 1) + " , cell number: " + cell_value[cell_W, cell_H + 1]);
                            is_Possible = true;
                        }
                            break;
                        case 3:
                            if (cell_value[cell_W, cell_H] != cell_value[cell_W - 1, cell_H])
                            {
                                maze[cell_W, cell_H].Walls[cell_which_wall] = false;
                                maze[cell_W - 1, cell_H].Walls[1] = false;
                                if (cell_value[cell_W, cell_H] > cell_value[cell_W - 1, cell_H])
                                {
                                bufor_number = cell_value[cell_W, cell_H];
                                cell_value[cell_W, cell_H] = cell_value[cell_W - 1, cell_H];
                                }
                                else
                                {
                                bufor_number = cell_value[cell_W - 1, cell_H];
                                    cell_value[cell_W - 1, cell_H] = cell_value[cell_W, cell_H];
                                }
                                Console.WriteLine("cell W: " + cell_W + " , cell H: " + cell_H + " , cell number: " + cell_value[cell_W, cell_H]);
                                Console.WriteLine("cell W: " + (cell_W - 1) + " , cell H: " + cell_H + " , cell number: " + cell_value[cell_W - 1, cell_H]);
                            is_Possible = true;
                        }
                            break;
                    }
                    if (is_Possible == true)
                {

                    for (int i = 0; i < w; i++)
                    {
                        for (int j = 0; j < h; j++)
                        {
                            if (cell_value[i, j] == bufor_number)
                            {
                                cell_value[i, j]= cell_value[cell_W, cell_H];
                            }
                        }
                    }
                }
                


                    check_if_all_cells_are_connected = true;
                    for (int i = 0; i < w; i++)
                    {
                        for (int j = 0; j < h; j++)
                        {
                            if (cell_value[i, j] != 1)
                            {
                                check_if_all_cells_are_connected = false;
                            }
                        }
                    }
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

