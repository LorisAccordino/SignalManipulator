namespace SignalManipulator
{
    partial class MainForm
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
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.leftSideSplitContainer = new System.Windows.Forms.SplitContainer();
            this.importGroupBox = new System.Windows.Forms.GroupBox();
            this.effectsGroupBox = new System.Windows.Forms.GroupBox();
            this.rightSideSplitContainer = new System.Windows.Forms.SplitContainer();
            this.visualizationGroupBox = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.waveformPage = new System.Windows.Forms.TabPage();
            this.spectrumPage = new System.Windows.Forms.TabPage();
            this.stereoPage = new System.Windows.Forms.TabPage();
            this.bottomSplitContainer = new System.Windows.Forms.SplitContainer();
            this.playbackGroupBox = new System.Windows.Forms.GroupBox();
            this.routingGroupBox = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openAudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.audioOFD = new System.Windows.Forms.OpenFileDialog();
            this.effectChain = new SignalManipulator.UI.Controls.EffectChainControl();
            this.waveformViewer = new SignalManipulator.UI.Controls.WaveformViewerControl();
            this.spectrumViewer = new SignalManipulator.UI.Controls.SpectrumViewerControl();
            this.lissajousViewer = new SignalManipulator.UI.Controls.LissajousViewerControl();
            this.audioPlayer = new SignalManipulator.UI.Controls.AudioPlayerControl();
            this.audioRouter = new SignalManipulator.UI.Controls.AudioRouterControl();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftSideSplitContainer)).BeginInit();
            this.leftSideSplitContainer.Panel1.SuspendLayout();
            this.leftSideSplitContainer.Panel2.SuspendLayout();
            this.leftSideSplitContainer.SuspendLayout();
            this.effectsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightSideSplitContainer)).BeginInit();
            this.rightSideSplitContainer.Panel1.SuspendLayout();
            this.rightSideSplitContainer.Panel2.SuspendLayout();
            this.rightSideSplitContainer.SuspendLayout();
            this.visualizationGroupBox.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.waveformPage.SuspendLayout();
            this.spectrumPage.SuspendLayout();
            this.stereoPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bottomSplitContainer)).BeginInit();
            this.bottomSplitContainer.Panel1.SuspendLayout();
            this.bottomSplitContainer.Panel2.SuspendLayout();
            this.bottomSplitContainer.SuspendLayout();
            this.playbackGroupBox.SuspendLayout();
            this.routingGroupBox.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 24);
            this.mainSplitContainer.Margin = new System.Windows.Forms.Padding(2);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.leftSideSplitContainer);
            this.mainSplitContainer.Panel1MinSize = 300;
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.rightSideSplitContainer);
            this.mainSplitContainer.Panel2MinSize = 700;
            this.mainSplitContainer.Size = new System.Drawing.Size(1113, 554);
            this.mainSplitContainer.SplitterDistance = 369;
            this.mainSplitContainer.SplitterWidth = 3;
            this.mainSplitContainer.TabIndex = 1;
            // 
            // leftSideSplitContainer
            // 
            this.leftSideSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftSideSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.leftSideSplitContainer.Margin = new System.Windows.Forms.Padding(2);
            this.leftSideSplitContainer.Name = "leftSideSplitContainer";
            this.leftSideSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // leftSideSplitContainer.Panel1
            // 
            this.leftSideSplitContainer.Panel1.Controls.Add(this.importGroupBox);
            this.leftSideSplitContainer.Panel1Collapsed = true;
            this.leftSideSplitContainer.Panel1MinSize = 150;
            // 
            // leftSideSplitContainer.Panel2
            // 
            this.leftSideSplitContainer.Panel2.Controls.Add(this.effectsGroupBox);
            this.leftSideSplitContainer.Panel2MinSize = 300;
            this.leftSideSplitContainer.Size = new System.Drawing.Size(369, 554);
            this.leftSideSplitContainer.SplitterDistance = 399;
            this.leftSideSplitContainer.SplitterWidth = 3;
            this.leftSideSplitContainer.TabIndex = 0;
            // 
            // importGroupBox
            // 
            this.importGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.importGroupBox.Location = new System.Drawing.Point(0, 0);
            this.importGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.importGroupBox.Name = "importGroupBox";
            this.importGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.importGroupBox.Size = new System.Drawing.Size(150, 399);
            this.importGroupBox.TabIndex = 3;
            this.importGroupBox.TabStop = false;
            this.importGroupBox.Text = "Import:";
            // 
            // effectsGroupBox
            // 
            this.effectsGroupBox.Controls.Add(this.effectChain);
            this.effectsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.effectsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.effectsGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.effectsGroupBox.Name = "effectsGroupBox";
            this.effectsGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.effectsGroupBox.Size = new System.Drawing.Size(369, 554);
            this.effectsGroupBox.TabIndex = 1;
            this.effectsGroupBox.TabStop = false;
            this.effectsGroupBox.Text = "Effects chain:";
            // 
            // rightSideSplitContainer
            // 
            this.rightSideSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightSideSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.rightSideSplitContainer.Margin = new System.Windows.Forms.Padding(2);
            this.rightSideSplitContainer.Name = "rightSideSplitContainer";
            this.rightSideSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // rightSideSplitContainer.Panel1
            // 
            this.rightSideSplitContainer.Panel1.Controls.Add(this.visualizationGroupBox);
            this.rightSideSplitContainer.Panel1MinSize = 350;
            // 
            // rightSideSplitContainer.Panel2
            // 
            this.rightSideSplitContainer.Panel2.Controls.Add(this.bottomSplitContainer);
            this.rightSideSplitContainer.Panel2MinSize = 150;
            this.rightSideSplitContainer.Size = new System.Drawing.Size(741, 554);
            this.rightSideSplitContainer.SplitterDistance = 398;
            this.rightSideSplitContainer.SplitterWidth = 3;
            this.rightSideSplitContainer.TabIndex = 0;
            // 
            // visualizationGroupBox
            // 
            this.visualizationGroupBox.Controls.Add(this.tabControl1);
            this.visualizationGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visualizationGroupBox.Location = new System.Drawing.Point(0, 0);
            this.visualizationGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.visualizationGroupBox.Name = "visualizationGroupBox";
            this.visualizationGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.visualizationGroupBox.Size = new System.Drawing.Size(741, 398);
            this.visualizationGroupBox.TabIndex = 0;
            this.visualizationGroupBox.TabStop = false;
            this.visualizationGroupBox.Text = "Visualization:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.waveformPage);
            this.tabControl1.Controls.Add(this.spectrumPage);
            this.tabControl1.Controls.Add(this.stereoPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(2, 15);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(737, 381);
            this.tabControl1.TabIndex = 1;
            // 
            // waveformPage
            // 
            this.waveformPage.Controls.Add(this.waveformViewer);
            this.waveformPage.Location = new System.Drawing.Point(4, 22);
            this.waveformPage.Name = "waveformPage";
            this.waveformPage.Padding = new System.Windows.Forms.Padding(3);
            this.waveformPage.Size = new System.Drawing.Size(729, 355);
            this.waveformPage.TabIndex = 0;
            this.waveformPage.Text = "Signal Waveform";
            this.waveformPage.UseVisualStyleBackColor = true;
            // 
            // spectrumPage
            // 
            this.spectrumPage.Controls.Add(this.spectrumViewer);
            this.spectrumPage.Location = new System.Drawing.Point(4, 22);
            this.spectrumPage.Name = "spectrumPage";
            this.spectrumPage.Padding = new System.Windows.Forms.Padding(3);
            this.spectrumPage.Size = new System.Drawing.Size(729, 355);
            this.spectrumPage.TabIndex = 1;
            this.spectrumPage.Text = "FFT Spectrum";
            this.spectrumPage.UseVisualStyleBackColor = true;
            // 
            // stereoPage
            // 
            this.stereoPage.Controls.Add(this.lissajousViewer);
            this.stereoPage.Location = new System.Drawing.Point(4, 22);
            this.stereoPage.Name = "stereoPage";
            this.stereoPage.Padding = new System.Windows.Forms.Padding(3);
            this.stereoPage.Size = new System.Drawing.Size(729, 355);
            this.stereoPage.TabIndex = 2;
            this.stereoPage.Text = "XY Stero Oscilloscpe";
            this.stereoPage.UseVisualStyleBackColor = true;
            // 
            // bottomSplitContainer
            // 
            this.bottomSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.bottomSplitContainer.Margin = new System.Windows.Forms.Padding(2);
            this.bottomSplitContainer.Name = "bottomSplitContainer";
            // 
            // bottomSplitContainer.Panel1
            // 
            this.bottomSplitContainer.Panel1.Controls.Add(this.playbackGroupBox);
            this.bottomSplitContainer.Panel1MinSize = 400;
            // 
            // bottomSplitContainer.Panel2
            // 
            this.bottomSplitContainer.Panel2.Controls.Add(this.routingGroupBox);
            this.bottomSplitContainer.Panel2MinSize = 300;
            this.bottomSplitContainer.Size = new System.Drawing.Size(741, 153);
            this.bottomSplitContainer.SplitterDistance = 400;
            this.bottomSplitContainer.SplitterWidth = 3;
            this.bottomSplitContainer.TabIndex = 0;
            // 
            // playbackGroupBox
            // 
            this.playbackGroupBox.Controls.Add(this.audioPlayer);
            this.playbackGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playbackGroupBox.Location = new System.Drawing.Point(0, 0);
            this.playbackGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.playbackGroupBox.Name = "playbackGroupBox";
            this.playbackGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.playbackGroupBox.Size = new System.Drawing.Size(400, 153);
            this.playbackGroupBox.TabIndex = 2;
            this.playbackGroupBox.TabStop = false;
            this.playbackGroupBox.Text = "Playback:";
            // 
            // routingGroupBox
            // 
            this.routingGroupBox.Controls.Add(this.audioRouter);
            this.routingGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.routingGroupBox.Location = new System.Drawing.Point(0, 0);
            this.routingGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.routingGroupBox.Name = "routingGroupBox";
            this.routingGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.routingGroupBox.Size = new System.Drawing.Size(338, 153);
            this.routingGroupBox.TabIndex = 4;
            this.routingGroupBox.TabStop = false;
            this.routingGroupBox.Text = "Audio routing:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1113, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openAudioToolStripMenuItem,
            this.saveAudioToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openAudioToolStripMenuItem
            // 
            this.openAudioToolStripMenuItem.Name = "openAudioToolStripMenuItem";
            this.openAudioToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.openAudioToolStripMenuItem.Text = "Open audio";
            this.openAudioToolStripMenuItem.Click += new System.EventHandler(this.openAudioToolStripMenuItem_Click);
            // 
            // saveAudioToolStripMenuItem
            // 
            this.saveAudioToolStripMenuItem.Name = "saveAudioToolStripMenuItem";
            this.saveAudioToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.saveAudioToolStripMenuItem.Text = "Save audio";
            // 
            // audioOFD
            // 
            this.audioOFD.Filter = "Audio files|*.mp3;*.wav;*.aiff;*.flac";
            // 
            // effectChain
            // 
            this.effectChain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.effectChain.Location = new System.Drawing.Point(2, 15);
            this.effectChain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.effectChain.Name = "effectChain";
            this.effectChain.Size = new System.Drawing.Size(365, 537);
            this.effectChain.TabIndex = 0;
            // 
            // waveformViewer
            // 
            this.waveformViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.waveformViewer.Location = new System.Drawing.Point(3, 3);
            this.waveformViewer.Margin = new System.Windows.Forms.Padding(2);
            this.waveformViewer.Name = "waveformViewer";
            this.waveformViewer.Size = new System.Drawing.Size(723, 349);
            this.waveformViewer.TabIndex = 0;
            // 
            // spectrumViewer
            // 
            this.spectrumViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spectrumViewer.Location = new System.Drawing.Point(3, 3);
            this.spectrumViewer.Margin = new System.Windows.Forms.Padding(4);
            this.spectrumViewer.Name = "spectrumViewer";
            this.spectrumViewer.Size = new System.Drawing.Size(723, 349);
            this.spectrumViewer.TabIndex = 0;
            // 
            // lissajousViewer
            // 
            this.lissajousViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lissajousViewer.Location = new System.Drawing.Point(3, 3);
            this.lissajousViewer.Margin = new System.Windows.Forms.Padding(4);
            this.lissajousViewer.Name = "lissajousViewer";
            this.lissajousViewer.Size = new System.Drawing.Size(723, 349);
            this.lissajousViewer.TabIndex = 0;
            // 
            // audioPlayer
            // 
            this.audioPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.audioPlayer.Location = new System.Drawing.Point(2, 15);
            this.audioPlayer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.audioPlayer.MinimumSize = new System.Drawing.Size(262, 110);
            this.audioPlayer.Name = "audioPlayer";
            this.audioPlayer.Size = new System.Drawing.Size(396, 136);
            this.audioPlayer.TabIndex = 0;
            // 
            // audioRouter
            // 
            this.audioRouter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.audioRouter.Location = new System.Drawing.Point(2, 15);
            this.audioRouter.Margin = new System.Windows.Forms.Padding(4);
            this.audioRouter.Name = "audioRouter";
            this.audioRouter.Size = new System.Drawing.Size(334, 136);
            this.audioRouter.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 578);
            this.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(1128, 613);
            this.Name = "MainForm";
            this.Text = "Signal Manipulator";
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.leftSideSplitContainer.Panel1.ResumeLayout(false);
            this.leftSideSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leftSideSplitContainer)).EndInit();
            this.leftSideSplitContainer.ResumeLayout(false);
            this.effectsGroupBox.ResumeLayout(false);
            this.rightSideSplitContainer.Panel1.ResumeLayout(false);
            this.rightSideSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rightSideSplitContainer)).EndInit();
            this.rightSideSplitContainer.ResumeLayout(false);
            this.visualizationGroupBox.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.waveformPage.ResumeLayout(false);
            this.spectrumPage.ResumeLayout(false);
            this.stereoPage.ResumeLayout(false);
            this.bottomSplitContainer.Panel1.ResumeLayout(false);
            this.bottomSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bottomSplitContainer)).EndInit();
            this.bottomSplitContainer.ResumeLayout(false);
            this.playbackGroupBox.ResumeLayout(false);
            this.routingGroupBox.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox visualizationGroupBox;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.SplitContainer leftSideSplitContainer;
        private System.Windows.Forms.GroupBox importGroupBox;
        private System.Windows.Forms.GroupBox effectsGroupBox;
        private System.Windows.Forms.SplitContainer rightSideSplitContainer;
        private System.Windows.Forms.SplitContainer bottomSplitContainer;
        private System.Windows.Forms.GroupBox playbackGroupBox;
        private System.Windows.Forms.GroupBox routingGroupBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openAudioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAudioToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog audioOFD;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage waveformPage;
        private System.Windows.Forms.TabPage spectrumPage;
        private System.Windows.Forms.TabPage stereoPage;
        private UI.Controls.EffectChainControl effectChain;
        private UI.Controls.WaveformViewerControl waveformViewer;
        private UI.Controls.SpectrumViewerControl spectrumViewer;
        private UI.Controls.AudioRouterControl audioRouter;
        private UI.Controls.AudioPlayerControl audioPlayer;
        private UI.Controls.LissajousViewerControl lissajousViewer;
    }
}