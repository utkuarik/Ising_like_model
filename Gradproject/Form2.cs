using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics;
using System.Timers;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using Microsoft.Office.Interop.Excel;
using MathNet.Numerics.Statistics;
/// <summary>
/// dwadawdawdawdaw
/// </summary>
namespace Gradproject
{

    public partial class Form2 : Form
    {

        int locals_num;    // Universal variables
        int min_num;
        public int xaxis;
        public int yaxis;
        public int utility_check;
        public int cellSize = 5;
        public double lower_bound = 0.0;
        public double lower_bound2 = 0.0;
        public double upper_bound = 0.0;
        public int econ;
        public int no_free_cells;
        public int geo_value;
        public int algo_value;
        public double exp_het;
        public double[] MIX;
        public int[] Keep_arr;
        public double[] AVE_VALUE;
        public double[] FSI_VALUE;
        public double[] RATE;
        public double[] SEPAR;
        public int index_loc = 0;
        public int index_min = 0;
        public int number_loc = 0;
        public int number_min = 0;
        public double[] loc_number;
        public double[] mino_number;
        public int local_index;
        public int minor_index;
        public int dice;
        public double variance_FSI;
        public double variance_MIX;
        public double variance_ASN;
        public double[] count_green;
        public double[] count_red;
        public double[,] fract;
        public int frac_count;
        public List<Agents> agents = new List<Agents>();

        Agents[] Locals;

        Agents[] Minors;

        


        public int sim_value = 0;

        



        public Form2(string population, string minority, string x_axis, string y_axis, string lowerbound,string lowerbound2,
            string eco, string upperbound, string utilitycheck, string sim, string geo, string no_freecells,
            string algo)
        {   // Get the values from previous form

            InitializeComponent();


            locals_num = Convert.ToInt32(population);
            min_num = Convert.ToInt32(minority);
            xaxis = Convert.ToInt32(x_axis);
            yaxis = Convert.ToInt32(y_axis);
            lower_bound = Convert.ToDouble(lowerbound);
            lower_bound2 = Convert.ToDouble(lowerbound2);
            econ = Convert.ToInt32(eco);
            upper_bound = Convert.ToDouble(upperbound);
            utility_check = Convert.ToInt32(utilitycheck);
            sim_value = Convert.ToInt32(sim);
            geo_value = Convert.ToInt32(geo);
            no_free_cells = Convert.ToInt32(no_freecells);
            algo_value = Convert.ToInt32(algo);
            MIX = new double[sim_value];
            FSI_VALUE = new double[sim_value];
            AVE_VALUE = new double[sim_value];
            loc_number = new double[sim_value];
            mino_number = new double[sim_value];
            SEPAR = new double[sim_value];
            Locals = new Agents[locals_num];
            Minors = new Agents[min_num];
            fract = new double[50,250];
            count_green = new double[4];
            count_red = new double[4];




        }




        public void Draw_World(int x_axis, int y_axis) //Draw the initial empty world

        {
            Graphics g;

            Pen p = new Pen(Color.Black);
            Brush bBrush = (Brush)Brushes.Gray;

            g = this.CreateGraphics();

            for (int y = 0; y < y_axis; ++y)
            {
                g.DrawLine(p, 0, y * cellSize, y_axis * cellSize, y * cellSize);
            }

            for (int x = 0; x < x_axis; ++x)
            {
                g.DrawLine(p, x * cellSize, 0, x * cellSize, x_axis * cellSize);
            }
            exp_het = 2 * (locals_num / (locals_num + min_num * 1.00)) * (min_num / (locals_num + min_num * 1.00));


        }
        public MathNet.Numerics.LinearAlgebra.Matrix<double> execute() // Make the initial implanting and create agents
                                                                       // and draw the initial world

