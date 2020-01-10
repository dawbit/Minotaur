using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minotaur.Algorithms
{
    class Dijkstra
    {
        private Node start;
        private Node end;
        private Node[,] grid;
        private int width, height;

        public Dijkstra(Point s, Point e, Cell[,] g)
        {
            width = g.GetLength(0);
            height = g.GetLength(1);
            grid = new Node[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    grid[i, j] = new Node(g[i, j]);
                }
            }

            start = new Node(grid[s.X / Variables.Instance.size, s.Y / Variables.Instance.size]);
            end = new Node(grid[e.X / Variables.Instance.size, e.Y / Variables.Instance.size]);
            Console.WriteLine("End: " + end.X + " " + end.Y);
        }


        List<Node> Neighbours(Node n)
        {
            List<Node> result = new List<Node>();

            if (!n.Walls[0] && n.Y - 1 >= 0)
            {
                result.Add(grid[n.X, n.Y - 1]);
            }
            if (!n.Walls[1] && n.X + 1 < width)
            {
                result.Add(grid[n.X + 1, n.Y]);
            }
            if (!n.Walls[2] && n.Y + 1 < height)
            {
                result.Add(grid[n.X, n.Y + 1]);
            }
            if (!n.Walls[3] && n.X - 1 >= 0)
            {
                result.Add(grid[n.X - 1, n.Y]);
            }

            return result;
        }

        Node Evaluate()
        {
            List<Node> open = new List<Node>();
            List<Node> closed = new List<Node>();
            List<Node> neighbours;
            List<Node> temp = new List<Node>();

            open.Add(start);

            while (open.Count > 0)
            {
                temp.Clear();

                foreach (Node current in open)
                {
                    neighbours = Neighbours(current);

                    foreach (Node n in neighbours)
                    {

                        Console.WriteLine("Liczba sąsiadów: " + temp.Count());
                        Console.WriteLine("OPEN COUNT: " + open.Count);

                        if (!temp.Contains(n) && !closed.Contains(n))
                        {
                            n.parent = current;
                            temp.Add(n);

                        }

                        if (n.Equals(end))
                        {
                            return current;
                        }

                    }

                }

                closed.AddRange(open);
                open.Clear();
                open.AddRange(temp);

            }

            return null;
        }

        public List<Point> GetShortestPath()
        {
            List<Point> path = new List<Point>();
            Node current = Evaluate();

            if (current != null)
            {
                path.Insert(0, current.ToPoint());

                while (current.parent != null)
                {
                    current = current.parent;
                    path.Insert(0, current.ToPoint());
                }

                return path;
            }

            return null;
        }

    }
}
