namespace SignalManipulator.UI.Controls
{
    partial class LissajousViewerControl
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
            this.SuspendLayout();
            // 
            // formsPlot
            // 
            this.formsPlot.DisplayScale = 0F;
            this.formsPlot.Location = new System.Drawing.Point(18, 13);
            this.formsPlot.Name = "formsPlot";
            this.formsPlot.Size = new System.Drawing.Size(406, 225);
            this.formsPlot.TabIndex = 0;
            // 
            // LissajousViewerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.formsPlot);
            this.Name = "LissajousViewerControl";
            this.Size = new System.Drawing.Size(461, 253);
            this.ResumeLayout(false);

        }

        #endregion

        private ScottPlot.WinForms.FormsPlot formsPlot;
    }
}