        {
            Keep_arr = new int[10000];
            

            Matrix<double> map = Matrix<double>.Build.Dense(xaxis, yaxis, 0);

            if (geo_value == 1)
            {
                for (int k = 0; k < xaxis; k++)
                    for (int l = yaxis / 2; l <= yaxis / 2 + 1; l++)

                    {
                        {
                            map[k, l] = 3;


                        }

                    }
            }
            else if (geo_value == 2)
            {
                int ja = 0;
                for (int i = yaxis - 1; i >= 0; i--)
                {

                    map[ja, i] = 3;

                    ja = ja + 1;
                }
            }

            Random rnd = new Random();

            for (int i = 0; i < locals_num; ++i)
            {


                int x = rnd.Next(0, xaxis);
                int y = rnd.Next(0, yaxis);



                while (map[x, y] == 1 || map[x, y] == 3)
                {
                    x = rnd.Next(0, xaxis);
                    y = rnd.Next(0, yaxis);

                }

                Locals[i] = new Agents()
                {
                    xpos = x,
                    ypos = y,


                };
                Locals[i].type = 1;
                map[x, y] = 1;
            }
            for (int i = 0; i < min_num; ++i)
            {



                int x = rnd.Next(0, xaxis);
                int y = rnd.Next(0, yaxis);

                while (map[x, y] == 1 || map[x, y] == 2 || map[x, y] == 3)
                {
                    x = rnd.Next(0, xaxis);
                    y = rnd.Next(0, yaxis);

                }
                Minors[i] = new Agents()
                {
                    xpos = x,
                    ypos = y,

                    //map[Locals[i].xpos, Locals[i].ypos] = "full";
                    // xpos = i + 1,
                    //ypos = i + 5,
                };

                Minors[i].type = 2;
                map[x, y] = 2;
            }
            Brush bbrush = (Brush)Brushes.Green;
            Brush cbrush = (Brush)Brushes.Red;
            Brush dbrush = (Brush)Brushes.Black;
            Pen p = new Pen(Color.Black);

            Graphics g;
            g = this.CreateGraphics();

            for (int i = 0; i < locals_num; ++i)
            {
                g.FillRectangle(bbrush, Locals[i].xpos * cellSize, Locals[i].ypos * cellSize, cellSize, cellSize);
            }

            for (int i = 0; i < min_num; ++i)
            {
                g.FillRectangle(cbrush, Minors[i].xpos * cellSize, Minors[i].ypos * cellSize, cellSize, cellSize);
            }

            if (geo_value == 1)// If there is natural boundary horizontal
            {
                for (int k = 0; k < xaxis; k++)
                    for (int l = yaxis / 2; l <= yaxis / 2 + 1; l++)

                    {
                        {
                            g.FillRectangle(dbrush, k * cellSize, l * cellSize, cellSize, cellSize);


                        }

                    }

            }
            else if (geo_value == 2)// ıf there is a natural boundary diagonal
            {
                int ja = 0;
                for (int i = yaxis - 1; i >= 0; i--)
                {

                    g.FillRectangle(dbrush, ja * cellSize, i * cellSize, cellSize, cellSize);

                    ja = ja + 1;
                }

            }
            for (int y = 0; y < yaxis; ++y)
            {
                g.DrawLine(p, 0, y * cellSize, yaxis * cellSize, y * cellSize);
            }

            for (int x = 0; x < xaxis; ++x)
            {
                g.DrawLine(p, x * cellSize, 0, x * cellSize, xaxis * cellSize);
            }
            return map;

        }   // end of execute function
        public Matrix<double> AdjacentElements(Matrix<double> map2, int row, int column)
        {
            // Collect neighbor cells' positions
            Matrix<double> w = Matrix<double>.Build.Dense(25, 3, 5);

            int rows = map2.RowCount;
            int columns = map2.ColumnCount;

            int r = 0;
            int s = 0;
            int p = 0;
            for (int j = row - 1; j <= row + 1; j++)
            {
                for (int i = column - 1; i <= column + 1; i++)
                {
                    if (i >= 0 && j >= 0 && i < columns && j < rows && !(j == row && i == column))
                    {
                        w[r, 0] = map2[j, i];
                        r = r + 1;
                    }

                }

            }

            for (int j = row - 2; j <= row + 2; j++)
            {

                if (j >= 0 && j < rows)
                {
                    if (column - 2 >= 0)
                    {
                        w[s, 1] = map2[j, column - 2];
                        s = s + 1;
                    }
                    if (column + 2 < column)
                    {
                        w[s, 1] = map2[j, column + 2];
                        s = s + 1;
                    }
                }
            }

            for (int i = column - 2; i <= column + 2; i++)
            {
                if (i >= 0 && i < rows)
                {
                    if (row - 2 >= 0)
                    {
                        w[s, 1] = map2[row - 2, i];
                        s = s + 1;
                    }
                    if (row + 2 < rows)
                    {
                        w[s, 1] = map2[row + 2, i];
                        s = s + 1;
                    }
                }

            }


            for (int j = row - 3; j <= row + 3; j++)
            {

                if (j >= 0 && j < rows)
                {
                    if (column - 3 >= 0)
                    {
                        w[p, 2] = map2[j, column - 3];
                        p = p + 1;
                    }
                    if (column + 3 < columns)
                    {
                        w[p, 2] = map2[j, column + 3];
                        p = p + 1;
                    }
                }
            }

            for (int i = column - 2; i <= column + 2; i++)
            {
                if (i >= 0 && i < rows)
                {
                    if (row - 3 >= rows)
                    {
                        w[p, 2] = map2[row - 3, i];
                        p = p + 1;
                    }
                    if (row + 3 < rows)
                    {
                        w[p, 2] = map2[row + 3, i];
                        p = p + 1;
                    }
                }

            }





            s = 0;
            p = 0;
            if (row == 0 || column == 0 || row == yaxis - 1 || column == xaxis - 1)
                if (column == 0 && row == 0)
                {
                    r = r + 1;
                    w[r, 0] = map2[row, column + xaxis - 1];
                    r = r + 1;
                    w[r, 0] = map2[yaxis - 1, column + xaxis - 1];
                    r = r + 1;
                    w[r, 0] = map2[row + 1, column + xaxis - 1];
                    r = r + 1;
                    w[r, 0] = map2[row + yaxis - 1, column + xaxis - 1];
                    r = r + 1;
                    w[r, 0] = map2[row + yaxis - 1, column];


                    w[s, 1] = map2[2, 0];
                    s = s + 1;
                    w[s, 1] = map2[2, 1];
                    s = s + 1;
                    w[s, 1] = map2[2, 2];
                    s = s + 1;
                    w[s, 1] = map2[1, 2];
                    s = s + 1;
                    w[s, 1] = map2[0, 2];


                    w[p, 2] = map2[3, 0];
                    p = p + 1;
                    w[p, 2] = map2[3, 1];
                    p = p + 1;
                    w[p, 2] = map2[3, 2];
                    p = p + 1;
                    w[p, 2] = map2[3, 3];
                    p = p + 1;
                    w[p, 2] = map2[2, 3];
                    p = p + 1;
                    w[p, 2] = map2[1, 3];
                    p = p + 1;
                    w[p, 2] = map2[0, 3];

                }
                else if (column == xaxis - 1 && row == 0)
                {
                    r = r + 1;
                    w[r, 0] = map2[0, 0];
                    r = r + 1;
                    w[r, 0] = map2[1, 0];
                    r = r + 1;
                    w[r, 0] = map2[row + yaxis - 1, column - (xaxis - 1)];
                    r = r + 1;
                    w[r, 0] = map2[row + yaxis - 1, 0];
                    r = r + 1;
                    w[r, 0] = map2[row + yaxis - 1, column - 1];

                    w[s, 1] = map2[row, xaxis - 3];
                    s = s + 1;
                    w[s, 1] = map2[row + 1, xaxis - 3];
                    s = s + 1;
                    w[s, 1] = map2[row + 2, xaxis - 3];
                    s = s + 1;
                    w[s, 1] = map2[row + 2, xaxis - 2];
                    s = s + 1;
                    w[s, 1] = map2[row + 2, xaxis - 1];


                    w[p, 2] = map2[row, xaxis - 4];
                    p = p + 1;
                    w[p, 2] = map2[row + 1, xaxis - 4];
                    p = p + 1;
                    w[p, 2] = map2[row + 2, xaxis - 4];
                    p = p + 1;
                    w[p, 2] = map2[row + 3, xaxis - 4];
                    p = p + 1;
                    w[p, 2] = map2[row + 3, xaxis - 3];
                    p = p + 1;
                    w[p, 2] = map2[row + 3, xaxis - 2];
                    p = p + 1;
                    w[p, 2] = map2[row + 3, xaxis - 1];
                }
                else if (column == 0 && row == yaxis - 1)
                {
                    r = r + 1;
                    w[r, 0] = map2[yaxis - 1, column + xaxis - 1];
                    r = r + 1;
                    w[r, 0] = map2[0, 0];
                    r = r + 1;
                    w[r, 0] = map2[0, column + xaxis - 1];
                    r = r + 1;
                    w[r, 0] = map2[0, 1];
                    r = r + 1;
                    w[r, 0] = map2[yaxis - 1 - 1, xaxis - 1];

                    w[s, 1] = map2[row - 2, 0];
                    s = s + 1;
                    w[s, 1] = map2[row - 2, 1];
                    s = s + 1;
                    w[s, 1] = map2[row - 2, 2];
                    s = s + 1;
                    w[s, 1] = map2[row - 1, 2];
                    s = s + 1;
                    w[s, 1] = map2[row, 2];


                    w[p, 2] = map2[row - 3, 0];
                    p = p + 1;
                    w[p, 2] = map2[row - 3, 1];
                    p = p + 1;
                    w[p, 2] = map2[row - 3, 2];
                    p = p + 1;
                    w[p, 2] = map2[row - 3, 3];
                    p = p + 1;
                    w[p, 2] = map2[row - 2, 3];
                    p = p + 1;
                    w[p, 2] = map2[row - 1, 3];
                    p = p + 1;
                    w[p, 2] = map2[row, 3];
                }
                else if (column == xaxis - 1 && row == yaxis - 1)
                {
                    r = r + 1;
                    w[r, 0] = map2[yaxis - 1, 0];
                    r = r + 1;
                    w[r, 0] = map2[0, 0];
                    r = r + 1;
                    w[r, 0] = map2[0, +xaxis - 1];
                    r = r + 1;
                    w[r, 0] = map2[0, column - 1 - 1];
                    r = r + 1;
                    w[r, 0] = map2[yaxis - 1 - 1, 0];

                    w[s, 1] = map2[row - 2, column];
                    s = s + 1;
                    w[s, 1] = map2[row - 2, column - 1];
                    s = s + 1;
                    w[s, 1] = map2[row - 2, column - 2];
                    s = s + 1;
                    w[s, 1] = map2[row - 1, column - 2];
                    s = s + 1;
                    w[s, 1] = map2[row, column - 2];


                    w[p, 2] = map2[row - 3, column];
                    p = p + 1;
                    w[p, 2] = map2[row - 3, column - 1];
                    p = p + 1;
                    w[p, 2] = map2[row - 3, column - 2];
                    p = p + 1;
                    w[p, 2] = map2[row - 3, column - 3];
                    p = p + 1;
                    w[p, 2] = map2[row - 2, column - 3];
                    p = p + 1;
                    w[p, 2] = map2[row - 1, column - 3];
                    p = p + 1;
                    w[p, 2] = map2[row, column - 3];
                }
                else if (column == 0 && row != 0 && row != yaxis - 1)
                {
                    r = r + 1;
                    w[r, 0] = map2[row, column + xaxis - 1];
                    r = r + 1;
                    w[r, 0] = map2[row - 1, column + xaxis - 1];
                    r = r + 1;
                    w[r, 0] = map2[row + 1, column + xaxis - 1];
                }
                else if (column != 0 && row == yaxis - 1 && column != xaxis - 1)
                {
                    r = r + 1;
                    w[r, 0] = map2[0, column];
                    r = r + 1;
                    w[r, 0] = map2[0, column - 1];
                    r = r + 1;
                    w[r, 0] = map2[0, column + 1];
                }
                else if (column == xaxis - 1 && row != 0 && row != yaxis - 1)
                {
                    r = r + 1;
                    w[r, 0] = map2[row, 0];
                    r = r + 1;
                    w[r, 0] = map2[row + 1, 0];
                    r = r + 1;
                    w[r, 0] = map2[row - 1, 0];
                }
                else if (column != 0 && column != xaxis - 1 && row == 0)
                {
                    r = r + 1;
                    w[r, 0] = map2[yaxis - 1, column];
                    r = r + 1;
                    w[r, 0] = map2[yaxis - 1, column + 1];
                    r = r + 1;
                    w[r, 0] = map2[yaxis - 1, column - 1];
                }

            return w;
        }// end of adjacent elements function

