namespace MonteCarlo
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.startButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.widthTextBox = new System.Windows.Forms.TextBox();
            this.heightTextBox = new System.Windows.Forms.TextBox();
            this.conditionComboBox = new System.Windows.Forms.ComboBox();
            this.neighbourhoodBox = new System.Windows.Forms.ComboBox();
            this.ktBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numberOfIterationBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.growButton = new System.Windows.Forms.Button();
            this.energyButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(46, 49);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(684, 472);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(763, 414);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(91, 37);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseMnemonic = false;
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButtonClick);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(763, 469);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(91, 34);
            this.resetButton.TabIndex = 2;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // widthTextBox
            // 
            this.widthTextBox.Location = new System.Drawing.Point(763, 26);
            this.widthTextBox.Name = "widthTextBox";
            this.widthTextBox.Size = new System.Drawing.Size(100, 20);
            this.widthTextBox.TabIndex = 3;
            // 
            // heightTextBox
            // 
            this.heightTextBox.Location = new System.Drawing.Point(763, 67);
            this.heightTextBox.Name = "heightTextBox";
            this.heightTextBox.Size = new System.Drawing.Size(100, 20);
            this.heightTextBox.TabIndex = 4;
            // 
            // conditionComboBox
            // 
            this.conditionComboBox.FormattingEnabled = true;
            this.conditionComboBox.Items.AddRange(new object[] {
            "Periodyczne",
            "Absorbujące"});
            this.conditionComboBox.Location = new System.Drawing.Point(763, 134);
            this.conditionComboBox.Name = "conditionComboBox";
            this.conditionComboBox.Size = new System.Drawing.Size(100, 21);
            this.conditionComboBox.TabIndex = 5;
            // 
            // neighbourhoodBox
            // 
            this.neighbourhoodBox.FormattingEnabled = true;
            this.neighbourhoodBox.Items.AddRange(new object[] {
            "Moore",
            "Neumann"});
            this.neighbourhoodBox.Location = new System.Drawing.Point(763, 198);
            this.neighbourhoodBox.Name = "neighbourhoodBox";
            this.neighbourhoodBox.Size = new System.Drawing.Size(100, 21);
            this.neighbourhoodBox.TabIndex = 6;
            // 
            // ktBox
            // 
            this.ktBox.Location = new System.Drawing.Point(763, 257);
            this.ktBox.Name = "ktBox";
            this.ktBox.Size = new System.Drawing.Size(100, 20);
            this.ktBox.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(767, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Warunki brzegowe";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(767, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Typ sąsiedztwa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(804, 232);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "kt";
            // 
            // numberOfIterationBox
            // 
            this.numberOfIterationBox.Location = new System.Drawing.Point(763, 320);
            this.numberOfIterationBox.Name = "numberOfIterationBox";
            this.numberOfIterationBox.Size = new System.Drawing.Size(100, 20);
            this.numberOfIterationBox.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(783, 294);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Liczba iteracji";
            // 
            // growButton
            // 
            this.growButton.Location = new System.Drawing.Point(763, 364);
            this.growButton.Name = "growButton";
            this.growButton.Size = new System.Drawing.Size(91, 34);
            this.growButton.TabIndex = 13;
            this.growButton.Text = "Rozrost";
            this.growButton.UseVisualStyleBackColor = true;
            this.growButton.Click += new System.EventHandler(this.GrowButton_Click);
            // 
            // energyButton
            // 
            this.energyButton.Location = new System.Drawing.Point(763, 520);
            this.energyButton.Name = "energyButton";
            this.energyButton.Size = new System.Drawing.Size(91, 30);
            this.energyButton.TabIndex = 14;
            this.energyButton.Text = "Energia";
            this.energyButton.UseVisualStyleBackColor = true;
            this.energyButton.Click += new System.EventHandler(this.EnergyButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 562);
            this.Controls.Add(this.energyButton);
            this.Controls.Add(this.growButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numberOfIterationBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ktBox);
            this.Controls.Add(this.neighbourhoodBox);
            this.Controls.Add(this.conditionComboBox);
            this.Controls.Add(this.heightTextBox);
            this.Controls.Add(this.widthTextBox);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.TextBox widthTextBox;
        private System.Windows.Forms.TextBox heightTextBox;
        private System.Windows.Forms.ComboBox conditionComboBox;
        private System.Windows.Forms.ComboBox neighbourhoodBox;
        private System.Windows.Forms.TextBox ktBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox numberOfIterationBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button growButton;
        private System.Windows.Forms.Button energyButton;
    }
}

