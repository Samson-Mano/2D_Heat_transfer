namespace Heat2D_solver.Front_end
{
    partial class view_settings_form
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
            this.button_ok = new System.Windows.Forms.Button();
            this.checkBox_show_nodal_prop = new System.Windows.Forms.CheckBox();
            this.checkBox_show_edge_prop = new System.Windows.Forms.CheckBox();
            this.checkBox_show_element_prop = new System.Windows.Forms.CheckBox();
            this.checkBox_show_values = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_contour_interval = new System.Windows.Forms.TextBox();
            this.checkBox_show_result_vectors = new System.Windows.Forms.CheckBox();
            this.checkBox_show_contour_lines = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button_ok
            // 
            this.button_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ok.Location = new System.Drawing.Point(93, 242);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(100, 37);
            this.button_ok.TabIndex = 0;
            this.button_ok.Text = "Ok";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // checkBox_show_nodal_prop
            // 
            this.checkBox_show_nodal_prop.AutoSize = true;
            this.checkBox_show_nodal_prop.Location = new System.Drawing.Point(12, 12);
            this.checkBox_show_nodal_prop.Name = "checkBox_show_nodal_prop";
            this.checkBox_show_nodal_prop.Size = new System.Drawing.Size(186, 23);
            this.checkBox_show_nodal_prop.TabIndex = 2;
            this.checkBox_show_nodal_prop.Text = "Show nodal properties";
            this.checkBox_show_nodal_prop.UseVisualStyleBackColor = true;
            this.checkBox_show_nodal_prop.CheckedChanged += new System.EventHandler(this.checkBox_show_nodal_prop_CheckedChanged);
            // 
            // checkBox_show_edge_prop
            // 
            this.checkBox_show_edge_prop.AutoSize = true;
            this.checkBox_show_edge_prop.Location = new System.Drawing.Point(12, 41);
            this.checkBox_show_edge_prop.Name = "checkBox_show_edge_prop";
            this.checkBox_show_edge_prop.Size = new System.Drawing.Size(180, 23);
            this.checkBox_show_edge_prop.TabIndex = 3;
            this.checkBox_show_edge_prop.Text = "Show edge properties";
            this.checkBox_show_edge_prop.UseVisualStyleBackColor = true;
            this.checkBox_show_edge_prop.CheckedChanged += new System.EventHandler(this.checkBox_show_edge_prop_CheckedChanged);
            // 
            // checkBox_show_element_prop
            // 
            this.checkBox_show_element_prop.AutoSize = true;
            this.checkBox_show_element_prop.Location = new System.Drawing.Point(12, 70);
            this.checkBox_show_element_prop.Name = "checkBox_show_element_prop";
            this.checkBox_show_element_prop.Size = new System.Drawing.Size(202, 23);
            this.checkBox_show_element_prop.TabIndex = 4;
            this.checkBox_show_element_prop.Text = "Show element properties";
            this.checkBox_show_element_prop.UseVisualStyleBackColor = true;
            this.checkBox_show_element_prop.CheckedChanged += new System.EventHandler(this.checkBox_show_element_prop_CheckedChanged);
            // 
            // checkBox_show_values
            // 
            this.checkBox_show_values.AutoSize = true;
            this.checkBox_show_values.Location = new System.Drawing.Point(12, 99);
            this.checkBox_show_values.Name = "checkBox_show_values";
            this.checkBox_show_values.Size = new System.Drawing.Size(114, 23);
            this.checkBox_show_values.TabIndex = 5;
            this.checkBox_show_values.Text = "Show values";
            this.checkBox_show_values.UseVisualStyleBackColor = true;
            this.checkBox_show_values.CheckedChanged += new System.EventHandler(this.checkBox_show_values_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 203);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "Contour interval :";
            // 
            // textBox_contour_interval
            // 
            this.textBox_contour_interval.Location = new System.Drawing.Point(149, 200);
            this.textBox_contour_interval.Name = "textBox_contour_interval";
            this.textBox_contour_interval.Size = new System.Drawing.Size(65, 26);
            this.textBox_contour_interval.TabIndex = 7;
            this.textBox_contour_interval.Text = "10";
            // 
            // checkBox_show_result_vectors
            // 
            this.checkBox_show_result_vectors.AutoSize = true;
            this.checkBox_show_result_vectors.Enabled = false;
            this.checkBox_show_result_vectors.Location = new System.Drawing.Point(12, 128);
            this.checkBox_show_result_vectors.Name = "checkBox_show_result_vectors";
            this.checkBox_show_result_vectors.Size = new System.Drawing.Size(189, 23);
            this.checkBox_show_result_vectors.TabIndex = 8;
            this.checkBox_show_result_vectors.Text = "Show heat flow vectors";
            this.checkBox_show_result_vectors.UseVisualStyleBackColor = true;
            this.checkBox_show_result_vectors.CheckedChanged += new System.EventHandler(this.checkBox_show_result_vectors_CheckedChanged);
            // 
            // checkBox_show_contour_lines
            // 
            this.checkBox_show_contour_lines.AutoSize = true;
            this.checkBox_show_contour_lines.Location = new System.Drawing.Point(12, 157);
            this.checkBox_show_contour_lines.Name = "checkBox_show_contour_lines";
            this.checkBox_show_contour_lines.Size = new System.Drawing.Size(161, 23);
            this.checkBox_show_contour_lines.TabIndex = 9;
            this.checkBox_show_contour_lines.Text = "Show contour lines";
            this.checkBox_show_contour_lines.UseVisualStyleBackColor = true;
            this.checkBox_show_contour_lines.CheckedChanged += new System.EventHandler(this.checkBox_show_contour_lines_CheckedChanged);
            // 
            // view_settings_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 291);
            this.Controls.Add(this.checkBox_show_contour_lines);
            this.Controls.Add(this.checkBox_show_result_vectors);
            this.Controls.Add(this.textBox_contour_interval);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox_show_values);
            this.Controls.Add(this.checkBox_show_element_prop);
            this.Controls.Add(this.checkBox_show_edge_prop);
            this.Controls.Add(this.checkBox_show_nodal_prop);
            this.Controls.Add(this.button_ok);
            this.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 330);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 330);
            this.Name = "view_settings_form";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "View settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.view_settings_form_FormClosing);
            this.Load += new System.EventHandler(this.view_settings_form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.CheckBox checkBox_show_nodal_prop;
        private System.Windows.Forms.CheckBox checkBox_show_edge_prop;
        private System.Windows.Forms.CheckBox checkBox_show_element_prop;
        private System.Windows.Forms.CheckBox checkBox_show_values;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_contour_interval;
        private System.Windows.Forms.CheckBox checkBox_show_result_vectors;
        private System.Windows.Forms.CheckBox checkBox_show_contour_lines;
    }
}