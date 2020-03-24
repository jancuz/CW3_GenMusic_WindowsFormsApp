using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
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

using SimpleSynth.Synths;

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
            openFileDialog1.InitialDirectory = @"D:\3 курс\курсовая\CW3_GenMusic_WindowsFormsApp\CW3_GenMusic_WindowsFormsApp\bin\Debug\music";
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
            string fileDirectoryNameWAV = @".\convertedMusic\" + fileNameWAV;
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
            // Выбор начальной популяции
            // Извлечение данных из мелодий для последующей оценки нейросетью
            bool execute = false;
            do
            {
                execute = ExtractDataFromMelodies();
                foreach (var item in lbStartPopulationWAV.Items)
                {
                    var mf = item as MusicFile;
                    string path1 = mf.Directory;
                    string path2 = @".\startPopulation\";
                    File.Delete(path2 + mf.Name);
                }
            } while (!execute);
            MessageBox.Show("Данные извлечены");

            // Загрузка нейросети и оценка классовой принадлежности изначальной популяции
            bool classify = false;
            do
            {
                classify = ClassifyMelodies();
            } while (!classify);
            MessageBox.Show("Исходная популяция оценена");

            // Извлечение оценок
            List<int> classes = ExtractClasses();

            // Формирование начальной популяции
            List<int> chords = new List<int>();
            List<int> notes = new List<int>();
            // Извлечение информации о первой популяции
            List<MidiFile> firstPopulation = new List<MidiFile>();
            foreach (var melody in filesStartPopulation)
            {
                var melodyMIDI = MidiFile.Read(melody.Directory);
                firstPopulation.Add(melodyMIDI);
                chords.Add(melodyMIDI.GetChords().Count());
                notes.Add(melodyMIDI.GetNotes().Count());
            }

            int sizeOfСhromosomeChords = chords.Min();
            int sizeOfСhromosomeNotes = notes.Min();

            // Выполнение ГА
            GA(firstPopulation, classes, sizeOfСhromosomeChords, sizeOfСhromosomeNotes);


        }

        /// <summary>
        /// Генетический алгоритм
        /// </summary>
        /// <param name="allPopulation"></param>
        /// <param name="fitness"></param>
        /// <param name="sizeOfChromosomeChords"></param>
        /// <param name="sizeOfChromosomeNotes"></param>
        /// <param name="numberOfGeneratedPopulations"></param>
        /// <param name="sizeOfCreatedPopulation"></param>
        /// <param name="numberOfMutations"></param>
        private void GA(List<MidiFile> allPopulation, List<int> fitness, int sizeOfChromosomeChords, int sizeOfChromosomeNotes,
            int numberOfGeneratedPopulations = 10, int sizeOfCreatedPopulation = 20, int sizeOfSelection = 10, int numberOfMutations = 2)
        {
            for(int i = 0; i < 1; i++)
            {
                List<MidiFile> curPopulation = new List<MidiFile>();
                // Создание новой популяции
                while(curPopulation.Count < sizeOfCreatedPopulation)
                {
                    var parents = Parents(allPopulation);
                    var children = Crossing(sizeOfChromosomeChords, sizeOfChromosomeNotes, parents, i);
                    foreach(var m in children)
                        curPopulation.Add(m);
                }
                foreach (var p in allPopulation)
                    curPopulation.Add(p);

                // Мутация (изменение curPopulation)
                Mutation(ref curPopulation, numberOfMutations, i);

                // Селекция

            }
        }

        private List<MidiFile> Selection(List<MidiFile> curPopulation, int sizeOfSelection)
        {
            List<MidiFile> selected = new List<MidiFile>();

            // Конвертация MIDI -> WAV

            // Жанровая классификация

            // Выбор 10-ти лучших мелодий

            return selected;
        }


        Random rnd;
        /// <summary>
        /// Выбор двух родителей, выбранных случайным образом из текущей популяции
        /// </summary>
        /// <param name="curPopulation"></param>
        /// <returns></returns>
        private List<MidiFile> Parents(List<MidiFile> curPopulation)
        {
            int sizeOfCurPopulation = curPopulation.Count();
            rnd = new Random();

            int firstParent = rnd.Next(0, curPopulation.Count());
            int secondParent = rnd.Next(0, curPopulation.Count());
            if(secondParent == firstParent)
                while(secondParent == firstParent)
                    secondParent = rnd.Next(0, curPopulation.Count());

            List<MidiFile> curParents = new List<MidiFile>();
            curParents.Add(curPopulation.ElementAt(firstParent));
            curParents.Add(curPopulation.ElementAt(secondParent));

            return curParents;
        }

        private List<MidiFile> Crossing(int sizeOfChromosomeChords, int sizeOfChromosomeNotes, List<MidiFile> parents, int numberCurPopulaiton)
        {
            rnd = new Random();
            int positionChords = rnd.Next(sizeOfChromosomeChords / 10, sizeOfChromosomeChords * 9 / 10);
            int positionNotes = rnd.Next(sizeOfChromosomeNotes / 4, sizeOfChromosomeNotes * 3 / 4);

            var firstParent = parents.ElementAt(0);
            var secondParent = parents.ElementAt(1);

            var firstParentChords = firstParent.GetChords();
            var secondParentChords = secondParent.GetChords();
            var firstParentNotes = firstParent.GetNotes();
            var secondParentNotes = secondParent.GetNotes();

            List<Chord> chromosome_1_chords = new List<Chord>();
            List<Note> chromosome_1_notes = new List<Note>();
            List<Chord> chromosome_2_chords = new List<Chord>();
            List<Note> chromosome_2_notes = new List<Note>();

            // Скрещивание (одноточечный кроссовер)
            for (int i = 0; i < positionChords; i++)
            {
                chromosome_1_chords.Add(firstParentChords.ElementAt(i));
                chromosome_2_chords.Add(secondParentChords.ElementAt(i));
            }
            for (int i = positionChords; i < sizeOfChromosomeChords; i++)
            {
                var time = secondParentChords.ElementAt(i).Time;
                secondParentChords.ElementAt(i).Time = firstParentChords.ElementAt(i).Time;
                firstParentChords.ElementAt(i).Time = time;
                chromosome_1_chords.Add(secondParentChords.ElementAt(i));
                chromosome_2_chords.Add(firstParentChords.ElementAt(i));
            }

            List<MidiFile> children = new List<MidiFile>();
            children.Add(WriteToFile(chromosome_1_chords, numberCurPopulaiton));
            children.Add(WriteToFile(chromosome_2_chords, numberCurPopulaiton));
            return children;
        }

        private MidiFile WriteToFile(List<Chord> chords, int numberCurPopulaiton)
        {
            var midiFile = new MidiFile();
            var tempoMap = midiFile.GetTempoMap();

            var trackChunk = new TrackChunk();
            using (var notesManager = trackChunk.ManageNotes())
            {
                foreach(var chord in chords)
                    notesManager.Notes.Add(chord.Notes);
            }

            midiFile.Chunks.Add(trackChunk);
            midiFile.Write(@"D:\3 курс\курсовая\CW3_GenMusic_WindowsFormsApp\CW3_GenMusic_WindowsFormsApp\bin\Debug\musicToConvert\music" + numberCurPopulaiton + "_" + DateTime.Now.ToString().Replace(':', ',').Replace(' ', '_') + "_" + rnd.Next() + ".mid");
            return midiFile;
        }

        private void Mutation(ref List<MidiFile> curPopulation, int numberOfMutations, int numberCurPopulation)
        {
            int firstChromosome = rnd.Next(0, curPopulation.Count());
            int secondChromosome = rnd.Next(0, curPopulation.Count());
            if (secondChromosome == firstChromosome)
                while (secondChromosome == firstChromosome)
                    secondChromosome = rnd.Next(0, curPopulation.Count());

            rnd = new Random();
            int typeOfMutation = rnd.Next(0, 3);
            MidiFile mutant = null;
            switch (typeOfMutation)
            {
                case 0:
                    mutant = MutationOneNote(curPopulation.ElementAt(firstChromosome));
                    break;
                case 1:
                    mutant = MutationFragmentsChange(curPopulation.ElementAt(firstChromosome), numberCurPopulation);
                    break;
                case 2:
                    mutant = MutationFragmentNotesChange(curPopulation.ElementAt(firstChromosome), numberCurPopulation);
                    break;
            }
            curPopulation.Add(mutant);

            typeOfMutation = rnd.Next(0, 3);
            switch (typeOfMutation)
            {
                case 0:
                    mutant = MutationOneNote(curPopulation.ElementAt(secondChromosome));
                    break;
                case 1:
                    mutant = MutationFragmentsChange(curPopulation.ElementAt(secondChromosome), numberCurPopulation);
                    break;
                case 2:
                    mutant = MutationFragmentNotesChange(curPopulation.ElementAt(secondChromosome), numberCurPopulation);
                    break;
            }
            curPopulation.Add(mutant);
        }
        /// <summary>
        /// 1.	Мутация одной ноты – замена случайным образом одной ноты на другую
        /// </summary>
        private MidiFile MutationOneNote(MidiFile music)
        {
            rnd = new Random();
            int notesCount = music.GetNotes().Count();

            int posicionOld = rnd.Next(0, notesCount);
            int positionNew = rnd.Next(0, notesCount);
            var newNote = music.GetNotes().ElementAt(positionNew);

            music.GetNotes().ElementAt(posicionOld).SetNoteNameAndOctave(newNote.NoteName, newNote.Octave);
            return music;
        }

        /// <summary>
        /// 2.	Обмен фрагментов мелодии – разбиение мелодии на одинаковые фрагменты и перестановка данных фрагментов местами.
        /// </summary>
        private MidiFile MutationFragmentsChange(MidiFile music, int numberCurPopulation)
        {
            rnd = new Random();
            int chordsCount = music.GetChords().Count();
            int lengthOfFragment = rnd.Next(chordsCount / 100, chordsCount / 50);
            int startPos1 = rnd.Next(0, chordsCount / 2);
            int startPos2 = rnd.Next(chordsCount / 2, chordsCount - chordsCount / 50);

            var musicChords = music.GetChords();
            List<Chord> fragment1 = new List<Chord>();
            List<Chord> fragment2 = new List<Chord>();

            int start = startPos2;
            for (int i = startPos1; i < startPos1 + lengthOfFragment; i++)
            {
                var time = musicChords.ElementAt(i).Time;
                musicChords.ElementAt(i).Time = musicChords.ElementAt(start).Time;
                musicChords.ElementAt(start).Time = time;
                fragment1.Add(musicChords.ElementAt(start++));
                fragment2.Add(musicChords.ElementAt(i));
            }

            List<Chord> newMusic = new List<Chord>();
            for(int i = 0; i < startPos1; i++)
                newMusic.Add(musicChords.ElementAt(i));
            int pos = 0;
            for (int i = startPos1; i < startPos1 + lengthOfFragment; i++)
                newMusic.Add(fragment1.ElementAt(pos++));
            for (int i = startPos1 + lengthOfFragment; i < startPos2; i++)
                newMusic.Add(musicChords.ElementAt(i));
            pos = 0;
            for (int i = startPos2; i < startPos2 + lengthOfFragment; i++)
                newMusic.Add(fragment2.ElementAt(pos++));
            for (int i = startPos2 + lengthOfFragment; i < chordsCount; i++)
                newMusic.Add(musicChords.ElementAt(i));

            return WriteToFile(newMusic, numberCurPopulation);
        }

        /// <summary>
        /// 3.	Перестановка нот в случайном фрагменте мелодии.
        /// </summary>
        private MidiFile MutationFragmentNotesChange(MidiFile music, int numberCurPopulation)
        {
            rnd = new Random();
            int chordsCount = music.GetChords().Count();
            int lengthOfFragment = rnd.Next(chordsCount / 100, chordsCount / 50);
            int startPos = rnd.Next(0, chordsCount - chordsCount / 50);

            var musicChords = music.GetChords();
            List<Chord> fragment = new List<Chord>();

            for (int i = startPos; i < startPos + lengthOfFragment; i++)
            {
                int positionToChange = rnd.Next(startPos, startPos + lengthOfFragment);
                var time = musicChords.ElementAt(i).Time;
                musicChords.ElementAt(positionToChange).Time = time;
                fragment.Add(musicChords.ElementAt(positionToChange));
            }

            List<Chord> newMusic = new List<Chord>();
            for (int i = 0; i < startPos; i++)
                newMusic.Add(musicChords.ElementAt(i));
            int pos = 0;
            for (int i = startPos; i < startPos + lengthOfFragment; i++)
                newMusic.Add(fragment.ElementAt(pos++));
            for (int i = startPos + lengthOfFragment; i < chordsCount; i++)
                newMusic.Add(musicChords.ElementAt(i));
          
            return WriteToFile(newMusic, numberCurPopulation);
        }

        /// <summary>
        /// Извлечение кодировок полученных музыкальных классов (жанров)
        /// </summary>
        /// <returns></returns>
        private List<int> ExtractClasses()
        {
            string data = @".\Data\data.csv";
            string[] musicData = File.ReadAllLines(data);
            List<int> classes = new List<int>();
            for (int i = 1; i < musicData.Length; i++)
            {
                var mas = musicData[i].Split(',');
                classes.Add(Convert.ToInt32(mas.Last().Substring(0, mas.Last().IndexOf('.'))));
            }
            return classes;
        }

        /// <summary>
        /// Классификация мелодий с использованием нейросети.
        /// </summary>
        private bool ClassifyMelodies()
        {
            // Директория скрипта neuroClassifier.py
            string cmd_classify = @".\Scripts\neuroClassifier.py";
            ProcessStartInfo start_reading = new ProcessStartInfo();
            start_reading.FileName = @"C:\Users\Яна\AppData\Local\Programs\Python\Python36\python.exe";
            start_reading.Arguments = string.Format("{0}", cmd_classify);
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
