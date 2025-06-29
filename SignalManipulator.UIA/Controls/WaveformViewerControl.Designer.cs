namespace SignalManipulator.UI.Controls
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
            this.formsPlot = new ScottPlot.WinForms.FormsPlot();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.navigatorControl = new SignalManipulator.UI.Misc.ZoomPanControl();
            this.secLbl = new System.Windows.Forms.Label();
            this.secNum = new System.Windows.Forms.NumericUpDown();
            this.monoCheckBox = new System.Windows.Forms.CheckBox();
            this.zoomLbl = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.secNum)).BeginInit();
            this.SuspendLayout();
            // 
            // formsPlot
            // 
            this.formsPlot.DisplayScale = 0F;
            this.formsPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlot.Location = new System.Drawing.Point(2, 2);
            this.formsPlot.Margin = new System.Windows.Forms.Padding(2);
            this.formsPlot.Name = "formsPlot";
            this.formsPlot.Size = new System.Drawing.Size(556, 255);
            this.formsPlot.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.formsPlot, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(560, 324);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.navigatorControl);
            this.panel1.Controls.Add(this.secLbl);
            this.panel1.Controls.Add(this.secNum);
            this.panel1.Controls.Add(this.monoCheckBox);
            this.panel1.Controls.Add(this.zoomLbl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 262);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(554, 59);
            this.panel1.TabIndex = 2;
            // 
            // navigatorControl
            // 
            this.navigatorControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.navigatorControl.BackColor = System.Drawing.SystemColors.Control;
            this.navigatorControl.Location = new System.Drawing.Point(3, 3);
            this.navigatorControl.Name = "navigatorControl";
            this.navigatorControl.Size = new System.Drawing.Size(450, 53);
            this.navigatorControl.TabIndex = 9;
            // 
            // secLbl
            // 
            this.secLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.secLbl.AutoSize = true;
            this.secLbl.Location = new System.Drawing.Point(455, 30);
            this.secLbl.Name = "secLbl";
            this.secLbl.Size = new System.Drawing.Size(52, 13);
            this.secLbl.TabIndex = 8;
            this.secLbl.Text = "Seconds:";
            // 
            // secNum
            // 
            this.secNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.secNum.Location = new System.Drawing.Point(507, 27);
            this.secNum.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.secNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.secNum.Name = "secNum";
            this.secNum.Size = new System.Drawing.Size(44, 20);
            this.secNum.TabIndex = 7;
            this.secNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // monoCheckBox
            // 
            this.monoCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.monoCheckBox.AutoSize = true;
            this.monoCheckBox.Location = new System.Drawing.Point(459, 7);
            this.monoCheckBox.Name = "monoCheckBox";
            this.monoCheckBox.Size = new System.Drawing.Size(92, 17);
            this.monoCheckBox.TabIndex = 5;
            this.monoCheckBox.Text = "Split channels";
            this.monoCheckBox.UseVisualStyleBackColor = true;
            // 
            // zoomLbl
            // 
            this.zoomLbl.AutoSize = true;
            this.zoomLbl.Location = new System.Drawing.Point(12, 11);
            this.zoomLbl.Name = "zoomLbl";
            this.zoomLbl.Size = new System.Drawing.Size(0, 13);
            this.zoomLbl.TabIndex = 1;
            // 
            // WaveformViewerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "WaveformViewerControl";
            this.Size = new System.Drawing.Size(560, 324);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.secNum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ScottPlot.WinForms.FormsPlot formsPlot;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label zoomLbl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox monoCheckBox;
        private System.Windows.Forms.NumericUpDown secNum;
        private System.Windows.Forms.Label secLbl;
        private Misc.ZoomPanControl navigatorControl;
    }
}
