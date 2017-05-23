//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Project 3 - Infix to Postfix Expressions
//	File Name:		main.cs
//	Description:	Define a class InfixtoPostfix
//	Course:			CSCI 2210-001 - Data Structures
//	Author:			Shuhai Li, lis002@goldmail.etsu.edu, Department of Computing, East Tennessee State University
//	Created:		Friday, 11/06/2015
//	Copyright:		Shuhai Li, 2015
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfixtoPostfix
{
    /// <summary>
    /// Provide UI functions
    /// </summary>

    public partial class InfixToPostfix : Form
    {
        List<string> input = new List<string>();
        public InfixToPostfix()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void inputInFixDataFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            string fileName = null;
            if (DialogResult.Cancel!=ofd.ShowDialog())
            {
                fileName = ofd.FileName;
            }
            //StreamReader rdr = new StreamReader("C:/Users/Shuhai Li/Documents/Visual Studio 2015/Projects/InfixtoPostfix/inputdata.txt");
            StreamReader rdr = new StreamReader(fileName);
            //List<string> input = new List<string>();
            while (rdr.Peek() != -1)
            {
                input.Add(rdr.ReadLine());
            }

            foreach (string x in input)
            {
                listBox1.Items.Add(x);
            }
            //textBox1.Text = input[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex!=-1)
            {
                //textBox2.Text = listBox1.SelectedItem.ToString();
                textBox2.Text = input[listBox1.SelectedIndex];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string inputString = null;
            string outputString = null;
            if (textBox2.Text!=null)
            {
                inputString = textBox2.Text;
                Postfix a = new Postfix(inputString);
                outputString=a.Convert(inputString);
            }
            textBox3.Text = outputString;
        }
 
        private void aboutInfixToPostfixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Application.Run(new AboutBox1());
            AboutBox a = new AboutBox();
            a.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName;
        }

    }
}
