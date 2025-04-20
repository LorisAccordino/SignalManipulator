namespace SignalManipulator.UI.Controls
{
    partial class AudioRouterControl
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
            this.devicesLbl = new System.Windows.Forms.Label();
            this.devicesCmbx = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // devicesLbl
            // 
            this.devicesLbl.AutoSize = true;
            this.devicesLbl.Location = new System.Drawing.Point(12, 14);
            this.devicesLbl.Name = "devicesLbl";
            this.devicesLbl.Size = new System.Drawing.Size(93, 13);
            this.devicesLbl.TabIndex = 0;
            this.devicesLbl.Text = "Avalaible devices:";
            // 
            // devicesCmbx
            // 
            this.devicesCmbx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.devicesCmbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.devicesCmbx.FormattingEnabled = true;
            this.devicesCmbx.Location = new System.Drawing.Point(111, 11);
            this.devicesCmbx.Name = "devicesCmbx";
            this.devicesCmbx.Size = new System.Drawing.Size(235, 21);
            this.devicesCmbx.TabIndex = 1;
            this.devicesCmbx.SelectedIndexChanged += new System.EventHandler(this.devicesCmbx_SelectedIndexChanged);
            // 
            // AudioRouter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.devicesCmbx);
            this.Controls.Add(this.devicesLbl);
            this.Name = "AudioRouter";
            this.Size = new System.Drawing.Size(353, 86);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label devicesLbl;
        private System.Windows.Forms.ComboBox devicesCmbx;
    }
}
