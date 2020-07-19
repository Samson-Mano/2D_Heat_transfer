namespace Heat2D_solver
{
    partial class Main_form
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_ZoomValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.main_pic = new System.Windows.Forms.Panel();
            this.mt_pic = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preProcessingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importMeshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nodalPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edgePropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.elementPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.finiteElementSolveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeLastLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllLabelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.main_pic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mt_pic)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_ZoomValue});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_ZoomValue
            // 
            this.toolStripStatusLabel_ZoomValue.Name = "toolStripStatusLabel_ZoomValue";
            this.toolStripStatusLabel_ZoomValue.Size = new System.Drawing.Size(0, 17);
            // 
            // main_pic
            // 
            this.main_pic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.main_pic.BackColor = System.Drawing.Color.White;
            this.main_pic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.main_pic.Controls.Add(this.mt_pic);
            this.main_pic.Location = new System.Drawing.Point(12, 27);
            this.main_pic.Name = "main_pic";
            this.main_pic.Size = new System.Drawing.Size(760, 509);
            this.main_pic.TabIndex = 4;
            this.main_pic.KeyUp += new System.Windows.Forms.KeyEventHandler(this.main_pic_KeyUp);
            this.main_pic.KeyDown += new System.Windows.Forms.KeyEventHandler(this.main_pic_KeyDown);
            this.main_pic.Paint += new System.Windows.Forms.PaintEventHandler(this.main_pic_Paint);
            this.main_pic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.main_pic_MouseClick);
            this.main_pic.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.main_pic_MouseDoubleClick);
            this.main_pic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.main_pic_MouseDown);
            this.main_pic.MouseEnter += new System.EventHandler(this.main_pic_MouseEnter);
            this.main_pic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.main_pic_MouseMove);
            this.main_pic.MouseUp += new System.Windows.Forms.MouseEventHandler(this.main_pic_MouseUp);
            this.main_pic.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.main_pic_MouseWheel);
            this.main_pic.Resize += new System.EventHandler(this.main_pic_Resize);
            // 
            // mt_pic
            // 
            this.mt_pic.BackColor = System.Drawing.Color.Transparent;
            this.mt_pic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mt_pic.Enabled = false;
            this.mt_pic.Location = new System.Drawing.Point(0, 0);
            this.mt_pic.Name = "mt_pic";
            this.mt_pic.Size = new System.Drawing.Size(756, 505);
            this.mt_pic.TabIndex = 0;
            this.mt_pic.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.preProcessingToolStripMenuItem,
            this.solveToolStripMenuItem,
            this.resultToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // preProcessingToolStripMenuItem
            // 
            this.preProcessingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importMeshToolStripMenuItem,
            this.nodalPropertiesToolStripMenuItem,
            this.edgePropertiesToolStripMenuItem,
            this.elementPropertiesToolStripMenuItem});
            this.preProcessingToolStripMenuItem.Name = "preProcessingToolStripMenuItem";
            this.preProcessingToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.preProcessingToolStripMenuItem.Text = "Pre-Processing";
            // 
            // importMeshToolStripMenuItem
            // 
            this.importMeshToolStripMenuItem.Name = "importMeshToolStripMenuItem";
            this.importMeshToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.importMeshToolStripMenuItem.Text = "Import Mesh";
            this.importMeshToolStripMenuItem.Click += new System.EventHandler(this.importMeshToolStripMenuItem_Click);
            // 
            // nodalPropertiesToolStripMenuItem
            // 
            this.nodalPropertiesToolStripMenuItem.Name = "nodalPropertiesToolStripMenuItem";
            this.nodalPropertiesToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.nodalPropertiesToolStripMenuItem.Text = "Nodal Properties";
            this.nodalPropertiesToolStripMenuItem.Click += new System.EventHandler(this.nodalPropertiesToolStripMenuItem_Click);
            // 
            // edgePropertiesToolStripMenuItem
            // 
            this.edgePropertiesToolStripMenuItem.Name = "edgePropertiesToolStripMenuItem";
            this.edgePropertiesToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.edgePropertiesToolStripMenuItem.Text = "Edge Properties";
            this.edgePropertiesToolStripMenuItem.Click += new System.EventHandler(this.edgePropertiesToolStripMenuItem_Click);
            // 
            // elementPropertiesToolStripMenuItem
            // 
            this.elementPropertiesToolStripMenuItem.Name = "elementPropertiesToolStripMenuItem";
            this.elementPropertiesToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.elementPropertiesToolStripMenuItem.Text = "Element Properties";
            this.elementPropertiesToolStripMenuItem.Click += new System.EventHandler(this.elementPropertiesToolStripMenuItem_Click);
            // 
            // solveToolStripMenuItem
            // 
            this.solveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.finiteElementSolveToolStripMenuItem});
            this.solveToolStripMenuItem.Name = "solveToolStripMenuItem";
            this.solveToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.solveToolStripMenuItem.Text = "Solve";
            // 
            // finiteElementSolveToolStripMenuItem
            // 
            this.finiteElementSolveToolStripMenuItem.Name = "finiteElementSolveToolStripMenuItem";
            this.finiteElementSolveToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.finiteElementSolveToolStripMenuItem.Text = "Finite Element solve";
            this.finiteElementSolveToolStripMenuItem.Click += new System.EventHandler(this.finiteElementSolveToolStripMenuItem_Click);
            // 
            // resultToolStripMenuItem
            // 
            this.resultToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showResultsToolStripMenuItem,
            this.removeLastLabelToolStripMenuItem,
            this.removeAllLabelsToolStripMenuItem});
            this.resultToolStripMenuItem.Name = "resultToolStripMenuItem";
            this.resultToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.resultToolStripMenuItem.Text = "Result";
            // 
            // showResultsToolStripMenuItem
            // 
            this.showResultsToolStripMenuItem.Checked = true;
            this.showResultsToolStripMenuItem.CheckOnClick = true;
            this.showResultsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showResultsToolStripMenuItem.Name = "showResultsToolStripMenuItem";
            this.showResultsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.showResultsToolStripMenuItem.Text = "Show Results";
            this.showResultsToolStripMenuItem.Click += new System.EventHandler(this.showResultsToolStripMenuItem_Click);
            // 
            // removeLastLabelToolStripMenuItem
            // 
            this.removeLastLabelToolStripMenuItem.Name = "removeLastLabelToolStripMenuItem";
            this.removeLastLabelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.removeLastLabelToolStripMenuItem.Text = "Remove last label";
            this.removeLastLabelToolStripMenuItem.Click += new System.EventHandler(this.removeLastLabelToolStripMenuItem_Click);
            // 
            // removeAllLabelsToolStripMenuItem
            // 
            this.removeAllLabelsToolStripMenuItem.Name = "removeAllLabelsToolStripMenuItem";
            this.removeAllLabelsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.removeAllLabelsToolStripMenuItem.Text = "Remove all labels";
            this.removeAllLabelsToolStripMenuItem.Click += new System.EventHandler(this.removeAllLabelsToolStripMenuItem_Click);
            // 
            // Main_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.main_pic);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Main_form";
            this.Text = "2D Heat Equation Solver ----- Developed by Samson Mano <https://sites.goolge.com/" +
    "site/samsoninfinite/>";
            this.Load += new System.EventHandler(this.Main_form_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.main_pic.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mt_pic)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel main_pic;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preProcessingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importMeshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nodalPropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem edgePropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem elementPropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_ZoomValue;
        public System.Windows.Forms.PictureBox mt_pic;
        private System.Windows.Forms.ToolStripMenuItem solveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem finiteElementSolveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resultToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showResultsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeLastLabelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAllLabelsToolStripMenuItem;
    }
}