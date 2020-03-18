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
            this.button2 = new System.Windows.Forms.Button();
            this.lbStartPopulationWAV = new System.Windows.Forms.ListBox();
            this.labelGeneratedMelodies = new System.Windows.Forms.Label();
            this.bnRemoveMIDI = new System.Windows.Forms.Button();
            this.bnAddMIDI = new System.Windows.Forms.Button();
            this.bnGenMusic = new System.Windows.Forms.Button();
            this.lablStartPopulation = new System.Windows.Forms.Label();
            this.lbStartPopulationMIDI = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurPopulation)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCurPopulation
            // 
            this.dgvCurPopulation.AllowUserToAddRows = false;
            this.dgvCurPopulation.AllowUserToDeleteRows = false;
            this.dgvCurPopulation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCurPopulation.Location = new System.Drawing.Point(387, 41);
            this.dgvCurPopulation.Name = "dgvCurPopulation";
            this.dgvCurPopulation.ReadOnly = true;
            this.dgvCurPopulation.Size = new System.Drawing.Size(344, 236);
            this.dgvCurPopulation.TabIndex = 20;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(79, 143);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(167, 23);
            this.button2.TabIndex = 19;
            this.button2.Text = "Очистить";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // lbStartPopulationWAV
            // 
            this.lbStartPopulationWAV.FormattingEnabled = true;
            this.lbStartPopulationWAV.Location = new System.Drawing.Point(15, 182);
            this.lbStartPopulationWAV.Name = "lbStartPopulationWAV";
            this.lbStartPopulationWAV.Size = new System.Drawing.Size(231, 95);
            this.lbStartPopulationWAV.TabIndex = 18;
            // 
            // labelGeneratedMelodies
            // 
            this.labelGeneratedMelodies.AutoSize = true;
            this.labelGeneratedMelodies.BackColor = System.Drawing.Color.Linen;
            this.labelGeneratedMelodies.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelGeneratedMelodies.Location = new System.Drawing.Point(387, 14);
            this.labelGeneratedMelodies.Name = "labelGeneratedMelodies";
            this.labelGeneratedMelodies.Size = new System.Drawing.Size(265, 24);
            this.labelGeneratedMelodies.TabIndex = 17;
            this.labelGeneratedMelodies.Text = "Сгенерированные мелодии:";
            // 
            // bnRemoveMIDI
            // 
            this.bnRemoveMIDI.Location = new System.Drawing.Point(47, 143);
            this.bnRemoveMIDI.Name = "bnRemoveMIDI";
            this.bnRemoveMIDI.Size = new System.Drawing.Size(26, 23);
            this.bnRemoveMIDI.TabIndex = 16;
            this.bnRemoveMIDI.Text = "--";
            this.bnRemoveMIDI.UseVisualStyleBackColor = true;
            this.bnRemoveMIDI.Click += new System.EventHandler(this.bnRemoveMIDI_Click);
            // 
            // bnAddMIDI
            // 
            this.bnAddMIDI.Location = new System.Drawing.Point(15, 143);
            this.bnAddMIDI.Name = "bnAddMIDI";
            this.bnAddMIDI.Size = new System.Drawing.Size(26, 23);
            this.bnAddMIDI.TabIndex = 15;
            this.bnAddMIDI.Text = "+";
            this.bnAddMIDI.UseVisualStyleBackColor = true;
            this.bnAddMIDI.Click += new System.EventHandler(this.bnAddMIDI_Click);
            // 
            // bnGenMusic
            // 
            this.bnGenMusic.Location = new System.Drawing.Point(261, 64);
            this.bnGenMusic.Name = "bnGenMusic";
            this.bnGenMusic.Size = new System.Drawing.Size(105, 53);
            this.bnGenMusic.TabIndex = 14;
            this.bnGenMusic.Text = "Сгенерировать мелодии";
            this.bnGenMusic.UseVisualStyleBackColor = true;
            this.bnGenMusic.Click += new System.EventHandler(this.bnGenMusic_Click);
            // 
            // lablStartPopulation
            // 
            this.lablStartPopulation.AutoSize = true;
            this.lablStartPopulation.BackColor = System.Drawing.Color.Linen;
            this.lablStartPopulation.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lablStartPopulation.Location = new System.Drawing.Point(15, 14);
            this.lablStartPopulation.Name = "lablStartPopulation";
            this.lablStartPopulation.Size = new System.Drawing.Size(190, 24);
            this.lablStartPopulation.TabIndex = 13;
            this.lablStartPopulation.Text = "Исходные мелодии:";
            // 
            // lbStartPopulationMIDI
            // 
            this.lbStartPopulationMIDI.FormattingEnabled = true;
            this.lbStartPopulationMIDI.Location = new System.Drawing.Point(15, 41);
            this.lbStartPopulationMIDI.Name = "lbStartPopulationMIDI";
            this.lbStartPopulationMIDI.Size = new System.Drawing.Size(231, 95);
            this.lbStartPopulationMIDI.TabIndex = 12;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FormGenMusic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(761, 345);
            this.Controls.Add(this.dgvCurPopulation);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lbStartPopulationWAV);
            this.Controls.Add(this.labelGeneratedMelodies);
            this.Controls.Add(this.bnRemoveMIDI);
            this.Controls.Add(this.bnAddMIDI);
            this.Controls.Add(this.bnGenMusic);
            this.Controls.Add(this.lablStartPopulation);
            this.Controls.Add(this.lbStartPopulationMIDI);
            this.Name = "FormGenMusic";
            this.Text = "Генератор мелодий";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCurPopulation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCurPopulation;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox lbStartPopulationWAV;
        private System.Windows.Forms.Label labelGeneratedMelodies;
        private System.Windows.Forms.Button bnRemoveMIDI;
        private System.Windows.Forms.Button bnAddMIDI;
        private System.Windows.Forms.Button bnGenMusic;
        private System.Windows.Forms.Label lablStartPopulation;
        private System.Windows.Forms.ListBox lbStartPopulationMIDI;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

