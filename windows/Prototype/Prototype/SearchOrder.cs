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
    public partial class SearchOrder : Form
    {
        public SearchOrder()
        {
            InitializeComponent();
        }

        //private void button4_Click(object sender, EventArgs e)
        //{

        //}

        //private void button3_Click(object sender, EventArgs e)
        //{

        //}

        //private void button1_Click(object sender, EventArgs e)
        //{

        //}

        private void button5_Click(object sender, EventArgs e)
        {
            AddOrder a = new AddOrder();
            a.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UpdateOrder a = new UpdateOrder();
            a.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReceiveStock a = new ReceiveStock();
            a.ShowDialog();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentCell.ColumnIndex == 0)
            {
                ViewOrder a = new ViewOrder();
                a.ShowDialog();
            }
        }

        private void SearchOrder_Load(object sender, EventArgs e)
        {

        }
    }
}
