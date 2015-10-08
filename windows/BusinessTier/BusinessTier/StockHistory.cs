using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace BusinessTier
{
    public class JsonResponseStockHistory
    {
        [JsonProperty(PropertyName = "rows")]
        public List<StockHistory> Rows { get; set; }

        /*[JsonProperty(PropertyName = "SQLstats")]
        public Stats sqlstats { get; set; }
    */
    }

    public class StockHistory
    {
        [JsonProperty(PropertyName = "StockHistory_id")]
        public int StockHistoryID { get; set; }

        [JsonProperty(PropertyName = "Price")]
        public double Price { get; set; }

        [JsonProperty(PropertyName = "PriceDateFrom")]
        public DateTime PriceDFrom { get; set; }

        [JsonProperty(PropertyName = "PriceDateTo")]
        public DateTime PriceDTo { get; set; }

        [JsonProperty(PropertyName = "Stock_ID")]
        public int StockID { get; set; }



        public StockHistory(int oID, double p, DateTime from, DateTime to, int i)
        {
            StockHistoryID = oID;
            Price = p;
            PriceDFrom = from;
            PriceDTo = to;
            StockID = i;
        }
    }

}

