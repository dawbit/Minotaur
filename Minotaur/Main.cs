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

            //tutaj dodaj swoją opcję
            algorithmComboBox.Items.Add("Test2");
            algorithmComboBox.Items.Add("Prim's");

            algorithmComboBox.SelectedIndex = 0;

            int size = Variables.Instance.size;
            Rectangle resolution = Screen.PrimaryScreen.Bounds;

            widthUpDown.Maximum = resolution.Width / size - 1;
            heightUpDown.Maximum = resolution.Height / size - 3;
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            string s = algorithmComboBox.SelectedItem.ToString();
            int w = (int)widthUpDown.Value;
            int h = (int)heightUpDown.Value;

            switch (s)
            {
                //tutaj dodaj case'a ze swoim algorytmem
                case "Test2":
                    Algorithms.Test2.Generate(w, h);
                    break;
                case "Prim's":
                    //Algorithms.Prims.Generate(w, h);
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
