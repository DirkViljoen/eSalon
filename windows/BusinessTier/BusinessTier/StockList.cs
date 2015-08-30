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
    class StockList : System.ComponentModel.BindingList<Stock>
    {

        RestRequest request = new RestRequest();
        RestClient stock = new RestClient();
        
        public StockList()
        {
            //Create an object for each Stock in the dataset and add to list

            foreach (Stock StockRow in GetStock())
            {
                Stock Stock = new Stock(
                    StockRow.StockID,
                    StockRow.Brand,
                    StockRow.Product,
                    StockRow.Price,
                    StockRow.Size,
                    StockRow.Active,
                    StockRow.Quantity,
                    StockRow.Barcode,
                    StockRow.CategoryID,
                    StockRow.SupplierID);

                this.Add(Stock);
            }

        }

        public StockList(int StockID)
        {

            //Create an object for each Stock in dataset and add to list
            foreach (Stock StockRow in GetStock(StockID))
            {
                if (StockID == StockRow.StockID)
                {
                    Stock Stock =
                        new Stock(StockRow.StockID,
                                    StockRow.Brand,
                                    StockRow.Product,
                                    StockRow.Price,
                                    StockRow.Size,
                                    StockRow.Active,
                                    StockRow.Quantity,
                                    StockRow.Barcode,
                                    StockRow.CategoryID,
                                    StockRow.SupplierID);
                    this.Add(Stock);
                    break;
                }

            }
        }

        public StockList GetStock()
        {
            // GET
            //RestClient stock = new RestClient();
            stock.BaseUrl = new Uri("http://localhost:8000");

            //var request = new RestRequest();
            request.Resource = "/api/stock";

            IRestResponse response = stock.Execute(request);

            string temp = response.Content.Replace("\"", "'");
            //List<Client> list = JsonConvert.DeserializeObject<List<Client>>(temp);
            Stock test = JsonConvert.DeserializeObject<Stock>(temp);
           
            return this;
        }

        public StockList GetStock(int inID)
        {
            StockList temp = new StockList(inID);

            // GET
            stock.BaseUrl = new Uri("http://localhost:8000");

            request.Resource = "/api/stock/:id";

            IRestResponse response = stock.Execute(request);

            string temp2 = response.Content.Replace("\"", "'");

            Stock test = JsonConvert.DeserializeObject<Stock>(temp2);

            return temp;
        }

        public void InsertStock(Stock s)
        {
            // POST

            IRestResponse response = stock.Execute(request);
            request = new RestRequest(Method.POST);
            request.Resource = "/api/stock";

            request.AddParameter("quantity", s.Brand);
            request.AddParameter("stockId", s.Product);
            request.AddParameter("quantity", s.Price);
            request.AddParameter("stockId", s.Size);
            request.AddParameter("quantity", s.Active);
            request.AddParameter("stockId", s.Quantity);
            request.AddParameter("quantity", s.Barcode);
            request.AddParameter("stockId", s.CategoryID);
            request.AddParameter("quantity", s.SupplierID);

            response = stock.Execute(request);
        }

        public void DeleteStock(Stock s)
        {
            IRestResponse response = stock.Execute(request);

            request = new RestRequest(Method.PUT);
            request.Resource = "/api/stock/:id";

            request.AddParameter("stockId", s.StockID);
            request.AddParameter("quantity", s.Active);

            response = stock.Execute(request);
        }

        public void UpdateStock(Stock s)
        {
            IRestResponse response = stock.Execute(request);

            request = new RestRequest(Method.PUT);
            request.Resource = "/api/stock";

            request.AddParameter("stockId", s.StockID);
            request.AddParameter("quantity", s.Brand);
            request.AddParameter("stockId", s.Product);
            request.AddParameter("quantity", s.Price);
            request.AddParameter("stockId", s.Size);
            request.AddParameter("quantity", s.Active);
            request.AddParameter("stockId", s.Quantity);
            request.AddParameter("quantity", s.Barcode);
            request.AddParameter("stockId", s.CategoryID);
            request.AddParameter("quantity", s.SupplierID);

            response = stock.Execute(request);

        }
    }
}
