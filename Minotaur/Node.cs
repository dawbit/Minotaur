using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minotaur
{
    class Node : Cell
    {
        public double gCost = 0; // distance to starting node
        public double hCost = 0; // distance to target node
        public double fCost // f cost is total of g cost and h cost
        {
            get
            {
                return gCost + hCost;
            }
        }

        public Node parent = null; // shows, which node is parent

        public Node(Cell c) : base(c.X, c.Y)
        {
            this.Walls = c.Walls;
        }

        public Point ToPoint()
        {
            return new Point(this.X, this.Y);
        }

        public bool Equals(Node n)
        {
            if (this.X == n.X && this.Y == n.Y)
                return true;
            return false;
        }
    }
}
