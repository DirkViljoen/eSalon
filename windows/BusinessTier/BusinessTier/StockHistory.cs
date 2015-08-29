using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessTier
{
    public class StockHistory
    {
        private int mStockHId;
        private double mPrice;
        private DateTime mFrom;
        private DateTime mTo;
        private int mStockID;

        public StockHistory()
        {

        }

        public StockHistory(int oID, double price, DateTime oPlace, DateTime oRec, int oSuppID)
        {
            mStockHId = oID;
            mPrice = price;
            mFrom = oPlace;
            mTo = oRec;
            mStockID = oSuppID;
        }

        public int StockHistoryID
        {
            get { return StockHistoryID; }
            set { StockHistoryID = value; }
        }
        public double Price
        {
            get { return mPrice; }
            set { mPrice = value; }
        }
        public DateTime From
        {
            get { return mFrom; }
            set { mFrom = value; }
        }
        public DateTime To
        {
            get { return mTo; }
            set { mTo = value; }
        }
        public int StockID
        {
            get { return mStockID; }
            set { mStockID = value; }
        }
    }
}
