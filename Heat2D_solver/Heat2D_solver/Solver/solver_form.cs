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
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Factorization;

namespace Heat2D_solver.Front_end
{
    public partial class solver_form : Form
    {
        Main_form my_parent_form;
        pslg_datastructure fe_object;

        public solver_form(Main_form t_form, ref pslg_datastructure t_fe_object)
        {
            InitializeComponent();
            my_parent_form = t_form;
            fe_object = t_fe_object;
        }

        private void solver_form_Load(object sender, EventArgs e)
        {
            this.Location = new Point(my_parent_form.Location.X + 14, my_parent_form.Location.Y + 59);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Opacity = 0.9;
            this.BringToFront();
            this.TopMost = true;

            // Check the model validity
            bool model_valid = true;

            // Check element count
            if (fe_object.main_mesh.all_triangles.Count == 0)
            {
                model_valid = false;
                goto exit0;
            }

            // Check element thermal conductivity
            model_valid = false;
            foreach (pslg_datastructure.triangle2d tri in fe_object.main_mesh.all_triangles)
            {
                if (tri.thermal_conductivity_x != 0 || tri.thermal_conductivity_y != 0)
                {
                    model_valid = true;
                    break;
                }

            }

        exit0:;
            if (model_valid == false)
            {
                MessageBox.Show("Model is not Complete (Elements or Constraints are missing)", "Samson Mano", MessageBoxButtons.OK);
                //this.Close();
                this.BeginInvoke(new MethodInvoker(this.Close));
            }
        }

        private void solver_form_FormClosing(object sender, FormClosingEventArgs e)
        {
            static_parameters.is_childFormOpen = false; // Inform the main form that this form is closed
            static_parameters.is_solverformopen = false;
            fe_object.selection_index.Clear();
            my_parent_form.mt_pic.Refresh();
        }

        private void button_solve_Click(object sender, EventArgs e)
        {
            int elm_order = 1;
            // Find the element order
            if (radioButton_first_order.Checked == true)
            {
                // Linear element
                elm_order = 1;
            }
            else if (radioButton_second_order.Checked == true)
            {
                // Quadratic element
                elm_order = 2;
            }
            else if (radioButton_third_order.Checked == true)
            {
                // Cubic element
                elm_order = 3;
            }

            // Start the solver
            heat2d_3_noded_triangle_solver tesla_solver = new heat2d_3_noded_triangle_solver(elm_order, fe_object, ref richTextBox_AnalysisUpdate);


            my_parent_form.mt_pic.Refresh();

        }

        public class heat2d_3_noded_triangle_solver
        {
            // Implementation of Program Heat2d from
            // Introduction to Finite Elements in Engineering (3rd Edition)
            // By T.R.Chandrupatla and A.D.Belegundu
            // Heat 2-D with 3-Noded Triangles

            pslg_datastructure inpt_fe_object;

            //__________________________ Main program variables _____________________________
            // Element order
            int element_order;

            //// Number of nodes
            //int node_count;
            //// Number of element
            //int element_count;
            //// Number of material (Thermal conductivity k)
            //int material_count;
            //// Number of co-ordinates per node
            //int coord_per_node = 2;
            //// Number of nodes per element
            //int nodes_per_element = 3;
            //// Number of degrees of freedom per node (2 for CST element)
            //int dim_count = 2;
            //// Number of specified temperature
            //int spec_temp_count;
            //// Number of nodal heat source
            //int nodal_heat_source_count;
            //// Number of element heat source
            //int element_heat_source_count;

            matrix_class coord_matrix = new matrix_class();

            // element matrices
            matrix_class[] element_k_matrix = new matrix_class[1];
            matrix_class[] element_f_matrix = new matrix_class[1];



