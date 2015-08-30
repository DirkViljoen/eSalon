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
    public class OrderLineList : System.ComponentModel.BindingList<OrderLine>
    {
 RestRequest request = new RestRequest();
        RestClient OrderLine = new RestClient();
        
        public OrderLineList()
        {
            //Create an object for each OrderLine in the dataset and add to list

            foreach (OrderLine OrderLineRow in GetOrderLine())
            {
                OrderLine OrderLine = new OrderLine(
                                    OrderLineRow.OrderLineID,
                                    OrderLineRow.Quantity,
                                    OrderLineRow.StockID,
                                    OrderLineRow.OrderID);

                this.Add(OrderLine);
            }

        }

        public OrderLineList(int OrderLineID)
        {

            //Create an object for each OrderLine in dataset and add to list
            foreach (OrderLine OrderLineRow in GetOrderLine(OrderLineID))
            {
                if (OrderLineID == OrderLineRow.OrderLineID)
                {
                    OrderLine OrderLine =
                        new OrderLine(OrderLineRow.OrderLineID,
                                    OrderLineRow.Quantity,
                                    OrderLineRow.StockID,
                                    OrderLineRow.OrderID);
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

            IRestResponse response = OrderLine.Execute(request);
            request = new RestRequest(Method.POST);
            request.Resource = "/api/order";

            request.AddParameter("quantity", s.Quantity);
            request.AddParameter("stockID", s.StockID);
            request.AddParameter("orderID", s.OrderID);

            response = OrderLine.Execute(request);
        }

       /* public void DeleteOrderLine(OrderLine s)
        {
            IRestResponse response = OrderLine.Execute(request);

            request = new RestRequest(Method.PUT);
            request.Resource = "/api/orders/:id";

            request.AddParameter("OrderLineId", s.OrderLineID);
            request.AddParameter("quantity", s.Active);

            response = OrderLine.Execute(request);
        }*/

        public void UpdateOrderLine(OrderLine s)
        {
            IRestResponse response = OrderLine.Execute(request);

            request = new RestRequest(Method.PUT);
            request.Resource = "/api/orders";

            request.AddParameter("OrderLineId", s.OrderLineID);
            request.AddParameter("quantity", s.Quantity);
            request.AddParameter("stockID", s.StockID);
            request.AddParameter("orderID", s.OrderID);

            response = OrderLine.Execute(request);

        }
    }
}
