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
    public class CategoryList : System.ComponentModel.BindingList<Category>
    {
        RestRequest request = new RestRequest();
        RestClient Category = new RestClient();

        public CategoryList()
        {
            try
            {
                Category.BaseUrl = new Uri("http://localhost:8000");
                request.Resource = "/api/Category";
                IRestResponse response = Category.Execute(request);
                string temp = response.Content.Replace("\"", "'");
                JsonResponseCategory test = JsonConvert.DeserializeObject<JsonResponseCategory>(temp);

                for (int k = 0; k < test.Rows.Count; k++)
                {
                    this.Add(test.Rows[k]);
                }
            }
            catch (Exception d)
            {
               
            }

        }

        public CategoryList(int CategoryID)
        {
            foreach (Category o in GetCategory(CategoryID))
            {
                if (CategoryID == o.CategoryID)
                {
                    Category Category =
                        new Category(o.CategoryID,
                                    o.name);
                    this.Add(Category);
                    break;
                }

            }
        }

        public CategoryList GetCategory()
        {

            // GET
            //RestClient Category = new RestClient();
            Category.BaseUrl = new Uri("http://localhost:8000");

            //var request = new RestRequest();
            request.Resource = "/api/Category";

            IRestResponse response = Category.Execute(request);

            string temp = response.Content.Replace("\"", "'");
            //List<Client> list = JsonConvert.DeserializeObject<List<Client>>(temp);
            Category test = JsonConvert.DeserializeObject<Category>(temp);

            return this;
        }

        public CategoryList GetCategory(int inID)
        {
            CategoryList temp = new CategoryList(inID);

            // GET
            Category.BaseUrl = new Uri("http://localhost:8000");

            request.Resource = "/api/Category/:id";

            IRestResponse response = Category.Execute(request);

            string temp2 = response.Content.Replace("\"", "'");

            Category test = JsonConvert.DeserializeObject<Category>(temp2);

            return temp;
        }



        
    }
}
