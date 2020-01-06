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
        public double gCost = 0;
        public double hCost = 0;
        public double fCost
        {
            get
            {
                return gCost + hCost;
            }
        }

        public Node parent = null;

        public Node(Cell c) : base(c.X, c.Y)
        {
            this.Walls = c.Walls;
        }
        public Node(Point p) : base(p.X / Variables.Instance.size, p.Y / Variables.Instance.size)
        {

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
