using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minotaur
{
    public class Variables
    {
        public string path;
        public string mazeName;
        public int size;
        private static Variables instance;

        private Variables() { }

        public static Variables Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Variables();
                    instance.init();
                }
                return instance;
            }
        }

        private void init()
        {
            string workingDirectory = Environment.CurrentDirectory;
            this.path = Directory.GetParent(workingDirectory).Parent.FullName + "\\Maze";
            this.size = 20;
            this.mazeName = "sample_name";
        }
    }
}
