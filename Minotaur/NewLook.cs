using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minotaur
{
    public partial class NewLook : Form
    {
        private int width = 30;
        private int heigth = 20;
        private int size = Variables.Instance.size;
        private int topMargin = 50;
        private Cell[,] grid;

        public Cell[,] PaintMaze
        {
            get
            {
                return grid;
            }
            set
            {
                MessageBox.Show("Test");
                grid = value;
                this.Refresh();

            }
        }

        public NewLook()
        {
            InitializeComponent();
            this.ClientSize = new Size(width * size, heigth * size + topMargin);

            algorithmComboBox.Items.Add("Prim's");

            algorithmComboBox.SelectedIndex = 0;

            typeof(Form).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, this, new object[] { true });
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            string s = algorithmComboBox.SelectedItem.ToString();

            switch (s)
            {
                //tutaj dodaj case'a ze swoim algorytmem
                case "Prim's":
                    Algorithms.Prims.Generate(width, heigth, PaintMaze);
                    break;
            }
        }

        private void PaintGrid(object sender, PaintEventArgs e)
        {
            if (grid != null)
            {
                Graphics g;
                g = e.Graphics;

                Pen myPen = new Pen(Color.Black);
                myPen.Width = 1;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < heigth; j++)
                    {
                        Cell c = this.grid[i, j];

                        if (c.Walls[0])
                            g.DrawLine(myPen, c.X * size, c.Y * size + topMargin, (c.X + 1) * size, c.Y * size + topMargin);
                        if (c.Walls[1])
                            g.DrawLine(myPen, (c.X + 1) * size, c.Y * size + topMargin, (c.X + 1) * size, (c.Y + 1) * size + topMargin);
                        if (c.Walls[2])
                            g.DrawLine(myPen, c.X * size, (c.Y + 1) * size + topMargin, (c.X + 1) * size, (c.Y + 1) * size + topMargin);
                        if (c.Walls[3])
                            g.DrawLine(myPen, c.X * size, c.Y * size + topMargin, c.X * size, (c.Y + 1) * size + topMargin);
                    }
                }
            }

            //SolidBrush brush = new SolidBrush(Color.LightGreen);
            //g.FillRectangle(brush, start.X + 1, start.Y + 1, size - 1, size - 1);
            //brush = new SolidBrush(Color.IndianRed);
            //g.FillRectangle(brush, end.X + 1, end.Y + 1, size - 1, size - 1);
        }
    }
}
