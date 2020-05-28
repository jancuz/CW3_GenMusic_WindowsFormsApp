namespace CW3_GenMusic_WindowsFormsApp
{
    partial class FormGenMusic
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGenMusic));
            this.dgvCurPopulation = new System.Windows.Forms.DataGridView();
            this.bnRemoveMIDI = new System.Windows.Forms.Button();
            this.bnAddMIDI = new System.Windows.Forms.Button();
            this.bnGenMusic = new System.Windows.Forms.Button();
            this.lbStartPopulationMIDI = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.clbMusicGenre = new System.Windows.Forms.CheckedListBox();
            this.gbStartPopulation = new System.Windows.Forms.GroupBox();
            this.lablNumbOfMutations = new System.Windows.Forms.Label();
            this.tbNumbOfMutations = new System.Windows.Forms.TextBox();
            this.tbSizeOfSelection = new System.Windows.Forms.TextBox();
            this.lablSizeOfSelection = new System.Windows.Forms.Label();
            this.lablNumOfIter = new System.Windows.Forms.Label();
            this.tBNumberOfIter = new System.Windows.Forms.TextBox();
            this.tBSizeOfPopulation = new System.Windows.Forms.TextBox();
            this.lablSizePopulation = new System.Windows.Forms.Label();
            this.gBSelectedMelodies = new System.Windows.Forms.GroupBox();
            this.tbAccuracy = new System.Windows.Forms.TextBox();
            this.tbPrecision = new System.Windows.Forms.TextBox();
            this.tbSizeOfSample = new System.Windows.Forms.TextBox();
            this.lablSizeOfSample = new System.Windows.Forms.Label();
            this.lablAccuracy = new System.Windows.Forms.Label();
            this.lablPrecision = new System.Windows.Forms.Label();
            this.bnAnalyze = new System.Windows.Forms.Button();
            this.bnSaveMark = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurPopulation)).BeginInit();
            this.gbStartPopulation.SuspendLayout();
            this.gBSelectedMelodies.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvCurPopulation
            // 
            this.dgvCurPopulation.AllowUserToAddRows = false;
            this.dgvCurPopulation.AllowUserToDeleteRows = false;
            this.dgvCurPopulation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCurPopulation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCurPopulation.Location = new System.Drawing.Point(6, 19);
            this.dgvCurPopulation.Name = "dgvCurPopulation";
            this.dgvCurPopulation.Size = new System.Drawing.Size(427, 139);
            this.dgvCurPopulation.TabIndex = 20;
            this.dgvCurPopulation.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCurPopulation_CellClick);
            // 
            // bnRemoveMIDI
            // 
            this.bnRemoveMIDI.Location = new System.Drawing.Point(38, 170);
            this.bnRemoveMIDI.Name = "bnRemoveMIDI";
            this.bnRemoveMIDI.Size = new System.Drawing.Size(26, 23);
            this.bnRemoveMIDI.TabIndex = 16;
            this.bnRemoveMIDI.Text = "--";
            this.bnRemoveMIDI.UseVisualStyleBackColor = true;
            this.bnRemoveMIDI.Click += new System.EventHandler(this.bnRemoveMIDI_Click);
            // 
            // bnAddMIDI
            // 
            this.bnAddMIDI.Location = new System.Drawing.Point(6, 170);
            this.bnAddMIDI.Name = "bnAddMIDI";
            this.bnAddMIDI.Size = new System.Drawing.Size(26, 23);
            this.bnAddMIDI.TabIndex = 15;
            this.bnAddMIDI.Text = "+";
            this.bnAddMIDI.UseVisualStyleBackColor = true;
            this.bnAddMIDI.Click += new System.EventHandler(this.bnAddMIDI_Click);
            // 
            // bnGenMusic
            // 
            this.bnGenMusic.Location = new System.Drawing.Point(439, 19);
            this.bnGenMusic.Name = "bnGenMusic";
            this.bnGenMusic.Size = new System.Drawing.Size(105, 53);
            this.bnGenMusic.TabIndex = 14;
            this.bnGenMusic.Text = "Сгенерировать мелодии";
            this.bnGenMusic.UseVisualStyleBackColor = true;
            this.bnGenMusic.Click += new System.EventHandler(this.bnGenMusic_Click);
            // 
            // lbStartPopulationMIDI
            // 
            this.lbStartPopulationMIDI.FormattingEnabled = true;
            this.lbStartPopulationMIDI.Location = new System.Drawing.Point(6, 19);
            this.lbStartPopulationMIDI.Name = "lbStartPopulationMIDI";
            this.lbStartPopulationMIDI.Size = new System.Drawing.Size(302, 147);
            this.lbStartPopulationMIDI.TabIndex = 12;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // clbMusicGenre
            // 
            this.clbMusicGenre.CheckOnClick = true;
            this.clbMusicGenre.FormattingEnabled = true;
            this.clbMusicGenre.Location = new System.Drawing.Point(314, 19);
            this.clbMusicGenre.MultiColumn = true;
            this.clbMusicGenre.Name = "clbMusicGenre";
            this.clbMusicGenre.Size = new System.Drawing.Size(234, 124);
            this.clbMusicGenre.TabIndex = 21;
            this.clbMusicGenre.SelectedIndexChanged += new System.EventHandler(this.clbMusicGenre_SelectedIndexChanged);
            // 
            // gbStartPopulation
            // 
            this.gbStartPopulation.Controls.Add(this.lablNumbOfMutations);
            this.gbStartPopulation.Controls.Add(this.tbNumbOfMutations);
            this.gbStartPopulation.Controls.Add(this.tbSizeOfSelection);
            this.gbStartPopulation.Controls.Add(this.lablSizeOfSelection);
            this.gbStartPopulation.Controls.Add(this.lablNumOfIter);
            this.gbStartPopulation.Controls.Add(this.tBNumberOfIter);
            this.gbStartPopulation.Controls.Add(this.tBSizeOfPopulation);
            this.gbStartPopulation.Controls.Add(this.lablSizePopulation);
            this.gbStartPopulation.Controls.Add(this.lbStartPopulationMIDI);
            this.gbStartPopulation.Controls.Add(this.clbMusicGenre);
            this.gbStartPopulation.Controls.Add(this.bnAddMIDI);
            this.gbStartPopulation.Controls.Add(this.bnRemoveMIDI);
            this.gbStartPopulation.Location = new System.Drawing.Point(12, 11);
            this.gbStartPopulation.Name = "gbStartPopulation";
            this.gbStartPopulation.Size = new System.Drawing.Size(554, 259);
            this.gbStartPopulation.TabIndex = 22;
            this.gbStartPopulation.TabStop = false;
            this.gbStartPopulation.Text = "Исходные данные:";
            // 
            // lablNumbOfMutations
            // 
            this.lablNumbOfMutations.AutoSize = true;
            this.lablNumbOfMutations.Location = new System.Drawing.Point(8, 231);
            this.lablNumbOfMutations.Name = "lablNumbOfMutations";
            this.lablNumbOfMutations.Size = new System.Drawing.Size(89, 13);
            this.lablNumbOfMutations.TabIndex = 29;
            this.lablNumbOfMutations.Text = "Кол-во мутаций:";
            // 
            // tbNumbOfMutations
            // 
            this.tbNumbOfMutations.Location = new System.Drawing.Point(113, 228);
            this.tbNumbOfMutations.Name = "tbNumbOfMutations";
            this.tbNumbOfMutations.Size = new System.Drawing.Size(123, 20);
            this.tbNumbOfMutations.TabIndex = 28;
            this.tbNumbOfMutations.Text = "5";
            // 
            // tbSizeOfSelection
            // 
            this.tbSizeOfSelection.Location = new System.Drawing.Point(424, 206);
            this.tbSizeOfSelection.Name = "tbSizeOfSelection";
            this.tbSizeOfSelection.Size = new System.Drawing.Size(123, 20);
            this.tbSizeOfSelection.TabIndex = 27;
            this.tbSizeOfSelection.Text = "15";
            // 
            // lablSizeOfSelection
            // 
            this.lablSizeOfSelection.AutoSize = true;
            this.lablSizeOfSelection.Location = new System.Drawing.Point(317, 204);
            this.lablSizeOfSelection.Name = "lablSizeOfSelection";
            this.lablSizeOfSelection.Size = new System.Drawing.Size(71, 39);
            this.lablSizeOfSelection.TabIndex = 26;
            this.lablSizeOfSelection.Text = "Размер \r\nотбираемой \r\nпопуляцици:";
            // 
            // lablNumOfIter
            // 
            this.lablNumOfIter.AutoSize = true;
            this.lablNumOfIter.Location = new System.Drawing.Point(8, 199);
            this.lablNumOfIter.Name = "lablNumOfIter";
            this.lablNumOfIter.Size = new System.Drawing.Size(94, 13);
            this.lablNumOfIter.TabIndex = 25;
            this.lablNumOfIter.Text = "Кол-во итераций:";
            // 
            // tBNumberOfIter
            // 
            this.tBNumberOfIter.Location = new System.Drawing.Point(113, 196);
            this.tBNumberOfIter.Name = "tBNumberOfIter";
            this.tBNumberOfIter.Size = new System.Drawing.Size(123, 20);
            this.tBNumberOfIter.TabIndex = 24;
            this.tBNumberOfIter.Text = "20";
            // 
            // tBSizeOfPopulation
            // 
            this.tBSizeOfPopulation.Location = new System.Drawing.Point(424, 153);
            this.tBSizeOfPopulation.Name = "tBSizeOfPopulation";
            this.tBSizeOfPopulation.Size = new System.Drawing.Size(123, 20);
            this.tBSizeOfPopulation.TabIndex = 23;
            this.tBSizeOfPopulation.Text = "20";
            // 
            // lablSizePopulation
            // 
            this.lablSizePopulation.AutoSize = true;
            this.lablSizePopulation.Location = new System.Drawing.Point(316, 153);
            this.lablSizePopulation.Name = "lablSizePopulation";
            this.lablSizePopulation.Size = new System.Drawing.Size(82, 39);
            this.lablSizePopulation.TabIndex = 22;
            this.lablSizePopulation.Text = "Размер \r\nгенерируемой \r\nпопуляции:";
            // 
            // gBSelectedMelodies
            // 
            this.gBSelectedMelodies.Controls.Add(this.tbAccuracy);
            this.gBSelectedMelodies.Controls.Add(this.tbPrecision);
            this.gBSelectedMelodies.Controls.Add(this.tbSizeOfSample);
            this.gBSelectedMelodies.Controls.Add(this.lablSizeOfSample);
            this.gBSelectedMelodies.Controls.Add(this.lablAccuracy);
            this.gBSelectedMelodies.Controls.Add(this.lablPrecision);
            this.gBSelectedMelodies.Controls.Add(this.bnAnalyze);
            this.gBSelectedMelodies.Controls.Add(this.bnSaveMark);
            this.gBSelectedMelodies.Controls.Add(this.dgvCurPopulation);
            this.gBSelectedMelodies.Controls.Add(this.bnGenMusic);
            this.gBSelectedMelodies.Location = new System.Drawing.Point(12, 267);
            this.gBSelectedMelodies.Name = "gBSelectedMelodies";
            this.gBSelectedMelodies.Size = new System.Drawing.Size(554, 217);
            this.gBSelectedMelodies.TabIndex = 23;
            this.gBSelectedMelodies.TabStop = false;
            this.gBSelectedMelodies.Text = "Сгенерированные мелодии:";
            // 
            // tbAccuracy
            // 
            this.tbAccuracy.Location = new System.Drawing.Point(216, 190);
            this.tbAccuracy.Name = "tbAccuracy";
            this.tbAccuracy.ReadOnly = true;
            this.tbAccuracy.Size = new System.Drawing.Size(143, 20);
            this.tbAccuracy.TabIndex = 28;
            // 
            // tbPrecision
            // 
            this.tbPrecision.Location = new System.Drawing.Point(216, 166);
            this.tbPrecision.Name = "tbPrecision";
            this.tbPrecision.ReadOnly = true;
            this.tbPrecision.Size = new System.Drawing.Size(143, 20);
            this.tbPrecision.TabIndex = 27;
            // 
            // tbSizeOfSample
            // 
            this.tbSizeOfSample.Location = new System.Drawing.Point(108, 190);
            this.tbSizeOfSample.Name = "tbSizeOfSample";
            this.tbSizeOfSample.ReadOnly = true;
            this.tbSizeOfSample.Size = new System.Drawing.Size(44, 20);
            this.tbSizeOfSample.TabIndex = 26;
            // 
            // lablSizeOfSample
            // 
            this.lablSizeOfSample.AutoSize = true;
            this.lablSizeOfSample.Location = new System.Drawing.Point(6, 193);
            this.lablSizeOfSample.Name = "lablSizeOfSample";
            this.lablSizeOfSample.Size = new System.Drawing.Size(96, 13);
            this.lablSizeOfSample.TabIndex = 25;
            this.lablSizeOfSample.Text = "Размер выборки:";
            // 
            // lablAccuracy
            // 
            this.lablAccuracy.AutoSize = true;
            this.lablAccuracy.Location = new System.Drawing.Point(155, 193);
            this.lablAccuracy.Name = "lablAccuracy";
            this.lablAccuracy.Size = new System.Drawing.Size(55, 13);
            this.lablAccuracy.TabIndex = 24;
            this.lablAccuracy.Text = "Accuracy:";
            // 
            // lablPrecision
            // 
            this.lablPrecision.AutoSize = true;
            this.lablPrecision.Location = new System.Drawing.Point(157, 169);
            this.lablPrecision.Name = "lablPrecision";
            this.lablPrecision.Size = new System.Drawing.Size(53, 13);
            this.lablPrecision.TabIndex = 23;
            this.lablPrecision.Text = "Precision:";
            // 
            // bnAnalyze
            // 
            this.bnAnalyze.Location = new System.Drawing.Point(6, 164);
            this.bnAnalyze.Name = "bnAnalyze";
            this.bnAnalyze.Size = new System.Drawing.Size(146, 23);
            this.bnAnalyze.TabIndex = 22;
            this.bnAnalyze.Text = "Оценка работы модели";
            this.bnAnalyze.UseVisualStyleBackColor = true;
            this.bnAnalyze.Click += new System.EventHandler(this.bnAnalyze_Click);
            // 
            // bnSaveMark
            // 
            this.bnSaveMark.Location = new System.Drawing.Point(439, 78);
            this.bnSaveMark.Name = "bnSaveMark";
            this.bnSaveMark.Size = new System.Drawing.Size(105, 50);
            this.bnSaveMark.TabIndex = 21;
            this.bnSaveMark.Text = "Сохранить оценку";
            this.bnSaveMark.UseVisualStyleBackColor = true;
            this.bnSaveMark.Click += new System.EventHandler(this.bnSaveMark_Click);
            // 
            // FormGenMusic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(578, 492);
            this.Controls.Add(this.gBSelectedMelodies);
            this.Controls.Add(this.gbStartPopulation);
            this.Name = "FormGenMusic";
            this.Text = "Генератор мелодий";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurPopulation)).EndInit();
            this.gbStartPopulation.ResumeLayout(false);
            this.gbStartPopulation.PerformLayout();
            this.gBSelectedMelodies.ResumeLayout(false);
            this.gBSelectedMelodies.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCurPopulation;
        private System.Windows.Forms.Button bnRemoveMIDI;
        private System.Windows.Forms.Button bnAddMIDI;
        private System.Windows.Forms.Button bnGenMusic;
        private System.Windows.Forms.ListBox lbStartPopulationMIDI;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckedListBox clbMusicGenre;
        private System.Windows.Forms.GroupBox gbStartPopulation;
        private System.Windows.Forms.Label lablNumOfIter;
        private System.Windows.Forms.TextBox tBNumberOfIter;
        private System.Windows.Forms.TextBox tBSizeOfPopulation;
        private System.Windows.Forms.Label lablSizePopulation;
        private System.Windows.Forms.GroupBox gBSelectedMelodies;
        private System.Windows.Forms.TextBox tbAccuracy;
        private System.Windows.Forms.TextBox tbPrecision;
        private System.Windows.Forms.TextBox tbSizeOfSample;
        private System.Windows.Forms.Label lablSizeOfSample;
        private System.Windows.Forms.Label lablAccuracy;
        private System.Windows.Forms.Label lablPrecision;
        private System.Windows.Forms.Button bnAnalyze;
        private System.Windows.Forms.Button bnSaveMark;
        private System.Windows.Forms.Label lablNumbOfMutations;
        private System.Windows.Forms.TextBox tbNumbOfMutations;
        private System.Windows.Forms.TextBox tbSizeOfSelection;
        private System.Windows.Forms.Label lablSizeOfSelection;
    }
}

