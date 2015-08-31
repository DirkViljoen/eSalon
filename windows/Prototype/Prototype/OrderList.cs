using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using RestSharp;
using Newtonsoft.Json;
using RestSharp;
using Newtonsoft.Json;

namespace Prototype
{
    public class OrderList : System.ComponentModel.BindingList<Order>
    {
        RestRequest request = new RestRequest();
        RestClient order = new RestClient();

        public OrderList()
        {
            try
            {
                order.BaseUrl = new Uri("http://localhost:8000");
                request.Resource = "/api/order";
                IRestResponse response = order.Execute(request);
                string temp = response.Content.Replace("\"", "'");
                JsonResponseOrder test = JsonConvert.DeserializeObject<JsonResponseOrder>(temp);

                for (int k = 0; k < test.Rows.Count; k++)
                {
                    this.Add(test.Rows[k]);
                }
            }
            catch (Exception d)
            {
                 
            }

        }

        public OrderList(int OrderID)
        {
            foreach (Order o in GetOrder(OrderID))
            {
                if (OrderID == o.OrderID)
                {
                    Order Order =
                        new Order(o.OrderID,
                                    o.dPlaced,
                                    o.dReceived,
                                    o.SupplierID);
                    this.Add(Order);
                    break;
                }

            }
        }

        public void GetOrder(string sname, string dateTo, string dateFrom)
        {
            // GET
            //RestClient stock = new RestClient();
            order.BaseUrl = new Uri("http://localhost:8000");

            //var request = new RestRequest();
            request.Resource = "/api/order?sname=sname?dateTo=c&dateFrom=t";

            request = new RestRequest("/api/order?sname={sname}&dateTo={dateTo}&dateFrom={dateFrom}", Method.GET);
            //request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
            request.AddUrlSegment("dateTo", dateTo); // replaces matching token in request.Resource
            request.AddUrlSegment("sname", sname); // replaces matching token in request.Resource
            request.AddUrlSegment("dateFrom", dateFrom); // replaces matching token in request.Resource

            IRestResponse response = order.Execute(request);

            string temp = response.Content.Replace("\"", "'");
            //List<Client> list = JsonConvert.DeserializeObject<List<Client>>(temp);
            JsonResponseOrder test = JsonConvert.DeserializeObject<JsonResponseOrder>(temp);

            this.ClearItems();

            for (int k = 0; k < test.Rows.Count; k++)
            // foreach (Rows x in test)
            {
                this.Add(test.Rows[k]);
            }
        }
        
        public OrderList GetOrder()
        {

            // GET
            //RestClient Order = new RestClient();
            order.BaseUrl = new Uri("http://localhost:8000");

            //var request = new RestRequest();
            request.Resource = "/api/order";

            IRestResponse response = order.Execute(request);

            string temp = response.Content.Replace("\"", "'");
            //List<Client> list = JsonConvert.DeserializeObject<List<Client>>(temp);
            Order test = JsonConvert.DeserializeObject<Order>(temp);

            return this;
        }

        public OrderList GetOrder(int inID)
        {
            OrderList temp = new OrderList(inID);

            // GET
            order.BaseUrl = new Uri("http://localhost:8000");

            request.Resource = "/api/order/:id";

            IRestResponse response = order.Execute(request);

            string temp2 = response.Content.Replace("\"", "'");

            Order test = JsonConvert.DeserializeObject<Order>(temp2);

            return temp;
        }
        
        

        public void InsertOrder(Order s)
        {
            // POST
            this.Add(s);

            IRestResponse response = order.Execute(request);
            request = new RestRequest(Method.POST);
            request.Resource = "/api/order";

            request.AddParameter("dateTo", s.dReceived);
            request.AddParameter("dateFrom", s.dPlaced);
            request.AddParameter("sname", s.SupplierID);

            response = order.Execute(request);
        }

        public void UpdateOrder(Order s)
        {
            IRestResponse response = order.Execute(request);

            request = new RestRequest(Method.PUT);
            request.Resource = "/api/order";

            request.AddParameter("orderID", s.OrderID);
            request.AddParameter("dateTo", s.dReceived);
            request.AddParameter("dateFrom", s.dPlaced);
            request.AddParameter("supplierID", s.SupplierID);

            response = order.Execute(request);

        }
    }
}
