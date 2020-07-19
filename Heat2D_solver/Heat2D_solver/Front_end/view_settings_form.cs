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
    public partial class view_settings_form : Form
    {
        Main_form my_parent_form;
        pslg_datastructure fe_object;
        // List<int> _tselected_nd_list = new List<int>();

        //public List<int> selected_nd_list
        //{
        //    get { return _tselected_nd_list; }
        //}

        public view_settings_form(Main_form t_form, ref pslg_datastructure t_fe_object)
        {
            InitializeComponent();
            my_parent_form = t_form;
            fe_object = t_fe_object;
        }

        private void view_settings_form_Load(object sender, EventArgs e)
        {
            this.Location = new Point(my_parent_form.Location.X + 14, my_parent_form.Location.Y + 59);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Opacity = 0.9;
            this.BringToFront();
            this.TopMost = true;

            // Set the settings selection
            checkBox_show_nodal_prop.Checked = static_parameters.show_node_values;
            checkBox_show_edge_prop.Checked = static_parameters.show_edge_values;
            checkBox_show_element_prop.Checked = static_parameters.show_element_values;
            checkBox_show_values.Checked = static_parameters.show_values;
            checkBox_show_result_vectors.Checked = static_parameters.show_result_vectors;
            checkBox_show_contour_lines.Checked = static_parameters.show_result_contours;

            textBox_contour_interval.Text = static_parameters.n_contour_intervals.ToString();
        }

        private void view_settings_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            static_parameters.is_childFormOpen = false; // Inform the main form that this form is closed
            static_parameters.is_viewsettingsform = false;
            fe_object.selection_index.Clear();
            my_parent_form.mt_pic.Refresh();
        }


        private void button_ok_Click(object sender, EventArgs e)
        {
            if (co_functions.Test_a_textboxvalue_validity(textBox_contour_interval.Text,true,false) == true)
            {
                static_parameters.n_contour_intervals = co_functions.ConvertStringToInt(textBox_contour_interval.Text);
            }

            // Add the view settings
            my_parent_form.mt_pic.Refresh();
            this.Close();
        }

        #region "View settings"
        private void checkBox_show_nodal_prop_CheckedChanged(object sender, EventArgs e)
        {
            static_parameters.show_node_values = checkBox_show_nodal_prop.Checked;
            my_parent_form.mt_pic.Refresh();
        }

        private void checkBox_show_edge_prop_CheckedChanged(object sender, EventArgs e)
        {
            static_parameters.show_edge_values = checkBox_show_edge_prop.Checked;
            my_parent_form.mt_pic.Refresh();
        }

        private void checkBox_show_element_prop_CheckedChanged(object sender, EventArgs e)
        {
            static_parameters.show_element_values = checkBox_show_element_prop.Checked;
            my_parent_form.mt_pic.Refresh();
        }

        private void checkBox_show_values_CheckedChanged(object sender, EventArgs e)
        {
            static_parameters.show_values = checkBox_show_values.Checked;
            my_parent_form.mt_pic.Refresh();
        }

        private void checkBox_show_result_vectors_CheckedChanged(object sender, EventArgs e)
        {
            static_parameters.show_result_vectors = checkBox_show_result_vectors.Checked;
            my_parent_form.mt_pic.Refresh();
        }

        private void checkBox_show_contour_lines_CheckedChanged(object sender, EventArgs e)
        {
            static_parameters.show_result_contours = checkBox_show_contour_lines.Checked;
            my_parent_form.mt_pic.Refresh();
        }
        #endregion

    }
}
