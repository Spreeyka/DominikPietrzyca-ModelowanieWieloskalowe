namespace Rekrystalizacja
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
            this.GrowButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            this.energyButton = new System.Windows.Forms.Button();
            this.heightTextBox = new System.Windows.Forms.TextBox();
            this.widthTextBox = new System.Windows.Forms.TextBox();
            this.conditionComboBox = new System.Windows.Forms.ComboBox();
            this.neighbourhoodBox = new System.Windows.Forms.ComboBox();
            this.ktBox = new System.Windows.Forms.TextBox();
            this.numberOfIterationBox = new System.Windows.Forms.TextBox();
            this.dislocationButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(92, 32);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(715, 532);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // GrowButton
            // 
            this.GrowButton.Location = new System.Drawing.Point(891, 281);
            this.GrowButton.Name = "GrowButton";
            this.GrowButton.Size = new System.Drawing.Size(96, 40);
            this.GrowButton.TabIndex = 1;
            this.GrowButton.Text = "Rozrost";
            this.GrowButton.UseVisualStyleBackColor = true;
            this.GrowButton.Click += new System.EventHandler(this.GrowButton_Click_1);
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(893, 345);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(94, 40);
            this.StartButton.TabIndex = 2;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(893, 408);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(94, 41);
            this.ResetButton.TabIndex = 3;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click_1);
            // 
            // energyButton
            // 
            this.energyButton.Location = new System.Drawing.Point(893, 526);
            this.energyButton.Name = "energyButton";
            this.energyButton.Size = new System.Drawing.Size(94, 38);
            this.energyButton.TabIndex = 4;
            this.energyButton.Text = "Energia";
            this.energyButton.UseVisualStyleBackColor = true;
            this.energyButton.Click += new System.EventHandler(this.EnergyButton_Click_1);
            // 
            // heightTextBox
            // 
            this.heightTextBox.Location = new System.Drawing.Point(893, 60);
            this.heightTextBox.Name = "heightTextBox";
            this.heightTextBox.Size = new System.Drawing.Size(100, 20);
            this.heightTextBox.TabIndex = 5;
            // 
            // widthTextBox
            // 
            this.widthTextBox.Location = new System.Drawing.Point(893, 21);
            this.widthTextBox.Name = "widthTextBox";
            this.widthTextBox.Size = new System.Drawing.Size(100, 20);
            this.widthTextBox.TabIndex = 6;
            // 
            // conditionComboBox
            // 
            this.conditionComboBox.FormattingEnabled = true;
            this.conditionComboBox.Location = new System.Drawing.Point(893, 96);
            this.conditionComboBox.Name = "conditionComboBox";
            this.conditionComboBox.Size = new System.Drawing.Size(121, 21);
            this.conditionComboBox.TabIndex = 7;
            // 
            // neighbourhoodBox
            // 
            this.neighbourhoodBox.FormattingEnabled = true;
            this.neighbourhoodBox.Location = new System.Drawing.Point(893, 135);
            this.neighbourhoodBox.Name = "neighbourhoodBox";
            this.neighbourhoodBox.Size = new System.Drawing.Size(121, 21);
            this.neighbourhoodBox.TabIndex = 8;
            // 
            // ktBox
            // 
            this.ktBox.Location = new System.Drawing.Point(893, 184);
            this.ktBox.Name = "ktBox";
            this.ktBox.Size = new System.Drawing.Size(100, 20);
            this.ktBox.TabIndex = 9;
            // 
            // numberOfIterationBox
            // 
            this.numberOfIterationBox.Location = new System.Drawing.Point(893, 228);
            this.numberOfIterationBox.Name = "numberOfIterationBox";
            this.numberOfIterationBox.Size = new System.Drawing.Size(100, 20);
            this.numberOfIterationBox.TabIndex = 10;
            // 
            // dislocationButton
            // 
            this.dislocationButton.Location = new System.Drawing.Point(893, 468);
            this.dislocationButton.Name = "dislocationButton";
            this.dislocationButton.Size = new System.Drawing.Size(94, 40);
            this.dislocationButton.TabIndex = 11;
            this.dislocationButton.Text = "Dyslokacje";
            this.dislocationButton.UseVisualStyleBackColor = true;
            this.dislocationButton.Click += new System.EventHandler(this.DislocationButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 603);
            this.Controls.Add(this.dislocationButton);
            this.Controls.Add(this.numberOfIterationBox);
            this.Controls.Add(this.ktBox);
            this.Controls.Add(this.neighbourhoodBox);
            this.Controls.Add(this.conditionComboBox);
            this.Controls.Add(this.widthTextBox);
            this.Controls.Add(this.heightTextBox);
            this.Controls.Add(this.energyButton);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.GrowButton);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button GrowButton;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button energyButton;
        private System.Windows.Forms.TextBox heightTextBox;
        private System.Windows.Forms.TextBox widthTextBox;
        private System.Windows.Forms.ComboBox conditionComboBox;
        private System.Windows.Forms.ComboBox neighbourhoodBox;
        private System.Windows.Forms.TextBox ktBox;
        private System.Windows.Forms.TextBox numberOfIterationBox;
        private System.Windows.Forms.Button dislocationButton;
    }
}

