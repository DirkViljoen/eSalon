using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessTier
{
    public class Stock
    {
        private int mStockID;
        private string mBrand;
        private string mProduct;
        private double mPrice;
        private int mSize;
        private bool mActive;
        private int mQuantity;
        private string mBarcode;
        private int mCategoryID;
        private int mSupplierID;

        public Stock()
        {

        }

        public Stock(int oID, string sbrand, string sProduct, double sPrice, int sSize, bool sAct, int sQuan, string sBar, int sCat, int sSupp)
        {
            mStockID = oID;
            mBrand = sbrand;
            mProduct = sProduct;
            mPrice = sPrice;
            mSize = sSize;
            mActive = sAct;
            mQuantity = sQuan;
            mBarcode = sBar;
            mCategoryID = sCat;
            mSupplierID = sSupp;
        }

        public int StockID
        {
            get { return mStockID; }
            set { mStockID = value; }
        }
        public string Brand
        {
            get { return mBrand; }
            set { mBrand = value; }
        }
        public string Product
        {
            get { return mProduct; }
            set { mProduct = value; }
        }
        public double Price
        {
            get { return mPrice; }
            set { mPrice = value; }
        }
        public int Size
        {
            get { return mSize; }
            set { mSize = value; }
        }
        public bool Active
        {
            get { return mActive; }
            set { mActive = value; }
        }
        public int Quantity
        {
            get { return mQuantity; }
            set { mQuantity = value; }
        }
        public string Barcode
        {
            get { return mBarcode; }
            set { mBarcode = value; }
        }
        public int CategoryID
        {
            get { return mCategoryID; }
            set { mCategoryID = value; }
        }
        public int SupplierID
        {
            get { return mSupplierID; }
            set { mSupplierID = value; }
        }
    }
}
