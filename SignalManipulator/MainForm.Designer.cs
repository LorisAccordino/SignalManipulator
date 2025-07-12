using SignalManipulator.Controls;
using SignalManipulator.UI.Controls.User;
using SignalManipulator.ViewModels;

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
            components = new System.ComponentModel.Container();
            mainSplitContainer = new SplitContainer();
            leftSideSplitContainer = new SplitContainer();
            importGroupBox = new GroupBox();
            effectsGroupBox = new GroupBox();
            effectChain = new EffectChainControl();
            rightSideSplitContainer = new SplitContainer();
            visualizationGroupBox = new GroupBox();
            viewersTab = new TabControl();
            waveformPage = new TabPage();
            waveformViewer = new WaveformViewModel();
            spectrumPage = new TabPage();
            spectrumViewer = new SpectrumViewModel();
            stereoXYPage = new TabPage();
            lissajousViewer = new LissajousViewModel();
            surroundAnalyzerPage = new TabPage();
            surroundAnalyzer = new SurroundAnalyzerViewModel();
            bottomSplitContainer = new SplitContainer();
            playbackGroupBox = new GroupBox();
            audioPlayer = new AudioPlayerControl();
            routingGroupBox = new GroupBox();
            audioRouter = new AudioRouterControl();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openAudioToolStripMenuItem = new ToolStripMenuItem();
            saveAudioToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            effectsToolStripMenuItem = new ToolStripMenuItem();
            playbackToolStripMenuItem = new ToolStripMenuItem();
            routingToolStripMenuItem = new ToolStripMenuItem();
            audioOFD = new OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).BeginInit();
            mainSplitContainer.Panel1.SuspendLayout();
            mainSplitContainer.Panel2.SuspendLayout();
            mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)leftSideSplitContainer).BeginInit();
            leftSideSplitContainer.Panel1.SuspendLayout();
            leftSideSplitContainer.Panel2.SuspendLayout();
            leftSideSplitContainer.SuspendLayout();
            effectsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)rightSideSplitContainer).BeginInit();
            rightSideSplitContainer.Panel1.SuspendLayout();
            rightSideSplitContainer.Panel2.SuspendLayout();
            rightSideSplitContainer.SuspendLayout();
            visualizationGroupBox.SuspendLayout();
            viewersTab.SuspendLayout();
            waveformPage.SuspendLayout();
            spectrumPage.SuspendLayout();
            stereoXYPage.SuspendLayout();
            surroundAnalyzerPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bottomSplitContainer).BeginInit();
            bottomSplitContainer.Panel1.SuspendLayout();
            bottomSplitContainer.Panel2.SuspendLayout();
            bottomSplitContainer.SuspendLayout();
            playbackGroupBox.SuspendLayout();
            routingGroupBox.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // mainSplitContainer
            // 
            mainSplitContainer.Dock = DockStyle.Fill;
            mainSplitContainer.Location = new Point(0, 24);
            mainSplitContainer.Margin = new Padding(2);
            mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            mainSplitContainer.Panel1.Controls.Add(leftSideSplitContainer);
            mainSplitContainer.Panel1.Padding = new Padding(5, 0, 0, 5);
            mainSplitContainer.Panel1MinSize = 300;
            // 
            // mainSplitContainer.Panel2
            // 
            mainSplitContainer.Panel2.Controls.Add(rightSideSplitContainer);
            mainSplitContainer.Panel2MinSize = 800;
            mainSplitContainer.Size = new Size(1284, 637);
            mainSplitContainer.SplitterDistance = 425;
            mainSplitContainer.TabIndex = 1;
            // 
            // leftSideSplitContainer
            // 
            leftSideSplitContainer.Dock = DockStyle.Fill;
            leftSideSplitContainer.Location = new Point(5, 0);
            leftSideSplitContainer.Margin = new Padding(2);
            leftSideSplitContainer.Name = "leftSideSplitContainer";
            leftSideSplitContainer.Orientation = Orientation.Horizontal;
            // 
            // leftSideSplitContainer.Panel1
            // 
            leftSideSplitContainer.Panel1.Controls.Add(importGroupBox);
            leftSideSplitContainer.Panel1Collapsed = true;
            leftSideSplitContainer.Panel1MinSize = 150;
            // 
            // leftSideSplitContainer.Panel2
            // 
            leftSideSplitContainer.Panel2.Controls.Add(effectsGroupBox);
            leftSideSplitContainer.Panel2MinSize = 300;
            leftSideSplitContainer.Size = new Size(420, 632);
            leftSideSplitContainer.SplitterDistance = 460;
            leftSideSplitContainer.SplitterWidth = 3;
            leftSideSplitContainer.TabIndex = 0;
            // 
            // importGroupBox
            // 
            importGroupBox.Dock = DockStyle.Fill;
            importGroupBox.Location = new Point(0, 0);
            importGroupBox.Margin = new Padding(2);
            importGroupBox.Name = "importGroupBox";
            importGroupBox.Padding = new Padding(2);
            importGroupBox.Size = new Size(150, 460);
            importGroupBox.TabIndex = 3;
            importGroupBox.TabStop = false;
            importGroupBox.Text = "Import:";
            // 
            // effectsGroupBox
            // 
            effectsGroupBox.Controls.Add(effectChain);
            effectsGroupBox.Dock = DockStyle.Fill;
            effectsGroupBox.Location = new Point(0, 0);
            effectsGroupBox.Margin = new Padding(2);
            effectsGroupBox.Name = "effectsGroupBox";
            effectsGroupBox.Padding = new Padding(2);
            effectsGroupBox.Size = new Size(420, 632);
            effectsGroupBox.TabIndex = 1;
            effectsGroupBox.TabStop = false;
            effectsGroupBox.Text = "Effects chain:";
            // 
            // effectChain
            // 
            effectChain.Dock = DockStyle.Fill;
            effectChain.Location = new Point(2, 18);
            effectChain.Margin = new Padding(2);
            effectChain.Name = "effectChain";
            effectChain.Size = new Size(416, 612);
            effectChain.TabIndex = 0;
            // 
            // rightSideSplitContainer
            // 
            rightSideSplitContainer.Dock = DockStyle.Fill;
            rightSideSplitContainer.Location = new Point(0, 0);
            rightSideSplitContainer.Margin = new Padding(2);
            rightSideSplitContainer.Name = "rightSideSplitContainer";
            rightSideSplitContainer.Orientation = Orientation.Horizontal;
            // 
            // rightSideSplitContainer.Panel1
            // 
            rightSideSplitContainer.Panel1.Controls.Add(visualizationGroupBox);
            rightSideSplitContainer.Panel1.Padding = new Padding(0, 0, 5, 0);
            rightSideSplitContainer.Panel1MinSize = 420;
            // 
            // rightSideSplitContainer.Panel2
            // 
            rightSideSplitContainer.Panel2.Controls.Add(bottomSplitContainer);
            rightSideSplitContainer.Panel2MinSize = 200;
            rightSideSplitContainer.Size = new Size(855, 637);
            rightSideSplitContainer.SplitterDistance = 434;
            rightSideSplitContainer.SplitterWidth = 3;
            rightSideSplitContainer.TabIndex = 0;
            // 
            // visualizationGroupBox
            // 
            visualizationGroupBox.Controls.Add(viewersTab);
            visualizationGroupBox.Dock = DockStyle.Fill;
            visualizationGroupBox.Location = new Point(0, 0);
            visualizationGroupBox.Margin = new Padding(2);
            visualizationGroupBox.Name = "visualizationGroupBox";
            visualizationGroupBox.Padding = new Padding(2);
            visualizationGroupBox.Size = new Size(850, 434);
            visualizationGroupBox.TabIndex = 0;
            visualizationGroupBox.TabStop = false;
            visualizationGroupBox.Text = "Visualization:";
            // 
            // viewersTab
            // 
            viewersTab.Controls.Add(waveformPage);
            viewersTab.Controls.Add(spectrumPage);
            viewersTab.Controls.Add(stereoXYPage);
            viewersTab.Controls.Add(surroundAnalyzerPage);
            viewersTab.Dock = DockStyle.Fill;
            viewersTab.Location = new Point(2, 18);
            viewersTab.Margin = new Padding(4, 3, 4, 3);
            viewersTab.Name = "viewersTab";
            viewersTab.SelectedIndex = 0;
            viewersTab.Size = new Size(846, 414);
            viewersTab.TabIndex = 1;
            // 
            // waveformPage
            // 
            waveformPage.BackColor = SystemColors.Control;
            waveformPage.Controls.Add(waveformViewer);
            waveformPage.Location = new Point(4, 24);
            waveformPage.Margin = new Padding(4, 3, 4, 3);
            waveformPage.Name = "waveformPage";
            waveformPage.Padding = new Padding(4, 3, 4, 3);
            waveformPage.Size = new Size(838, 386);
            waveformPage.TabIndex = 0;
            waveformPage.Text = "Signal Waveform";
            // 
            // waveformViewer
            // 
            waveformViewer.Dock = DockStyle.Fill;
            waveformViewer.IsFloating = false;
            waveformViewer.Location = new Point(4, 3);
            waveformViewer.Margin = new Padding(2);
            waveformViewer.Name = "waveformViewer";
            waveformViewer.Size = new Size(830, 380);
            waveformViewer.TabIndex = 0;
            waveformViewer.Text = "Signal Waveform";
            // 
            // spectrumPage
            // 
            spectrumPage.BackColor = SystemColors.Control;
            spectrumPage.Controls.Add(spectrumViewer);
            spectrumPage.Location = new Point(4, 24);
            spectrumPage.Margin = new Padding(4, 3, 4, 3);
            spectrumPage.Name = "spectrumPage";
            spectrumPage.Padding = new Padding(4, 3, 4, 3);
            spectrumPage.Size = new Size(192, 72);
            spectrumPage.TabIndex = 1;
            spectrumPage.Text = "FFT Spectrum";
            // 
            // spectrumViewer
            // 
            spectrumViewer.Dock = DockStyle.Fill;
            spectrumViewer.IsFloating = false;
            spectrumViewer.Location = new Point(4, 3);
            spectrumViewer.Margin = new Padding(5);
            spectrumViewer.Name = "spectrumViewer";
            spectrumViewer.Size = new Size(184, 66);
            spectrumViewer.TabIndex = 0;
            spectrumViewer.Text = "FFT Spectrum";
            // 
            // stereoXYPage
            // 
            stereoXYPage.BackColor = SystemColors.Control;
            stereoXYPage.Controls.Add(lissajousViewer);
            stereoXYPage.Location = new Point(4, 24);
            stereoXYPage.Margin = new Padding(4, 3, 4, 3);
            stereoXYPage.Name = "stereoXYPage";
            stereoXYPage.Padding = new Padding(4, 3, 4, 3);
            stereoXYPage.Size = new Size(192, 72);
            stereoXYPage.TabIndex = 2;
            stereoXYPage.Text = "XY Stereo Oscilloscope";
            // 
            // lissajousViewer
            // 
            lissajousViewer.Dock = DockStyle.Fill;
            lissajousViewer.IsFloating = false;
            lissajousViewer.Location = new Point(4, 3);
            lissajousViewer.Margin = new Padding(5);
            lissajousViewer.Name = "lissajousViewer";
            lissajousViewer.Size = new Size(380, 380);
            lissajousViewer.TabIndex = 0;
            lissajousViewer.Text = "XY Stereo Oscilloscope";
            // 
            // surroundAnalyzerPage
            // 
            surroundAnalyzerPage.BackColor = SystemColors.Control;
            surroundAnalyzerPage.Controls.Add(surroundAnalyzer);
            surroundAnalyzerPage.Location = new Point(4, 24);
            surroundAnalyzerPage.Name = "surroundAnalyzerPage";
            surroundAnalyzerPage.Padding = new Padding(3);
            surroundAnalyzerPage.Size = new Size(192, 72);
            surroundAnalyzerPage.TabIndex = 3;
            surroundAnalyzerPage.Text = "Surround Analyzer";
            // 
            // surroundAnalyzer
            // 
            surroundAnalyzer.Dock = DockStyle.Fill;
            surroundAnalyzer.IsFloating = false;
            surroundAnalyzer.Location = new Point(3, 3);
            surroundAnalyzer.Margin = new Padding(4, 3, 4, 3);
            surroundAnalyzer.Name = "surroundAnalyzer";
            surroundAnalyzer.Size = new Size(380, 380);
            surroundAnalyzer.TabIndex = 2;
            surroundAnalyzer.Text = "Surround Analyzer";
            // 
            // bottomSplitContainer
            // 
            bottomSplitContainer.Dock = DockStyle.Fill;
            bottomSplitContainer.Location = new Point(0, 0);
            bottomSplitContainer.Margin = new Padding(2);
            bottomSplitContainer.Name = "bottomSplitContainer";
            // 
            // bottomSplitContainer.Panel1
            // 
            bottomSplitContainer.Panel1.Controls.Add(playbackGroupBox);
            bottomSplitContainer.Panel1.Padding = new Padding(0, 0, 0, 5);
            bottomSplitContainer.Panel1MinSize = 400;
            // 
            // bottomSplitContainer.Panel2
            // 
            bottomSplitContainer.Panel2.Controls.Add(routingGroupBox);
            bottomSplitContainer.Panel2.Padding = new Padding(0, 0, 5, 5);
            bottomSplitContainer.Panel2MinSize = 300;
            bottomSplitContainer.Size = new Size(855, 200);
            bottomSplitContainer.SplitterDistance = 460;
            bottomSplitContainer.TabIndex = 0;
            // 
            // playbackGroupBox
            // 
            playbackGroupBox.Controls.Add(audioPlayer);
            playbackGroupBox.Dock = DockStyle.Fill;
            playbackGroupBox.Location = new Point(0, 0);
            playbackGroupBox.Margin = new Padding(2);
            playbackGroupBox.Name = "playbackGroupBox";
            playbackGroupBox.Padding = new Padding(2);
            playbackGroupBox.Size = new Size(460, 195);
            playbackGroupBox.TabIndex = 2;
            playbackGroupBox.TabStop = false;
            playbackGroupBox.Text = "Playback:";
            // 
            // audioPlayer
            // 
            audioPlayer.Dock = DockStyle.Fill;
            audioPlayer.Location = new Point(2, 18);
            audioPlayer.Margin = new Padding(2);
            audioPlayer.MinimumSize = new Size(306, 170);
            audioPlayer.Name = "audioPlayer";
            audioPlayer.Size = new Size(456, 175);
            audioPlayer.TabIndex = 0;
            // 
            // routingGroupBox
            // 
            routingGroupBox.Controls.Add(audioRouter);
            routingGroupBox.Dock = DockStyle.Fill;
            routingGroupBox.Location = new Point(0, 0);
            routingGroupBox.Margin = new Padding(2);
            routingGroupBox.Name = "routingGroupBox";
            routingGroupBox.Padding = new Padding(2);
            routingGroupBox.Size = new Size(386, 195);
            routingGroupBox.TabIndex = 4;
            routingGroupBox.TabStop = false;
            routingGroupBox.Text = "Audio routing:";
            // 
            // audioRouter
            // 
            audioRouter.Dock = DockStyle.Fill;
            audioRouter.Location = new Point(2, 18);
            audioRouter.Margin = new Padding(5);
            audioRouter.Name = "audioRouter";
            audioRouter.Size = new Size(382, 175);
            audioRouter.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, viewToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            menuStrip1.Size = new Size(1284, 24);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openAudioToolStripMenuItem, saveAudioToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openAudioToolStripMenuItem
            // 
            openAudioToolStripMenuItem.Name = "openAudioToolStripMenuItem";
            openAudioToolStripMenuItem.Size = new Size(136, 22);
            openAudioToolStripMenuItem.Text = "Open audio";
            openAudioToolStripMenuItem.Click += openAudioToolStripMenuItem_Click;
            // 
            // saveAudioToolStripMenuItem
            // 
            saveAudioToolStripMenuItem.Name = "saveAudioToolStripMenuItem";
            saveAudioToolStripMenuItem.Size = new Size(136, 22);
            saveAudioToolStripMenuItem.Text = "Save audio";
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { effectsToolStripMenuItem, playbackToolStripMenuItem, routingToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(44, 20);
            viewToolStripMenuItem.Text = "View";
            // 
            // effectsToolStripMenuItem
            // 
            effectsToolStripMenuItem.Checked = true;
            effectsToolStripMenuItem.CheckOnClick = true;
            effectsToolStripMenuItem.CheckState = CheckState.Checked;
            effectsToolStripMenuItem.Name = "effectsToolStripMenuItem";
            effectsToolStripMenuItem.Size = new Size(121, 22);
            effectsToolStripMenuItem.Text = "Effects";
            effectsToolStripMenuItem.Click += ShowHideEffects;
            // 
            // playbackToolStripMenuItem
            // 
            playbackToolStripMenuItem.Checked = true;
            playbackToolStripMenuItem.CheckOnClick = true;
            playbackToolStripMenuItem.CheckState = CheckState.Checked;
            playbackToolStripMenuItem.Name = "playbackToolStripMenuItem";
            playbackToolStripMenuItem.Size = new Size(121, 22);
            playbackToolStripMenuItem.Text = "Playback";
            playbackToolStripMenuItem.Click += ShowHidePlaybackAndRouting;
            // 
            // routingToolStripMenuItem
            // 
            routingToolStripMenuItem.Checked = true;
            routingToolStripMenuItem.CheckOnClick = true;
            routingToolStripMenuItem.CheckState = CheckState.Checked;
            routingToolStripMenuItem.Name = "routingToolStripMenuItem";
            routingToolStripMenuItem.Size = new Size(121, 22);
            routingToolStripMenuItem.Text = "Routing";
            routingToolStripMenuItem.Click += ShowHidePlaybackAndRouting;
            // 
            // audioOFD
            // 
            audioOFD.Filter = "Audio files|*.mp3;*.wav;*.aiff;*.flac";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1284, 661);
            Controls.Add(mainSplitContainer);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(2);
            MinimumSize = new Size(1150, 700);
            Name = "MainForm";
            Text = "Signal Manipulator";
            mainSplitContainer.Panel1.ResumeLayout(false);
            mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)mainSplitContainer).EndInit();
            mainSplitContainer.ResumeLayout(false);
            leftSideSplitContainer.Panel1.ResumeLayout(false);
            leftSideSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)leftSideSplitContainer).EndInit();
            leftSideSplitContainer.ResumeLayout(false);
            effectsGroupBox.ResumeLayout(false);
            rightSideSplitContainer.Panel1.ResumeLayout(false);
            rightSideSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)rightSideSplitContainer).EndInit();
            rightSideSplitContainer.ResumeLayout(false);
            visualizationGroupBox.ResumeLayout(false);
            viewersTab.ResumeLayout(false);
            waveformPage.ResumeLayout(false);
            spectrumPage.ResumeLayout(false);
            stereoXYPage.ResumeLayout(false);
            surroundAnalyzerPage.ResumeLayout(false);
            bottomSplitContainer.Panel1.ResumeLayout(false);
            bottomSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)bottomSplitContainer).EndInit();
            bottomSplitContainer.ResumeLayout(false);
            playbackGroupBox.ResumeLayout(false);
            routingGroupBox.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.TabControl viewersTab;
        private System.Windows.Forms.TabPage waveformPage;
        private System.Windows.Forms.TabPage spectrumPage;
        private System.Windows.Forms.TabPage stereoXYPage;
        private EffectChainControl effectChain;
        private LissajousViewModel lissajousViewer;
        private AudioRouterControl audioRouter;
        private SpectrumViewModel spectrumViewer;
        private AudioPlayerControl audioPlayer;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem effectsToolStripMenuItem;
        private ToolStripMenuItem playbackToolStripMenuItem;
        private ToolStripMenuItem routingToolStripMenuItem;
        private WaveformViewModel waveformViewer;
        private TabPage surroundAnalyzerPage;
        private SurroundAnalyzerViewModel surroundAnalyzer;
    }
}