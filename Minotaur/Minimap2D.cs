using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minotaur
{
    public partial class Minimap2D : Form
    {
        int size;
        int width, height;
        Cell[,] grid;

        public Minimap2D(string json)
        {
            InitializeComponent();
            this.grid = JsonConvert.DeserializeObject<Cell[,]>(json);
            this.width = grid.GetLength(0);
            this.height = grid.GetLength(1);
            this.size = Variables.Instance.size;
            this.Size = new Size((width + 1) * size, (height + 1) * size);
            this.ClientSize = new Size(width * size + 1, height * size + 1);
        }

        private void PaintGrid(object sender, PaintEventArgs e)
        {
            Graphics g;
            g = e.Graphics;

            Pen myPen = new Pen(Color.Black);
            myPen.Width = 1;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Cell c = this.grid[i, j];

                    if (c.Walls[0])
                        g.DrawLine(myPen, c.X * size,       c.Y * size,      (c.X + 1) * size,  c.Y * size);
                    if (c.Walls[1])
                        g.DrawLine(myPen, (c.X + 1) * size, c.Y * size,      (c.X + 1) * size, (c.Y + 1) * size);
                    if (c.Walls[2])
                        g.DrawLine(myPen, c.X * size,      (c.Y + 1) * size, (c.X + 1) * size, (c.Y + 1) * size);
                    if (c.Walls[3])
                        g.DrawLine(myPen, c.X * size,       c.Y * size,       c.X * size,      (c.Y + 1) * size);
                }
            }
        }
    }
}
