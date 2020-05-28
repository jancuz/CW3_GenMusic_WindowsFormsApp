using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections.Generic;
using System.Collections;
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
using Melanchall.DryWetMidi.Devices;
using CsvHelper;
using System.Globalization;

namespace CW3_GenMusic_WindowsFormsApp
{
    public partial class FormGenMusic : Form
    {
        public FormGenMusic()
        {
            InitializeComponent();

            DeleteAllMIDI();
            DeleteAllWAV();

            // Заполнение жанров
            genresMusic.Add("blues");
            genresMusic.Add("classical");
            genresMusic.Add("country");
            genresMusic.Add("disco");
            genresMusic.Add("hiphop");
            genresMusic.Add("jazz");
            genresMusic.Add("metal");
            genresMusic.Add("pop");
            genresMusic.Add("reggae");
            genresMusic.Add("rock");

            foreach(var g in genresMusic)
                clbMusicGenre.Items.Add(g);


            // Заполняем таблицу
            var columnName = new DataGridViewColumn();
            columnName.HeaderText = "Название";
            columnName.ReadOnly = true; 
            columnName.Name = "name";
            columnName.CellTemplate = new DataGridViewTextBoxCell();

            //DataGridViewButtonColumn columnPlayButton = new DataGridViewButtonColumn();
            //columnPlayButton.HeaderText = "Проигрыватель";
            //columnPlayButton.Name = "play";
            //columnPlayButton.Text = "play/stop";
            //columnPlayButton.UseColumnTextForButtonValue = true;

            var columnClassComboBox = new DataGridViewComboBoxColumn();
            DataTable data = new DataTable();
            data.Columns.Add(new DataColumn("Value", typeof(string)));
            data.Columns.Add(new DataColumn("Description", typeof(string)));

            data.Rows.Add("-1", "NaM");
            data.Rows.Add("0", "blues");
            data.Rows.Add("1", "classical");
            data.Rows.Add("2", "country");
            data.Rows.Add("3", "disco");
            data.Rows.Add("4", "hiphop");
            data.Rows.Add("5", "jazz");
            data.Rows.Add("6", "metal");
            data.Rows.Add("7", "pop");
            data.Rows.Add("8", "reaggae");
            data.Rows.Add("9", "rock");

            columnClassComboBox.DataSource = data;
            columnClassComboBox.ValueMember = "Value";
            columnClassComboBox.DisplayMember = "Description";
            columnClassComboBox.HeaderText = "Класс";
            columnClassComboBox.Name = "class";

            dgvCurPopulation.Columns.Add(columnName);
            //dgvCurPopulation.Columns.Add(columnPlayButton);
            dgvCurPopulation.Columns.Add(columnClassComboBox);

            dgvCurPopulation.AllowUserToAddRows = false;


        }
        // список жанров
        List<string> genresMusic = new List<string>();
        //int selectedGenre = -2;
        // Стартовая популяция - текущие родители
        public List<MusicFile> filesCurStartPopulation = new List<MusicFile>();
        public List<MusicFile> filesStartPopulation = new List<MusicFile>();
        // Сгенерированная популяция - потомки
        public List<MusicFile> filesCurPopulation = new List<MusicFile>();
        // Результат
        public List<MusicFile> filesResult = new List<MusicFile>();
        public List<int> startClasses = new List<int>();
        string pathToPython = @"C:\Users\Яна\AppData\Local\Programs\Python\Python36\python.exe";

