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
            
            Login a = new Login();
            a.ShowDialog();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string Url = "http://localhost:8000/api/clients/2";

            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString(Url);
            }

            // GET
            var client = new RestClient();
            client.BaseUrl = new Uri("http://localhost:8000");

            var request = new RestRequest();
            request.Resource = "/api/clients/2";

            IRestResponse response = client.Execute(request);

            string temp = response.Content.Replace("\"", "'");
            //List<Client> list = JsonConvert.DeserializeObject<List<Client>>(temp);
            JsonResponse test = JsonConvert.DeserializeObject<JsonResponse>(temp);

            // POST
            request = new RestRequest(Method.POST);
            request.Resource = "/api/orderLine";

            request.AddParameter("quantity", 3);
            request.AddParameter("stockID", 1);
            request.AddParameter("orderID", 2);

            response = client.Execute(request);

            // PUT
            request = new RestRequest(Method.PUT);
            request.Resource = "/api/orderLine";

            request.AddParameter("orderLineID", 1);
            request.AddParameter("quantity", 8);

            response = client.Execute(request);

        }

        private void webProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.google.co.za");
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
