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
using MathNet.Numerics.Random;
using MathNet.Numerics.Distributions;
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
        public int cellSize = 5;
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
        int red;
        int green;
        //End of universal variables
        public static List<Agents> unhappy_agents_list = new List<Agents>();
        
        static Agents[] Locals;

        static Agents[] Minors;

        


        public int sim_value = 0;
        public int dix;
        private int lxpos;
        private int lypos;
        private int mxpos;
        private int mypos;

        public Form2(string population, string minority, string x_axis, string y_axis, string lowerbound, string lowerbound2,
            string eco, string upperbound, string utilitycheck, string sim, string geo, string no_freecells,
            string algo, string upperbound2, string wsize)

        {   // Get the values from previous windows form

            InitializeComponent();

            // Convert text variables
           
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
            unhappy_array = new int[1000, 100];
            w_size1 = Convert.ToInt32(wsize);
            locals_num = Convert.ToInt16(population);
            min_num = Convert.ToInt16(minority);
        }

       


        public void Draw_World(int x_axis, int y_axis) //Draw the initial empty world

        {
            Graphics g;
          
            Pen p = new Pen(Color.Black);
            Brush bBrush = (Brush)Brushes.Gray;

            g = this.CreateGraphics();                  
           
            exp_het = 2 * (locals_num / (locals_num + min_num * 1.00)) * (min_num / (locals_num + min_num * 1.00));
        }


        public Agents[,] execute() // Make the initial implanting and create agents
                                   // and draw the initial world

        {

            Pen bluePen = new Pen(Color.Blue, 2);
            Pen orangePen = new Pen(Color.Orange, 2);
            Pen aquqPen = new Pen(Color.Aqua, 2);

            Matrix<double> map = Matrix<double>.Build.Dense(xaxis, yaxis, 0);
            Agents[,] node_map = new Agents[xaxis,yaxis];
            double[] samples = SystemRandomSource.Doubles(1000000, 100);

            // Check for world type///////////////////////////////////////////

            Random rnd3 = new Random();
            Random rndd = new Random();
            Random rnd = new Random();
            int numero = 1;
            int x, y;

            /////// Attach agents' types ///////////////////////

            for (int i = 0; i < locals_num; i++)
            {
                dix = rndd.Next(0, 2);
                if (dix == 1)
                {
                    green++;
                    Locals[i] = new Agents()
                    {
                        type = 1

                    };

                }
                else
                {
                    red++;
                    Locals[i] = new Agents()
                    {
                        type = 2
                    };
                }                          
            }
            for (int i = 0; i < min_num; i++)
            {

                dix = rndd.Next(0, 2);
                if (dix == 1)
                {
                    green++;
                    Minors[i] = new Agents()
                    {
                        type = 1,

                    };
                }
                else
                {
                    red++;
                    Minors[i] = new Agents()
                    {
                        type = 2,

                    };
                }                
            }

            /////// End of agents' types ///////////////////////

            /////// Attach agents' positions ///////////////////////
            for (int i = 0; i < locals_num; ++i)
            {              
                x = rndd.Next(0, xaxis);
                y = rndd.Next(0, yaxis);

                while (map[x, y] == 1 || map[x, y] == 2 || map[x, y] == 3)
                {
                    x = rndd.Next(0, xaxis);
                    y = rndd.Next(0, yaxis);

                }

                Locals[i].xpos = x;
                Locals[i].ypos = y;

                map[x, y] = Locals[i].type;
                node_map[x,y] = Locals[i];
            }
            for (int i = 0; i < min_num; ++i)
            {

                x = rndd.Next(0, xaxis);
                y = rndd.Next(0, yaxis);

                while (map[x, y] == 1 || map[x, y] == 2 || map[x, y] == 3)
                {
                    x = rndd.Next(0, xaxis);
                    y = rndd.Next(0, yaxis);

                }

                Minors[i].xpos = x;
                Minors[i].ypos = y;

                map[x, y] = Minors[i].type;
                node_map[x, y] = Minors[i];
            }

            numero = 0;
      
            /////// End of attachment of agents' positions ///////////////////////

            //////////////////////////////////////////////////////////////////
            Brush bbrush = (Brush)Brushes.Green;
            Brush cbrush = (Brush)Brushes.Red;
            Brush dbrush = (Brush)Brushes.Black;
            Pen p = new Pen(Color.Black);

            Graphics g;
            g = this.CreateGraphics();
            ///// Draw the agents in the map/////////////////////////////////
            for (int i = 0; i < locals_num; ++i)
            {
                g.FillRectangle(bbrush, Locals[i].xpos * cellSize, Locals[i].ypos * cellSize, cellSize, cellSize);
            }

            for (int i = 0; i < min_num; ++i)
            {
                g.FillRectangle(cbrush, Minors[i].xpos * cellSize, Minors[i].ypos * cellSize, cellSize, cellSize);
            }
            //// End of Drawing ////////////////////////////////////////////////////////
           

            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, Convert.ToInt16(xaxis / 3), Convert.ToInt16(xaxis / 3));
            System.Drawing.Rectangle rect1 = new System.Drawing.Rectangle(
                Convert.ToInt16(xaxis - xaxis / 4), xaxis - xaxis / 4, Convert.ToInt16(xaxis / 2), Convert.ToInt16(xaxis / 2));
            System.Drawing.Rectangle rect2 = new System.Drawing.Rectangle(cellSize *
                Convert.ToInt16(xaxis - xaxis / 4), cellSize * Convert.ToInt16(xaxis - xaxis / 4), Convert.ToInt16(xaxis / 4), Convert.ToInt16(xaxis / 4));
           
            g.DrawRectangle(bluePen, rect);
            g.DrawRectangle(orangePen, rect1);
            g.DrawRectangle(aquqPen, rect2);
          
            local_number.Text = Convert.ToString(green / sim_value);
            minor_number.Text = Convert.ToString(red / sim_value);

            return node_map;

        }   // end of execute function

        public int[,] count_unhappy(Agents[,] map, int kuar, int a) /////////Count unhappy agents for excel output

        {
            rate_check_for_all(map);
            unhappyloc = 0;
            unhappymin = 0;
            for (int r = 0; r < locals_num; r++)
            {
                if ((  (Locals[r].rate < Locals[r].lower_bound || Locals[r].rate > upper_bound) &&   Locals[r].type==1) ||
                   (Locals[r].rate < Locals[r].lower_bound || Locals[r].rate > upper_bound2) && Locals[r].type == 2)
                    
                {
                    unhappyloc++;
                }
            }
            for (int r = 0; r < min_num; r++)
            {
                if (((Minors[r].rate < Minors[r].lower_bound || Minors[r].rate > upper_bound) && Minors[r].type == 1) ||
                   ((Minors[r].rate < Minors[r].lower_bound || Minors[r].rate > upper_bound2) && Minors[r].type == 2))
                {
                    unhappymin++;
                }
            }
            unhappy_array[kuar, 2 * a] = unhappyloc;
            unhappy_array[kuar, 2 * a + 1] = unhappymin;

            return unhappy_array;

        }//end of count unhappy agent function


        public Agents[,] AdjacentElements(Agents[,] node_map, int row, int column)
        {
           ////// Collect neighbor cells' positions with periodic boundary conditions
        
            Agents[,] w = new Agents[500, 3];
            int rows = yaxis;
            int columns = xaxis;

            int r = 0;
             
            for (int j = row - w_size1; j <= row + w_size1; j++)
            {
                for (int i = column - w_size1; i <= column + w_size1; i++)
                {
                    if (!(j == row && i == column))
                    {
                        if (j >= 0 && i >= 0)
                        {
                            w[r, 0] = node_map[j % xaxis, i % xaxis];
                            r = r + 1;
                        }
                        else if (j >= 0 && i < 0)
                        {
                            w[r, 0] = node_map[j % xaxis, i + xaxis];
                            r = r + 1;

                        }
                        else if (j < 0 && i < 0)
                        {
                            w[r, 0] = node_map[j + xaxis, i + xaxis];
                            r = r + 1;

                        }
                        else if (j < 0 && i >= 0)
                        {
                            w[r, 0] = node_map[j + xaxis, i % xaxis];
                            r = r + 1;

                        }
                    }

                }

            }

           
            return w;
        }// end of adjacent elements function

        public int[,] rate_check(int x_pos, int y_pos, Agents[,] node_map)//Count neighbors' types
        {

            Agents[,] map2 = node_map;
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

            for (int i = 0; i < results.GetLength(0); i++)
            {
                if (results[i, 0] != null)
                {
                    if (results[i, 0].type == 1)
                    {
                        count1 = count1 + 1;
                    }
                    else if (results[i, 0].type == 2)
                    {
                        count2 = count2 + 1;
                    }
                    else if (results[i, 0].type == 0)
                    {
                        count3 = count3 + 1;
                    }
                }
                else
                {
                    break;
                }
            }

            //for (int i = 0; i < results.GetLength(1); i++)
            //{

            //    if (results[i, 1].type == 1)
            //    {
            //        count21 = count21 + 1;
            //    }
            //    else if (results[i, 1].type == 2)
            //    {
            //        count22 = count22 + 1;
            //    }

            //    else if (results[i, 1].type == 0)
            //    {
            //        count20 = count20 + 1;
            //    }
            //}

            //for (int i = 0; i < results.GetLength(1); i++)
            //{
            //    if (results[i, 2].type == 1)
            //    {
            //        count31 = count31 + 1;

            //    }
            //    else if (results[i, 2].type == 2)
            //    {
            //        count32 = count32 + 1;

            //    }

            //    else if (results[i, 2].type == 0)
            //    {
            //        count30 = count30 + 1;
            //    }

            //}
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

        public double rate_check_for_one(int lxpos, int lypos, Agents[,] map)
            //Calculate the neigbor type proportions
        {
            int[,] a;
           
            double rate1=0;
          
            double c = 1.00;
            a = rate_check(lxpos, lypos, map);

            if (map[lxpos, lypos].type == 1)
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

            else if (map[lxpos, lypos].type == 2)
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
                                         
            return rate1;
        }

        public void rate_check_for_all(Agents[,] node_map)//Compute neigbors rate for all agents and statistics
            // Main function to calculate all agents' happiness in the network, sufficient to use alone.
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
                t = rate_check(Locals[i].xpos, Locals[i].ypos, node_map);
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
                t = rate_check(Minors[i].xpos, Minors[i].ypos, node_map);
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

            
        }// end of rate_check_for_all func.

        public Agents[,] continue_to(Agents[,] map)
            //Reimplanting, basically the "move function" after the happiness check for all agents..
            ///..
        {
            Random rnd1 = new Random();  /// pseudo random generator variables
            Random rnd2 = new Random();
            Random rnd3 = new Random();
           
            int local_index;
            int minor_index;

            //local_index = rnd2.Next(0, locals_num);
            //minor_index = rnd2.Next(0, min_num);



            if (algo_value == 0)/// If it is Kawasaki dynamic
            {



                for (int i = 0; i < xaxis * yaxis; i++)
                {

                    local_index = rnd2.Next(0, locals_num);
                    minor_index = rnd2.Next(0, min_num);
                    if ((Locals[local_index].rate < lower_bound || Locals[local_index].rate > upper_bound) &&
                            (Minors[minor_index].rate < lower_bound2 || Minors[minor_index].rate > upper_bound2))
                    {


                        lxpos = Locals[local_index].xpos;
                        lypos = Locals[local_index].ypos;
                        mxpos = Minors[minor_index].xpos;
                        mypos = Minors[minor_index].ypos;                        
                          

                        }
                 }

                  

            }


            else   //If the algorithm is Glauber dynamic
            {
                                           
            for (int j = 0; j < locals_num; j++)
            {
                if (((Locals[j].rate < lower_bound || Locals[j].rate > upper_bound) && Locals[j].type == 1) ||
                  ((Locals[j].rate < lower_bound2 || Locals[j].rate > upper_bound2) && Locals[j].type == 2))
                {
                    unhappy_agents_list.Add(Locals[j]);// Collect unhappy list

                }

            }
            for (int j = 0; j < min_num; j++)
            {
                if (((Minors[j].rate < lower_bound || Minors[j].rate > upper_bound) && Minors[j].type == 1) ||
                  ((Minors[j].rate < lower_bound2 || Minors[j].rate > upper_bound2) && Minors[j].type == 2))
                {
                    unhappy_agents_list.Add(Minors[j]);// Collect unhappy list
                }
            }

               
                for (int index = 0; index < unhappy_agents_list.Count; index++)// Respect to unhappy agent list take random element from the list 
                    // convert it than take next random element from the list but only if the next agent is still unhappy if not choose next random


            {

                dice = rnd3.Next(0, unhappy_agents_list.Count);// Roll a dice for random pick

                if ((unhappy_agents_list[dice] != null&& unhappy_agents_list[dice].type==1 && (rate_check_for_one(unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos,map)<lower_bound
                    || rate_check_for_one(unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos,map) > upper_bound)||
                        (unhappy_agents_list[dice] != null && unhappy_agents_list[dice].type == 2 && (rate_check_for_one(unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos, map) < lower_bound2
                    || rate_check_for_one(unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos, map) > upper_bound2))))
                {
                    if (unhappy_agents_list[dice].type == 1 && (unhappy_agents_list[dice].rate < lower_bound || unhappy_agents_list[dice].rate > upper_bound))
                    {
                        map[unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos].type = 2;
                        unhappy_agents_list[dice].type = 2;
                        unhappy_agents_list.RemoveAt(dice);




                    }

                    else if (unhappy_agents_list[dice].type == 2 && (unhappy_agents_list[dice].rate < lower_bound2 || unhappy_agents_list[dice].rate > upper_bound2))
                    {
                        map[unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos].type = 1;
                        unhappy_agents_list[dice].type = 1;
                        unhappy_agents_list.RemoveAt(dice);

                    }

                }
                else
                {
                        unhappy_agents_list.RemoveAt(dice);


                }
            }
         
            unhappy_agents_list.Clear();


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

            }
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
            //System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0,0,Convert.ToInt16(xaxis/3), Convert.ToInt16(xaxis / 3)) ;
            //System.Drawing.Rectangle rect1 = new System.Drawing.Rectangle(
            //    Convert.ToInt16(xaxis-xaxis/4), xaxis-xaxis/4, Convert.ToInt16(xaxis / 2), Convert.ToInt16(xaxis / 2));
            //System.Drawing.Rectangle rect2 = new System.Drawing.Rectangle(cellSize * 
            //    Convert.ToInt16(xaxis - xaxis / 4), cellSize*Convert.ToInt16(xaxis - xaxis / 4), Convert.ToInt16(xaxis / 4), Convert.ToInt16(xaxis / 4));
            //g.FillRectangle(ebrush, 0, 0, xaxis * yaxis, xaxis * yaxis);
           
           

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

            //g.DrawRectangle(bluePen, rect);
            //g.DrawRectangle(orangePen, rect1);
            //g.DrawRectangle(aquqPen, rect2);
        }

       

        public Agents[,] continue_2(Agents[,] map)
        {

            Agents[,] map1 = map;
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


            int kuar = 0;
        
            for (int a = 0; a <= sim_value - 1; a++)
            {
                Draw_World(xaxis, yaxis);
                frac_count = 0;
                kuar = 0;
                Agents[,] map = execute();
                count_unhappy(map, kuar, a);
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
                kuar = 1;
                while (sum1 + sum2 > 0)
                {

                    count_unhappy(map, kuar, a);
                    rate_check_for_all(map);




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
                                        if (map[i, j].type == 1)
                                        {
                                            count_green = count_green + 1;
                                        }
                                        else if (map[i, j].type == 2)
                                        {
                                            count_red = count_red + 1;
                                        }
                                    }
                                }

                                prob_dist2[0, queue] = z;
                                if (count_green / (count_green * 1.00 + count_red) == 0)
                                {
                                    prob_dist2[kuar, queue] = prob_dist2[kuar, queue] + 1;
                                }
                                else if (count_green / (count_green * 1.00 + count_red) == 1)
                                {
                                    prob_dist2[kuar, queue] = prob_dist2[kuar, queue] + 1;
                                }
                                count_red = 0;
                                count_green = 0;
                            }
                        }

                        if (prob_dist2[kuar, queue] == 0)
                        {

                            break;
                        }

                    }

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


                    number_loc = 0;
                    number_min = 0;
                    for (int i = 0; i < locals_num; i++)  // Count the number of agent types
                    {
                        if (Locals[i] != null && Locals[i].type == 1)
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




                    update_map();




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
                        if (map[i, j].type == 0)
                        {

                            ndf[count_empties, 0] = ndf_matrice[2, 0] / ((ndf_matrice[0, 0] + ndf_matrice[1, 0] + ndf_matrice[2, 0]) * 1.00);
                            ndf[count_empties, 1] = ndf_matrice[2, 1] / ((ndf_matrice[0, 1] + ndf_matrice[1, 1] + ndf_matrice[2, 1]) * 1.00);
                            ndf[count_empties, 2] = ndf_matrice[2, 2] / ((ndf_matrice[0, 2] + ndf_matrice[1, 2] + ndf_matrice[2, 2]) * 1.00);
                            count_empties = count_empties + 1;
                        }
                        else if (map[i, j].type == 1)
                        {
                            ndf2[count_locals, 0] = ndf_matrice[0, 0] / ((ndf_matrice[0, 0] + ndf_matrice[1, 0] + ndf_matrice[2, 0]) * 1.00);
                            ndf2[count_locals, 1] = ndf_matrice[0, 1] / ((ndf_matrice[0, 1] + ndf_matrice[1, 1] + ndf_matrice[2, 1]) * 1.00);
                            ndf2[count_locals, 2] = ndf_matrice[0, 2] / ((ndf_matrice[0, 2] + ndf_matrice[1, 2] + ndf_matrice[2, 2]) * 1.00);
                            count_locals = count_locals + 1;


                        }
                        else if (map[i, j].type == 2)
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
                loc_number[a] = number_loc * 1.00;
                mino_number[a] = number_min * 1.00;
                number_loc = 0;
                number_min = 0;
                
                unhappy_agents_list.Clear();
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

                                    if (map[i, j].type == 1)
                                    {

                                        count_green = count_green + 1;

                                    }
                                    else if (map[i, j].type == 2)
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

                    if (z==10)//prob_dist[12, queue] + prob_dist[1, queue] == 0)
                    {

                        break;
                    }

                }//end of square analysis counting

                update_map();
                

               


            }// end of simulations

            local_number.Text = Convert.ToString(loc_number.Sum()/sim_value);
            minor_number.Text = Convert.ToString(mino_number.Sum() / sim_value);

            ave_sim_neigh.Text = Convert.ToString(Math.Round(B / sim_value, 3));
            ave_mix.Text = Convert.ToString(Math.Round(C / sim_value, 3));
            ave_FSI.Text = Convert.ToString(Math.Round(D / sim_value, 3));
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
            var_asn.Text = Convert.ToString(Math.Round(variance_ASN, 5));
            var_fsi.Text = Convert.ToString(Math.Round(variance_FSI, 5));
            var_mix.Text = Convert.ToString(Math.Round(variance_MIX, 5));
            local_number.Text = Convert.ToString(loc_number.Sum() / sim_value);
            minor_number.Text = Convert.ToString(mino_number.Sum() / sim_value);
            min_var.Text = Convert.ToString(Math.Round(mino_number.StandardDeviation(), 3));
            sep_var.Text = Convert.ToString(Math.Round(SEPAR.StandardDeviation(), 3));
            unhloc.Text = Convert.ToString(unhappyloc / sim_value);
            unhmin.Text = Convert.ToString(unhappymin / sim_value);






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


            ////xlc.Visible = true;
            ////xld.Visible = true;

            ////xlb.Visible = true;
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

        public double lower_bound { get; set; }
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