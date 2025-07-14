namespace SignalManipulator.EffectUI
{
    partial class EchoEffectUI
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
            delaySlider = new UI.Components.Sliders.Precision.PrecisionSlider();
            feedbackSlider = new UI.Components.Sliders.Precision.PrecisionSlider();
            wetMix = new UI.Components.Sliders.Precision.PrecisionSlider();
            dryMix = new UI.Components.Sliders.Precision.PrecisionSlider();
            SuspendLayout();
            // 
            // delaySlider
            // 
            delaySlider.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            delaySlider.Description = "Delay (ms):";
            delaySlider.Location = new Point(12, 12);
            delaySlider.Maximum = 2000D;
            delaySlider.Minimum = 50D;
            delaySlider.Name = "delaySlider";
            delaySlider.Precision = 1D;
            delaySlider.Size = new Size(361, 30);
            delaySlider.Suffix = " ms";
            delaySlider.TabIndex = 0;
            delaySlider.TickFrequency = 1;
            delaySlider.TickStyle = TickStyle.None;
            delaySlider.Value = 300D;
            // 
            // feedbackSlider
            // 
            feedbackSlider.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            feedbackSlider.Description = "Feedback:  ";
            feedbackSlider.Location = new Point(12, 35);
            feedbackSlider.Maximum = 1D;
            feedbackSlider.Name = "feedbackSlider";
            feedbackSlider.Size = new Size(335, 30);
            feedbackSlider.TabIndex = 1;
            feedbackSlider.TickFrequency = 1;
            feedbackSlider.TickStyle = TickStyle.None;
            feedbackSlider.Value = 0.5D;
            // 
            // wetMix
            // 
            wetMix.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            wetMix.Description = "Wet mix:    ";
            wetMix.Location = new Point(12, 62);
            wetMix.Maximum = 1D;
            wetMix.Name = "wetMix";
            wetMix.Size = new Size(335, 30);
            wetMix.TabIndex = 2;
            wetMix.TickFrequency = 1;
            wetMix.TickStyle = TickStyle.None;
            wetMix.Value = 0.5D;
            // 
            // dryMix
            // 
            dryMix.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dryMix.Description = "Dry mix:     ";
            dryMix.Location = new Point(12, 90);
            dryMix.Maximum = 1D;
            dryMix.Name = "dryMix";
            dryMix.Size = new Size(335, 30);
            dryMix.TabIndex = 3;
            dryMix.TickFrequency = 1;
            dryMix.TickStyle = TickStyle.None;
            dryMix.Value = 1D;
            // 
            // EchoEffectUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(371, 123);
            Controls.Add(dryMix);
            Controls.Add(wetMix);
            Controls.Add(feedbackSlider);
            Controls.Add(delaySlider);
            Name = "EchoEffectUI";
            Text = "Echo Settings";
            ResumeLayout(false);
        }

        #endregion

        private UI.Components.Sliders.Precision.PrecisionSlider delaySlider;
        private UI.Components.Sliders.Precision.PrecisionSlider feedbackSlider;
        private UI.Components.Sliders.Precision.PrecisionSlider wetMix;
        private UI.Components.Sliders.Precision.PrecisionSlider dryMix;
    }
}