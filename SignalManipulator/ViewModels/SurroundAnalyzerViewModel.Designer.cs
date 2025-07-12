namespace SignalManipulator.ViewModels
{
    partial class SurroundAnalyzerViewModel
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
            SuspendLayout();
            // 
            // formsPlot
            // 
            formsPlot.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            formsPlot.DisplayScale = 0F;
            formsPlot.Location = new System.Drawing.Point(107, 0);
            formsPlot.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            formsPlot.Name = "formsPlot";
            formsPlot.Size = new System.Drawing.Size(468, 366);
            formsPlot.TabIndex = 0;
            // 
            // SurroundAnalyzerViewer
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(formsPlot);
            IsSquaredControl = true;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "SurroundAnalyzerViewer";
            Size = new System.Drawing.Size(680, 370);
            Text = "Surround Analyzer";
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot formsPlot;
    }
}
