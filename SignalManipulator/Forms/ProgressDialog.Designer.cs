namespace SignalManipulator.Forms
{
    partial class ProgressDialog
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
            progressBar = new ProgressBar();
            percentLbl = new Label();
            abortBtn = new Button();
            SuspendLayout();
            // 
            // progressBar
            // 
            progressBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBar.Location = new Point(12, 13);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(297, 28);
            progressBar.TabIndex = 0;
            // 
            // percentLbl
            // 
            percentLbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            percentLbl.AutoSize = true;
            percentLbl.Location = new Point(315, 20);
            percentLbl.Name = "percentLbl";
            percentLbl.Size = new Size(23, 15);
            percentLbl.TabIndex = 1;
            percentLbl.Text = "0%";
            // 
            // abortBtn
            // 
            abortBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            abortBtn.Location = new Point(349, 14);
            abortBtn.Name = "abortBtn";
            abortBtn.Size = new Size(54, 26);
            abortBtn.TabIndex = 2;
            abortBtn.Text = "Abort";
            abortBtn.UseVisualStyleBackColor = true;
            abortBtn.Click += abortBtn_Click;
            // 
            // ProgressDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(411, 55);
            ControlBox = false;
            Controls.Add(abortBtn);
            Controls.Add(percentLbl);
            Controls.Add(progressBar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ProgressDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Progress...";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ProgressBar progressBar;
        private Label percentLbl;
        private Button abortBtn;
    }
}