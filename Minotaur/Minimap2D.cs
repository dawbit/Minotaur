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

        Form parent;

        public Minimap2D(string json, Form parent)
        {
            InitializeComponent();
            this.parent = parent;
            typeof(Form).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, this, new object[] { true });

            this.path = new List<Point>();

            this.json = json;
            this.grid = JsonConvert.DeserializeObject<Cell[,]>(json);
            this.width = grid.GetLength(0);
            this.height = grid.GetLength(1);

            Variables.Instance.size = (int)(width > height ? Math.Floor(600f / width) : Math.Floor(600f / height));

            this.size = Variables.Instance.size;


            int temp = width > height ? width : height;
            this.ClientSize = new Size(800, temp * size + 1);

            Size buttonSize = new Size(70, 70);

            //start button
            this.startButton.Size = buttonSize;
            this.startButton.Location = new Point(665, 20);

            //end button
            this.endButton.Size = buttonSize;
            this.endButton.Location = new Point(665, 100);

            //3D button
            this.lunch3DButton.Size = buttonSize;
            this.lunch3DButton.Location = new Point(665, 220);

            //combobox
            this.algorithmComboBox.Size = new Size(120, 40);
            this.algorithmComboBox.Location = new Point(640, 400);
            this.algorithmComboBox.Items.Add("A*");
            this.algorithmComboBox.Items.Add("Dijkstra");
            this.algorithmComboBox.SelectedIndex = 0;

            //find button
            this.findButton.Size = buttonSize;
            this.findButton.Location = new Point(665, 440);

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
            //Process.Start("D:\\Semestr5\\Unity\\Minotaur\\Minotaur\\Game\\Minotaur.exe", argument1, arguments2, argument3);
            //argument 1 - ścieżka do labiryntu json
            //argument 2 - punkt startu
            //argument 3 - punkt konca
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            Algorithms.Pathfinding a = null;
            Algorithms.Pathfinding dijkstra = null;

            switch (algorithmComboBox.SelectedItem.ToString())
            {
                case "A*":
                    a = new Algorithms.Pathfinding(start, end, grid, "AStar");
                    path = a.GetShortestPath();
                    break;

                case "Dijkstra":
                    dijkstra = new Algorithms.Pathfinding(start, end, grid, "Dijkstra");
                    path = dijkstra.GetShortestPath();
                    break;

            }

            this.Invalidate();
        }

        private void Minimap2D_FormClosed(object sender, FormClosedEventArgs e)
        {
            parent.Enabled = true;
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
                Point temp = new Point(x, y);
                if (color == Color.LightGreen && end != temp)
                    start = temp;
                else if(start != temp)
                    end = temp;

                if (temp == end)
                {
                    color = Color.IndianRed;
                    endButton.Select();
                }
                else if (temp == start)
                {
                    color = Color.LightGreen;
                    startButton.Select();
                }
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

            myPen.Color = Color.Red;
            myPen.Width = 2;
            if (path != null)
                for(int i = 0; i < path.Count - 1; i++)
                {
                    g.DrawLine(myPen, path[i].X * size + 0.5f * size, path[i].Y * size + 0.5f * size, path[i + 1].X * size + 0.5f * size, path[i + 1].Y * size + 0.5f * size);
                }
        }
          
    }
}
