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
    public class StockList : System.ComponentModel.BindingList<Stock>
    {

        RestRequest request = new RestRequest();
        RestClient stock = new RestClient();

        public StockList()
        {
            // GET
            //RestClient stock = new RestClient();
            stock.BaseUrl = new Uri("http://localhost:8000");

            //var request = new RestRequest();
            request.Resource = "/api/stock";

            IRestResponse response = stock.Execute(request);

            string temp = response.Content.Replace("\"", "'");
            //List<Client> list = JsonConvert.DeserializeObject<List<Client>>(temp);

            JsonResponseStock test = JsonConvert.DeserializeObject<JsonResponseStock>(temp);

            for (int k = 0; k < test.Rows.Count; k++)
            // foreach (Rows x in test)
            {
                this.Add(test.Rows[k]);
            }
        }

        public StockList(string empty)
        {

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

        public void GetStock(string sname, string bname, string pname)
        {
            // GET
            //RestClient stock = new RestClient();
            stock.BaseUrl = new Uri("http://localhost:8000");

            //var request = new RestRequest();
            request.Resource = "/api/stock?sname=stock?pname=c&bname=t";

            request = new RestRequest("/api/stock?sname={sname}&pname={pname}&bname={bname}", Method.GET);
            //request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
            request.AddUrlSegment("pname", pname); // replaces matching token in request.Resource
            request.AddUrlSegment("sname", sname); // replaces matching token in request.Resource
            request.AddUrlSegment("bname", bname); // replaces matching token in request.Resource

            IRestResponse response = stock.Execute(request);

            string temp = response.Content.Replace("\"", "'");
            //List<Client> list = JsonConvert.DeserializeObject<List<Client>>(temp);
            JsonResponseStock test = JsonConvert.DeserializeObject<JsonResponseStock>(temp);

            this.ClearItems();

            for (int k = 0; k < test.Rows.Count; k++)
            // foreach (Rows x in test)
            {
                this.Add(test.Rows[k]);
            }
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
            this.Add(s);

            IRestResponse response = stock.Execute(request);
            request = new RestRequest(Method.POST);
            request.Resource = "/api/stock";

            request.AddParameter("brandName", s.Brand);
            request.AddParameter("productName", s.Product);
            request.AddParameter("price", s.Price);
            request.AddParameter("_size", s.Size);
            request.AddParameter("quantity", s.Quantity);
            request.AddParameter("barcode", s.Barcode);
            request.AddParameter("supplierID", s.SupplierID);

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
            request.AddParameter("brandName", s.Brand);
            request.AddParameter("productName", s.Product);
            request.AddParameter("price", s.Price);
            request.AddParameter("_size", s.Size);
            request.AddParameter("active", s.Active);
            request.AddParameter("quantity", s.Quantity);
            request.AddParameter("barcode", s.Barcode);
            request.AddParameter("supplierID", s.SupplierID);

            response = stock.Execute(request);

        }
    }
}