        /// <summary>
        /// Добавление мелодии.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bnAddMIDI_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = @".\music";
            openFileDialog1.Filter = "MIDI|*.mid|MIDI|*.midi";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string fileDirectoryName = openFileDialog1.FileName;
            string fileName = fileDirectoryName.Substring(fileDirectoryName.LastIndexOf(@"\") + 1);
            string fileExt = fileName.Substring(fileName.LastIndexOf("."));

            if (filesStartPopulation.Contains(new MusicFile(fileName, fileExt, fileDirectoryName)) == true)
                MessageBox.Show("Данный файл уже добавлен!");
            else
            {
                // добавляем
                MusicFile mf = new MusicFile(fileName, fileExt, fileDirectoryName);
                lbStartPopulationMIDI.Items.Add(mf);
                filesCurStartPopulation.Add(mf);
                filesStartPopulation.Add(mf);
                // перемещаем в .\curPopulationMIDI
                File.Copy(mf.Directory, @".\curPopulationMIDI\" + mf.Name);
            }
        }

        /// <summary>
        /// Конвертация MIDI в WAV
        /// </summary>
        /// <returns></returns>
        private bool ConvertMIDI_WAV()
        {
            // Директория скрипта
            string cmd_classify = @".\Scripts\ConverterWAV_MIDI.py";
            ProcessStartInfo start_reading = new ProcessStartInfo();
            start_reading.FileName = pathToPython;
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
        /// Удаление мелодии.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bnRemoveMIDI_Click(object sender, EventArgs e)
        {
            if (lbStartPopulationMIDI.SelectedItems.Count != 0)
            {
                var sItem = lbStartPopulationMIDI.SelectedIndex;
                if (File.Exists(@".\curPopulationMIDI\" + filesCurStartPopulation.ElementAt(sItem).Name))
                {
                    File.Delete(@".\curPopulationMIDI\" + filesCurStartPopulation.ElementAt(sItem).Name);
                    filesCurStartPopulation.RemoveAt(sItem);
                }
                filesStartPopulation.RemoveAt(sItem);
                lbStartPopulationMIDI.Items.RemoveAt(sItem);
            }
        }

        /// <summary>
        /// Создание новых мелодий.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bnGenMusic_Click(object sender, EventArgs e)
        {
            // Формирование стартовой популяции
            if(filesCurStartPopulation.Count() == 0)
            {
                foreach(var mf in filesStartPopulation)
                {
                    // перемещаем в .\curPopulationMIDI
                    File.Copy(mf.Directory, @".\curPopulationMIDI\" + mf.Name);
                    filesCurStartPopulation.Add(mf);
                }
            }

            // конвертация файла и размещение в .\curPopulationWAV
            ConvertMIDI_WAV();

            // Выбор начальной популяции
            // Извлечение данных из мелодий для последующей оценки нейросетью
            bool execute = false;
            do
            {
                execute = ExtractDataFromMelodies();
                // Удаление оцененных мелодий WAV
                foreach (var item in lbStartPopulationMIDI.Items)
                {
                    var mf = item as MusicFile;
                    string path = @".\curPopulationWAV\";
                    File.Delete(path + mf.Name.Replace(".mid", ".wav"));
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

            // Запись в итоговую таблицу
            //WriteResultData();

            // Извлечение оценок (фитнесс)
            List<int> classes = ExtractClasses();
            startClasses = new List<int>();
            int i = 0;
            for (int j = 0; j < filesCurStartPopulation.Count; j++)
            {
                filesStartPopulation.ElementAt(j).ClassNeuro = classes.ElementAt(i);
                filesCurStartPopulation.ElementAt(j).ClassNeuro = classes.ElementAt(i);
                startClasses.Add(classes.ElementAt(i++));
            }

            // Формирование начальной популяции
            List<int> chords = new List<int>();
            List<int> notes = new List<int>();
            // Извлечение информации о первой популяции
            List<MidiFile> firstPopulation = new List<MidiFile>();
            foreach (var melody in filesCurStartPopulation)
            {
                var melodyMIDI = MidiFile.Read(melody.Directory);
                firstPopulation.Add(melodyMIDI);
                chords.Add(melodyMIDI.GetChords().Count());
                notes.Add(melodyMIDI.GetNotes().Count());
            }

            int sizeOfСhromosomeChords = chords.Min();
            int sizeOfСhromosomeNotes = notes.Min();

            int numberOfGeneratedPopulations = Convert.ToInt32(tBNumberOfIter.Text.ToString());
            int sizeOfCreatedPopulation = Convert.ToInt32(tBSizeOfPopulation.Text.ToString());
            int sizeOfSelection = Convert.ToInt32(tbSizeOfSelection.Text.ToString());
            int numberOfMutations = Convert.ToInt32(tbNumbOfMutations.Text.ToString());
            if (sizeOfSelection > sizeOfCreatedPopulation)
                MessageBox.Show("Размер отбираемой популяции должен быть < чем Размер генерируемой популяции!");
            else
                if (numberOfMutations > sizeOfCreatedPopulation)
                    MessageBox.Show("Кол-во мутаций должно быть <= чем Размер генерируемой популяции!");
                else
                    // Выполнение ГА
                    GA(firstPopulation, classes, sizeOfСhromosomeChords, sizeOfСhromosomeNotes, numberOfGeneratedPopulations, sizeOfCreatedPopulation);
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
            int numberOfGeneratedPopulations = 20, int sizeOfCreatedPopulation = 20, int sizeOfSelection = 10, int numberOfMutations = 2)
        {
            for(int i = 0; i < numberOfGeneratedPopulations; i++)
            {
                List<MidiFile> curPopulation = new List<MidiFile>();
                // Создание новой популяции
                while(curPopulation.Count < sizeOfCreatedPopulation)
                {
                    var parents = Parents(allPopulation);
                    // Определение размера хромосомы
                    List<int> chords = new List<int>();
                    List<int> notes = new List<int>();
                    foreach (var melody in parents)
                    {
                        chords.Add(melody.GetChords().Count());
                        notes.Add(melody.GetNotes().Count());
                    }

                    sizeOfChromosomeChords = chords.Min();
                    sizeOfChromosomeNotes = notes.Min();

                    var children = Crossing(sizeOfChromosomeChords, sizeOfChromosomeNotes, parents, i);
                    foreach(var m in children)
                        curPopulation.Add(m);
                }

                // Мутация (изменение curPopulation)
                Mutation(ref curPopulation, numberOfMutations, i);

                // Селекция
                allPopulation = Selection(curPopulation, sizeOfSelection, fitness, numberOfGeneratedPopulations, i);
            }

            if (filesResult.Count == 0)
                MessageBox.Show("Мелодии не были сгенерированны для данного жанра");

            // Вывод результата
            for(int i = 0; i < filesResult.Count; i++)
            {
                dgvCurPopulation.Rows.Add();
                dgvCurPopulation.Rows[i].Cells[0].Value = filesResult.ElementAt(i).Name;
                // мелодия подходит по изначальному жанру -> выделяем цветом
                if (startClasses.Contains(filesResult.ElementAt(i).ClassNeuro))
                    dgvCurPopulation.Rows[i].Cells[0].Style.BackColor = Color.Green;
            }

        }

        private List<MidiFile> Selection(List<MidiFile> curPopulation, int sizeOfSelection, List<int> fitness, int numberOfGeneratedPopulations, int numCurPopulation)
        {
            List<MidiFile> selected = new List<MidiFile>();

            // Удаление родительской популяции
            foreach(var m in filesCurStartPopulation)
                File.Delete(@".\curPopulationMIDI\" + m.Name);

            // Конвертация MIDI -> WAV
            ConvertMIDI_WAV();

            // Жанровая классификация (скрипт)
            ExtractDataFromMelodies();
            ClassifyMelodies();
            List<int> classes = ExtractClasses();
            int j = 0;
            foreach (var cp in filesCurPopulation)
            {
                if(j < classes.Count())
                    cp.ClassNeuro = classes.ElementAt(j);
                j++;
            }

            // Выбор sizeOfSelection-ти лучших мелодий
            int count = 0;
            List <MusicFile> selectedMusic = new List<MusicFile>(); // выбранные мелодии MIDI с директорией
            List<int> posSelected = new List<int>(); // выбранные мелодии (индексы)
            for(int i = 0; i < classes.Count; i++)
            {
                // проверка на совпадение с фитнесс-функцией
                if(fitness.Contains(classes[i]) || (clbMusicGenre.SelectedIndex != -1 && fitness.Contains(clbMusicGenre.SelectedIndex)))
                {
                    selectedMusic.Add(filesCurPopulation.ElementAt(i));
                    posSelected.Add(i);
                    count++;
                }
                if (count == sizeOfSelection)
                    break;
            }

            // Последняя селекция (перемещение итоговой мелодии в папку .\result)
            if (numberOfGeneratedPopulations == numCurPopulation+1)
            {
                //// Отбор результирующей популяции в соответствии с изначальными классами
                //ResultPopulation(selectedMusic);
                //// очищение текущей популяции
                //filesCurStartPopulation = new List<MusicFile>();
                //filesCurPopulation = new List<MusicFile>();
                //DeleteAllWAV();
                //DeleteAllMIDI();
                //// Формирование итоговой популяции
                //FormSelection(posSelected, curPopulation, ref selected);

                // Выбор всех мелодий
                for (int i = 0; i < classes.Count; i++)
                {
                    // добавление еще недобавленных мелодий
                    if (!selectedMusic.Contains(filesCurPopulation.ElementAt(i)))
                    {
                        selectedMusic.Add(filesCurPopulation.ElementAt(i));
                        posSelected.Add(i);
                        count++;
                    }
                    if (count == sizeOfSelection)
                        break;
                }

                ResultPopulation(selectedMusic);
                // очищение текущей популяции
                filesCurStartPopulation = new List<MusicFile>();
                filesCurPopulation = new List<MusicFile>();
                DeleteAllWAV();
                DeleteAllMIDI();
                // Формирование итоговой популяции
                FormSelection(posSelected, curPopulation, ref selected);

                return selected;
            }

            // Если сгенерированные мелодии не принадлежат изначальному заданному классу
            if (count < sizeOfSelection)
            {
                for (int i = 0; i < classes.Count; i++)
                {
                    // добавление еще недобавленных мелодий
                    if (!selectedMusic.Contains(filesCurPopulation.ElementAt(i)))
                    {
                        selectedMusic.Add(filesCurPopulation.ElementAt(i));
                        posSelected.Add(i);
                        count++;
                    }
                    if (count == sizeOfSelection)
                        break;
                }
            }

            // новые родители
            NewParents(selectedMusic);

            // Удаление всех файлов WAV, НЕ отобранных MIDI
            DeleteAllWAV();
            DeleteNotSelectedMIDI(selectedMusic);

            // Формирование итоговой популяции
            FormSelection(posSelected, curPopulation, ref selected);
            return selected;
        }

        /// <summary>
        /// Выбор новых родителей.
        /// </summary>
        /// <param name="selectedMusic"></param>
        void NewParents(List<MusicFile> selectedMusic)
        {
            // новые родители
            filesCurStartPopulation = new List<MusicFile>();
            foreach (var f in selectedMusic)
                filesCurStartPopulation.Add(f);
            filesCurPopulation = new List<MusicFile>();
        }

        void FormSelection(List<int> posSelected, List<MidiFile> curPopulation, ref List<MidiFile> selected)
        {
            foreach (var p in posSelected)
                selected.Add(curPopulation.ElementAt(p));
        }

        /// <summary>
        /// Удаление НЕ отобранных MIDI
        /// </summary>
        /// <param name="selectedMusic"></param>
        void DeleteNotSelectedMIDI(List<MusicFile> selectedMusic)
        {
            var dir = @".\curPopulationMIDI";
            string[] files = Directory.GetFiles(dir);
            foreach (var f in files)
            {
                var name = f.Substring(f.LastIndexOf(@"\") + 1);
                if (!selectedMusic.Contains(new MusicFile(name, ".mid", f)))
                    File.Delete(f);
            }
        }

        /// <summary>
        /// Удаление всех MIDI
        /// </summary>
        void DeleteAllMIDI()
        {
            var dir = @".\curPopulationMIDI";
            var files = Directory.GetFiles(dir);
            foreach (var f in files)
                File.Delete(f);
        }

        /// <summary>
        /// Удаление всех WAV
        /// </summary>
        void DeleteAllWAV()
        {
            var dir = @".\curPopulationWAV";
            var files = Directory.GetFiles(dir);
            foreach (var f in files)
                File.Delete(f);
        }

        /// <summary>
        /// Формирование результирующей популяции, записывание в .\result
        /// </summary>
        /// <param name="selectedMusic"></param>
        void ResultPopulation(List<MusicFile> selectedMusic)
        {
            filesResult = new List<MusicFile>();
            var dir = @".\curPopulationMIDI";
            string[] files = Directory.GetFiles(dir);
            foreach (var f in files)
            {
                var name = f.Substring(f.LastIndexOf(@"\") + 1);
                var mf = new MusicFile(name, ".mid", f);
                if (selectedMusic.Contains(mf))
                {
                    File.Copy(f, @".\result\" + name);
                    filesResult.Add(new MusicFile(name, ".mid", f, selectedMusic.ElementAt(selectedMusic.IndexOf(mf)).ClassNeuro));
                }
            }

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
            string name = numberCurPopulaiton + "_" + DateTime.Now.ToString().Replace(':', ',').Replace(' ', '_') + "_" + rnd.Next() + ((char)rnd.Next('A', 'Z' + 1)).ToString() + ".mid";
            string format = ".mid";
            string directory = @".\curPopulationMIDI\" + name;

            // проверка на существование файла
            while(File.Exists(directory))
            {
                name = numberCurPopulaiton + "_" + DateTime.Now.ToString().Replace(':', ',').Replace(' ', '_') + "_" + rnd.Next() + ((char)rnd.Next('A', 'Z' + 1)).ToString() + ".mid";
                directory = @".\curPopulationMIDI\" + name;
            }

            filesCurPopulation.Add(new MusicFile(name, format, directory));

            midiFile.Write(directory);
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
                if (musicData[i] != "")
                {
                    var mas = musicData[i].Split(',');
                    classes.Add(Convert.ToInt32(mas.Last().Substring(0, mas.Last().IndexOf('.'))));
                }
            }
            return classes;
        }

        /// <summary>
        /// Записывание оценок (итоговая таблица для расчета оценочных метрик).
        /// </summary>
        private bool WriteResultData()
        {
            // Директория скрипта neuroClassifier.py
            string cmd_classify = @".\Scripts\writeResultData.py";
            ProcessStartInfo start_reading = new ProcessStartInfo();
            start_reading.FileName = pathToPython;
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
        /// Классификация мелодий с использованием нейросети.
        /// </summary>
        private bool ClassifyMelodies()
        {
            // Директория скрипта neuroClassifier.py
            string cmd_classify = @".\Scripts\neuroClassifier.py";
            ProcessStartInfo start_reading = new ProcessStartInfo();
            start_reading.FileName = pathToPython;
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
        /// Извлечение следующих характеристик из мелодий.wav с использованием скрипта на на python:
        /// filename,chroma_stft,spectral_centroid,spectral_bandwidth,rolloff,zero_crossing_rate,mfcc1, ...,mfcc20
        /// </summary>
        /// <returns></returns>
        private bool ExtractDataFromMelodies()
        {
            // Директория скрипта reader.py
            string cmd_read = @".\Scripts\reader.py";
            ProcessStartInfo start_reading = new ProcessStartInfo();
            start_reading.FileName = @"C:\Python27\python.exe";
            start_reading.Arguments = string.Format("{0}", cmd_read);
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
        /// Выбор случайных мелодий определенного жанра
        /// </summary>
        /// <param name="genre"></param>
        void RandomMelodiesForSelectedGenre(string genre)
        {
            // Очищение списков
            lbStartPopulationMIDI.Items.Clear();
            filesCurStartPopulation = new List<MusicFile>();
            filesStartPopulation = new List<MusicFile>();
            // Очищение папок
            DeleteAllMIDI();
            DeleteAllWAV();

            string dir = @".\musicdb\" + genre;
            DirectoryInfo df = new DirectoryInfo(dir);
            var files = df.GetFiles();
            rnd = new Random();
            // размер начальной популяции
            int numberOfMelodies = rnd.Next(2, files.Count());
            // добавляемые мелодии
            List<int> melodies = new List<int>();
            melodies.Add(rnd.Next(0, files.Count()));
            for(int i = 1; i < numberOfMelodies; i++)
            {
                int m = rnd.Next(0, files.Count());
                while(melodies.Contains(m))
                    m = rnd.Next(0, files.Count());
                melodies.Add(m);
            }
            // Формируем начальную популяцию
            foreach (var m in melodies)
            {
                var fileDirectoryName = files[m].FullName;
                var fileName = fileDirectoryName.Substring(fileDirectoryName.LastIndexOf(@"\") + 1);
                var fileExt = fileName.Substring(fileName.LastIndexOf("."));
                MusicFile mf = new MusicFile(fileName, fileExt, fileDirectoryName);
                lbStartPopulationMIDI.Items.Add(mf);
                filesCurStartPopulation.Add(mf);
                filesStartPopulation.Add(mf);
                // перемещаем в .\curPopulationMIDI
                File.Copy(mf.Directory, @".\curPopulationMIDI\" + mf.Name);
            }
        }

        /// <summary>
        /// Изменение жанра генерируемых мелодий
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clbMusicGenre_SelectedIndexChanged(object sender, EventArgs e)
        {
            var sItem = clbMusicGenre.SelectedIndex;
            // жанр выбран
            if (clbMusicGenre.CheckedIndices.Count != 0)
            {
                string genre = genresMusic.ElementAt(sItem);
                RandomMelodiesForSelectedGenre(genre);
            }
            // жанр не выбран -> очищение списка
            else
            {
                // Очищение списков
                lbStartPopulationMIDI.Items.Clear();
                filesCurStartPopulation = new List<MusicFile>();
                filesStartPopulation = new List<MusicFile>();
                // Очищение папок
                DeleteAllMIDI();
                DeleteAllWAV();
            }
        }

        /// <summary>
        /// Воспроизведение мелодии - не работает
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCurPopulation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Игнорировать нажатие на другие ячейки.
            //if (e.RowIndex < 0 || e.ColumnIndex !=
                //dgvCurPopulation.Columns["play"].Index) return;

            //var musicToPlay = filesResult.ElementAt(Convert.ToInt32(e.RowIndex));
            //var mf = MidiFile.Read(musicToPlay.Directory).GetTimedEvents();

            //using (var outputDevice = OutputDevice.GetByName("Microsoft GS Wavetable Synth"))
            //using (var playback = new Playback(mf, TempoMap.Default, outputDevice))
            //{
                //playback.Start();
            //}
        }

        /// <summary>
        /// Сохранение введенной пользовательской оценки и оценки ИНС - потестить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bnSaveMark_Click(object sender, EventArgs e)
        {
            // Запись в .csv файл dataResult
            // указываем путь к файлу csv
            string pathCsvFile = @".\Data\dataResult.csv";

            // добавляем данные, которые будем записывать в csv файл
            List<CSVMusicGenre> classes = new List<CSVMusicGenre>();
            for(int i = 0; i < filesResult.Count; i++)
            {
                classes.Add(new CSVMusicGenre
                {
                    Name = filesResult.ElementAt(i).Name,
                    ClassPredicted = filesResult.ElementAt(i).ClassNeuro,
                    ClassUser = Convert.ToInt16(dgvCurPopulation.Rows[i].Cells[1].Value)
                });
            }

            int c = 0;
            foreach(var item in lbStartPopulationMIDI.Items)
            {
                var mf = item as MusicFile;
                classes.Add(new CSVMusicGenre
                {
                    Name = mf.Name,
                    ClassPredicted = startClasses.ElementAt(c++),
                    ClassUser = clbMusicGenre.SelectedIndex
                });
            }

            using (StreamWriter streamWriter = new StreamWriter(pathCsvFile))
            {
                using (CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    // указываем разделитель, который будет использоваться в файле
                    csvWriter.Configuration.Delimiter = ",";
                    // записываем данные в csv файл
                    csvWriter.WriteRecords(classes);
                }
            }

            MessageBox.Show("Оценка записана.");
        }

        /// <summary>
        /// Оценка работы модели
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bnAnalyze_Click(object sender, EventArgs e)
        {
            int TP = 0;
            int FN = 0;
            int FP = 0;
            int TN = 0;
            int count = 0;
            // чтение файла dataResult.csv
            string pathCsvFile = @".\Data\dataResultCalc.csv";
            using (StreamReader streamWriter = new StreamReader(pathCsvFile))
            {
                using (CsvReader csvWriter = new CsvReader(streamWriter, CultureInfo.InvariantCulture))
                {
                    var records = csvWriter.GetRecords<CSVMusicGenre>().ToList();
                    count = records.Count();
                    foreach (var r in records)
                    {
                        var prediction = r.ClassPredicted;
                        var user = r.ClassUser;
                        if (prediction == user && prediction != -1)
                            TP++;
                        if (prediction != -1 && user == -1)
                            FP++;
                        if (prediction == -1 && user != -1)
                            FN++;
                        if (prediction == -1 && user == -1)
                            TN++;
                    }
                }
            }
            if(count == 0)
            {
                MessageBox.Show("Результирующий файл пуст!");
                return;
            }

            // подсчет accuracy, precision, recall
            var accuracy = (TP + TN) / count;
            var precision = 0;
            var recall = 0;
            if (TP + FP != 0)
                precision = TP / (TP + FP);
            if (TP + FN != 0)
                recall = TP / (TP + FN);

            tbSizeOfSample.Text = "";
            tbSizeOfSample.Text += count;
            tbAccuracy.Text = "";
            tbAccuracy.Text += accuracy;
            tbPrecision.Text = "";
            tbPrecision.Text += precision;
        }

        private void bnClear_Click(object sender, EventArgs e)
        {

        }
    }
}
