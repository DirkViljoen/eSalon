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
    public class OrderLineList : System.ComponentModel.BindingList<OrderLine>
    {
        RestRequest request = new RestRequest();
        RestClient OrderLine = new RestClient();

        public OrderLineList()
        {
            OrderLine.BaseUrl = new Uri("http://localhost:8000");
            request.Resource = "/api/order";
            IRestResponse response = OrderLine.Execute(request);
            string temp = response.Content.Replace("\"", "'");
            JsonResponseOrderLine test = JsonConvert.DeserializeObject<JsonResponseOrderLine>(temp);

            for (int k = 0; k < test.Rows.Count; k++)
            {
                this.Add(test.Rows[k]);
            }

        }

        public OrderLineList(int OrderLineID)
        {
            foreach (OrderLine o in GetOrderLine(OrderLineID))
            {
                if (OrderLineID == o.OrderLineLID)
                {
                    OrderLine OrderLine =
                        new OrderLine(o.OrderLineLID,
                                    o.Quantity,
                                    o.StockID,
                                    o.OrderID);
                    this.Add(OrderLine);
                    break;
                }

            }
        }

        public OrderLineList GetOrderLine()
        {

            // GET
            //RestClient OrderLine = new RestClient();
            OrderLine.BaseUrl = new Uri("http://localhost:8000");

            //var request = new RestRequest();
            request.Resource = "/api/order";

            IRestResponse response = OrderLine.Execute(request);

            string temp = response.Content.Replace("\"", "'");
            //List<Client> list = JsonConvert.DeserializeObject<List<Client>>(temp);
            OrderLine test = JsonConvert.DeserializeObject<OrderLine>(temp);

            return this;
        }

        public OrderLineList GetOrderLine(int inID)
        {
            OrderLineList temp = new OrderLineList(inID);

            // GET
            OrderLine.BaseUrl = new Uri("http://localhost:8000");

            request.Resource = "/api/order/:id";

            IRestResponse response = OrderLine.Execute(request);

            string temp2 = response.Content.Replace("\"", "'");

            OrderLine test = JsonConvert.DeserializeObject<OrderLine>(temp2);

            return temp;
        }

        public void InsertOrderLine(OrderLine s)
        {
            // POST
            this.Add(s);

            IRestResponse response = OrderLine.Execute(request);
            request = new RestRequest(Method.POST);
            request.Resource = "/api/order";

            request.AddParameter("orderLineID", s.OrderLineLID);
            request.AddParameter("quantity", s.Quantity);
            request.AddParameter("stockID", s.StockID);
            request.AddParameter("orderID", s.OrderID);

            response = OrderLine.Execute(request);
        }
        
    }
}
