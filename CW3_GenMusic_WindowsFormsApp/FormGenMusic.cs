using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CW3_GenMusic_WindowsFormsApp
{
    public partial class FormGenMusic : Form
    {
        public FormGenMusic()
        {
            InitializeComponent();
        }

        public List<MusicFile> filesStartPopulation = new List<MusicFile>();

        /// <summary>
        /// Добавление мелодии.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bnAddMIDI_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @"D:\drywetmidi-develop\drywetmidi-develop\GenMusicWindowsForms\bin\Debug\music";
            openFileDialog1.Filter = "MIDI|*.mid|MIDI|*.midi";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string fileDirectoryName = openFileDialog1.FileName;
            string fileName = fileDirectoryName.Substring(fileDirectoryName.LastIndexOf(@"\") + 1);
            string fileExt = fileName.Substring(fileName.LastIndexOf("."));

            if (filesStartPopulation.Contains(new MusicFile(fileName, fileExt, fileDirectoryName)) == true)
                MessageBox.Show("Данный файл уже добавлен!");

            string fileNameWAV = null;
            if (fileName.Contains(".mid"))
                fileNameWAV = fileName.Replace(".mid", ".wav");
            if (fileName.Contains(".midi"))
                fileNameWAV = fileName.Replace(".mid", ".wav");
            string fileDirectoryNameWAV = @"D:\drywetmidi-develop\drywetmidi-develop\GenMusicWindowsForms\bin\Debug\convertedMusic\" + fileNameWAV;
            if (System.IO.File.Exists(fileDirectoryNameWAV))
            {
                MusicFile mf = new MusicFile(fileName, fileExt, fileDirectoryName);
                lbStartPopulationMIDI.Items.Add(mf);
                filesStartPopulation.Add(mf);

                string fileExtWAV = ".wav";
                mf = new MusicFile(fileNameWAV, fileExtWAV, fileDirectoryNameWAV);
                lbStartPopulationWAV.Items.Add(mf);
            }
            else
                MessageBox.Show(@"Необходимо сначала конвертировать данный файл в формат .wav, сохранив исходное название, и поместить в следуюущую директорию: *\bin\Debug\convertedMusic");
        }

        /// <summary>
        /// Удаление мелодии.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bnRemoveMIDI_Click(object sender, EventArgs e)
        {
            if (lbStartPopulationMIDI.SelectedItems.Count != 0)
            {
                var sItem = lbStartPopulationMIDI.SelectedIndex;
                lbStartPopulationMIDI.Items.RemoveAt(sItem);
                lbStartPopulationWAV.Items.RemoveAt(sItem);
            }
        }

        /// <summary>
        /// Создание новых мелодий.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bnGenMusic_Click(object sender, EventArgs e)
        {
            // Извлечение данных из мелодий для последующей оценки нейросетью
            bool execute = false;
            do
            {
                execute = ExtractDataFromMelodies();
            } while (execute);
            MessageBox.Show("Данные извлечены");
            // Загрузка нейросети и оценка классовой принадлежности изначальной популяции
            var res1 = ClassifyMelodies();
        }

        /// <summary>
        /// Классификация мелодий с использованием нейросети.
        /// </summary>
        private string ClassifyMelodies()
        {
            // Директория скрипта neuroClassifier.py
            string cmd_classify = @".\Scripts\neuroClassifier.py";
            ProcessStartInfo start_reading = new ProcessStartInfo();
            start_reading.FileName = @"C:\Users\Яна\AppData\Local\Programs\Python\Python36\python.exe";
            start_reading.Arguments = string.Format("{0}", cmd_classify);  // give filename, dates from the UI to python and query datatype
            start_reading.UseShellExecute = false;
            start_reading.RedirectStandardOutput = true;
            start_reading.CreateNoWindow = true;
            using (Process process = Process.Start(start_reading))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Извлечение следующих характеристик из мелодий.wav с использованием скрипона на python:
        /// filename,chroma_stft,spectral_centroid,spectral_bandwidth,rolloff,zero_crossing_rate,mfcc1, ...,mfcc20
        /// </summary>
        /// <returns></returns>
        private bool ExtractDataFromMelodies()
        {
            foreach (var item in lbStartPopulationWAV.Items)
            {
                var mf = item as MusicFile;
                string path1 = mf.Directory;
                string path2 = @".\startPopulation\";
                File.Copy(path1, path2 + mf.Name, true);
            }
            // Директория скрипта reader.py
            string cmd_read = @".\Scripts\reader.py";
            ProcessStartInfo start_reading = new ProcessStartInfo();
            start_reading.FileName = @"C:\Python27\python.exe";
            start_reading.Arguments = string.Format("{0}", cmd_read);  // give filename, dates from the UI to python and query datatype
            start_reading.UseShellExecute = false;
            start_reading.RedirectStandardOutput = true;
            start_reading.CreateNoWindow = true;
            using (Process process = Process.Start(start_reading))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string s = reader.ReadToEnd();
                    return true;
                }
            }
        }
    }
}
