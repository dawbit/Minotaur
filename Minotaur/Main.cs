using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minotaur
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            init();

            algorithmComboBox.Items.Add("Test");
            algorithmComboBox.Items.Add("Test2");
            //tutaj dodaj swoją opcję
            algorithmComboBox.SelectedIndex = 0;
        }

        private void TestAlgorithm()
        {
            int size = Variables.Instance.size;
            int width = 8;
            int height = 10;
            Cell[,] grid = new Cell[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    grid[i, j] = new Cell(i * size, j * size);
                    grid[i, j].Randomize();
                }
            }

            //zapisywanie pliku do formatu json
            string json = JsonConvert.SerializeObject(grid);
            string path = Variables.Instance.path + "\\" + DateTime.Now.ToString("MM-dd-yyyy_h-mm-ss") + ".json";

            using (var tw = new StreamWriter(path, true))
            {
                tw.WriteLine(json.ToString());
                tw.Close();
            }
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            string s = algorithmComboBox.SelectedItem.ToString();
            int w = (int)widthUpDown.Value;
            int h = (int)heightUpDown.Value;

            switch (s)
            {
                case "Test":
                    TestAlgorithm();
                    break;
                //tutaj dodaj case'a ze swoim algorytmem
                case "Test2":
                    Algorithms.Test2.Generate(w, h);
                    break;
            }

            init();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            if (mazeListBox.SelectedIndex != -1)
            {
                string path = Variables.Instance.path + "\\" + mazeListBox.SelectedItem.ToString();
                string json = "";
                using (StreamReader r = new StreamReader(path))
                {
                    json = r.ReadToEnd();
                }
                Form minimap2d = new Minimap2D(json);
                minimap2d.Show();
            }
        }

        private void init()
        {
            string[] files = Directory.GetFiles(Variables.Instance.path);
            mazeListBox.Items.Clear();

            foreach (string s in files)
            {
                int last = s.LastIndexOf('\\');
                string result = s.Substring(last + 1);
                mazeListBox.Items.Add(result);
            }
        }
    }
}
