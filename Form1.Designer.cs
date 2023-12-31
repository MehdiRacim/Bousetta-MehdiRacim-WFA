﻿namespace snake
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            StartButton = new Button();
            SnapButton = new Button();
            picCanvas = new PictureBox();
            txtScore = new Label();
            txtHighScore = new Label();
            gameTimer = new System.Windows.Forms.Timer(components);
            difficultyComboBox = new ComboBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)picCanvas).BeginInit();
            SuspendLayout();
            // 
            // StartButton
            // 
            StartButton.BackColor = Color.SkyBlue;
            StartButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            StartButton.Location = new Point(478, 10);
            StartButton.Margin = new Padding(2);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(101, 45);
            StartButton.TabIndex = 0;
            StartButton.Text = "START";
            StartButton.UseVisualStyleBackColor = false;
            StartButton.Click += StartGame;
            // 
            // SnapButton
            // 
            SnapButton.BackColor = Color.PaleGreen;
            SnapButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            SnapButton.Location = new Point(478, 59);
            SnapButton.Margin = new Padding(2);
            SnapButton.Name = "SnapButton";
            SnapButton.Size = new Size(101, 45);
            SnapButton.TabIndex = 1;
            SnapButton.Text = "SNAP";
            SnapButton.UseVisualStyleBackColor = false;
            SnapButton.Click += TakeSnapshot;
            // 
            // picCanvas
            // 
            picCanvas.BackColor = Color.Beige;
            picCanvas.Location = new Point(11, 10);
            picCanvas.Margin = new Padding(2);
            picCanvas.Name = "picCanvas";
            picCanvas.Size = new Size(464, 544);
            picCanvas.TabIndex = 2;
            picCanvas.TabStop = false;
            picCanvas.Paint += UpdatePictureBoxGraphics;
            // 
            // txtScore
            // 
            txtScore.AutoSize = true;
            txtScore.Font = new Font("Algerian", 12F, FontStyle.Bold, GraphicsUnit.Point);
            txtScore.Location = new Point(478, 138);
            txtScore.Margin = new Padding(2, 0, 2, 0);
            txtScore.Name = "txtScore";
            txtScore.Size = new Size(100, 22);
            txtScore.TabIndex = 3;
            txtScore.Text = "Score: 0";
            // 
            // txtHighScore
            // 
            txtHighScore.AutoSize = true;
            txtHighScore.Font = new Font("Algerian", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            txtHighScore.Location = new Point(478, 182);
            txtHighScore.Margin = new Padding(2, 0, 2, 0);
            txtHighScore.Name = "txtHighScore";
            txtHighScore.Size = new Size(117, 20);
            txtHighScore.TabIndex = 4;
            txtHighScore.Text = "High Score";
            // 
            // gameTimer
            // 
            gameTimer.Interval = 40;
            gameTimer.Tick += GameTimerEvent;
            // 
            // difficultyComboBox
            // 
            difficultyComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            difficultyComboBox.FormattingEnabled = true;
            difficultyComboBox.Items.AddRange(new object[] { "facile", "normal", "difficile" });
            difficultyComboBox.Location = new Point(480, 275);
            difficultyComboBox.Name = "difficultyComboBox";
            difficultyComboBox.Size = new Size(151, 28);
            difficultyComboBox.TabIndex = 5;
            difficultyComboBox.Tag = "difficulté";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Algerian", 10.2F, FontStyle.Italic, GraphicsUnit.Point);
            label1.ForeColor = Color.Green;
            label1.Location = new Point(480, 240);
            label1.Name = "label1";
            label1.Size = new Size(230, 19);
            label1.TabIndex = 6;
            label1.Text = "choisir votre difficulté :";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveCaption;
            ClientSize = new Size(731, 566);
            Controls.Add(label1);
            Controls.Add(difficultyComboBox);
            Controls.Add(txtHighScore);
            Controls.Add(txtScore);
            Controls.Add(picCanvas);
            Controls.Add(SnapButton);
            Controls.Add(StartButton);
            Margin = new Padding(2);
            Name = "Form1";
            Text = "Form1";
            KeyDown += KeyIsDown;
            KeyUp += KeyIsUp;
            ((System.ComponentModel.ISupportInitialize)picCanvas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button StartButton;
        private Button SnapButton;
        private PictureBox picCanvas;
        private Label txtScore;
        private Label txtHighScore;
        private System.Windows.Forms.Timer gameTimer;
        private ComboBox difficultyComboBox;
        private Label label1;
    }
}