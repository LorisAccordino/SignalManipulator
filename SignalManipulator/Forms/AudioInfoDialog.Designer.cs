namespace SignalManipulator.Forms
{
    partial class AudioInfoDialog
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

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tabControl = new TabControl();
            techPage = new TabPage();
            framesLbl = new UI.Components.Labels.DescriptorLabel(components);
            samplesLbl = new UI.Components.Labels.DescriptorLabel(components);
            blockAlignLbl = new UI.Components.Labels.DescriptorLabel(components);
            bitRateLbl = new UI.Components.Labels.DescriptorLabel(components);
            encodingLbl = new UI.Components.Labels.DescriptorLabel(components);
            channelsLbl = new UI.Components.Labels.DescriptorLabel(components);
            bitDepthLbl = new UI.Components.Labels.DescriptorLabel(components);
            sampleRateLbl = new UI.Components.Labels.DescriptorLabel(components);
            metadataPage = new TabPage();
            coverImageBox = new PictureBox();
            durationLbl = new UI.Components.Labels.DescriptorLabel(components);
            trackNumberLbl = new UI.Components.Labels.DescriptorLabel(components);
            yearLbl = new UI.Components.Labels.DescriptorLabel(components);
            genreLbl = new UI.Components.Labels.DescriptorLabel(components);
            albumLbl = new UI.Components.Labels.DescriptorLabel(components);
            artistLbl = new UI.Components.Labels.DescriptorLabel(components);
            titleLbl = new UI.Components.Labels.DescriptorLabel(components);
            statsPage = new TabPage();
            tabControl.SuspendLayout();
            techPage.SuspendLayout();
            metadataPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)coverImageBox).BeginInit();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(techPage);
            tabControl.Controls.Add(metadataPage);
            tabControl.Controls.Add(statsPage);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(359, 191);
            tabControl.TabIndex = 0;
            // 
            // techPage
            // 
            techPage.Controls.Add(framesLbl);
            techPage.Controls.Add(samplesLbl);
            techPage.Controls.Add(blockAlignLbl);
            techPage.Controls.Add(bitRateLbl);
            techPage.Controls.Add(encodingLbl);
            techPage.Controls.Add(channelsLbl);
            techPage.Controls.Add(bitDepthLbl);
            techPage.Controls.Add(sampleRateLbl);
            techPage.Location = new Point(4, 24);
            techPage.Name = "techPage";
            techPage.Padding = new Padding(3);
            techPage.Size = new Size(351, 163);
            techPage.TabIndex = 0;
            techPage.Text = "Tech info";
            techPage.UseVisualStyleBackColor = true;
            // 
            // framesLbl
            // 
            framesLbl.AutoSize = true;
            framesLbl.Description = "Total frames";
            framesLbl.Font = new Font("Microsoft Sans Serif", 9.75F);
            framesLbl.Location = new Point(8, 135);
            framesLbl.Name = "framesLbl";
            framesLbl.Size = new Size(88, 16);
            framesLbl.TabIndex = 7;
            framesLbl.Value = "";
            // 
            // samplesLbl
            // 
            samplesLbl.AutoSize = true;
            samplesLbl.Description = "Total samples";
            samplesLbl.Font = new Font("Microsoft Sans Serif", 9.75F);
            samplesLbl.Location = new Point(8, 117);
            samplesLbl.Name = "samplesLbl";
            samplesLbl.Size = new Size(99, 16);
            samplesLbl.TabIndex = 6;
            samplesLbl.Value = "";
            // 
            // blockAlignLbl
            // 
            blockAlignLbl.AutoSize = true;
            blockAlignLbl.Description = "Block align";
            blockAlignLbl.Font = new Font("Microsoft Sans Serif", 9.75F);
            blockAlignLbl.Location = new Point(8, 99);
            blockAlignLbl.Name = "blockAlignLbl";
            blockAlignLbl.Size = new Size(79, 16);
            blockAlignLbl.TabIndex = 5;
            blockAlignLbl.Value = "";
            // 
            // bitRateLbl
            // 
            bitRateLbl.AutoSize = true;
            bitRateLbl.Description = "Bit rate (kbps)";
            bitRateLbl.Font = new Font("Microsoft Sans Serif", 9.75F);
            bitRateLbl.Location = new Point(8, 81);
            bitRateLbl.Name = "bitRateLbl";
            bitRateLbl.Size = new Size(95, 16);
            bitRateLbl.TabIndex = 4;
            bitRateLbl.Value = "";
            // 
            // encodingLbl
            // 
            encodingLbl.AutoSize = true;
            encodingLbl.Description = "Encoding";
            encodingLbl.Font = new Font("Microsoft Sans Serif", 9.75F);
            encodingLbl.Location = new Point(8, 63);
            encodingLbl.Name = "encodingLbl";
            encodingLbl.Size = new Size(70, 16);
            encodingLbl.TabIndex = 3;
            encodingLbl.Value = "";
            // 
            // channelsLbl
            // 
            channelsLbl.AutoSize = true;
            channelsLbl.Description = "Channels";
            channelsLbl.Font = new Font("Microsoft Sans Serif", 9.75F);
            channelsLbl.Location = new Point(8, 45);
            channelsLbl.Name = "channelsLbl";
            channelsLbl.Size = new Size(69, 16);
            channelsLbl.TabIndex = 2;
            channelsLbl.Value = "";
            // 
            // bitDepthLbl
            // 
            bitDepthLbl.AutoSize = true;
            bitDepthLbl.Description = "Bit depth (bit)";
            bitDepthLbl.Font = new Font("Microsoft Sans Serif", 9.75F);
            bitDepthLbl.Location = new Point(8, 27);
            bitDepthLbl.Name = "bitDepthLbl";
            bitDepthLbl.Size = new Size(90, 16);
            bitDepthLbl.TabIndex = 1;
            bitDepthLbl.Value = "";
            // 
            // sampleRateLbl
            // 
            sampleRateLbl.AutoSize = true;
            sampleRateLbl.Description = "Sample rate (Hz)";
            sampleRateLbl.Font = new Font("Microsoft Sans Serif", 9.75F);
            sampleRateLbl.Location = new Point(8, 9);
            sampleRateLbl.Name = "sampleRateLbl";
            sampleRateLbl.Size = new Size(113, 16);
            sampleRateLbl.TabIndex = 0;
            sampleRateLbl.Value = "";
            // 
            // metadataPage
            // 
            metadataPage.Controls.Add(coverImageBox);
            metadataPage.Controls.Add(durationLbl);
            metadataPage.Controls.Add(trackNumberLbl);
            metadataPage.Controls.Add(yearLbl);
            metadataPage.Controls.Add(genreLbl);
            metadataPage.Controls.Add(albumLbl);
            metadataPage.Controls.Add(artistLbl);
            metadataPage.Controls.Add(titleLbl);
            metadataPage.Location = new Point(4, 24);
            metadataPage.Name = "metadataPage";
            metadataPage.Padding = new Padding(3);
            metadataPage.Size = new Size(351, 163);
            metadataPage.TabIndex = 1;
            metadataPage.Text = "Metadata";
            metadataPage.UseVisualStyleBackColor = true;
            // 
            // coverImageBox
            // 
            coverImageBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            coverImageBox.Location = new Point(241, 53);
            coverImageBox.Name = "coverImageBox";
            coverImageBox.Size = new Size(110, 110);
            coverImageBox.SizeMode = PictureBoxSizeMode.Zoom;
            coverImageBox.TabIndex = 14;
            coverImageBox.TabStop = false;
            // 
            // durationLbl
            // 
            durationLbl.AutoSize = true;
            durationLbl.Description = "Duration (hh:mm:ss.mss)";
            durationLbl.Font = new Font("Microsoft Sans Serif", 9.75F);
            durationLbl.Location = new Point(8, 134);
            durationLbl.Name = "durationLbl";
            durationLbl.Size = new Size(158, 16);
            durationLbl.TabIndex = 13;
            durationLbl.Value = "";
            // 
            // trackNumberLbl
            // 
            trackNumberLbl.AutoSize = true;
            trackNumberLbl.Description = "Track #";
            trackNumberLbl.Font = new Font("Microsoft Sans Serif", 9.75F);
            trackNumberLbl.Location = new Point(8, 113);
            trackNumberLbl.Name = "trackNumberLbl";
            trackNumberLbl.Size = new Size(58, 16);
            trackNumberLbl.TabIndex = 12;
            trackNumberLbl.Value = "";
            // 
            // yearLbl
            // 
            yearLbl.AutoSize = true;
            yearLbl.Description = "Year";
            yearLbl.Font = new Font("Microsoft Sans Serif", 9.75F);
            yearLbl.Location = new Point(8, 92);
            yearLbl.Name = "yearLbl";
            yearLbl.Size = new Size(42, 16);
            yearLbl.TabIndex = 11;
            yearLbl.Value = "";
            // 
            // genreLbl
            // 
            genreLbl.AutoSize = true;
            genreLbl.Description = "Genre";
            genreLbl.Font = new Font("Microsoft Sans Serif", 9.75F);
            genreLbl.Location = new Point(8, 71);
            genreLbl.Name = "genreLbl";
            genreLbl.Size = new Size(50, 16);
            genreLbl.TabIndex = 10;
            genreLbl.Value = "";
            // 
            // albumLbl
            // 
            albumLbl.AutoSize = true;
            albumLbl.Description = "Album";
            albumLbl.Font = new Font("Microsoft Sans Serif", 9.75F);
            albumLbl.Location = new Point(8, 50);
            albumLbl.Name = "albumLbl";
            albumLbl.Size = new Size(51, 16);
            albumLbl.TabIndex = 9;
            albumLbl.Value = "";
            // 
            // artistLbl
            // 
            artistLbl.AutoSize = true;
            artistLbl.Description = "Artist";
            artistLbl.Font = new Font("Microsoft Sans Serif", 9.75F);
            artistLbl.Location = new Point(8, 29);
            artistLbl.Name = "artistLbl";
            artistLbl.Size = new Size(42, 16);
            artistLbl.TabIndex = 8;
            artistLbl.Value = "";
            // 
            // titleLbl
            // 
            titleLbl.AutoSize = true;
            titleLbl.Description = "Title";
            titleLbl.Font = new Font("Microsoft Sans Serif", 9.75F);
            titleLbl.Location = new Point(8, 9);
            titleLbl.Name = "titleLbl";
            titleLbl.Size = new Size(39, 16);
            titleLbl.TabIndex = 7;
            titleLbl.Value = "";
            // 
            // statsPage
            // 
            statsPage.Location = new Point(4, 24);
            statsPage.Name = "statsPage";
            statsPage.Size = new Size(351, 163);
            statsPage.TabIndex = 2;
            statsPage.Text = "Audio analysis";
            statsPage.UseVisualStyleBackColor = true;
            // 
            // AudioInfoDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(359, 191);
            Controls.Add(tabControl);
            Margin = new Padding(2);
            MaximizeBox = false;
            MaximumSize = new Size(1000, 230);
            MinimizeBox = false;
            MinimumSize = new Size(375, 230);
            Name = "AudioInfoDialog";
            ShowIcon = false;
            Text = "Audio Info";
            tabControl.ResumeLayout(false);
            techPage.ResumeLayout(false);
            techPage.PerformLayout();
            metadataPage.ResumeLayout(false);
            metadataPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)coverImageBox).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private TabControl tabControl;
        private TabPage techPage;
        private TabPage metadataPage;
        private TabPage statsPage;
        private UI.Components.Labels.DescriptorLabel sampleRateLbl;
        private UI.Components.Labels.DescriptorLabel bitDepthLbl;
        private UI.Components.Labels.DescriptorLabel channelsLbl;
        private UI.Components.Labels.DescriptorLabel blockAlignLbl;
        private UI.Components.Labels.DescriptorLabel bitRateLbl;
        private UI.Components.Labels.DescriptorLabel encodingLbl;
        private UI.Components.Labels.DescriptorLabel samplesLbl;
        private UI.Components.Labels.DescriptorLabel framesLbl;
        private UI.Components.Labels.DescriptorLabel titleLbl;
        private UI.Components.Labels.DescriptorLabel albumLbl;
        private UI.Components.Labels.DescriptorLabel artistLbl;
        private UI.Components.Labels.DescriptorLabel yearLbl;
        private UI.Components.Labels.DescriptorLabel genreLbl;
        private UI.Components.Labels.DescriptorLabel trackNumberLbl;
        private UI.Components.Labels.DescriptorLabel durationLbl;
        private PictureBox coverImageBox;
    }
}