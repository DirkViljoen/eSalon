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
    public class UserList : System.ComponentModel.BindingList<User>
    {
        RestRequest request = new RestRequest();
        RestClient User = new RestClient();

        public UserList()
        {
            try
            {
                User.BaseUrl = new Uri("http://localhost:8000");
                request.Resource = "/api/User";
                IRestResponse response = User.Execute(request);
                string temp = response.Content.Replace("\"", "'");
                JsonResponseUser test = JsonConvert.DeserializeObject<JsonResponseUser>(temp);

                for (int k = 0; k < test.Rows.Count; k++)
                {
                    this.Add(test.Rows[k]);
                }
            }
            catch (Exception d)
            {

            }

        }

        public UserList(int UserID)
        {
            foreach (User o in GetUser(UserID))
            {
                if (UserID == o.UserID)
                {
                    User User =
                        new User(o.UserID,
                                    o.name,
                                    o.pass,
                                    o.empID,
                                    o.roleID);
                    this.Add(User);
                    break;
                }

            }
        }

        public void GetUser(string sname, string dateTo, string dateFrom)
        {
            // GET
            //RestClient stock = new RestClient();
            User.BaseUrl = new Uri("http://localhost:8000");

            //var request = new RestRequest();
            request.Resource = "/api/User?sname=sname?dateTo=c&dateFrom=t";

            request = new RestRequest("/api/User?sname={sname}&dateTo={dateTo}&dateFrom={dateFrom}", Method.GET);
            //request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
            request.AddUrlSegment("dateTo", dateTo); // replaces matching token in request.Resource
            request.AddUrlSegment("sname", sname); // replaces matching token in request.Resource
            request.AddUrlSegment("dateFrom", dateFrom); // replaces matching token in request.Resource

            IRestResponse response = User.Execute(request);

            string temp = response.Content.Replace("\"", "'");
            //List<Client> list = JsonConvert.DeserializeObject<List<Client>>(temp);
            JsonResponseUser test = JsonConvert.DeserializeObject<JsonResponseUser>(temp);

            this.ClearItems();

            for (int k = 0; k < test.Rows.Count; k++)
            // foreach (Rows x in test)
            {
                this.Add(test.Rows[k]);
            }
        }

        public UserList GetUser()
        {

            // GET
            //RestClient User = new RestClient();
            User.BaseUrl = new Uri("http://localhost:8000");

            //var request = new RestRequest();
            request.Resource = "/api/User";

            IRestResponse response = User.Execute(request);

            string temp = response.Content.Replace("\"", "'");
            //List<Client> list = JsonConvert.DeserializeObject<List<Client>>(temp);
            User test = JsonConvert.DeserializeObject<User>(temp);

            return this;
        }

        public UserList GetUser(int inID)
        {
            UserList temp = new UserList(inID);

            // GET
            User.BaseUrl = new Uri("http://localhost:8000");

            request.Resource = "/api/User/:id";

            IRestResponse response = User.Execute(request);

            string temp2 = response.Content.Replace("\"", "'");

            User test = JsonConvert.DeserializeObject<User>(temp2);

            return temp;
        }

    }
}
