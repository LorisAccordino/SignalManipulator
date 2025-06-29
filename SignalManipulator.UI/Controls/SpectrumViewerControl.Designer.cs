namespace SignalManipulator.UI.Controls
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
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            groupBox2 = new System.Windows.Forms.GroupBox();
            smaLbl = new System.Windows.Forms.Label();
            emaNum = new System.Windows.Forms.NumericUpDown();
            emaLbl = new System.Windows.Forms.Label();
            smaNum = new System.Windows.Forms.NumericUpDown();
            navigatorControl = new Misc.ZoomPanControl();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
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
            formsPlot.Size = new System.Drawing.Size(621, 258);
            formsPlot.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(formsPlot, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            tableLayoutPanel1.Size = new System.Drawing.Size(629, 351);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.77778F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.2222214F));
            tableLayoutPanel2.Controls.Add(groupBox2, 1, 0);
            tableLayoutPanel2.Controls.Add(navigatorControl, 0, 0);
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel2.Location = new System.Drawing.Point(4, 267);
            tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new System.Drawing.Size(621, 81);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(smaLbl);
            groupBox2.Controls.Add(emaNum);
            groupBox2.Controls.Add(emaLbl);
            groupBox2.Controls.Add(smaNum);
            groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox2.Location = new System.Drawing.Point(487, 3);
            groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox2.Size = new System.Drawing.Size(130, 75);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Smoothing:";
            // 
            // smaLbl
            // 
            smaLbl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            smaLbl.AutoSize = true;
            smaLbl.Location = new System.Drawing.Point(5, 20);
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
            emaNum.Location = new System.Drawing.Point(71, 44);
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
            emaLbl.Location = new System.Drawing.Point(5, 47);
            emaLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            emaLbl.Name = "emaLbl";
            emaLbl.Size = new System.Drawing.Size(52, 15);
            emaLbl.TabIndex = 7;
            emaLbl.Text = "EMA (F):";
            // 
            // smaNum
            // 
            smaNum.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            smaNum.Location = new System.Drawing.Point(71, 16);
            smaNum.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            smaNum.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            smaNum.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            smaNum.Name = "smaNum";
            smaNum.Size = new System.Drawing.Size(56, 23);
            smaNum.TabIndex = 5;
            smaNum.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // navigatorControl
            // 
            navigatorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            navigatorControl.Location = new System.Drawing.Point(5, 3);
            navigatorControl.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            navigatorControl.Name = "navigatorControl";
            navigatorControl.Size = new System.Drawing.Size(473, 75);
            navigatorControl.TabIndex = 1;
            navigatorControl.ZoomMax = 20D;
            navigatorControl.ZoomMin = 1D;
            navigatorControl.ZoomPrecision = 0.01D;
            // 
            // SpectrumViewerControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "SpectrumViewerControl";
            Size = new System.Drawing.Size(629, 351);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)emaNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)smaNum).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot formsPlot;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Misc.ZoomPanControl navigatorControl;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label smaLbl;
        private System.Windows.Forms.NumericUpDown emaNum;
        private System.Windows.Forms.Label emaLbl;
        private System.Windows.Forms.NumericUpDown smaNum;
    }
}
