using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessTier;

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

        private void pnlTrack_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                string str = "Are you sure you want to update this order?";
                string form = "UpdateStock";

                //sl.UpdateStock(new Stock(0, txtBrand.Text, txtProduct.Text, Convert.ToDouble(txtPrice.Text),
                //Convert.ToInt32(txtSize.Text), true, Convert.ToInt32(txtQuantity.Text), txtBarcode.Text, 0,
                //Convert.ToInt32(txtSupplier.Text)));
                ConfirmationMessage a = new ConfirmationMessage(str, form);
                a.ShowDialog();
                MessageBox.Show("A Product has been updated");
            }
            catch (Exception d)
            {
                MessageBox.Show("ERROR: " + d);
            }
        }
    }
}
