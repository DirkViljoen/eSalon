using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace BusinessTier
{
        public class JsonResponseUser
        {
            [JsonProperty(PropertyName = "rows")]
            public List<User> Rows { get; set; }

            /*[JsonProperty(PropertyName = "SQLstats")]
            public Stats sqlstats { get; set; }
        */
        }

        public class User
        {
            [JsonProperty(PropertyName = "User_ID")]
            public int UserID { get; set; }

            [JsonProperty(PropertyName = "Username")]
            public string name { get; set; }

            [JsonProperty(PropertyName = "Password")]
            public string pass { get; set; }

            [JsonProperty(PropertyName = "Employee_ID")]
            public int empID { get; set; }

            [JsonProperty(PropertyName = "Role_ID")]
            public int roleID { get; set; }

            [JsonProperty(PropertyName = "Active")]
            public bool active { get; set; }

            public User(int oID, string n, string p, int e, int r)
            {
                UserID = oID;
                name = n;
                pass = p;
                empID = e;
                roleID = r;
            }
        }

    }

