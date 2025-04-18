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
            this.visualizationGroupBox = new System.Windows.Forms.GroupBox();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.leftSideSplitContainer = new System.Windows.Forms.SplitContainer();
            this.importGroupBox = new System.Windows.Forms.GroupBox();
            this.effectsGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.effectsList = new System.Windows.Forms.CheckedListBox();
            this.rightSideSplitContainer = new System.Windows.Forms.SplitContainer();
            this.bottomSplitContainer = new System.Windows.Forms.SplitContainer();
            this.playbackGroupBox = new System.Windows.Forms.GroupBox();
            this.routingGroupBox = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openAudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftSideSplitContainer)).BeginInit();
            this.leftSideSplitContainer.Panel1.SuspendLayout();
            this.leftSideSplitContainer.Panel2.SuspendLayout();
            this.leftSideSplitContainer.SuspendLayout();
            this.effectsGroupBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightSideSplitContainer)).BeginInit();
            this.rightSideSplitContainer.Panel1.SuspendLayout();
            this.rightSideSplitContainer.Panel2.SuspendLayout();
            this.rightSideSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bottomSplitContainer)).BeginInit();
            this.bottomSplitContainer.Panel1.SuspendLayout();
            this.bottomSplitContainer.Panel2.SuspendLayout();
            this.bottomSplitContainer.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // visualizationGroupBox
            // 
            this.visualizationGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visualizationGroupBox.Location = new System.Drawing.Point(0, 0);
            this.visualizationGroupBox.Name = "visualizationGroupBox";
            this.visualizationGroupBox.Size = new System.Drawing.Size(986, 504);
            this.visualizationGroupBox.TabIndex = 0;
            this.visualizationGroupBox.TabStop = false;
            this.visualizationGroupBox.Text = "Visualization:";
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 28);
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
            this.mainSplitContainer.Size = new System.Drawing.Size(1482, 675);
            this.mainSplitContainer.SplitterDistance = 492;
            this.mainSplitContainer.TabIndex = 1;
            // 
            // leftSideSplitContainer
            // 
            this.leftSideSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftSideSplitContainer.Location = new System.Drawing.Point(0, 0);
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
            this.leftSideSplitContainer.Size = new System.Drawing.Size(492, 675);
            this.leftSideSplitContainer.SplitterDistance = 399;
            this.leftSideSplitContainer.TabIndex = 0;
            // 
            // importGroupBox
            // 
            this.importGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.importGroupBox.Location = new System.Drawing.Point(0, 0);
            this.importGroupBox.Name = "importGroupBox";
            this.importGroupBox.Size = new System.Drawing.Size(150, 399);
            this.importGroupBox.TabIndex = 3;
            this.importGroupBox.TabStop = false;
            this.importGroupBox.Text = "Import:";
            // 
            // effectsGroupBox
            // 
            this.effectsGroupBox.Controls.Add(this.tableLayoutPanel1);
            this.effectsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.effectsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.effectsGroupBox.Name = "effectsGroupBox";
            this.effectsGroupBox.Size = new System.Drawing.Size(492, 675);
            this.effectsGroupBox.TabIndex = 1;
            this.effectsGroupBox.TabStop = false;
            this.effectsGroupBox.Text = "Effects chain:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.effectsList, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(486, 654);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 622);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(480, 29);
            this.panel1.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(93, 1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 25);
            this.button2.TabIndex = 2;
            this.button2.Text = "Remove";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(3, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 25);
            this.button1.TabIndex = 1;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // effectsList
            // 
            this.effectsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.effectsList.FormattingEnabled = true;
            this.effectsList.Items.AddRange(new object[] {
            "Effect Test #1",
            "Effect Test #2",
            "Effect Test #3",
            "Effect Test #4",
            "Effect Test #5",
            "Effect Test #6",
            "Effect Test #7",
            "Effect Test #8",
            "Effect Test #9",
            "Effect Test #10",
            "Effect Test #11",
            "Effect Test #12",
            "Effect Test #13",
            "Effect Test #14",
            "Effect Test #15",
            "Effect Test #16",
            "Effect Test #17",
            "Effect Test #18",
            "Effect Test #19",
            "Effect Test #20"});
            this.effectsList.Location = new System.Drawing.Point(3, 3);
            this.effectsList.Name = "effectsList";
            this.effectsList.Size = new System.Drawing.Size(480, 613);
            this.effectsList.TabIndex = 0;
            // 
            // rightSideSplitContainer
            // 
            this.rightSideSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightSideSplitContainer.Location = new System.Drawing.Point(0, 0);
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
            this.rightSideSplitContainer.Size = new System.Drawing.Size(986, 675);
            this.rightSideSplitContainer.SplitterDistance = 504;
            this.rightSideSplitContainer.TabIndex = 0;
            // 
            // bottomSplitContainer
            // 
            this.bottomSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.bottomSplitContainer.Name = "bottomSplitContainer";
            // 
            // bottomSplitContainer.Panel1
            // 
            this.bottomSplitContainer.Panel1.Controls.Add(this.playbackGroupBox);
            this.bottomSplitContainer.Panel1MinSize = 300;
            // 
            // bottomSplitContainer.Panel2
            // 
            this.bottomSplitContainer.Panel2.Controls.Add(this.routingGroupBox);
            this.bottomSplitContainer.Panel2MinSize = 300;
            this.bottomSplitContainer.Size = new System.Drawing.Size(986, 167);
            this.bottomSplitContainer.SplitterDistance = 503;
            this.bottomSplitContainer.TabIndex = 0;
            // 
            // playbackGroupBox
            // 
            this.playbackGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playbackGroupBox.Location = new System.Drawing.Point(0, 0);
            this.playbackGroupBox.Name = "playbackGroupBox";
            this.playbackGroupBox.Size = new System.Drawing.Size(503, 167);
            this.playbackGroupBox.TabIndex = 2;
            this.playbackGroupBox.TabStop = false;
            this.playbackGroupBox.Text = "Playback:";
            // 
            // routingGroupBox
            // 
            this.routingGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.routingGroupBox.Location = new System.Drawing.Point(0, 0);
            this.routingGroupBox.Name = "routingGroupBox";
            this.routingGroupBox.Size = new System.Drawing.Size(479, 167);
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
            this.menuStrip1.Size = new System.Drawing.Size(1482, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openAudioToolStripMenuItem,
            this.saveAudioToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openAudioToolStripMenuItem
            // 
            this.openAudioToolStripMenuItem.Name = "openAudioToolStripMenuItem";
            this.openAudioToolStripMenuItem.Size = new System.Drawing.Size(170, 26);
            this.openAudioToolStripMenuItem.Text = "Open audio";
            // 
            // saveAudioToolStripMenuItem
            // 
            this.saveAudioToolStripMenuItem.Name = "saveAudioToolStripMenuItem";
            this.saveAudioToolStripMenuItem.Size = new System.Drawing.Size(170, 26);
            this.saveAudioToolStripMenuItem.Text = "Save audio";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1482, 703);
            this.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1500, 750);
            this.Name = "MainForm";
            this.Text = "Signal Simulator";
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.leftSideSplitContainer.Panel1.ResumeLayout(false);
            this.leftSideSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leftSideSplitContainer)).EndInit();
            this.leftSideSplitContainer.ResumeLayout(false);
            this.effectsGroupBox.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.rightSideSplitContainer.Panel1.ResumeLayout(false);
            this.rightSideSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rightSideSplitContainer)).EndInit();
            this.rightSideSplitContainer.ResumeLayout(false);
            this.bottomSplitContainer.Panel1.ResumeLayout(false);
            this.bottomSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bottomSplitContainer)).EndInit();
            this.bottomSplitContainer.ResumeLayout(false);
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
        private System.Windows.Forms.CheckedListBox effectsList;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openAudioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAudioToolStripMenuItem;
    }
}

