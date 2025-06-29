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
            this.devicesLbl.Location = new System.Drawing.Point(16, 17);
            this.devicesLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.devicesLbl.Name = "devicesLbl";
            this.devicesLbl.Size = new System.Drawing.Size(118, 16);
            this.devicesLbl.TabIndex = 0;
            this.devicesLbl.Text = "Avalaible devices:";
            // 
            // devicesCmbx
            // 
            this.devicesCmbx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.devicesCmbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.devicesCmbx.FormattingEnabled = true;
            this.devicesCmbx.Location = new System.Drawing.Point(148, 14);
            this.devicesCmbx.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.devicesCmbx.Name = "devicesCmbx";
            this.devicesCmbx.Size = new System.Drawing.Size(312, 24);
            this.devicesCmbx.TabIndex = 1;
            this.devicesCmbx.SelectedIndexChanged += new System.EventHandler(this.devicesCmbx_SelectedIndexChanged);
            // 
            // AudioRouterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.devicesCmbx);
            this.Controls.Add(this.devicesLbl);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AudioRouterControl";
            this.Size = new System.Drawing.Size(471, 106);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label devicesLbl;
        private System.Windows.Forms.ComboBox devicesCmbx;
    }
}
