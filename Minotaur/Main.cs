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
            algorithmComboBox.Items.Add("Prim's");
            algorithmComboBox.Items.Add("Kruskal's");
            algorithmComboBox.Items.Add("Growing Tree");
            algorithmComboBox.Items.Add("Backtrack");
            algorithmComboBox.Items.Add("Hunt'n'kill");

            algorithmComboBox.SelectedIndex = 0;

            int size = Variables.Instance.size;
            Rectangle resolution = Screen.PrimaryScreen.Bounds;

            widthUpDown.Minimum = Variables.Instance.minSize;
            heightUpDown.Minimum = Variables.Instance.minSize;

            widthUpDown.Maximum = Variables.Instance.maxSize;
            heightUpDown.Maximum = Variables.Instance.maxSize;
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            createButton.Enabled = false;

            if (0 < nameTextBox.Text.Length && nameTextBox.Text.Length < 30)
                Variables.Instance.mazeName = ValidatePathName(nameTextBox.Text);
            else
                Variables.Instance.mazeName = "sample_name";

            string s = algorithmComboBox.SelectedItem.ToString();

            widthUpDown.Validate();
            heightUpDown.Validate();

            int w = (int)widthUpDown.Value;
            int h = (int)heightUpDown.Value;

            switch (s)
            {
                //tutaj dodaj case'a ze swoim algorytmem
                case "Prim's":
                    Algorithms.Prims.Generate(w, h);
                    break;
                case "Kruskal's":
                    Algorithms.Kruskal.Generate(w, h);
                    break;
                case "Growing Tree":
                    Algorithms.GrowingTree.Generate(w, h);
                    break;
                case "Backtrack":
                    Algorithms.Backtracker.Generate(w, h);
                    break;
                case "Hunt'n'kill":
                    Algorithms.HuntAndKill.Generate(w, h);
                    break;
            }

            init();

            createButton.Enabled = true;
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            if (mazeListBox.SelectedIndex != -1)
            {
                string path = Variables.Instance.path + "\\" + mazeListBox.SelectedItem.ToString();
                
                Form minimap2d = new Minimap2D(path, this);

                this.Enabled = false;
                minimap2d.Show();
            }
        }

        private void init()
        {
            if (!Directory.Exists(Variables.Instance.path))
            {
                Directory.CreateDirectory(Variables.Instance.path);
            }

            string[] files = Directory.GetFiles(Variables.Instance.path);
            mazeListBox.Items.Clear();

            foreach (string s in files)
            {
                int last = s.LastIndexOf('\\');
                string result = s.Substring(last + 1);
                mazeListBox.Items.Add(result);
            }
        }

        private void mazeListBox_DoubleClick(object sender, EventArgs e)
        {
            if (mazeListBox.SelectedItem != null)
                LoadButton_Click(sender, e);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if(mazeListBox.SelectedItem != null)
            {
                string path = mazeListBox.SelectedItem.ToString();
                File.Delete(Variables.Instance.path + "\\" + path);
                init();
            }
        }

        private string ValidatePathName(string name)
        {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            foreach (char c in invalid)
            {
                name = name.Replace(c.ToString(), "");
            }

            if (name.Length <= 0)
                name = "sample_name";

            return name;
        }
    }
}
