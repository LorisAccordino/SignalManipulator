namespace SignalManipulator.UI.Controls.Viewers
{
    partial class WaveformViewer
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
            formsPlot = new ScottPlot.WinForms.FormsPlot();
            mainPanelTableLayout = new System.Windows.Forms.TableLayoutPanel();
            settingsPanel = new System.Windows.Forms.TableLayoutPanel();
            navigator = new Misc.ZoomPanControl();
            displaySettingsGroupBox = new System.Windows.Forms.GroupBox();
            secNum = new System.Windows.Forms.NumericUpDown();
            stereoSplitRadBtn = new System.Windows.Forms.RadioButton();
            stereoMixRadBtn = new System.Windows.Forms.RadioButton();
            secLbl = new System.Windows.Forms.Label();
            mainPanelTableLayout.SuspendLayout();
            settingsPanel.SuspendLayout();
            displaySettingsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)secNum).BeginInit();
            SuspendLayout();
            // 
            // formsPlot
            // 
            formsPlot.DisplayScale = 0F;
            formsPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            formsPlot.Location = new System.Drawing.Point(2, 2);
            formsPlot.Margin = new System.Windows.Forms.Padding(2);
            formsPlot.Name = "formsPlot";
            formsPlot.Size = new System.Drawing.Size(682, 283);
            formsPlot.TabIndex = 0;
            // 
            // mainPanelTableLayout
            // 
            mainPanelTableLayout.ColumnCount = 1;
            mainPanelTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            mainPanelTableLayout.Controls.Add(formsPlot, 0, 0);
            mainPanelTableLayout.Controls.Add(settingsPanel, 0, 1);
            mainPanelTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            mainPanelTableLayout.Location = new System.Drawing.Point(0, 0);
            mainPanelTableLayout.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mainPanelTableLayout.Name = "mainPanelTableLayout";
            mainPanelTableLayout.RowCount = 2;
            mainPanelTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            mainPanelTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            mainPanelTableLayout.Size = new System.Drawing.Size(686, 374);
            mainPanelTableLayout.TabIndex = 1;
            // 
            // settingsPanel
            // 
            settingsPanel.ColumnCount = 2;
            settingsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            settingsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            settingsPanel.Controls.Add(navigator, 0, 0);
            settingsPanel.Controls.Add(displaySettingsGroupBox, 1, 0);
            settingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            settingsPanel.Location = new System.Drawing.Point(3, 290);
            settingsPanel.Name = "settingsPanel";
            settingsPanel.RowCount = 1;
            settingsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            settingsPanel.Size = new System.Drawing.Size(680, 81);
            settingsPanel.TabIndex = 1;
            // 
            // navigator
            // 
            navigator.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            navigator.BackColor = System.Drawing.SystemColors.Control;
            navigator.Location = new System.Drawing.Point(5, 17);
            navigator.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            navigator.Name = "navigator";
            navigator.Pan = 0D;
            navigator.Size = new System.Drawing.Size(490, 61);
            navigator.TabIndex = 11;
            navigator.Zoom = 1D;
            navigator.ZoomMax = 100D;
            navigator.ZoomMin = 1D;
            navigator.ZoomPrecision = 0.1D;
            // 
            // displaySettingsGroupBox
            // 
            displaySettingsGroupBox.Controls.Add(secNum);
            displaySettingsGroupBox.Controls.Add(stereoSplitRadBtn);
            displaySettingsGroupBox.Controls.Add(stereoMixRadBtn);
            displaySettingsGroupBox.Controls.Add(secLbl);
            displaySettingsGroupBox.Location = new System.Drawing.Point(503, 3);
            displaySettingsGroupBox.Name = "displaySettingsGroupBox";
            displaySettingsGroupBox.Size = new System.Drawing.Size(173, 75);
            displaySettingsGroupBox.TabIndex = 12;
            displaySettingsGroupBox.TabStop = false;
            displaySettingsGroupBox.Text = "Display settings:";
            // 
            // secNum
            // 
            secNum.Location = new System.Drawing.Point(107, 42);
            secNum.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            secNum.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            secNum.Name = "secNum";
            secNum.Size = new System.Drawing.Size(58, 23);
            secNum.TabIndex = 11;
            secNum.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // stereoSplitRadBtn
            // 
            stereoSplitRadBtn.AutoSize = true;
            stereoSplitRadBtn.Location = new System.Drawing.Point(85, 18);
            stereoSplitRadBtn.Name = "stereoSplitRadBtn";
            stereoSplitRadBtn.Size = new System.Drawing.Size(83, 19);
            stereoSplitRadBtn.TabIndex = 10;
            stereoSplitRadBtn.Text = "Stereo split";
            stereoSplitRadBtn.UseVisualStyleBackColor = true;
            // 
            // stereoMixRadBtn
            // 
            stereoMixRadBtn.AutoSize = true;
            stereoMixRadBtn.Checked = true;
            stereoMixRadBtn.Location = new System.Drawing.Point(5, 18);
            stereoMixRadBtn.Name = "stereoMixRadBtn";
            stereoMixRadBtn.Size = new System.Drawing.Size(80, 19);
            stereoMixRadBtn.TabIndex = 9;
            stereoMixRadBtn.TabStop = true;
            stereoMixRadBtn.Text = "Stereo mix";
            stereoMixRadBtn.UseVisualStyleBackColor = true;
            // 
            // secLbl
            // 
            secLbl.AutoSize = true;
            secLbl.Location = new System.Drawing.Point(4, 46);
            secLbl.Name = "secLbl";
            secLbl.Size = new System.Drawing.Size(100, 15);
            secLbl.TabIndex = 7;
            secLbl.Text = "Window seconds:";
            // 
            // WaveformViewer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(mainPanelTableLayout);
            Margin = new System.Windows.Forms.Padding(2);
            Name = "WaveformViewer";
            Size = new System.Drawing.Size(686, 374);
            Text = "Waveform Viewer";
            mainPanelTableLayout.ResumeLayout(false);
            settingsPanel.ResumeLayout(false);
            displaySettingsGroupBox.ResumeLayout(false);
            displaySettingsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)secNum).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot formsPlot;
        private System.Windows.Forms.TableLayoutPanel mainPanelTableLayout;
        private System.Windows.Forms.TableLayoutPanel settingsPanel;
        private Misc.ZoomPanControl navigator;
        private System.Windows.Forms.GroupBox displaySettingsGroupBox;
        private System.Windows.Forms.RadioButton stereoSplitRadBtn;
        private System.Windows.Forms.RadioButton stereoMixRadBtn;
        private System.Windows.Forms.Label secLbl;
        private System.Windows.Forms.NumericUpDown secNum;
    }
}
