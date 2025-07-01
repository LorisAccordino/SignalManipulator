using SignalManipulator.UI.Components.Labels;

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
            stopBtn = new System.Windows.Forms.Button();
            playBtn = new System.Windows.Forms.Button();
            pauseBtn = new System.Windows.Forms.Button();
            waveFmtLbl = new DescriptorLabel(components);
            playingAudioLbl = new DescriptorLabel(components);
            settingsPanel = new System.Windows.Forms.Panel();
            pitchCheckBox = new System.Windows.Forms.CheckBox();
            timeLbl = new TimeLabel(components);
            volumeSlider = new Components.Precision.PrecisionSlider();
            timeSlider = new Components.Precision.PrecisionSlider();
            playbackSpeedSlider = new Components.Precision.PrecisionSlider();
            timeInfoLbl = new System.Windows.Forms.Label();
            settingsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // stopBtn
            // 
            stopBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            stopBtn.Location = new System.Drawing.Point(86, 158);
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
            playBtn.Location = new System.Drawing.Point(12, 158);
            playBtn.Margin = new System.Windows.Forms.Padding(2);
            playBtn.Name = "playBtn";
            playBtn.Size = new System.Drawing.Size(70, 23);
            playBtn.TabIndex = 3;
            playBtn.Text = "Play";
            playBtn.UseVisualStyleBackColor = true;
            playBtn.Click += playBtn_Click;
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
            waveFmtLbl.Location = new System.Drawing.Point(10, 31);
            waveFmtLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            waveFmtLbl.Name = "waveFmtLbl";
            waveFmtLbl.Separator = ":  ";
            waveFmtLbl.Size = new System.Drawing.Size(92, 16);
            waveFmtLbl.TabIndex = 18;
            waveFmtLbl.Value = "";
            // 
            // playingAudioLbl
            // 
            playingAudioLbl.AutoSize = true;
            playingAudioLbl.Description = "Audio playing";
            playingAudioLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            playingAudioLbl.Location = new System.Drawing.Point(10, 10);
            playingAudioLbl.Name = "playingAudioLbl";
            playingAudioLbl.Size = new System.Drawing.Size(95, 16);
            playingAudioLbl.TabIndex = 22;
            playingAudioLbl.Value = "";
            // 
            // settingsPanel
            // 
            settingsPanel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            settingsPanel.Controls.Add(pitchCheckBox);
            settingsPanel.Controls.Add(timeLbl);
            settingsPanel.Controls.Add(playbackSpeedSlider);
            settingsPanel.Controls.Add(timeInfoLbl);
            settingsPanel.Controls.Add(volumeSlider);
            settingsPanel.Controls.Add(timeSlider);
            settingsPanel.Location = new System.Drawing.Point(0, 69);
            settingsPanel.Name = "settingsPanel";
            settingsPanel.Size = new System.Drawing.Size(431, 88);
            settingsPanel.TabIndex = 23;
            // 
            // pitchCheckBox
            // 
            pitchCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            pitchCheckBox.AutoSize = true;
            pitchCheckBox.Location = new System.Drawing.Point(349, 59);
            pitchCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pitchCheckBox.Name = "pitchCheckBox";
            pitchCheckBox.Size = new System.Drawing.Size(82, 19);
            pitchCheckBox.TabIndex = 22;
            pitchCheckBox.Text = "Keep pitch";
            pitchCheckBox.UseVisualStyleBackColor = true;
            // 
            // timeLbl
            // 
            timeLbl.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            timeLbl.AutoSize = true;
            timeLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            timeLbl.Location = new System.Drawing.Point(98, 8);
            timeLbl.Name = "timeLbl";
            timeLbl.Size = new System.Drawing.Size(62, 16);
            timeLbl.TabIndex = 26;
            timeLbl.Time = TimeSpan.Parse("00:00:00");
            // 
            // volumeSlider
            // 
            volumeSlider.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            volumeSlider.Curvature = 2D;
            volumeSlider.Description = "Volume:";
            volumeSlider.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            volumeSlider.Location = new System.Drawing.Point(3, 29);
            volumeSlider.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            volumeSlider.Maximum = 3D;
            volumeSlider.Name = "volumeSlider";
            volumeSlider.Precision = 0.001D;
            volumeSlider.PrecisionScale = Components.Precision.PrecisionScale.Logarithmic;
            volumeSlider.Size = new System.Drawing.Size(430, 30);
            volumeSlider.TabIndex = 25;
            volumeSlider.TickFrequency = 150;
            volumeSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            volumeSlider.Value = 1D;
            // 
            // timeSlider
            // 
            timeSlider.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            timeSlider.Location = new System.Drawing.Point(157, 4);
            timeSlider.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            timeSlider.Name = "timeSlider";
            timeSlider.ShowDescription = false;
            timeSlider.ShowValue = false;
            timeSlider.Size = new System.Drawing.Size(271, 30);
            timeSlider.TabIndex = 24;
            timeSlider.TickFrequency = 1;
            timeSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            timeSlider.UpdateMode = Components.Precision.ValueUpdateMode.UserOnly;
            // 
            // playbackSpeedSlider
            // 
            playbackSpeedSlider.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            playbackSpeedSlider.Curvature = 2.5D;
            playbackSpeedSlider.Description = "Speed:";
            playbackSpeedSlider.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            playbackSpeedSlider.Location = new System.Drawing.Point(3, 54);
            playbackSpeedSlider.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            playbackSpeedSlider.Maximum = 3D;
            playbackSpeedSlider.Minimum = 0.01D;
            playbackSpeedSlider.Name = "playbackSpeedSlider";
            playbackSpeedSlider.Size = new System.Drawing.Size(349, 30);
            playbackSpeedSlider.Suffix = "x";
            playbackSpeedSlider.TabIndex = 23;
            playbackSpeedSlider.TickFrequency = 30;
            playbackSpeedSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            playbackSpeedSlider.Value = 1D;
            // 
            // timeInfoLbl
            // 
            timeInfoLbl.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            timeInfoLbl.AutoSize = true;
            timeInfoLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            timeInfoLbl.Location = new System.Drawing.Point(3, 7);
            timeInfoLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            timeInfoLbl.Name = "timeInfoLbl";
            timeInfoLbl.Size = new System.Drawing.Size(95, 16);
            timeInfoLbl.TabIndex = 21;
            timeInfoLbl.Text = "Playback time:";
            // 
            // AudioPlayerControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(settingsPanel);
            Controls.Add(playingAudioLbl);
            Controls.Add(waveFmtLbl);
            Controls.Add(playBtn);
            Controls.Add(stopBtn);
            Controls.Add(pauseBtn);
            Margin = new System.Windows.Forms.Padding(2);
            MinimumSize = new System.Drawing.Size(306, 170);
            Name = "AudioPlayerControl";
            Size = new System.Drawing.Size(431, 187);
            settingsPanel.ResumeLayout(false);
            settingsPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.Button playBtn;
        private System.Windows.Forms.Button pauseBtn;
        private Components.Labels.DescriptorLabel waveFmtLbl;
        private Components.Labels.DescriptorLabel playingAudioLbl;
        private System.Windows.Forms.Panel settingsPanel;
        private TimeLabel timeLbl;
        private Components.Precision.PrecisionSlider volumeSlider;
        private Components.Precision.PrecisionSlider timeSlider;
        private Components.Precision.PrecisionSlider playbackSpeedSlider;
        private System.Windows.Forms.CheckBox pitchCheckBox;
        private System.Windows.Forms.Label timeInfoLbl;
    }
}
