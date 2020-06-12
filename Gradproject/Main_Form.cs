///****** Created by Utku Arık*****2016********
///****** Agent based simulation on regular lattice*****
///****** Includes Glauber and Kawasaki dynamics and more*****
///****** User interface is included*****************


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

namespace Gradproject
{


    public partial class Form2 : Form
    {


        //*************** Universal variables******************
        int locals_num;
        int min_num;
        int stub_num;
        public int xaxis;
        public int yaxis;
        public int x;
        public int y;
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
        public double rate_sum;
        public double rate_sum2;
        public double[] AVE_VALUE;
        public double[] FSI_VALUE;
        public double energy_sum;
        public double[] RATE;
        public double[] SEPAR;
        public double z;
        public int index_loc = 0;
        public int index_min = 0;
        public int final_number_loc = 0;
        public int final_number_min = 0;
        public double[] loc_number;
        public double[] mino_number;
        public double[,] rate_histogram;
        public double[,] rate_histogram2;
        public double[,] rate_histogram3;
        public double[,] rate_histogram4;
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
        public double[,] energy_arr;
        public double temperature;
        public double boltzmann;
        public int queue;
        public int queue1;
        public int unhappyloc;
        public int unhappymin;
        public int[,] unhappy_array;
        public int w_size1;
        public int simultaneous;
        public int periodic_boundary;
        int red;
        int green;
        public int ground_state = 0;
        Agents[,] map;
        public int[,] map3;
        public static List<Agents> unhappy_agents_list = new List<Agents>();
        public static List<Agents> agent_list = new List<Agents>();
        public static List<int[,]> freecell_list = new List<int[,]>();
        public static List<Agents> radicals_lst = new List<Agents>();
        static Agents[] Locals;
        static Agents[] Minors;
        static Agents[] Stubborn;
        public static Agents[,] node_map;
        public int sim_value = 0;
        public int dix;
        private int lxpos;
        private int lypos;
        private int mxpos;
        private int mypos;
        public int excel_wnt;
        public int periodic;
        public int ind = 0;
        public int effect;
        public int num_ini_adopters;
        public double hamiltonian;
        public double hamiltonian1;
        public double hamiltonian2;
        public int percent_stubborn;
        public List<Agents> pots_lst = new List<Agents>();
        
        //*******************End of universal variables**************************
        public Form2(string population, string minority, string x_axis, string y_axis, string lowerbound, string lowerbound2,
            string eco, string upperbound, string utilitycheck, string sim, string geo, string no_freecells,
            string algo, string upperbound2, string wsize, string async, string cellsize, int excl, int periodica, string effect_1, string ini_adp, string stub_per)

        {   // Get the values from previous windows form

            InitializeComponent();
            excel_wnt = excl;
            // Convert text variables
            num_ini_adopters = Convert.ToInt16(ini_adp);
            cellSize = Convert.ToInt16(cellsize);
            periodic_boundary = periodica;
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
            Stubborn = new Agents[xaxis * yaxis];
            // fract = new double[50,1500000];
            prob_dist = new double[13, 10000];
            prob_dist1 = new double[13, 10000];
            prob_dist2 = new double[10000, 200];
            unhappy_array = new int[10000, 200];
            effect = Convert.ToInt16(effect_1);
            energy_arr = new double[5000,100];
            w_size1 = Convert.ToInt32(wsize);
            rate_histogram = new double[sim_value, Convert.ToInt16((Math.Pow((2 * w_size1 + 1), 2)))];
            rate_histogram2 = new double[10000, Convert.ToInt16((Math.Pow((2 * w_size1 + 1), 2)))];
            rate_histogram3 = new double[sim_value, Convert.ToInt16((Math.Pow((2 * w_size1 + 1), 2)))];
            rate_histogram4 = new double[sim_value, Convert.ToInt16((Math.Pow((2 * w_size1 + 1), 2)))];
            locals_num = Convert.ToInt16(population);
            min_num = Convert.ToInt16(minority);
            percent_stubborn = Convert.ToInt16(stub_per);
            simultaneous = Convert.ToInt32(async);
            node_map = new Agents[xaxis, yaxis];
        }


        public static Tuple<double, double> Conf(double[] samples, double interval)
        {
            double theta = (interval + 1.0) / 2;
            double T = FindRoots.OfFunction(x => StudentT.CDF(0, 1, samples.Length - 1, x) - theta, -800, 800);

            double mean = samples.Mean();
            double sd = samples.StandardDeviation();
            double t = T * (sd / Math.Sqrt(samples.Length));
            return Tuple.Create(mean - t, mean + t);
        }

        public void window_keydown(object sender, System.Windows.Input.KeyboardEventArgs e)
        {
            if (Form.ModifierKeys == Keys.Shift)
            {
                MessageBox.Show("hello");
                algo_value = 1;

            }


        }

        ///*********Energy calculations************
        public void Calculate_energy(int tr, int sim_value)

        { 
            for (int i = 0; i < locals_num; i++)
            {
                Locals[i].energy = -(Locals[i].rate * (Math.Pow(2 * w_size1 + 1, 2) - 1) - ((Math.Pow(2 * w_size1 + 1, 2) - 1) -
                    Locals[i].rate * (Math.Pow(2 * w_size1 + 1, 2) - 1)));
                energy_sum = energy_sum + Locals[i].energy;
            }

            for (int i = 0; i < min_num; i++)
            {
                Minors[i].energy = -(Minors[i].rate * (Math.Pow(2 * w_size1 + 1, 2) - 1) - ((Math.Pow(2 * w_size1 + 1, 2) - 1) -
                    Minors[i].rate * (Math.Pow(2 * w_size1 + 1, 2) - 1)));
                energy_sum = energy_sum + Minors[i].energy;
            }

            energy_arr[tr,sim_value] = energy_sum ;
            energy_sum = 0;    
        }

        public double Calculate_hamilton( int sim_value)

        {
            for (int i = 0; i < locals_num; i++)
            {
                Locals[i].energy = -(Locals[i].rate * (Math.Pow(2 * w_size1 + 1, 2) - 1) - ((Math.Pow(2 * w_size1 + 1, 2) - 1) -
                    Locals[i].rate * (Math.Pow(2 * w_size1 + 1, 2) - 1)));
                energy_sum = energy_sum + Locals[i].energy;
            }

            for (int i = 0; i < min_num; i++)
            {
                Minors[i].energy = -(Minors[i].rate * (Math.Pow(2 * w_size1 + 1, 2) - 1) - ((Math.Pow(2 * w_size1 + 1, 2) - 1) -
                    Minors[i].rate * (Math.Pow(2 * w_size1 + 1, 2) - 1)));
                energy_sum = energy_sum + Minors[i].energy;
            }

           
            hamiltonian1 = -energy_sum;
            energy_sum = 0;
            return hamiltonian1;

        }
        ///// *********end of energy calculations********************

         //Draw the initial empty world
        public void Draw_World(int x_axis, int y_axis)

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

            //Matrix<double> map = Matrix<double>.Build.Dense(xaxis, yaxis, 0);
            int[,] map = new int[xaxis, yaxis];
            map3 = new int[xaxis, yaxis];

            double[] samples = SystemRandomSource.Doubles(1000000, 100);


            // Check for world type///////////////////////////////////////////
            Random rnd3 = new Random();
            Random rndd = new Random();
            Random rnd = new Random();
            int numero = 1;
            int x, y;

            /////// Attach agents' types ///////////////////////
            
            if (algo_value == 9)
            {
                stub_num = Convert.ToInt16(Math.Round(xaxis * 1.00 * yaxis * percent_stubborn / 100, 0));
                locals_num = (xaxis * yaxis - stub_num) / 2;
                min_num = (xaxis * yaxis - stub_num) / 2;

                for (int i = 0; i < locals_num; i++)
                {
                    dix = rndd.Next(0, 2);
                    if (dix == 1)
                    {
                        green++;
                        Locals[i] = new Agents()
                        {
                            type = 1,
                            check = 0
                        };
                    }
                    else
                    {
                        red++;
                        Locals[i] = new Agents()
                        {
                            type = 1,
                            check = 0
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
                            check = 0

                        };
                    }
                    else
                    {
                        red++;
                        Minors[i] = new Agents()
                        {
                            type = 1,
                            check = 0
                        };
                    }
                }




                /////********Initiate the Stubborn agents*******************************************
                for (int i = 0; i < stub_num; i++)
                {
                        Stubborn[i] = new Agents()
                        {

                        type = 4,
                        check = -1
                        };

                }
            }

