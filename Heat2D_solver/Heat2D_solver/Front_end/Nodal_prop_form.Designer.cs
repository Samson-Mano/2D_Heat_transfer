namespace Heat2D_solver.Front_end
{
    partial class Nodal_prop_form
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
            this.button_cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_heat_source = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_spec_temp = new System.Windows.Forms.TextBox();
            this.radioButton_heat_source = new System.Windows.Forms.RadioButton();
            this.radioButton_spec_temp = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_circle = new System.Windows.Forms.RadioButton();
            this.radioButton_square = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_ok
            // 
            this.button_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ok.Location = new System.Drawing.Point(153, 192);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(100, 37);
            this.button_ok.TabIndex = 0;
            this.button_ok.Text = "Ok";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cancel.Location = new System.Drawing.Point(259, 192);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(97, 37);
            this.button_cancel.TabIndex = 1;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nodal heat source q = ";
            // 
            // textBox_heat_source
            // 
            this.textBox_heat_source.Location = new System.Drawing.Point(261, 49);
            this.textBox_heat_source.Name = "textBox_heat_source";
            this.textBox_heat_source.Size = new System.Drawing.Size(95, 26);
            this.textBox_heat_source.TabIndex = 3;
            this.textBox_heat_source.Text = "0.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(232, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "Nodal specified temperature T =";
            // 
            // textBox_spec_temp
            // 
            this.textBox_spec_temp.Location = new System.Drawing.Point(261, 108);
            this.textBox_spec_temp.Name = "textBox_spec_temp";
            this.textBox_spec_temp.Size = new System.Drawing.Size(95, 26);
            this.textBox_spec_temp.TabIndex = 7;
            this.textBox_spec_temp.Text = "0.0";
            // 
            // radioButton_heat_source
            // 
            this.radioButton_heat_source.AutoSize = true;
            this.radioButton_heat_source.Checked = true;
            this.radioButton_heat_source.Location = new System.Drawing.Point(27, 26);
            this.radioButton_heat_source.Name = "radioButton_heat_source";
            this.radioButton_heat_source.Size = new System.Drawing.Size(152, 23);
            this.radioButton_heat_source.TabIndex = 8;
            this.radioButton_heat_source.TabStop = true;
            this.radioButton_heat_source.Text = "Apply heat source";
            this.radioButton_heat_source.UseVisualStyleBackColor = true;
            this.radioButton_heat_source.CheckedChanged += new System.EventHandler(this.radioButton_heat_source_CheckedChanged);
            // 
            // radioButton_spec_temp
            // 
            this.radioButton_spec_temp.AutoSize = true;
            this.radioButton_spec_temp.Location = new System.Drawing.Point(27, 85);
            this.radioButton_spec_temp.Name = "radioButton_spec_temp";
            this.radioButton_spec_temp.Size = new System.Drawing.Size(158, 23);
            this.radioButton_spec_temp.TabIndex = 9;
            this.radioButton_spec_temp.Text = "Apply temperature";
            this.radioButton_spec_temp.UseVisualStyleBackColor = true;
            this.radioButton_spec_temp.CheckedChanged += new System.EventHandler(this.radioButton_spec_temp_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_circle);
            this.groupBox1.Controls.Add(this.radioButton_square);
            this.groupBox1.Location = new System.Drawing.Point(9, 145);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(121, 84);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selection type";
            // 
            // radioButton_circle
            // 
            this.radioButton_circle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButton_circle.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton_circle.Location = new System.Drawing.Point(6, 52);
            this.radioButton_circle.Name = "radioButton_circle";
            this.radioButton_circle.Size = new System.Drawing.Size(68, 26);
            this.radioButton_circle.TabIndex = 7;
            this.radioButton_circle.Text = "Circle";
            this.radioButton_circle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton_circle.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.radioButton_circle.UseVisualStyleBackColor = true;
            this.radioButton_circle.CheckedChanged += new System.EventHandler(this.radioButton_circle_CheckedChanged);
            // 
            // radioButton_square
            // 
            this.radioButton_square.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButton_square.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton_square.Checked = true;
            this.radioButton_square.Location = new System.Drawing.Point(6, 20);
            this.radioButton_square.Name = "radioButton_square";
            this.radioButton_square.Size = new System.Drawing.Size(68, 26);
            this.radioButton_square.TabIndex = 6;
            this.radioButton_square.TabStop = true;
            this.radioButton_square.Text = "Square";
            this.radioButton_square.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.radioButton_square.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.radioButton_square.UseVisualStyleBackColor = true;
            this.radioButton_square.CheckedChanged += new System.EventHandler(this.radioButton_square_CheckedChanged);
            // 
            // Nodal_prop_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 241);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.radioButton_spec_temp);
            this.Controls.Add(this.radioButton_heat_source);
            this.Controls.Add(this.textBox_spec_temp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_heat_source);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(380, 280);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(380, 280);
            this.Name = "Nodal_prop_form";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Nodal Properties";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Nodal_prop_form_FormClosing);
            this.Load += new System.EventHandler(this.Nodal_prop_form_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_heat_source;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_spec_temp;
        private System.Windows.Forms.RadioButton radioButton_heat_source;
        private System.Windows.Forms.RadioButton radioButton_spec_temp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton_circle;
        private System.Windows.Forms.RadioButton radioButton_square;
    }
}