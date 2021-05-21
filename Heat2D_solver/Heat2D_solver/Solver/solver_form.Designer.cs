namespace Heat2D_solver.Front_end
{
    partial class solver_form
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
            this.button_solve = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_third_order = new System.Windows.Forms.RadioButton();
            this.radioButton_second_order = new System.Windows.Forms.RadioButton();
            this.radioButton_first_order = new System.Windows.Forms.RadioButton();
            this.richTextBox_AnalysisUpdate = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_solve
            // 
            this.button_solve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_solve.Location = new System.Drawing.Point(496, 407);
            this.button_solve.Name = "button_solve";
            this.button_solve.Size = new System.Drawing.Size(120, 37);
            this.button_solve.TabIndex = 0;
            this.button_solve.Text = "Solve";
            this.button_solve.UseVisualStyleBackColor = true;
            this.button_solve.Click += new System.EventHandler(this.button_solve_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_third_order);
            this.groupBox1.Controls.Add(this.radioButton_second_order);
            this.groupBox1.Controls.Add(this.radioButton_first_order);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(604, 79);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Element Type";
            // 
            // radioButton_third_order
            // 
            this.radioButton_third_order.AutoSize = true;
            this.radioButton_third_order.Enabled = false;
            this.radioButton_third_order.Location = new System.Drawing.Point(398, 34);
            this.radioButton_third_order.Name = "radioButton_third_order";
            this.radioButton_third_order.Size = new System.Drawing.Size(157, 23);
            this.radioButton_third_order.TabIndex = 2;
            this.radioButton_third_order.TabStop = true;
            this.radioButton_third_order.Text = "3rd Order Element";
            this.radioButton_third_order.UseVisualStyleBackColor = true;
            // 
            // radioButton_second_order
            // 
            this.radioButton_second_order.AutoSize = true;
            this.radioButton_second_order.Enabled = false;
            this.radioButton_second_order.Location = new System.Drawing.Point(205, 34);
            this.radioButton_second_order.Name = "radioButton_second_order";
            this.radioButton_second_order.Size = new System.Drawing.Size(159, 23);
            this.radioButton_second_order.TabIndex = 1;
            this.radioButton_second_order.Text = "2nd Order Element";
            this.radioButton_second_order.UseVisualStyleBackColor = true;
            // 
            // radioButton_first_order
            // 
            this.radioButton_first_order.AutoSize = true;
            this.radioButton_first_order.Checked = true;
            this.radioButton_first_order.Location = new System.Drawing.Point(25, 34);
            this.radioButton_first_order.Name = "radioButton_first_order";
            this.radioButton_first_order.Size = new System.Drawing.Size(153, 23);
            this.radioButton_first_order.TabIndex = 0;
            this.radioButton_first_order.TabStop = true;
            this.radioButton_first_order.Text = "1st Order Element";
            this.radioButton_first_order.UseVisualStyleBackColor = true;
            // 
            // richTextBox_AnalysisUpdate
            // 
            this.richTextBox_AnalysisUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_AnalysisUpdate.Location = new System.Drawing.Point(12, 97);
            this.richTextBox_AnalysisUpdate.Name = "richTextBox_AnalysisUpdate";
            this.richTextBox_AnalysisUpdate.ReadOnly = true;
            this.richTextBox_AnalysisUpdate.Size = new System.Drawing.Size(604, 301);
            this.richTextBox_AnalysisUpdate.TabIndex = 3;
            this.richTextBox_AnalysisUpdate.Text = "";
            // 
            // solver_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 456);
            this.Controls.Add(this.richTextBox_AnalysisUpdate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_solve);
            this.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(3600, 2500);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(360, 250);
            this.Name = "solver_form";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Finite Element Solver";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.solver_form_FormClosing);
            this.Load += new System.EventHandler(this.solver_form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_solve;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton_second_order;
        private System.Windows.Forms.RadioButton radioButton_first_order;
        private System.Windows.Forms.RadioButton radioButton_third_order;
        private System.Windows.Forms.RichTextBox richTextBox_AnalysisUpdate;
    }
}