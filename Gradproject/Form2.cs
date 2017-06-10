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

using MathNet.Numerics.Statistics;
using Microsoft.Office.Interop.Excel;
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
        public int cellSize = 2;
        public double lower_bound = 0.0;
        public double lower_bound2 = 0.0;
        public double upper_bound = 0.0;
        public double upper_bound2 = 0.0;
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
        public double count_green;
        public double count_red;
        public double[,] fract;
        public int frac_count;
        public double[,] prob_dist;
        public double[,] prob_dist1;
        public double[,] prob_dist2;
        public int queue;
        public int queue1;
        public int unhappyloc;
        public int unhappymin;
        public int[,] unhappy_array;
        public int w_size1;
        //End of universal variables
        public List<Agents> agents = new List<Agents>();
        public List<Agents> agents2 = new List<Agents>();
        Agents[] Locals;

        Agents[] Minors;

        


        public int sim_value = 0;





        public Form2(string population, string minority, string x_axis, string y_axis, string lowerbound, string lowerbound2,
            string eco, string upperbound, string utilitycheck, string sim, string geo, string no_freecells,
            string algo, string upperbound2, string wsize)

        {   // Get the values from previous form

            InitializeComponent();

            // Convert text variables
            locals_num = Convert.ToInt32(population);
            min_num = Convert.ToInt32(minority);
            xaxis = Convert.ToInt32(x_axis);
            yaxis = Convert.ToInt32(y_axis);
            lower_bound = Convert.ToDouble(lowerbound);
            lower_bound2 = Convert.ToDouble(lowerbound2);
            econ = Convert.ToInt32(eco);
            upper_bound = Convert.ToDouble(upperbound);
            upper_bound2 = Convert.ToDouble(upperbound2);
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
            Locals = new Agents[xaxis * xaxis];
            Minors = new Agents[xaxis * xaxis];
           // fract = new double[50,1500000];
            prob_dist = new double[13, 1000];
            prob_dist1 = new double[13, 1000];
            prob_dist2 = new double[1000, 100];
            unhappy_array = new int[10000, 100];
            w_size1 = Convert.ToInt32(wsize);
        }

       


        public void Draw_World(int x_axis, int y_axis) //Draw the initial empty world

        {
            Graphics g;
          
            Pen p = new Pen(Color.Black);
            Brush bBrush = (Brush)Brushes.Gray;

            g = this.CreateGraphics();
           

         

 

            // g.Clear(Color.White);
          
           

            //for (int y = 0; y < y_axis; ++y)
            //{
            //    g.DrawLine(p, 0, y * cellSize, y_axis * cellSize, y * cellSize);
            //}

            //for (int x = 0; x < x_axis; ++x)
            //{
            //    g.DrawLine(p, x * cellSize, 0, x * cellSize, x_axis * cellSize);
            //}
            exp_het = 2 * (locals_num / (locals_num + min_num * 1.00)) * (min_num / (locals_num + min_num * 1.00));



        }


        public MathNet.Numerics.LinearAlgebra.Matrix<double> execute()
        // Make the initial implanting and create agents
        // and draw the initial world
        {
            Keep_arr = new int[10000];
            Pen bluePen = new Pen(Color.Blue, 2);
            Pen orangePen = new Pen(Color.Orange, 2);
            Pen aquqPen = new Pen(Color.Aqua, 2);

            Matrix<double> map = Matrix<double>.Build.Dense(xaxis, yaxis, 0);

            // Check for world type///////////////////////////////////////////
            

            Random rnd = new Random();


            /////// Attach local and minor agents' positions ///////////////////////
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
                  
                };

                Minors[i].type = 2;
                map[x, y] = 2;


               




            }

            //////////////////////////////////////////////////////////////////
            Brush bbrush = (Brush)Brushes.Green;
            Brush cbrush = (Brush)Brushes.Red;
            Brush dbrush = (Brush)Brushes.Black;
            Pen p = new Pen(Color.Black);

            Graphics g;
            g = this.CreateGraphics();

            ///// Place the agetns in the map/////////////////////////////////
            for (int i = 0; i < locals_num; ++i)
            {
                g.FillRectangle(bbrush, Locals[i].xpos * cellSize, Locals[i].ypos * cellSize, cellSize, cellSize);
            }

            for (int i = 0; i < min_num; ++i)
            {
                g.FillRectangle(cbrush, Minors[i].xpos * cellSize, Minors[i].ypos * cellSize, cellSize, cellSize);
            }
            //////////////////////////////////////////////////////////////////

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

            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, Convert.ToInt16(xaxis / 3), Convert.ToInt16(xaxis / 3));
            System.Drawing.Rectangle rect1 = new System.Drawing.Rectangle(
                Convert.ToInt16(xaxis - xaxis / 4), xaxis - xaxis / 4, Convert.ToInt16(xaxis / 2), Convert.ToInt16(xaxis / 2));
            System.Drawing.Rectangle rect2 = new System.Drawing.Rectangle(cellSize *
                Convert.ToInt16(xaxis - xaxis / 4), cellSize * Convert.ToInt16(xaxis - xaxis / 4), Convert.ToInt16(xaxis / 4), Convert.ToInt16(xaxis / 4));


            //for (int y = 0; y < y_axis; ++y)
            //{
            //    g.DrawLine(p, 0, y * cellSize, y_axis * cellSize, y * cellSize);
            //}

            //for (int x = 0; x < x_axis; ++x)
            //{
            //    g.DrawLine(p, x * cellSize, 0, x * cellSize, x_axis * cellSize);
            //}
           


            g.DrawRectangle(bluePen, rect);
            g.DrawRectangle(orangePen, rect1);
            g.DrawRectangle(aquqPen, rect2);
            //for (int y = 0; y < yaxis; ++y)
            //{
            //    g.DrawLine(p, 0, y * cellSize, yaxis * cellSize, y * cellSize);
            //}

            //for (int x = 0; x < xaxis; ++x)
            //{
            //    g.DrawLine(p, x * cellSize, 0, x * cellSize, xaxis * cellSize);
            //}





            return map;

        }   // end of execute function

        public int[,] count_unhappy(Matrix<double> map, int kuar, int a)
            //Count unhappy agents
        {
            rate_check_for_all(map);
            unhappyloc = 0;
            unhappymin = 0;
            for (int r = 0; r < locals_num; r++)
            {
                if ((Locals[r] != null && (Locals[r].rate < lower_bound || Locals[r].rate > upper_bound) &&   Locals[r].type==1) ||
                   (Minors[r] != null && (Minors[r].rate < lower_bound || Minors[r].rate > upper_bound) &&   Minors[r].type == 1))
                {
                    unhappyloc++;
                }
            }
            for (int r = 0; r < min_num; r++)
            {
                if (((Locals[r].rate < lower_bound2 || Locals[r].rate > upper_bound2) && Locals[r] != null && Locals[r].type == 2) ||
                   ((Minors[r].rate < lower_bound2 || Minors[r].rate > upper_bound2) && Minors[r] != null && Minors[r].type == 2))
                {
                    unhappymin++;
                }
            }
            unhappy_array[kuar, 2 * a] = unhappyloc;
            unhappy_array[kuar, 2 * a + 1] = unhappymin;

            return unhappy_array;

        }//end of count unhappy agent function
        public Matrix<double> AdjacentElements(Matrix<double> map2, int row, int column)
        {
            // Collect neighbor cells' positions
            Matrix<double> w = Matrix<double>.Build.Dense(100, 3, 5);

            int rows = map2.RowCount;
            int columns = map2.ColumnCount;

            int r = 0;
            int s = 0;
            int p = 0;
            //for (int j = row - 1; j <= row + 1; j++)
            //{
            //    for (int i = column - 1; i <= column + 1; i++)
            //    {
            //        if (i >= 0 && j >= 0 && i < columns && j < rows && !(j == row && i == column))
            //        {
            //            w[r, 0] = map2[j, i];
            //            r = r + 1;
            //        }

            //    }

            //}
            for (int j = row - w_size1; j <= row + w_size1; j++)
            {
                for (int i = column - w_size1; i <= column + w_size1; i++)
                {
                    if (!(j == row && i == column))
                    {
                        if (j >= 0 && i >= 0)
                        {
                            w[r, 0] = map2[j % xaxis, i % xaxis];
                            r = r + 1;
                        }
                        else if (j >= 0 && i < 0)
                        {
                            w[r, 0] = map2[j % xaxis, i + xaxis];
                            r = r + 1;

                        }
                        else if (j < 0 && i < 0)
                        {
                            w[r, 0] = map2[j + xaxis, i + xaxis];
                            r = r + 1;

                        }
                        else if (j < 0 && i >= 0)
                        {
                            w[r, 0] = map2[j + xaxis, i % xaxis];
                            r = r + 1;

                        }
                    }

                }

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
        public double rate_check_for_one(int lxpos, int lypos, Matrix<double> map)
        {
            int[,] a;
            int[,] b;
           
            double rate1=0;
          
            double c = 1.00;
            a = rate_check(lxpos, lypos, map);

            if (map[lxpos, lypos] == 1)
            {
                if (a[0, 0] == 0)
                {
                    rate1 = 0;
                }
                else
                {
                    rate1 = a[0, 0] / c / (a[1, 0] + a[0, 0]);
                }
            }

            else if (map[lxpos, lypos] == 2)
            {
                if (a[1, 0] == 0)
                {
                    rate1 = 0;
                }
                else
                {
                    rate1 = a[1, 0] / c / (a[1, 0] + a[0, 0]);
                }


            }

            //uti_map[Minors[i].xpos, Minors[i].ypos] = a;
          
            
                  

            return rate1;
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

            return uti_map.Transpose();
        }// end of rate_check_for_all func.

        public Matrix<double> continue_to(Matrix<double> map)//Reimplanting
        {
            Random rnd1 = new Random();
            Random rnd2 = new Random();
            Random rnd3 = new Random();
            int x;
            int y;
            int dice1;

            

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




            for (int j = 0; j < locals_num; j++)
            {
                if (((Locals[j].rate < lower_bound || Locals[j].rate > upper_bound) && Locals[j].type == 1) ||
                  ((Locals[j].rate < lower_bound2 || Locals[j].rate > upper_bound2) && Locals[j].type == 2))
                {
                    agents.Add(Locals[j]);

                }

            }
            for (int j = 0; j < min_num; j++)
            {
                if (((Minors[j].rate < lower_bound || Minors[j].rate > upper_bound) && Minors[j].type == 1) ||
                  ((Minors[j].rate < lower_bound2 || Minors[j].rate > upper_bound2) && Minors[j].type == 2))
                {
                    agents.Add(Minors[j]);
                }
            }
            int i = 0;

           

            //for (int j = 0; j < agents.Count; ++j)
            //{
            //    for (i = 0; i < agents.Count; ++i)
            //    {
            //        int randomIndex = rnd2.Next(agents.Count);
            //        var temp = agents[randomIndex];
            //        agents[randomIndex] = agents[i];
            //        agents[i] = temp;
            //    }
            //}

            for (dice1 = 0; dice1 < agents.Count; dice1++)


            {

                dice = rnd3.Next(0, agents.Count);

                if (agents[dice] != null&& rate_check_for_one(agents[dice].xpos, agents[dice].ypos,map)<lower_bound
                    || rate_check_for_one(agents[dice].xpos, agents[dice].ypos,map) > upper_bound)
                {
                    if (agents[dice].type == 1 && (agents[dice].rate < lower_bound || agents[dice].rate > upper_bound))
                    {
                        map[agents[dice].xpos, agents[dice].ypos] = 2;
                        agents[dice].type = 2;
                        agents.RemoveAt(dice);




                    }

                    else if (agents[dice].type == 2 && (agents[dice].rate < lower_bound2 || agents[dice].rate > upper_bound2))
                    {
                        map[agents[dice].xpos, agents[dice].ypos] = 1;
                        agents[dice].type = 1;
                        agents.RemoveAt(dice);

                    }

                }
            }
         
            agents.Clear();


            //while (i < agents.Count)
            //{
            //    dice = rnd3.Next(0, 2);


            //    //if (dice == 0)
            //    //{
            //    local_index = rnd2.Next(0, locals_num);

            //    dice = rnd3.Next(agents.Count);

            //    if (agents[dice].type == 1 && (agents[dice].rate < lower_bound || agents[dice].rate > upper_bound))
            //    {
            //        map[agents[dice].xpos, agents[dice].ypos] = 2;
            //        agents[dice].type = 2;
            //        agents.RemoveAt(dice);
            //        agents.RemoveAll(item => item == null);



            //    }

            //    else if (agents[dice].type == 2 && (agents[dice].rate < lower_bound2 || agents[dice].rate > upper_bound2))
            //    {
            //        map[agents[dice].xpos, agents[dice].ypos] = 1;
            //        agents[dice].type = 1;
            //        agents.RemoveAt(dice);
            //        agents.RemoveAll(item => item == null);


            //    }
            //    i = i + 1;
            //}///


            return map;
            }// end of continue function, it  basically includes the main algorithm of the code
          
        public void update_map()// update the visual map
        {
            Brush bbrush = (Brush)Brushes.Green;
            Brush cbrush = (Brush)Brushes.Red;
            Brush dbrush = (Brush)Brushes.Black;
            Brush ebrush = (Brush)Brushes.White;
            Pen bluePen = new Pen(Color.Blue, 2);
            Pen orangePen = new Pen(Color.Orange, 2);
            Pen aquqPen = new Pen(Color.Aqua, 2);

            Graphics g;
           
            g = this.CreateGraphics();
       
            // g.Clear(Color.White);
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0,0,Convert.ToInt16(xaxis/3), Convert.ToInt16(xaxis / 3)) ;
            System.Drawing.Rectangle rect1 = new System.Drawing.Rectangle(
                Convert.ToInt16(xaxis-xaxis/4), xaxis-xaxis/4, Convert.ToInt16(xaxis / 2), Convert.ToInt16(xaxis / 2));
            System.Drawing.Rectangle rect2 = new System.Drawing.Rectangle(cellSize * 
                Convert.ToInt16(xaxis - xaxis / 4), cellSize*Convert.ToInt16(xaxis - xaxis / 4), Convert.ToInt16(xaxis / 4), Convert.ToInt16(xaxis / 4));
            g.FillRectangle(ebrush, 0, 0, xaxis * yaxis, xaxis * yaxis);
           
           

            // Create rectangle.
            

            // Draw rectangle to screen.
           // e.Graphics.DrawRectangle(blackPen, rect);
            Pen p = new Pen(Color.Black);
            for (int i = 0; i < locals_num; ++i)
            {

                while (Locals[i] == null && i < locals_num)
                {
                    i = i + 1;

                }


                if (Locals[i] != null && Locals[i].type == 1 )
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
                if ( Minors[i] != null && Minors[i].type == 2)
                {
                    g.FillRectangle(cbrush, Minors[i].xpos * cellSize, Minors[i].ypos * cellSize, cellSize, cellSize);
                }
                else
                {
                    g.FillRectangle(bbrush, Minors[i].xpos * cellSize, Minors[i].ypos * cellSize, cellSize, cellSize);
                }
            }

            g.DrawRectangle(bluePen, rect);
            g.DrawRectangle(orangePen, rect1);
            g.DrawRectangle(aquqPen, rect2);
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
                kuar = 0;
                Matrix<double> map = execute();

                for (int j = 0; j < min_num; j++)
                {

                    Minors[j].s = 0;

                }
                for (int i = 0; i < locals_num; i++)
                {

                    Locals[i].s = 0;

                }

                double sum1 = 100000;
                double sum2 = 100000;

                while (sum1 + sum2 > 1)//xaxis*xaxis/10000)
                {


                    rate_check_for_all(map);




                    //queue = 0;

                    //for (int z = 2; z <= yaxis / 2; z++)//Square analysis counting
                    //{
                    //    queue = queue + 1;
                    //    for (int e = 0; e <= yaxis - z; e++)
                    //    {

                    //        for (int k = 0; k <= xaxis - z; k++)
                    //        {
                    //            for (int i = k; i < z + k; i++)
                    //            {
                    //                for (int j = e; j < z + e; j++)
                    //                {
                    //                    if (map[i, j] == 1)
                    //                    {
                    //                        count_green = count_green + 1;
                    //                    }
                    //                    else if (map[i, j] == 2)
                    //                    {
                    //                        count_red = count_red + 1;
                    //                    }
                    //                }
                    //            }

                    //            prob_dist2[0, queue] = z;
                    //            if (count_green / (count_green * 1.00 + count_red) == 0)
                    //            {
                    //                prob_dist2[kuar, queue] = prob_dist2[kuar, queue] + 1;
                    //            }
                    //            else if (count_green / (count_green * 1.00 + count_red) == 1)
                    //            {
                    //                prob_dist2[kuar, queue] = prob_dist2[kuar, queue] + 1;
                    //            }
                    //            count_red = 0;
                    //            count_green = 0;
                    //        }
                    //    }

                    //    if (prob_dist2[kuar, queue] == 0)
                    //    {

                    //        break;
                    //    }

                    //}
                    count_unhappy(map, kuar, a);
                    map = continue_2(map);
                    kuar = kuar + 1;
                    for (int i = 0; i < locals_num; i++)
                    {

                        if (Locals[i] != null)
                        {
                            if (((Locals[i].rate < lower_bound || Locals[i].rate > upper_bound) && Locals[i].type == 1) ||
                                ((Locals[i].rate < lower_bound2 || Locals[i].rate > upper_bound2) && Locals[i].type == 2))
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
                            if (((Minors[j].rate < lower_bound || Minors[j].rate > upper_bound) && Minors[j].type == 1) ||
                                 ((Minors[j].rate < lower_bound2 || Minors[j].rate > upper_bound2) && Minors[j].type == 2))
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

                   

                    number_loc = 0;
                    number_min = 0;
                    for (int i = 0; i < locals_num; i++)  // Count the number of agent types
                    {
                        if (Locals[i] != null && Locals[i].type == 1  )
                        {
                            number_loc = number_loc + 1;


                        }

                        else if (Locals[i] != null && Locals[i].type == 2)
                        {

                            number_min = number_min + 1;
                        }


                    }
                    for (int i = 0; i < min_num; i++)
                    {
                        if (Minors[i] != null && Minors[i].type == 1)
                        {
                            number_loc = number_loc + 1;


                        }

                        else if (Minors[i] != null && Minors[i].type == 2)
                        {

                            number_min = number_min + 1;
                        }


                    }
                    if (sum1 + sum2 < 1000)
                    {
                        //MessageBox.Show(Convert.ToString(number_loc), Convert.ToString(number_min));
                    }




                  // update_map();
                   
                    


                }

                //for (int i = 0; i < 10000; i++)

                //{
                //    rate_check_for_all(map);



                //    map = continue_2(map);


                //    //MessageBox.Show(Convert.ToString(z));


                //}

                
            
            



               
                                                                                                  
                
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
                loc_number[a] = number_loc * 1.00;
                mino_number[a] = number_min * 1.00;
                number_loc = 0;
                number_min = 0;
                
                agents.Clear();
                queue = 0;

                for (int z = 2; z <= yaxis / 2; z++)//Square analysis counting

                {



                    queue = queue + 1;
                    for (int e = 0; e <= yaxis - z; e++)
                    {

                        for (int k = 0; k <= xaxis - z; k++)
                        {
                            for (int i = k; i < z + k; i++)

                            {
                                for (int j = e; j < z + e; j++)
                                {

                                    if (map[i, j] == 1)
                                    {

                                        count_green = count_green + 1;

                                    }
                                    else if (map[i, j] == 2)
                                    {

                                        count_red = count_red + 1;

                                    }

                                }

                            }
                            //frac_count = frac_count + 1;

                            //fract[a, frac_count] = count_green / (count_green * 1.00 + count_red);

                            prob_dist[0, queue] = z;

                            if (count_green / (count_green * 1.00 + count_red) == 0)
                            {

                                prob_dist[1, queue] = prob_dist[1, queue] + 1;
                            }

                            else if (count_green / (count_green * 1.00 + count_red) > 0 &&
                               count_green / (count_green * 1.00 + count_red) < 0.1)

                            {
                                prob_dist[2, queue] = prob_dist[2, queue] + 1;
                            }
                            else if (count_green / (count_green * 1.00 + count_red) >= 0.1 &&
                               count_green / (count_green * 1.00 + count_red) < 0.2)
                            {

                                prob_dist[3, queue] = prob_dist[3, queue] + 1;

                            }

                            else if (count_green / (count_green * 1.00 + count_red) >= 0.2 &&
                               count_green / (count_green * 1.00 + count_red) < 0.3)
                            {

                                prob_dist[4, queue] = prob_dist[4, queue] + 1;

                            }

                            else if (count_green / (count_green * 1.00 + count_red) >= 0.3 &&
                               count_green / (count_green * 1.00 + count_red) < 0.4)
                            {

                                prob_dist[5, queue] = prob_dist[5, queue] + 1;

                            }
                            else if (count_green / (count_green * 1.00 + count_red) >= 0.4 &&
                               count_green / (count_green * 1.00 + count_red) < 0.5)
                            {

                                prob_dist[6, queue] = prob_dist[6, queue] + 1;

                            }
                            else if (count_green / (count_green * 1.00 + count_red) >= 0.5 &&
                               count_green / (count_green * 1.00 + count_red) < 0.6)
                            {

                                prob_dist[7, queue] = prob_dist[7, queue] + 1;

                            }
                            else if (count_green / (count_green * 1.00 + count_red) >= 0.6 &&
                               count_green / (count_green * 1.00 + count_red) < 0.7)
                            {

                                prob_dist[8, queue] = prob_dist[8, queue] + 1;

                            }
                            else if (count_green / (count_green * 1.00 + count_red) >= 0.7 &&
                               count_green / (count_green * 1.00 + count_red) < 0.8)
                            {

                                prob_dist[9, queue] = prob_dist[9, queue] + 1;

                            }
                            else if (count_green / (count_green * 1.00 + count_red) >= 0.8 &&
                               count_green / (count_green * 1.00 + count_red) < 0.9)
                            {

                                prob_dist[10, queue] = prob_dist[10, queue] + 1;

                            }
                            else if (count_green / (count_green * 1.00 + count_red) >= 0.9 &&
                               count_green / (count_green * 1.00 + count_red) < 1)
                            {

                                prob_dist[11, queue] = prob_dist[11, queue] + 1;

                            }
                            else if (count_green / (count_green * 1.00 + count_red) == 1)

                            {

                                prob_dist[12, queue] = prob_dist[12, queue] + 1;

                            }



                            count_red = 0;
                            count_green = 0;
                        }
                    }

                    if (prob_dist[12, queue] + prob_dist[1, queue] == 0)
                    {

                        break;
                    }

                }//end of square analysis counting

                update_map();

               


            }// end of simulations




            local_number.Text = Convert.ToString(loc_number.Sum() / sim_value);
            minor_number.Text = Convert.ToString(mino_number.Sum() / sim_value);




            //Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
            //Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
            //Worksheet ws = (Worksheet)xla.ActiveSheet;
            //Microsoft.Office.Interop.Excel.Range rng = ws.Cells.get_Resize(prob_dist1.GetLength(0));

            //Microsoft.Office.Interop.Excel.Application xlb = new Microsoft.Office.Interop.Excel.Application();//square analysis
            //Workbook wc = xlb.Workbooks.Add(XlSheetType.xlWorksheet);
            //Worksheet wt = (Worksheet)xlb.ActiveSheet;
            //Microsoft.Office.Interop.Excel.Range rngg = wt.Cells.get_Resize(prob_dist.GetLength(0), prob_dist.GetLength(1));


            //Microsoft.Office.Interop.Excel.Application xlc = new Microsoft.Office.Interop.Excel.Application();
            //Workbook wd = xlc.Workbooks.Add(XlSheetType.xlWorksheet);
            //Worksheet wf = (Worksheet)xlc.ActiveSheet;
            //Microsoft.Office.Interop.Excel.Range rngg1 = wf.Cells.get_Resize(unhappy_array.GetLength(0), unhappy_array.GetLength(1));

            //Microsoft.Office.Interop.Excel.Application xld = new Microsoft.Office.Interop.Excel.Application();//count mono by time
            //Workbook we = xld.Workbooks.Add(XlSheetType.xlWorksheet);
            //Worksheet wg = (Worksheet)xld.ActiveSheet;
            //Microsoft.Office.Interop.Excel.Range rngg2 = wg.Cells.get_Resize(prob_dist2.GetLength(0), prob_dist2.GetLength(1));




            //rngg1.Value2 = unhappy_array;
            //rngg2.Value2 = prob_dist2;
            //rngg.Value2 = prob_dist;


            //xlc.Visible = true;
            //xld.Visible = true;

            //xlb.Visible = true;
            //xlb.WindowState = XlWindowState.xlMaximized;
            //xlc.WindowState = XlWindowState.xlMaximized;
            //xld.WindowState = XlWindowState.xlMaximized;
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