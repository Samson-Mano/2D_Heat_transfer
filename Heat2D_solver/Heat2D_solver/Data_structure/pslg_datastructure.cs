using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Heat2D_solver.Useful_Function;
using System.Drawing.Drawing2D;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Factorization;

namespace Heat2D_solver.Data_structure
{
    [Serializable]
    public class pslg_datastructure
    {
        private mesh2d _main_mesh = new mesh2d();
        public List<int> selection_index = new List<int>();
        // private double paint_scale = 1.0f;
        //private double _model_extent_x;
        //private double _model_extent_y;

        public mesh2d main_mesh
        {
            get { return this._main_mesh; }
        }

        public pslg_datastructure()
        {
            // Empty constructor
        }

        public pslg_datastructure(mesh2d i_main_mesh)
        {
            this._main_mesh = i_main_mesh;
        }

        [Serializable]
        public class mesh2d
        {
            List<point2d> _all_points = new List<point2d>(); // List of point object to store all the points in the drawing area
            List<edge2d> _all_edges = new List<edge2d>(); // List of edge object to store all the edges created from Delaunay triangulation
            List<triangle2d> _all_triangles = new List<triangle2d>(); // List of face object to store all the faces created from Delaunay triangulation
            result_store result_values = new result_store(); // Variable to contain all the results

            PointF _model_mid_pt;
            double _model_extent_x;
            double _model_extent_y;

            bool _analysis_complete = false;

            public List<point2d> all_points
            {
                get { return this._all_points; }
            }

            public List<edge2d> all_edges
            {
                get { return this._all_edges; }
            }

            public List<triangle2d> all_triangles
            {
                get { return this._all_triangles; }
            }

            public double model_extent_x
            {
                get { return this._model_extent_x; }
            }

            public double model_extent_y
            {
                get { return this._model_extent_y; }
            }

            public PointF model_mid_pt
            {
                get { return this._model_mid_pt; }
            }

            public bool is_analysis_complete
            {
                get { return this._analysis_complete; }
            }

            public mesh2d()
            {
                // Empty constructor
            }

            public mesh2d(List<point2d> i_all_pts, List<edge2d> i_all_edg, List<triangle2d> i_all_fcs)
            {
                this._all_points = i_all_pts;
                this._all_edges = i_all_edg;
                this._all_triangles = i_all_fcs;

                // Set the model extent
                double min_xval = double.MaxValue, min_yval = double.MaxValue, max_xval = double.MinValue, max_yval = double.MinValue;
                foreach (point2d nd in i_all_pts)
                {
                    if (min_xval > nd.x)
                        min_xval = nd.x;

                    if (min_yval > nd.y)
                        min_yval = nd.y;

                    if (max_xval < nd.x)
                        max_xval = nd.x;

                    if (max_yval < nd.y)
                        max_yval = nd.y;
                }

                // Length of model extent
                _model_extent_x = (max_xval - min_xval);
                _model_extent_y = (max_yval - min_yval);
                _model_mid_pt = new PointF(co_functions.tosingle(min_xval + (model_extent_x * 0.5)), co_functions.tosingle(min_yval + (model_extent_y * 0.5)));

                _analysis_complete = false;
            }

            public void set_nodal_constraint(List<int> selection_index, double i_heat_source, double i_spec_temp)
            {
                foreach (int i in selection_index)
                {
                    // add the heat source values to nodes
                    this._all_points[i].set_constraint(i_heat_source, i_spec_temp);
                }
            }

            public void set_temperature_label(point2d i_label_node)
            {
                // Add the result label to the model
                result_values.create_label(i_label_node);
            }

            public void remove_temperature_label(int iremove_type)
            {
                // remove the result label
                result_values.remove_label(iremove_type);
            }

            public void set_edge_constraint(List<int> selection_index, double i_heat_source, double i_specified_temp, double i_heat_transfer_coeff, double i_ambient_temp)
            {
                foreach (int i in selection_index)
                {
                    // add the heat source, specified temperature, heat transfer co-eff, ambient temp t_inf values to edges
                    this._all_edges[i].set_constraint(i_heat_source, i_specified_temp, i_heat_transfer_coeff, i_ambient_temp);
                }
            }

            public void set_element_constraint(List<int> selection_index, double i_heat_source, double i_specified_temp, double i_heat_transfer_coeff, double i_ambient_temp, double i_thermal_cond_x, double i_thermal_cond_y, double i_elmnt_thickness)
            {
                foreach (int i in selection_index)
                {
                    // add the heat source, specified temperature, heat transfer co-eff, ambient temp t_inf, thermal conductivity values to elements
                    this._all_triangles[i].set_constraint(i_heat_source, i_specified_temp, i_heat_transfer_coeff, i_ambient_temp, i_thermal_cond_x, i_thermal_cond_y, i_elmnt_thickness);
                }
            }

            public void set_analysis_results(result_store i_rslt_values)
            {
                this.result_values = i_rslt_values;
                this._analysis_complete = true;
            }

            public void paint_me(ref Graphics gr0, ref GraphicsState bef_trans, double paint_scale) // this function is used to paint the mesh
            {
                Graphics gr1 = gr0;

                if (_analysis_complete == true && static_parameters.show_analysis_result == true)
                {
                    // paint the results
                    gr1.SmoothingMode = SmoothingMode.None;
                    result_values.paint_me(ref gr1, ref bef_trans, model_mid_pt, ref paint_scale);
                }
                else
                {
                    Pen temp_tri_pen = new Pen(Color.Lavender, 1);
                    all_triangles.ForEach(obj => obj.paint_me(ref gr1, model_mid_pt, ref paint_scale, ref temp_tri_pen)); // Paint the faces

                    Pen temp_edge_pen = new Pen(Color.DarkOrange, 1);
                    all_edges.ForEach(obj => obj.paint_me(ref gr1, model_mid_pt, ref paint_scale, temp_edge_pen)); // Paint the edges

                    Pen temp_node_pen = new Pen(Color.DarkRed, 2);
                    all_points.ForEach(obj => obj.paint_me(ref gr1, model_mid_pt, ref paint_scale, ref temp_node_pen)); // Paint the nodes
                }
            }

            public void paint_selection(ref Graphics gr0, double paint_scale, int selection_case, List<int> selection_index)
            {
                Graphics gr1 = gr0;

                if (selection_case == 1)
                {
                    // Node select
                    foreach (int i in selection_index)
                    {
                        Pen temp_node_pen = new Pen(Color.DarkRed, 1);
                        all_points[i].paint_selection(ref gr1, model_mid_pt, ref paint_scale, temp_node_pen);
                    }
                }
                else if (selection_case == 2)
                {
                    // Edge select
                    foreach (int i in selection_index)
                    {
                        Pen temp_edge_pen = new Pen(Color.DarkRed, 3);
                        all_edges[i].paint_selection(ref gr1, model_mid_pt, ref paint_scale, ref temp_edge_pen);
                    }
                }
                else if (selection_case == 3)
                {
                    // Element select
                    foreach (int i in selection_index)
                    {
                        Pen temp_tri_pen = new Pen(Color.DarkRed, 1);
                        all_triangles[i].paint_selection(ref gr1, model_mid_pt, ref paint_scale, ref temp_tri_pen);
                    }
                }
            }

        }

        [Serializable]
        public class point2d // class to store the points
        {
            int _id;
            double _x;
            double _y;

            // heat flux q (+ive away from normal -ive towards the normal) Heat supplied rate (W/m2)
            double _heat_source = 0.0f;
            // specified temperature deg C
            double _spec_temperature = 0.0f;

            public int id
            {
                get { return this._id; }
            }

            public double x
            {
                get { return this._x; }
            }

            public double y
            {
                get { return this._y; }
            }

            public double heat_source
            {
                // heat flux q (+ive away from normal -ive towards the normal) Heat supplied rate (W/m2)
                get { return this._heat_source; }
            }

            public double spec_temp
            {
                // specified temperature
                get { return this._spec_temperature; }
            }


