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
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            settingsPanel = new System.Windows.Forms.TableLayoutPanel();
            groupBox2 = new System.Windows.Forms.GroupBox();
            smaLbl = new System.Windows.Forms.Label();
            emaNum = new System.Windows.Forms.NumericUpDown();
            emaLbl = new System.Windows.Forms.Label();
            smaNum = new System.Windows.Forms.NumericUpDown();
            navigator = new Misc.ZoomPanControl();
            tableLayoutPanel1.SuspendLayout();
            settingsPanel.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)emaNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)smaNum).BeginInit();
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
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(formsPlot, 0, 0);
            tableLayoutPanel1.Controls.Add(settingsPanel, 0, 1);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            tableLayoutPanel1.Size = new System.Drawing.Size(834, 351);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // settingsPanel
            // 
            settingsPanel.ColumnCount = 2;
            settingsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            settingsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            settingsPanel.Controls.Add(groupBox2, 1, 0);
            settingsPanel.Controls.Add(navigator, 0, 0);
            settingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            settingsPanel.Location = new System.Drawing.Point(4, 267);
            settingsPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            settingsPanel.Name = "settingsPanel";
            settingsPanel.RowCount = 1;
            settingsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            settingsPanel.Size = new System.Drawing.Size(826, 81);
            settingsPanel.TabIndex = 2;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(smaLbl);
            groupBox2.Controls.Add(emaNum);
            groupBox2.Controls.Add(emaLbl);
            groupBox2.Controls.Add(smaNum);
            groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox2.Location = new System.Drawing.Point(689, 3);
            groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox2.Size = new System.Drawing.Size(133, 75);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Smoothing:";
            // 
            // smaLbl
            // 
            smaLbl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            smaLbl.AutoSize = true;
            smaLbl.Location = new System.Drawing.Point(4, 20);
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
            emaNum.Location = new System.Drawing.Point(70, 44);
            emaNum.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            emaNum.Maximum = new decimal(new int[] { 85, 0, 0, 131072 });
            emaNum.Name = "emaNum";
            emaNum.Size = new System.Drawing.Size(56, 23);
            emaNum.TabIndex = 9;
            // 
            // emaLbl
            // 
            emaLbl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            emaLbl.AutoSize = true;
            emaLbl.Location = new System.Drawing.Point(4, 47);
            emaLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            emaLbl.Name = "emaLbl";
            emaLbl.Size = new System.Drawing.Size(52, 15);
            emaLbl.TabIndex = 7;
            emaLbl.Text = "EMA (F):";
            // 
            // smaNum
            // 
            smaNum.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            smaNum.Location = new System.Drawing.Point(70, 16);
            smaNum.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            smaNum.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            smaNum.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            smaNum.Name = "smaNum";
            smaNum.Size = new System.Drawing.Size(56, 23);
            smaNum.TabIndex = 5;
            smaNum.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // navigator
            // 
            navigator.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            navigator.Location = new System.Drawing.Point(5, 17);
            navigator.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            navigator.Name = "navigator";
            navigator.Pan = -1D;
            navigator.Size = new System.Drawing.Size(675, 61);
            navigator.TabIndex = 1;
            navigator.Zoom = 1D;
            navigator.ZoomMax = 20D;
            navigator.ZoomMin = 1D;
            navigator.ZoomPrecision = 0.01D;
            // 
            // SpectrumViewerControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "SpectrumViewerControl";
            Size = new System.Drawing.Size(834, 351);
            tableLayoutPanel1.ResumeLayout(false);
            settingsPanel.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)emaNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)smaNum).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot formsPlot;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Misc.ZoomPanControl navigator;
        private System.Windows.Forms.TableLayoutPanel settingsPanel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label smaLbl;
        private System.Windows.Forms.NumericUpDown emaNum;
        private System.Windows.Forms.Label emaLbl;
        private System.Windows.Forms.NumericUpDown smaNum;
    }
}