        public int[,] rate_check(int x_pos, int y_pos, Matrix<double> map)//Count neighbors' types
        {

            Matrix<double> map2 = map;
            int count1 = 0;
            var arr = map2;
            int count2 = 0;
            int count3 = 0;
            int count21 = 0;
            int count22 = 0;
            int count31 = 0;
            int count32 = 0;
            int count20 = 0;
            int count30 = 0;
            int row = x_pos;
            int column = y_pos;

            var results = AdjacentElements(map2, x_pos, y_pos);

            for (int i = 0; i < results.RowCount; i++)
            {
                if (results[i, 0] == 1)
                {
                    count1 = count1 + 1;
                }
                else if (results[i, 0] == 2)
                {
                    count2 = count2 + 1;
                }
                else if (results[i, 0] == 0)
                {
                    count3 = count3 + 1;
                }
            }

            for (int i = 0; i < results.RowCount; i++)
            {

                if (results[i, 1] == 1)
                {
                    count21 = count21 + 1;
                }
                else if (results[i, 1] == 2)
                {
                    count22 = count22 + 1;
                }

                else if (results[i, 1] == 0)
                {
                    count20 = count20 + 1;
                }
            }

            for (int i = 0; i < results.RowCount; i++)
            {
                if (results[i, 2] == 1)
                {
                    count31 = count31 + 1;



                }
                else if (results[i, 2] == 2)
                {

                    count32 = count32 + 1;

                }

                else if (results[i, 2] == 0)
                {

                    count30 = count30 + 1;

                }

            }
            int[,] a;
            a = new int[3, 3];
            a[0, 0] = count1;
            a[1, 0] = count2;
            a[2, 0] = count3;
            a[0, 1] = count21;
            a[1, 1] = count22;
            a[2, 1] = count20;
            a[0, 2] = count31;
            a[1, 2] = count32;
            a[2, 2] = count30;
            return a;

        }// end of rate_check func.
        public double rate_check_for_one(int lxpos, int lypos, int mxpos, int mypos, Matrix<double> map)
        {
            int[,] a;
            int[,] b;
            double rate_sum;
            double rate1;
            double rate2;
            double c = 1.00;
            a = rate_check(lxpos, lypos, map);

            if (a[0, 0] == 0)
            {
                rate1 = 0;
            }
            else
            {
                rate1 = a[0, 0] / c / (a[1, 0] + a[0, 0]);
            }



            b = rate_check(mxpos, mypos, map);
            if (b[1, 0] == 0)
            {
                rate2 = 0;
            }
            else
            {
                rate2 = b[1, 0] / c / (b[1, 0] + b[0, 0]);
            }

            rate_sum = rate1 + rate2;

            return rate_sum;
        }

        public Matrix<double> rate_check_for_all(Matrix<double> map)//Compute neigbors rate for all agents and statistics
        {
            Matrix<double> uti_map = Matrix<double>.Build.Dense(xaxis, yaxis, 0);
            double a = 0.0;
            double c = 1.0;
            for (int i = 0; i < locals_num; i++)
            {
                int[,] t;
                t = new int[3, 3];
                while (Locals[i] == null)
                {
                    i = i + 1;
                    if (i >= locals_num)
                    {
                        i = i - 1;
                        break;
                    }
                }
                if (Locals[i] == null)
                { break; }
                t = rate_check(Locals[i].xpos, Locals[i].ypos, map);
                if (Locals[i].type == 1)
                {
                    if (t[0, 0] == 0)
                    {
                        a = 0;
                    }
                    else
                    {
                        a = t[0, 0] / c / (t[1, 0] + t[0, 0]);
                    }
                }
                else if (Locals[i].type == 2)
                {
                    if (t[1, 0] == 0)
                    {
                        a = 0;
                    }
                    else
                    {
                        a = t[1, 0] / c / (t[1, 0] + t[0, 0]);
                    }


                }
                uti_map[Locals[i].xpos, Locals[i].ypos] = a;

                if (t[1, 0] == 0 && Locals[i].type == 1)
                {
                    Locals[i].seperatist = 1;


                }
                else if (Locals[i].type == 2 && t[0, 0] == 0)
                {
                    Locals[i].seperatist = 1;


                }
                else
                {
                    Locals[i].seperatist = 0;
                }
                if (econ == 1)
                {
                    if (economy_check(Locals[i].xpos, Locals[i].ypos) == 2)
                    {
                        Locals[i].rate = 2 * a;
                    }
                }

                else
                {
                    Locals[i].rate = a;
                    Locals[i].mixity = 1 - a;
                    if (Locals[i].type == 1)
                    {
                        Locals[i].het_neigh = t[1, 0];
                    }
                    else
                    {
                        Locals[i].het_neigh = t[0, 0];

                    }
                    Locals[i].total_neigh = t[0, 0] + t[1, 0];
                    Locals[i].FSI = (exp_het - Locals[i].het_neigh) / exp_het;
                    Locals[i].emp_neigh = t[2, 0];
                }
            }
            for (int i = 0; i < min_num; i++)
            {
                int[,] t;
                t = new int[3, 3];
                while (Minors[i] == null)
                {
                    i = i + 1;
                }
                t = rate_check(Minors[i].xpos, Minors[i].ypos, map);
                if (Minors[i].type == 2)
                {
                    if (t[1, 0] == 0)
                    {
                        a = 0;
                    }
                    else
                    {
                        a = t[1, 0] / c / (t[1, 0] + t[0, 0]);
                    }
                }

                //uti_map[Minors[i].xpos, Minors[i].ypos] = a;
                else if (Minors[i].type == 1)
                {
                    if (t[0, 0] == 0)
                    {
                        a = 0;
                    }
                    else
                    {
                        a = t[0, 0] / c / (t[1, 0] + t[0, 0]);
                    }


                }
                if (t[1, 0] == 0 && Minors[i].type == 2)
                {
                    Minors[i].seperatist = 1;


                }

                else if (t[0, 0] == 0 && Minors[i].type == 1)
                {

                    Minors[i].seperatist = 1;

                }
                else
                {
                    Minors[i].seperatist = 0;


                }
                if (econ == 1)
                {
                    if (economy_check(Minors[i].xpos, Minors[i].ypos) == 2)
                    {
                        Minors[i].rate = 2 * a;
                    }
                }

                else
                {
                    Minors[i].rate = a;
                    Minors[i].mixity = 1 - a;
                    if (Minors[i].type == 2)
                    {
                        Minors[i].het_neigh = t[0, 0];

                    }
                    else
                    {
                        Minors[i].het_neigh = t[1, 0];

                    }
                    Minors[i].total_neigh = t[0, 0] + t[1, 0];
                    Minors[i].FSI = (exp_het - Minors[i].het_neigh) / exp_het;
                }
            }

            return uti_map.Transpose();
        }// end of rate_check_for_all func.