            public heat2d_3_noded_triangle_solver(int i_element_order, pslg_datastructure i_fe_object, ref RichTextBox richTextBox_analysis_status)
            {
                this.inpt_fe_object = i_fe_object;
                element_order = i_element_order;

                if (element_order == 1)
                {
                    // call first order solver
                    first_order_solver(ref richTextBox_analysis_status);
                }
                else if (element_order == 2)
                {
                    // call second order solver
                    second_order_solver();
                }
                else if (element_order == 3)
                {
                    // call third order solver

                }

            }

            private void first_order_solver(ref RichTextBox richTextBox_analysis_status)
            {

                richTextBox_analysis_status.Clear(); // clear the richTextBox_AnalysisUpdate

                // loop variable
                int i, j, k;
                int dof = inpt_fe_object.main_mesh.all_points.Count;
                int edge_count = inpt_fe_object.main_mesh.all_edges.Count;
                int elmt_count = inpt_fe_object.main_mesh.all_triangles.Count;

                matrix_class global_k_matrix = new matrix_class(dof, dof);
                matrix_class global_f_matrix = new matrix_class(dof, 1);
                matrix_class global_dof_matrix = new matrix_class(dof, 1);

                // create nodes, edges and elements just for analysis
                matrix_class all_nodes_matrix = new matrix_class(dof, 6);
                matrix_class all_edges_matrix = new matrix_class(edge_count, 8);
                matrix_class all_elements_matrix = new matrix_class(elmt_count, 8);

                // create node matrix
                for (i = 0; i < dof; i++)
                {
                    // node index, node id, node x coord, node y coord, node heat source, node specififed temperature
                    all_nodes_matrix.SetRow(i, new double[] { i,
                        inpt_fe_object.main_mesh.all_points[i].id,
                        inpt_fe_object.main_mesh.all_points[i].x,
                        inpt_fe_object.main_mesh.all_points[i].y,
                        inpt_fe_object.main_mesh.all_points[i].heat_source,
                        inpt_fe_object.main_mesh.all_points[i].spec_temp});
                }

                // create edges matrix
                for (i = 0; i < edge_count; i++)
                {
                    // edge index, edge id, start node index, end node index, edge heat source, edge specified temperature, edge heat transfer coeff, edge ambient temp
                    all_edges_matrix.SetRow(i, new double[] {i,
                        inpt_fe_object.main_mesh.all_edges[i].edge_id,
                        get_matrix_index(all_nodes_matrix,  inpt_fe_object.main_mesh.all_edges[i].start_pt.id,dof),
                        get_matrix_index(all_nodes_matrix, inpt_fe_object.main_mesh.all_edges[i].end_pt.id,dof),
                        inpt_fe_object.main_mesh.all_edges[i].heat_source,
                        inpt_fe_object.main_mesh.all_edges[i].specified_temp,
                        inpt_fe_object.main_mesh.all_edges[i].heat_transfer_coeff,
                        inpt_fe_object.main_mesh.all_edges[i].ambient_temp}); ;
                }

                richTextBox_analysis_status.AppendText("Number of Nodes " + inpt_fe_object.main_mesh.all_points.Count.ToString() + "\n\n");
                richTextBox_analysis_status.AppendText("Number of Edges " + inpt_fe_object.main_mesh.all_edges.Count.ToString() + "\n\n");
                richTextBox_analysis_status.AppendText("Number of Elements " + inpt_fe_object.main_mesh.all_triangles.Count.ToString() + "\n\n");

                // create element matrix
                for (i = 0; i < elmt_count; i++)
                {
                    // extract current element
                    pslg_datastructure.triangle2d current_element = inpt_fe_object.main_mesh.all_triangles[i];

                    // element index, element id, vertex 1 index, vertex 2 index, vertex 3 index, edge 1 index, edge 2 index, edge 3 index 
                    all_elements_matrix.SetRow(i, new double[] { i,
                        inpt_fe_object.main_mesh.all_triangles[i].face_id,
                        get_matrix_index(all_nodes_matrix,  inpt_fe_object.main_mesh.all_triangles[i].vertices[0].id,dof),
                        get_matrix_index(all_nodes_matrix,  inpt_fe_object.main_mesh.all_triangles[i].vertices[1].id,dof),
                        get_matrix_index(all_nodes_matrix,  inpt_fe_object.main_mesh.all_triangles[i].vertices[2].id,dof),
                        get_matrix_index(all_edges_matrix,  inpt_fe_object.main_mesh.all_triangles[i].edge_id[0],edge_count),
                        get_matrix_index(all_edges_matrix,  inpt_fe_object.main_mesh.all_triangles[i].edge_id[1],edge_count),
                        get_matrix_index(all_edges_matrix,  inpt_fe_object.main_mesh.all_triangles[i].edge_id[2],edge_count)});

                    // extract the edges of the element
                    pslg_datastructure.edge2d current_elm_edge1 = inpt_fe_object.main_mesh.all_edges[(int)all_elements_matrix[i, 5]];
                    pslg_datastructure.edge2d current_elm_edge2 = inpt_fe_object.main_mesh.all_edges[(int)all_elements_matrix[i, 6]];
                    pslg_datastructure.edge2d current_elm_edge3 = inpt_fe_object.main_mesh.all_edges[(int)all_elements_matrix[i, 7]];

                    // conduction matrix
                    matrix_class conduction_matrix = new matrix_class(2, 2);
                    conduction_matrix.SetRow(0, new double[] { current_element.thermal_conductivity_x, 0.0 });
                    conduction_matrix.SetRow(1, new double[] { 0.0, current_element.thermal_conductivity_y });

                    // linear shape function parameters
                    double a_i, b_i, c_i, a_j, b_j, c_j, a_k, b_k, c_k;
                    a_i = (all_nodes_matrix[(int)all_elements_matrix[i, 3], 2] * all_nodes_matrix[(int)all_elements_matrix[i, 4], 3]) -
                                (all_nodes_matrix[(int)all_elements_matrix[i, 4], 2] * all_nodes_matrix[(int)all_elements_matrix[i, 3], 3]);
                    b_i = all_nodes_matrix[(int)all_elements_matrix[i, 3], 3] - all_nodes_matrix[(int)all_elements_matrix[i, 4], 3];
                    c_i = all_nodes_matrix[(int)all_elements_matrix[i, 4], 2] - all_nodes_matrix[(int)all_elements_matrix[i, 3], 2];

                    a_j = (all_nodes_matrix[(int)all_elements_matrix[i, 4], 2] * all_nodes_matrix[(int)all_elements_matrix[i, 2], 3]) -
                                (all_nodes_matrix[(int)all_elements_matrix[i, 2], 2] * all_nodes_matrix[(int)all_elements_matrix[i, 4], 3]);
                    b_j = all_nodes_matrix[(int)all_elements_matrix[i, 4], 3] - all_nodes_matrix[(int)all_elements_matrix[i, 2], 3];
                    c_j = all_nodes_matrix[(int)all_elements_matrix[i, 2], 2] - all_nodes_matrix[(int)all_elements_matrix[i, 4], 2];

                    a_k = (all_nodes_matrix[(int)all_elements_matrix[i, 2], 2] * all_nodes_matrix[(int)all_elements_matrix[i, 3], 3]) -
                                (all_nodes_matrix[(int)all_elements_matrix[i, 3], 2] * all_nodes_matrix[(int)all_elements_matrix[i, 2], 3]);
                    b_k = all_nodes_matrix[(int)all_elements_matrix[i, 2], 3] - all_nodes_matrix[(int)all_elements_matrix[i, 3], 3];
                    c_k = all_nodes_matrix[(int)all_elements_matrix[i, 3], 2] - all_nodes_matrix[(int)all_elements_matrix[i, 2], 2];

                    // B_matrix of linear triangle
                    matrix_class b_matrix = new matrix_class(2, 3);

                    b_matrix.SetRow(0, new double[] { b_i, b_j, b_k });
                    b_matrix.SetRow(1, new double[] { c_i, c_j, c_k });

                    b_matrix = (1.0 / (2.0 * current_element.element_area)) * b_matrix;

                    // element conduction matrix
                    matrix_class element_conduction_matrix = new matrix_class(3, 3);
                    element_conduction_matrix = (current_element.element_thickness * current_element.element_area) * (b_matrix.Transpose() * (conduction_matrix * b_matrix));

                    // element convection matrix
                    double c_param = (-2.0 * current_element.heat_transfer_coeff) / (current_element.element_thickness);
                    matrix_class element_convection_matrix = new matrix_class(3, 3);

                    element_convection_matrix.SetRow(0, new double[] { 2, 1, 1 });
                    element_convection_matrix.SetRow(1, new double[] { 1, 2, 1 });
                    element_convection_matrix.SetRow(2, new double[] { 1, 1, 2 });

                    element_convection_matrix = ((c_param * current_element.element_area) / 12.0) * element_convection_matrix;

                    // element heat convection due to edge exposed to ambient temperature (internal edges shouldn't have convection (user caution is required))
                    // element convection due to edge i-j convection
                    matrix_class element_edge_ij_convection_matrix = new matrix_class(3, 3);

                    element_edge_ij_convection_matrix.SetRow(0, new double[] { 2, 1, 0 });
                    element_edge_ij_convection_matrix.SetRow(1, new double[] { 1, 2, 0 });
                    element_edge_ij_convection_matrix.SetRow(2, new double[] { 0, 0, 0 });

                    element_edge_ij_convection_matrix = ((current_elm_edge1.heat_transfer_coeff * current_elm_edge1.edge_length * current_element.element_thickness) / 6.0) * element_edge_ij_convection_matrix;

                    // element convection due to edge j-k convection
                    matrix_class element_edge_jk_convection_matrix = new matrix_class(3, 3);

                    element_edge_jk_convection_matrix.SetRow(0, new double[] { 0, 0, 0 });
                    element_edge_jk_convection_matrix.SetRow(1, new double[] { 0, 2, 1 });
                    element_edge_jk_convection_matrix.SetRow(2, new double[] { 0, 1, 2 });

                    element_edge_jk_convection_matrix = ((current_elm_edge2.heat_transfer_coeff * current_elm_edge2.edge_length * current_element.element_thickness) / 6.0) * element_edge_jk_convection_matrix;

                    // element convection due to edge k-i convection
                    matrix_class element_edge_ki_convection_matrix = new matrix_class(3, 3);

                    element_edge_ki_convection_matrix.SetRow(0, new double[] { 2, 0, 1 });
                    element_edge_ki_convection_matrix.SetRow(1, new double[] { 0, 0, 0 });
                    element_edge_ki_convection_matrix.SetRow(2, new double[] { 1, 0, 2 });

                    element_edge_ki_convection_matrix = ((current_elm_edge3.heat_transfer_coeff * current_elm_edge3.edge_length * current_element.element_thickness) / 6.0) * element_edge_ki_convection_matrix;

                    // element heat source matrix
                    matrix_class element_heat_source_matrix = new matrix_class(3, 1);

                    element_heat_source_matrix.SetColumn(0, new double[] { 1, 1, 1 });

                    element_heat_source_matrix = ((current_element.heat_source * current_element.element_area * current_element.element_thickness) / 3.0) * element_heat_source_matrix;

                    // element heat convection 1 matrix
                    matrix_class element_heat_convection_matrix = new matrix_class(3, 1);

                    element_heat_convection_matrix.SetColumn(0, new double[] { 1, 1, 1 });

                    element_heat_convection_matrix = ((c_param * current_element.ambient_temp * current_element.element_area) / 3.0) * element_heat_convection_matrix;

                    // edge heat source matrix
                    matrix_class edge_heatsource_matrix = new matrix_class(3, 1);
                    // edge heat soure due to edge i-j source
                    matrix_class edge_ij_heatsource_matrix = new matrix_class(3, 1);

                    edge_ij_heatsource_matrix.SetColumn(0, new double[] { 1, 1, 0 });

                    edge_ij_heatsource_matrix = ((current_elm_edge1.heat_source * current_elm_edge1.edge_length * current_element.element_thickness) / 2.0) * edge_ij_heatsource_matrix;

                    // edge heat soure due to edge j-k source
                    matrix_class edge_jk_heatsource_matrix = new matrix_class(3, 1);

                    edge_jk_heatsource_matrix.SetColumn(0, new double[] { 0, 1, 1 });

                    edge_jk_heatsource_matrix = ((current_elm_edge2.heat_source * current_elm_edge2.edge_length * current_element.element_thickness) / 2.0) * edge_jk_heatsource_matrix;

                    // edge heat soure due to edge k-i source
                    matrix_class edge_ki_heatsource_matrix = new matrix_class(3, 1);

                    edge_ki_heatsource_matrix.SetColumn(0, new double[] { 1, 0, 1 });

                    edge_ki_heatsource_matrix = ((current_elm_edge3.heat_source * current_elm_edge3.edge_length * current_element.element_thickness) / 2.0) * edge_ki_heatsource_matrix;

                    edge_heatsource_matrix = edge_ij_heatsource_matrix + edge_jk_heatsource_matrix + edge_ki_heatsource_matrix;

                    // edge heat convection matrix
                    matrix_class edge_heatconvection_matrix = new matrix_class(3, 1);
                    // edge heat convection due to edge i-j ambient temperature
                    matrix_class edge_ij_heatconvection_matrix = new matrix_class(3, 1);

                    edge_ij_heatconvection_matrix.SetColumn(0, new double[] { 1, 1, 0 });

                    edge_ij_heatconvection_matrix = ((current_elm_edge1.heat_transfer_coeff * current_elm_edge1.ambient_temp * current_elm_edge1.edge_length * current_element.element_thickness) / 2.0) * edge_ij_heatconvection_matrix;

                    // edge heat convection due to edge j-k ambient temperature
                    matrix_class edge_jk_heatconvection_matrix = new matrix_class(3, 1);

                    edge_jk_heatconvection_matrix.SetColumn(0, new double[] { 0, 1, 1 });

                    edge_jk_heatconvection_matrix = ((current_elm_edge2.heat_transfer_coeff * current_elm_edge2.ambient_temp * current_elm_edge2.edge_length * current_element.element_thickness) / 2.0) * edge_jk_heatconvection_matrix;

                    // edge heat convection due to edge k-i ambient temperature
                    matrix_class edge_ki_heatconvection_matrix = new matrix_class(3, 1);

                    edge_ki_heatconvection_matrix.SetColumn(0, new double[] { 1, 0, 1 });

                    edge_ki_heatconvection_matrix = ((current_elm_edge3.heat_transfer_coeff * current_elm_edge3.ambient_temp * current_elm_edge3.edge_length * current_element.element_thickness) / 2.0) * edge_ki_heatconvection_matrix;

                    edge_heatconvection_matrix = edge_ij_heatconvection_matrix + edge_jk_heatconvection_matrix + edge_ki_heatconvection_matrix;

                    // edge specified temperature matrix
                    matrix_class edge_spectemp_matrix = new matrix_class(3, 1);
                    // edge i-j specified temperature
                    matrix_class edge_ij_spectemp_matrix = new matrix_class(3, 1);

                    edge_ij_spectemp_matrix.SetColumn(0, new double[] { 1, 1, 0 });

                    edge_ij_spectemp_matrix = (current_elm_edge1.specified_temp / 2.0) * edge_ij_spectemp_matrix;

                    // edge j-k specified temperature
                    matrix_class edge_jk_spectemp_matrix = new matrix_class(3, 1);

                    edge_jk_spectemp_matrix.SetColumn(0, new double[] { 0, 1, 1 });

                    edge_jk_spectemp_matrix = (current_elm_edge2.specified_temp / 2.0) * edge_jk_spectemp_matrix;

                    // edge k-i specified temperature
                    matrix_class edge_ki_spectemp_matrix = new matrix_class(3, 1);

                    edge_ki_spectemp_matrix.SetColumn(0, new double[] { 1, 0, 1 });

                    edge_ki_spectemp_matrix = (current_elm_edge3.specified_temp / 2.0) * edge_ki_spectemp_matrix;

                    edge_spectemp_matrix = edge_ij_spectemp_matrix + edge_jk_spectemp_matrix + edge_ki_spectemp_matrix;

                    // element k matrix
                    matrix_class element_k_matrix = new matrix_class(3, 3);

                    element_k_matrix = element_conduction_matrix + ( - element_convection_matrix) + element_edge_ij_convection_matrix + element_edge_jk_convection_matrix + element_edge_ki_convection_matrix;

                    // element f matrix
                    matrix_class element_f_matrix = new matrix_class(3, 1);

                    element_f_matrix = element_heat_source_matrix + element_heat_convection_matrix + edge_heatsource_matrix + edge_heatconvection_matrix;

                    // dof matrix carries the specified temperature
                    // element dof matrix
                    matrix_class element_dof_matrix = new matrix_class(3, 1);

                    element_dof_matrix = edge_spectemp_matrix;

                    // global k matrix
                    for (k = 0; k < 3; k++)
                    {
                        int v_index1 = (int)all_elements_matrix[i, 2 + k];
                        for (j = 0; j < 3; j++)
                        {
                            int v_index2 = (int)all_elements_matrix[i, 2 + j];
                            // global k matrix
                            global_k_matrix[v_index1, v_index2] = global_k_matrix[v_index1, v_index2] + element_k_matrix[k, j];

                        }
                        // global f matrix
                        global_f_matrix[v_index1, 0] = global_f_matrix[v_index1, 0] + element_f_matrix[k, 0];

                        // global dof matrix
                        global_dof_matrix[v_index1, 0] = global_dof_matrix[v_index1, 0] + element_dof_matrix[k, 0];
                    }
                }

                // Apply nodal heat source & specified temperature
                for (i = 0; i < dof; i++)
                {
                    // heat source at node
                    global_f_matrix[i, 0] = global_f_matrix[i, 0] + inpt_fe_object.main_mesh.all_points[i].heat_source;

                    // specified temperature
                    global_dof_matrix[i, 0] = global_dof_matrix[i, 0] + inpt_fe_object.main_mesh.all_points[i].spec_temp;
                }

                // Applying specified temperature as source
                matrix_class global_spec_temp_matrix = new matrix_class(dof, 1);
                global_spec_temp_matrix = -1 * (global_k_matrix * global_dof_matrix);

                // Find the size of matrix after boundary condition is applied
                int soln_matrix_count = 0;
                for (i = 0; i < dof; i++)
                {
                    if (global_dof_matrix[i, 0] == 0)
                    {
                        soln_matrix_count++;
                    }

                }

                richTextBox_analysis_status.AppendText("Total number of DOF without prescribed Temperature " + soln_matrix_count.ToString() + "\n\n");


                // curtailed matrix after applying boundary condition
                matrix_class curtailed_global_k_matrix = new matrix_class(soln_matrix_count, soln_matrix_count);
                matrix_class curtailed_global_f_matrix = new matrix_class(soln_matrix_count, 1);

                int r = 0, s = 0;
                for (i = 0; i < dof; i++)
                {
                    if (global_dof_matrix[i, 0] != 0)
                    {
                        continue;
                    }
                    else
                    {
                        // apply boundary condition to global f matrix
                        curtailed_global_f_matrix[r, 0] = global_f_matrix[i, 0] + global_spec_temp_matrix[i, 0];
                        s = 0;
                        for (j = 0; j < dof; j++)
                        {
                            if (global_dof_matrix[j, 0] != 0)
                            {
                                continue;
                            }
                            else
                            {
                                // apply boundary condition to global k matrix
                                curtailed_global_k_matrix[r, s] = global_k_matrix[i, j];
                                s++;
                            }
                        }
                        r++;
                    }
                }

                // Build matrix to use in mathnet.numerics
                Matrix<double> A_var = Matrix<double>.Build.DenseOfArray(curtailed_global_k_matrix.return_matrix_as_double);
                Vector<double> B_var = Vector<double>.Build.Dense(curtailed_global_f_matrix.GetColumn(0));

                // solve the matrix using mathnet.numerics
                Vector<double> x_var = A_var.Solve(B_var);

                // global t matrix
                double[] gloabl_t_val = new double[dof];

                r = 0;
                for (i = 0; i < dof; i++)
                {
                    if (global_dof_matrix[i, 0] != 0)
                    {
                        gloabl_t_val[i] = global_dof_matrix[i, 0];
                    }
                    else
                    {
                        gloabl_t_val[i] = x_var[r];
                        r++;
                    }
                }

                // create element store list
                List<pslg_datastructure.result_store.first_order_element_store> f_o_elements = new List<pslg_datastructure.result_store.first_order_element_store>();

                // maximum and minimum z val
                double max_zval = gloabl_t_val.Max();
                double min_zval = gloabl_t_val.Min();

                // set the contour intervals
                double z_val_range = max_zval - min_zval;
                double[] contour_interval = new double[static_parameters.n_contour_intervals];
                for (i = 1; i < static_parameters.n_contour_intervals; i++)
                {
                    contour_interval[i] = min_zval + ((z_val_range / static_parameters.n_contour_intervals) * i);
                }

                if (z_val_range <= 1E-10 || double.IsNaN(z_val_range) == true)
                {
                    richTextBox_analysis_status.AppendText("!!!!!!!! Analysis Failed !!!!!!!! " + "\n\n");
                    return; // Exit sub
                }
                else
                {
                    richTextBox_analysis_status.AppendText("Maximum Temperature " + max_zval.ToString() + "\n");
                    richTextBox_analysis_status.AppendText("Minimum Temperature " + min_zval.ToString() + "\n\n");
                    richTextBox_analysis_status.AppendText("!!!!!!!! Analysis Complete !!!!!!!! " + "\n\n");
                }

                // Set the results to output
                for (i = 0; i < elmt_count; i++)
                {
                    double[] elm_z_val = new double[3];

                    elm_z_val[0] = gloabl_t_val[(int)all_elements_matrix[i, 2]];
                    elm_z_val[1] = gloabl_t_val[(int)all_elements_matrix[i, 3]];
                    elm_z_val[2] = gloabl_t_val[(int)all_elements_matrix[i, 4]];

                    pslg_datastructure.result_store.first_order_element_store f_o_elm = new pslg_datastructure.result_store.first_order_element_store(inpt_fe_object.main_mesh.all_triangles[i],
                        elm_z_val, contour_interval, max_zval, min_zval);

                    f_o_elements.Add(f_o_elm);
                }

                // create result store and add it to the model
                pslg_datastructure.result_store rslt_output = new pslg_datastructure.result_store();
                rslt_output = new pslg_datastructure.result_store(1, f_o_elements, null, null, max_zval, min_zval, contour_interval);
                inpt_fe_object.main_mesh.set_analysis_results(rslt_output);

            }

            private void second_order_solver()
            {






            }

            private int get_matrix_index(matrix_class i_the_matrix, int i_id, int row_count)
            {
                for (int i = 0; i < row_count; i++)
                {
                    if (i_the_matrix.GetRow(i)[1] == i_id)
                    {
                        return i;
                    }
                }
                return -1;
            }






        }





    }
}
