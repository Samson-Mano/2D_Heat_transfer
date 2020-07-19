using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Heat2D_solver.Front_end;

namespace Heat2D_solver.Data_structure
{
    public static class static_parameters
    {
        // Color data
        public static int color_alpha_i = 120; // maximum value allowed is ~180
        public static double hsllight = 0.4;


        public static double zm = 1.0f;
        public static double eps = 0.00001;

        // Main Form Variables
        public static bool is_shiftKeyDown = false;
        public static bool is_childFormOpen = false;// Variable to control opening of one form
        public static bool is_nodeFormOpen = false; // Variable to note down which form is open --> Node form
        public static bool is_edgeFormOpen = false; // Variable to note down which form is open --> Edge form
        public static bool is_elementFormOpen = false; // Variable to note down which form is open --> Element form
        public static bool is_viewsettingsform = false; // Variable to note down which form is open --> Settings form
        public static bool is_solverformopen = false; // Variable to note down which form is open --> Solver form

        public static bool is_selection_flg = false; // Varibale to note down whether selection happening 
        public static bool is_square_select = true; // Variable to square or circle select

        public static bool is_middle_drag = false;

        public static SizeF main_pic_size;
        public static PointF main_pic_midpt; // Mid point of main_pic
        public static PointF main_pic_panstartdrag; // Point to control pan operation
        public static PointF main_pic_start_pt;  // Point to control start point 
        public static PointF main_pic_cur_pt; // Point to control current point

        // scale values
        public static double scale_val;
        public static int load_scale_len;
        public static double const_scale_len;
        public static int deform_scale_len;

        // Form variables
        public static Nodal_prop_form add_node_prop_form;
        public static Edge_prop_form add_edge_prop_form;
        public static Element_prop_form add_element_prop_form;
        public static view_settings_form modify_view_settings_form;
        public static solver_form solve_heat2d_form;

        // Variable to control view
        public static bool show_node_values = true;
        public static bool show_edge_values = true;
        public static bool show_element_values = false;
        public static bool show_values = true;
        public static bool show_analysis_result = true;
        public static bool show_result_vectors = false;
        public static bool show_result_contours = true;

        // Result control
        public static int n_contour_intervals = 10;
    }
}
