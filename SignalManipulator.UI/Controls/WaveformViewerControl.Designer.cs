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
            this.monoCheckBox = new System.Windows.Forms.CheckBox();
            this.zoomLbl = new System.Windows.Forms.Label();
            this.zoomSlider = new SignalManipulator.UI.Components.Precision.PrecisionSlider();
            this.panSlider = new SignalManipulator.UI.Components.Precision.PrecisionSlider();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // formsPlot
            // 
            this.formsPlot.DisplayScale = 0F;
            this.formsPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlot.Location = new System.Drawing.Point(2, 2);
            this.formsPlot.Margin = new System.Windows.Forms.Padding(2);
            this.formsPlot.Name = "formsPlot";
            this.formsPlot.Size = new System.Drawing.Size(556, 226);
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(560, 324);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panSlider);
            this.panel1.Controls.Add(this.monoCheckBox);
            this.panel1.Controls.Add(this.zoomSlider);
            this.panel1.Controls.Add(this.zoomLbl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 233);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(554, 88);
            this.panel1.TabIndex = 2;
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
            // zoomSlider
            // 
            this.zoomSlider.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomSlider.Curvature = 2.5D;
            this.zoomSlider.Description = "Zoom:";
            this.zoomSlider.Location = new System.Drawing.Point(0, 3);
            this.zoomSlider.Maximum = 100D;
            this.zoomSlider.Minimum = 1D;
            this.zoomSlider.Name = "zoomSlider";
            this.zoomSlider.Precision = 0.1D;
            this.zoomSlider.PrecisionScale = SignalManipulator.UI.Components.Precision.PrecisionScale.Logarithmic;
            this.zoomSlider.Size = new System.Drawing.Size(453, 30);
            this.zoomSlider.Suffix = "x";
            this.zoomSlider.TabIndex = 4;
            this.zoomSlider.TickFrequency = 100;
            this.zoomSlider.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.zoomSlider.UpdateMode = SignalManipulator.UI.Components.Precision.ValueUpdateMode.UserOnly;
            this.zoomSlider.Value = 1D;
            // 
            // panSlider
            // 
            this.panSlider.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panSlider.Description = "Pan:   ";
            this.panSlider.Location = new System.Drawing.Point(-1, 28);
            this.panSlider.Maximum = 1D;
            this.panSlider.Minimum = -1D;
            this.panSlider.Name = "panSlider";
            this.panSlider.Size = new System.Drawing.Size(443, 30);
            this.panSlider.TabIndex = 6;
            this.panSlider.TickFrequency = 0;
            this.panSlider.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
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
            this.ResumeLayout(false);

        }

        #endregion

        private ScottPlot.WinForms.FormsPlot formsPlot;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label zoomLbl;
        private System.Windows.Forms.Panel panel1;
        private Components.Precision.PrecisionSlider zoomSlider;
        private System.Windows.Forms.CheckBox monoCheckBox;
        private Components.Precision.PrecisionSlider panSlider;
    }
}
