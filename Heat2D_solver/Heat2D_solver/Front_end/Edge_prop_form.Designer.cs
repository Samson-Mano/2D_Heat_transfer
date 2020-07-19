namespace Heat2D_solver.Front_end
{
    partial class Edge_prop_form
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_heat_source = new System.Windows.Forms.TextBox();
            this.textBox_specified_temp = new System.Windows.Forms.TextBox();
            this.textBox_heat_transfer_coeff = new System.Windows.Forms.TextBox();
            this.textBox_ambient_temp = new System.Windows.Forms.TextBox();
            this.radioButton_heat_source = new System.Windows.Forms.RadioButton();
            this.radioButton_spec_temp = new System.Windows.Forms.RadioButton();
            this.radioButton_heat_convection = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_circle = new System.Windows.Forms.RadioButton();
            this.radioButton_square = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_ok
            // 
            this.button_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ok.Location = new System.Drawing.Point(133, 292);
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
            this.button_cancel.Location = new System.Drawing.Point(239, 292);
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
            this.label1.Location = new System.Drawing.Point(73, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "Edge heat source q = ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 19);
            this.label2.TabIndex = 7;
            this.label2.Text = "Specified temperature T =";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(207, 19);
            this.label3.TabIndex = 8;
            this.label3.Text = "Heat transfer co-efficient h =";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(208, 19);
            this.label4.TabIndex = 9;
            this.label4.Text = "Ambient temperature T_inf =";
            // 
            // textBox_heat_source
            // 
            this.textBox_heat_source.Location = new System.Drawing.Point(237, 51);
            this.textBox_heat_source.Name = "textBox_heat_source";
            this.textBox_heat_source.Size = new System.Drawing.Size(95, 26);
            this.textBox_heat_source.TabIndex = 10;
            this.textBox_heat_source.Text = "0.0";
            // 
            // textBox_specified_temp
            // 
            this.textBox_specified_temp.Location = new System.Drawing.Point(237, 111);
            this.textBox_specified_temp.Name = "textBox_specified_temp";
            this.textBox_specified_temp.Size = new System.Drawing.Size(95, 26);
            this.textBox_specified_temp.TabIndex = 11;
            this.textBox_specified_temp.Text = "0.0";
            // 
            // textBox_heat_transfer_coeff
            // 
            this.textBox_heat_transfer_coeff.Location = new System.Drawing.Point(237, 171);
            this.textBox_heat_transfer_coeff.Name = "textBox_heat_transfer_coeff";
            this.textBox_heat_transfer_coeff.Size = new System.Drawing.Size(95, 26);
            this.textBox_heat_transfer_coeff.TabIndex = 12;
            this.textBox_heat_transfer_coeff.Text = "0.0";
            // 
            // textBox_ambient_temp
            // 
            this.textBox_ambient_temp.Location = new System.Drawing.Point(237, 203);
            this.textBox_ambient_temp.Name = "textBox_ambient_temp";
            this.textBox_ambient_temp.Size = new System.Drawing.Size(95, 26);
            this.textBox_ambient_temp.TabIndex = 13;
            this.textBox_ambient_temp.Text = "0.0";
            // 
            // radioButton_heat_source
            // 
            this.radioButton_heat_source.AutoSize = true;
            this.radioButton_heat_source.Checked = true;
            this.radioButton_heat_source.Location = new System.Drawing.Point(27, 26);
            this.radioButton_heat_source.Name = "radioButton_heat_source";
            this.radioButton_heat_source.Size = new System.Drawing.Size(152, 23);
            this.radioButton_heat_source.TabIndex = 14;
            this.radioButton_heat_source.TabStop = true;
            this.radioButton_heat_source.Text = "Apply heat source";
            this.radioButton_heat_source.UseVisualStyleBackColor = true;
            this.radioButton_heat_source.CheckedChanged += new System.EventHandler(this.radioButton_heat_source_CheckedChanged);
            // 
            // radioButton_spec_temp
            // 
            this.radioButton_spec_temp.AutoSize = true;
            this.radioButton_spec_temp.Location = new System.Drawing.Point(26, 86);
            this.radioButton_spec_temp.Name = "radioButton_spec_temp";
            this.radioButton_spec_temp.Size = new System.Drawing.Size(158, 23);
            this.radioButton_spec_temp.TabIndex = 15;
            this.radioButton_spec_temp.TabStop = true;
            this.radioButton_spec_temp.Text = "Apply temperature";
            this.radioButton_spec_temp.UseVisualStyleBackColor = true;
            this.radioButton_spec_temp.CheckedChanged += new System.EventHandler(this.radioButton_spec_temp_CheckedChanged);
            // 
            // radioButton_heat_convection
            // 
            this.radioButton_heat_convection.AutoSize = true;
            this.radioButton_heat_convection.Location = new System.Drawing.Point(28, 146);
            this.radioButton_heat_convection.Name = "radioButton_heat_convection";
            this.radioButton_heat_convection.Size = new System.Drawing.Size(146, 23);
            this.radioButton_heat_convection.TabIndex = 16;
            this.radioButton_heat_convection.TabStop = true;
            this.radioButton_heat_convection.Text = "Apply convection";
            this.radioButton_heat_convection.UseVisualStyleBackColor = true;
            this.radioButton_heat_convection.CheckedChanged += new System.EventHandler(this.radioButton_heat_convection_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_circle);
            this.groupBox1.Controls.Add(this.radioButton_square);
            this.groupBox1.Location = new System.Drawing.Point(3, 239);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(124, 90);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selection type";
            // 
            // radioButton_circle
            // 
            this.radioButton_circle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButton_circle.Appearance = System.Windows.Forms.Appearance.Button;
            this.radioButton_circle.Location = new System.Drawing.Point(9, 57);
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
            this.radioButton_square.Location = new System.Drawing.Point(9, 25);
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
            // Edge_prop_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 341);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.radioButton_heat_convection);
            this.Controls.Add(this.radioButton_spec_temp);
            this.Controls.Add(this.radioButton_heat_source);
            this.Controls.Add(this.textBox_ambient_temp);
            this.Controls.Add(this.textBox_heat_transfer_coeff);
            this.Controls.Add(this.textBox_specified_temp);
            this.Controls.Add(this.textBox_heat_source);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(360, 380);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(360, 380);
            this.Name = "Edge_prop_form";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Edge Properties";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Edge_prop_form_FormClosing);
            this.Load += new System.EventHandler(this.Edge_prop_form_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_heat_source;
        private System.Windows.Forms.TextBox textBox_specified_temp;
        private System.Windows.Forms.TextBox textBox_heat_transfer_coeff;
        private System.Windows.Forms.TextBox textBox_ambient_temp;
        private System.Windows.Forms.RadioButton radioButton_heat_source;
        private System.Windows.Forms.RadioButton radioButton_spec_temp;
        private System.Windows.Forms.RadioButton radioButton_heat_convection;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton_circle;
        private System.Windows.Forms.RadioButton radioButton_square;
    }
}