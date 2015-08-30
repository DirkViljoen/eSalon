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
    public class StockHistoryList : System.ComponentModel.BindingList<StockHistory>
    {
 RestRequest request = new RestRequest();
        RestClient StockHistory = new RestClient();
        
        public StockHistoryList()
        {
            //Create an object for each StockHistory in the dataset and add to list

            foreach (StockHistory StockHistoryRow in GetStockHistory())
            {
                StockHistory StockHistory = new StockHistory(
                                    StockHistoryRow.StockHistoryID,
                                    StockHistoryRow.Price,
                                    StockHistoryRow.To,
                                    StockHistoryRow.From,
                                    StockHistoryRow.StockID);

                this.Add(StockHistory);
            }

        }

        public StockHistoryList(int StockHistoryID)
        {

            //Create an object for each StockHistory in dataset and add to list
            foreach (StockHistory StockHistoryRow in GetStockHistory(StockHistoryID))
            {
                if (StockHistoryID == StockHistoryRow.StockHistoryID)
                {
                    StockHistory StockHistory =
                        new StockHistory(StockHistoryRow.StockHistoryID,
                                    StockHistoryRow.Price,
                                    StockHistoryRow.To,
                                    StockHistoryRow.From,
                                    StockHistoryRow.StockID);
                    this.Add(StockHistory);
                    break;
                }

            }
        }

        public StockHistoryList GetStockHistory()
        {

            // GET
            //RestClient StockHistory = new RestClient();
            StockHistory.BaseUrl = new Uri("http://localhost:8000");

            //var request = new RestRequest();
            request.Resource = "/api/stock";

            IRestResponse response = StockHistory.Execute(request);

            string temp = response.Content.Replace("\"", "'");
            //List<Client> list = JsonConvert.DeserializeObject<List<Client>>(temp);
            StockHistory test = JsonConvert.DeserializeObject<StockHistory>(temp);
           
            return this;
        }

        public StockHistoryList GetStockHistory(int inID)
        {
            StockHistoryList temp = new StockHistoryList(inID);

            // GET
            StockHistory.BaseUrl = new Uri("http://localhost:8000");

            request.Resource = "/api/stock/:id";

            IRestResponse response = StockHistory.Execute(request);

            string temp2 = response.Content.Replace("\"", "'");

            StockHistory test = JsonConvert.DeserializeObject<StockHistory>(temp2);

            return temp;
        }

        public void InsertStockHistory(StockHistory s)
        {
            // POST

            IRestResponse response = StockHistory.Execute(request);
            request = new RestRequest(Method.POST);
            request.Resource = "/api/stock";

            request.AddParameter("price", s.Price);
            request.AddParameter("startdate", s.From);
            request.AddParameter("enddate", s.To);
            request.AddParameter("id", s.StockID);

            response = StockHistory.Execute(request);
        }

        public void DeleteStockHistory(StockHistory s)
        {
            IRestResponse response = StockHistory.Execute(request);

            request = new RestRequest(Method.PUT);
            request.Resource = "/api/stock/:id";

            request.AddParameter("stockHistoryId", s.StockHistoryID);
            request.AddParameter("price", s.Price);
            request.AddParameter("startdate", s.From);
            request.AddParameter("enddate", s.To);
            request.AddParameter("id", s.StockID);

            response = StockHistory.Execute(request);
        }

        public void UpdateStockHistory(StockHistory s)
        {
            IRestResponse response = StockHistory.Execute(request);

            request = new RestRequest(Method.PUT);
            request.Resource = "/api/stock";

            request.AddParameter("stockHistoryId", s.StockHistoryID);
            request.AddParameter("price", s.Price);
            request.AddParameter("startdate", s.From);
            request.AddParameter("enddate", s.To);
            request.AddParameter("id", s.StockID);

            response = StockHistory.Execute(request);

        }
    }
}
