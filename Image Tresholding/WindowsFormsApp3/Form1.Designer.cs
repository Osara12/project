namespace WindowsFormsApp3
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ExitButton = new System.Windows.Forms.Label();
            this.ProgramNameLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.InputLabel = new System.Windows.Forms.Label();
            this.OutputLabel = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.LoadButton = new System.Windows.Forms.LinkLabel();
            this.RecordButton = new System.Windows.Forms.LinkLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.TrackBarName = new System.Windows.Forms.Label();
            this.trackBarValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // ExitButton
            // 
            this.ExitButton.AutoSize = true;
            this.ExitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.ExitButton.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.ExitButton.Location = new System.Drawing.Point(1308, 9);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(16, 20);
            this.ExitButton.TabIndex = 2;
            this.ExitButton.Text = "x";
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // ProgramNameLabel
            // 
            this.ProgramNameLabel.AutoSize = true;
            this.ProgramNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.ProgramNameLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.ProgramNameLabel.Location = new System.Drawing.Point(12, 9);
            this.ProgramNameLabel.Name = "ProgramNameLabel";
            this.ProgramNameLabel.Size = new System.Drawing.Size(137, 18);
            this.ProgramNameLabel.TabIndex = 3;
            this.ProgramNameLabel.Text = "Image Thresholding";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(15, 62);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(640, 480);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(678, 62);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(640, 480);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // InputLabel
            // 
            this.InputLabel.AutoSize = true;
            this.InputLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.InputLabel.Location = new System.Drawing.Point(12, 39);
            this.InputLabel.Name = "InputLabel";
            this.InputLabel.Size = new System.Drawing.Size(31, 13);
            this.InputLabel.TabIndex = 1001;
            this.InputLabel.Text = "Input";
            // 
            // OutputLabel
            // 
            this.OutputLabel.AutoSize = true;
            this.OutputLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.OutputLabel.Location = new System.Drawing.Point(675, 39);
            this.OutputLabel.Name = "OutputLabel";
            this.OutputLabel.Size = new System.Drawing.Size(39, 13);
            this.OutputLabel.TabIndex = 1002;
            this.OutputLabel.Text = "Output";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // LoadButton
            // 
            this.LoadButton.ActiveLinkColor = System.Drawing.Color.Black;
            this.LoadButton.AutoSize = true;
            this.LoadButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoadButton.LinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.LoadButton.Location = new System.Drawing.Point(576, 39);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(31, 13);
            this.LoadButton.TabIndex = 1003;
            this.LoadButton.TabStop = true;
            this.LoadButton.Text = "Load";
            this.LoadButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LoadButton_Clicked);
            // 
            // RecordButton
            // 
            this.RecordButton.ActiveLinkColor = System.Drawing.Color.Black;
            this.RecordButton.AutoSize = true;
            this.RecordButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RecordButton.LinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.RecordButton.Location = new System.Drawing.Point(613, 39);
            this.RecordButton.Name = "RecordButton";
            this.RecordButton.Size = new System.Drawing.Size(42, 13);
            this.RecordButton.TabIndex = 1004;
            this.RecordButton.TabStop = true;
            this.RecordButton.Text = "Record";
            this.RecordButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.RecordButton_Clicked);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 10;
            this.trackBar1.Location = new System.Drawing.Point(678, 581);
            this.trackBar1.Maximum = 255;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(640, 45);
            this.trackBar1.SmallChange = 10;
            this.trackBar1.TabIndex = 20;
            this.trackBar1.TickFrequency = 20;
            this.trackBar1.Value = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // TrackBarName
            // 
            this.TrackBarName.AutoSize = true;
            this.TrackBarName.BackColor = System.Drawing.Color.Transparent;
            this.TrackBarName.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.TrackBarName.Location = new System.Drawing.Point(679, 555);
            this.TrackBarName.Name = "TrackBarName";
            this.TrackBarName.Size = new System.Drawing.Size(40, 13);
            this.TrackBarName.TabIndex = 1005;
            this.TrackBarName.Text = "Value :";
            // 
            // trackBarValue
            // 
            this.trackBarValue.AutoSize = true;
            this.trackBarValue.Location = new System.Drawing.Point(741, 555);
            this.trackBarValue.Name = "trackBarValue";
            this.trackBarValue.Size = new System.Drawing.Size(13, 13);
            this.trackBarValue.TabIndex = 1006;
            this.trackBarValue.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1336, 621);
            this.Controls.Add(this.trackBarValue);
            this.Controls.Add(this.TrackBarName);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.RecordButton);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.OutputLabel);
            this.Controls.Add(this.InputLabel);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.ProgramNameLabel);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label ExitButton;
        private System.Windows.Forms.Label ProgramNameLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label InputLabel;
        private System.Windows.Forms.Label OutputLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.LinkLabel LoadButton;
        private System.Windows.Forms.LinkLabel RecordButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label TrackBarName;
        private System.Windows.Forms.Label trackBarValue;
    }
}

