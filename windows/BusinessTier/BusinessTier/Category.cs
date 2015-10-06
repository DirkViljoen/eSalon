using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace BusinessTier
{
    public class JsonResponseCategory
    {
        [JsonProperty(PropertyName = "rows")]
        public List<Category> Rows { get; set; }

        /*[JsonProperty(PropertyName = "SQLstats")]
        public Stats sqlstats { get; set; }
    */
    }

    public class Category
    {
        [JsonProperty(PropertyName = "Category_id")]
        public int CategoryID { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string name { get; set; }


        public Category(int oID, string n)
        {
            CategoryID = oID;
            name = n;
        }
    }

}

