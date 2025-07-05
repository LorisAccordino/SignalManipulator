namespace SignalManipulator.UI.Controls.Viewers
{
    partial class WaveformViewerControl
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
            settingsPanel = new System.Windows.Forms.Panel();
            navigator = new Misc.ZoomPanControl();
            secLbl = new System.Windows.Forms.Label();
            secNum = new System.Windows.Forms.NumericUpDown();
            monoCheckBox = new System.Windows.Forms.CheckBox();
            zoomLbl = new System.Windows.Forms.Label();
            tableLayoutPanel1.SuspendLayout();
            settingsPanel.SuspendLayout();
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
            formsPlot.Size = new System.Drawing.Size(649, 295);
            formsPlot.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(settingsPanel, 0, 1);
            tableLayoutPanel1.Controls.Add(formsPlot, 0, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            tableLayoutPanel1.Size = new System.Drawing.Size(653, 374);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // settingsPanel
            // 
            settingsPanel.BackColor = System.Drawing.SystemColors.Control;
            settingsPanel.Controls.Add(navigator);
            settingsPanel.Controls.Add(secLbl);
            settingsPanel.Controls.Add(secNum);
            settingsPanel.Controls.Add(monoCheckBox);
            settingsPanel.Controls.Add(zoomLbl);
            settingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            settingsPanel.Location = new System.Drawing.Point(4, 302);
            settingsPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            settingsPanel.Name = "settingsPanel";
            settingsPanel.Size = new System.Drawing.Size(645, 69);
            settingsPanel.TabIndex = 2;
            // 
            // navigator
            // 
            navigator.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            navigator.BackColor = System.Drawing.SystemColors.Control;
            navigator.Location = new System.Drawing.Point(1, 5);
            navigator.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            navigator.Name = "navigator";
            navigator.Pan = 0D;
            navigator.Size = new System.Drawing.Size(532, 61);
            navigator.TabIndex = 10;
            navigator.Zoom = 1D;
            navigator.ZoomMax = 100D;
            navigator.ZoomMin = 1D;
            navigator.ZoomPrecision = 0.1D;
            // 
            // secLbl
            // 
            secLbl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            secLbl.AutoSize = true;
            secLbl.Location = new System.Drawing.Point(530, 35);
            secLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            secLbl.Name = "secLbl";
            secLbl.Size = new System.Drawing.Size(54, 15);
            secLbl.TabIndex = 8;
            secLbl.Text = "Seconds:";
            // 
            // secNum
            // 
            secNum.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            secNum.Location = new System.Drawing.Point(591, 31);
            secNum.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            secNum.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            secNum.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            secNum.Name = "secNum";
            secNum.Size = new System.Drawing.Size(51, 23);
            secNum.TabIndex = 7;
            secNum.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // monoCheckBox
            // 
            monoCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            monoCheckBox.AutoSize = true;
            monoCheckBox.Location = new System.Drawing.Point(542, 8);
            monoCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            monoCheckBox.Name = "monoCheckBox";
            monoCheckBox.Size = new System.Drawing.Size(99, 19);
            monoCheckBox.TabIndex = 5;
            monoCheckBox.Text = "Split channels";
            monoCheckBox.UseVisualStyleBackColor = true;
            // 
            // zoomLbl
            // 
            zoomLbl.AutoSize = true;
            zoomLbl.Location = new System.Drawing.Point(14, 13);
            zoomLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            zoomLbl.Name = "zoomLbl";
            zoomLbl.Size = new System.Drawing.Size(0, 15);
            zoomLbl.TabIndex = 1;
            // 
            // WaveformViewerControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Margin = new System.Windows.Forms.Padding(2);
            Name = "WaveformViewerControl";
            Size = new System.Drawing.Size(653, 374);
            tableLayoutPanel1.ResumeLayout(false);
            settingsPanel.ResumeLayout(false);
            settingsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)secNum).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot formsPlot;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label zoomLbl;
        private System.Windows.Forms.Panel settingsPanel;
        private System.Windows.Forms.CheckBox monoCheckBox;
        private System.Windows.Forms.NumericUpDown secNum;
        private System.Windows.Forms.Label secLbl;
        private Misc.ZoomPanControl navigator;
    }
}
