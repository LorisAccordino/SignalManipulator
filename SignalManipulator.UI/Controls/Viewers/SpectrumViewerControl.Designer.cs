namespace SignalManipulator.UI.Controls.Viewers
{
    partial class SpectrumViewerControl
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
            smoothingGroupBox = new System.Windows.Forms.GroupBox();
            smaLbl = new System.Windows.Forms.Label();
            emaNum = new System.Windows.Forms.NumericUpDown();
            emaLbl = new System.Windows.Forms.Label();
            smaNum = new System.Windows.Forms.NumericUpDown();
            displaySettingsGroupBox = new System.Windows.Forms.GroupBox();
            stereoSplitRadBtn = new System.Windows.Forms.RadioButton();
            stereoMixRadBtn = new System.Windows.Forms.RadioButton();
            fftSizeCmbx = new System.Windows.Forms.ComboBox();
            fftSizeLbl = new System.Windows.Forms.Label();
            mainPanelTableLayout.SuspendLayout();
            settingsPanel.SuspendLayout();
            smoothingGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)emaNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)smaNum).BeginInit();
            displaySettingsGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // formsPlot
            // 
            formsPlot.DisplayScale = 0F;
            formsPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            formsPlot.Location = new System.Drawing.Point(4, 3);
            formsPlot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            formsPlot.Name = "formsPlot";
            formsPlot.Size = new System.Drawing.Size(826, 258);
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
            mainPanelTableLayout.Size = new System.Drawing.Size(834, 351);
            mainPanelTableLayout.TabIndex = 1;
            // 
            // settingsPanel
            // 
            settingsPanel.ColumnCount = 3;
            settingsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            settingsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            settingsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            settingsPanel.Controls.Add(navigator, 0, 0);
            settingsPanel.Controls.Add(smoothingGroupBox, 2, 0);
            settingsPanel.Controls.Add(displaySettingsGroupBox, 1, 0);
            settingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            settingsPanel.Location = new System.Drawing.Point(4, 267);
            settingsPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            settingsPanel.Name = "settingsPanel";
            settingsPanel.RowCount = 1;
            settingsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            settingsPanel.Size = new System.Drawing.Size(826, 81);
            settingsPanel.TabIndex = 2;
            // 
            // navigator
            // 
            navigator.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            navigator.Location = new System.Drawing.Point(5, 17);
            navigator.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            navigator.Name = "navigator";
            navigator.Pan = -1D;
            navigator.Size = new System.Drawing.Size(506, 61);
            navigator.TabIndex = 1;
            navigator.Zoom = 1D;
            navigator.ZoomMax = 20D;
            navigator.ZoomMin = 1D;
            navigator.ZoomPrecision = 0.01D;
            // 
            // smoothingGroupBox
            // 
            smoothingGroupBox.Controls.Add(smaLbl);
            smoothingGroupBox.Controls.Add(emaNum);
            smoothingGroupBox.Controls.Add(emaLbl);
            smoothingGroupBox.Controls.Add(smaNum);
            smoothingGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            smoothingGroupBox.Location = new System.Drawing.Point(700, 3);
            smoothingGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            smoothingGroupBox.Name = "smoothingGroupBox";
            smoothingGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            smoothingGroupBox.Size = new System.Drawing.Size(122, 75);
            smoothingGroupBox.TabIndex = 6;
            smoothingGroupBox.TabStop = false;
            smoothingGroupBox.Text = "Smoothing:";
            // 
            // smaLbl
            // 
            smaLbl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            smaLbl.AutoSize = true;
            smaLbl.Location = new System.Drawing.Point(7, 21);
            smaLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            smaLbl.Name = "smaLbl";
            smaLbl.Size = new System.Drawing.Size(55, 15);
            smaLbl.TabIndex = 11;
            smaLbl.Text = "SMA (N):";
            // 
            // emaNum
            // 
            emaNum.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            emaNum.DecimalPlaces = 2;
            emaNum.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            emaNum.Location = new System.Drawing.Point(65, 46);
            emaNum.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            emaNum.Maximum = new decimal(new int[] { 85, 0, 0, 131072 });
            emaNum.Name = "emaNum";
            emaNum.Size = new System.Drawing.Size(51, 23);
            emaNum.TabIndex = 9;
            // 
            // emaLbl
            // 
            emaLbl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            emaLbl.AutoSize = true;
            emaLbl.Location = new System.Drawing.Point(8, 49);
            emaLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            emaLbl.Name = "emaLbl";
            emaLbl.Size = new System.Drawing.Size(52, 15);
            emaLbl.TabIndex = 7;
            emaLbl.Text = "EMA (F):";
            // 
            // smaNum
            // 
            smaNum.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            smaNum.Location = new System.Drawing.Point(65, 18);
            smaNum.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            smaNum.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            smaNum.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            smaNum.Name = "smaNum";
            smaNum.Size = new System.Drawing.Size(51, 23);
            smaNum.TabIndex = 5;
            smaNum.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // displaySettingsGroupBox
            // 
            displaySettingsGroupBox.Controls.Add(stereoSplitRadBtn);
            displaySettingsGroupBox.Controls.Add(stereoMixRadBtn);
            displaySettingsGroupBox.Controls.Add(fftSizeCmbx);
            displaySettingsGroupBox.Controls.Add(fftSizeLbl);
            displaySettingsGroupBox.Location = new System.Drawing.Point(519, 3);
            displaySettingsGroupBox.Name = "displaySettingsGroupBox";
            displaySettingsGroupBox.Size = new System.Drawing.Size(173, 75);
            displaySettingsGroupBox.TabIndex = 7;
            displaySettingsGroupBox.TabStop = false;
            displaySettingsGroupBox.Text = "Display settings:";
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
            // fftSizeCmbx
            // 
            fftSizeCmbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            fftSizeCmbx.FormattingEnabled = true;
            fftSizeCmbx.Items.AddRange(new object[] { "256", "512", "1024", "2048", "4096", "8192", "16384", "32768" });
            fftSizeCmbx.Location = new System.Drawing.Point(53, 43);
            fftSizeCmbx.Name = "fftSizeCmbx";
            fftSizeCmbx.Size = new System.Drawing.Size(115, 23);
            fftSizeCmbx.TabIndex = 8;
            // 
            // fftSizeLbl
            // 
            fftSizeLbl.AutoSize = true;
            fftSizeLbl.Location = new System.Drawing.Point(3, 46);
            fftSizeLbl.Name = "fftSizeLbl";
            fftSizeLbl.Size = new System.Drawing.Size(51, 15);
            fftSizeLbl.TabIndex = 7;
            fftSizeLbl.Text = "FFT size:";
            // 
            // SpectrumViewerControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(mainPanelTableLayout);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "SpectrumViewerControl";
            Size = new System.Drawing.Size(834, 351);
            mainPanelTableLayout.ResumeLayout(false);
            settingsPanel.ResumeLayout(false);
            smoothingGroupBox.ResumeLayout(false);
            smoothingGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)emaNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)smaNum).EndInit();
            displaySettingsGroupBox.ResumeLayout(false);
            displaySettingsGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot formsPlot;
        private System.Windows.Forms.TableLayoutPanel mainPanelTableLayout;
        private Misc.ZoomPanControl navigator;
        private System.Windows.Forms.TableLayoutPanel settingsPanel;
        private System.Windows.Forms.GroupBox smoothingGroupBox;
        private System.Windows.Forms.Label smaLbl;
        private System.Windows.Forms.NumericUpDown emaNum;
        private System.Windows.Forms.Label emaLbl;
        private System.Windows.Forms.NumericUpDown smaNum;
        private System.Windows.Forms.GroupBox displaySettingsGroupBox;
        private System.Windows.Forms.Label fftSizeLbl;
        private System.Windows.Forms.ComboBox fftSizeCmbx;
        private System.Windows.Forms.RadioButton stereoMixRadBtn;
        private System.Windows.Forms.RadioButton stereoSplitRadBtn;
    }
}