            else
            {
                if (locals_num == min_num)
                {
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

                }
                else
                {

                    for (int i = 0; i < locals_num; i++)
                    {

                        green++;
                        Locals[i] = new Agents()
                        {
                            type = 1

                        };

                    }


                    for (int i = 0; i < min_num; i++)
                    {

                        red++;
                        Minors[i] = new Agents()
                        {
                            type = 2,

                        };

                    }
                }
            }
            /////// End of agents' types ///////////////////////

            /////// Attach agents' positions ///////////////////////
            for (int i = 0; i < xaxis; i++)
                for (int j = 0; j < yaxis; j++)
                {
                    {

                        map[i, j] = 3;


                    }
                }
            ind = 0;

            if (algo_value == 6)

            {


                for (int i = 0; i < locals_num; i++)
                {
                    green++;
                    Locals[i] = new Agents()
                    {
                        type = 1
                    };
                }
                for (int i = 0; i < min_num; i++)
                {
                    red++;
                    Minors[i] = new Agents()
                    {
                        type = 2,
                    };
                }

                for (int i = 0; i < xaxis; i++)
                {
                    for (int j = 0; j < yaxis / 2; j++)

                    {


                        Locals[ind].type = 1;
                        map[i, 2 * j + 1] = Locals[ind].type;
                        Locals[ind].xpos = i;
                        Locals[ind].ypos = 2 * j + 1;
                        node_map[i, 2 * j + 1] = Locals[ind];
                        ind = ind + 1;
                    }
                }

                ind = 0;
                for (int i = 0; i < xaxis; i++)
                {
                    for (int j = 0; j < yaxis / 2; j++)

                    {

                        Minors[ind].type = 2;
                        map[i, 2 * j] = Minors[ind].type;
                        Minors[ind].xpos = i;
                        Minors[ind].ypos = 2 * j;
                        node_map[i, 2 * j] = Minors[ind];
                        ind = ind + 1;
                    }
                }

                Minors[2200].type = 1;
            }

            else if (algo_value == 7 || algo_value ==8)
            {
                ind = 0;
                for (int i = 0; i < locals_num; i++)
                {
                    green++;
                    Locals[i] = new Agents()
                    {
                        type = 1
                    };
                }
                for (int i = 0; i < min_num; i++)
                {
                    red++;
                    Minors[i] = new Agents()
                    {
                        type = 2,
                    };
                }



                for (int j = 0; j < yaxis; j++)
                {
                    for (int i = 0; i < xaxis; i+=2)
                    {
                        if (j % 2 == 0)
                        {
                            Locals[ind].xpos = i;
                            Locals[ind].ypos = j;
                            Minors[ind].xpos = i + 1;
                            Minors[ind].ypos = j;
                            map[i, j] = Locals[ind].type;
                            node_map[i, j] = Locals[ind];
                            map[i + 1, j] = Minors[ind].type;
                            node_map[i + 1, j] = Minors[ind];
                        }

                        else
                        {
                            Locals[ind].xpos = i + 1;
                            Locals[ind].ypos = j;
                            Minors[ind].xpos = i;
                            Minors[ind].ypos = j;
                            map[i, j] = Minors[ind].type;
                            node_map[i, j] = Minors[ind];
                            map[i + 1, j] = Locals[ind].type;
                            node_map[i + 1, j] = Locals[ind];

                        }
                        ind = ind + 1;
                    }
                }

                Minors[2200].type = 1;
            }
            else
            {
                for (int i = 0; i < locals_num; ++i)
                {
                    x = rndd.Next(0, xaxis);
                    y = rndd.Next(0, yaxis);

                    while (map[x, y] == 1 || map[x, y] == 2 || map[x, y] == 4)
                    {
                        x = rndd.Next(0, xaxis);
                        y = rndd.Next(0, yaxis);

                    }

                    Locals[i].xpos = x;
                    Locals[i].ypos = y;

                    map[x, y] = Locals[i].type;
                    node_map[x, y] = Locals[i];
                }
                for (int i = 0; i < min_num; ++i)
                {

                    x = rndd.Next(0, xaxis);
                    y = rndd.Next(0, yaxis);

                    while (map[x, y] == 1 || map[x, y] == 2 || map[x, y] == 4)
                    {
                        x = rndd.Next(0, xaxis);
                        y = rndd.Next(0, yaxis);
                    }

                    Minors[i].xpos = x;
                    Minors[i].ypos = y;

                    map[x, y] = Minors[i].type;
                    node_map[x, y] = Minors[i];
                }

                for( int i = 0; i < stub_num; i ++)
                {
                    x = rndd.Next(0, xaxis);
                    y = rndd.Next(0, yaxis);

                    while (map[x, y] == 1 || map[x, y] == 2 || map[x, y] == 4)
                    {
                        x = rndd.Next(0, xaxis);
                        y = rndd.Next(0, yaxis);
                    }

                    Stubborn[i].xpos = x;
                    Stubborn[i].ypos = y;

                    map[x, y] = Stubborn[i].type;
                    node_map[x, y] = Stubborn[i];

                }

                var w = AdjacentElements2(node_map, Minors[1000].xpos, Minors[1000].ypos,6);

                for (int j = 0; j < w.Length; j++)
                {
                    if (w[j].type ==1 )
                    {
                        radicals_lst.Add(w[j]);
                        w[j].type = 2;
                        w[j].stub = 1;
                    }


                }
            }



            numero = 0;

            /////// End of attachment of agents' positions ///////////////////////

            //////////////////////////////////////////////////////////////////
            Brush gbrush = (Brush)Brushes.Green;
            Brush rbrush = (Brush)Brushes.Red;
            Brush bbrush = (Brush)Brushes.Black;
            Brush ybrush = (Brush)Brushes.Yellow;
            Pen p = new Pen(Color.Black);

            Graphics g;
            g = this.CreateGraphics();
            ///// Draw the agents in the map/////////////////////////////////
            for (int i = 0; i < locals_num; ++i)
            {
                g.FillRectangle(gbrush, Locals[i].xpos * cellSize, Locals[i].ypos * cellSize, cellSize, cellSize);
            }

            for (int i = 0; i < min_num; ++i)
            {
                g.FillRectangle(rbrush, Minors[i].xpos * cellSize, Minors[i].ypos * cellSize, cellSize, cellSize);
            }

            for (int i = 0; i < stub_num; ++i)
            {
                g.FillRectangle(bbrush, Stubborn[i].xpos * cellSize, Stubborn[i].ypos * cellSize, cellSize, cellSize);
            }
            //// End of Drawing ////////////////////////////////////////////////////////


            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, Convert.ToInt16(xaxis / 3), Convert.ToInt16(xaxis / 3));
            System.Drawing.Rectangle rect1 = new System.Drawing.Rectangle(
                Convert.ToInt16(xaxis - xaxis / 4), xaxis - xaxis / 4, Convert.ToInt16(xaxis / 2), Convert.ToInt16(xaxis / 2));
            System.Drawing.Rectangle rect2 = new System.Drawing.Rectangle(cellSize *
                Convert.ToInt16(xaxis - xaxis / 4), cellSize * Convert.ToInt16(xaxis - xaxis / 4), Convert.ToInt16(xaxis / 4), Convert.ToInt16(xaxis / 4));

            //g.DrawRectangle(bluePen, rect);
            //g.DrawRectangle(orangePen, rect1);
            //g.DrawRectangle(aquqPen, rect2);

            local_number.Text = Convert.ToString(green / sim_value);
            minor_number.Text = Convert.ToString(red / sim_value);

