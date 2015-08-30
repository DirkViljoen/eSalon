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
    public partial class SearchStock : Form
    {
        StockList sl = new StockList();

        public SearchStock()
        {
            InitializeComponent();
            dataGridView1.DataSource = sl;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell.ColumnIndex == 0)
                {
                    int id = dataGridView1.CurrentCell.RowIndex;
                    //MessageBox.Show(id);
                    viewStock a = new viewStock(id);
                    a.ShowDialog();

                }
            }
            catch (Exception d)
            {
                MessageBox.Show("ERROR: " + d);
            }

            //if (dataGridView1.CurrentCell.ColumnIndex == 7)
            //{
            //    if (pnlTrack.Visible == false)
            //    {
            //        pnlTrack.Visible = true;
            //        pnlTrack.Left = Cursor.Position.X;
            //        pnlTrack.Top = Cursor.Position.Y;
            //    }
            //    else
            //    {
            //        pnlTrack.Visible = false;
            //    }
            //}
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Your changes have been saved! ");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            pnlTrack.Visible = false;
        }

        private void SearchStock_Load(object sender, EventArgs e)
        {
            pnlTrack.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                sl.GetStock(txtSName.Text, txtBName.Text, txtPName.Text);

                dataGridView1.DataSource = sl;
            }
            catch (Exception d)
            {
                MessageBox.Show("ERROR: " + d);
            }
        }
    }
}
