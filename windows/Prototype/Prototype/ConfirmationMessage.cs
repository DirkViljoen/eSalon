using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prototype
{
    public partial class ConfirmationMessage : Form
    {
        string form;
        internal string res = "cancel";

        public ConfirmationMessage(string str, string _form)
        {
            InitializeComponent();
            label1.Text = str;
            form = _form;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            res = "No";
            this.Close();
            //if (form == "AddOrder")
            //{
            //    new AddOrder().ShowDialog();
            //}
            //else if (form == "UpdateOrder")
            //{
            //    new UpdateOrder().ShowDialog();
            //    this.Close();
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            res = "yes";
            this.Close();
            //MessageBox.Show("Well Done Son!");
            //Form1 a = new Form1();
            //a.ShowDialog();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            res = "cancel";
            this.Close();
            //Form1 a = new Form1();
            //a.ShowDialog();
        }
    }
}
