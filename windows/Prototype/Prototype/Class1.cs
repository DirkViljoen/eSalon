using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Prototype
{
    public class JsonResponse
    {
        [JsonProperty(PropertyName = "rows")]
        public List<Client> Rows { get; set; }

        [JsonProperty(PropertyName = "SQLstats")]
        public Stats sqlstats { get; set; }
    }

    public class Stats
    {

        [JsonProperty(PropertyName = "insertId")]
        public int insertId { get; set; }

    }

    public class Client
    {

        [JsonProperty(PropertyName = "Name")]
        public string FName { get; set; }

        [JsonProperty(PropertyName = "Surname")]
        public string LName { get; set; }

    }


}
