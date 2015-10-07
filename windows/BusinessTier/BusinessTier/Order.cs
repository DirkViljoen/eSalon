using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Collections;

namespace BusinessTier
{
    public class JsonResponseOrder
    {
        [JsonProperty(PropertyName = "rows")]
        public List<Order> Rows { get; set; }

        /*[JsonProperty(PropertyName = "SQLstats")]
        public Stats sqlstats { get; set; }*/
    }

    

    public class Order : OrderLine
    {
        [JsonProperty(PropertyName = "Order_id")]
        public int OrderID { get; set; }

        [JsonProperty(PropertyName = "DatePlaced")]
        public DateTime dPlaced { get; set; }

        [JsonProperty(PropertyName = "DateReceived")]
        public DateTime dReceived { get; set; }

        [JsonProperty(PropertyName = "Supplier_ID")]
        public int SupplierID { get; set; }

        ArrayList ol = new ArrayList();

        public Order(int oID, DateTime oPlace, DateTime oRec, int oSuppID)
        {
            OrderID = oID;
            dPlaced = oPlace;
            dReceived = oRec;
            SupplierID = oSuppID;

            OrderLine obj = new OrderLine(OrderLineLID, Quantity, StockID, OrderID);
            ol.Add(obj);
        }
    }

}
