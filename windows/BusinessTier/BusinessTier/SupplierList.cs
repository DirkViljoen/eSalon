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
    public class SupplierList : List<Supplier>
    {
        RestRequest request = new RestRequest();
        RestClient Supplier = new RestClient();
        
        public SupplierList()
        {

            Supplier.BaseUrl = new Uri("http://localhost:8000");
            request.Resource = "/api/supplier";
            IRestResponse response = Supplier.Execute(request);
            string temp = response.Content.Replace("\"", "'");
            JsonResponseSupplier test = JsonConvert.DeserializeObject<JsonResponseSupplier>(temp);

            for (int k = 0; k < test.Rows.Count; k++)
            {
                this.Add(test.Rows[k]);
            }

        }

        public SupplierList(int SupplierID)
        {

            //Create an object for each Supplier in dataset and add to list
            foreach (Supplier SupplierRow in GetSupplier(SupplierID))
            {
                if (SupplierID == SupplierRow.SupplierID)
                {
                    Supplier Supplier =
                        new Supplier(SupplierRow.SupplierID,
                                    SupplierRow.Name,
                                    SupplierRow.Contact,
                                    SupplierRow.Email);
                    this.Add(Supplier);
                    break;
                }

            }
        }

        public SupplierList GetSupplier()
        {

            // GET
            //RestClient Supplier = new RestClient();
            Supplier.BaseUrl = new Uri("http://localhost:8000");

            //var request = new RestRequest();
            request.Resource = "/api/supplier";

            IRestResponse response = Supplier.Execute(request);

            string temp = response.Content.Replace("\"", "'");
            //List<Client> list = JsonConvert.DeserializeObject<List<Client>>(temp);
            Supplier test = JsonConvert.DeserializeObject<Supplier>(temp);
           
            return this;
        }

        public SupplierList GetSupplier(int inID)
        {
            SupplierList temp = new SupplierList(inID);

            // GET
            Supplier.BaseUrl = new Uri("http://localhost:8000");

            request.Resource = "/api/Supplier/:id";

            IRestResponse response = Supplier.Execute(request);

            string temp2 = response.Content.Replace("\"", "'");

            Supplier test = JsonConvert.DeserializeObject<Supplier>(temp2);

            return temp;
        }

    }
}
