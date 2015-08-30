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
    class SupplierList : System.ComponentModel.BindingList<Supplier>
    {
 RestRequest request = new RestRequest();
        RestClient Supplier = new RestClient();
        
        public SupplierList()
        {
            //Create an object for each Supplier in the dataset and add to list

            foreach (Supplier SupplierRow in GetSupplier())
            {
                Supplier Supplier = new Supplier(
                    SupplierRow.SupplierID,
                    SupplierRow.Name,
                    SupplierRow.Contact,
                    SupplierRow.Email);

                this.Add(Supplier);
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

        /*public void InsertSupplier(Supplier s)
        {
            // POST

            IRestResponse response = Supplier.Execute(request);
            request = new RestRequest(Method.POST);
            request.Resource = "/api/Supplier";

            request.AddParameter("quantity", s.Brand);
            request.AddParameter("SupplierId", s.Product);
            request.AddParameter("quantity", s.Price);
            request.AddParameter("SupplierId", s.Size);
            request.AddParameter("quantity", s.Active);
            request.AddParameter("SupplierId", s.Quantity);
            request.AddParameter("quantity", s.Barcode);
            request.AddParameter("SupplierId", s.CategoryID);
            request.AddParameter("quantity", s.SupplierID);

            response = Supplier.Execute(request);
        }

        public void DeleteSupplier(Supplier s)
        {
            IRestResponse response = Supplier.Execute(request);

            request = new RestRequest(Method.PUT);
            request.Resource = "/api/Supplier/:id";

            request.AddParameter("SupplierId", s.SupplierID);
            request.AddParameter("quantity", s.Active);

            response = Supplier.Execute(request);
        }

        public void UpdateSupplier(Supplier s)
        {
            IRestResponse response = Supplier.Execute(request);

            request = new RestRequest(Method.PUT);
            request.Resource = "/api/supplier";

            request.AddParameter("supplierID", s.SupplierID);
            request.AddParameter("name", s.Name);
            request.AddParameter("contact", s.Contact);
            request.AddParameter("email", s.Email);

            response = Supplier.Execute(request);

        }*/
    }
}
