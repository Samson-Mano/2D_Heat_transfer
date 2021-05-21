using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Heat2D_solver.Data_structure;
using Heat2D_solver.Useful_Function;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Specialized;
using System.Reflection;

namespace Heat2D_solver
{
    public partial class Main_form : Form
    {
        // main variable
        private pslg_datastructure _fe_objects = new pslg_datastructure();
        // private List<int> selected_index = new List<int>();

        public pslg_datastructure fe_objects
        {
            get { return this._fe_objects; }
            set { this._fe_objects = value; }
        }

        public Main_form()
        {
            InitializeComponent();
        }

        #region " Pre-processing - Menu Items"

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // new model
            DialogResult msg_rslt = DialogResult.Yes;
            if (fe_objects.main_mesh.all_points.Count != 0)
            {
                msg_rslt = MessageBox.Show("Do you want to start new model? Unsaved items will be discarded", "Samson Mano", MessageBoxButtons.YesNo);
            }

            if (msg_rslt == DialogResult.Yes)
            {
                this._fe_objects = new pslg_datastructure();

                // Zoom screen fit
                PointF MidPt = new PointF((main_pic.Width / 2), co_functions.tosingle(main_pic.Height / 2));
                static_parameters.zm = 1.0f;
                static_parameters.main_pic_midpt = MidPt;


                toolStripStatusLabel_ZoomValue.Text = "Zoom: " + (static_parameters.zm * 100) + "%"; // Show the zoom value in status tool label
                mt_pic.Refresh();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ow = new OpenFileDialog();
            ow.DefaultExt = ".2ht";
            ow.Filter = "Samson Mano's 2D - heat model files (*.2ht)|*.2ht";
            ow.ShowDialog();

            if (File.Exists(ow.FileName))
            {
                List<object> trobject = new List<object>();

                using (Stream gsf = File.OpenRead(ow.FileName))
                {
                    BinaryFormatter deserializer = new BinaryFormatter();
                    try
                    {
                        trobject = (List<object>)deserializer.Deserialize(gsf);

                        this._fe_objects = new pslg_datastructure();
                        this._fe_objects = (pslg_datastructure)trobject[0];


                        // Zoom screen fit
                        PointF MidPt = new PointF((main_pic.Width / 2), co_functions.tosingle(main_pic.Height / 2));
                        static_parameters.zm = 1.0f;
                        static_parameters.main_pic_midpt = MidPt;


                        toolStripStatusLabel_ZoomValue.Text = "Zoom: " + (static_parameters.zm * 100) + "%"; // Show the zoom value in status tool label
                        mt_pic.Refresh();
                    }
                    catch
                    {
                        MessageBox.Show("Sorry!!!!! Unable to Open.. File Reading Error", "Samson Mano", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fe_objects.main_mesh.all_points.Count != 0)
            {
                SaveFileDialog sw = new SaveFileDialog();
                sw.DefaultExt = ".2ht"; // 2d heat transfet analysis
                sw.Filter = "Samson Mano's 2D - heat model files (*.2ht)|*.2ht";
                sw.FileName = "heat_surface";
                DialogResult = sw.ShowDialog();

                List<object> trobject = new List<object>();
                trobject.Add(this._fe_objects);

                using (Stream psf = File.Create(sw.FileName))
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    serializer.Serialize(psf, trobject);
                }

            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Exit the program
            DialogResult msg_rslt = DialogResult.Yes;
            if (fe_objects.main_mesh.all_points.Count != 0)
            {
                msg_rslt = MessageBox.Show("Do you want to start new model? Unsaved items will be discarded", "Samson Mano", MessageBoxButtons.YesNo);
            }

            if (msg_rslt == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // View settings
            if (static_parameters.is_childFormOpen == false)
            {
                static_parameters.modify_view_settings_form = new Front_end.view_settings_form(this, ref _fe_objects); // create a instance for Node form
                static_parameters.modify_view_settings_form.Show();
                static_parameters.is_childFormOpen = true;
                static_parameters.is_viewsettingsform = true;
            }
            else
            {
                //--- Prompt the user to close other opened forms
                MessageBox.Show("Close other windows before opening this !!", "Samson Mano", MessageBoxButtons.OK);
            }
            mt_pic.Refresh();
        }

        private void importMeshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Import of Nodes from a Text File
            OpenFileDialog openfiledialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\"
            openfiledialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openfiledialog1.FilterIndex = 2;
            openfiledialog1.RestoreDirectory = true;

            // Temporary mesh variable (point2d, edge2d and tri2d)
            List<pslg_datastructure.point2d> temp_pslg_pointlist = new List<pslg_datastructure.point2d>();
            List<pslg_datastructure.edge2d> temp_pslg_edgelist = new List<pslg_datastructure.edge2d>();
            List<pslg_datastructure.triangle2d> temp_pslg_trilist = new List<pslg_datastructure.triangle2d>();

            if (openfiledialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    StreamReader txtreader = new StreamReader(File.OpenRead(openfiledialog1.FileName), Encoding.UTF8, true, 128);
                    int input_mode = 0;
                    // node id list is to re number nodes from 0 to n index
                    List<int> node_id_list = new List<int>();

                    if (txtreader != null)
                    {
                        int temp_ed_id = 1;
                        // code to read the stream
                        string txtline = txtreader.ReadLine();
                        while (txtline != null)
                        {
                            if (txtline.StartsWith("**"))
                            {
                                // comment from the user input is skipped
                                txtline = txtreader.ReadLine();
                                continue;
                            }
                            else if (txtline.StartsWith("*NODE"))
                            {
                                // input mode 1 is node input
                                txtline = txtreader.ReadLine();
                                input_mode = 1;
                                continue;
                            }
                            else if (txtline.StartsWith("*ELEMENT"))
                            {
                                // input mode 2 is element input
                                txtline = txtreader.ReadLine();
                                input_mode = 2;
                                continue;
                            }


                            if (input_mode == 1)
                            {
                                // Read the nodes
                                string[] txt_word = txtline.Split(',');

                                int node_id = Convert.ToInt32(txt_word[0]);
                                double inp_x = Convert.ToDouble(txt_word[1]);
                                double inp_y = Convert.ToDouble(txt_word[2]);

                                // Add to the temp_pt
                                pslg_datastructure.point2d temp_pt = new pslg_datastructure.point2d(node_id_list.Count, inp_x, inp_y);

                                if (temp_pslg_pointlist.Exists(obj => obj.Equals(temp_pt)) == false)
                                {
                                    // Add to the temp_pslg_pointlist
                                    node_id_list.Add(node_id);
                                    temp_pslg_pointlist.Add(temp_pt);
                                }
                            }
                            else if (input_mode == 2)
                            {
                                // Read the Elements
                                string[] txt_word = txtline.Split(',');

                                int elem_id = (Convert.ToInt32(txt_word[0]));
                                int nd1_id = (Convert.ToInt32(txt_word[1]));
                                int nd2_id = (Convert.ToInt32(txt_word[2]));
                                int nd3_id = (Convert.ToInt32(txt_word[3]));

                                int pt1_index = temp_pslg_pointlist.FindIndex(obj => node_id_list[obj.id] == nd1_id);
                                int pt2_index = temp_pslg_pointlist.FindIndex(obj => node_id_list[obj.id] == nd2_id);
                                int pt3_index = temp_pslg_pointlist.FindIndex(obj => node_id_list[obj.id] == nd3_id);

                                if (pt1_index != -1 && pt2_index != -1 && pt3_index != -1)
                                {
                                    // Add to the temp_pt
                                    pslg_datastructure.triangle2d temp_tri = new pslg_datastructure.triangle2d(elem_id, temp_pslg_pointlist[pt1_index],
                                                                                                                        temp_pslg_pointlist[pt2_index],
                                                                                                                        temp_pslg_pointlist[pt3_index]);

                                    // Triangle addition
                                    if (temp_pslg_trilist.Exists(obj => obj.Equals(temp_tri)) == false)
                                    {
                                        // Add to the temp_pslg_trilist
                                        temp_pslg_trilist.Add(temp_tri);
                                    }
                                    else
                                    {
                                        continue;
                                    }

                                    int temp_edge_index, t_id1, t_id2, t_id3;
                                    // Edge 1
                                    pslg_datastructure.edge2d temp_edge1 = new pslg_datastructure.edge2d(temp_ed_id, temp_pslg_pointlist[pt1_index],
                                                                                                                     temp_pslg_pointlist[pt2_index]);
                                    temp_edge_index = temp_pslg_edgelist.FindIndex(obj => obj.Equals_without_orientation(temp_edge1)); ;
                                    if (temp_edge_index == -1)
                                    {
                                        // add to the list
                                        temp_pslg_edgelist.Add(temp_edge1);
                                        temp_pslg_edgelist[temp_pslg_edgelist.Count - 1].add_triangle(temp_tri.face_id, temp_tri.mid_pt);
                                        // note down the edge id
                                        t_id1 = temp_pslg_edgelist[temp_pslg_edgelist.Count - 1].edge_id;
                                        temp_ed_id++;
                                    }
                                    else
                                    {
                                        // update the values
                                        temp_pslg_edgelist[temp_edge_index].add_triangle(temp_tri.face_id, temp_tri.mid_pt);
                                        // note down the edge id
                                        t_id1 = temp_pslg_edgelist[temp_edge_index].edge_id;
                                    }

                                    // Edge 2
                                    pslg_datastructure.edge2d temp_edge2 = new pslg_datastructure.edge2d(temp_ed_id, temp_pslg_pointlist[pt2_index],
                                                                                                                     temp_pslg_pointlist[pt3_index]);
                                    temp_edge_index = temp_pslg_edgelist.FindIndex(obj => obj.Equals_without_orientation(temp_edge2)); ;
                                    if (temp_edge_index == -1)
                                    {
                                        // add to the list
                                        temp_pslg_edgelist.Add(temp_edge2);
                                        temp_pslg_edgelist[temp_pslg_edgelist.Count - 1].add_triangle(temp_tri.face_id, temp_tri.mid_pt);
                                        // note down the edge id
                                        t_id2 = temp_pslg_edgelist[temp_pslg_edgelist.Count - 1].edge_id;
                                        temp_ed_id++;
                                    }
                                    else
                                    {
                                        // update the values
                                        temp_pslg_edgelist[temp_edge_index].add_triangle(temp_tri.face_id, temp_tri.mid_pt);
                                        // note down the edge id
                                        t_id2 = temp_pslg_edgelist[temp_edge_index].edge_id;
                                    }

                                    // Edge 1;
                                    pslg_datastructure.edge2d temp_edge3 = new pslg_datastructure.edge2d(temp_ed_id, temp_pslg_pointlist[pt3_index],
                                                                                                                     temp_pslg_pointlist[pt1_index]);
                                    temp_edge_index = temp_pslg_edgelist.FindIndex(obj => obj.Equals_without_orientation(temp_edge3)); ;
                                    if (temp_edge_index == -1)
                                    {
                                        // add to the list
                                        temp_pslg_edgelist.Add(temp_edge3);
                                        temp_pslg_edgelist[temp_pslg_edgelist.Count - 1].add_triangle(temp_tri.face_id, temp_tri.mid_pt);
                                        // note down the edge id
                                        t_id3 = temp_pslg_edgelist[temp_pslg_edgelist.Count - 1].edge_id;
                                        temp_ed_id++;
                                    }
                                    else
                                    {
                                        // update the values
                                        temp_pslg_edgelist[temp_edge_index].add_triangle(temp_tri.face_id, temp_tri.mid_pt);
                                        // note down the edge id
                                        t_id3 = temp_pslg_edgelist[temp_edge_index].edge_id;
                                    }

                                    // Add the index to the triangle
                                    temp_pslg_trilist[temp_pslg_trilist.Count - 1].set_edges(t_id1, t_id2, t_id3);

                                }
                            }
                            txtline = txtreader.ReadLine();
                        }
                        //string msgtext = "**" + "\n" + "**" + "\n" + "**Template:  Heat 2D Program" + "\n" + "**" + "\n" + "*NODE" + "\n";
                        //foreach (pslg_datastructure.point2d pt in temp_pslg_pointlist)
                        //{
                        //    msgtext = msgtext + "\t" + pt.id.ToString() + "," + "\t" + pt.x.ToString() + "," + "\t" + pt.y.ToString() + "\n";
                        //}
                        //msgtext = msgtext + "*ELEMENT,TYPE=S3" + "\n";
                        //foreach (pslg_datastructure.triangle2d tr in temp_pslg_trilist)
                        //{
                        //    msgtext = msgtext + "\t" + tr.face_id.ToString() + "," + "\t" + tr.vertices[0].id.ToString() + "," + "\t" + tr.vertices[1].id.ToString() + "," + "\t" + tr.vertices[2].id.ToString() + "\n";
                        //}
                        //msgtext = msgtext + "*****";
                        //Clipboard.SetText(msgtext);

                        // Find the nodes which are not connected to any element
                        List<int> disassociated_node_id = new List<int>();
                        foreach (pslg_datastructure.point2d pt in temp_pslg_pointlist)
                        {
                            bool nd_found = false;
                            foreach (pslg_datastructure.triangle2d tr in temp_pslg_trilist)
                            {
                                if (tr.vertices[0].id == pt.id || tr.vertices[1].id == pt.id || tr.vertices[2].id == pt.id)
                                {
                                    nd_found = true;
                                    // Exit for because node is connected to an element
                                    break;
                                }
                            }

                            if (nd_found == false)
                            {
                                // node is not found
                                disassociated_node_id.Add(pt.id);
                            }
                        }

                        // Remove the nodes which are not connected to any element
                        foreach (int nd_id in disassociated_node_id)
                        {
                            temp_pslg_pointlist.RemoveAt(temp_pslg_pointlist.FindIndex(obj => obj.id == nd_id));
                        }


                        MessageBox.Show(" Reading Input Successfull !!!", "Samson Mano", MessageBoxButtons.OK);


                        // Load the mesh to the main data
                        fe_objects = new pslg_datastructure();
                        pslg_datastructure.mesh2d temp_mesh = new pslg_datastructure.mesh2d(temp_pslg_pointlist, temp_pslg_edgelist, temp_pslg_trilist);
                        fe_objects = new pslg_datastructure(temp_mesh);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cannot read file from disk. Original error: " + ex.Message.ToString());
                }
                mt_pic.Refresh();
            }
            openfiledialog1.Dispose();
        }

        private void nodalPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Add nodes form open event
            if (static_parameters.is_childFormOpen == false)
            {
                static_parameters.add_node_prop_form = new Front_end.Nodal_prop_form(this, ref _fe_objects); // create a instance for Node form
                static_parameters.add_node_prop_form.Show();
                static_parameters.is_childFormOpen = true;
                static_parameters.is_nodeFormOpen = true;
            }
            else
            {
                //--- Prompt the user to close other opened forms
                MessageBox.Show("Close other windows before opening this !!", "Samson Mano", MessageBoxButtons.OK);
            }
            mt_pic.Refresh();
        }


        private void edgePropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Add edges form open event
            if (static_parameters.is_childFormOpen == false)
            {
                static_parameters.add_edge_prop_form = new Front_end.Edge_prop_form(this, ref _fe_objects); // create a instance for Node form
                static_parameters.add_edge_prop_form.Show();
                static_parameters.is_childFormOpen = true;
                static_parameters.is_edgeFormOpen = true;
            }
            else
            {
                //--- Prompt the user to close other opened forms
                MessageBox.Show("Close other windows before opening this !!", "Samson Mano", MessageBoxButtons.OK);
            }
            mt_pic.Refresh();
        }

        private void elementPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Add element form open event
            if (static_parameters.is_childFormOpen == false)
            {
                static_parameters.add_element_prop_form = new Front_end.Element_prop_form(this, ref _fe_objects); // create a instance for Node form
                static_parameters.add_element_prop_form.Show();
                static_parameters.is_childFormOpen = true;
                static_parameters.is_elementFormOpen = true;
            }
            else
            {
                //--- Prompt the user to close other opened forms
                MessageBox.Show("Close other windows before opening this !!", "Samson Mano", MessageBoxButtons.OK);
            }
            mt_pic.Refresh();
        }

