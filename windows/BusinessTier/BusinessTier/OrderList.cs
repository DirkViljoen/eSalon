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

namespace BusinessTier
{
    public class OrderList : List<Order>
    {
        RestRequest request = new RestRequest();
        RestClient Order = new RestClient();

        public OrderList()
        {
            Order.BaseUrl = new Uri("http://localhost:8000");
            request.Resource = "/api/order";
            IRestResponse response = Order.Execute(request);
            string temp = response.Content.Replace("\"", "'");
            JsonResponseOrder test = JsonConvert.DeserializeObject<JsonResponseOrder>(temp);

            for (int k = 0; k < test.Rows.Count; k++)
            {
                this.Add(test.Rows[k]);
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

        public OrderList GetOrder()
        {

            // GET
            //RestClient Order = new RestClient();
            Order.BaseUrl = new Uri("http://localhost:8000");

            //var request = new RestRequest();
            request.Resource = "/api/order";

            IRestResponse response = Order.Execute(request);

            string temp = response.Content.Replace("\"", "'");
            //List<Client> list = JsonConvert.DeserializeObject<List<Client>>(temp);
            Order test = JsonConvert.DeserializeObject<Order>(temp);

            return this;
        }

        public OrderList GetOrder(int inID)
        {
            OrderList temp = new OrderList(inID);

            // GET
            Order.BaseUrl = new Uri("http://localhost:8000");

            request.Resource = "/api/order/:id";

            IRestResponse response = Order.Execute(request);

            string temp2 = response.Content.Replace("\"", "'");

            Order test = JsonConvert.DeserializeObject<Order>(temp2);

            return temp;
        }

        public void InsertOrder(Order s)
        {
            // POST
            this.Add(s);

            IRestResponse response = Order.Execute(request);
            request = new RestRequest(Method.POST);
            request.Resource = "/api/order";

            request.AddParameter("Order_id", s.OrderID);
            request.AddParameter("DatePlaced", s.dPlaced);
            request.AddParameter("DateReceived", s.dReceived);
            request.AddParameter("Supplier_ID", s.SupplierID);

            response = Order.Execute(request);
        }

    }
}
