namespace SignalManipulator.Controls
{
    partial class EffectChainControl
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
            this.panel = new System.Windows.Forms.TableLayoutPanel();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.removeEffectButton = new System.Windows.Forms.Button();
            this.addEffectButton = new System.Windows.Forms.Button();
            this.effectList = new System.Windows.Forms.CheckedListBox();
            this.panel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.ColumnCount = 1;
            this.panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel.Controls.Add(this.buttonPanel, 0, 1);
            this.panel.Controls.Add(this.effectList, 0, 0);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.RowCount = 2;
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.panel.Size = new System.Drawing.Size(800, 450);
            this.panel.TabIndex = 1;
            // 
            // buttonPanel
            // 
            this.buttonPanel.AutoScroll = true;
            this.buttonPanel.Controls.Add(this.removeEffectButton);
            this.buttonPanel.Controls.Add(this.addEffectButton);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPanel.Location = new System.Drawing.Point(3, 418);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(794, 29);
            this.buttonPanel.TabIndex = 3;
            // 
            // removeEffectButton
            // 
            this.removeEffectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeEffectButton.Location = new System.Drawing.Point(93, 1);
            this.removeEffectButton.Name = "removeEffectButton";
            this.removeEffectButton.Size = new System.Drawing.Size(85, 25);
            this.removeEffectButton.TabIndex = 2;
            this.removeEffectButton.Text = "Remove";
            this.removeEffectButton.UseVisualStyleBackColor = true;
            this.removeEffectButton.Click += new System.EventHandler(this.OnRemoveEffect);
            // 
            // addEffectButton
            // 
            this.addEffectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addEffectButton.Location = new System.Drawing.Point(3, 1);
            this.addEffectButton.Name = "addEffectButton";
            this.addEffectButton.Size = new System.Drawing.Size(84, 25);
            this.addEffectButton.TabIndex = 1;
            this.addEffectButton.Text = "Add";
            this.addEffectButton.UseVisualStyleBackColor = true;
            this.addEffectButton.Click += new System.EventHandler(this.OnAddEffect);
            // 
            // effectList
            // 
            this.effectList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.effectList.FormattingEnabled = true;
            this.effectList.Location = new System.Drawing.Point(3, 3);
            this.effectList.Name = "effectList";
            this.effectList.Size = new System.Drawing.Size(794, 409);
            this.effectList.TabIndex = 0;
            // 
            // EffectChainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.Name = "EffectChainControl";
            this.Size = new System.Drawing.Size(800, 450);
            this.panel.ResumeLayout(false);
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel panel;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button removeEffectButton;
        private System.Windows.Forms.Button addEffectButton;
        private System.Windows.Forms.CheckedListBox effectList;
    }
}
