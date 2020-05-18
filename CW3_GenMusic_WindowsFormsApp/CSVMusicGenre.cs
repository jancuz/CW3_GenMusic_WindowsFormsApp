using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.Collections.Generic;
using System.IO;
using CsvHelper.Configuration.Attributes;

namespace CW3_GenMusic_WindowsFormsApp
{
    class CSVMusicGenre
    {
        [Name("filename")]
        public string Name { get; set; }
        [Name("class_predicted")]
        public float ClassPredicted { get; set; }
        [Name("class_user")]
        public float ClassUser { get; set; }
    }
}