            public point2d(int i_id, double i_x, double i_y)
            {
                // constructor 1
                this._id = i_id;
                this._x = i_x;
                this._y = i_y;
            }

            public void set_constraint(double i_heat_source, double i_specified_temp)
            {
                // Add the heat source
                this._heat_source = i_heat_source;
                // Add the specified temperature
                this._spec_temperature = i_specified_temp;
            }

            public void paint_me(ref Graphics gr0, PointF model_mid_pt, ref double paint_scale, ref Pen node_pen) // this function is used to paint the points
            {
                gr0.FillEllipse(node_pen.Brush, new RectangleF(get_point_for_ellipse(model_mid_pt, 1, paint_scale), new SizeF(2, 2)));

                if (static_parameters.show_node_values == true)
                {
                    // Show node value is on
                    if (heat_source != 0 || spec_temp != 0)
                    {
                        paint_heat_source(gr0, model_mid_pt, paint_scale);
                    }
                }
            }

            private void paint_heat_source(Graphics gr0, PointF model_mid_pt, double paint_scale)
            {

                Pen load_pen = new Pen(Color.Green, 1);
                PointF nd_pt = get_paint_point(model_mid_pt, paint_scale);
                //  gr0.DrawEllipse(load_pen, new RectangleF(get_point_for_ellipse(model_mid_pt, 2, paint_scale), new SizeF(4, 4)));
                if (heat_source > 0)
                {
                    // heat flow away from the node
                    // Make a GraphicsPath to define the end cap.
                    GraphicsPath end_path = new GraphicsPath();
                    end_path.AddLine(0, 0, -1.5f, -1.5f);
                    end_path.AddLine(0, 0, 1.5f, -1.5f);

                    // Make the end cap.
                    CustomLineCap end_cap = new CustomLineCap(null, end_path);
                    load_pen.CustomEndCap = end_cap;

                    double degrees = 45;
                    for (int i = 0; i < 4; i++)
                    {
                        PointF pt1 = co_functions.rotate_point(2, 0, nd_pt.X, nd_pt.Y, degrees);
                        PointF pt2 = co_functions.rotate_point(6, 0, nd_pt.X, nd_pt.Y, degrees);

                        gr0.DrawLine(load_pen, pt1, pt2);

                        degrees = degrees + 90;
                    }
                }
                else
                {
                    // heat flow towards the node
                    // Make a GraphicsPath to define the start cap.
                    GraphicsPath start_path = new GraphicsPath();
                    start_path.AddLine(0, 0, -1, -1);
                    start_path.AddLine(0, 0, 1, -1);

                    // Make the start cap.
                    CustomLineCap start_cap = new CustomLineCap(null, start_path);
                    load_pen.CustomStartCap = start_cap;

                    double degrees = 45;
                    for (int i = 0; i < 4; i++)
                    {
                        PointF pt1 = co_functions.rotate_point(2, 0, nd_pt.X, nd_pt.Y, degrees);
                        PointF pt2 = co_functions.rotate_point(6, 0, nd_pt.X, nd_pt.Y, degrees);

                        gr0.DrawLine(load_pen, pt1, pt2);

                        degrees = degrees + 90;
                    }
                }

                // Paint the heat_source value
                if (static_parameters.show_values == true)
                {
                    string str = "";
                    if (heat_source != 0.0)
                    {
                        str = "q = " + heat_source.ToString();
                    }

                    if (spec_temp != 0.0)
                    {
                        str = str + "T = " + spec_temp.ToString();
                    }

                    gr0.DrawString(str, new Font("Cambria", 4), load_pen.Brush, nd_pt);
                }
            }


            public void paint_selection(ref Graphics gr0, PointF model_mid_pt, ref double paint_scale, Pen node_pen) // this function is used to paint the points
            {
                gr0.FillEllipse(node_pen.Brush, new RectangleF(get_point_for_ellipse(model_mid_pt, 3, paint_scale), new SizeF(6, 6)));
            }

            public PointF get_point_for_ellipse(PointF model_mid_pt, int ellipse_offset, double model_scale)
            {
                return (new PointF(co_functions.tosingle((this._x - model_mid_pt.X) * model_scale - ellipse_offset),
                                   co_functions.tosingle(-1 * (this._y - model_mid_pt.Y) * model_scale - ellipse_offset))); // return the point as PointF as edge of an ellipse
            }

            public PointF get_point()
            {
                return (new PointF(co_functions.tosingle(this._x),
                                   co_functions.tosingle(this._y))); // return the point as PointF as edge of an ellipse
            }

            public PointF get_paint_point(PointF model_mid_pt, double model_scale)
            {
                return (new PointF(co_functions.tosingle((this._x - model_mid_pt.X) * model_scale),
                                   -1 * co_functions.tosingle((this._y - model_mid_pt.Y) * model_scale))); // return the point as PointF as edge of an ellipse
            }

            public bool Equals(point2d other)
            {
                return (Math.Abs(this._x - other.x) < static_parameters.eps && Math.Abs(this._y - other.y) < static_parameters.eps); // Equal function is used to check the uniqueness of the points added
            }
        }

        [Serializable]
        public class edge2d
        {
            int _edge_id;
            point2d _start_pt;
            point2d _end_pt;
            point2d _mid_pt; // not stored in point list
            int _left_tri_id;
            int _right_tri_id;

            // specified temperature T (deg C)
            double _specified_temp;
            // heat flux q (+ive away from normal -ive towards the normal) Heat supplied rate (W/m2)
            double _heat_source = 0.0f;
            // heat transfer co-efficient h (W/m2 deg C)
            double _heat_transfer_coeff;
            // Ambient temperature T infinite (T_inf) (deg C)
            double _ambient_temp;


            public int edge_id
            {
                get { return this._edge_id; }
            }

            public point2d start_pt
            {
                get { return this._start_pt; }
            }

            public point2d end_pt
            {
                get { return this._end_pt; }
            }

            public point2d mid_pt
            {
                get { return this._mid_pt; }
            }

            public int left_tri_id
            {
                get { return this._left_tri_id; }
            }

            public int right_tri_id
            {
                get { return this._right_tri_id; }
            }

            public double edge_length
            {
                get { return Math.Sqrt(Math.Pow(_start_pt.x - _end_pt.x, 2) + Math.Pow(_start_pt.y - _end_pt.y, 2)); }
            }

            public double heat_source
            {
                // heat flux q (+ive away from normal -ive towards the normal) Heat supplied rate (W/m2)
                get { return this._heat_source; }
            }

            public double specified_temp
            {
                // specified temperature T (deg C)
                get { return this._specified_temp; }
            }

            public double heat_transfer_coeff
            {
                // heat transfer co-efficient h (W/m2 deg C)
                get { return this._heat_transfer_coeff; }
            }

            public double ambient_temp
            {
                // Ambient temperature T infinite (T_inf) (deg C)
                get { return this._ambient_temp; }
            }

            public edge2d(int i_edge_id, point2d i_start_pt, point2d i_end_pt)
            {
                // constructor 1
                this._edge_id = i_edge_id;
                this._start_pt = i_start_pt;
                this._end_pt = i_end_pt;
                this._mid_pt = new point2d(-1, (i_start_pt.x + i_end_pt.x) * 0.5, (i_start_pt.y + i_end_pt.y) * 0.5);
                this._left_tri_id = -1;
                this._right_tri_id = -1;
            }

            public void set_constraint(double i_heat_source, double i_specified_temp, double i_heat_transfer_coeff, double i_ambient_temp)
            {
                // heat flux q (+ive away from normal -ive towards the normal) Heat supplied rate (W/m2)
                this._heat_source = i_heat_source;
                // specified temperature T (deg C)
                this._specified_temp = i_specified_temp;
                // heat transfer co-efficient h (W/m2 deg C)
                this._heat_transfer_coeff = i_heat_transfer_coeff;
                // Ambient temperature T infinite (T_inf) (deg C)
                this._ambient_temp = i_ambient_temp;
            }

