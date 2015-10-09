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
        OrderLineList ol = new OrderLineList();
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

        public void GetOrder(string sid, DateTime from, DateTime to)
        {
            // GET
            //RestClient stock = new RestClient();
            Order.BaseUrl = new Uri("http://localhost:8000");
            string tempDate1 = from.ToString("yyyy-MM-dd");
            string tempDate2 = to.ToString("yyyy-MM-dd");

            //var request = new RestRequest();
            request.Resource = "/api/order?sid=1?dateTo=2015-01-01&dateFrom=2012-01-01";

            request = new RestRequest("/api/order?sid={sname}&dateTo={dateTo}&dateFrom={dateFrom}", Method.GET);
            //request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
            request.AddUrlSegment("sname", sid); // replaces matching token in request.Resource
            request.AddUrlSegment("dateTo", tempDate2); // replaces matching token in request.Resource
            request.AddUrlSegment("dateFrom", tempDate1); // replaces matching token in request.Resource

            IRestResponse response = Order.Execute(request);

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

        public void InsertOrder(Order s, string stock)
        {
            // POST
            this.Add(s);

            IRestResponse response = Order.Execute(request);
            request = new RestRequest(Method.POST);
            request.Resource = "/api/order";

            string tempDate = s.dPlaced.ToString("yyyy-MM-dd");

            request.AddParameter("Order_id", null);
            request.AddParameter("DatePlaced", tempDate);
            request.AddParameter("DateReceived", "2020-01-01");
            //say hello
            request.AddParameter("Supplier_ID", s.SupplierID);
            request.AddParameter("Stock", stock);

            response = Order.Execute(request);
        }

        public void DeleteOrder(Order s)
        {
            IRestResponse response = Order.Execute(request);

            request = new RestRequest(Method.DELETE);
            request.Resource = "/api/order/:id";

            request.AddParameter("OrderID", s.OrderID);

            response = Order.Execute(request);
        }

        public void UpdateOrder(Order s, OrderLineList oll)
        {
            IRestResponse response = Order.Execute(request);

            request = new RestRequest(Method.PUT);
            request.Resource = "/api/order";

            string tempDate = s.dReceived.ToString("yyyy-MM-dd");

            for (int i = 0; i < oll.Count; i++)
            {
                request.AddParameter("date", tempDate);
                request.AddParameter("orderID", s.OrderID);
                request.AddParameter("quantity", oll[i].Quantity);
                request.AddParameter("orderLID", oll[i].OrderLineLID);

                response = Order.Execute(request);
            }
        }

    }
}
