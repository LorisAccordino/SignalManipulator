namespace SignalManipulator.UI.Controls.User
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
            devicesLbl = new System.Windows.Forms.Label();
            devicesCmbx = new System.Windows.Forms.ComboBox();
            SuspendLayout();
            // 
            // devicesLbl
            // 
            devicesLbl.AutoSize = true;
            devicesLbl.Location = new System.Drawing.Point(14, 16);
            devicesLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            devicesLbl.Name = "devicesLbl";
            devicesLbl.Size = new System.Drawing.Size(100, 15);
            devicesLbl.TabIndex = 0;
            devicesLbl.Text = "Avalaible devices:";
            // 
            // devicesCmbx
            // 
            devicesCmbx.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            devicesCmbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            devicesCmbx.FormattingEnabled = true;
            devicesCmbx.Location = new System.Drawing.Point(130, 13);
            devicesCmbx.Margin = new System.Windows.Forms.Padding(4);
            devicesCmbx.Name = "devicesCmbx";
            devicesCmbx.Size = new System.Drawing.Size(274, 23);
            devicesCmbx.TabIndex = 1;
            devicesCmbx.SelectedIndexChanged += devicesCmbx_SelectedIndexChanged;
            // 
            // AudioRouterControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(devicesCmbx);
            Controls.Add(devicesLbl);
            Margin = new System.Windows.Forms.Padding(4);
            Name = "AudioRouterControl";
            Size = new System.Drawing.Size(412, 99);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label devicesLbl;
        private System.Windows.Forms.ComboBox devicesCmbx;
    }
}
