namespace SignalManipulator.UI.Controls
{
    partial class AudioPlayerControl
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
            this.playBtn = new System.Windows.Forms.Button();
            this.timeInfoLbl = new System.Windows.Forms.Label();
            this.timeLbl = new System.Windows.Forms.Label();
            this.speedInfoLbl = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.speedLbl = new System.Windows.Forms.Label();
            this.pitchCheckBox = new System.Windows.Forms.CheckBox();
            this.pauseBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // playingAudioInfoLbl
            // 
            this.playingAudioInfoLbl.AutoSize = true;
            this.playingAudioInfoLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playingAudioInfoLbl.Location = new System.Drawing.Point(12, 11);
            this.playingAudioInfoLbl.Name = "playingAudioInfoLbl";
            this.playingAudioInfoLbl.Size = new System.Drawing.Size(113, 20);
            this.playingAudioInfoLbl.TabIndex = 0;
            this.playingAudioInfoLbl.Text = "Audio playing:";
            // 
            // playingAudioLbl
            // 
            this.playingAudioLbl.AutoSize = true;
            this.playingAudioLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playingAudioLbl.Location = new System.Drawing.Point(135, 11);
            this.playingAudioLbl.Name = "playingAudioLbl";
            this.playingAudioLbl.Size = new System.Drawing.Size(0, 22);
            this.playingAudioLbl.TabIndex = 1;
            // 
            // stopBtn
            // 
            this.stopBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.stopBtn.Location = new System.Drawing.Point(99, 129);
            this.stopBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(80, 25);
            this.stopBtn.TabIndex = 2;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // playBtn
            // 
            this.playBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.playBtn.Location = new System.Drawing.Point(13, 129);
            this.playBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.playBtn.Name = "playBtn";
            this.playBtn.Size = new System.Drawing.Size(80, 25);
            this.playBtn.TabIndex = 3;
            this.playBtn.Text = "Play";
            this.playBtn.UseVisualStyleBackColor = true;
            this.playBtn.Click += new System.EventHandler(this.playBtn_Click);
            // 
            // timeInfoLbl
            // 
            this.timeInfoLbl.AutoSize = true;
            this.timeInfoLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeInfoLbl.Location = new System.Drawing.Point(12, 33);
            this.timeInfoLbl.Name = "timeInfoLbl";
            this.timeInfoLbl.Size = new System.Drawing.Size(118, 20);
            this.timeInfoLbl.TabIndex = 4;
            this.timeInfoLbl.Text = "Playback time:";
            // 
            // timeLbl
            // 
            this.timeLbl.AutoSize = true;
            this.timeLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLbl.Location = new System.Drawing.Point(133, 34);
            this.timeLbl.Name = "timeLbl";
            this.timeLbl.Size = new System.Drawing.Size(81, 20);
            this.timeLbl.TabIndex = 5;
            this.timeLbl.Text = "00:00.000";
            // 
            // speedInfoLbl
            // 
            this.speedInfoLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.speedInfoLbl.AutoSize = true;
            this.speedInfoLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speedInfoLbl.Location = new System.Drawing.Point(12, 95);
            this.speedInfoLbl.Name = "speedInfoLbl";
            this.speedInfoLbl.Size = new System.Drawing.Size(131, 20);
            this.speedInfoLbl.TabIndex = 6;
            this.speedInfoLbl.Text = "Playback speed:";
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.AutoSize = false;
            this.trackBar1.Location = new System.Drawing.Point(149, 95);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar1.Maximum = 175;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(189, 28);
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
            this.speedLbl.Location = new System.Drawing.Point(336, 98);
            this.speedLbl.Name = "speedLbl";
            this.speedLbl.Size = new System.Drawing.Size(20, 16);
            this.speedLbl.TabIndex = 8;
            this.speedLbl.Text = "1x";
            // 
            // pitchCheckBox
            // 
            this.pitchCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pitchCheckBox.AutoSize = true;
            this.pitchCheckBox.Location = new System.Drawing.Point(203, 133);
            this.pitchCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.pitchCheckBox.Name = "pitchCheckBox";
            this.pitchCheckBox.Size = new System.Drawing.Size(115, 20);
            this.pitchCheckBox.TabIndex = 9;
            this.pitchCheckBox.Text = "Preserve pitch";
            this.pitchCheckBox.UseVisualStyleBackColor = true;
            this.pitchCheckBox.CheckedChanged += new System.EventHandler(this.pitchCheckBox_CheckedChanged);
            // 
            // pauseBtn
            // 
            this.pauseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pauseBtn.Location = new System.Drawing.Point(13, 129);
            this.pauseBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pauseBtn.Name = "pauseBtn";
            this.pauseBtn.Size = new System.Drawing.Size(80, 25);
            this.pauseBtn.TabIndex = 10;
            this.pauseBtn.Text = "Pause";
            this.pauseBtn.UseVisualStyleBackColor = true;
            this.pauseBtn.Visible = false;
            this.pauseBtn.Click += new System.EventHandler(this.pauseBtn_Click);
            // 
            // AudioPlayerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pitchCheckBox);
            this.Controls.Add(this.speedLbl);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.speedInfoLbl);
            this.Controls.Add(this.timeLbl);
            this.Controls.Add(this.timeInfoLbl);
            this.Controls.Add(this.playBtn);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.playingAudioLbl);
            this.Controls.Add(this.playingAudioInfoLbl);
            this.Controls.Add(this.pauseBtn);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(349, 135);
            this.Name = "AudioPlayerControl";
            this.Size = new System.Drawing.Size(379, 161);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label playingAudioInfoLbl;
        private System.Windows.Forms.Label playingAudioLbl;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.Button playBtn;
        private System.Windows.Forms.Label timeInfoLbl;
        private System.Windows.Forms.Label timeLbl;
        private System.Windows.Forms.Label speedInfoLbl;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label speedLbl;
        private System.Windows.Forms.CheckBox pitchCheckBox;
        private System.Windows.Forms.Button pauseBtn;
    }
}
