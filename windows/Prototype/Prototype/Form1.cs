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


        private void addProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddStock a = new AddStock();
            a.ShowDialog();
        }

        private void aDDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddOrder a = new AddOrder();
            a.ShowDialog();
        }

        private void awesomeRestOfTheProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.google.co.za");
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchOrder a = new SearchOrder();
            a.ShowDialog();
        }

        private void viewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SearchStock a = new SearchStock();
            a.ShowDialog();
        }

        private void addEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void viewToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void mainMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {

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

        }

        //public InitiateOutboundCall(CallOptions options)
        //{
        //    Require.Argument("Caller", options.Caller);
        //    Require.Argument("Called", options.Called);
        //    Require.Argument("Url", options.Url);

        //    var request = new RestRequest(Method.POST);
        //    request.Resource = "Accounts/{AccountSid}/Calls";
        //    request.RootElement = "Calls";

        //    request.AddParameter("Caller", options.Caller);
        //    request.AddParameter("Called", options.Called);
        //    request.AddParameter("Url", options.Url);

        //    if (options.Method.HasValue) request.AddParameter("Method", options.Method);
        //    if (options.SendDigits.HasValue()) request.AddParameter("SendDigits", options.SendDigits);
        //    if (options.IfMachine.HasValue) request.AddParameter("IfMachine", options.IfMachine.Value);
        //    if (options.Timeout.HasValue) request.AddParameter("Timeout", options.Timeout.Value);

            
        //}

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

    }
}
