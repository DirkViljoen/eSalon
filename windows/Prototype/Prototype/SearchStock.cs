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
        public SearchStock()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                viewStock a = new viewStock();
                a.ShowDialog();
            }

            if (dataGridView1.CurrentCell.ColumnIndex == 7)
            {
                if (pnlTrack.Visible == false)
                {
                    pnlTrack.Visible = true;
                    pnlTrack.Left = Cursor.Position.X;
                    pnlTrack.Top = Cursor.Position.Y;
                }
                else
                {
                    pnlTrack.Visible = false;
                }
            }
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
    }
}
