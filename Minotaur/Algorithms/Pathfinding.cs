using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minotaur.Algorithms
{
    class Pathfinding
    {
        private Node start;
        private Node end;
        private Node[,] grid;
        private int width, height;
        private string mode;

        public Pathfinding(Point s, Point e, Cell[,] g, string m)
        {
            width = g.GetLength(0);
            height = g.GetLength(1);
            grid = new Node[width, height];
            mode = m;

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

        double Distance(Node a, Node b) // distance (h cost) is calculated with Manhattan method
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        List<Node> Neighbours(Node n) // we look for neighbour in 4 direction (top, right, bottom, left) and check if neighbour is within our grid and if there is no wall between neighbour and current node
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

        Node AStar()
        {
            List<Node> open = new List<Node>(); // list with all not calculated nodes
            List<Node> closed = new List<Node>(); // list with all already calculated nodes
            Node current; // currently calculated node
            List<Node> neighbours; // list with all available neighbour nodes
            start.gCost = 0; // firstly we set the attributes of starting node
            start.hCost = Distance(start, end);
            open.Add(start); // then we add it to open set

            while (open.Count > 0) // while there are still nodes that weren't calculated
            {
                current = LowestFCost(open); // current node is the node from open set with lowest f cost
                closed.Add(current); // while we will calculated current node, we add it to closed set
                open.Remove(current); // and remove it from open set

                if (current.Equals(end)) // if current node is our target node, we end the program and return this node
                {
                    return current;
                }

                neighbours = Neighbours(current); // we find all neighbours to our node

                foreach (Node n in neighbours) 
                {
                    if (!closed.Contains(n)) // for each neighbour node, which is not already in closed set, we calculate its g and h cost
                    {
                        double temp = current.gCost + Distance(current, n); // g cost = current node's g cost + distance from neighbour to current node
                        if(temp < n.gCost || !open.Contains(n)) // if new g cost is lower, we update it or if neigbour is not in open set yet, we put it there
                        {
                            n.gCost = temp; // we set g cost
                            n.hCost = Distance(n, end); // set h cost, which is distance to the target node
                            n.parent = current; // we set currrent node as the parent node of this neighbour

                            if (!open.Contains(n)) // if neighbour is not already in open set, we add him there
                                open.Add(n);
                        }
                    }
                }
            }

            return null; // if the path wasn't found, return null
        }

        Node Dijkstra()
        {
            List<Node> open = new List<Node>();
            List<Node> closed = new List<Node>();
            List<Node> neighbours;
            List<Node> temp = new List<Node>();
            start.gCost = 0;
            start.hCost = Distance(start, end);
            open.Add(start);

            while (open.Count > 0)
            {
                temp.Clear();

                foreach (Node current in open)
                {
                    neighbours = Neighbours(current);

                    foreach(Node n in neighbours)
                    {
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

        public List<Point> GetShortestPath() // pathfinding algorithm returns last node and this function backtracks its parents, creating a path from start to target node
        {
            List<Point> path = new List<Point>();
            Node current = null;

            switch (mode)
            {
                case "Dijkstra":
                    current = Dijkstra();
                    break;

                case "AStar":
                    current = AStar();
                    break;
            }

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

        Node LowestFCost(List<Node> list) // function looks for node with lowest f cost
        {
            Node lowest = list[0];

            foreach(Node n in list)
            {
                if(n.fCost < lowest.fCost || (n.fCost == lowest.fCost && n.hCost < lowest.hCost)) // if two nodes has the same f cost, we pick one with lower h cost
                {
                    lowest = n;
                }
            }

            return lowest;
        }
    }
}
