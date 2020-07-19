using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Heat2D_solver.Data_structure;
using Heat2D_solver.Useful_Function;

namespace Heat2D_solver.Front_end
{
    public partial class Nodal_prop_form : Form
    {
        Main_form my_parent_form;
        pslg_datastructure fe_object;

        public Nodal_prop_form(Main_form t_form, ref pslg_datastructure t_fe_object)
        {
            InitializeComponent();
            my_parent_form = t_form;
            fe_object = t_fe_object;
        }

        private void Nodal_prop_form_Load(object sender, EventArgs e)
        {
            this.Location = new Point(my_parent_form.Location.X + 14, my_parent_form.Location.Y + 59);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Opacity = 0.9;
            this.BringToFront();
            this.TopMost = true;

            // Set the selection mode
            if (static_parameters.is_square_select == true)
            {
                radioButton_square.Checked = true;
            }
            else
            {
                radioButton_circle.Checked = true;
            }

            // set the load application type
            radioButton_heat_source.Checked = true;

            label1.Enabled = true;
            label2.Enabled = false;

            textBox_heat_source.Enabled = true;
            textBox_spec_temp.Enabled = false;
        }

        private void Nodal_prop_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            static_parameters.is_childFormOpen = false; // Inform the main form that this form is closed
            static_parameters.is_nodeFormOpen = false;
            fe_object.selection_index.Clear();
            my_parent_form.mt_pic.Refresh();
        }

        private void radioButton_square_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_square.Checked == true)
            {
                static_parameters.is_square_select = true;
            }
            else
            {
                static_parameters.is_square_select = false;
            }
        }

        private void radioButton_circle_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_circle.Checked == true)
            {
                static_parameters.is_square_select = false;
            }
            else
            {
                static_parameters.is_square_select = true;
            }
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            // Add the values to the Nodes
            if (radioButton_heat_source.Checked == true)
            {
                // Apply heat source to the node
                if (co_functions.Test_a_textboxvalue_validity(textBox_heat_source.Text, false, false) == true)
                {
                    fe_object.main_mesh.set_nodal_constraint(fe_object.selection_index,
                                                             co_functions.ConvertStringToDouble(textBox_heat_source.Text),0.0f);

                    my_parent_form.mt_pic.Refresh();
                }
            }
            else if (radioButton_spec_temp.Checked == true)
            {
                // Apply specified temperature to the node
                if (co_functions.Test_a_textboxvalue_validity(textBox_spec_temp.Text, false, false) == true)
                {
                    fe_object.main_mesh.set_nodal_constraint(fe_object.selection_index,
                                                             0.0f, co_functions.ConvertStringToDouble(textBox_spec_temp.Text));

                    my_parent_form.mt_pic.Refresh();
                }
            }
            
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            my_parent_form.mt_pic.Refresh();
            this.Close();
        }

        private void radioButton_heat_source_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_heat_source.Checked == true)
            {
                label1.Enabled = true;
                label2.Enabled = false;

                textBox_heat_source.Enabled = true;
                textBox_spec_temp.Enabled = false;
            }
        }

        private void radioButton_spec_temp_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_spec_temp.Checked == true)
            {
                label1.Enabled = false;
                label2.Enabled = true;

                textBox_heat_source.Enabled = false;
                textBox_spec_temp.Enabled = true;
            }
        }

    }
}
