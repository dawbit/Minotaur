using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minotaur.Algorithms
{
    static class AdditionalMethods
    {
        public static void SaveMazeToFile(string json)
        {
            string name = Variables.Instance.mazeName;
            string path = Variables.Instance.path + "\\" + name;
            string temp = path;
            int number = 1;

            while (File.Exists(temp))
            {
                temp = path + " " + number.ToString();
                number++;
            }

            using (var tw = new StreamWriter(temp, true))
            {
                tw.WriteLine(json.ToString());
                tw.Close();
            }
        }
    }
}
