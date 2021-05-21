using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Heat2D_solver.Useful_Function
{
    public static class co_functions
    {
        public static bool Test_Point_on_line(ref PointF Pt, ref PointF SPt, ref PointF EPt, ref double Threshold)
        {
            bool Rslt = false;
            // -- Step: 1 Find the cross product
            float dxc, dyc; // --- Vector 1 Between Given Point and First point of the line
            dxc = Pt.X - SPt.X;
            dyc = Pt.Y - SPt.Y;

            float dx1, dy1; // --- Vector 2 Between the Second and First point of the line
            dx1 = EPt.X - SPt.X;
            dy1 = EPt.Y - SPt.Y;

            double CrossPrd;
            CrossPrd = dxc * dy1 - dyc * dx1; // Vector cross product

            if (Math.Abs(CrossPrd) <= Threshold)
            {
                if (Math.Abs(dx1) >= Math.Abs(dy1))
                    // If dx1 > 0 = true ' EPt.x is bigger else 'Spt.x is bigger
                    Rslt = dx1 > 0 ? SPt.X < Pt.X & Pt.X < EPt.X ? true : false : EPt.X < Pt.X & Pt.X < SPt.X ? true : false;
                else
                    // If dy1 > 0 = true ' EPt.y is bigger else 'Spt.y is bigger
                    Rslt = dy1 > 0 ? SPt.Y < Pt.Y & Pt.Y < EPt.Y ? true : false : EPt.Y < Pt.Y & Pt.Y < SPt.Y ? true : false;
            }
            return Rslt;
        }

        /// <summary>
        /// Function which check whether a Point is inside the circle
        /// </summary>
        /// <param name="Pt">Point to be checked</param>
        /// <param name="Circle_center">Center of Circle</param>
        /// <param name="Circle_Radii">Raidus of the Circle</param>
        /// <returns>Returns True or False depends on whether the point is inside the circle</returns>
        /// <remarks></remarks>
        public static bool Test_Point_in_Circle(ref PointF Pt, ref PointF Circle_center, ref double Circle_Radii)
        {
            bool Rslt = false;
            if (Circle_center.X - Circle_Radii < Pt.X & Circle_center.X + Circle_Radii > Pt.X & Circle_center.Y - Circle_Radii < Pt.Y & Circle_center.Y + Circle_Radii > Pt.Y)
                Rslt = true;
            return Rslt;
        }


        /// <summary>
        /// Function to Check the valid of Numerical text from textbox.text
        /// </summary>
        /// <param name="tB_txt">Textbox.text value</param>
        /// <param name="Negative_check">Is negative number Not allowed (True) or allowed (False)</param>
        /// <param name="zero_check">Is zero Not allowed (True) or allowed (False)</param>
        /// <returns>Return the validity (True means its valid) </returns>
        /// <remarks></remarks>
        public static bool Test_a_textboxvalue_validity(string tB_txt, bool Negative_check, bool zero_check)
        {
            bool Am_I_valid = false;
            double argresult = 0;
            // 'This function returns false if the textbox doesn't contains number 
            if (double.TryParse(tB_txt, out argresult) == true)
            {
                Am_I_valid = true;
                // -- Additional modificaiton to avoid negative number
                if (Negative_check == true)
                {
                    if (Convert.ToDouble(tB_txt) < 0)
                        Am_I_valid = false;
                }
                if (zero_check == true)
                {
                    if (Convert.ToDouble(tB_txt) == 0)
                        Am_I_valid = false;
                }
            }
            return Am_I_valid;
        }

        public static PointF line_parametric_t(PointF spt, PointF ept, double t)
        {
            double s_x = (spt.X * (1 - t)) + (ept.X * t);
            double s_y = (spt.Y * (1 - t)) + (ept.Y * t);

            return new PointF(tosingle(s_x), tosingle(s_y));
        }


        public static RectangleF return_ellipse_rectangle(PointF pt, float el_radius)
        {
            return new RectangleF(pt.X - el_radius, pt.Y - el_radius, el_radius * 2, el_radius * 2);
        }

        public static PointF rotate_point(double x, double y, double about_x, double about_y, double degrees)
        {
            double radians = degrees * (Math.PI / 180.0f);

            double rot_x = Math.Cos(radians) * x - Math.Sin(radians) * y;
            double rot_y = Math.Sin(radians) * x + Math.Cos(radians) * y;

            return new PointF(tosingle(rot_x + about_x), tosingle(rot_y + about_y));
        }


        public static int ConvertStringToInt(string intString)
        {
            int i = 0;
            return (Int32.TryParse(intString, out i) ? i : -1); //returns -1 if the given string is not integer
        }

        public static double ConvertStringToDouble(string intString)
        {
            if (intString == null)
            {
                return -1;
            }
            else
            {
                double OutVal;
                double.TryParse(intString, out OutVal);

                if (double.IsNaN(OutVal) || double.IsInfinity(OutVal))
                {
                    return -1;
                }
                return OutVal;
            }
        }

        public static bool is_linesegment_intersect(double p0_x, double p0_y, double p1_x, double p1_y,
                                                    double p2_x, double p2_y, double p3_x, double p3_y)
        {
            // https://stackoverflow.com/questions/563198/how-do-you-detect-where-two-line-segments-intersect

            double s02_x, s02_y, s10_x, s10_y, s32_x, s32_y, s_numer, t_numer, denom, t;
            s10_x = p1_x - p0_x;
            s10_y = p1_y - p0_y;
            s32_x = p3_x - p2_x;
            s32_y = p3_y - p2_y;

            denom = s10_x * s32_y - s32_x * s10_y;
            if (denom == 0)
                return false; // Collinear
            bool denomPositive = denom > 0;

            s02_x = p0_x - p2_x;
            s02_y = p0_y - p2_y;
            s_numer = s10_x * s02_y - s10_y * s02_x;
            if ((s_numer < 0) == denomPositive)
                return false; // No collision

            t_numer = s32_x * s02_y - s32_y * s02_x;
            if ((t_numer < 0) == denomPositive)
                return false; // No collision

            if (((s_numer > denom) == denomPositive) || ((t_numer > denom) == denomPositive))
                return false; // No collision
                              // Collision detected
            t = t_numer / denom;
            //if (i_x != NULL)
            //    *i_x = p0_x + (t * s10_x);
            //if (i_y != NULL)
            //    *i_y = p0_y + (t * s10_y);

            return true;
        }


        /// <summary>
        /// Function which returns Rectangle for the ellipse to be drawn
        /// </summary>
        /// <param name="Pt">Center of circle</param>
        /// <param name="El_radius">Redius of circle</param>
        /// <returns>Returns Rectangle which can be used in drawellipse or fillellipse functions</returns>
        /// <remarks></remarks>
        public static RectangleF Return_Ellipse_Rectangle(PointF Pt, int El_radius)
        {
            return new RectangleF(Pt.X - El_radius, Pt.Y - El_radius, El_radius * 2, El_radius * 2);
        }


        /// <summary>
        /// Function to check whether the value is valid or not
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool hasvalidvalue(this double value)
        {
            return !Double.IsNaN(value) && !Double.IsInfinity(value);
        }

        /// <summary>
        /// Function which returns Mid point of two point
        /// </summary>
        /// <param name="pt1">Point 1</param>
        /// <param name="pt2">Point 2</param>
        /// <returns>Returns mid point of two points</returns>
        /// <remarks></remarks>
        public static PointF return_mid_pointf(PointF pt1, PointF pt2)
        {
            return new PointF(tosingle((pt1.X + pt2.X) * 0.5), tosingle((pt1.Y + pt2.Y) * 0.5));
        }

        /// <summary>
        /// Function to return float values (single data)
        /// </summary>
        /// <param name="v1">double values</param>
        /// <returns>Returns the float for double</returns>
        /// <remarks></remarks>
        public static float tosingle(double v1)
        {
            return (float)Math.Round(v1, 12);
        }

        /// <summary>
        /// Function to return one dimensional matrix as double
        /// </summary>
        /// <param name="_inpt_matrix"></param>
        /// <returns></returns>
        public static double uni_dim_matrix_to_double(matrix_class _inpt_matrix)
        {
            return (Math.Round(_inpt_matrix[0, 0], 12));
        }

        public static Color GetRandomColor(int hash)
        {
            //Random randonGen = new Random(DateTime.Now.Millisecond.GetHashCode());
            Random randomGen = new Random((hash + 19) * DateTime.Now.Millisecond.GetHashCode());
            Color randomColor = Color.FromArgb(randomGen.Next(0, 256), randomGen.Next(0, 256), randomGen.Next(0, 256));
            return randomColor;
        }

        public static System.Drawing.Drawing2D.HatchStyle GetRandomHatchStyle(int hash)
        {
            //Random randomGen = new Random(hash.GetHashCode());
            //int randomhatchindex = randomGen.Next(0, 6);
            int randomhatchindex = hash > 6 ? 0 : hash;
            System.Drawing.Drawing2D.HatchStyle style = new System.Drawing.Drawing2D.HatchStyle();

            switch (randomhatchindex)
            {
                case 0:
                    style = System.Drawing.Drawing2D.HatchStyle.BackwardDiagonal;
                    break;
                case 1:
                    style = System.Drawing.Drawing2D.HatchStyle.DashedVertical;
                    break;
                case 2:
                    style = System.Drawing.Drawing2D.HatchStyle.Cross;
                    break;
                case 3:
                    style = System.Drawing.Drawing2D.HatchStyle.DiagonalCross;
                    break;
                case 4:
                    style = System.Drawing.Drawing2D.HatchStyle.HorizontalBrick;
                    break;
                case 5:
                    style = System.Drawing.Drawing2D.HatchStyle.LightDownwardDiagonal;
                    break;
                case 6:
                    style = System.Drawing.Drawing2D.HatchStyle.LightUpwardDiagonal;
                    break;
                default:
                    break;
            }

            return style;
        }

        // Function used by point in triangle
        private static float sign(PointF p1, PointF p2, PointF p3)
        {
            return (p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y);
        }

        // Function returns whether a point in triangle
        public static bool IsPointInTriangle(PointF pt, PointF v1, PointF v2, PointF v3)
        {
            float d1, d2, d3;
            bool has_neg, has_pos;

            d1 = sign(pt, v1, v2);
            d2 = sign(pt, v2, v3);
            d3 = sign(pt, v3, v1);

            has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
            has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

            return !(has_neg && has_pos);
        }


        // Return True if the point is in the polygon.
        public static bool IsPointInPolygon(PointF[] polygon, PointF testPoint)
        {
            // Get the angle between the point and the
            // first and last vertices.
            int max_point = polygon.Length - 1;
            double total_angle = GetAngle(
                polygon[max_point].X, polygon[max_point].Y,
                testPoint.X, testPoint.Y,
                polygon[0].X, polygon[0].Y);

            // Add the angles from the point
            // to each other pair of vertices.
            for (int i = 0; i < max_point; i++)
            {
                total_angle += GetAngle(
                    polygon[i].X, polygon[i].Y,
                    testPoint.X, testPoint.Y,
                    polygon[i + 1].X, polygon[i + 1].Y);
            }

            // The total angle should be 2 * PI or -2 * PI if
            // the point is in the polygon and close to zero
            // if the point is outside the polygon.
            // The following statement was changed. See the comments.
            //return (Math.Abs(total_angle) > 0.000001);
            return (Math.Abs(total_angle) > 1);
        }

        public static double SignedPolygonArea(PointF[] polygon)
        {
            //The total calculated area is negative if the polygon is oriented clockwise

            // Add the first point to the end.
            int num_points = polygon.Length;
            PointF[] pts = new PointF[num_points + 1];
            polygon.CopyTo(pts, 0);
            pts[num_points] = polygon[0];

            // Get the areas.
            double area = 0;
            for (int i = 0; i < num_points; i++)
            {
                area +=
                    (pts[i + 1].X - pts[i].X) *
                    (pts[i + 1].Y + pts[i].Y) / 2;
            }

            // Return the result.
            return area;
        }

        // Return the angle ABC.
        // Return a value between PI and -PI.
        // Note that the value is the opposite of what you might
        // expect because Y coordinates increase downward.
        public static double GetAngle(double Ax, double Ay,
            double Bx, double By, double Cx, double Cy)
        {
            // Get the dot product.
            double dot_product = DotProduct(Ax, Ay, Bx, By, Cx, Cy);

            // Get the cross product.
            double cross_product = CrossProductLength(Ax, Ay, Bx, By, Cx, Cy);

            // Calculate the angle.
            return Math.Atan2(cross_product, dot_product);
        }

        // Return the cross product AB x BC.
        // The cross product is a vector perpendicular to AB
        // and BC having length |AB| * |BC| * Sin(theta) and
        // with direction given by the right-hand rule.
        // For two vectors in the X-Y plane, the result is a
        // vector with X and Y components 0 so the Z component
        // gives the vector's length and direction.
        public static double CrossProductLength(double Ax, double Ay,
            double Bx, double By, double Cx, double Cy)
        {
            // Get the vectors' coordinates.
            double BAx = Ax - Bx;
            double BAy = Ay - By;
            double BCx = Cx - Bx;
            double BCy = Cy - By;

            // Calculate the Z coordinate of the cross product.
            return (BAx * BCy - BAy * BCx);
        }

        // Return the dot product AB · BC.
        // Note that AB · BC = |AB| * |BC| * Cos(theta).
        private static double DotProduct(double Ax, double Ay,
            double Bx, double By, double Cx, double Cy)
        {
            // Get the vectors' coordinates.
            double BAx = Ax - Bx;
            double BAy = Ay - By;
            double BCx = Cx - Bx;
            double BCy = Cy - By;

            // Calculate the dot product.
            return (BAx * BCx + BAy * BCy);
        }


        #region "HSL to RGB Fundamental code -Not by Me"
        //---- The below code is from https://www.programmingalgorithms.com/algorithm/hsl-to-rgb?lang=VB.Net
        //0    : blue   (hsl(240, 100%, 50%))
        //0.25 : cyan   (hsl(180, 100%, 50%))
        //0.5  : green  (hsl(120, 100%, 50%))
        //0.75 : yellow (hsl(60, 100%, 50%))
        //1    : red    (hsl(0, 100%, 50%))
        public static Color HSLToRGB(int alpha_i, double hsl_H, double hsl_S, double hsl_L)
        {
            byte r = 0;
            byte g = 0;
            byte b = 0;


            if (hsl_S == 0)
            {
                r = g = b = (byte)(hsl_L * 255);
            }
            else
            {
                double v1, v2;
                double hue = hsl_H / 360;

                v2 = (hsl_L < 0.5) ? (hsl_L * (1 + hsl_S)) : ((hsl_L + hsl_S) - (hsl_L * hsl_S));
                v1 = 2 * hsl_L - v2;

                r = (byte)(255 * HueToRGB(v1, v2, hue + (1.0f / 3)));
                g = (byte)(255 * HueToRGB(v1, v2, hue));
                b = (byte)(255 * HueToRGB(v1, v2, hue - (1.0f / 3)));
            }

            return Color.FromArgb(alpha_i, r, g, b);
        }


        private static double HueToRGB(double v1, double v2, double vH)
        {
            if (vH < 0)
                vH += 1;

            if (vH > 1)
                vH -= 1;

            if ((6 * vH) < 1)
                return (v1 + (v2 - v1) * 6 * vH);

            if ((2 * vH) < 1)
                return v2;

            if ((3 * vH) < 2)
                return (v1 + (v2 - v1) * ((2.0f / 3) - vH) * 6);

            return v1;
        }
        #endregion
    }

    public class TupleList<T1, T2> : List<Tuple<T1, T2>> where T1 : IComparable
    {
        public void Add(T1 item, T2 item2)
        {
            Add(new Tuple<T1, T2>(item, item2));
        }

        public new void Sort()
        {
            Comparison<Tuple<T1, T2>> c = (a, b) => a.Item1.CompareTo(b.Item1);
            base.Sort(c);
        }

    }

    //public static KeyValuePair<K, V> MakePair<K, V>(this K k, V v) { return new KeyValuePair<K, V>(k, v); }

    // Class 3 - string values are stored in this class
    public class string_store
    {
        string my_string;
        Font my_font;
        Color my_color;
        Brush paint_brush;
        bool my_visible;
        bool paint_vertical;

        public Color my_clr
        {
            get { return my_color; }
        }

        public Font m_fnt
        {
            get { return my_font; }
        }

        public string_store(string str0, Font fnt0, Color clr0, bool is_visible, bool is_vertical)
        {
            my_string = str0;
            my_font = fnt0;
            my_color = clr0;
            paint_brush = new Pen(my_color, 2).Brush;
            my_visible = is_visible;
            paint_vertical = is_vertical;
        }

        public void paint_me_vertical(ref Graphics gr0, ref PointF location, int x_offset = 0, int y_offset = 0, bool is_bold_true = false)
        {
            if (my_visible == true)
            {
                SizeF str_size = new SizeF();
                str_size = gr0.MeasureString(my_string, my_font);
                // Paint the x-axis in vertical alignment
                System.Drawing.Drawing2D.GraphicsState temp_gstate = gr0.Save();
                gr0.TranslateTransform((location.X + x_offset - co_functions.tosingle(str_size.Height * 0.5)), (location.Y + y_offset + co_functions.tosingle(str_size.Width * 0.5)));
                gr0.RotateTransform(270);
                gr0.TranslateTransform(-(location.X + x_offset - co_functions.tosingle(str_size.Height * 0.5)), -(location.Y + y_offset + co_functions.tosingle(str_size.Width * 0.5)));

                if (is_bold_true == true)
                    my_font = new Font(my_font.FontFamily, my_font.Size, FontStyle.Bold);

                gr0.DrawString(my_string, my_font, paint_brush, (location.X + x_offset - co_functions.tosingle(str_size.Height * 0.5)), (location.Y + y_offset + co_functions.tosingle(str_size.Width * 0.5)));
                gr0.Restore(temp_gstate);
            }

        }

        public void paint_me(ref Graphics gr0, ref PointF location, bool is_mid = true, int x_offset = 0, int y_offset = 0, bool is_bold_true = false)
        {
            if (my_visible == true)
            {
                SizeF str_size = new SizeF();
                str_size = gr0.MeasureString(my_string, my_font);

                if (is_bold_true == true)
                    my_font = new Font(my_font.FontFamily, my_font.Size, FontStyle.Bold);

                if (is_mid == true)
                {
                    gr0.DrawString(my_string, my_font,
                                paint_brush,
                                location.X + x_offset - co_functions.tosingle(str_size.Width * 0.5),
                                location.Y + y_offset - co_functions.tosingle(str_size.Height * 0.5));
                }
                else
                {
                    gr0.DrawString(my_string, my_font,
                                paint_brush,
                                location.X + x_offset,
                                location.Y + y_offset);
                }

            }
        }
    }
}