            public void paint_me(ref Graphics gr0, PointF model_mid_pt, ref double paint_scale, Pen edge_pen) // this function is used to paint the points
            {
                Color pen_color = Color.DarkOrange;
                if (left_tri_id == -1 || right_tri_id == -1)
                {
                    edge_pen.Width = 1;
                    edge_pen.Color = Color.OrangeRed;
                }
                else
                {
                    edge_pen.Width = 1;
                    edge_pen.Color = pen_color;
                }

                gr0.DrawLine(edge_pen, start_pt.get_paint_point(model_mid_pt, paint_scale), end_pt.get_paint_point(model_mid_pt, paint_scale));

                if (static_parameters.show_edge_values == true)
                {
                    // Show node value is on
                    string str = "";
                    // Paint heat source
                    if (heat_source != 0)
                    {
                        paint_heat_source(gr0, model_mid_pt, paint_scale, ref str);
                    }
                    // Paint specified temperature
                    if (specified_temp != 0)
                    {
                        paint_specified_temp(gr0, model_mid_pt, paint_scale, ref str);
                    }
                    // Paint ambient temperature
                    if (ambient_temp != 0 && heat_transfer_coeff != 0)
                    {
                        paint_ambient_temp(gr0, model_mid_pt, paint_scale, ref str);
                    }

                    // Paint the string
                    if (static_parameters.show_values == true)
                    {
                        if (str != "")
                        {
                            Pen load_pen = new Pen(Color.Green, 1);
                            PointF nd_pt = mid_pt.get_paint_point(model_mid_pt, paint_scale);
                            gr0.DrawString(str, new Font("Cambria", 4), load_pen.Brush, nd_pt);
                        }
                    }

                }

                //gr0.DrawLine(new Pen(edge_pen.Color,edge_pen.Width),mid_pt.get_point(), end_pt.get_point());

                //System.Drawing.Drawing2D.AdjustableArrowCap bigArrow = new System.Drawing.Drawing2D.AdjustableArrowCap(3, 3);
                //edge_pen.CustomEndCap = bigArrow;
                //gr0.DrawLine(edge_pen, start_pt.get_point(), mid_pt.get_point());
            }

            private void paint_heat_source(Graphics gr0, PointF model_mid_pt, double paint_scale, ref string str)
            {
                // Paint the heat_source
                Pen load_pen = new Pen(Color.Green, 1);
                PointF nd_pt = mid_pt.get_paint_point(model_mid_pt, paint_scale);
                //  gr0.DrawEllipse(load_pen, new RectangleF(get_point_for_ellipse(model_mid_pt, 2, paint_scale), new SizeF(4, 4)));
                if (heat_source > 0)
                {
                    // heat flow away from the node
                    // Make a GraphicsPath to define the end cap.
                    GraphicsPath end_path = new GraphicsPath();
                    end_path.AddLine(0, 0, -1.4f, -1.4f);
                    end_path.AddLine(0, 0, 1.4f, -1.4f);

                    // Make the end cap.
                    CustomLineCap end_cap = new CustomLineCap(null, end_path);
                    load_pen.CustomEndCap = end_cap;

                    double degrees = (co_functions.GetAngle(100, 0, 0, 0, start_pt.x - end_pt.x, end_pt.y - start_pt.y) * (180 / Math.PI)) + 90;
                    for (int i = 0; i < 2; i++)
                    {
                        PointF pt1 = co_functions.rotate_point(2, 0, nd_pt.X, nd_pt.Y, degrees);
                        PointF pt2 = co_functions.rotate_point(6, 0, nd_pt.X, nd_pt.Y, degrees);

                        gr0.DrawLine(load_pen, pt1, pt2);

                        degrees = degrees + 180;
                    }
                }
                else
                {
                    // heat flow towards the node
                    // Make a GraphicsPath to define the start cap.
                    GraphicsPath start_path = new GraphicsPath();
                    start_path.AddLine(0, 0, -1.4f, -1.4f);
                    start_path.AddLine(0, 0, 1.4f, -1.4f);

                    // Make the start cap.
                    CustomLineCap start_cap = new CustomLineCap(null, start_path);
                    load_pen.CustomStartCap = start_cap;

                    double degrees = (co_functions.GetAngle(100, 0, 0, 0, start_pt.x - end_pt.x, end_pt.y - start_pt.y) * (180 / Math.PI)) + 90;
                    for (int i = 0; i < 2; i++)
                    {
                        PointF pt1 = co_functions.rotate_point(2, 0, nd_pt.X, nd_pt.Y, degrees);
                        PointF pt2 = co_functions.rotate_point(6, 0, nd_pt.X, nd_pt.Y, degrees);

                        gr0.DrawLine(load_pen, pt1, pt2);

                        degrees = degrees + 180;
                    }
                }

                // Paint the heat_source value
                str = str + "q = " + heat_source.ToString() + "\n";
                //gr0.DrawString(str, new Font("Cambria", 6), load_pen.Brush, nd_pt);
            }

            private void paint_specified_temp(Graphics gr0, PointF model_mid_pt, double paint_scale, ref string str)
            {
                // Paint the specified temperature
                str = str + "T = " + specified_temp.ToString() + "\n";

            }

            private void paint_ambient_temp(Graphics gr0, PointF model_mid_pt, double paint_scale, ref string str)
            {
                // Paint the ambient temperature
                Pen load_pen = new Pen(Color.Green, 1);
                PointF nd_pt = mid_pt.get_paint_point(model_mid_pt, paint_scale);
                // paint the drawing right side of the edge
                GraphicsPath end_path = new GraphicsPath();
                end_path.AddLine(0, 0, -1.4f, -1.4f);
                end_path.AddLine(0, 0, 1.4f, -1.4f);

                // Make the end cap.
                CustomLineCap end_cap = new CustomLineCap(null, end_path);
                load_pen.CustomEndCap = end_cap;

                double degrees = (co_functions.GetAngle(100, 0, 0, 0, start_pt.x - end_pt.x, end_pt.y - start_pt.y) * (180 / Math.PI));
                int gp;
                double e_len = (edge_length * 0.6) * 0.5 * paint_scale;

                if (left_tri_id == -1)
                {
                    gp = 8;
                    float pe_len = co_functions.tosingle(e_len);
                    for (int i = 0; i < 3; i++)
                    {
                        PointF mpt = co_functions.rotate_point(gp, 0, nd_pt.X, nd_pt.Y, degrees + 90);
                        PointF pt1 = co_functions.rotate_point(-pe_len, 0, mpt.X, mpt.Y, degrees);
                        PointF pt2 = co_functions.rotate_point(pe_len, 0, mpt.X, mpt.Y, degrees);

                        gr0.DrawLine(load_pen, pt1, pt2);
                        pe_len = pe_len - 2;
                        gp = gp + 4;
                    }
                }
                else if (right_tri_id == -1)
                {
                    // paint the drawing left side of the edge
                    gp = 8;
                    float pe_len = co_functions.tosingle(e_len);
                    for (int i = 0; i < 3; i++)
                    {
                        PointF mpt = co_functions.rotate_point(gp, 0, nd_pt.X, nd_pt.Y, degrees - 90);
                        PointF pt1 = co_functions.rotate_point(-pe_len, 0, mpt.X, mpt.Y, degrees);
                        PointF pt2 = co_functions.rotate_point(pe_len, 0, mpt.X, mpt.Y, degrees);

                        gr0.DrawLine(load_pen, pt1, pt2);
                        pe_len = pe_len - 2;
                        gp = gp + 4;
                    }
                }
                else
                {
                    // paint the drawing both side of the edge
                    gp = 8;
                    float pe_len = co_functions.tosingle(e_len);
                    for (int i = 0; i < 3; i++)
                    {
                        PointF mpt = co_functions.rotate_point(gp, 0, nd_pt.X, nd_pt.Y, degrees - 90);
                        PointF pt1 = co_functions.rotate_point(-pe_len, 0, mpt.X, mpt.Y, degrees);
                        PointF pt2 = co_functions.rotate_point(pe_len, 0, mpt.X, mpt.Y, degrees);

                        gr0.DrawLine(load_pen, pt1, pt2);
                        pe_len = pe_len - 2;
                        gp = gp + 4;
                    }

                    gp = 8;
                    pe_len = co_functions.tosingle(e_len);
                    for (int i = 0; i < 3; i++)
                    {
                        PointF mpt = co_functions.rotate_point(gp, 0, nd_pt.X, nd_pt.Y, degrees + 90);
                        PointF pt1 = co_functions.rotate_point(-pe_len, 0, mpt.X, mpt.Y, degrees);
                        PointF pt2 = co_functions.rotate_point(pe_len, 0, mpt.X, mpt.Y, degrees);

                        gr0.DrawLine(load_pen, pt1, pt2);
                        pe_len = pe_len - 2;
                        gp = gp + 4;
                    }

                }

                str = str + "h = " + heat_transfer_coeff.ToString() + "\n" + "T_inf = " + ambient_temp.ToString() + "\n";


            }


