using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alternanza_Disk
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Data.form2.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            BackColor = Color.Pink;
            panel1.BackColor = Color.Pink;
            panel2.BackColor = Color.Pink;
            button1.BackColor = Color.Pink;
            button2.BackColor = Color.Pink;
            button3.BackColor = Color.Pink;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BackColor = Color.White;
            panel1.BackColor = Color.White;
            panel2.BackColor = Color.White;
            button1.BackColor = Color.White;
            button2.BackColor = Color.White;
            button3.BackColor = Color.White;


        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            BackColor = Color.Yellow;
            panel1.BackColor = Color.Yellow;
            panel2.BackColor = Color.Yellow;
            button1.BackColor = Color.Yellow;
            button2.BackColor = Color.Yellow;
            button3.BackColor = Color.Yellow;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            BackColor = Color.Green;
            panel1.BackColor = Color.Green;
            panel2.BackColor = Color.Green;
            button1.BackColor = Color.Green;
            button2.BackColor = Color.Green;
            button3.BackColor = Color.Green;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_Click(object sender, EventArgs e)
        {

            }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
    }

