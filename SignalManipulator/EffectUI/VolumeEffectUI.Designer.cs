namespace SignalManipulator.EffectUI
{
    partial class VolumeEffectUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            volumeSlider = new UI.Components.Sliders.Precision.PrecisionSlider();
            warningLbl = new Label();
            SuspendLayout();
            // 
            // volumeSlider
            // 
            volumeSlider.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            volumeSlider.Curvature = 2D;
            volumeSlider.Description = "Volume:";
            volumeSlider.Location = new Point(12, 12);
            volumeSlider.Maximum = 2D;
            volumeSlider.Name = "volumeSlider";
            volumeSlider.Precision = 0.001D;
            volumeSlider.PrecisionScale = UI.Components.Sliders.Precision.PrecisionScale.Logarithmic;
            volumeSlider.Size = new Size(295, 30);
            volumeSlider.TabIndex = 0;
            volumeSlider.TickFrequency = 1;
            volumeSlider.TickStyle = TickStyle.None;
            volumeSlider.Value = 1D;
            // 
            // warningLbl
            // 
            warningLbl.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            warningLbl.Location = new Point(12, 36);
            warningLbl.Name = "warningLbl";
            warningLbl.Size = new Size(295, 23);
            warningLbl.TabIndex = 1;
            warningLbl.Text = "Warning: Excessive volumes may cause audio clipping!";
            warningLbl.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // VolumeEffectUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(319, 65);
            Controls.Add(warningLbl);
            Controls.Add(volumeSlider);
            Name = "VolumeEffectUI";
            Text = "Volume Settings";
            ResumeLayout(false);
        }

        #endregion

        private UI.Components.Sliders.Precision.PrecisionSlider volumeSlider;
        private Label warningLbl;
    }
}