            public void paint_selection(ref Graphics gr0, PointF model_mid_pt, ref double paint_scale, ref Pen edge_pen) // this function is used to paint the points
            {
                gr0.DrawLine(edge_pen, start_pt.get_paint_point(model_mid_pt, paint_scale), end_pt.get_paint_point(model_mid_pt, paint_scale));
            }

            public void add_triangle(int the_triangle_id, point2d tri_midpt)
            {
                if (rightof(tri_midpt, this) == true)
                {
                    // Add the right triangle
                    this._right_tri_id = the_triangle_id;
                }
                else
                {
                    // Add the left triangle
                    this._left_tri_id = the_triangle_id;
                }
            }

            private bool ccw(point2d a, point2d b, point2d c)
            {
                // Computes | a.x a.y  1 |
                //          | b.x b.y  1 | > 0
                //          | c.x c.y  1 |
                return (((b.x - a.x) * (c.y - a.y)) - ((b.y - a.y) * (c.x - a.x))) > 0;
            }

            private bool rightof(point2d x, edge2d e)
            {
                return ccw(x, e.end_pt, e.start_pt);
            }

            public bool Equals(edge2d other)
            {
                return (other.start_pt.Equals(this._start_pt) && other.end_pt.Equals(this._end_pt));
            }

            public bool Equals_without_orientation(edge2d other)
            {
                return (other.start_pt.Equals(this._start_pt) && other.end_pt.Equals(this._end_pt) || other.start_pt.Equals(this._end_pt) && other.end_pt.Equals(this._start_pt));
            }

            public bool vertex_exists(point2d other)
            {

                if (start_pt.Equals(other) == true || end_pt.Equals(other) == true)
                {
                    return true;
                }
                return false;
            }

        }

        [Serializable]
        public class triangle2d
        {
            int _face_id;
            public point2d[] vertices { get; } = new point2d[3];
            public int[] edge_id { get; } = new int[3];

            point2d _mid_pt;
            double shrink_factor = 0.6f; //
            PointF model_mid_pt;
            double paint_scale;

            // heat flux q (+ive away from normal -ive towards the normal) Heat supplied rate (W/m2)
            double _heat_source;
            // specified temperature T (deg C)
            double _specified_temp;
            // heat transfer co-efficient h (W/m2 deg C)
            double _heat_transfer_coeff;
            // Ambient temperature T infinite (T_inf) (deg C)
            double _ambient_temp;
            // Thermal conductivity kx (W/m deg C)
            double _thermal_conductivity_x;
            // Thermal conductivity ky (W/m deg C)
            double _thermal_conductivity_y;
            // element thickness
            double _element_thickness;
            // element area
            double _element_area;

            public int face_id
            {
                get { return this._face_id; }
            }

            public point2d mid_pt
            {
                get { return this._mid_pt; }
            }

            public double heat_source
            {
                // heat flux q (+ive away from normal -ive towards the normal) Heat supplied rate (W/m2)
                get { return this._heat_source; }
            }

            public double specified_temp
            {
                // specified temperature T (deg C)
                get { return this._specified_temp; }
            }

            public double heat_transfer_coeff
            {
                // heat transfer co-efficient h (W/m2 deg C)
                get { return this._heat_transfer_coeff; }
            }

            public double ambient_temp
            {
                // Ambient temperature T infinite (T_inf) (deg C)
                get { return this._ambient_temp; }
            }

            public double thermal_conductivity_x
            {
                // Thermal conductivity kx (W/m deg C)
                get { return this._thermal_conductivity_x; }
            }

            public double thermal_conductivity_y
            {
                // Thermal conductivity ky (W/m deg C)
                get { return this._thermal_conductivity_y; }
            }

            public double element_thickness
            {
                // Element thickness
                get { return this._element_thickness; }
            }

            public double element_area
            {
                // Element area
                get { return this._element_area; }
            }


            public PointF get_p1
            {
                get
                {
                    return new PointF(get_x(vertices[0]), get_y(vertices[0]));
                }
            }

            public PointF get_p2
            {
                get
                {
                    return new PointF(get_x(vertices[1]), get_y(vertices[1]));
                }
            }

            public PointF get_p3
            {
                get
                {
                    return new PointF(get_x(vertices[2]), get_y(vertices[2]));
                }
            }

            public Single get_x(point2d pt)
            {
                return co_functions.tosingle((_mid_pt.get_paint_point(model_mid_pt, paint_scale).X * (1 - shrink_factor) + pt.get_paint_point(model_mid_pt, paint_scale).X * shrink_factor));
            }

            public Single get_y(point2d pt)
            {
                return co_functions.tosingle((_mid_pt.get_paint_point(model_mid_pt, paint_scale).Y * (1 - shrink_factor) + pt.get_paint_point(model_mid_pt, paint_scale).Y * shrink_factor));
            }

            public triangle2d(int i_face_id, point2d i_p1, point2d i_p2, point2d i_p3)
            {
                this._face_id = i_face_id;
                //if (!IsCounterClockwise(i_p1, i_p2, i_p3))
                //{
                //    this.vertices[0] = i_p1;
                //    this.vertices[1] = i_p3;
                //    this.vertices[2] = i_p2;
                //}
                //else
                //{
                this.vertices[0] = i_p1;
                this.vertices[1] = i_p2;
                this.vertices[2] = i_p3;
                //}

                this._mid_pt = new point2d(-1, (this.vertices[0].x + this.vertices[1].x + this.vertices[2].x) / 3.0f, (this.vertices[0].y + this.vertices[1].y + this.vertices[2].y) / 3.0f);
            }

            public void set_edges(int i_edge1, int i_edge2, int i_edge3)
            {
                // set the edge id
                this.edge_id[0] = i_edge1;
                this.edge_id[1] = i_edge2;
                this.edge_id[2] = i_edge3;
            }

            public void set_constraint(double i_heat_source, double i_specified_temp, double i_heat_transfer_coeff, double i_ambient_temp, double i_thermal_conductivity_x, double i_thermal_conductivity_y, double i_element_thickness)
            {
                // heat flux q (+ive away from normal -ive towards the normal) Heat supplied rate (W/m2)
                this._heat_source = i_heat_source;
                // specified temperature T (deg C)
                this._specified_temp = i_specified_temp;
                // heat transfer co-efficient h (W/m2 deg C)
                this._heat_transfer_coeff = i_heat_transfer_coeff;
                // Ambient temperature T infinite (T_inf) (deg C)
                this._ambient_temp = i_ambient_temp;
                // Thermal conductivity kx (W/m deg C)
                this._thermal_conductivity_x = i_thermal_conductivity_x;
                // Thermal conductivity ky (W/m deg C)
                this._thermal_conductivity_y = i_thermal_conductivity_y;
                // Thickness of element 
                this._element_thickness = i_element_thickness;

                // Find area
                Matrix<double> m = Matrix<double>.Build.Dense(3, 3);
                m[0, 0] = 1.0;
                m[0, 1] = vertices[0].x;
                m[0, 2] = vertices[0].y;

                m[1, 0] = 1.0;
                m[1, 1] = vertices[1].x;
                m[1, 2] = vertices[1].y;

                m[2, 0] = 1.0;
                m[2, 1] = vertices[2].x;
                m[2, 2] = vertices[2].y;

                double det = m.Determinant();

                this._element_area = det / 2.0f;
            }

