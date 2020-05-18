using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW3_GenMusic_WindowsFormsApp
{
    public class MusicFile
    {
        public string Name;
        public string Format;
        public string Directory;
        public int ClassNeuro;
        public MusicFile(string n, string f, string d, int c = -1)
        {
            Name = n;
            Format = f;
            Directory = d;
            ClassNeuro = c;

        }
        public override string ToString() { return Name; }

        public override bool Equals(object obj)
        {
            var item = obj as MusicFile;

            if (item == null)
            {
                return false;
            }

            return this.Name == item.Name && this.Format == item.Format &&
                this.Directory == item.Directory;
        }
    }
}