        public Matrix<double> continue_to(Matrix<double> map)//Reimplanting
        {
            Random rnd1 = new Random();
            Random rnd2 = new Random();
            Random rnd3 = new Random();
            int x;
            int y;

            if (no_free_cells == 1)
            {

                int local_index;
                int minor_index;
                int lxpos;
                int lypos;
                int mxpos;
                int mypos;
                double rate_sum;
                double rate_sum1;
                double chance;
                local_index = rnd2.Next(0, locals_num);
                minor_index = rnd2.Next(0, min_num);
                if (algo_value == 0)
                {



                    for (int i = 0; i < xaxis * yaxis; i++)
                    {

                        local_index = rnd2.Next(0, locals_num);
                        minor_index = rnd2.Next(0, min_num);
                        if ((Locals[local_index].rate < lower_bound || Locals[local_index].rate > upper_bound) &&
                                (Minors[minor_index].rate < lower_bound || Minors[minor_index].rate > upper_bound))
                        {




                            lxpos = Locals[local_index].xpos;
                            lypos = Locals[local_index].ypos;
                            mxpos = Minors[minor_index].xpos;
                            mypos = Minors[minor_index].ypos;

                            if (utility_check != 0)
                            {
                                double a;
                                double b;
                                double a2;
                                double b2;
                                a = utility(map, Locals[local_index].xpos, Locals[local_index].ypos, Locals[local_index].type);
                                b = utility(map, Minors[minor_index].xpos, Minors[minor_index].ypos, Minors[minor_index].type);
                                rate_sum = a + b;


                                Locals[local_index].xpos = mxpos;
                                Locals[local_index].ypos = mypos;
                                map[mxpos, mypos] = 1;
                                Minors[minor_index].xpos = lxpos;
                                Minors[minor_index].ypos = lypos;
                                map[lxpos, lypos] = 2;

                                a2 = utility(map, Locals[local_index].xpos, Locals[local_index].ypos, Locals[local_index].type);
                                b2 = utility(map, Minors[minor_index].xpos, Minors[minor_index].ypos, Minors[minor_index].type);

                                //rate_sum1 = a2 + b2;
                                //if (rate_sum1 < rate_sum)
                                //{
                                //    map[lxpos, lypos] = 1;
                                //    map[mxpos, mypos] = 2;
                                //    Locals[local_index].xpos = lxpos;
                                //    Locals[local_index].ypos = lypos;
                                //    Minors[minor_index].xpos = mxpos;
                                //    Minors[minor_index].ypos = mypos;

                                //    Locals[local_index].utility = utility(map, lxpos, lypos, 1);
                                //    Minors[minor_index].utility = utility(map, mxpos, mypos, 2);

                                //    local_index = rnd2.Next(0, locals_num);
                                //    minor_index = rnd2.Next(0, min_num);



                                //}
                                //else
                                //{
                                //    Locals[local_index].utility = utility(map, mxpos, mypos, 1);
                                //    Minors[minor_index].utility = utility(map, lxpos, lypos, 2);

                                //    local_index = rnd2.Next(0, locals_num);
                                //    minor_index = rnd2.Next(0, min_num);



                                //}


                            }
                            else
                            {
                                rate_sum = Locals[local_index].rate + Minors[minor_index].rate;
                                Locals[local_index].xpos = mxpos;
                                Locals[local_index].ypos = mypos;
                                map[mxpos, mypos] = 1;
                                Minors[minor_index].xpos = lxpos;
                                Minors[minor_index].ypos = lypos;
                                map[lxpos, lypos] = 2;



                                rate_sum1 = rate_check_for_one(Locals[local_index].xpos, Locals[local_index].ypos,
                                Minors[minor_index].xpos, Minors[minor_index].ypos, map);

                                if (rate_sum1 < rate_sum)
                                {
                                    map[lxpos, lypos] = 1;
                                    map[mxpos, mypos] = 2;
                                    Locals[local_index].xpos = lxpos;
                                    Locals[local_index].ypos = lypos;
                                    Minors[minor_index].xpos = mxpos;
                                    Minors[minor_index].ypos = mypos;

                                    Locals[local_index].utility = utility(map, lxpos, lypos, 1);
                                    Minors[minor_index].utility = utility(map, mxpos, mypos, 2);

                                    local_index = rnd2.Next(0, locals_num);
                                    minor_index = rnd2.Next(0, min_num);


                                }
                                else
                                {

                                    Locals[local_index].utility = utility(map, mxpos, mypos, 1);
                                    Minors[minor_index].utility = utility(map, lxpos, lypos, 2);

                                    local_index = rnd2.Next(0, locals_num);
                                    minor_index = rnd2.Next(0, min_num);

                                }

                            }
                        }



                        else
                        {

                            minor_index = rnd2.Next(0, min_num);
                            local_index = rnd2.Next(0, locals_num);

                        }

                    }

                }

                else if (algo_value == 1)// Chose one unhappy agent then replace it with opposite random one
                {

                    for (int i = 0; i < xaxis * yaxis; i++)
                    {
                        if ((Locals[local_index].rate < lower_bound || Locals[local_index].rate > upper_bound) ||
                            (Minors[minor_index].rate < lower_bound || Minors[minor_index].rate > upper_bound))
                        {




                            lxpos = Locals[local_index].xpos;
                            lypos = Locals[local_index].ypos;
                            mxpos = Minors[minor_index].xpos;
                            mypos = Minors[minor_index].ypos;

                            if (utility_check != 0)
                            {
                                double a;
                                double b;
                                double a2;
                                double b2;
                                a = utility(map, Locals[local_index].xpos, Locals[local_index].ypos, Locals[local_index].type);
                                b = utility(map, Minors[minor_index].xpos, Minors[minor_index].ypos, Minors[minor_index].type);
                                rate_sum = a + b;


                                Locals[local_index].xpos = mxpos;
                                Locals[local_index].ypos = mypos;
                                map[mxpos, mypos] = 1;
                                Minors[minor_index].xpos = lxpos;
                                Minors[minor_index].ypos = lypos;
                                map[lxpos, lypos] = 2;

                                a2 = utility(map, Locals[local_index].xpos, Locals[local_index].ypos, Locals[local_index].type);
                                b2 = utility(map, Minors[minor_index].xpos, Minors[minor_index].ypos, Minors[minor_index].type);

                                rate_sum1 = a2 + b2;
                                if (rate_sum1 < rate_sum)
                                {
                                    map[lxpos, lypos] = 1;
                                    map[mxpos, mypos] = 2;
                                    Locals[local_index].xpos = lxpos;
                                    Locals[local_index].ypos = lypos;
                                    Minors[minor_index].xpos = mxpos;
                                    Minors[minor_index].ypos = mypos;

                                    Locals[local_index].utility = utility(map, lxpos, lypos, 1);
                                    Minors[minor_index].utility = utility(map, mxpos, mypos, 2);

                                    local_index = rnd2.Next(0, locals_num);
                                    minor_index = rnd2.Next(0, min_num);



                                }
                                else
                                {
                                    Locals[local_index].utility = utility(map, mxpos, mypos, 1);
                                    Minors[minor_index].utility = utility(map, lxpos, lypos, 2);

                                    local_index = rnd2.Next(0, locals_num);
                                    minor_index = rnd2.Next(0, min_num);



                                }


                            }
                            else
                            {
                                rate_sum = Locals[local_index].rate + Minors[minor_index].rate;
                                Locals[local_index].xpos = mxpos;
                                Locals[local_index].ypos = mypos;
                                map[mxpos, mypos] = 1;
                                Minors[minor_index].xpos = lxpos;
                                Minors[minor_index].ypos = lypos;
                                map[lxpos, lypos] = 2;



                                rate_sum1 = rate_check_for_one(Locals[local_index].xpos, Locals[local_index].ypos,
                                Minors[minor_index].xpos, Minors[minor_index].ypos, map);

                                if (rate_sum1 < rate_sum)
                                {
                                    map[lxpos, lypos] = 1;
                                    map[mxpos, mypos] = 2;
                                    Locals[local_index].xpos = lxpos;
                                    Locals[local_index].ypos = lypos;
                                    Minors[minor_index].xpos = mxpos;
                                    Minors[minor_index].ypos = mypos;

                                    Locals[local_index].utility = utility(map, lxpos, lypos, 1);
                                    Minors[minor_index].utility = utility(map, mxpos, mypos, 2);

                                    local_index = rnd2.Next(0, locals_num);
                                    minor_index = rnd2.Next(0, min_num);


                                }
                                else
                                {

                                    Locals[local_index].utility = utility(map, mxpos, mypos, 1);
                                    Minors[minor_index].utility = utility(map, lxpos, lypos, 2);

                                    local_index = rnd2.Next(0, locals_num);
                                    minor_index = rnd2.Next(0, min_num);

                                }

                            }
                        }



                        else
                        {

                            minor_index = rnd2.Next(0, min_num);
                            local_index = rnd2.Next(0, locals_num);

                        }

                    }
                }

                else if (algo_value == 2) // all move randomly independent of happiness
                {
                    local_index = rnd2.Next(0, locals_num);
                    minor_index = rnd2.Next(0, min_num);


                    for (int i = 0; i < xaxis * yaxis; i++)
                    {

                        lxpos = Locals[local_index].xpos;
                        lypos = Locals[local_index].ypos;
                        mxpos = Minors[minor_index].xpos;
                        mypos = Minors[minor_index].ypos;

                        Locals[local_index].xpos = mxpos;
                        Locals[local_index].ypos = mypos;

                        Minors[minor_index].xpos = lxpos;
                        Minors[minor_index].ypos = lypos;

                        map[mxpos, mypos] = 1;
                        map[lxpos, lypos] = 2;

                        Locals[local_index].utility = utility(map, mxpos, mypos, 1);
                        Minors[minor_index].utility = utility(map, lxpos, lypos, 2);

                        local_index = rnd2.Next(0, locals_num);
                        minor_index = rnd2.Next(0, min_num);

                    }




                }

                else if (algo_value == 3)
                {

                    for (int i = 0; i < xaxis * yaxis; i++)
                    {
                        chance = rnd1.Next(0, index_loc + index_min);


                        if (chance <= index_loc)
                        {
                            index_loc = 0;

                            for (int j = 0; j < locals_num; j++)
                            {
                                if (Locals[j] != null)
                                {
                                    Keep_arr[index_loc] = j;
                                    index_loc = index_loc + 1;
                                }



                            }
                            if (index_loc == 0)
                            {

                                break;
                            }

                            local_index = rnd2.Next(0, index_loc);

                            local_index = Keep_arr[local_index];

                            if (Locals[local_index].rate < lower_bound)

                            {
                                lxpos = Locals[local_index].xpos;
                                lypos = Locals[local_index].ypos;

                                map[lxpos, lypos] = 2;
                                // locals_num = locals_num - 1;
                                min_num = min_num + 1;
                                Minors[min_num] = new Agents()
                                {
                                    xpos = lxpos,
                                    ypos = lypos,


                                };
                                Locals[local_index] = null;


                                if (locals_num < 0)
                                {

                                    break;
                                }


                            }
                            else
                            {
                                local_index = Keep_arr[local_index];


                            }
                        }
                        else
                        {
                            index_min = 0;
                            minor_index = rnd2.Next(0, min_num);
                            for (int j = 0; j < min_num; j++)
                            {
                                if (Minors[j] != null)
                                {
                                    Keep_arr[index_min] = j;
                                    index_min = index_min + 1;
                                }



                            }
                            if (index_min == 0)
                            {

                                break;
                            }

                            minor_index = rnd2.Next(0, index_min);
                            minor_index = Keep_arr[minor_index];

                            if (Minors[minor_index].rate < lower_bound)
                            {
                                mxpos = Minors[minor_index].xpos;
                                mypos = Minors[minor_index].ypos;

                                map[mxpos, mypos] = 1;
                                locals_num = locals_num + 1;
                                //min_num = min_num - 1;

                                Locals[locals_num] = new Agents()
                                {
                                    xpos = mxpos,
                                    ypos = mypos,


                                };
                                Minors[minor_index] = null;
                                index_min = 0;

                                if (minor_index < 0)
                                {

                                    break;
                                }

                            }
                            else
                            {

                                minor_index = Keep_arr[minor_index];



                            }
                        }



                    }



                }

                else if (algo_value == 4)
                {
                    for (int i = 0; i < xaxis * yaxis; i++)
                    {
                        chance = rnd1.Next(0, 2);
                        if (chance == 0)
                        {
                            local_index = rnd2.Next(0, locals_num);
                            while (Locals[i].rate > lower_bound)
                            {
                                local_index = rnd2.Next(0, locals_num);

                                Locals[local_index].type = 2;
                                Locals[local_index].color = "red";
                                map[Locals[local_index].xpos, Locals[local_index].ypos] = 2;

                            }



                        }




                    }




                }


            }

            else if (no_free_cells == 0)
            {
                if (utility_check == 0)

                {
                    for (int i = 0; i < locals_num; i++)
                    {
                        if (Locals[i].rate < lower_bound || Locals[i].rate > upper_bound)
                        {
                            x = rnd1.Next(0, xaxis);
                            y = rnd1.Next(0, yaxis);

                            while (map[x, y] == 1 || map[x, y] == 2 || map[x, y] == 3)
                            {
                                x = rnd1.Next(0, xaxis);
                                y = rnd1.Next(0, yaxis);
                            }

                            map[Locals[i].xpos, Locals[i].ypos] = 0; // clear the previous position on the map
                            Locals[i].xpos = x;
                            Locals[i].ypos = y;
                        }

                        map[Locals[i].xpos, Locals[i].ypos] = 1;
                    }
                    for (int i = 0; i < min_num; i++)
                    {
                        if (Minors[i].rate < lower_bound || Minors[i].rate > upper_bound)
                        {
                            x = rnd1.Next(0, xaxis);
                            y = rnd1.Next(0, yaxis);

                            while (map[x, y] == 1 || map[x, y] == 2 || map[x, y] == 3)
                            {
                                x = rnd1.Next(0, xaxis);
                                y = rnd1.Next(0, yaxis);
                            }

                            map[Minors[i].xpos, Minors[i].ypos] = 0;// clear the previous position on the map
                            Minors[i].xpos = x;
                            Minors[i].ypos = y;
                        }


                        map[Minors[i].xpos, Minors[i].ypos] = 2;
                    }
                }
                else
                {

                    for (int i = 0; i < locals_num; i++)
                    {
                        double[] uti;
                        uti = new double[3];

                        uti[1] = Locals[i].xpos;
                        uti[2] = Locals[i].ypos;
                        uti[0] = utility(map, Locals[i].xpos, Locals[i].ypos, Locals[i].type);
                        double s = 0.0;
                        if (Locals[i].rate < lower_bound || Locals[i].rate > upper_bound)
                        {

                            for (int k = 0; k < xaxis; k++)
                            {
                                for (int l = 0; l < yaxis; l++)
                                {

                                    if (uti[0] < utility(map, k, l, 0) && (map[k, l] == 0))
                                    {
                                        uti[0] = utility(map, k, l, 0);
                                        uti[1] = k;
                                        uti[2] = l;
                                    }

                                }
                            }

                            map[Locals[i].xpos, Locals[i].ypos] = 0;
                            Locals[i].xpos = Convert.ToInt32(uti[1]);
                            Locals[i].ypos = Convert.ToInt32(uti[2]);
                            Locals[i].utility = uti[0];
                            map[Locals[i].xpos, Locals[i].ypos] = 1;
                        }



                    }
                    for (int i = 0; i < min_num; i++)
                    {
                        double[] uti;

                        uti = new double[3];

                        uti[1] = Minors[i].xpos;
                        uti[2] = Minors[i].ypos;
                        uti[0] = utility(map, Minors[i].xpos, Minors[i].ypos, Minors[i].type);
                        double s = 0.0;
                        if (Minors[i].rate < lower_bound || Minors[i].rate > upper_bound)
                        {

                            for (int k = 0; k < xaxis; k++)
                            {
                                for (int l = 0; l < yaxis; l++)
                                {

                                    if (uti[0] < utility(map, k, l, 1) && (map[k, l] == 0))
                                    {
                                        uti[0] = utility(map, k, l, 1);
                                        uti[1] = k;
                                        uti[2] = l;
                                    }

                                }
                            }
                            map[Minors[i].xpos, Minors[i].ypos] = 0;
                            Minors[i].xpos = Convert.ToInt32(uti[1]);
                            Minors[i].ypos = Convert.ToInt32(uti[2]);
                            Minors[i].utility = uti[0];
                            map[Minors[i].xpos, Minors[i].ypos] = 2;
                        }



                    }



                }

            }

            else if (no_free_cells == 2)


            {


                for (int j = 0; j < locals_num; j++)
                {
                    if ((Locals[j].rate < lower_bound && Locals[j].type == 1) ||
                                     (Locals[j].rate < lower_bound2 && Locals[j].type == 2))
                    {
                        agents.Add(Locals[j]);

                    }



                }
                for (int j = 0; j < min_num; j++)
                {
                    if ((Minors[j].rate < lower_bound2 && Minors[j].type == 2) ||
                                (Minors[j].rate < lower_bound && Minors[j].type == 1))
                    {
                        agents.Add(Minors[j]);
                    }
                }
                int i = 0;
                while (i < agents.Count)
                {
                    dice = rnd3.Next(0, 2);


                    //if (dice == 0)
                    //{
                    local_index = rnd2.Next(0, locals_num);



                    dice = rnd3.Next(agents.Count);

                    if (agents[dice].type == 1)
                    {
                        map[agents[dice].xpos, agents[dice].ypos] = 2;
                        agents[dice].type = 2;
                      

                        agents.Clear();

                        rate_check_for_all(map);
                        for (int j = 0; j < locals_num; j++)
                        {
                            if ((Locals[j].rate < lower_bound && Locals[j].type==1) || 
                                    (Locals[j].rate < lower_bound2 && Locals[j].type == 2))
                            {
                                agents.Add(Locals[j]);

                            }



                        }
                        for (int j = 0; j < min_num; j++)
                        {
                            if ((Minors[j].rate < lower_bound2 && Minors[j].type==2) ||
                                (Minors[j].rate < lower_bound && Minors[j].type == 1))
                            {
                                agents.Add(Minors[j]);
                            }
                        }

                       

                    }

                    else if (agents[dice].type == 2)
                    {
                        map[agents[dice].xpos, agents[dice].ypos] = 1;
                        agents[dice].type = 1;
                      
                        rate_check_for_all(map);
                        agents.Clear();

                        for (int j = 0; j < locals_num; j++)
                        {
                            if ((Locals[j].rate < lower_bound && Locals[j].type == 1) ||
                                     (Locals[j].rate < lower_bound2 && Locals[j].type == 2))
                            {
                                agents.Add(Locals[j]);

                            }



                        }
                        for (int j = 0; j < min_num; j++)
                        {
                            if ((Minors[j].rate < lower_bound2 && Minors[j].type == 2) ||
                                (Minors[j].rate < lower_bound && Minors[j].type == 1))
                            {
                                agents.Add(Minors[j]);
                            }
                        }

                    }
                    i = i +1;
                }///


                //    while (Locals[local_index].rate > lower_bound)

                //    {
                //        local_index = rnd2.Next(0, locals_num);
                //    }

                //    if (Locals[local_index].type == 1 && Locals[local_index].rate < lower_bound)
                //    {
                //        map[Locals[local_index].xpos, Locals[local_index].ypos] = 2;
                //        Locals[local_index].type = 2;


                //    }
                //    else if (Locals[local_index].type == 2 && Locals[local_index].rate < lower_bound)
                //    {

                //        map[Locals[local_index].xpos, Locals[local_index].ypos] = 1;
                //        Locals[local_index].type = 1;

                //    }




                //}
                //else if (dice == 1)
                //{
                //    minor_index = rnd2.Next(0, min_num);
                //    while (Minors[minor_index].rate > lower_bound)
                //    {
                //        minor_index = rnd2.Next(0, min_num);
                //    }



                //    if (Minors[minor_index].rate < lower_bound && Minors[minor_index].type == 2)
                //    {
                //        map[Minors[minor_index].xpos, Minors[minor_index].ypos] = 1;
                //        Minors[minor_index].type = 1;
                //    }

                //    else if (Minors[minor_index].rate < lower_bound && Minors[minor_index].type == 1)
                //    {
                //        map[Minors[minor_index].xpos, Minors[minor_index].ypos] = 2;
                //        Minors[minor_index].type = 2;

                //    }




            
                    
                }


                //for (int i = 0; i < locals_num; i++)
                //{


                //    if (((Locals[i].rate < lower_bound || Locals[i].rate > upper_bound))&&( Locals[i].type==1))

                //    {
                //        map[Locals[i].xpos, Locals[i].ypos] = 2;
                //        Locals[i].type = 2;


                //    }
                //    else if((((Locals[i].rate < lower_bound || Locals[i].rate > upper_bound)) && (Locals[i].type == 2)))
                //    {
                //        map[Locals[i].xpos, Locals[i].ypos] = 1;
                //        Locals[i].type = 1;


                //    }
                //}
                //for (int i = 0; i < min_num; i++)
                //{


                //    if (((Minors[i].rate < lower_bound || Minors[i].rate > upper_bound))&& Minors[i].type ==2)

                //    {
                //        map[Minors[i].xpos, Minors[i].ypos] = 1;
                //        Minors[i].type = 1;
                //    }
                //    else if (((Minors[i].rate < lower_bound || Minors[i].rate > upper_bound)) && Minors[i].type == 1)
                //    {
                //        map[Minors[i].xpos, Minors[i].ypos] = 2;
                //        Minors[i].type = 2;


                //    }
                //}

                return map;
            }// end of continue function, it  basically includes the main algorithm of the code
        
