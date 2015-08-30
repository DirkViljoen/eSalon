using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Prototype
{
    public class JsonResponseOrderLine
    {
        [JsonProperty(PropertyName = "rows")]
        public List<OrderLine> Rows { get; set; }

        [JsonProperty(PropertyName = "SQLstats")]
        public Stats sqlstats { get; set; }
    }

    public class OrderLine
    {
        [JsonProperty(PropertyName = "OrderLine_id")]
        public int OrderLineLID { get; set; }

        [JsonProperty(PropertyName = "Quantity")]
        public int Quantity { get; set; }

        [JsonProperty(PropertyName = "Stock_ID")]
        public int StockID { get; set; }

        [JsonProperty(PropertyName = "Order_ID")]
        public int OrderID { get; set; }

        public OrderLine(int oID, int oQuan, int oSt, int oOrder)
        {
            OrderLineLID = oID;
            Quantity = oQuan;
            StockID = oSt;
            OrderID = oOrder;
        }
    }
}
