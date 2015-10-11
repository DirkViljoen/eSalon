using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace BusinessTier
{
    public class Stock
    {
        private int mStockID;
        public int StockID
        {
            get { return mStockID; }
            set { mStockID = value; }
        }

        private string mBrand;
        public string Brand
        {
            get { return mBrand; }
            set { mBrand = value; }
        }

        private string mProduct;
        public string Product
        {
            get { return mProduct; }
            set { mProduct = value; }
        }

        private double mPrice;
        public double Price
        {
            get { return mPrice; }
            set { mPrice = value; }
        }

        private int mSize;
        public int Size
        {
            get { return mSize; }
            set { mSize = value; }
        }

        private bool mActive;
        public bool Active
        {
            get { return mActive; }
            set { mActive = value; }
        }

        private int mQuantity;
        public int Quantity
        {
            get { return mQuantity; }
            set { mQuantity = value; }
        }

        private string mBarcode;
        public string Barcode
        {
            get { return mBarcode; }
            set { mBarcode = value; }
        }

        private int mCategoryID;
        public int CategoryID
        {
            get { return mCategoryID; }
            set { mCategoryID = value; }
        }

        private int mSupplierID;
        public int SupplierID
        {
            get { return mSupplierID; }
            set { mSupplierID = value; }
        }

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

        public Stock()
        {}
    }
}
