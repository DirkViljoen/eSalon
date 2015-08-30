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
   public class OrderList : System.ComponentModel.BindingList<Order>
    {
 RestRequest request = new RestRequest();
        RestClient Order = new RestClient();
        
        public OrderList()
        {
            //Create an object for each Order in the dataset and add to list

            foreach (Order OrderRow in GetOrder())
            {
                Order Order = new Order(
                    OrderRow.OrderID,
                    OrderRow.Place,
                    OrderRow.Receive,
                    OrderRow.SupplierID);

                this.Add(Order);
            }

        }

        public OrderList(int OrderID)
        {

            //Create an object for each Order in dataset and add to list
            foreach (Order OrderRow in GetOrder(OrderID))
            {
                if (OrderID == OrderRow.OrderID)
                {
                    Order Order =
                        new Order(OrderRow.OrderID,
                                    OrderRow.Place,
                                    OrderRow.Receive,
                                    OrderRow.SupplierID);

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

            IRestResponse response = Order.Execute(request);
            request = new RestRequest(Method.POST);
            request.Resource = "/api/Order";

            request.AddParameter("orderID", s.OrderID);
            request.AddParameter("dateTo", s.Receive);
            request.AddParameter("dateFrom", s.Place);
            request.AddParameter("supplierID", s.SupplierID);

            response = Order.Execute(request);
        }

        public void UpdateOrder(Order s)
        {
            IRestResponse response = Order.Execute(request);

            request = new RestRequest(Method.PUT);
            request.Resource = "/api/Order";

            request.AddParameter("orderID", s.OrderID);
            request.AddParameter("dateTo", s.Receive);
            request.AddParameter("dateFrom", s.Place);
            request.AddParameter("supplierID", s.SupplierID);

            response = Order.Execute(request);

        }
    }
}
