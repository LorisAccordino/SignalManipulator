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
            timeSlider = new Components.TimeSlider();
            pitchCheckBox = new System.Windows.Forms.CheckBox();
            playbackSpeedSlider = new Components.Precision.PrecisionSlider();
            volumeSlider = new Components.Precision.PrecisionSlider();
            infoLbl = new System.Windows.Forms.LinkLabel();
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
            stopBtn.Click += OnStop;
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
            playBtn.Click += OnPlay;
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
            pauseBtn.Click += OnPause;
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
            settingsPanel.Controls.Add(timeSlider);
            settingsPanel.Controls.Add(pitchCheckBox);
            settingsPanel.Controls.Add(playbackSpeedSlider);
            settingsPanel.Controls.Add(volumeSlider);
            settingsPanel.Location = new System.Drawing.Point(0, 69);
            settingsPanel.Name = "settingsPanel";
            settingsPanel.Size = new System.Drawing.Size(431, 88);
            settingsPanel.TabIndex = 23;
            // 
            // timeSlider
            // 
            timeSlider.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            timeSlider.AutoUpdate = true;
            timeSlider.CurrentTime = TimeSpan.Parse("00:00:00");
            timeSlider.Description = "Time:";
            timeSlider.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            timeSlider.Location = new System.Drawing.Point(3, 4);
            timeSlider.Name = "timeSlider";
            timeSlider.Size = new System.Drawing.Size(428, 30);
            timeSlider.TabIndex = 25;
            timeSlider.TimeFormat = "hh\\:mm\\:ss\\.fff";
            timeSlider.TotalTime = TimeSpan.Parse("00:01:00");
            // 
            // pitchCheckBox
            // 
            pitchCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            pitchCheckBox.AutoSize = true;
            pitchCheckBox.Location = new System.Drawing.Point(338, 59);
            pitchCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pitchCheckBox.Name = "pitchCheckBox";
            pitchCheckBox.Size = new System.Drawing.Size(82, 19);
            pitchCheckBox.TabIndex = 22;
            pitchCheckBox.Text = "Keep pitch";
            pitchCheckBox.UseVisualStyleBackColor = true;
            // 
            // playbackSpeedSlider
            // 
            playbackSpeedSlider.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            playbackSpeedSlider.Curvature = 2.5D;
            playbackSpeedSlider.Description = "Speed:";
            playbackSpeedSlider.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            playbackSpeedSlider.Location = new System.Drawing.Point(3, 54);
            playbackSpeedSlider.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            playbackSpeedSlider.Maximum = 2D;
            playbackSpeedSlider.Minimum = 0.01D;
            playbackSpeedSlider.Name = "playbackSpeedSlider";
            playbackSpeedSlider.Size = new System.Drawing.Size(338, 30);
            playbackSpeedSlider.Suffix = "x";
            playbackSpeedSlider.TabIndex = 23;
            playbackSpeedSlider.TickFrequency = 30;
            playbackSpeedSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            playbackSpeedSlider.Value = 1D;
            // 
            // volumeSlider
            // 
            volumeSlider.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            volumeSlider.Curvature = 2D;
            volumeSlider.Description = "Volume:";
            volumeSlider.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            volumeSlider.Location = new System.Drawing.Point(3, 29);
            volumeSlider.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            volumeSlider.Maximum = 2D;
            volumeSlider.Name = "volumeSlider";
            volumeSlider.Precision = 0.001D;
            volumeSlider.PrecisionScale = Components.Precision.PrecisionScale.Logarithmic;
            volumeSlider.Size = new System.Drawing.Size(419, 30);
            volumeSlider.TabIndex = 25;
            volumeSlider.TickFrequency = 150;
            volumeSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            volumeSlider.Value = 1D;
            // 
            // infoLbl
            // 
            infoLbl.AutoSize = true;
            infoLbl.Location = new System.Drawing.Point(17, 47);
            infoLbl.Name = "infoLbl";
            infoLbl.Size = new System.Drawing.Size(91, 15);
            infoLbl.TabIndex = 24;
            infoLbl.TabStop = true;
            infoLbl.Text = "Show more info";
            infoLbl.LinkClicked += OnShowMoreInfo;
            // 
            // AudioPlayerControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(infoLbl);
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
        private Components.Precision.PrecisionSlider playbackSpeedSlider;
        private System.Windows.Forms.CheckBox pitchCheckBox;
        private System.Windows.Forms.Label timeInfoLbl;
        private Components.TimeSlider timeSlider;
        private System.Windows.Forms.LinkLabel infoLbl;
    }
}