        public double utility(Matrix<double> map, int xpos, int ypos, int type)// calculates 
                                                                               //the continous utility
        {
            double utility_value = 0.0;
            int[,] t;
            double c = 1.0;
            double a = 0.0;
            t = rate_check(xpos, ypos, map);
            if (type == 0)
            {
                if (t[0, 0] == 0)
                {
                    a = 0;
                }
                else
                {
                    a = t[0, 0] / c / (t[1, 0] + t[0, 0]);
                }


            }
            else if (type == 1)
            {
                if (t[1, 0] == 0)
                {
                    a = 0;
                }
                else
                {
                    a = t[1, 0] / c / (t[1, 0] + t[0, 0]);
                }

            }

            if (utility_check == 1)   // Triangular Utility function
            {

                if (lower_bound <= a && a <= 0.5)
                {

                    utility_value = (a - lower_bound) / (0.5 - lower_bound);

                }
                else if (a >= 0.5 && a < upper_bound)
                {

                    utility_value = (upper_bound - a) / (upper_bound - 0.5);


                }
                else if (a < lower_bound)
                {
                    utility_value = 0;

                }
                else if (a > upper_bound)
                {
                    utility_value = 0;

                }
            }
            else if (utility_check == 2) //Circular utility function
            {

                if (a < lower_bound)
                {

                    utility_value = 0;
                }

                else if (lower_bound <= a && a <= upper_bound)
                {

                    utility_value = Math.Sqrt(Math.Pow((upper_bound - lower_bound) / 2, 2) - Math.Pow(a - 0.5, 2));


                }
                else
                {
                    utility_value = 0;

                }

            }
            else if (utility_check == 3)// asymmetric favors similarity
            {
                if (a <= 0.5)
                {
                    utility_value = a / 0.5;

                }

                else if (a > 0.5)

                {
                    utility_value = ((0.75 - 1) * a + 1 - 0.75 * 0.5) / 0.5;
                }


            }

            else if (utility_check == 4) // asymmetric favors dissimilarity
            {

                if (a <= 0.5)
                {

                    utility_value = ((1 - 0.75) * a + 0.75 * 0.5) / 0.5;
                }

                else if (a > 0.5)

                {
                    utility_value = 1 - a / 0.5;
                }



            }

            return utility_value;
        }
        public void update_map()// update the visual map
        {
            Brush bbrush = (Brush)Brushes.Green;
            Brush cbrush = (Brush)Brushes.Red;
            Brush dbrush = (Brush)Brushes.Black;
            Brush ebrush = (Brush)Brushes.White;
            Graphics g;
            g = this.CreateGraphics();
            // g.Clear(Color.White);
            g.FillRectangle(ebrush, 0, 0, xaxis * yaxis, xaxis * yaxis);
            Pen p = new Pen(Color.Black);
            for (int i = 0; i < locals_num; ++i)
            {

                while (Locals[i] == null && i < locals_num)
                {
                    i = i + 1;

                }


                if (Locals[i].type == 1 && Locals[i] != null)
                {
                    g.FillRectangle(bbrush, Locals[i].xpos * cellSize, Locals[i].ypos * cellSize, cellSize, cellSize);
                }
                else
                {
                    g.FillRectangle(cbrush, Locals[i].xpos * cellSize, Locals[i].ypos * cellSize, cellSize, cellSize);
                }
            }

            for (int i = 0; i < min_num; ++i)
            {
                while (Minors[i] == null)
                { i = i + 1; }
                if (Minors[i].type == 2)
                {
                    g.FillRectangle(cbrush, Minors[i].xpos * cellSize, Minors[i].ypos * cellSize, cellSize, cellSize);
                }
                else
                {
                    g.FillRectangle(bbrush, Minors[i].xpos * cellSize, Minors[i].ypos * cellSize, cellSize, cellSize);
                }
            }
            if (geo_value == 1)
            {
                for (int k = 0; k < xaxis; k++)
                    for (int l = yaxis / 2; l <= yaxis / 2 + 1; l++)

                    {
                        {
                            g.FillRectangle(dbrush, k * cellSize, l * cellSize, cellSize, cellSize);


                        }

                    }
            }
            else if (geo_value == 2)
            {
                int ja = 0;
                for (int i = yaxis - 1; i >= 0; i--)
                {

                    g.FillRectangle(dbrush, ja * cellSize, i * cellSize, cellSize, cellSize);

                    ja = ja + 1;
                }

            }

            for (int y = 0; y < yaxis; ++y)
            {
                g.DrawLine(p, 0, y * cellSize, yaxis * cellSize, y * cellSize);
            }

            for (int x = 0; x < xaxis; ++x)
            {
                g.DrawLine(p, x * cellSize, 0, x * cellSize, xaxis * cellSize);
            }




        }