            private bool IsCounterClockwise(point2d point1, point2d point2, point2d point3)
            {
                double result = (point2.x - point1.x) * (point3.y - point1.y) -
                    (point3.x - point1.x) * (point2.y - point1.y);
                return result > 0;
            }


            public void paint_me(ref Graphics gr0, PointF imodel_mid_pt, ref double ipaint_scale, ref Pen face_pen) // this function is used to paint the points
            {
                //Pen triangle_pen = new Pen(Color.LightGreen, 1);

                //if (Form1.the_static_class.is_paint_mesh == true)
                //{

                model_mid_pt = imodel_mid_pt;
                paint_scale = ipaint_scale;

                shrink_factor = 0.6f;
                PointF[] curve_pts = { get_p1, get_p2, get_p3 };
                gr0.FillPolygon(face_pen.Brush, curve_pts); // Fill the polygon

                if (static_parameters.show_element_values == true)
                {
                    // Show node value is on
                    string str = "";
                    if (thermal_conductivity_x != 0 || thermal_conductivity_y != 0)
                    {
                        if (thermal_conductivity_x == thermal_conductivity_y)
                        {
                            str = "k = " + thermal_conductivity_x.ToString() + "\n" + "Thk = " + element_thickness.ToString();
                        }
                        else
                        {
                            str = "kxx = " + thermal_conductivity_x.ToString() + ", kyy = " + thermal_conductivity_y.ToString() + "\n" + "Thk = " + element_thickness.ToString();
                        }
                    }

                    // Paint heat source
                    if (heat_source != 0)
                    {
                        paint_heat_source(gr0, model_mid_pt, paint_scale, ref str);
                    }
                    // Paint specified temperature
                    if (specified_temp != 0)
                    {
                        paint_specified_temp(gr0, model_mid_pt, paint_scale, ref str);
                    }
                    // Paint ambient temperature
                    if (ambient_temp != 0 && heat_transfer_coeff != 0)
                    {
                        paint_ambient_temp(gr0, model_mid_pt, paint_scale, ref str);
                    }

                    // Paint the string
                    if (static_parameters.show_values == true)
                    {
                        if (str != "")
                        {
                            string_store str_p = new string_store(str, new Font("Cambria", 4), Color.Green, true, false);
                            PointF nd_pt = mid_pt.get_paint_point(model_mid_pt, paint_scale);
                            str_p.paint_me(ref gr0, ref nd_pt, true);

                            //Pen load_pen = new Pen(Color.Green, 1);

                            //gr0.DrawString(str, new Font("Cambria", 6), load_pen.Brush, nd_pt);
                        }
                    }
                }
            }

            private void paint_heat_source(Graphics gr0, PointF model_mid_pt, double paint_scale, ref string str)
            {

                Pen load_pen = new Pen(Color.Green, 1);
                PointF nd_pt = mid_pt.get_paint_point(model_mid_pt, paint_scale);
                //  gr0.DrawEllipse(load_pen, new RectangleF(get_point_for_ellipse(model_mid_pt, 2, paint_scale), new SizeF(4, 4)));
                if (heat_source > 0)
                {
                    // heat flow away from the node
                    // Make a GraphicsPath to define the end cap.
                    GraphicsPath end_path = new GraphicsPath();
                    end_path.AddLine(0, 0, -1.5f, -1.5f);
                    end_path.AddLine(0, 0, 1.5f, -1.5f);

                    // Make the end cap.
                    CustomLineCap end_cap = new CustomLineCap(null, end_path);
                    load_pen.CustomEndCap = end_cap;

                    double degrees = 45;
                    for (int i = 0; i < 4; i++)
                    {
                        PointF pt1 = co_functions.rotate_point(2, 0, nd_pt.X, nd_pt.Y, degrees);
                        PointF pt2 = co_functions.rotate_point(6, 0, nd_pt.X, nd_pt.Y, degrees);

                        gr0.DrawLine(load_pen, pt1, pt2);

                        degrees = degrees + 90;
                    }
                }
                else
                {
                    // heat flow towards the node
                    // Make a GraphicsPath to define the start cap.
                    GraphicsPath start_path = new GraphicsPath();
                    start_path.AddLine(0, 0, -1, -1);
                    start_path.AddLine(0, 0, 1, -1);

                    // Make the start cap.
                    CustomLineCap start_cap = new CustomLineCap(null, start_path);
                    load_pen.CustomStartCap = start_cap;

                    double degrees = 45;
                    for (int i = 0; i < 4; i++)
                    {
                        PointF pt1 = co_functions.rotate_point(2, 0, nd_pt.X, nd_pt.Y, degrees);
                        PointF pt2 = co_functions.rotate_point(6, 0, nd_pt.X, nd_pt.Y, degrees);

                        gr0.DrawLine(load_pen, pt1, pt2);

                        degrees = degrees + 90;
                    }
                }

                // Paint the heat_source value
                str = str + "q = " + heat_source.ToString();
            }

            private void paint_specified_temp(Graphics gr0, PointF model_mid_pt, double paint_scale, ref string str)
            {
                // Paint the specified temperature
                str = str + "T = " + specified_temp.ToString() + "\n";
            }

            private void paint_ambient_temp(Graphics gr0, PointF model_mid_pt, double paint_scale, ref string str)
            {

                Pen load_pen = new Pen(Color.Green, 1);
                PointF nd_pt = mid_pt.get_paint_point(model_mid_pt, paint_scale);
                //  gr0.DrawEllipse(load_pen, new RectangleF(get_point_for_ellipse(model_mid_pt, 2, paint_scale), new SizeF(4, 4)));

                // heat flow away from the node
                // Make a GraphicsPath to define the end cap.
                GraphicsPath end_path = new GraphicsPath();
                end_path.AddLine(0, 0, -1.5f, -1.5f);
                end_path.AddLine(0, 0, 1.5f, -1.5f);

                // Make the end cap.
                CustomLineCap end_cap = new CustomLineCap(null, end_path);
                load_pen.CustomEndCap = end_cap;

                double degrees = 0;
                for (int i = 0; i < 4; i++)
                {
                    PointF pt1 = co_functions.rotate_point(4, 0, nd_pt.X, nd_pt.Y, degrees);
                    PointF pt2 = co_functions.rotate_point(10, 0, nd_pt.X, nd_pt.Y, degrees);

                    gr0.DrawLine(load_pen, pt1, pt2);

                    degrees = degrees + 90;
                }

                // Paint the heat_source value
                str = str + "h = " + heat_transfer_coeff.ToString() + "\n" + "T_inf = " + ambient_temp.ToString() + "\n";
            }

            public void paint_selection(ref Graphics gr0, PointF imodel_mid_pt, ref double ipaint_scale, ref Pen face_pen) // this function is used to paint the points
            {
                //Pen triangle_pen = new Pen(Color.LightGreen, 1);

                //if (Form1.the_static_class.is_paint_mesh == true)
                //{

                model_mid_pt = imodel_mid_pt;
                paint_scale = ipaint_scale;

                shrink_factor = 0.8f;
                PointF[] curve_pts = { get_p1, get_p2, get_p3 };
                gr0.FillPolygon(face_pen.Brush, curve_pts); // Fill the polygon

                //if (Form1.the_static_class.ispaint_label == true)
                //{
                //    string my_string = this._face_id.ToString();
                //    SizeF str_size = gr0.MeasureString(my_string, new Font("Cambria", 6)); // Measure string size to position the dimension

                //    gr0.DrawString(my_string, new Font("Cambria", 6), new Pen(Color.DeepPink, 2).Brush, this._mid_pt.get_point());

                //}
                // }

            }
        }