        private void finiteElementSolveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // solve form open event
            if (static_parameters.is_childFormOpen == false)
            {
                static_parameters.solve_heat2d_form = new Front_end.solver_form(this, ref _fe_objects); // create a instance for Node form
                static_parameters.solve_heat2d_form.Show();
                static_parameters.is_childFormOpen = true;
                static_parameters.is_solverformopen = true;
            }
            else
            {
                //--- Prompt the user to close other opened forms
                MessageBox.Show("Close other windows before opening this !!", "Samson Mano", MessageBoxButtons.OK);
            }
            mt_pic.Refresh();
        }

        private void showResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            static_parameters.show_analysis_result = showResultsToolStripMenuItem.Checked;
            mt_pic.Refresh();
        }

        private void removeLastLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fe_objects.main_mesh.remove_temperature_label(1);
            mt_pic.Refresh();
        }

        private void removeAllLabelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fe_objects.main_mesh.remove_temperature_label(2);
            mt_pic.Refresh();
        }
        #endregion

        #region "Main pic paint events"
        private void Main_form_Load(object sender, EventArgs e)
        {
            // Adjust Mid Point
            PointF MidPt = new PointF((main_pic.Width / 2), co_functions.tosingle(main_pic.Height / 2));
            static_parameters.main_pic_size = new SizeF(main_pic.Width, main_pic.Height);
            static_parameters.main_pic_midpt = MidPt;
            this.DoubleBuffered = true;

            // Invoke the panel doublebuggered, using the InvokeMember method
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
           | BindingFlags.Instance | BindingFlags.NonPublic, null,
           main_pic, new object[] { true });

            mt_pic.Refresh();
        }

        private void main_pic_Resize(object sender, EventArgs e)
        {
            // Adjust Mid Point
            PointF MidPt = new PointF((main_pic.Width / 2), co_functions.tosingle(main_pic.Height / 2));
            static_parameters.main_pic_size = new SizeF(main_pic.Width, main_pic.Height);
            static_parameters.main_pic_midpt = MidPt;
            mt_pic.Refresh();
        }

        private void main_pic_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr0 = e.Graphics;
            System.Drawing.Drawing2D.GraphicsState bef_trans = e.Graphics.Save();

            gr0.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            double scale_xy = main_pic.Width > main_pic.Height ? (main_pic.Height * 0.8) : (main_pic.Width * 0.8);
            double model_extent_xy = fe_objects.main_mesh.model_extent_x > fe_objects.main_mesh.model_extent_y ? fe_objects.main_mesh.model_extent_x : fe_objects.main_mesh.model_extent_y;
            double paint_scale = co_functions.tosingle(scale_xy / model_extent_xy);

            e.Graphics.ScaleTransform(co_functions.tosingle(static_parameters.zm), co_functions.tosingle(static_parameters.zm));
            e.Graphics.TranslateTransform(static_parameters.main_pic_midpt.X, static_parameters.main_pic_midpt.Y);

            fe_objects.main_mesh.paint_me(ref gr0, ref bef_trans, paint_scale);

            // Child form open
            if (static_parameters.is_childFormOpen == true && fe_objects.selection_index.Count != 0)
            {
                if (static_parameters.is_nodeFormOpen == true)
                {
                    // Selection of nodes
                    fe_objects.main_mesh.paint_selection(ref gr0, paint_scale, 1, fe_objects.selection_index);
                }
                else if (static_parameters.is_edgeFormOpen == true)
                {
                    // Selection of edges
                    fe_objects.main_mesh.paint_selection(ref gr0, paint_scale, 2, fe_objects.selection_index);
                }
                else if (static_parameters.is_elementFormOpen == true)
                {
                    // Selection of elements
                    fe_objects.main_mesh.paint_selection(ref gr0, paint_scale, 3, fe_objects.selection_index);
                }
            }

            paint_selection_boundary(ref gr0);
        }

        private void main_pic_MouseClick(object sender, MouseEventArgs e)
        {
            if (static_parameters.show_analysis_result == true && e.Button == MouseButtons.Left)
            {
                if (fe_objects.main_mesh.is_analysis_complete == true)
                {
                    static_parameters.main_pic_start_pt = new PointF(co_functions.tosingle((e.X / static_parameters.zm)) - static_parameters.main_pic_midpt.X,
                                                                    co_functions.tosingle((e.Y / static_parameters.zm) - static_parameters.main_pic_midpt.Y));

                    // Reverse the paint point to model point
                    double scale_xy = main_pic.Width > main_pic.Height ? (main_pic.Height * 0.8) : (main_pic.Width * 0.8);
                    double model_extent_xy = fe_objects.main_mesh.model_extent_x > fe_objects.main_mesh.model_extent_y ? fe_objects.main_mesh.model_extent_x : fe_objects.main_mesh.model_extent_y;
                    double paint_scale = co_functions.tosingle(scale_xy / model_extent_xy);

                    double i_nx = (static_parameters.main_pic_start_pt.X / paint_scale) + fe_objects.main_mesh.model_mid_pt.X;
                    double i_ny = ((-1 * static_parameters.main_pic_start_pt.Y) / paint_scale) + fe_objects.main_mesh.model_mid_pt.Y;

                    pslg_datastructure.point2d label_node = new pslg_datastructure.point2d(-10, i_nx, i_ny);

                    // Add the label to mesh
                    fe_objects.main_mesh.set_temperature_label(label_node);
                }
                mt_pic.Refresh();
            }
        }

        private void main_pic_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Zoom screen fit
            PointF MidPt = new PointF((main_pic.Width / 2), co_functions.tosingle(main_pic.Height / 2));
            static_parameters.zm = 1.0f;
            static_parameters.main_pic_midpt = MidPt;


            toolStripStatusLabel_ZoomValue.Text = "Zoom: " + (static_parameters.zm * 100) + "%"; // Show the zoom value in status tool label
            mt_pic.Refresh();
        }

        private void main_pic_MouseMove(object sender, MouseEventArgs e)
        {
            // mouse move drag for pan operation
            if (static_parameters.is_middle_drag == true)
            {
                // Pan Operation
                static_parameters.main_pic_midpt = new PointF(co_functions.tosingle(e.X / static_parameters.zm) - static_parameters.main_pic_panstartdrag.X,
                                                              co_functions.tosingle(e.Y / static_parameters.zm) - static_parameters.main_pic_panstartdrag.Y);

                mt_pic.Refresh();
            }

            // Selection operation
            if (static_parameters.is_selection_flg == true)
            {
                static_parameters.main_pic_cur_pt = new PointF(co_functions.tosingle((e.X / static_parameters.zm) - static_parameters.main_pic_midpt.X),
                                                                  co_functions.tosingle((e.Y / static_parameters.zm) - static_parameters.main_pic_midpt.Y));

                mt_pic.Refresh();
            }
        }

        private void main_pic_MouseWheel(object sender, MouseEventArgs e)
        {
            // Mouse wheel to capture zoom events
            double xw, yw;
            main_pic.Focus();

            PointF MidPt = static_parameters.main_pic_midpt;

            xw = (e.X / static_parameters.zm) - MidPt.X;
            yw = (e.Y / static_parameters.zm) - MidPt.Y;

            if (e.Delta > 0)
            {
                if (static_parameters.zm < 100)
                    static_parameters.zm = static_parameters.zm + 0.2; // Zoom positve

            }
            else if (e.Delta < 0)
            {
                if (static_parameters.zm > 0.201)
                    static_parameters.zm = static_parameters.zm - 0.2; // Zoom negative
            }

            MidPt.X = co_functions.tosingle((e.X / static_parameters.zm) - xw);
            MidPt.Y = co_functions.tosingle((e.Y / static_parameters.zm) - yw);

            // Set the mid point to global static variable
            static_parameters.main_pic_midpt = MidPt;

            toolStripStatusLabel_ZoomValue.Text = "Zoom: " + (static_parameters.zm * 100) + "%"; // Show the zoom value in status tool label
            mt_pic.Refresh();
        }

        private void main_pic_MouseUp(object sender, MouseEventArgs e)
        {
            // Mouse up for Click and drag
            if (e.Button == MouseButtons.Middle)
            {
                // Pan operation stops
                static_parameters.is_middle_drag = false;
            }
            if ((e.Button == MouseButtons.Left || e.Button == MouseButtons.Right) && static_parameters.is_selection_flg == true && static_parameters.is_childFormOpen == true)
            {
                static_parameters.is_selection_flg = false;
                static_parameters.main_pic_cur_pt = new PointF(co_functions.tosingle((e.X / static_parameters.zm) - static_parameters.main_pic_midpt.X),
                                                                  co_functions.tosingle((e.Y / static_parameters.zm) - static_parameters.main_pic_midpt.Y));
                if (static_parameters.is_nodeFormOpen == true)
                {
                    // Selection of nodes
                    if (e.Button == MouseButtons.Left)
                    {
                        add_remove_selection(1, true);
                    }
                    else
                    {
                        add_remove_selection(1, false);
                    }
                }
                else if (static_parameters.is_edgeFormOpen == true)
                {
                    // Selection of edges
                    if (e.Button == MouseButtons.Left)
                    {
                        add_remove_selection(2, true);
                    }
                    else
                    {
                        add_remove_selection(2, false);
                    }
                }
                else if (static_parameters.is_elementFormOpen == true)
                {
                    // Selection of elements
                    if (e.Button == MouseButtons.Left)
                    {
                        add_remove_selection(3, true);
                    }
                    else
                    {
                        add_remove_selection(3, false);
                    }
                }
            }
            mt_pic.Refresh();
        }

        private void main_pic_MouseDown(object sender, MouseEventArgs e)
        {
            // Mouse up for Click and drag
            if (e.Button == MouseButtons.Middle)
            {
                // Pan operation stops
                static_parameters.is_middle_drag = true;
                static_parameters.main_pic_panstartdrag = new PointF(co_functions.tosingle((e.X / static_parameters.zm) - static_parameters.main_pic_midpt.X),
                                                                        co_functions.tosingle((e.Y / static_parameters.zm) - static_parameters.main_pic_midpt.Y));

                mt_pic.Refresh();
            }

            if ((e.Button == MouseButtons.Left || e.Button == MouseButtons.Right) && static_parameters.is_childFormOpen == true)
            {
                static_parameters.main_pic_start_pt = new PointF(co_functions.tosingle((e.X / static_parameters.zm) - static_parameters.main_pic_midpt.X),
                                                                      co_functions.tosingle((e.Y / static_parameters.zm) - static_parameters.main_pic_midpt.Y));
                static_parameters.is_selection_flg = true;
                //if (static_parameters.is_nodeFormOpen == true)
                //{
                //    // Selection of nodes
                //}
                //else if (static_parameters.is_edgeFormOpen == true)
                //{
                //    // Selection of edges
                //}
                //else if (static_parameters.is_elementFormOpen == true)
                //{
                //    // Selection of elements
                //}
            }
        }

        private void main_pic_MouseEnter(object sender, EventArgs e)
        {
            // Mouse Enter to get the focus to main pic
            if (static_parameters.is_childFormOpen == false)
            {
                main_pic.Focus();
            }

        }

        private void main_pic_KeyDown(object sender, KeyEventArgs e)
        {
            // Key down event handlers
            if (e.Shift == true)
            {
                static_parameters.is_shiftKeyDown = true;
            }
        }

        private void main_pic_KeyUp(object sender, KeyEventArgs e)
        {
            // key Up event
            static_parameters.is_shiftKeyDown = false;
        }

        public void paint_selection_boundary(ref Graphics gr0)
        {
            if (static_parameters.is_selection_flg == true)
            {
                Pen selection_bndry_pen = new Pen(Color.IndianRed, co_functions.tosingle(1.0f / static_parameters.zm));
                Pen selection_fill_pen = new Pen(Color.FromArgb(50, Color.Crimson));

                // Paint selection rectangle
                if (static_parameters.is_square_select == true)
                {
                    PointF pt1 = static_parameters.main_pic_start_pt;
                    PointF pt2 = new PointF(static_parameters.main_pic_start_pt.X, static_parameters.main_pic_cur_pt.Y);
                    PointF pt3 = static_parameters.main_pic_cur_pt;
                    PointF pt4 = new PointF(static_parameters.main_pic_cur_pt.X, static_parameters.main_pic_start_pt.Y);

                    float corner_x = Math.Min(Math.Min(Math.Min(pt1.X, pt2.X), pt3.X), pt4.X);
                    float corner_y = Math.Min(Math.Min(Math.Min(pt1.Y, pt2.Y), pt3.Y), pt4.Y);

                    PointF corner_pt = new PointF(corner_x, corner_y);

                    float width_x = Math.Abs(pt1.X - pt3.X);
                    float width_y = Math.Abs(pt1.Y - pt3.Y);

                    SizeF width_size = new SizeF(width_x, width_y);


                    gr0.DrawRectangle(selection_bndry_pen, corner_pt.X, corner_pt.Y, width_size.Width, width_size.Height);
                    gr0.FillRectangle(selection_fill_pen.Brush, corner_pt.X, corner_pt.Y, width_size.Width, width_size.Height);
                }
                else
                {
                    // Paint selection circle
                    PointF pt1 = static_parameters.main_pic_start_pt;
                    PointF pt2 = new PointF(static_parameters.main_pic_start_pt.X, static_parameters.main_pic_cur_pt.Y);
                    PointF pt3 = static_parameters.main_pic_cur_pt;
                    PointF pt4 = new PointF(static_parameters.main_pic_cur_pt.X, static_parameters.main_pic_start_pt.Y);

                    float diamf = co_functions.tosingle(Math.Sqrt(Math.Pow(pt1.X - pt3.X, 2) + Math.Pow(pt1.Y - pt3.Y, 2)));

                    PointF center_pt = new PointF(co_functions.tosingle((pt1.X + pt3.X) * 0.5), co_functions.tosingle((pt1.Y + pt3.Y) * 0.5));
                    PointF corner_pt = new PointF(co_functions.tosingle(center_pt.X - (diamf * 0.5)), co_functions.tosingle(center_pt.Y - (diamf * 0.5)));

                    gr0.DrawEllipse(selection_bndry_pen, new RectangleF(corner_pt, new SizeF(diamf, diamf)));
                    gr0.FillEllipse(selection_fill_pen.Brush, new RectangleF(corner_pt, new SizeF(diamf, diamf)));
                }
            }
        }

        public void add_remove_selection(int selection_case, bool is_add)
        {
            if (static_parameters.is_shiftKeyDown == false)
            {
                // shift key is not down so clear the list
                fe_objects.selection_index.Clear();
            }

            double scale_xy = main_pic.Width > main_pic.Height ? (main_pic.Height * 0.8) : (main_pic.Width * 0.8);
            double model_extent_xy = fe_objects.main_mesh.model_extent_x > fe_objects.main_mesh.model_extent_y ? fe_objects.main_mesh.model_extent_x : fe_objects.main_mesh.model_extent_y;
            double paint_scale = co_functions.tosingle(scale_xy / model_extent_xy);


            // Get the boundary
            if (static_parameters.is_square_select == true)
            {
                // selection rectangle
                PointF pt1 = static_parameters.main_pic_start_pt;
                PointF pt2 = new PointF(static_parameters.main_pic_start_pt.X, static_parameters.main_pic_cur_pt.Y);
                PointF pt3 = static_parameters.main_pic_cur_pt;
                PointF pt4 = new PointF(static_parameters.main_pic_cur_pt.X, static_parameters.main_pic_start_pt.Y);

                float corner_x = Math.Min(Math.Min(Math.Min(pt1.X, pt2.X), pt3.X), pt4.X);
                float corner_y = Math.Min(Math.Min(Math.Min(pt1.Y, pt2.Y), pt3.Y), pt4.Y);

                PointF corner_pt = new PointF(corner_x, corner_y);

                float width_x = Math.Abs(pt1.X - pt3.X);
                float width_y = Math.Abs(pt1.Y - pt3.Y);

                SizeF width_size = new SizeF(width_x, width_y);

                RectangleF selection_rect = new RectangleF(corner_pt, width_size);
                int i;


                if (selection_case == 1)
                {
                    // Node select
                    for (i = 0; i < fe_objects.main_mesh.all_points.Count; i++)
                    {
                        PointF pt = fe_objects.main_mesh.all_points[i].get_paint_point(fe_objects.main_mesh.model_mid_pt, paint_scale);
                        if (is_add == true)
                        {
                            // add the selection index
                            // Find whether the node is inside the selection rectangle
                            if (selection_rect.Contains(pt) == true)
                            {
                                if (fe_objects.selection_index.Contains(i) == false)
                                {
                                    fe_objects.selection_index.Add(i);
                                }
                            }
                        }
                        else
                        {
                            // remove the selection index
                            // Find whether the node is inside the selection rectangle
                            if (selection_rect.Contains(pt) == true)
                            {
                                if (fe_objects.selection_index.Contains(i) == true)
                                {
                                    fe_objects.selection_index.Remove(i);
                                }
                            }
                        }
                    }
                }
                else if (selection_case == 2)
                {
                    // Edge select
                    for (i = 0; i < fe_objects.main_mesh.all_edges.Count; i++)
                    {
                        PointF s_pt = fe_objects.main_mesh.all_edges[i].start_pt.get_paint_point(fe_objects.main_mesh.model_mid_pt, paint_scale);
                        PointF e_pt = fe_objects.main_mesh.all_edges[i].end_pt.get_paint_point(fe_objects.main_mesh.model_mid_pt, paint_scale);

                        //PointF pt_0_2 = co_functions.line_parametric_t(s_pt, e_pt, 0.2);
                        //PointF pt_0_4 = co_functions.line_parametric_t(s_pt, e_pt, 0.4);
                        //PointF pt_0_6 = co_functions.line_parametric_t(s_pt, e_pt, 0.6);
                        //PointF pt_0_8 = co_functions.line_parametric_t(s_pt, e_pt, 0.8);

                        PointF pt_0_3 = co_functions.line_parametric_t(s_pt, e_pt, 0.3);
                        PointF pt_0_5 = co_functions.line_parametric_t(s_pt, e_pt, 0.4);
                        PointF pt_0_7 = co_functions.line_parametric_t(s_pt, e_pt, 0.7);

                        if (is_add == true)
                        {
                            // add the selection index
                            // Find whether the node is inside the selection rectangle
                            if (selection_rect.Contains(pt_0_3) == true ||
                                selection_rect.Contains(pt_0_5) == true ||
                                selection_rect.Contains(pt_0_7) == true)
                            {
                                if (fe_objects.selection_index.Contains(i) == false)
                                {
                                    fe_objects.selection_index.Add(i);
                                }
                            }
                        }
                        else
                        {
                            // remove the selection index
                            // Find whether the node is inside the selection rectangle
                            if (selection_rect.Contains(pt_0_3) == true ||
                                selection_rect.Contains(pt_0_5) == true ||
                                selection_rect.Contains(pt_0_7) == true)
                            {
                                if (fe_objects.selection_index.Contains(i) == true)
                                {
                                    fe_objects.selection_index.Remove(i);
                                }
                            }
                        }
                    }
                }
                else if (selection_case == 3)
                {
                    // Element select
                    for (i = 0; i < fe_objects.main_mesh.all_triangles.Count; i++)
                    {
                        pslg_datastructure.point2d t_p1 = new pslg_datastructure.point2d(-1, fe_objects.main_mesh.all_triangles[i].mid_pt.x * 0.5 + fe_objects.main_mesh.all_triangles[i].vertices[0].x * 0.5,
                                                                                          fe_objects.main_mesh.all_triangles[i].mid_pt.y * 0.5 + fe_objects.main_mesh.all_triangles[i].vertices[0].y * 0.5);
                        pslg_datastructure.point2d t_p2 = new pslg_datastructure.point2d(-1, fe_objects.main_mesh.all_triangles[i].mid_pt.x * 0.5 + fe_objects.main_mesh.all_triangles[i].vertices[1].x * 0.5,
                                                                                          fe_objects.main_mesh.all_triangles[i].mid_pt.y * 0.5 + fe_objects.main_mesh.all_triangles[i].vertices[1].y * 0.5);
                        pslg_datastructure.point2d t_p3 = new pslg_datastructure.point2d(-1, fe_objects.main_mesh.all_triangles[i].mid_pt.x * 0.5 + fe_objects.main_mesh.all_triangles[i].vertices[2].x * 0.5,
                                                                                          fe_objects.main_mesh.all_triangles[i].mid_pt.y * 0.5 + fe_objects.main_mesh.all_triangles[i].vertices[2].y * 0.5);

                        PointF p1 = t_p1.get_paint_point(fe_objects.main_mesh.model_mid_pt, paint_scale);
                        PointF p2 = t_p2.get_paint_point(fe_objects.main_mesh.model_mid_pt, paint_scale);
                        PointF p3 = t_p3.get_paint_point(fe_objects.main_mesh.model_mid_pt, paint_scale);
                        PointF m1 = fe_objects.main_mesh.all_triangles[i].mid_pt.get_paint_point(fe_objects.main_mesh.model_mid_pt, paint_scale);

                        if (is_add == true)
                        {
                            // add the selection index
                            // Find whether the node is inside the selection rectangle
                            if (selection_rect.Contains(p1) == true ||
                                selection_rect.Contains(p2) == true ||
                                selection_rect.Contains(p3) == true ||
                                selection_rect.Contains(m1) == true)
                            {
                                if (fe_objects.selection_index.Contains(i) == false)
                                {
                                    fe_objects.selection_index.Add(i);
                                }
                            }
                        }
                        else
                        {
                            // remove the selection index
                            // Find whether the node is inside the selection rectangle
                            if (selection_rect.Contains(p1) == true ||
                                selection_rect.Contains(p2) == true ||
                                selection_rect.Contains(p3) == true ||
                                selection_rect.Contains(m1) == true)
                            {
                                if (fe_objects.selection_index.Contains(i) == true)
                                {
                                    fe_objects.selection_index.Remove(i);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                // selection circle
                PointF pt1 = static_parameters.main_pic_start_pt;
                PointF pt2 = new PointF(static_parameters.main_pic_start_pt.X, static_parameters.main_pic_cur_pt.Y);
                PointF pt3 = static_parameters.main_pic_cur_pt;
                PointF pt4 = new PointF(static_parameters.main_pic_cur_pt.X, static_parameters.main_pic_start_pt.Y);

                float diamf = co_functions.tosingle(Math.Sqrt(Math.Pow(pt1.X - pt3.X, 2) + Math.Pow(pt1.Y - pt3.Y, 2)));
                double selection_radius = diamf * 0.5;

                PointF center_pt = new PointF(co_functions.tosingle((pt1.X + pt3.X) * 0.5), co_functions.tosingle((pt1.Y + pt3.Y) * 0.5));
                PointF corner_pt = new PointF(co_functions.tosingle(center_pt.X - (diamf * 0.5)), co_functions.tosingle(center_pt.Y - (diamf * 0.5)));
                int i;

                if (selection_case == 1)
                {
                    // Node select
                    for (i = 0; i < fe_objects.main_mesh.all_points.Count; i++)
                    {
                        PointF pt = fe_objects.main_mesh.all_points[i].get_paint_point(fe_objects.main_mesh.model_mid_pt, paint_scale);
                        double pt_dist = Math.Sqrt((Math.Pow(pt.X - center_pt.X, 2) + Math.Pow(pt.Y - center_pt.Y, 2)));
                        if (is_add == true)
                        {
                            // add the selection index
                            // Find whether the node is inside the selection rectangle
                            if (pt_dist < selection_radius)
                            {
                                if (fe_objects.selection_index.Contains(i) == false)
                                {
                                    fe_objects.selection_index.Add(i);
                                }
                            }
                        }
                        else
                        {
                            // remove the selection index
                            // Find whether the node is inside the selection rectangle
                            if (pt_dist < selection_radius)
                            {
                                if (fe_objects.selection_index.Contains(i) == true)
                                {
                                    fe_objects.selection_index.Remove(i);
                                }
                            }
                        }
                    }
                }
                else if (selection_case == 2)
                {
                    // Edge select
                    for (i = 0; i < fe_objects.main_mesh.all_edges.Count; i++)
                    {
                        PointF s_pt = fe_objects.main_mesh.all_edges[i].start_pt.get_paint_point(fe_objects.main_mesh.model_mid_pt, paint_scale);
                        PointF e_pt = fe_objects.main_mesh.all_edges[i].end_pt.get_paint_point(fe_objects.main_mesh.model_mid_pt, paint_scale);

                        //PointF pt_0_2 = co_functions.line_parametric_t(s_pt, e_pt, 0.2);
                        //PointF pt_0_4 = co_functions.line_parametric_t(s_pt, e_pt, 0.4);
                        //PointF pt_0_6 = co_functions.line_parametric_t(s_pt, e_pt, 0.6);
                        //PointF pt_0_8 = co_functions.line_parametric_t(s_pt, e_pt, 0.8);

                        PointF pt_0_3 = co_functions.line_parametric_t(s_pt, e_pt, 0.3);
                        PointF pt_0_5 = co_functions.line_parametric_t(s_pt, e_pt, 0.4);
                        PointF pt_0_7 = co_functions.line_parametric_t(s_pt, e_pt, 0.7);


                        double pt_dist1 = Math.Sqrt((Math.Pow(pt_0_3.X - center_pt.X, 2) + Math.Pow(pt_0_3.Y - center_pt.Y, 2)));
                        double pt_dist2 = Math.Sqrt((Math.Pow(pt_0_5.X - center_pt.X, 2) + Math.Pow(pt_0_5.Y - center_pt.Y, 2)));
                        double pt_dist3 = Math.Sqrt((Math.Pow(pt_0_7.X - center_pt.X, 2) + Math.Pow(pt_0_7.Y - center_pt.Y, 2)));
                        //double pt_dist4 = Math.Sqrt((Math.Pow(pt_0_4.X - center_pt.X, 2) + Math.Pow(pt_0_4.Y - center_pt.Y, 2)));
                        //double pt_dist5 = Math.Sqrt((Math.Pow(pt_0_6.X - center_pt.X, 2) + Math.Pow(pt_0_6.Y - center_pt.Y, 2)));
                        //double pt_dist6 = Math.Sqrt((Math.Pow(pt_0_8.X - center_pt.X, 2) + Math.Pow(pt_0_8.Y - center_pt.Y, 2)));

                        if (is_add == true)
                        {
                            // add the selection index
                            // Find whether the node is inside the selection rectangle
                            if (pt_dist1 < selection_radius ||
                                pt_dist2 < selection_radius ||
                                pt_dist3 < selection_radius)
                            {
                                if (fe_objects.selection_index.Contains(i) == false)
                                {
                                    fe_objects.selection_index.Add(i);
                                }
                            }
                        }
                        else
                        {
                            // remove the selection index
                            // Find whether the node is inside the selection rectangle
                            if (pt_dist1 < selection_radius ||
                                pt_dist2 < selection_radius ||
                                pt_dist3 < selection_radius)
                            {
                                if (fe_objects.selection_index.Contains(i) == true)
                                {
                                    fe_objects.selection_index.Remove(i);
                                }
                            }
                        }
                    }
                }
                else if (selection_case == 3)
                {
                    // Element select
                    // Element select
                    for (i = 0; i < fe_objects.main_mesh.all_triangles.Count; i++)
                    {
                        pslg_datastructure.point2d t_p1 = new pslg_datastructure.point2d(-1, fe_objects.main_mesh.all_triangles[i].mid_pt.x * 0.5 + fe_objects.main_mesh.all_triangles[i].vertices[0].x * 0.5,
                                                                                          fe_objects.main_mesh.all_triangles[i].mid_pt.y * 0.5 + fe_objects.main_mesh.all_triangles[i].vertices[0].y * 0.5);
                        pslg_datastructure.point2d t_p2 = new pslg_datastructure.point2d(-1, fe_objects.main_mesh.all_triangles[i].mid_pt.x * 0.5 + fe_objects.main_mesh.all_triangles[i].vertices[1].x * 0.5,
                                                                                          fe_objects.main_mesh.all_triangles[i].mid_pt.y * 0.5 + fe_objects.main_mesh.all_triangles[i].vertices[1].y * 0.5);
                        pslg_datastructure.point2d t_p3 = new pslg_datastructure.point2d(-1, fe_objects.main_mesh.all_triangles[i].mid_pt.x * 0.5 + fe_objects.main_mesh.all_triangles[i].vertices[2].x * 0.5,
                                                                                          fe_objects.main_mesh.all_triangles[i].mid_pt.y * 0.5 + fe_objects.main_mesh.all_triangles[i].vertices[2].y * 0.5);

                        PointF p1 = t_p1.get_paint_point(fe_objects.main_mesh.model_mid_pt, paint_scale);
                        PointF p2 = t_p2.get_paint_point(fe_objects.main_mesh.model_mid_pt, paint_scale);
                        PointF p3 = t_p3.get_paint_point(fe_objects.main_mesh.model_mid_pt, paint_scale);
                        PointF m1 = fe_objects.main_mesh.all_triangles[i].mid_pt.get_paint_point(fe_objects.main_mesh.model_mid_pt, paint_scale);

                        double pt_dist1 = Math.Sqrt((Math.Pow(p1.X - center_pt.X, 2) + Math.Pow(p1.Y - center_pt.Y, 2)));
                        double pt_dist2 = Math.Sqrt((Math.Pow(p2.X - center_pt.X, 2) + Math.Pow(p2.Y - center_pt.Y, 2)));
                        double pt_dist3 = Math.Sqrt((Math.Pow(p3.X - center_pt.X, 2) + Math.Pow(p3.Y - center_pt.Y, 2)));
                        double pt_dist4 = Math.Sqrt((Math.Pow(m1.X - center_pt.X, 2) + Math.Pow(m1.Y - center_pt.Y, 2)));

                        if (is_add == true)
                        {
                            // add the selection index
                            // Find whether the node is inside the selection rectangle
                            if (pt_dist1 < selection_radius ||
                                pt_dist2 < selection_radius ||
                                pt_dist3 < selection_radius ||
                                pt_dist4 < selection_radius)
                            {
                                if (fe_objects.selection_index.Contains(i) == false)
                                {
                                    fe_objects.selection_index.Add(i);
                                }
                            }
                        }
                        else
                        {
                            // remove the selection index
                            // Find whether the node is inside the selection rectangle
                            if (pt_dist1 < selection_radius ||
                                pt_dist2 < selection_radius ||
                                pt_dist3 < selection_radius ||
                                pt_dist4 < selection_radius)
                            {
                                if (fe_objects.selection_index.Contains(i) == true)
                                {
                                    fe_objects.selection_index.Remove(i);
                                }
                            }
                        }
                    }
                }
            }



        }
        #endregion

    }
}