            return node_map;

        }   // end of execute function


        public List<Agents> find_potentials(Agents[,] node_map)
        {
            int sum = 0;
            int sum1= 0;      
            for( int i = 0; i < min_num; i++)
            {
              if(Minors[i].type == 2)
                {
                    pots_lst.Add(Minors[i]);
                }
            }
           
            while(sum != (xaxis * yaxis - stub_num))
            {              
                sum1 = sum;
                sum = 0;
                for ( int i =0; i < pots_lst.Count; i++)
                {
                    var w = AdjacentElements(node_map, pots_lst[i].xpos, pots_lst[i].ypos);
                   
                    
                    for( int j = 0; j < w.Length; j ++)
                    {
                        if( w[j].type != 4 && w[j].check != 1 && w[j].check != -1)
                        {
                            pots_lst.Add(w[j]);
                            w[j].check = 1;
                        }               
                    }
                }
                for (int i = 0; i < locals_num; i++)
                {
                    sum = sum + Locals[i].check;

                }
                for (int i = 0; i < min_num; i++)
                {
                    sum = sum + Minors[i].check;

                }
                if(sum1 == sum)
                { break; }
            }

            MessageBox.Show("Finito");


            return pots_lst;



        }
        public int[,] count_unhappy(Agents[,] map, int kuar, int a) /////////Count unhappy agents for excel output

        {
            rate_check_for_all(map);
            unhappyloc = 0;
            unhappymin = 0;
            for (int r = 0; r < locals_num; r++)
            {
                if (((Locals[r].rate < Locals[r].lower_bound || Locals[r].rate > upper_bound) && Locals[r].type == 1) ||
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


        public Agents[] AdjacentElements2(Agents[,] node_map, int row, int column, int wsize)
        {
            ////// Collect neighbor cells' positions with periodic boundary conditions
            var w_size1 = wsize;
            Agents[] w = new Agents[Convert.ToUInt16(Math.Pow(2 * w_size1 + 1, 2) - 1)];
            int rows = yaxis;
            int columns = xaxis;

            int r = 0;
            if (periodic_boundary == 0)
                for (int j = row - w_size1; j <= row + w_size1; j++)
                {
                    for (int i = column - w_size1; i <= column + w_size1; i++)
                    {
                        if (!(j == row && i == column))
                        {
                            if (j >= 0 && i >= 0 && j < rows && i < columns)
                            {
                                w[r] = node_map[j, i];
                                r = r + 1;
                            }
                        }
                    }

                }
            else if (periodic_boundary == 1)
            {
                for (int j = row - w_size1; j <= row + w_size1; j++)
                {
                    for (int i = column - w_size1; i <= column + w_size1; i++)
                    {
                        if (!(j == row && i == column))
                        {
                            if (j >= 0 && i >= 0)
                            {
                                w[r] = node_map[j % xaxis, i % xaxis];
                                r = r + 1;
                            }
                            else if (j >= 0 && i < 0)
                            {
                                w[r] = node_map[j % xaxis, i + xaxis];
                                r = r + 1;

                            }
                            else if (j < 0 && i < 0)
                            {
                                w[r] = node_map[j + xaxis, i + xaxis];
                                r = r + 1;

                            }
                            else if (j < 0 && i >= 0)
                            {
                                w[r] = node_map[j + xaxis, i % xaxis];
                                r = r + 1;

                            }
                        }

                    }

                }

            }
            return w;
        }

        public Agents[] AdjacentElements(Agents[,] node_map, int row, int column)
        {
            ////// Collect neighbor cells' positions with periodic boundary conditions

            Agents[] w = new Agents[Convert.ToUInt16(Math.Pow(2 * w_size1 + 1, 2) - 1)];
            int rows = yaxis;
            int columns = xaxis;

            int r = 0;
            if (periodic_boundary == 0)
                for (int j = row - w_size1; j <= row + w_size1; j++)
                {
                    for (int i = column - w_size1; i <= column + w_size1; i++)
                    {
                        if (!(j == row && i == column))
                        {
                            if (j >= 0 && i >= 0 && j < rows && i < columns)
                            {
                                w[r] = node_map[j, i];
                                r = r + 1;
                            }
                        }
                    }

                }
            else if (periodic_boundary == 1)
            {
                for (int j = row - w_size1; j <= row + w_size1; j++)
                {
                    for (int i = column - w_size1; i <= column + w_size1; i++)
                    {
                        if (!(j == row && i == column))
                        {
                            if (j >= 0 && i >= 0)
                            {
                                w[r] = node_map[j % xaxis, i % xaxis];
                                r = r + 1;
                            }
                            else if (j >= 0 && i < 0)
                            {
                                w[r] = node_map[j % xaxis, i + xaxis];
                                r = r + 1;

                            }
                            else if (j < 0 && i < 0)
                            {
                                w[r] = node_map[j + xaxis, i + xaxis];
                                r = r + 1;

                            }
                            else if (j < 0 && i >= 0)
                            {
                                w[r] = node_map[j + xaxis, i % xaxis];
                                r = r + 1;

                            }
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
            int countx = 0;
            int county = 0;
            int count2x = 0;
            int count2y = 0;
            var arr = map2;
            int count2 = 0;
            int count3 = 0;
            int count4 = 0;
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
                if (results[i] != null)
                {
                    if (results[i].type == 1)
                    {
                        count1 = count1 + 1;
                    }
                    else if (results[i].type == 2)
                    {
                        count2 = count2 + 1;
                    }
                    else if (results[i].type == 0)
                    {
                        count3 = count3 + 1;
                    }
                    else if (results[i].type == 4)
                    {
                        count4 = count4 + 1;
                    }
                }
                else
                {
                    break;
                }
            }

            for (int i = 0; i < 8; i++)
            {

                if (results[i]!= null && results[i].type == 1 )
                {
                    countx = countx + 1;
                }
                else if (results[i] != null && results[i].type == 2)
                {
                    county = county + 1;
                }
            }

            if (w_size1 > 1)
            {
                for (int i = 8; i < 24; i++)
                {

                    if (results[i].type == 1)
                    {
                        count2x = count2x + 1;
                    }
                    else if (results[i].type == 2)
                    {
                        count2y = count2y + 1;
                    }



                }
            }

            if (node_map[x_pos, y_pos].type == 1)
            {
                node_map[x_pos, y_pos].rate_w1 = countx * 1.00 / (countx + county);
            }
            else if (node_map[x_pos, y_pos].type == 2)
            {
                node_map[x_pos, y_pos].rate_w1 = county * 1.00 / (countx + county);
            }

            if (node_map[x_pos, y_pos].type == 1)
            {
                node_map[x_pos, y_pos].rate_w2 = count2x * 1.00 / (count2x + count2y);
            }
            else if (node_map[x_pos, y_pos].type == 2)
            {
                node_map[x_pos, y_pos].rate_w2 = count2y * 1.00 / (count2x + count2y);
            }

            int[,] a;
            a = new int[4, 3];
            a[0, 0] = count1;
            a[1, 0] = count2;
            a[3, 0] = count4;
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

            double rate1 = 0;

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

            if(algo_value ==9)
            {

                if (map[lxpos, lypos].type == 1)
                {
                    if (a[0, 0] == 0)
                    {
                        rate1 = 0;
                    }
                    else
                    {
                        rate1 = a[0, 0] / c / (effect * a[1, 0] + a[0, 0]);
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
                        rate1 = effect * a[1, 0] / c / (effect * a[1, 0] + a[0, 0]);
                    }


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

                if (algo_value == 9)
                {
                    if (Locals[i].type == 1)
                    {
                        if (t[0, 0] == 0)
                        {
                            a = 0;
                        }
                        else
                        {
                            a = t[0, 0] / c / (effect * t[1, 0] + t[0, 0]);
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
                            a = effect * t[1, 0] / c / (effect * t[1, 0] + t[0, 0]);
                        }




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

                if (algo_value == 9)
                {
                    if (Minors[i].type == 1)
                    {
                        if (t[0, 0] == 0)
                        {
                            a = 0;
                        }
                        else
                        {
                            a = t[0, 0] / c / (effect * t[1, 0] + t[0, 0]);
                        }
                    }
                    else if (Minors[i].type == 2)
                    {
                        if (t[1, 0] == 0)
                        {
                            a = 0;
                        }
                        else
                        {
                            a = effect * t[1, 0] / c / (effect * t[1, 0] + t[0, 0]);
                        }




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

        public Agents[,] main_algorithm(Agents[,] map)
        //Reimplanting, basically the "move function" after the happiness check for all agents..
        ///..
        {
            Random rnd1 = new Random();  /// pseudo random generator variables
            Random rnd2 = new Random();
            Random rnd3 = new Random();
            int dice1;
            int local_index;
            int minor_index;

            //local_index = rnd2.Next(0, locals_num);
            //minor_index = rnd2.Next(0, min_num);



            if (algo_value == 0)/// If it is Kawasaki dynamic
            {

                for (int j = 0; j < locals_num; j++)
                {
                    if (((Locals[j].rate < lower_bound || Locals[j].rate > upper_bound) && Locals[j].type == 1) ||
                      ((Locals[j].rate < lower_bound2 || Locals[j].rate > upper_bound2) && Locals[j].type == 2))
                    {
                        unhappy_agents_list.Add(Locals[j]);// Collect unhappy list

                    }

                    //else if(Locals[j].rate==0.375)
                    //{

                    //    unhappy_agents_list.Add(Locals[j]);

                    //}

                }
                for (int j = 0; j < min_num; j++)

                {
                    if (((Minors[j].rate < lower_bound || Minors[j].rate > upper_bound) && Minors[j].type == 1) ||
                      ((Minors[j].rate < lower_bound2 || Minors[j].rate > upper_bound2) && Minors[j].type == 2))
                    {
                        unhappy_agents_list.Add(Minors[j]);// Collect unhappy list
                    }

                    //else if(Minors[j].rate==0.375)
                    //{
                    //    unhappy_agents_list.Add(Minors[j]);


                    //}



                }



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


            else if (algo_value == 1)  //If the algorithm is Glauber dynamic (Asyncronous)
            {

                for (int j = 0; j < locals_num; j++)
                {
                    if (((Locals[j].rate < lower_bound || Locals[j].rate > upper_bound) && Locals[j].type == 1) ||
                      ((Locals[j].rate < lower_bound2 || Locals[j].rate > upper_bound2) && Locals[j].type == 2))
                    {
                        unhappy_agents_list.Add(Locals[j]);// Collect unhappy list

                    }

                    //else if (Locals[j].rate == 0.5)
                    // {

                    //     unhappy_agents_list.Add(Locals[j]);

                    // }

                }
                for (int j = 0; j < min_num; j++)
                {
                    if (((Minors[j].rate < lower_bound || Minors[j].rate > upper_bound) && Minors[j].type == 1) ||
                      ((Minors[j].rate < lower_bound2 || Minors[j].rate > upper_bound2) && Minors[j].type == 2))
                    {
                        unhappy_agents_list.Add(Minors[j]);// Collect unhappy list
                    }

                    //else if (Minors[j].rate == 0.5)
                    //{

                    //    unhappy_agents_list.Add(Minors[j]);

                    //}
                }

                var count = unhappy_agents_list.Count;
                for (int index = 0; index < count; index++)// Respect to unhappy agent list take random element from the list 
                                                                               // convert it than take next random element from the list but only if the next agent is still unhappy if not choose next random

                {

                    dice = rnd3.Next(0, unhappy_agents_list.Count);// Roll a dice for random pick

                    if ((unhappy_agents_list[dice] != null && unhappy_agents_list[dice].type == 1 && (rate_check_for_one(unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos, map) < lower_bound
                        || rate_check_for_one(unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos, map) > upper_bound) ||
                            (unhappy_agents_list[dice] != null && unhappy_agents_list[dice].type == 2 && (rate_check_for_one(unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos, map) < lower_bound2
                        || rate_check_for_one(unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos, map) > upper_bound2))))
                    {
                        if (unhappy_agents_list[dice].type == 1 && (unhappy_agents_list[dice].rate < lower_bound || unhappy_agents_list[dice].rate > upper_bound))
                        {
                            map[unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos].type = 2;
                            unhappy_agents_list[dice].type = 2;
                            unhappy_agents_list.RemoveAt(dice);
                        }
                        //else if (unhappy_agents_list[dice].type == 1 && (unhappy_agents_list[dice].rate == 0.5))
                        //{
                        //    map[unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos].type = 2;
                        //    unhappy_agents_list[dice].type = 2;
                        //    unhappy_agents_list.RemoveAt(dice);
                        //}
                        else if (unhappy_agents_list[dice].type == 2 && (unhappy_agents_list[dice].rate < lower_bound2 || unhappy_agents_list[dice].rate > upper_bound2))
                        {
                            map[unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos].type = 1;
                            unhappy_agents_list[dice].type = 1;
                            unhappy_agents_list.RemoveAt(dice);

                        }

                        //else if (unhappy_agents_list[dice].type == 2 && (unhappy_agents_list[dice].rate == 0.5))
                        //{
                        //    map[unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos].type = 1;
                        //    unhappy_agents_list[dice].type = 1;
                        //    unhappy_agents_list.RemoveAt(dice);
                        //}

                    }
                    else
                    {
                        unhappy_agents_list.RemoveAt(dice);


                    }
                }

                unhappy_agents_list.Clear();

            }

            else if (algo_value == 2) //// Syncronous Glauber dynamic
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


                for (int i = 0; i < unhappy_agents_list.Count; i++)
                {
                    if (unhappy_agents_list[i].type == 1)
                    {
                        unhappy_agents_list[i].type = 2;
                        unhappy_agents_list.RemoveAt(i);
                    }

                    else if (unhappy_agents_list[i].type == 2)
                    {
                        unhappy_agents_list[i].type = 1;
                        unhappy_agents_list.RemoveAt(i);
                    }

                }

            }

            else if (algo_value == 3)
            {

                for (int j = 0; j < locals_num; j++)
                {
                    if (((!(Locals[j].rate == 0.5 || (Locals[j].rate == 0.25)) && Locals[j].type == 1) ||
                      ((!(Locals[j].rate == 0.5 || (Locals[j].rate == 0.25)) && Locals[j].type == 2))))
                    {
                        unhappy_agents_list.Add(Locals[j]);// Collect unhappy list

                    }



                }
                for (int j = 0; j < min_num; j++)
                {
                    if (((!(Minors[j].rate == 0.5 || Minors[j].rate == 0.25) && Minors[j].type == 1) ||
                      (((!(Minors[j].rate == 0.5 || Minors[j].rate == 0.25) && Minors[j].type == 2)))))
                    {
                        unhappy_agents_list.Add(Minors[j]);// Collect unhappy list
                    }


                }
                for (int index = 0; index < unhappy_agents_list.Count; index++)// Respect to unhappy agent list take random element from the list 
                                                                               // convert it than take next random element from the list but only if the next agent is still unhappy if not choose next random

                {

                    dice = rnd3.Next(0, unhappy_agents_list.Count);// Roll a dice for random pick

                    if (((unhappy_agents_list[dice] != null && unhappy_agents_list[dice].type == 1 && !(rate_check_for_one(unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos, map) == 0.5 ||
                        rate_check_for_one(unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos, map) == 0.25))
                      ) || (unhappy_agents_list[dice] != null && unhappy_agents_list[dice].type == 2 && !(rate_check_for_one(unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos, map) == 0.5 ||
                            rate_check_for_one(unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos, map) == 0.25)))

                    {
                        if (unhappy_agents_list[dice].type == 1 && !(unhappy_agents_list[dice].rate == 0.25 || unhappy_agents_list[dice].rate == 0.5))
                        {
                            map[unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos].type = 2;
                            unhappy_agents_list[dice].type = 2;
                            unhappy_agents_list.RemoveAt(dice);
                        }

                        else if (unhappy_agents_list[dice].type == 2 && !(unhappy_agents_list[dice].rate == 0.25 || unhappy_agents_list[dice].rate == 0.5))
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

            }

            else if (algo_value == 4)
            {
                for (int j = 0; j < locals_num; j++)
                {


                    if (Locals[j].rate != 0.25)
                    {

                        unhappy_agents_list.Add(Locals[j]);

                    }

                }
                for (int j = 0; j < min_num; j++)
                {


                    if (Minors[j].rate != 0.25)
                    {

                        unhappy_agents_list.Add(Minors[j]);

                    }
                }


                for (int index = 0; index < unhappy_agents_list.Count; index++)// Respect to unhappy agent list take random element from the list 
                                                                               // convert it than take next random element from the list but only if the next agent is still unhappy if not choose next random


                {

                    dice = rnd3.Next(0, unhappy_agents_list.Count);// Roll a dice for random pick

                    if ((unhappy_agents_list[dice] != null && unhappy_agents_list[dice].type == 1 && (rate_check_for_one(unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos, map)) != 0.25)
                        || (unhappy_agents_list[dice] != null && unhappy_agents_list[dice].type == 2 && (rate_check_for_one(unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos, map) != 0.25
                       )))
                    {
                        if (unhappy_agents_list[dice].type == 1 && (unhappy_agents_list[dice].rate != 0.25))
                        {
                            map[unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos].type = 2;
                            unhappy_agents_list[dice].type = 2;
                            unhappy_agents_list.RemoveAt(dice);




                        }

                        else if (unhappy_agents_list[dice].type == 2 && (unhappy_agents_list[dice].rate != 0.25))
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




            }

            else if (algo_value == 5)
            {

                if (utility_check == 0)

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


                    for (int i = 0; i < unhappy_agents_list.Count; ++i)
                    {

                        x = rnd1.Next(0, xaxis);
                        y = rnd1.Next(0, yaxis);

                        while (map[x, y].type == 1 || map[x, y].type == 2)
                        {
                            x = rnd1.Next(0, xaxis);
                            y = rnd1.Next(0, yaxis);

                        }

                        unhappy_agents_list[i].xpos = x;
                        unhappy_agents_list[i].ypos = y;

                        map[unhappy_agents_list[i].xpos, unhappy_agents_list[i].ypos].type = 3;
                        map[x, y].type = unhappy_agents_list[i].type;
                        node_map[x, y] = unhappy_agents_list[i];
                        unhappy_agents_list.RemoveAt(i);
                    }



                    unhappy_agents_list.Clear();
                }




                else
                {

                    for (int i = 0; i < locals_num; i++)
                    {
                        double[] uti;
                        uti = new double[3];

                        uti[1] = Locals[i].xpos;
                        uti[2] = Locals[i].ypos;
                    }
                    for (int i = 0; i < min_num; i++)
                    {
                        double[] uti;

                        uti = new double[3];

                    }
                }

            }

            else if (algo_value == 6)
            {
                for (int j = 0; j < locals_num; j++)
                {
                    if (Locals[j].rate != 0.25)
                    {
                        unhappy_agents_list.Add(Locals[j]);
                    }
                }
                for (int j = 0; j < min_num; j++)
                {
                    if (Minors[j].rate != 0.25)
                    {
                        unhappy_agents_list.Add(Minors[j]);
                    }
                }


                for (int index = 0; index < unhappy_agents_list.Count; index++)// Respect to unhappy agent list take random element from the list 
                                                                               // convert it than take next random element from the list but only if the next agent is still unhappy if not choose next random
                {

                    dice = rnd3.Next(0, unhappy_agents_list.Count);// Roll a dice for random pick

                    if ((unhappy_agents_list[dice] != null && unhappy_agents_list[dice].type == 1 && (rate_check_for_one(unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos, map)) != 0.25)
                        || (unhappy_agents_list[dice] != null && unhappy_agents_list[dice].type == 2 && (rate_check_for_one(unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos, map) != 0.25
                       )))
                    {
                        if (unhappy_agents_list[dice].type == 1 && (unhappy_agents_list[dice].rate != 0.25))
                        {
                            map[unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos].type = 2;
                            unhappy_agents_list[dice].type = 2;
                            unhappy_agents_list.RemoveAt(dice);
                        }
                        else if (unhappy_agents_list[dice].type == 2 && (unhappy_agents_list[dice].rate != 0.25))
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
            }


            else if (algo_value == 7)
            {
                for (int j = 0; j < locals_num; j++)
                {
                    if (Locals[j].rate != 0.5)
                    {
                        unhappy_agents_list.Add(Locals[j]);
                    }
                }
                for (int j = 0; j < min_num; j++)
                {
                    if (Minors[j].rate != 0.5)
                    {
                        unhappy_agents_list.Add(Minors[j]);
                    }
                }


                for (int index = 0; index < unhappy_agents_list.Count; index++)// Respect to unhappy agent list take random element from the list 
                                                                               // convert it than take next random element from the list but only if the next agent is still unhappy if not choose next random
                {

                    dice = rnd3.Next(0, unhappy_agents_list.Count);// Roll a dice for random pick

                    if ((unhappy_agents_list[dice] != null && unhappy_agents_list[dice].type == 1 && (rate_check_for_one(unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos, map)) != 0.5)
                        || (unhappy_agents_list[dice] != null && unhappy_agents_list[dice].type == 2 && (rate_check_for_one(unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos, map) != 0.5
                       )))
                    {
                        if (unhappy_agents_list[dice].type == 1 && (unhappy_agents_list[dice].rate != 0.5))
                        {
                            map[unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos].type = 2;
                            unhappy_agents_list[dice].type = 2;
                            unhappy_agents_list.RemoveAt(dice);
                        }
                        else if (unhappy_agents_list[dice].type == 2 && (unhappy_agents_list[dice].rate != 0.5))
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
            }

            else if (algo_value == 8)
            {
                for (int j = 0; j < locals_num; j++)
                {
                    if (Locals[j].rate != 0.5)
                    {
                        unhappy_agents_list.Add(Locals[j]);
                    }
                }
                for (int j = 0; j < min_num; j++)
                {
                    if (Minors[j].rate != 0.5)
                    {
                        unhappy_agents_list.Add(Minors[j]);
                    }
                }

                hamiltonian = Calculate_hamilton(sim_value);


                var count = unhappy_agents_list.Count;
                for (int indice = 0; indice < count; indice++)
                {
                    dice = rnd3.Next(0, unhappy_agents_list.Count);


                    if ((unhappy_agents_list[dice] != null && unhappy_agents_list[dice].type == 1 && (rate_check_for_one(unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos, map)) != 0.5)
                           || (unhappy_agents_list[dice] != null && unhappy_agents_list[dice].type == 2 && (rate_check_for_one(unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos, map) != 0.5
                        )))
                    {


                        if (unhappy_agents_list[dice].type == 1)
                        {
                            unhappy_agents_list[dice].type = 2;
                            map[unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos].type = 2;
                        }
                        else
                        {
                            unhappy_agents_list[dice].type = 1;
                            map[unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos].type = 1;
                        }

                        hamiltonian2 = Calculate_hamilton(sim_value);
                        double random = rnd3.Next(0, 100);
                        random = random / 100.0;

                        if (rate_check_for_one(unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos, map) != 0.5)
                        {
                            if (random > 0.05)
                            {
                                if (unhappy_agents_list[dice].type == 1)
                                {
                                    unhappy_agents_list[dice].type = 2;
                                    map[unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos].type = 2;
                                }
                                else
                                {
                                    unhappy_agents_list[dice].type = 1;
                                    map[unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos].type = 1;
                                }


                            }
                        }

                    }
                    unhappy_agents_list.RemoveAt(dice);
                }

                unhappy_agents_list.Clear();
            }

            else if (algo_value == 9)  //If the algorithm is diffusion
            {

                for (int j = 0; j < locals_num; j++)
                {
                    if (Locals[j] != null && pots_lst.Contains(Locals[j]))
                    {
                        if (((Locals[j].rate < lower_bound || Locals[j].rate > upper_bound) && Locals[j].type == 1) ||
                          ((Locals[j].rate < lower_bound2 || Locals[j].rate > upper_bound2) && Locals[j].type == 2))
                        {
                            unhappy_agents_list.Add(Locals[j]);// Collect unhappy list

                        }
                    }

                    if (Minors[j] != null && pots_lst.Contains(Minors[j]))
                    {
                        if ((((Minors[j].rate < lower_bound || Minors[j].rate > upper_bound) && Minors[j].type == 1) ||
                          ((Minors[j].rate < lower_bound2 || Minors[j].rate > upper_bound2) && Minors[j].type == 2)) && radicals_lst.Contains(Minors[j]) == false)
                        {
                            unhappy_agents_list.Add(Minors[j]);// Collect unhappy list
                        }
                    }
 
                }

                var count = unhappy_agents_list.Count;
                for (int index = 0; index < count; index++)// Respect to unhappy agent list take random element from the list 
                                                                               // convert it than take next random element from the list but only if the next agent is still unhappy if not choose next random

                {

                    dice = rnd3.Next(0, unhappy_agents_list.Count);// Roll a dice for random pick

                    if ((unhappy_agents_list[dice] != null && unhappy_agents_list[dice].type == 1 && (rate_check_for_one(unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos, map) < lower_bound
                        || rate_check_for_one(unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos, map) > upper_bound) ||
                            (unhappy_agents_list[dice] != null && unhappy_agents_list[dice].type == 2 && (rate_check_for_one(unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos, map) < lower_bound2
                        || rate_check_for_one(unhappy_agents_list[dice].xpos, unhappy_agents_list[dice].ypos, map) > upper_bound2) && unhappy_agents_list[dice].stub !=1)))
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
            }

            else// Voter model
            {
                for (int j = 0; j < locals_num; j++)
                {
                    agent_list.Add(Locals[j]);// Collect  list                 
                }
                for (int j = 0; j < min_num; j++)
                {
                    agent_list.Add(Minors[j]);// Collect  list                 
                }




                for (int i = 0; i < agent_list.Count; i++)
                {
                    dice = rnd1.Next(0, agent_list.Count);

                    var results = AdjacentElements(map, agent_list[dice].xpos, agent_list[dice].ypos);
                    dice1 = rnd1.Next(0, Convert.ToInt32(Math.Pow((2 * w_size1 + 1), 2)) - 1);

                    map[agent_list[dice].xpos, agent_list[dice].ypos].type = results[dice1].type;
                    agent_list[dice].type = results[dice1].type;


                }

                agent_list.Clear();

            }
            return map;
        }// end of continue function, it  basically includes the main algorithm of the code

        public void update_map2()// update the visual map
        {
            Brush gbrush = (Brush)Brushes.Green;
            Brush cbrush = (Brush)Brushes.LightGreen;
            Brush rbrush = (Brush)Brushes.Red;
            Brush bbrush = (Brush)Brushes.Black;
            Brush wbrush = (Brush)Brushes.White;
            Brush ybrush = (Brush)Brushes.Yellow;
            Pen bluePen = new Pen(Color.Blue, 2);
            Pen orangePen = new Pen(Color.Orange, 2);
            Pen aquqPen = new Pen(Color.Aqua, 2);

            Graphics g;

            g = this.CreateGraphics();

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


                if (Locals[i] != null && Locals[i].type == 1)
                {
                    g.FillRectangle(gbrush, Locals[i].xpos * cellSize, Locals[i].ypos * cellSize, cellSize, cellSize);
                }
                else
                {
                    g.FillRectangle(rbrush, Locals[i].xpos * cellSize, Locals[i].ypos * cellSize, cellSize, cellSize);
                }
            }

            for (int i = 0; i < min_num; ++i)
            {
                while (Minors[i] == null)
                { i = i + 1; }
                if (Minors[i] != null && Minors[i].type == 2)
                {
                    g.FillRectangle(rbrush, Minors[i].xpos * cellSize, Minors[i].ypos * cellSize, cellSize, cellSize);
                }
                else
                {
                    g.FillRectangle(gbrush, Minors[i].xpos * cellSize, Minors[i].ypos * cellSize, cellSize, cellSize);
                }
            }

            for (int i = 0; i < stub_num; i++)
            {
                g.FillRectangle(bbrush, Stubborn[i].xpos * cellSize, Stubborn[i].ypos * cellSize, cellSize, cellSize);

            }

            for (int i = 0; i < xaxis * yaxis; i++)
            {

                if (Locals[i] != null && Locals[i].check == 1)
                {
                    g.FillRectangle(cbrush, Locals[i].xpos * cellSize, Locals[i].ypos * cellSize, cellSize, cellSize);
                }
                if (Minors[i] != null && Minors[i].check == 1)
                {
                    g.FillRectangle(cbrush, Minors[i].xpos * cellSize, Minors[i].ypos * cellSize, cellSize, cellSize);
                }
                if (Stubborn[i] != null && Stubborn[i].check == 1)
                {
                    g.FillRectangle(cbrush, Stubborn[i].xpos * cellSize, Stubborn[i].ypos * cellSize, cellSize, cellSize);
                }


            }

        }
        public void update_map()// update the visual map
        {
            Brush gbrush = (Brush)Brushes.Green;
            Brush rbrush = (Brush)Brushes.Red;
            Brush bbrush = (Brush)Brushes.Black;
            Brush wbrush = (Brush)Brushes.White;
            Brush ybrush = (Brush)Brushes.Yellow;
            Brush cbrush = (Brush)Brushes.LightGreen;
            Pen bluePen = new Pen(Color.Blue, 2);
            Pen orangePen = new Pen(Color.Orange, 2);
            Pen aquqPen = new Pen(Color.Aqua, 2);

            Graphics g;

            g = this.CreateGraphics();


            // Create rectangle.


            // Draw rectangle to screen.
            // e.Graphics.DrawRectangle(blackPen, rect);
            Pen p = new Pen(Color.Black);
            if (algo_value == 9)


            {

                for (int i = 0; i < locals_num; ++i)
                {

                    while (Locals[i] == null && i < locals_num)
                    {
                        i = i + 1;

                    }


                    if (Locals[i] != null && Locals[i].type == 1 && pots_lst.Contains(Locals[i]))
                    {
                        g.FillRectangle(cbrush, Locals[i].xpos * cellSize, Locals[i].ypos * cellSize, cellSize, cellSize);
                    }
                    else if (Locals[i] != null && Locals[i].type == 2)
                    {
                        g.FillRectangle(rbrush, Locals[i].xpos * cellSize, Locals[i].ypos * cellSize, cellSize, cellSize);
                    }
                }

                for (int i = 0; i < min_num; ++i)
                {
                    while (Minors[i] == null)
                    { i = i + 1; }
                    if (Minors[i] != null && Minors[i].type == 2)
                    {
                        g.FillRectangle(rbrush, Minors[i].xpos * cellSize, Minors[i].ypos * cellSize, cellSize, cellSize);
                    }
                    else if (Minors[i] != null && Minors[i].type == 1 && pots_lst.Contains(Minors[i]))
                    {
                        g.FillRectangle(cbrush, Minors[i].xpos * cellSize, Minors[i].ypos * cellSize, cellSize, cellSize);
                    }
                }

                for (int i = 0; i < stub_num; i++)
                {
                    g.FillRectangle(bbrush, Stubborn[i].xpos * cellSize, Stubborn[i].ypos * cellSize, cellSize, cellSize);

                }


            }

            else
            {
                for (int i = 0; i < locals_num; ++i)
                {

                    while (Locals[i] == null && i < locals_num)
                    {
                        i = i + 1;

                    }


                    if (Locals[i] != null && Locals[i].type == 1)
                    {
                        g.FillRectangle(gbrush, Locals[i].xpos * cellSize, Locals[i].ypos * cellSize, cellSize, cellSize);
                    }
                    else if (Locals[i] != null && Locals[i].type == 2)
                    {
                        g.FillRectangle(rbrush, Locals[i].xpos * cellSize, Locals[i].ypos * cellSize, cellSize, cellSize);
                    }
                }

                for (int i = 0; i < min_num; ++i)
                {
                    while (Minors[i] == null)
                    { i = i + 1; }
                    if (Minors[i] != null && Minors[i].type == 2)
                    {
                        g.FillRectangle(rbrush, Minors[i].xpos * cellSize, Minors[i].ypos * cellSize, cellSize, cellSize);
                    }
                    else if (Minors[i] != null && Minors[i].type == 1)
                    {
                        g.FillRectangle(gbrush, Minors[i].xpos * cellSize, Minors[i].ypos * cellSize, cellSize, cellSize);
                    }
                }

                for (int i = 0; i < stub_num; i++)
                {
                    g.FillRectangle(bbrush, Stubborn[i].xpos * cellSize, Stubborn[i].ypos * cellSize, cellSize, cellSize);

                }

            }
        }



    
        public Agents[,] continue_2(Agents[,] map)
        {

            Agents[,] map1 = map;
            //Draw_World(xaxis, yaxis);
            //update_map();
            map1 = main_algorithm(map);

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
            final_number_loc = 0;
            final_number_min = 0;
            int kuar = 0;

            for (int a = 0; a <= sim_value - 1; a++)
            {
                Draw_World(xaxis, yaxis);
                frac_count = 0;
                kuar = 0;
                map = execute();
                find_potentials(node_map);
                update_map2();
                count_unhappy(map, kuar, a);
                for (int x = 0; x < xaxis; x++)
                {
                    for (int y = 0; y < yaxis; y++)
                    {
                        map3[x, y] = map[x, y].type;



                    }
                }
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

                for (int j = 0; j < locals_num; j++)
                {
                    for (int i = 0; i <= Math.Pow(2 * w_size1 + 1, 2) - 1; i++)
                    {
                        if (Locals[j].rate == Convert.ToDouble((i / (Math.Pow(2 * w_size1 + 1, 2) - 1))))
                        {
                            rate_histogram2[kuar, i] = rate_histogram2[kuar, i] + 1;

                        }
                    }
                }
                for (int j = 0; j < min_num; j++)
                {
                    for (int i = 0; i <= Math.Pow(2 * w_size1 + 1, 2) - 1; i++)
                    {

                        z = Convert.ToDouble((i / (Math.Pow(2 * w_size1 + 1, 2) - 1)));
                        if (Minors[j].rate == Convert.ToDouble((i / (Math.Pow(2 * w_size1 + 1, 2) - 1))))
                        {
                            rate_histogram2[kuar, i] = rate_histogram2[kuar, i] + 1;

                        }
                    }
                }

                ////************ Until sum of local and minor unhappy agents are zero****************
                int tr = 0;

                while (sum1 + sum2 > 0 )
                {
                    
                   
                    // window_keydown(null, new System.Windows.Input.KeyboardEventArgs(null, 1));
                    Calculate_energy(tr,a);
                    count_unhappy(map, kuar, a);
                    rate_check_for_all(map);

                    for (int j = 0; j < locals_num; j++)
                    {
                        for (int i = 0; i <= Math.Pow(2 * w_size1 + 1, 2) - 1; i++)
                        {
                            if (Locals[j].rate == Convert.ToDouble((i / (Math.Pow(2 * w_size1 + 1, 2) - 1))))
                            {
                                rate_histogram2[kuar, i] = rate_histogram2[kuar, i] + 1;

                            }
                        }
                    }
                    for (int j = 0; j < min_num; j++)
                    {
                        for (int i = 0; i <= Math.Pow(2 * w_size1 + 1, 2) - 1; i++)
                        {

                            z = Convert.ToDouble((i / (Math.Pow(2 * w_size1 + 1, 2) - 1)));
                            if (Minors[j].rate == Convert.ToDouble((i / (Math.Pow(2 * w_size1 + 1, 2) - 1))))
                            {
                                rate_histogram2[kuar, i] = rate_histogram2[kuar, i] + 1;

                            }
                        }
                    }



                    queue = 0;

                    map = continue_2(map);
                    kuar = kuar + 1;



                    ///********** Regarding to algorithm type determine the end condition of the system***********
                    for (int i = 0; i < locals_num; i++)
                    {

                        if (Locals[i] != null)
                        {
                            if (!(algo_value == 3 || algo_value == 4 ||algo_value ==6 || algo_value ==7  ))
                            {
                                if ((((Locals[i].rate < lower_bound || Locals[i].rate > upper_bound) && Locals[i].type == 1) ||
                                    ((Locals[i].rate < lower_bound2 || Locals[i].rate > upper_bound2) && Locals[i].type == 2)) && radicals_lst.Contains(Locals[i]) == false && pots_lst.Contains(Locals[i]))
                                {
                                    Locals[i].s = 1;

                                }
                                //else if(Locals[i].rate==0.5)
                                //{
                                //    Locals[i].s = 1;

                                //}
                                else
                                {

                                    Locals[i].s = 0;

                                }
                            }

                            else
                            {
                                if (algo_value == 6)
                                {
                                    if (!(Locals[i].rate == 0.25 || Locals[i].rate == 0.25))
                                    {
                                        Locals[i].s = 1;

                                    }

                                    else
                                    {

                                        Locals[i].s = 0;
                                    }
                                }

                                else if(algo_value ==7 || algo_value ==8)
                                {
                                    if (!(Locals[i].rate == 0.5 || Locals[i].rate == 0.5))
                                    {
                                        Locals[i].s = 1;
                                    }
                                    else
                                    {

                                        Locals[i].s = 0;
                                    }

                                }

                            }
                        }
                    }
                    for (int j = 0; j < min_num; j++)
                    {
                        if (Minors[j] != null)
                        {
                            if (!(algo_value == 3 || algo_value == 4 || algo_value == 6 || algo_value == 7))
                            {
                                if ((((Minors[j].rate < lower_bound || Minors[j].rate > upper_bound) && Minors[j].type == 1) ||
                                     ((Minors[j].rate < lower_bound2 || Minors[j].rate > upper_bound2) && Minors[j].type == 2)) && radicals_lst.Contains(Minors[j]) == false && pots_lst.Contains(Minors[j]) == true)
                                {
                                    Minors[j].s = 1;

                                }

                                //else if (Minors[j].rate == 0.5)
                                //{
                                //    Minors[j].s = 1;

                                //}
                                else
                                {
                                    Minors[j].s = 0;

                                }
                            }

                            else
                            {
                                if (algo_value == 6)
                                {
                                    if (!(Minors[j].rate == 0.25 || Minors[j].rate == 0.25))
                                    {
                                        Minors[j].s = 1;

                                    }

                                    else
                                    {

                                        Minors[j].s = 0;
                                    }
                                }

                                else if(algo_value == 7 || algo_value == 8)
                                {

                                    if (!(Minors[j].rate == 0.5 || Minors[j].rate == 0.5))
                                    {
                                        Minors[j].s = 1;

                                    }

                                    else
                                    {

                                        Minors[j].s = 0;
                                    }

                                }
                            }
                        }
                    }
                    sum1 = 0;

                    /////**************End of end condition determiner**************


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


                    final_number_loc = 0;
                    final_number_min = 0;

                    //************** Count the number of agent types in the network**********************
                    for (int i = 0; i < locals_num; i++)
                    {
                        if (Locals[i] != null && Locals[i].type == 1)
                        {
                            final_number_loc = final_number_loc + 1;
                        }

                        else if (Locals[i] != null && Locals[i].type == 2)
                        {

                            final_number_min = final_number_min + 1;
                        }

                    }
                    for (int i = 0; i < min_num; i++)
                    {
                        if (Minors[i] != null && Minors[i].type == 1)
                        {
                            final_number_loc = final_number_loc + 1;

                        }

                        else if (Minors[i] != null && Minors[i].type == 2)
                        {

                            final_number_min = final_number_min + 1;
                        }

                    }
                    price.Text = Convert.ToString(50000 - 2 * final_number_min);
                    if (simultaneous == 1)
                    {
                        price.Refresh();
                    }
                    /////************ end of agent type counting**************

                    if (sum1 + sum2 < 1000)
                    {
                        //MessageBox.Show(Convert.ToString(number_loc), Convert.ToString(number_min));
                    }



                    if (simultaneous == 1)
                    {
                        update_map();
                    }

                    
                    tr++;
                }
                ///***********End of main algorithm************************************************************

                

                


                ////****** similar neighbors histogram calculations*************
                for (int j = 0; j < locals_num; j++)
                {
                    for (int i = 0; i <= Math.Pow(2 * w_size1 + 1, 2) - 1; i++)
                    {
                        if (Locals[j].rate == Convert.ToDouble((i / (Math.Pow(2 * w_size1 + 1, 2) - 1))))
                        {
                            rate_histogram[a, i] = rate_histogram[a, i] + 1;


                        }

                        if (Locals[j].rate_w1 == Convert.ToDouble((i * 1.00 / 8)))
                        {
                            rate_histogram3[a, i] = rate_histogram3[a, i] + 1;
                        }

                        if (Locals[j].rate_w2 == Convert.ToDouble((i * 1.00 / 16)))
                        {
                            rate_histogram4[a, i] = rate_histogram4[a, i] + 1;
                        }
                    }
                }

                for (int j = 0; j < min_num; j++)
                {
                    for (int i = 0; i <= Math.Pow(2 * w_size1 + 1, 2) - 1; i++)
                    {

                        z = Convert.ToDouble((i / (Math.Pow(2 * w_size1 + 1, 2) - 1)));
                        if (Minors[j].rate == Convert.ToDouble((i / (Math.Pow(2 * w_size1 + 1, 2) - 1))))
                        {
                            rate_histogram[a, i] = rate_histogram[a, i] + 1;
                        }

                        if (Minors[j].rate_w1 == Convert.ToDouble((i * 1.00 / 8)))
                        {
                            rate_histogram3[a, i] = rate_histogram3[a, i] + 1;
                        }

                        if (Minors[j].rate_w2 == Convert.ToDouble((i * 1.00 / 16)))
                        {
                            rate_histogram4[a, i] = rate_histogram4[a, i] + 1;
                        }
                    }
                }


                ///*********************** NDF Calculations********************************             
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

                for (int i = 0; i < xaxis; i++)
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

                /////****************End of NDF CALCULATIONS*****************************************************

                update_map();
                double sum_1 = 0;
                double sum_2 = 0;
                double sum_3 = 0;
                double sum_4 = 0;
                double sum_5 = 0;
                double sum_6 = 0;
                double sum_7 = 0;
                double sum_8 = 0;


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


                local_number.Text = Convert.ToString(final_number_loc);
                minor_number.Text = Convert.ToString(final_number_min);


                if (final_number_min == 0 || final_number_loc == 0)
                { ground_state++; }

                loc_number[a] = final_number_loc * 1.00;
                mino_number[a] = final_number_min * 1.00;
                final_number_loc = 0;
                final_number_min = 0;

                unhappy_agents_list.Clear();
                pots_lst.Clear();
                queue = 0;

             
            

                //}//end of square analysis counting

                update_map();


                for (int i = 0; i < locals_num; i++)
                {
                    rate_sum = rate_sum + Locals[i].rate_w1;
                    rate_sum2 = rate_sum2 + Locals[i].rate_w2;
                }

                for (int i = 0; i < min_num; i++)
                {
                    rate_sum = rate_sum + Minors[i].rate_w1;
                    rate_sum2 = rate_sum2 + Minors[i].rate_w2;

                }

                rate_sum = rate_sum / (locals_num + min_num);

                rate_sum2 = rate_sum2 / (locals_num + min_num);

                //MessageBox.Show(Convert.ToString(rate_sum2));
                //MessageBox.Show(Convert.ToString(rate_sum));






            }// end of simulations


            if (sim_value > 1)
            {
                MessageBox.Show(Convert.ToString(ground_state));

               
            }
            Ini_mrk_shr.Text = Convert.ToString((num_ini_adopters*1.00 / xaxis / yaxis) * 100.00);
            fin_mrk_sh.Text = Convert.ToString((mino_number.Sum()*1.00 / sim_value / xaxis / yaxis) * 100.00);


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
                        //local_number.Text = Convert.ToString(loc_number.Sum() / sim_value);
            //minor_number.Text = Convert.ToString(mino_number.Sum() / sim_value);
            min_var.Text = Convert.ToString(Math.Round(mino_number.StandardDeviation(), 3));
            sep_var.Text = Convert.ToString(Math.Round(SEPAR.StandardDeviation(), 3));
            unhloc.Text = Convert.ToString(unhappyloc / sim_value);
            unhmin.Text = Convert.ToString(unhappymin / sim_value);

           // MessageBox.Show(Convert.ToString(energy_arr.Sum() / sim_value));


            if (excel_wnt == 1)
            {
                Microsoft.Office.Interop.Excel.Application xlb = new Microsoft.Office.Interop.Excel.Application();//square analysis
                Workbook wc = xlb.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet wt = (Worksheet)xlb.ActiveSheet;
                Microsoft.Office.Interop.Excel.Range rngg = wt.Cells.get_Resize(prob_dist.GetLength(0), prob_dist.GetLength(1));


                Microsoft.Office.Interop.Excel.Application xlc = new Microsoft.Office.Interop.Excel.Application();
                Workbook wd = xlc.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet wf = (Worksheet)xlc.ActiveSheet;
                Microsoft.Office.Interop.Excel.Range rngg1 = wf.Cells.get_Resize(unhappy_array.GetLength(0), unhappy_array.GetLength(1));

                Microsoft.Office.Interop.Excel.Application xld = new Microsoft.Office.Interop.Excel.Application();//count mono by time
                Workbook we = xld.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet wg = (Worksheet)xld.ActiveSheet;
                Microsoft.Office.Interop.Excel.Range rngg2 = wg.Cells.get_Resize(rate_histogram.GetLength(0), rate_histogram.GetLength(1));
                rngg2.Value2 = rate_histogram;

                Microsoft.Office.Interop.Excel.Application xle = new Microsoft.Office.Interop.Excel.Application();//count mono by time
                Workbook ws = xle.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet wr = (Worksheet)xle.ActiveSheet;
                Microsoft.Office.Interop.Excel.Range rngg3 = wr.Cells.get_Resize(rate_histogram3.GetLength(0), rate_histogram3.GetLength(1));
                rngg3.Value2 = rate_histogram3;

                Microsoft.Office.Interop.Excel.Application xlf = new Microsoft.Office.Interop.Excel.Application();//count mono by time
                Workbook wk = xlf.Workbooks.Add(XlSheetType.xlWorksheet);
                Worksheet wn = (Worksheet)xlf.ActiveSheet;
                Microsoft.Office.Interop.Excel.Range rngg4 = wn.Cells.get_Resize(energy_arr.GetLength(0), energy_arr.GetLength(1));
                rngg4.Value2 = energy_arr;

                ////rngg1.Value2 = unhappy_array;

                ////rngg.Value2 = prob_dist;


                //xlc.Visible = true;
                //xld.Visible = true;
                //xle.Visible = true;
                xlf.Visible = true;
                //xlb.Visible = true;
                //xlb.WindowState = XlWindowState.xlMaximized;
                //xlc.WindowState = XlWindowState.xlMaximized;
                xld.WindowState = XlWindowState.xlMaximized;
            }
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
        public double rate_w1 { get; set; }
        public double rate_w2 { get; set; }
        public double mixity { get; set; }
        public double het_neigh { get; set; }
        public double total_neigh { get; set; }

        public double lower_bound { get; set; }
        public double emp_neigh { get; set; }
        public double utility { get; set; }

        public double energy { get; set; }
        public double sim_neigh { get; set; }
        public string color { get; set; }
        public int type { get; set; }

        public int check { get; set;}
        public int stub {get; set; }
        public int seperatist { get; set; }
        public double FSI { get; set; }
        public int s { get; set; }

    }


}