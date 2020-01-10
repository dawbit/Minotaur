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
        List<Point> path;

        public Minimap2D(string json)
        {
            InitializeComponent();
            typeof(Form).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, this, new object[] { true });

            this.path = new List<Point>();

            this.json = json;
            this.grid = JsonConvert.DeserializeObject<Cell[,]>(json);
            this.width = grid.GetLength(0);
            this.height = grid.GetLength(1);
            this.size = Variables.Instance.size;

            //this.Size = new Size(width * size + 80, height * size + 1);
            if (height * size + 1 < 400)
            {
                this.ClientSize = new Size(width * size + 120, 400);
            }
            else
            {
                this.ClientSize = new Size(width * size + 120, height * size + 1);
            }

            //startButton
            this.startButton.Location = new Point(width * size + 35, 20);
            this.startButton.Size = new Size(50, 50);

            //endButton
            this.endButton.Location = new Point(width * size + 35, 70);
            this.endButton.Size = new Size(50, 50);

            //3DButton
            this.lunch3DButton.Location = new Point(width * size + 35, 120);
            this.lunch3DButton.Size = new Size(50, 50);

            //comboBox
            this.algorithmComboBox.Location = new Point(width * size + 15, 300);
            this.algorithmComboBox.Items.Add("A*");
            this.algorithmComboBox.Items.Add("Dijkstra");
            this.algorithmComboBox.SelectedIndex = 0;

            //findButton
            this.findButton.Location = new Point(width * size + 35, 330);
            this.findButton.Size = new Size(50, 50);

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

        private void findButton_Click(object sender, EventArgs e)
        {
            switch (algorithmComboBox.SelectedItem.ToString())
            {
                case "A*":
                    Algorithms.AStar a = new Algorithms.AStar(start, end, grid);
                    path = a.GetShortestPath();
                    Console.WriteLine(path.ToString());
                    break;
                case "Dijkstra":
                    Algorithms.Dijkstra dijkstra = new Algorithms.Dijkstra(start, end, grid);
                    path = dijkstra.GetShortestPath();
                    Console.WriteLine(path.ToString());
                    break;

            }

            this.Invalidate();
        }

        private void Minimap2D_Click(object sender, EventArgs e)
        {
            var relativePoint = this.PointToClient(Cursor.Position);
            int x = relativePoint.X;
            int y = relativePoint.Y;

            if (x < width * size && y < height * size)
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

            brush = new SolidBrush(Color.Aqua);
            if (path != null)
                foreach (Point p in path)
                {
                    g.FillRectangle(brush, p.X * size + 1, p.Y * size + 1, size - 1, size - 1);
                }
        }
    }
}
