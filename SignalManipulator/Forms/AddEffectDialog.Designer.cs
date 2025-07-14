namespace SignalManipulator.Forms
{
    partial class AddEffectDialog
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
            treeViewEffects = new TreeView();
            btnAdd = new Button();
            SuspendLayout();
            // 
            // treeViewEffects
            // 
            treeViewEffects.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            treeViewEffects.Location = new Point(12, 12);
            treeViewEffects.Name = "treeViewEffects";
            treeViewEffects.Size = new Size(510, 201);
            treeViewEffects.TabIndex = 0;
            treeViewEffects.NodeMouseHover += treeViewEffects_NodeMouseHover;
            treeViewEffects.AfterSelect += treeViewEffects_AfterSelect;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnAdd.Location = new Point(12, 219);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(510, 30);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Add effect";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // AddEffectDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(534, 261);
            Controls.Add(btnAdd);
            Controls.Add(treeViewEffects);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MaximumSize = new Size(850, 450);
            MinimizeBox = false;
            MinimumSize = new Size(350, 250);
            Name = "AddEffectDialog";
            Text = "Add effect";
            ResumeLayout(false);
        }

        #endregion

        private TreeView treeViewEffects;
        private Button btnAdd;
    }
}