        [Serializable]
        public class result_store
        {
            // result order stores the order of element
            int result_order;
            double max_z_val;
            double min_z_val;
            double[] contour_values;
            List<first_order_element_store> f_elements = new List<first_order_element_store>();
            List<second_order_element_store> s_elements = new List<second_order_element_store>();
            List<third_order_element_store> t_elements = new List<third_order_element_store>();

            // List<point2d> label_nodes = new List<point2d>();
            List<label_store> all_result_labels = new List<label_store>();

            public result_store()
            {
                // null constructor
            }

            public result_store(int elmnt_order, List<first_order_element_store> i_f_elements,
                                                List<second_order_element_store> i_s_elements,
                                                List<third_order_element_store> i_t_elements,
                                                double imax_z_val,
                                                double imin_z_val,
                                                double[] icontour_values)
            {
                this.result_order = elmnt_order;

                if (result_order == 1)
                {
                    // First order elements
                    this.f_elements = i_f_elements;
                }
                else if (result_order == 2)
                {
                    // Second order elements
                    this.s_elements = i_s_elements;
                }
                else if (result_order == 3)
                {
                    // Third order elements
                    this.t_elements = i_t_elements;
                }

                this.max_z_val = imax_z_val;
                this.min_z_val = imin_z_val;
                this.contour_values = icontour_values;
            }


            public void paint_me(ref Graphics gr0, ref GraphicsState bef_trans, PointF imodel_mid_pt, ref double ipaint_scale)
            {
                if (result_order == 1)
                {
                    // paint first order element results
                    foreach (first_order_element_store f_elm in f_elements)
                    {
                        f_elm.paint_me(ref gr0, imodel_mid_pt, ref ipaint_scale);
                    }
                }
                else if (result_order == 2)
                {
                    // paint second order element results
                }
                else if (result_order == 3)
                {
                    // paint third order element results
                }

                // Paint label
                Graphics gr1 = gr0;
                gr1.SmoothingMode = SmoothingMode.AntiAlias;
                double ipaint_scale1 = ipaint_scale;
                all_result_labels.ForEach(obj => obj.paint_me(ref gr1, imodel_mid_pt, ref ipaint_scale1)); // Paint the nodes

                // Paint Contour
                // save the current transformation
                GraphicsState current_trans = gr0.Save();
                // restore to original transformation (ie., no transformation)
                gr0.Restore(bef_trans);
                paint_contour_bar_label(ref gr0);
                // restor back to scaled transformation
                gr0.Restore(current_trans);
            }

            public void create_label(point2d i_label_node)
            {
                // find the element associated with label node
                PointF label_node_as_point = new PointF(co_functions.tosingle(i_label_node.x), co_functions.tosingle(i_label_node.y));

                if (result_order == 1)
                {
                    // paint first order element results
                    foreach (first_order_element_store f_elm in f_elements)
                    {
                        PointF tri_pt1 = new PointF(co_functions.tosingle(f_elm.result_triangle.vertices[0].x), co_functions.tosingle(f_elm.result_triangle.vertices[0].y));
                        PointF tri_pt2 = new PointF(co_functions.tosingle(f_elm.result_triangle.vertices[1].x), co_functions.tosingle(f_elm.result_triangle.vertices[1].y));
                        PointF tri_pt3 = new PointF(co_functions.tosingle(f_elm.result_triangle.vertices[2].x), co_functions.tosingle(f_elm.result_triangle.vertices[2].y));

                        if (co_functions.IsPointInTriangle(label_node_as_point, tri_pt1, tri_pt2, tri_pt3) == true)
                        {
                            // Find the triangle which has the point
                            // and the alpha values of the triangle (linear triangle element)
                            double alpha_1 = (0.5 / f_elm.assoc_triangle.element_area) * ((tri_pt2.X * tri_pt3.Y - tri_pt3.X * tri_pt2.Y) * f_elm.result_triangle.z_vals[0] +
                                                                                          (tri_pt3.X * tri_pt1.Y - tri_pt1.X * tri_pt3.Y) * f_elm.result_triangle.z_vals[1] +
                                                                                          (tri_pt1.X * tri_pt2.Y - tri_pt2.X * tri_pt1.Y) * f_elm.result_triangle.z_vals[2]);
                            double alpha_2 = (0.5 / f_elm.assoc_triangle.element_area) * ((tri_pt2.Y - tri_pt3.Y) * f_elm.result_triangle.z_vals[0] +
                                                                                          (tri_pt3.Y - tri_pt1.Y) * f_elm.result_triangle.z_vals[1] +
                                                                                          (tri_pt1.Y - tri_pt2.Y) * f_elm.result_triangle.z_vals[2]);
                            double alpha_3 = (0.5 / f_elm.assoc_triangle.element_area) * ((tri_pt3.X - tri_pt2.X) * f_elm.result_triangle.z_vals[0] +
                                                                                          (tri_pt1.X - tri_pt3.X) * f_elm.result_triangle.z_vals[1] +
                                                                                          (tri_pt2.X - tri_pt1.X) * f_elm.result_triangle.z_vals[2]);

                            double z_value = alpha_1 + (alpha_2 * i_label_node.x) + (alpha_3 * i_label_node.y);

                            double z_scaled = (z_value - min_z_val) / (max_z_val - min_z_val);

                            Color z_color = co_functions.HSLToRGB(static_parameters.color_alpha_i + 70, (1 - z_scaled) * 240, 1, static_parameters.hsllight);

                            label_store temp_label = new label_store(i_label_node, z_scaled, z_value, z_color);
                            // Add the label to the list
                            all_result_labels.Add(temp_label);
                        }

                    }
                }
                else if (result_order == 2)
                {
                    // paint second order element results
                }
                else if (result_order == 3)
                {
                    // paint third order element results
                }
            }

            public void remove_label(int remote_type)
            {
                if (all_result_labels.Count != 0)
                {
                    if (remote_type == 1)
                    {
                        // Remove type = 1 means single remove
                        all_result_labels.RemoveAt(all_result_labels.Count - 1);
                    }
                    else if (remote_type == 2)
                    {
                        // Remove type == 2 means clear all label
                        all_result_labels.Clear();
                    }
                }
            }

