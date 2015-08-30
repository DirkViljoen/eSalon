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
    public partial class AddOrder : Form
    {
        public AddOrder()
        {
            InitializeComponent();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = "Are you sure you want to add this order?";
            string form = "AddOrder";

            ConfirmationMessage f = new ConfirmationMessage(str, form);
            f.ShowDialog();
            MessageBox.Show(f.res);


        }
    }
}
