using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace BusinessTier
{
    public class JsonResponseSupplier
    {
        [JsonProperty(PropertyName = "rows")]
        public List<Supplier> Rows { get; set; }

        /*[JsonProperty(PropertyName = "SQLstats")]
        public Stats sqlstats { get; set; }
    */
    }

    public class Supplier
    {
        [JsonProperty(PropertyName = "Supplier_id")]
        public int SupplierID { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "ContactNumber")]
        public string Contact { get; set; }

        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "Active")]
        public bool active { get; set; }

        public Supplier(int sID, string sName, string sNum, string sEm)
        {
            SupplierID = sID;
            Name = sName;
            Contact = sNum;
            Email = sEm;
        }
    }

}

