using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minotaur.Algorithms
{
    class AStar
    {
        private Node start;
        private Node end;
        private Node[,] grid;
        private int width, height;

        public AStar(Point s, Point e, Cell[,] g)
        {
            width = g.GetLength(0);
            height = g.GetLength(1);
            grid = new Node[width, height];

            for(int i = 0; i < width; i++)
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

        double Distance(Node a, Node b)
        {
            return Math.Round(Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y)));
        }

        List<Node> Neighbours(Node n)
        {
            List<Node> result = new List<Node>();

            if (!n.Walls[0] && n.Y-1 >= 0)
            {
                result.Add(grid[n.X, n.Y-1]);
            }
            if (!n.Walls[1] && n.X+1 < width)
            {
                result.Add(grid[n.X+1, n.Y]);
            }
            if (!n.Walls[2] && n.Y+1 < height)
            {
                result.Add(grid[n.X, n.Y+1]);
            }
            if (!n.Walls[3] && n.X-1 >= 0)
            {
                result.Add(grid[n.X-1, n.Y]);
            }

            return result;
        }

        Node Evaluate()
        {
            List<Node> open = new List<Node>();
            List<Node> closed = new List<Node>();
            Node current;
            List<Node> neighbours;
            start.gCost = 0;
            start.hCost = Distance(start, end);
            open.Add(start);

            while (open.Count > 0)
            {
                current = LowestFCost(open);
                closed.Add(current);
                open.Remove(current);

                Console.WriteLine("Current: " + current.X + " " + current.Y);
                if (current.Equals(end))
                {
                    Console.WriteLine("Doszedlem do end");
                    return current;
                }

                neighbours = Neighbours(current);

                foreach (Node n in neighbours)
                {
                    if (!closed.Contains(n))
                    {
                        double temp = current.gCost + Distance(current, n);
                        if(temp < n.gCost || !open.Contains(n))
                        {
                            n.gCost = temp;
                            n.hCost = Distance(n, end);
                            n.parent = current;

                            if (!open.Contains(n))
                                open.Add(n);
                        }
                    }
                }
            }

            return null;
        }

        public List<Point> GetShortestPath()
        {
            List<Point> path = new List<Point>();
            Node current = Evaluate();

            if(current != null)
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

        Node LowestFCost(List<Node> list)
        {
            Node lowest = list[0];

            foreach(Node n in list)
            {
                if(n.fCost < lowest.fCost || (n.fCost == lowest.fCost && n.hCost < lowest.hCost))
                {
                    lowest = n;
                }
            }

            return lowest;
        }
    }
}
