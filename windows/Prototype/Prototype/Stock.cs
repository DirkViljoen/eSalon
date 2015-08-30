using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Prototype
{
    public class JsonResponseStock
    {
        [JsonProperty(PropertyName = "rows")]
        public List<Stock> Rows { get; set; }

        [JsonProperty(PropertyName = "SQLstats")]
        public Stats sqlstats { get; set; }
    }

    public class Stock
    {

        [JsonProperty(PropertyName = "Stock_id")]
        public int StockID { get; set; }

        [JsonProperty(PropertyName = "BrandName")]
        public string Brand { get; set; }

        [JsonProperty(PropertyName = "ProductName")]
        public string Product { get; set; }

        [JsonProperty(PropertyName = "Price")]
        public double Price { get; set; }

        [JsonProperty(PropertyName = "Size")]
        public int Size { get; set; }

        [JsonProperty(PropertyName = "Active")]
        public bool Active { get; set; }

        [JsonProperty(PropertyName = "Quantity")]
        public int Quantity { get; set; }

        [JsonProperty(PropertyName = "Barcode")]
        public string Barcode { get; set; }

        [JsonProperty(PropertyName = "Category_ID")]
        public int CategoryID { get; set; }

        [JsonProperty(PropertyName = "Supplier_ID")]
        public int SupplierID { get; set; }

        public Stock(int oID, string sbrand, string sProduct, double sPrice, int sSize, bool sAct, int sQuan, string sBar, int sCat, int sSupp)
        {
            StockID = oID;
            Brand = sbrand;
            Product = sProduct;
            Price = sPrice;
            Size = sSize;
            Active = sAct;
            Quantity = sQuan;
            Barcode = sBar;
            CategoryID = sCat;
            SupplierID = sSupp;
        }

        public Stock(int oID, string sbrand, string sProduct, double sPrice, int sSize, bool sAct, int sQuan, int sCat, int sSupp)
        {
            StockID = oID;
            Brand = sbrand;
            Product = sProduct;
            Price = sPrice;
            Size = sSize;
            Active = sAct;
            Quantity = sQuan;
            CategoryID = sCat;
            SupplierID = sSupp;
        }
    }
}
