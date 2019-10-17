using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
        Color color = Color.LightGreen;
        Point start, end;
        string json;

        public Minimap2D(string json)
        {
            InitializeComponent();
            typeof(Form).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, this, new object[] { true });

            this.json = json;
            this.grid = JsonConvert.DeserializeObject<Cell[,]>(json);
            this.width = grid.GetLength(0);
            this.height = grid.GetLength(1);
            this.size = Variables.Instance.size;

            //this.Size = new Size(width * size + 80, height * size + 1);
            this.ClientSize = new Size(width * size + 80, height * size + 1);

            this.startButton.Location = new Point(width * size + 15, 20);
            this.startButton.Size = new Size(50, 50);
            this.endButton.Size = new Size(50, 50);
            this.endButton.Location = new Point(width * size + 15, 70);
            this.lunch3DButton.Size = new Size(50, 50);
            this.lunch3DButton.Location = new Point(width * size + 15, 120);

            this.start = new Point(0, 0);
            this.end = new Point((width - 1) * size, (height - 1) * size);
        }

        private void Start_Click(object sender, EventArgs e)
        {
            color = Color.LightGreen;
        }

        private void End_Click(object sender, EventArgs e)
        {
            color = Color.IndianRed;
        }

        private void Lunch3DButton_Click(object sender, EventArgs e)
        {
            //Process.Start("D:\\Semestr5\\Unity\\Minotaur\\Minotaur\\Game\\Minotaur.exe", json);
        }

        private void Minimap2D_Click(object sender, EventArgs e)
        {
            var relativePoint = this.PointToClient(Cursor.Position);
            int x = relativePoint.X;
            int y = relativePoint.Y;

            if (x <= width * size && y <= height * size)
            {
                x -= x % size;
                y -= y % size;

                if (color == Color.LightGreen)
                    start = new Point(x, y);
                else
                    end = new Point(x, y);
            }

            this.Invalidate();
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

            SolidBrush brush = new SolidBrush(Color.LightGreen);
            g.FillRectangle(brush, start.X + 1, start.Y + 1, size - 1, size - 1);
            brush = new SolidBrush(Color.IndianRed);
            g.FillRectangle(brush, end.X + 1, end.Y + 1, size - 1, size - 1);
        }
    }
}
