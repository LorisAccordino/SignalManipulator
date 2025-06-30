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
            components = new System.ComponentModel.Container();
            playingAudioInfoLbl = new System.Windows.Forms.Label();
            playingAudioLbl = new System.Windows.Forms.Label();
            stopBtn = new System.Windows.Forms.Button();
            playBtn = new System.Windows.Forms.Button();
            timeInfoLbl = new System.Windows.Forms.Label();
            timeLbl = new System.Windows.Forms.Label();
            pitchCheckBox = new System.Windows.Forms.CheckBox();
            pauseBtn = new System.Windows.Forms.Button();
            waveFmtLbl = new Components.DescriptorLabel(components);
            timeSlider = new Components.Precision.PrecisionSlider();
            playbackSpeedSlider = new Components.Precision.PrecisionSlider();
            volumeSlider = new Components.Precision.PrecisionSlider();
            SuspendLayout();
            // 
            // playingAudioInfoLbl
            // 
            playingAudioInfoLbl.AutoSize = true;
            playingAudioInfoLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            playingAudioInfoLbl.Location = new System.Drawing.Point(10, 10);
            playingAudioInfoLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            playingAudioInfoLbl.Name = "playingAudioInfoLbl";
            playingAudioInfoLbl.Size = new System.Drawing.Size(92, 16);
            playingAudioInfoLbl.TabIndex = 0;
            playingAudioInfoLbl.Text = "Audio playing:";
            // 
            // playingAudioLbl
            // 
            playingAudioLbl.AutoSize = true;
            playingAudioLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            playingAudioLbl.Location = new System.Drawing.Point(118, 10);
            playingAudioLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            playingAudioLbl.Name = "playingAudioLbl";
            playingAudioLbl.Size = new System.Drawing.Size(12, 18);
            playingAudioLbl.TabIndex = 1;
            playingAudioLbl.Text = " ";
            // 
            // stopBtn
            // 
            stopBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            stopBtn.Location = new System.Drawing.Point(86, 157);
            stopBtn.Margin = new System.Windows.Forms.Padding(2);
            stopBtn.Name = "stopBtn";
            stopBtn.Size = new System.Drawing.Size(70, 23);
            stopBtn.TabIndex = 2;
            stopBtn.Text = "Stop";
            stopBtn.UseVisualStyleBackColor = true;
            stopBtn.Click += stopBtn_Click;
            // 
            // playBtn
            // 
            playBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            playBtn.Location = new System.Drawing.Point(12, 157);
            playBtn.Margin = new System.Windows.Forms.Padding(2);
            playBtn.Name = "playBtn";
            playBtn.Size = new System.Drawing.Size(70, 23);
            playBtn.TabIndex = 3;
            playBtn.Text = "Play";
            playBtn.UseVisualStyleBackColor = true;
            playBtn.Click += playBtn_Click;
            // 
            // timeInfoLbl
            // 
            timeInfoLbl.AutoSize = true;
            timeInfoLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            timeInfoLbl.Location = new System.Drawing.Point(10, 32);
            timeInfoLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            timeInfoLbl.Name = "timeInfoLbl";
            timeInfoLbl.Size = new System.Drawing.Size(95, 16);
            timeInfoLbl.TabIndex = 4;
            timeInfoLbl.Text = "Playback time:";
            // 
            // timeLbl
            // 
            timeLbl.AutoSize = true;
            timeLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            timeLbl.Location = new System.Drawing.Point(117, 33);
            timeLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            timeLbl.Name = "timeLbl";
            timeLbl.Size = new System.Drawing.Size(62, 16);
            timeLbl.TabIndex = 5;
            timeLbl.Text = "00:00.000";
            // 
            // pitchCheckBox
            // 
            pitchCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            pitchCheckBox.AutoSize = true;
            pitchCheckBox.Checked = true;
            pitchCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            pitchCheckBox.Location = new System.Drawing.Point(177, 161);
            pitchCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pitchCheckBox.Name = "pitchCheckBox";
            pitchCheckBox.Size = new System.Drawing.Size(100, 19);
            pitchCheckBox.TabIndex = 9;
            pitchCheckBox.Text = "Preserve pitch";
            pitchCheckBox.UseVisualStyleBackColor = true;
            pitchCheckBox.CheckedChanged += pitchCheckBox_CheckedChanged;
            // 
            // pauseBtn
            // 
            pauseBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            pauseBtn.Location = new System.Drawing.Point(12, 157);
            pauseBtn.Margin = new System.Windows.Forms.Padding(2);
            pauseBtn.Name = "pauseBtn";
            pauseBtn.Size = new System.Drawing.Size(70, 23);
            pauseBtn.TabIndex = 10;
            pauseBtn.Text = "Pause";
            pauseBtn.UseVisualStyleBackColor = true;
            pauseBtn.Visible = false;
            pauseBtn.Click += pauseBtn_Click;
            // 
            // waveFmtLbl
            // 
            waveFmtLbl.AutoSize = true;
            waveFmtLbl.Description = "Wave format";
            waveFmtLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            waveFmtLbl.Location = new System.Drawing.Point(12, 53);
            waveFmtLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            waveFmtLbl.Name = "waveFmtLbl";
            waveFmtLbl.Separator = ":  ";
            waveFmtLbl.Size = new System.Drawing.Size(92, 16);
            waveFmtLbl.TabIndex = 18;
            waveFmtLbl.Value = "";
            // 
            // timeSlider
            // 
            timeSlider.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            timeSlider.Location = new System.Drawing.Point(188, 30);
            timeSlider.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            timeSlider.Name = "timeSlider";
            timeSlider.ShowDescription = false;
            timeSlider.ShowValue = false;
            timeSlider.Size = new System.Drawing.Size(243, 30);
            timeSlider.TabIndex = 17;
            timeSlider.TickFrequency = 1;
            timeSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            timeSlider.UpdateMode = Components.Precision.ValueUpdateMode.UserOnly;
            // 
            // playbackSpeedSlider
            // 
            playbackSpeedSlider.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            playbackSpeedSlider.Curvature = 2.5D;
            playbackSpeedSlider.Description = "Playback speed:";
            playbackSpeedSlider.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            playbackSpeedSlider.Location = new System.Drawing.Point(12, 117);
            playbackSpeedSlider.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            playbackSpeedSlider.Maximum = 3D;
            playbackSpeedSlider.Minimum = 0.01D;
            playbackSpeedSlider.Name = "playbackSpeedSlider";
            playbackSpeedSlider.Size = new System.Drawing.Size(419, 30);
            playbackSpeedSlider.Suffix = "x";
            playbackSpeedSlider.TabIndex = 15;
            playbackSpeedSlider.TickFrequency = 30;
            playbackSpeedSlider.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            playbackSpeedSlider.Value = 1D;
            // 
            // volumeSlider
            // 
            volumeSlider.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            volumeSlider.Curvature = 2D;
            volumeSlider.Description = "Volume:";
            volumeSlider.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            volumeSlider.Location = new System.Drawing.Point(12, 88);
            volumeSlider.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            volumeSlider.Maximum = 3D;
            volumeSlider.Name = "volumeSlider";
            volumeSlider.Precision = 0.001D;
            volumeSlider.PrecisionScale = Components.Precision.PrecisionScale.Logarithmic;
            volumeSlider.Size = new System.Drawing.Size(416, 30);
            volumeSlider.TabIndex = 19;
            volumeSlider.TickFrequency = 150;
            volumeSlider.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            volumeSlider.Value = 1D;
            // 
            // AudioPlayerControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(volumeSlider);
            Controls.Add(waveFmtLbl);
            Controls.Add(timeSlider);
            Controls.Add(playbackSpeedSlider);
            Controls.Add(pitchCheckBox);
            Controls.Add(timeLbl);
            Controls.Add(timeInfoLbl);
            Controls.Add(playBtn);
            Controls.Add(stopBtn);
            Controls.Add(playingAudioLbl);
            Controls.Add(playingAudioInfoLbl);
            Controls.Add(pauseBtn);
            Margin = new System.Windows.Forms.Padding(2);
            MinimumSize = new System.Drawing.Size(306, 170);
            Name = "AudioPlayerControl";
            Size = new System.Drawing.Size(431, 187);
            ResumeLayout(false);
            PerformLayout();
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
        private Components.Precision.PrecisionSlider volumeSlider;
    }
}
