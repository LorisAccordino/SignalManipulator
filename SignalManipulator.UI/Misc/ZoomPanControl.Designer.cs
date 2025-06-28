namespace SignalManipulator.UI.Misc
{
    partial class ZoomPanControl
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
            this.panSlider = new SignalManipulator.UI.Components.Precision.PrecisionSlider();
            this.zoomSlider = new SignalManipulator.UI.Components.Precision.PrecisionSlider();
            this.SuspendLayout();
            // 
            // panSlider
            // 
            this.panSlider.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panSlider.Description = "Pan:   ";
            this.panSlider.Location = new System.Drawing.Point(3, 30);
            this.panSlider.Maximum = 1D;
            this.panSlider.Minimum = -1D;
            this.panSlider.Name = "panSlider";
            this.panSlider.Size = new System.Drawing.Size(455, 30);
            this.panSlider.Suffix = "   ";
            this.panSlider.TabIndex = 7;
            this.panSlider.TickFrequency = 0;
            this.panSlider.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            // 
            // zoomSlider
            // 
            this.zoomSlider.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomSlider.Curvature = 2.5D;
            this.zoomSlider.Description = "Zoom:";
            this.zoomSlider.Location = new System.Drawing.Point(3, 0);
            this.zoomSlider.Maximum = 100D;
            this.zoomSlider.Minimum = 1D;
            this.zoomSlider.Name = "zoomSlider";
            this.zoomSlider.Precision = 0.1D;
            this.zoomSlider.PrecisionScale = SignalManipulator.UI.Components.Precision.PrecisionScale.Logarithmic;
            this.zoomSlider.Size = new System.Drawing.Size(455, 30);
            this.zoomSlider.Suffix = "x";
            this.zoomSlider.TabIndex = 5;
            this.zoomSlider.TickFrequency = 100;
            this.zoomSlider.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.zoomSlider.UpdateMode = SignalManipulator.UI.Components.Precision.ValueUpdateMode.UserOnly;
            this.zoomSlider.Value = 1D;
            // 
            // ZoomPanControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panSlider);
            this.Controls.Add(this.zoomSlider);
            this.Name = "ZoomPanControl";
            this.Size = new System.Drawing.Size(461, 60);
            this.ResumeLayout(false);

        }

        #endregion

        private Components.Precision.PrecisionSlider zoomSlider;
        private Components.Precision.PrecisionSlider panSlider;
    }
}
