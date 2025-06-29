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
            this.formsPlot = new ScottPlot.WinForms.FormsPlot();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.smaLbl = new System.Windows.Forms.Label();
            this.emaNum = new System.Windows.Forms.NumericUpDown();
            this.emaLbl = new System.Windows.Forms.Label();
            this.smaNum = new System.Windows.Forms.NumericUpDown();
            this.navigatorControl = new SignalManipulator.UI.Misc.ZoomPanControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.emaNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smaNum)).BeginInit();
            this.SuspendLayout();
            // 
            // formsPlot
            // 
            this.formsPlot.DisplayScale = 0F;
            this.formsPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlot.Location = new System.Drawing.Point(3, 3);
            this.formsPlot.Name = "formsPlot";
            this.formsPlot.Size = new System.Drawing.Size(533, 223);
            this.formsPlot.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.formsPlot, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(539, 304);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.48593F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.51407F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.navigatorControl, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 232);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(533, 69);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.smaLbl);
            this.groupBox2.Controls.Add(this.emaNum);
            this.groupBox2.Controls.Add(this.emaLbl);
            this.groupBox2.Controls.Add(this.smaNum);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(416, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(114, 63);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Smoothing:";
            // 
            // smaLbl
            // 
            this.smaLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.smaLbl.AutoSize = true;
            this.smaLbl.Location = new System.Drawing.Point(7, 17);
            this.smaLbl.Name = "smaLbl";
            this.smaLbl.Size = new System.Drawing.Size(50, 13);
            this.smaLbl.TabIndex = 11;
            this.smaLbl.Text = "SMA (N):";
            // 
            // emaNum
            // 
            this.emaNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.emaNum.DecimalPlaces = 2;
            this.emaNum.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.emaNum.Location = new System.Drawing.Point(63, 38);
            this.emaNum.Maximum = new decimal(new int[] {
            85,
            0,
            0,
            131072});
            this.emaNum.Name = "emaNum";
            this.emaNum.Size = new System.Drawing.Size(48, 20);
            this.emaNum.TabIndex = 9;
            // 
            // emaLbl
            // 
            this.emaLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.emaLbl.AutoSize = true;
            this.emaLbl.Location = new System.Drawing.Point(7, 41);
            this.emaLbl.Name = "emaLbl";
            this.emaLbl.Size = new System.Drawing.Size(48, 13);
            this.emaLbl.TabIndex = 7;
            this.emaLbl.Text = "EMA (F):";
            // 
            // smaNum
            // 
            this.smaNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.smaNum.Location = new System.Drawing.Point(63, 14);
            this.smaNum.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.smaNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.smaNum.Name = "smaNum";
            this.smaNum.Size = new System.Drawing.Size(48, 20);
            this.smaNum.TabIndex = 5;
            this.smaNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // navigatorControl
            // 
            this.navigatorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigatorControl.Location = new System.Drawing.Point(3, 3);
            this.navigatorControl.Name = "navigatorControl";
            this.navigatorControl.Size = new System.Drawing.Size(407, 63);
            this.navigatorControl.TabIndex = 1;
            this.navigatorControl.ZoomMax = 20D;
            this.navigatorControl.ZoomMin = 1D;
            this.navigatorControl.ZoomPrecision = 0.01D;
            // 
            // SpectrumViewerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SpectrumViewerControl";
            this.Size = new System.Drawing.Size(539, 304);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.emaNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smaNum)).EndInit();
            this.ResumeLayout(false);

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
