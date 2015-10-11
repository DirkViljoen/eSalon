using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessTier;

using Excel = Microsoft.Office.Interop.Excel;

namespace Prototype
{
    public partial class SearchStock : Form
    {
        StockList sl = new StockList();

        public SearchStock()
        {
            InitializeComponent();
            dataGridView1.DataSource = sl.ViewAllStock();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell.ColumnIndex == 0)
                {
                    int id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value);
                    viewStock a = new viewStock(id);
                    a.ShowDialog();

                }
                if (dataGridView1.CurrentCell.ColumnIndex == 7)
                {
                    int id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[7].Value);
                    ReconcileStock a = new ReconcileStock(id);
                    a.ShowDialog();

                }
            }
            catch (Exception d)
            {
                MessageBox.Show("ERROR: " + d);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StockList ss = new StockList();
            try
            {
                ss = sl.SearchStock(txtSName.Text, txtBName.Text, txtPName.Text);
                
                dataGridView1.DataSource = ss;

                if (ss.Count == 0)
                {
                    MessageBox.Show("No Items Found");
                }
            }
            catch (Exception d)
            {
                MessageBox.Show("ERROR: " + d);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = true;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;

            
                worksheet.Cells[1] = dataGridView1.Columns[2].HeaderText;
                worksheet.Cells[2] = dataGridView1.Columns[3].HeaderText;
                worksheet.Cells[3] = dataGridView1.Columns[5].HeaderText;
                worksheet.Cells[4] = dataGridView1.Columns[7].HeaderText;
            

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    if (j == 2)
                    {
                        if (dataGridView1.Rows[i].Cells[j].Value != null)
                        {
                            worksheet.Cells[i + 2, 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                        else
                        {
                            worksheet.Cells[i + 2, 1] = "";
                        }
                    }
                    if (j == 3)
                    {
                        if (dataGridView1.Rows[i].Cells[j].Value != null)
                        {
                            worksheet.Cells[i + 2, 2] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                        else
                        {
                            worksheet.Cells[i + 2, 2] = "";
                        }
                    }
                    if (j == 5)
                    {
                        if (dataGridView1.Rows[i].Cells[j].Value != null)
                        {
                            worksheet.Cells[i + 2, 3] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                        else
                        {
                            worksheet.Cells[i + 2, 3] = "";
                        }
                    }
                    if (j == 7)
                    {
                        if (dataGridView1.Rows[i].Cells[j].Value != null)
                        {
                            worksheet.Cells[i + 2, 4] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                        else
                        {
                            worksheet.Cells[i + 2, 4] = "";
                        }
                    }
                }
            }
        }

        private void copyAlltoClipboard()
        {
            dataGridView1.SelectAll();
            DataObject dataObj = dataGridView1.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void SearchStock_Load(object sender, EventArgs e)
        {

        }

    }
}

