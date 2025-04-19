namespace SignalManipulator.UI.Modules
{
    partial class AudioPlayer
    {
        /// <summary> 
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione componenti

        /// <summary> 
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.playingAudioInfoLbl = new System.Windows.Forms.Label();
            this.playingAudioLbl = new System.Windows.Forms.Label();
            this.stopBtn = new System.Windows.Forms.Button();
            this.playPauseBtn = new System.Windows.Forms.Button();
            this.timeInfoLbl = new System.Windows.Forms.Label();
            this.timeLbl = new System.Windows.Forms.Label();
            this.speedInfoLbl = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.speedLbl = new System.Windows.Forms.Label();
            this.pitchCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // playingAudioInfoLbl
            // 
            this.playingAudioInfoLbl.AutoSize = true;
            this.playingAudioInfoLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playingAudioInfoLbl.Location = new System.Drawing.Point(9, 9);
            this.playingAudioInfoLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.playingAudioInfoLbl.Name = "playingAudioInfoLbl";
            this.playingAudioInfoLbl.Size = new System.Drawing.Size(97, 17);
            this.playingAudioInfoLbl.TabIndex = 0;
            this.playingAudioInfoLbl.Text = "Audio playing:";
            // 
            // playingAudioLbl
            // 
            this.playingAudioLbl.AutoSize = true;
            this.playingAudioLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playingAudioLbl.Location = new System.Drawing.Point(101, 9);
            this.playingAudioLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.playingAudioLbl.Name = "playingAudioLbl";
            this.playingAudioLbl.Size = new System.Drawing.Size(0, 18);
            this.playingAudioLbl.TabIndex = 1;
            // 
            // stopBtn
            // 
            this.stopBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.stopBtn.Location = new System.Drawing.Point(74, 105);
            this.stopBtn.Margin = new System.Windows.Forms.Padding(2);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(60, 20);
            this.stopBtn.TabIndex = 2;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // playPauseBtn
            // 
            this.playPauseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.playPauseBtn.Location = new System.Drawing.Point(10, 105);
            this.playPauseBtn.Margin = new System.Windows.Forms.Padding(2);
            this.playPauseBtn.Name = "playPauseBtn";
            this.playPauseBtn.Size = new System.Drawing.Size(60, 20);
            this.playPauseBtn.TabIndex = 3;
            this.playPauseBtn.Text = "Play";
            this.playPauseBtn.UseVisualStyleBackColor = true;
            this.playPauseBtn.Click += new System.EventHandler(this.playPauseBtn_Click);
            // 
            // timeInfoLbl
            // 
            this.timeInfoLbl.AutoSize = true;
            this.timeInfoLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeInfoLbl.Location = new System.Drawing.Point(9, 27);
            this.timeInfoLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.timeInfoLbl.Name = "timeInfoLbl";
            this.timeInfoLbl.Size = new System.Drawing.Size(99, 17);
            this.timeInfoLbl.TabIndex = 4;
            this.timeInfoLbl.Text = "Playback time:";
            // 
            // timeLbl
            // 
            this.timeLbl.AutoSize = true;
            this.timeLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLbl.Location = new System.Drawing.Point(100, 28);
            this.timeLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.timeLbl.Name = "timeLbl";
            this.timeLbl.Size = new System.Drawing.Size(72, 17);
            this.timeLbl.TabIndex = 5;
            this.timeLbl.Text = "00:00.000";
            // 
            // speedInfoLbl
            // 
            this.speedInfoLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.speedInfoLbl.AutoSize = true;
            this.speedInfoLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speedInfoLbl.Location = new System.Drawing.Point(9, 77);
            this.speedInfoLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.speedInfoLbl.Name = "speedInfoLbl";
            this.speedInfoLbl.Size = new System.Drawing.Size(112, 17);
            this.speedInfoLbl.TabIndex = 6;
            this.speedInfoLbl.Text = "Playback speed:";
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.AutoSize = false;
            this.trackBar1.Location = new System.Drawing.Point(112, 77);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(2);
            this.trackBar1.Maximum = 175;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(142, 23);
            this.trackBar1.SmallChange = 5;
            this.trackBar1.TabIndex = 7;
            this.trackBar1.TickFrequency = 0;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Value = 75;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // speedLbl
            // 
            this.speedLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.speedLbl.AutoSize = true;
            this.speedLbl.Location = new System.Drawing.Point(252, 80);
            this.speedLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.speedLbl.Name = "speedLbl";
            this.speedLbl.Size = new System.Drawing.Size(18, 13);
            this.speedLbl.TabIndex = 8;
            this.speedLbl.Text = "1x";
            // 
            // pitchCheckBox
            // 
            this.pitchCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pitchCheckBox.AutoSize = true;
            this.pitchCheckBox.Location = new System.Drawing.Point(152, 107);
            this.pitchCheckBox.Name = "pitchCheckBox";
            this.pitchCheckBox.Size = new System.Drawing.Size(94, 17);
            this.pitchCheckBox.TabIndex = 9;
            this.pitchCheckBox.Text = "Preserve pitch";
            this.pitchCheckBox.UseVisualStyleBackColor = true;
            this.pitchCheckBox.CheckedChanged += new System.EventHandler(this.pitchCheckBox_CheckedChanged);
            // 
            // AudioPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pitchCheckBox);
            this.Controls.Add(this.speedLbl);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.speedInfoLbl);
            this.Controls.Add(this.timeLbl);
            this.Controls.Add(this.timeInfoLbl);
            this.Controls.Add(this.playPauseBtn);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.playingAudioLbl);
            this.Controls.Add(this.playingAudioInfoLbl);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(262, 110);
            this.Name = "AudioPlayer";
            this.Size = new System.Drawing.Size(284, 131);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label playingAudioInfoLbl;
        private System.Windows.Forms.Label playingAudioLbl;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.Button playPauseBtn;
        private System.Windows.Forms.Label timeInfoLbl;
        private System.Windows.Forms.Label timeLbl;
        private System.Windows.Forms.Label speedInfoLbl;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label speedLbl;
        private System.Windows.Forms.CheckBox pitchCheckBox;
    }
}
