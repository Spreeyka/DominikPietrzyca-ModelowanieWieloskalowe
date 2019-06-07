namespace RozrostZiarna
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
            this.button1 = new System.Windows.Forms.Button();
            this.widthTextBox = new System.Windows.Forms.TextBox();
            this.heightTextBox = new System.Windows.Forms.TextBox();
            this.patternComboBox = new System.Windows.Forms.ComboBox();
            this.resetButton = new System.Windows.Forms.Button();
            this.chooseColorComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.conditionComboBox = new System.Windows.Forms.ComboBox();
            this.radiusBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.neighbourhoodBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.stopButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(9, 23);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(633, 456);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.InsertElementClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(669, 388);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 31);
            this.button1.TabIndex = 1;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Start_button_Click);
            // 
            // widthTextBox
            // 
            this.widthTextBox.Location = new System.Drawing.Point(676, 23);
            this.widthTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.widthTextBox.Name = "widthTextBox";
            this.widthTextBox.Size = new System.Drawing.Size(76, 20);
            this.widthTextBox.TabIndex = 2;
            // 
            // heightTextBox
            // 
            this.heightTextBox.Location = new System.Drawing.Point(676, 56);
            this.heightTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.heightTextBox.Name = "heightTextBox";
            this.heightTextBox.Size = new System.Drawing.Size(76, 20);
            this.heightTextBox.TabIndex = 3;
            // 
            // patternComboBox
            // 
            this.patternComboBox.FormattingEnabled = true;
            this.patternComboBox.Items.AddRange(new object[] {
            "pojedyncze",
            "test",
            "losowe"});
            this.patternComboBox.Location = new System.Drawing.Point(669, 109);
            this.patternComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.patternComboBox.Name = "patternComboBox";
            this.patternComboBox.Size = new System.Drawing.Size(92, 21);
            this.patternComboBox.TabIndex = 4;
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(669, 474);
            this.resetButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(82, 32);
            this.resetButton.TabIndex = 5;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // chooseColorComboBox
            // 
            this.chooseColorComboBox.FormattingEnabled = true;
            this.chooseColorComboBox.Items.AddRange(new object[] {
            "Czerwony",
            "Czarny",
            "Niebieski",
            "Zielony",
            "Beżowy",
            "Fioletowy",
            "Pomidorowy"});
            this.chooseColorComboBox.Location = new System.Drawing.Point(670, 285);
            this.chooseColorComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chooseColorComboBox.Name = "chooseColorComboBox";
            this.chooseColorComboBox.Size = new System.Drawing.Size(92, 21);
            this.chooseColorComboBox.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(700, 85);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Wzór";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(667, 144);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Warunki brzegowe";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(686, 259);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Kolor pędzla";
            // 
            // conditionComboBox
            // 
            this.conditionComboBox.FormattingEnabled = true;
            this.conditionComboBox.Items.AddRange(new object[] {
            "Periodyczne",
            "Absorbujące"});
            this.conditionComboBox.Location = new System.Drawing.Point(669, 168);
            this.conditionComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.conditionComboBox.Name = "conditionComboBox";
            this.conditionComboBox.Size = new System.Drawing.Size(92, 21);
            this.conditionComboBox.TabIndex = 11;
            // 
            // radiusBox
            // 
            this.radiusBox.Location = new System.Drawing.Point(669, 343);
            this.radiusBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radiusBox.Name = "radiusBox";
            this.radiusBox.Size = new System.Drawing.Size(92, 20);
            this.radiusBox.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(693, 318);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Promień";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(503, 493);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "label5";
            // 
            // neighbourhoodBox
            // 
            this.neighbourhoodBox.FormattingEnabled = true;
            this.neighbourhoodBox.Items.AddRange(new object[] {
            "Moore",
            "Neumann",
            "Pentagonalne",
            "Heksagonalne"});
            this.neighbourhoodBox.Location = new System.Drawing.Point(669, 224);
            this.neighbourhoodBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.neighbourhoodBox.Name = "neighbourhoodBox";
            this.neighbourhoodBox.Size = new System.Drawing.Size(92, 21);
            this.neighbourhoodBox.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(686, 201);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Sąsiedztwo";
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(670, 424);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(81, 31);
            this.stopButton.TabIndex = 17;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 515);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.neighbourhoodBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.radiusBox);
            this.Controls.Add(this.conditionComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chooseColorComboBox);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.patternComboBox);
            this.Controls.Add(this.heightTextBox);
            this.Controls.Add(this.widthTextBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox widthTextBox;
        private System.Windows.Forms.TextBox heightTextBox;
        private System.Windows.Forms.ComboBox patternComboBox;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.ComboBox chooseColorComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox conditionComboBox;
        private System.Windows.Forms.TextBox radiusBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox neighbourhoodBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button stopButton;
    }
}

