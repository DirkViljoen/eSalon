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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            res = "yes";
            this.Close();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            res = "cancel";
            this.Close();
        }

        private void ConfirmationMessage_Load(object sender, EventArgs e)
        {

        }
    }
}
