using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gradproject

{

    
    public partial class Form1 : Form
    {
        public int population = 0;
        public int minority = 0;
        public int x_axis = 0;
        public int y_axis = 0;
        public Form1()
        {
            InitializeComponent();
        }

        public void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        public void textBox4_TextChanged(object sender, EventArgs e)
        {
           
        }

        public void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        public void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 Form2 = new Form2(Population.Text, Minority.Text, X_axis.Text, Y_axis.Text,
                lowerbound.Text, lowerbound2.Text, eco.Text, upperbound.Text, Utility_check.Text, sim.Text,
                geo.Text, simultaneous.Text, algo.Text, upperbound2.Text, wsize.Text,simultaneous.Text);
            Form2.Show();

           
    
        }

       
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            Utility_check.Text = "0";


        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Utility_check.Text = "1";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Utility_check.Text = "2";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Utility_check.Text = "3";
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            Utility_check.Text = "4";
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void simultaneous_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
