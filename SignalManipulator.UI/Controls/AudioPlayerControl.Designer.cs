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
            this.components = new System.ComponentModel.Container();
            this.playingAudioInfoLbl = new System.Windows.Forms.Label();
            this.playingAudioLbl = new System.Windows.Forms.Label();
            this.stopBtn = new System.Windows.Forms.Button();
            this.playBtn = new System.Windows.Forms.Button();
            this.timeInfoLbl = new System.Windows.Forms.Label();
            this.timeLbl = new System.Windows.Forms.Label();
            this.pitchCheckBox = new System.Windows.Forms.CheckBox();
            this.pauseBtn = new System.Windows.Forms.Button();
            this.waveFmtLbl = new SignalManipulator.UI.Components.DescriptorLabel(this.components);
            this.timeSlider = new SignalManipulator.UI.Components.Precision.PrecisionSlider();
            this.playbackSpeedSlider = new SignalManipulator.UI.Components.Precision.PrecisionSlider();
            this.SuspendLayout();
            // 
            // playingAudioInfoLbl
            // 
            this.playingAudioInfoLbl.AutoSize = true;
            this.playingAudioInfoLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playingAudioInfoLbl.Location = new System.Drawing.Point(9, 9);
            this.playingAudioInfoLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.playingAudioInfoLbl.Name = "playingAudioInfoLbl";
            this.playingAudioInfoLbl.Size = new System.Drawing.Size(92, 16);
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
            this.playingAudioLbl.Size = new System.Drawing.Size(12, 18);
            this.playingAudioLbl.TabIndex = 1;
            this.playingAudioLbl.Text = " ";
            // 
            // stopBtn
            // 
            this.stopBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.stopBtn.Location = new System.Drawing.Point(74, 121);
            this.stopBtn.Margin = new System.Windows.Forms.Padding(2);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(60, 20);
            this.stopBtn.TabIndex = 2;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // playBtn
            // 
            this.playBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.playBtn.Location = new System.Drawing.Point(10, 121);
            this.playBtn.Margin = new System.Windows.Forms.Padding(2);
            this.playBtn.Name = "playBtn";
            this.playBtn.Size = new System.Drawing.Size(60, 20);
            this.playBtn.TabIndex = 3;
            this.playBtn.Text = "Play";
            this.playBtn.UseVisualStyleBackColor = true;
            this.playBtn.Click += new System.EventHandler(this.playBtn_Click);
            // 
            // timeInfoLbl
            // 
            this.timeInfoLbl.AutoSize = true;
            this.timeInfoLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeInfoLbl.Location = new System.Drawing.Point(9, 28);
            this.timeInfoLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.timeInfoLbl.Name = "timeInfoLbl";
            this.timeInfoLbl.Size = new System.Drawing.Size(95, 16);
            this.timeInfoLbl.TabIndex = 4;
            this.timeInfoLbl.Text = "Playback time:";
            // 
            // timeLbl
            // 
            this.timeLbl.AutoSize = true;
            this.timeLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLbl.Location = new System.Drawing.Point(100, 29);
            this.timeLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.timeLbl.Name = "timeLbl";
            this.timeLbl.Size = new System.Drawing.Size(62, 16);
            this.timeLbl.TabIndex = 5;
            this.timeLbl.Text = "00:00.000";
            // 
            // pitchCheckBox
            // 
            this.pitchCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pitchCheckBox.AutoSize = true;
            this.pitchCheckBox.Location = new System.Drawing.Point(152, 123);
            this.pitchCheckBox.Name = "pitchCheckBox";
            this.pitchCheckBox.Size = new System.Drawing.Size(94, 17);
            this.pitchCheckBox.TabIndex = 9;
            this.pitchCheckBox.Text = "Preserve pitch";
            this.pitchCheckBox.UseVisualStyleBackColor = true;
            this.pitchCheckBox.CheckedChanged += new System.EventHandler(this.pitchCheckBox_CheckedChanged);
            // 
            // pauseBtn
            // 
            this.pauseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pauseBtn.Location = new System.Drawing.Point(10, 121);
            this.pauseBtn.Margin = new System.Windows.Forms.Padding(2);
            this.pauseBtn.Name = "pauseBtn";
            this.pauseBtn.Size = new System.Drawing.Size(60, 20);
            this.pauseBtn.TabIndex = 10;
            this.pauseBtn.Text = "Pause";
            this.pauseBtn.UseVisualStyleBackColor = true;
            this.pauseBtn.Visible = false;
            this.pauseBtn.Click += new System.EventHandler(this.pauseBtn_Click);
            // 
            // waveFmtLbl
            // 
            this.waveFmtLbl.AutoSize = true;
            this.waveFmtLbl.Description = "Wave format";
            this.waveFmtLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.waveFmtLbl.Location = new System.Drawing.Point(10, 46);
            this.waveFmtLbl.Name = "waveFmtLbl";
            this.waveFmtLbl.Separator = ":  ";
            this.waveFmtLbl.Size = new System.Drawing.Size(92, 16);
            this.waveFmtLbl.TabIndex = 18;
            this.waveFmtLbl.Value = "";
            // 
            // timeSlider
            // 
            this.timeSlider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timeSlider.Location = new System.Drawing.Point(161, 26);
            this.timeSlider.Name = "timeSlider";
            this.timeSlider.ShowDescription = false;
            this.timeSlider.ShowValue = false;
            this.timeSlider.Size = new System.Drawing.Size(155, 30);
            this.timeSlider.TabIndex = 17;
            this.timeSlider.TickFrequency = 1;
            this.timeSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            this.timeSlider.UpdateMode = SignalManipulator.UI.Components.Precision.ValueUpdateMode.UserOnly;
            // 
            // playbackSpeedSlider
            // 
            this.playbackSpeedSlider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.playbackSpeedSlider.Curvature = 2.5D;
            this.playbackSpeedSlider.Description = "Playback speed:";
            this.playbackSpeedSlider.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.playbackSpeedSlider.Location = new System.Drawing.Point(10, 86);
            this.playbackSpeedSlider.Maximum = 3D;
            this.playbackSpeedSlider.Minimum = 0.01D;
            this.playbackSpeedSlider.Name = "playbackSpeedSlider";
            this.playbackSpeedSlider.Size = new System.Drawing.Size(306, 30);
            this.playbackSpeedSlider.Suffix = "x";
            this.playbackSpeedSlider.TabIndex = 15;
            this.playbackSpeedSlider.TickFrequency = 30;
            this.playbackSpeedSlider.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.playbackSpeedSlider.Value = 1D;
            // 
            // AudioPlayerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.waveFmtLbl);
            this.Controls.Add(this.timeSlider);
            this.Controls.Add(this.playbackSpeedSlider);
            this.Controls.Add(this.pitchCheckBox);
            this.Controls.Add(this.timeLbl);
            this.Controls.Add(this.timeInfoLbl);
            this.Controls.Add(this.playBtn);
            this.Controls.Add(this.stopBtn);
            this.Controls.Add(this.playingAudioLbl);
            this.Controls.Add(this.playingAudioInfoLbl);
            this.Controls.Add(this.pauseBtn);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(262, 110);
            this.Name = "AudioPlayerControl";
            this.Size = new System.Drawing.Size(316, 147);
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
        private System.Windows.Forms.CheckBox pitchCheckBox;
        private System.Windows.Forms.Button pauseBtn;
        private Components.Precision.PrecisionSlider playbackSpeedSlider;
        private Components.Precision.PrecisionSlider timeSlider;
        private Components.DescriptorLabel waveFmtLbl;
    }
}