            public void paint_contour_bar_label(ref Graphics gr2)
            {
                // Paint the side contour bar with values
                float txt_size1 = 10 + co_functions.tosingle(1.5 * (Math.Max(static_parameters.main_pic_size.Width, static_parameters.main_pic_size.Height) - 716) / 276);
                float txt_size2 = 7 + co_functions.tosingle(1.5 * (Math.Max(static_parameters.main_pic_size.Width, static_parameters.main_pic_size.Height) - 716) / 276);


                SizeF label_sizex = gr2.MeasureString("Temperature", new Font("Verdana", txt_size1, FontStyle.Regular, GraphicsUnit.Point));
                float label_x_loc = co_functions.tosingle((static_parameters.main_pic_size.Width - 30) - label_sizex.Width);
                float rectangle_ysize = co_functions.tosingle(static_parameters.main_pic_size.Height - 140);
                float rectangle_y_loc = co_functions.tosingle((static_parameters.main_pic_size.Height - rectangle_ysize) * 0.5);

                RectangleF contour_gradient_rect = new RectangleF((static_parameters.main_pic_size.Width - 50), rectangle_y_loc, 20, rectangle_ysize);

                // Draw heading
                gr2.DrawString("Temperature", new Font("Verdana", txt_size1), Brushes.Brown, label_x_loc, co_functions.tosingle(label_sizex.Height * 0.5));

                // Draw rectangle
                using (LinearGradientBrush linGrBrush = new LinearGradientBrush(new PointF(contour_gradient_rect.X, contour_gradient_rect.Y),
                                                                                 new PointF(contour_gradient_rect.X, contour_gradient_rect.Y + contour_gradient_rect.Height),
                                                                                 co_functions.HSLToRGB(static_parameters.color_alpha_i, (1 - 0) * 240, 1, static_parameters.hsllight),
                                                                                 co_functions.HSLToRGB(static_parameters.color_alpha_i, (1 - 1) * 240, 1, static_parameters.hsllight)))
                {
                    // Blend the colors
                    ColorBlend clrblnd = new ColorBlend();
                    List<Color> blendcolors = new List<Color>();
                    List<float> blendposition = new List<float>();
                    int spacing = 12;

                    string color_vals = "";

                    for (int i = 0; i < spacing; i++)
                    {
                        float psn = (float)i / co_functions.tosingle(spacing - 1);
                        blendcolors.Add(co_functions.HSLToRGB(static_parameters.color_alpha_i, psn * 240, 1, static_parameters.hsllight));
                        blendposition.Add(psn);

                        // values at the interval
                        color_vals = (min_z_val + ((max_z_val - min_z_val) * (1.0f - psn))).ToString("F4");

                        // Find the size of the text to be painted in the contour grid here
                        SizeF color_vals_label_sizex = gr2.MeasureString(color_vals, new Font("Verdana", txt_size2, FontStyle.Bold, GraphicsUnit.Point));

                        gr2.DrawString(color_vals, new Font("Verdana", txt_size2, FontStyle.Bold), new Pen(co_functions.HSLToRGB(static_parameters.color_alpha_i, psn * 240, 1, static_parameters.hsllight)).Brush,
                                           contour_gradient_rect.X - color_vals_label_sizex.Width,
                                           contour_gradient_rect.Y - co_functions.tosingle(color_vals_label_sizex.Height * 0.5) + (contour_gradient_rect.Height * psn));

                    }

                    clrblnd.Colors = blendcolors.ToArray();
                    clrblnd.Positions = blendposition.ToArray();

                    linGrBrush.InterpolationColors = clrblnd;

                    gr2.FillRectangle(linGrBrush, contour_gradient_rect);
                }

                //        Dim Txt_Size1 As Integer = 18 + 1.5 * ((Max(MT_Pic.Width, MT_Pic.Height) - 716) / 276)
                //Dim Txt_Size2 As Integer = 7 + 1.5 * ((Max(MT_Pic.Width, MT_Pic.Height) - 716) / 276)


                //Dim Label_sizeX As SizeF = Gr0.MeasureString(CLabel, New Font("Verdana", Txt_Size1, FontStyle.Regular, GraphicsUnit.Point))
                //Dim Label_X_Loc As Integer = (MainPic.Width / 2) - (Label_sizeX.Width * 0.5)
                //Dim rectangle_YSize As Integer = MainPic.Height - 140
                //Dim rectangle_Y_Loc As Integer = (MainPic.Height - rectangle_YSize) * 0.5
                //Dim Contour_Gradient_Rect As New Rectangle((MainPic.Width - 50), rectangle_Y_Loc, 20, rectangle_YSize)



                //'----- Draw heading
                //Gr0.DrawString(CLabel, New Font("Verdana", Txt_Size1), Brushes.Brown, Label_X_Loc, (Label_sizeX.Height * 0.5))

                //''---- - Draw rectangle
                // Using LinGrBrush As New LinearGradientBrush(New Point(Contour_Gradient_Rect.X, Contour_Gradient_Rect.Y), _
                //                                                New Point(Contour_Gradient_Rect.X, Contour_Gradient_Rect.Y +Contour_Gradient_Rect.Height), _
                //                                               HSLToRGB(120, (1 - 0) * 240, 1, HSLLight), _
                //                                               HSLToRGB(120, (1 - 1) * 240, 1, HSLLight))
                //    '------ Blend the colors
                //    Dim colorBlend As New ColorBlend
                //    Dim BlendColors As New List(Of Color)
                //    Dim BlendPosition As New List(Of Single)
                //    Dim spacing As Integer = 8

                //    Dim Color_vals As String = ""
                //    For i = 0 To(spacing - 1) Step + 1
                //        BlendColors.Add(HSLToRGB(120, ((i / (spacing - 1))) * 240, 1, HSLLight))
                //        BlendPosition.Add(i / (spacing - 1))

                //        Color_vals = Min_Val + ((Max_Val - Min_Val) * (1 - (i / (spacing - 1))))
                //        '----- Find the size of the text to be painted in the contour grid here
                //        Dim Color_vals_Label_sizeX As SizeF = Gr0.MeasureString(Color_vals, New Font("Verdana", Txt_Size2, FontStyle.Bold, GraphicsUnit.Point))

                //        Gr0.DrawString(Color_vals, New Font("Verdana", Txt_Size2, FontStyle.Bold), New Pen(HSLToRGB(120, (i / (spacing - 1)) * 240, 1, HSLLight)).Brush, _
                //                       Contour_Gradient_Rect.X - Color_vals_Label_sizeX.Width, _
                //                       Contour_Gradient_Rect.Y - (Color_vals_Label_sizeX.Height * 0.5) + (Contour_Gradient_Rect.Height * (i / (spacing - 1))))
                //    Next
                //    colorBlend.Colors = BlendColors.ToArray
                //    colorBlend.Positions = BlendPosition.ToArray

                //    LinGrBrush.InterpolationColors = colorBlend

                //    Gr0.FillRectangle(LinGrBrush, Contour_Gradient_Rect)
                //End Using


            }

            [Serializable]
            public class label_store
            {
                point2d label_location;
                double z_value;
                double z_scaled;
                Color z_color;

                public label_store(point2d ilabel_loc, double izscaled, double izvalue, Color iz_color)
                {
                    this.label_location = ilabel_loc;
                    this.z_scaled = izscaled;
                    this.z_value = izvalue;
                    this.z_color = iz_color;
                }

                public void paint_me(ref Graphics gr0, PointF imodel_mid_pt, ref double ipaint_scale)
                {
                    Pen temp_node_pen = new Pen(z_color, 2);

                    // Paint the label node
                    gr0.FillEllipse(temp_node_pen.Brush, new RectangleF(label_location.get_point_for_ellipse(imodel_mid_pt, 3, ipaint_scale), new SizeF(6, 6)));

                    // Point
                    PointF str_pt = label_location.get_paint_point(imodel_mid_pt, ipaint_scale);

                    // Paint the temperature
                    string_store temp_str = new string_store(z_value.ToString("F4"), new Font("Cambria", 8), z_color, true, false);
                    temp_str.paint_me(ref gr0, ref str_pt, true, 0, 10, true);
                }
            }


            [Serializable]
            public class contour_line_store
            {
                public point2d[] contour_vertices { get; } = new point2d[2];

                public double contour_val;

                public double contour_scaled;

                public Color contour_color;

                public contour_line_store(int elmnt_order, double cntr_lvl, double[] tri_z_vals, point2d[] tri_vertices, int crown_vertex_index, int other_v1_index, int other_v2_index, double max_zval, double min_zval)
                {
                    // parameter t is the parameterization value of the edge line
                    double param_t;
                    double tx, ty;

                    this.contour_val = cntr_lvl;
                    contour_scaled = (cntr_lvl - min_zval) / (max_zval - min_zval);
                    contour_color = co_functions.HSLToRGB((int)(static_parameters.color_alpha_i * 1.2), (1 - contour_scaled) * 240, 1, static_parameters.hsllight);

                    // contour lines intersection with the edge of the triangle
                    if (elmnt_order == 1)
                    {
                        param_t = (cntr_lvl - tri_z_vals[crown_vertex_index]) / (tri_z_vals[other_v1_index] - tri_z_vals[crown_vertex_index]);

                        // linear interpolation to find the point on the edge
                        tx = tri_vertices[crown_vertex_index].x * (1 - param_t) + tri_vertices[other_v1_index].x * param_t;
                        ty = tri_vertices[crown_vertex_index].y * (1 - param_t) + tri_vertices[other_v1_index].y * param_t;

                        this.contour_vertices[0] = new point2d(-10, tx, ty);
                    }

                    // contour lines intersection with the other edge of the triangle
                    if (elmnt_order == 1)
                    {
                        // find the parameter t
                        param_t = (cntr_lvl - tri_z_vals[crown_vertex_index]) / (tri_z_vals[other_v2_index] - tri_z_vals[crown_vertex_index]);

                        // linear interpolation to find the point on the other edge
                        tx = tri_vertices[crown_vertex_index].x * (1 - param_t) + tri_vertices[other_v2_index].x * param_t;
                        ty = tri_vertices[crown_vertex_index].y * (1 - param_t) + tri_vertices[other_v2_index].y * param_t;

                        this.contour_vertices[1] = new point2d(-10, tx, ty);
                    }
                }
            }


