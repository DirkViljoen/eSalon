using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using RestSharp;
using Newtonsoft.Json;

namespace Prototype
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //Login a = new Login();
            //a.ShowDialog();
            
        }

        private void webProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://localhost:8000");
        }

        private void viewMantainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchStock a = new SearchStock();
            a.ShowDialog();
            this.Close();
        }

        private void addStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddStock a = new AddStock();
            a.ShowDialog();
            this.Close();
        }

        private void addOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddOrder a = new AddOrder();
            a.ShowDialog();
            this.Close();
        }

        private void ordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchOrder a = new SearchOrder();
            a.ShowDialog();
            this.Close();
        }

        private void stockToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

    }
}
