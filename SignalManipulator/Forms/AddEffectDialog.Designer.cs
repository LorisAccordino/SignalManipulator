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
            searchTxt = new TextBox();
            searchLbl = new Label();
            SuspendLayout();
            // 
            // treeViewEffects
            // 
            treeViewEffects.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            treeViewEffects.Location = new Point(12, 32);
            treeViewEffects.Name = "treeViewEffects";
            treeViewEffects.Size = new Size(560, 231);
            treeViewEffects.TabIndex = 0;
            treeViewEffects.NodeMouseHover += OnEffects_NodeMouseHover;
            treeViewEffects.AfterSelect += OnEffects_AfterSelect;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnAdd.Location = new Point(12, 269);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(560, 30);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Add effect";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += OnAdd_Click;
            // 
            // searchTxt
            // 
            searchTxt.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            searchTxt.Location = new Point(150, 3);
            searchTxt.Name = "searchTxt";
            searchTxt.PlaceholderText = "Search effects...";
            searchTxt.Size = new Size(422, 23);
            searchTxt.TabIndex = 2;
            // 
            // searchLbl
            // 
            searchLbl.AutoSize = true;
            searchLbl.Location = new Point(12, 6);
            searchLbl.Name = "searchLbl";
            searchLbl.Size = new Size(132, 15);
            searchLbl.TabIndex = 3;
            searchLbl.Text = "Search effects by name:";
            // 
            // AddEffectDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 311);
            Controls.Add(searchLbl);
            Controls.Add(searchTxt);
            Controls.Add(btnAdd);
            Controls.Add(treeViewEffects);
            MaximizeBox = false;
            MaximumSize = new Size(850, 450);
            MinimizeBox = false;
            MinimumSize = new Size(350, 250);
            Name = "AddEffectDialog";
            ShowIcon = false;
            Text = "Add effect";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TreeView treeViewEffects;
        private Button btnAdd;
        private TextBox searchTxt;
        private Label searchLbl;
    }
}