namespace Gradproject
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.X_axis = new System.Windows.Forms.TextBox();
            this.Y_axis = new System.Windows.Forms.TextBox();
            this.Population = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Minority = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Run = new System.Windows.Forms.Button();
            this.lowerbound = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.upperbound = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Utility_check = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.eco = new System.Windows.Forms.TextBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.sim = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.geo = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.no_free_cells = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.algo = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.lowerbound2 = new System.Windows.Forms.TextBox();
            this.upperbound2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // X_axis
            // 
            this.X_axis.Location = new System.Drawing.Point(35, 49);
            this.X_axis.Name = "X_axis";
            this.X_axis.Size = new System.Drawing.Size(43, 20);
            this.X_axis.TabIndex = 0;
            this.X_axis.Text = "60";
            this.X_axis.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Y_axis
            // 
            this.Y_axis.Location = new System.Drawing.Point(35, 92);
            this.Y_axis.Name = "Y_axis";
            this.Y_axis.Size = new System.Drawing.Size(43, 20);
            this.Y_axis.TabIndex = 1;
            this.Y_axis.Text = "60";
            this.Y_axis.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // Population
            // 
            this.Population.Location = new System.Drawing.Point(134, 48);
            this.Population.Name = "Population";
            this.Population.Size = new System.Drawing.Size(100, 20);
            this.Population.TabIndex = 4;
            this.Population.Text = "1800";
            this.Population.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(131, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "# Locals";
            // 
            // Minority
            // 
            this.Minority.Location = new System.Drawing.Point(134, 91);
            this.Minority.Name = "Minority";
            this.Minority.Size = new System.Drawing.Size(100, 20);
            this.Minority.TabIndex = 6;
            this.Minority.Text = "1800";
            this.Minority.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(131, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "# Minority";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 27;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Y Axis:";
            // 
            // Run
            // 
            this.Run.Location = new System.Drawing.Point(83, 315);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(75, 23);
            this.Run.TabIndex = 12;
            this.Run.Text = "Run";
            this.Run.UseVisualStyleBackColor = true;
            this.Run.Click += new System.EventHandler(this.button1_Click);
            // 
            // lowerbound
            // 
            this.lowerbound.Location = new System.Drawing.Point(290, 48);
            this.lowerbound.Name = "lowerbound";
            this.lowerbound.Size = new System.Drawing.Size(34, 20);
            this.lowerbound.TabIndex = 13;
            this.lowerbound.Text = "0.5";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(287, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Lower Bound:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(32, 119);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(133, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Economic sectors?(1 or 0):";
            // 
            // upperbound
            // 
            this.upperbound.Location = new System.Drawing.Point(290, 91);
            this.upperbound.Name = "upperbound";
            this.upperbound.Size = new System.Drawing.Size(34, 20);
            this.upperbound.TabIndex = 17;
            this.upperbound.Text = "1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(287, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Upper Bound:";
            // 
            // Utility_check
            // 
            this.Utility_check.Location = new System.Drawing.Point(290, 176);
            this.Utility_check.Name = "Utility_check";
            this.Utility_check.Size = new System.Drawing.Size(100, 20);
            this.Utility_check.TabIndex = 19;
            this.Utility_check.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(287, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Continous Utility:";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(216, 202);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(136, 17);
            this.radioButton1.TabIndex = 23;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Discrete Utility Function";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(216, 225);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(174, 17);
            this.radioButton2.TabIndex = 24;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Triangular continous utility func.";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(216, 248);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(132, 17);
            this.radioButton3.TabIndex = 25;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Circular Utility Function";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(38, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 13);
            this.label10.TabIndex = 28;
            this.label10.Text = "X Axis:";
            // 
            // eco
            // 
            this.eco.Location = new System.Drawing.Point(35, 135);
            this.eco.Name = "eco";
            this.eco.Size = new System.Drawing.Size(100, 20);
            this.eco.TabIndex = 29;
            this.eco.Text = "0";
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(216, 271);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(151, 17);
            this.radioButton4.TabIndex = 30;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Asymmetric favors similarity";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(216, 294);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(164, 17);
            this.radioButton5.TabIndex = 31;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "Asymmetric favors dissimilarity";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // sim
            // 
            this.sim.Location = new System.Drawing.Point(290, 135);
            this.sim.Name = "sim";
            this.sim.Size = new System.Drawing.Size(100, 20);
            this.sim.TabIndex = 32;
            this.sim.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(287, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Simulation";
            // 
            // geo
            // 
            this.geo.Location = new System.Drawing.Point(35, 176);
            this.geo.Name = "geo";
            this.geo.Size = new System.Drawing.Size(100, 20);
            this.geo.TabIndex = 34;
            this.geo.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(32, 160);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 13);
            this.label11.TabIndex = 35;
            this.label11.Text = "Immovable Cells:";
            // 
            // no_free_cells
            // 
            this.no_free_cells.Location = new System.Drawing.Point(35, 233);
            this.no_free_cells.Name = "no_free_cells";
            this.no_free_cells.Size = new System.Drawing.Size(33, 20);
            this.no_free_cells.TabIndex = 36;
            this.no_free_cells.Text = "2";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(32, 204);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(120, 26);
            this.label12.TabIndex = 37;
            this.label12.Text = "World without free cells:\r\n                (0-1)";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // algo
            // 
            this.algo.Location = new System.Drawing.Point(35, 272);
            this.algo.Name = "algo";
            this.algo.Size = new System.Drawing.Size(33, 20);
            this.algo.TabIndex = 38;
            this.algo.Text = "3";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(35, 256);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(50, 13);
            this.label13.TabIndex = 39;
            this.label13.Text = "Algorithm";
            // 
            // lowerbound2
            // 
            this.lowerbound2.Location = new System.Drawing.Point(330, 46);
            this.lowerbound2.Name = "lowerbound2";
            this.lowerbound2.Size = new System.Drawing.Size(30, 20);
            this.lowerbound2.TabIndex = 40;
            this.lowerbound2.Text = "0.5";
            // 
            // upperbound2
            // 
            this.upperbound2.Location = new System.Drawing.Point(330, 92);
            this.upperbound2.Name = "upperbound2";
            this.upperbound2.Size = new System.Drawing.Size(30, 20);
            this.upperbound2.TabIndex = 41;
            this.upperbound2.Text = "1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 463);
            this.Controls.Add(this.upperbound2);
            this.Controls.Add(this.lowerbound2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.algo);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.no_free_cells);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.geo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sim);
            this.Controls.Add(this.radioButton5);
            this.Controls.Add(this.radioButton4);
            this.Controls.Add(this.eco);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Utility_check);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.upperbound);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lowerbound);
            this.Controls.Add(this.Run);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Minority);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Population);
            this.Controls.Add(this.Y_axis);
            this.Controls.Add(this.X_axis);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox X_axis;
        private System.Windows.Forms.TextBox Y_axis;
        private System.Windows.Forms.TextBox Population;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Minority;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Run;
        private System.Windows.Forms.TextBox lowerbound;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox upperbound;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox Utility_check;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox eco;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.TextBox sim;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox geo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox no_free_cells;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox algo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox lowerbound2;
        private System.Windows.Forms.TextBox upperbound2;
    }
}