        public double economy_check(int xpos, int ypos)
        {
            int econ_stat = 1;

            if (xpos < xaxis / 5 && ypos < yaxis / 5)
            {
                econ_stat = 2;
            }
            return econ_stat;

        }

        public Matrix<double> continue_2(Matrix<double> map)
        {

            Matrix<double> map1 = map;
            //Draw_World(xaxis, yaxis);
            //update_map();
            map1 = continue_to(map);

            return map1;

        }
        public void button1_Click(object sender, EventArgs l)
        {

            //Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
            //Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
            //Worksheet ws = (Worksheet)xla.ActiveSheet;
            //xla.Visible = true;

            double A = 0;
            double B = 0;
            double C = 0;
            double D = 0;
            double E = 0;
            double F = 0;
            double G = 0;
            double H = 0;
            double I = 0;
            double J = 0;
            double K = 0;
            double L = 0;
            double M = 0;
            double N = 0;

            int kuar = 0;
            int say = 0;
            for (int a = 0; a <= sim_value - 1; a++)
            {
                Draw_World(xaxis, yaxis);
                frac_count = 0;

                Matrix<double> map = execute();

                for (int j = 0; j < min_num; j++)
                {

                    Minors[j].s = 0;

                }
                for (int i = 0; i < locals_num; i++)
                {

                    Locals[i].s = 0;

                }

                double sum1 = 1;
                double sum2 = 1;

                while (sum1 + sum2 != 0)
                {
                    rate_check_for_all(map);

                    kuar = kuar + 1;

                    map = continue_2(map);

                    for (int i = 0; i < locals_num; i++)
                    {

                        if (Locals[i] != null)
                        {
                            if (((Locals[i].rate < lower_bound && Locals[i].type==1) || (Locals[i].rate > upper_bound && Locals[i].type == 1)) ||
                                    ((Locals[i].rate < lower_bound2 && Locals[i].type== 2)  || (Locals[i].rate >upper_bound && Locals[i].type ==2)))
                            {
                                Locals[i].s = 1;

                            }
                            else
                            {

                                Locals[i].s = 0;

                            }
                        }
                    }
                    for (int j = 0; j < min_num; j++)
                    {
                        if (Minors[j] != null)
                        {
                            if (((Minors[j].rate < lower_bound2 && Minors[j].type == 2) || (Minors[j].rate > upper_bound && Minors[j].type == 2)) ||
                                    ((Minors[j].rate < lower_bound && Minors[j].type == 1) || (Minors[j].rate > upper_bound && Minors[j].type == 1)))
                            {
                                Minors[j].s = 1;


                            }
                            else
                            {

                                Minors[j].s = 0;


                            }
                        }
                    }
                    sum1 = 0;
                    for (int i = 0; i < locals_num; i++)
                    {
                        if (Locals[i] != null)
                            sum1 = sum1 + Locals[i].s;


                    }
                    sum2 = 0;

                    for (int j = 0; j < min_num; j++)
                    {
                        if (Minors[j] != null)
                            sum2 = sum2 + Minors[j].s;



                    }

                    Random rnd4 = new Random();
                    int candidate_number;
                    int mypos;
                    int mxpos;
                    int lxpos;
                    int lypos;

                    if (((sum1 == 0 && sum2 > 0) || (sum2 == 0 && sum1 > 0)) && algo_value == 0)
                    {

                        say = say + 1;
                        if (say == 1)
                        {
                            ave_uti.Text = Convert.ToString(sum1 + sum2);

                        }
                        for (int i = 0; i < locals_num; i++)
                        {

                            if (Locals[i].s == 1)
                            {
                                candidate_number = rnd4.Next(0, min_num);
                                lxpos = Locals[i].xpos;
                                lypos = Locals[i].ypos;
                                mxpos = Minors[candidate_number].xpos;
                                mypos = Minors[candidate_number].ypos;
                                Locals[i].xpos = mxpos;
                                Locals[i].ypos = mypos;
                                map[mxpos, mypos] = 1;
                                Minors[candidate_number].xpos = lxpos;
                                Minors[candidate_number].ypos = lypos;
                                map[lxpos, lypos] = 2;

                            }


                        }
                        for (int i = 0; i < min_num; i++)
                        {

                            if (Minors[i].s == 1)
                            {
                                candidate_number = rnd4.Next(0, locals_num);
                                lxpos = Locals[candidate_number].xpos;
                                lypos = Locals[candidate_number].ypos;
                                mxpos = Minors[i].xpos;
                                mypos = Minors[i].ypos;
                                Locals[candidate_number].xpos = mxpos;
                                Locals[candidate_number].ypos = mypos;
                                map[mxpos, mypos] = 1;
                                Minors[i].xpos = lxpos;
                                Minors[i].ypos = lypos;
                                map[lxpos, lypos] = 2;

                            }


                        }




                    }


                }

                //for (int i = 0; i < 10000; i++)

                //{
                //    rate_check_for_all(map);



                //    map = continue_2(map);


                //    //MessageBox.Show(Convert.ToString(z));


                //}

                double[,] ndf;
                ndf = new double[xaxis * yaxis, 3];
                double[,] ndf2;
                double[,] ndf3;
                ndf2 = new double[xaxis * yaxis, 3];
                ndf3 = new double[xaxis * yaxis, 3];
                double ndf_sum = 0;
                double ndf_sum2 = 0;
                double ndf_sum3 = 0;
                double ndf_sum11 = 0;
                double ndf_sum12 = 0;
                double ndf_sum13 = 0;
                double ndf_sum21 = 0;
                double ndf_sum22 = 0;
                double ndf_sum23 = 0;
                int count_empties = 0; 

                int count_locals = 0;
                int count_minors = 0;
                for (int i = 0; i < xaxis; i++)  // NDF Calculations
                {
                    for (int j = 0; j < yaxis; j++)
                    {
                        var ndf_matrice = rate_check(i, j, map);
                        if (map[i, j] == 0)
                        {

                            ndf[count_empties, 0] = ndf_matrice[2, 0] / ((ndf_matrice[0, 0] + ndf_matrice[1, 0] + ndf_matrice[2, 0]) * 1.00);
                            ndf[count_empties, 1] = ndf_matrice[2, 1] / ((ndf_matrice[0, 1] + ndf_matrice[1, 1] + ndf_matrice[2, 1]) * 1.00);
                            ndf[count_empties, 2] = ndf_matrice[2, 2] / ((ndf_matrice[0, 2] + ndf_matrice[1, 2] + ndf_matrice[2, 2]) * 1.00);
                            count_empties = count_empties + 1;
                        }
                        else if (map[i, j] == 1)
                        {
                            ndf2[count_locals, 0] = ndf_matrice[0, 0] / ((ndf_matrice[0, 0] + ndf_matrice[1, 0] + ndf_matrice[2, 0]) * 1.00);
                            ndf2[count_locals, 1] = ndf_matrice[0, 1] / ((ndf_matrice[0, 1] + ndf_matrice[1, 1] + ndf_matrice[2, 1]) * 1.00);
                            ndf2[count_locals, 2] = ndf_matrice[0, 2] / ((ndf_matrice[0, 2] + ndf_matrice[1, 2] + ndf_matrice[2, 2]) * 1.00);
                            count_locals = count_locals + 1;


                        }
                        else if (map[i, j] == 2)
                        {
                            ndf3[count_minors, 0] = ndf_matrice[1, 0] / ((ndf_matrice[0, 0] + ndf_matrice[1, 0] + ndf_matrice[2, 0]) * 1.00);
                            ndf3[count_minors, 1] = ndf_matrice[1, 1] / ((ndf_matrice[0, 1] + ndf_matrice[1, 1] + ndf_matrice[2, 1]) * 1.00);
                            ndf3[count_minors, 2] = ndf_matrice[1, 2] / ((ndf_matrice[0, 2] + ndf_matrice[1, 2] + ndf_matrice[2, 2]) * 1.00);
                            count_minors = count_minors + 1;


                        }


                    }


                }
                for (int i = 0; i < count_empties; i++)
                {

                    ndf_sum = ndf_sum + ndf[i, 0];
                    ndf_sum2 = ndf_sum2 + ndf[i, 1];
                    ndf_sum3 = ndf_sum3 + ndf[i, 2];
                }

                ndf_sum = ndf_sum / count_empties;
                ndf_sum2 = ndf_sum2 / count_empties;
                ndf_sum3 = ndf_sum3 / count_empties;

                for (int i = 0; i < count_locals; i++)
                {

                    ndf_sum11 = ndf_sum11 + ndf2[i, 0];
                    ndf_sum12 = ndf_sum12 + ndf2[i, 1];
                    ndf_sum13 = ndf_sum13 + ndf2[i, 2];
                }

                ndf_sum11 = ndf_sum11 / count_locals;
                ndf_sum12 = ndf_sum12 / count_locals;
                ndf_sum13 = ndf_sum13 / count_locals;

                for (int i = 0; i < count_minors; i++)
                {

                    ndf_sum21 = ndf_sum21 + ndf3[i, 0];
                    ndf_sum22 = ndf_sum22 + ndf3[i, 1];
                    ndf_sum23 = ndf_sum23 + ndf3[i, 2];
                }

                ndf_sum21 = ndf_sum21 / count_minors;
                ndf_sum22 = ndf_sum22 / count_minors;
                ndf_sum23 = ndf_sum23 / count_minors;



                update_map();
                double sum_1 = 0;
                double sum_2 = 0;
                double sum_3 = 0;
                double sum_4 = 0;
                double sum_5 = 0;
                double sum_6 = 0;
                double sum_7 = 0;
                double sum_8 = 0;
                for (int i = 0; i < locals_num; i++) // Segregation index calculations
                {
                    while (Locals[i] == null && i <= locals_num)
                    { i = i + 1; }
                    sum_1 = sum_1 + Locals[i].utility;
                    sum_2 = sum_2 + Locals[i].mixity;
                    sum_3 = sum_3 + Locals[i].rate;
                    sum_4 = sum_4 + Locals[i].FSI;
                    sum_5 = sum_5 + Locals[i].het_neigh;
                    sum_6 = sum_6 + Locals[i].total_neigh;
                    Locals[i].sim_neigh = Locals[i].het_neigh / Locals[i].total_neigh;
                    sum_7 = sum_7 + Locals[i].sim_neigh;
                    sum_8 = sum_8 + Locals[i].seperatist;

                }
                for (int j = 0; j < min_num; j++)
                {
                    while (Minors[j] == null && j <= min_num)
                    { j = j + 1; }
                    sum_1 = sum_1 + Minors[j].utility;
                    sum_2 = sum_2 + Minors[j].mixity;
                    sum_3 = sum_3 + Minors[j].rate;
                    sum_4 = sum_4 + Minors[j].FSI;
                    sum_5 = sum_5 + Minors[j].het_neigh;
                    sum_6 = sum_6 + Minors[j].total_neigh;
                    Minors[j].sim_neigh = Minors[j].het_neigh / Minors[j].total_neigh;
                    sum_7 = sum_7 + Minors[j].sim_neigh;
                    sum_8 = sum_8 + Minors[j].seperatist;
                }

                

                

                ave_sim_neigh.Text = Convert.ToString(1 - sum_7 / (locals_num + min_num));
                ave_FSI.Text = Convert.ToString(sum_4 / (locals_num + min_num));
                NDF_emp.Text = Convert.ToString(Math.Round(ndf_sum, 3));
                NDF_emp_2.Text = Convert.ToString(Math.Round(ndf_sum2, 3));
                NDF_emp_3.Text = Convert.ToString(Math.Round(ndf_sum3, 3));
                ndf_11.Text = Convert.ToString(Math.Round(ndf_sum11, 3));
                ndf_12.Text = Convert.ToString(Math.Round(ndf_sum12, 3));
                ndf_13.Text = Convert.ToString(Math.Round(ndf_sum13, 3));
                ndf_21.Text = Convert.ToString(Math.Round(ndf_sum21, 3));
                ndf_22.Text = Convert.ToString(Math.Round(ndf_sum22, 3));
                ndf_23.Text = Convert.ToString(Math.Round(ndf_sum23, 3));

             

                exp_het = exp_het * sum_6;

                A = A + sum_1 / (locals_num + min_num);//utility
                B = B + (1 - sum_7 / (locals_num + min_num));// average similar neighbor
                C = C + sum_5 / sum_6;//mixity
                D = D + (exp_het - sum_5) / exp_het;//FSI
                E = E + ndf_sum; //NDF
                F = F + ndf_sum2;//
                G = G + ndf_sum3;//
                H = H + sum_8;
                I = I + ndf_sum11;
                J = J + ndf_sum12;
                K = K + ndf_sum13;
                L = L + ndf_sum21;
                M = M + ndf_sum22;
                N = N + ndf_sum23;
                SEPAR[a] = sum_8;
                
 

                FSI_VALUE[a] = (exp_het - sum_5) / exp_het;  // Put segregation indexes to vector
                AVE_VALUE[a] = (1 - sum_7 / (locals_num + min_num));
                MIX[a] = sum_5 / sum_6;

                variance_FSI = FSI_VALUE.Variance();
                variance_ASN = AVE_VALUE.Variance();
                variance_MIX = MIX.Variance();

                for (int i = 0; i < locals_num; i++)  // Count the number of agent types
                {
                    if (Locals[i].type == 1)
                    {
                        number_loc = number_loc + 1;


                    }

                    else if (Locals[i].type == 2)
                    {

                        number_min = number_min + 1;
                    }


                }
                for (int i = 0; i < min_num; i++)
                {
                    if (Minors[i].type == 1)
                    {
                        number_loc = number_loc + 1;


                    }

                    else if (Minors[i].type == 2)
                    {

                        number_min = number_min + 1;
                    }


                }

                simu_number.Text = Convert.ToString(a + 1);
                local_number.Text = Convert.ToString(number_loc);
                minor_number.Text = Convert.ToString(number_min);
                loc_number[a] = number_loc*1.00;
                mino_number[a] = number_min*1.00;
                number_loc = 0;
                number_min = 0;

                agents.Clear();

                for (int t = 0; t < yaxis / 5; t++)
                {

                    for (int k = 0; k < xaxis / 5; k++)
                    {
                        for (int i = 5 * k; i < 5 * k + 5; i++)

                        {
                            for (int j = 5 * t; j < 5 * t + 5; j++)
                            {

                                if (map[i, j] == 1)
                                {

                                    count_green[1] = count_green[1] + 1;

                                }
                                else if (map[i, j] == 2)
                                {

                                    count_red[1] = count_red[1] + 1;

                                }



                            }


                        }
                        frac_count = frac_count + 1;

                        fract[a, frac_count] = count_green[1] / (count_green[1] * 1.00 + count_red[1]);
                        count_red[1] = 0;
                        count_green[1] = 0;
                    }
                }


                for (int t = 0; t < yaxis / 10; t++)
                {

                    for (int k = 0; k < xaxis / 10; k++)
                    {
                        for (int i = 10 * k; i < 10 * k + 10; i++)

                        {
                            for (int j = 10* t; j < 10 * t + 10; j++)
                            {

                                if (map[i, j] == 1)
                                {

                                    count_green[2] = count_green[2] + 1;

                                }
                                else if (map[i, j] == 2)
                                {

                                    count_red[2] = count_red[2] + 1;

                                } 

                                 

                            }


                        }
                        frac_count = frac_count + 1;

                        fract[a, frac_count] = count_green[2] / (count_green[2] * 1.00 + count_red[2]);
                        count_red[2] = 0;
                        count_green[2] = 0;
                    }
                }


                for (int t = 0; t < yaxis/20; t++)
                {


                    for (int k = 0; k < xaxis / 20; k++)
                    {
                        for (int i = 20*k; i < 20*k + 20; i++)

                        {
                            for (int j = 20*t; j < 20*t+20; j++)
                            {

                                if (map[i, j] == 1)
                                {

                                    count_green[3] = count_green[3] + 1;

                                }
                                else if (map[i, j] == 2)
                                {

                                    count_red[3] = count_red[3] + 1;

                                }



                            }

                           
                        }
                        frac_count = frac_count + 1;

                        fract[a,frac_count] = count_green[3] / (count_green[3] * 1.00 + count_red[3]);
                        count_red[3] = 0;
                        count_green[3] = 0;
                    }
                }






            }// end of simulations






            

            ave_sim_neigh.Text = Convert.ToString(B / sim_value);
            ave_mix.Text = Convert.ToString(C / sim_value);
            ave_FSI.Text = Convert.ToString(D / sim_value);
            NDF_emp.Text = Convert.ToString(Math.Round(E, 3) / sim_value);
            NDF_emp_2.Text = Convert.ToString(Math.Round(F, 3) / sim_value);
            NDF_emp_3.Text = Convert.ToString(Math.Round(G, 3) / sim_value);
            ndf_11.Text = Convert.ToString(Math.Round(I, 3) / sim_value);
            ndf_12.Text = Convert.ToString(Math.Round(J, 3) / sim_value);
            ndf_13.Text = Convert.ToString(Math.Round(K, 3) / sim_value);
            ndf_21.Text = Convert.ToString(Math.Round(L, 3) / sim_value);
            ndf_22.Text = Convert.ToString(Math.Round(M, 3) / sim_value);
            ndf_23.Text = Convert.ToString(Math.Round(N, 3) / sim_value);
            sepera.Text = Convert.ToString(H / sim_value);
            var_asn.Text = Convert.ToString(Math.Round(variance_ASN,5));
            var_fsi.Text = Convert.ToString(Math.Round(variance_FSI,5));
            var_mix.Text = Convert.ToString(Math.Round(variance_MIX,5));
            local_number.Text = Convert.ToString(loc_number.Sum() / sim_value);
            minor_number.Text = Convert.ToString(mino_number.Sum() / sim_value);
            min_var.Text = Convert.ToString(Math.Round(mino_number.StandardDeviation(),3));
            sep_var.Text = Convert.ToString(Math.Round(SEPAR.StandardDeviation(),3));

            //Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
            //Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
            //Worksheet ws = (Worksheet)xla.ActiveSheet;
            //Microsoft.Office.Interop.Excel.Range rng = ws.Cells.get_Resize(fract.GetLength(0));

            //rng.Value2 = fract;
            //xla.Visible = true;


        }

        private void button2_Click(object sender, EventArgs e)
        {
        }


    }



    public class Agents
    {
        public int xpos { get; set; }
        public int ypos { get; set; }
        public double rate { get; set; }
        public double mixity { get; set; }
        public double het_neigh { get; set; }
        public double total_neigh { get; set; }

        public double emp_neigh { get; set; }
        public double utility { get; set; }

        public double sim_neigh { get; set; }
        public string color { get; set; }
        public int type { get; set; }

        public int seperatist { get; set; }
        public double FSI { get; set; }
        public int s { get; set; }

    }


}