            [Serializable]
            public class constant_value_triangle
            {
                int element_order;
                public point2d[] vertices { get; } = new point2d[3];
                public double[] z_vals { get; } = new double[3];
                private double[] z_scaled { get; } = new double[3];
                private Color[] z_color { get; } = new Color[3];

                // contour lines
                List<contour_line_store> contour_lines = new List<contour_line_store>();

                PointF model_mid_pt = new PointF(0, 0);
                double paint_scale = 0.0;

                public PointF get_p1
                {
                    get
                    {
                        return vertices[0].get_paint_point(model_mid_pt, paint_scale);
                    }
                }

                public PointF get_p2
                {
                    get
                    {
                        return vertices[1].get_paint_point(model_mid_pt, paint_scale);
                    }
                }

                public PointF get_p3
                {
                    get
                    {
                        return vertices[2].get_paint_point(model_mid_pt, paint_scale);
                    }
                }

                public constant_value_triangle(int ielement_order, point2d[] i_vertices, double[] i_z_vals, double[] z_contour_lines, double max_zval, double min_zval)
                {
                    // set the conour plot (heat map)
                    this.element_order = ielement_order;
                    this.vertices = i_vertices;
                    this.z_vals = i_z_vals;

                    z_scaled[0] = (z_vals[0] - min_zval) / (max_zval - min_zval);
                    z_scaled[1] = (z_vals[1] - min_zval) / (max_zval - min_zval);
                    z_scaled[2] = (z_vals[2] - min_zval) / (max_zval - min_zval);

                    z_color[0] = co_functions.HSLToRGB(static_parameters.color_alpha_i, (1 - z_scaled[0]) * 240, 1, static_parameters.hsllight);
                    z_color[1] = co_functions.HSLToRGB(static_parameters.color_alpha_i, (1 - z_scaled[1]) * 240, 1, static_parameters.hsllight);
                    z_color[2] = co_functions.HSLToRGB(static_parameters.color_alpha_i, (1 - z_scaled[2]) * 240, 1, static_parameters.hsllight);

                    // set the contour lines
                    for (int i = 0; i < z_contour_lines.Length; i++)
                    {
                        set_contour_line(z_contour_lines[i], max_zval, min_zval);
                    }
                }

                private void set_contour_line(double cntr_val, double max_zval, double min_zval)
                {
                    contour_line_store temp_contour_line;

                    if (((z_vals[0] - cntr_val) * (z_vals[1] - cntr_val)) < 0)
                    {
                        // Triangle with Zval1 or Zval 2 is below the contour range
                        if (((z_vals[1] - cntr_val) * (z_vals[2] - cntr_val)) < 0)
                        {
                            // Triangle with Zval2 is above or below the contour range
                            // line is found from crown vertex as zval2 and zval1, zval3 be other two vertex of the triangle
                            temp_contour_line = new contour_line_store(element_order, cntr_val, z_vals, vertices, 1, 0, 2, max_zval, min_zval);
                            contour_lines.Add(temp_contour_line);
                        }
                        else if (((z_vals[0] - cntr_val) * (z_vals[2] - cntr_val)) < 0)
                        {
                            // Triangle with Zval1 is above or below the contour range
                            // line is found from crown vertex as zval1 and zval2, zval3 be other two vertex of the triangle
                            temp_contour_line = new contour_line_store(element_order, cntr_val, z_vals, vertices, 0, 1, 2, max_zval, min_zval);
                            contour_lines.Add(temp_contour_line);
                        }
                    }
                    else if (((z_vals[1] - cntr_val) * (z_vals[2] - cntr_val)) < 0)
                    {
                        if (((z_vals[0] - cntr_val) * (z_vals[2] - cntr_val)) < 0)
                        {
                            // Triangle with Zval 3 is above or below  the contour range
                            // line is found from crown vertex as zval3 and zval1, zval2 be other two vertex of the triangle
                            temp_contour_line = new contour_line_store(element_order, cntr_val, z_vals, vertices, 2, 0, 1, max_zval, min_zval);
                            contour_lines.Add(temp_contour_line);
                        }
                    }
                }

                public void paint_me(ref Graphics gr0, PointF imodel_mid_pt, ref double ipaint_scale)
                {
                    model_mid_pt = imodel_mid_pt;
                    paint_scale = ipaint_scale;

                    PointF[] curve_pts = { get_p1, get_p2, get_p3 };

                    using (GraphicsPath c_path = new GraphicsPath())
                    {
                        c_path.AddLines(curve_pts);

                        using (PathGradientBrush pthGrBrush = new PathGradientBrush(c_path))
                        {
                            // Paint contour heat map
                            int r0 = (int)((z_color[0].R + z_color[1].R + z_color[2].R) / 3);
                            int g0 = (int)((z_color[0].G + z_color[1].G + z_color[2].G) / 3);
                            int b0 = (int)((z_color[0].B + z_color[1].B + z_color[2].B) / 3);

                            pthGrBrush.CenterColor = Color.FromArgb(static_parameters.color_alpha_i, r0, g0, b0);
                            pthGrBrush.SurroundColors = new Color[] { z_color[0], z_color[1], z_color[2] };
                            gr0.FillPolygon(pthGrBrush, curve_pts);
                        }
                    }

                    // show contour lines
                    if (static_parameters.show_result_contours == true)
                    {
                        foreach (contour_line_store cntr_line in contour_lines)
                        {
                            PointF c_start_pt = cntr_line.contour_vertices[0].get_paint_point(model_mid_pt, paint_scale);
                            PointF c_end_pt = cntr_line.contour_vertices[1].get_paint_point(model_mid_pt, paint_scale);

                            Pen cntr_pen = new Pen(cntr_line.contour_color, 2);

                            gr0.DrawLine(cntr_pen, c_start_pt, c_end_pt);
                        }
                    }


                }
            }

            [Serializable]
            public class first_order_element_store
            {
                //          2
                //         /\
                //        /  \
                //       /    \
                //      /      \
                //     /        \
                //    1 -------- 3

                triangle2d _assoc_triangle;
                constant_value_triangle _result_triangle;

                public triangle2d assoc_triangle
                {
                    get { return this._assoc_triangle; }
                }

                public constant_value_triangle result_triangle
                {
                    get { return this._result_triangle; }
                }

                public first_order_element_store(triangle2d i_assoc_triangle, double[] result_vals, double[] z_contour_interval, double max_z_val, double min_z_val)
                {
                    this._assoc_triangle = i_assoc_triangle;
                    this._result_triangle = new constant_value_triangle(1, i_assoc_triangle.vertices, result_vals, z_contour_interval, max_z_val, min_z_val);
                }

                public void paint_me(ref Graphics gr0, PointF imodel_mid_pt, ref double ipaint_scale)
                {
                    this._result_triangle.paint_me(ref gr0, imodel_mid_pt, ref ipaint_scale);
                }
            }

            [Serializable]
            public class second_order_element_store
            {
                //          2
                //         /\
                //        /  \
                //       4----5
                //      /\    /\
                //     /  \  /  \
                //    1 ---6---- 3



            }

            [Serializable]
            public class third_order_element_store
            {
                //            2 
                //           / \
                //          5---6
                //         /\   /\
                //        /  \ /  \
                //       4----10---7
                //      /\    /\   /\
                //     /  \  /  \ /  \
                //    1 ---9---- 8----3


            }


        }


    }
}
