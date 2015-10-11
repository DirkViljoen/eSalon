using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace BusinessTier
{
    

    public class StockHistory
    {
        private int mStockHistory_id;
        private double mPrice;
        private DateTime mpFrom;
        private DateTime mpTo;
        private int mStockID;

        public int StockHistory_id
        {
            get { return mStockHistory_id; }
            set { mStockHistory_id = value; }
        }
        public double Price
        {
            get { return mPrice; }
            set { mPrice = value; }
        }
        public DateTime pFrom
        {
            get { return mpFrom; }
            set { mpFrom = value; }
        }
        public DateTime pTo
        {
            get { return mpTo; }
            set { mpTo = value; }
        }
        public int StockID
        {
            get { return mStockID; }
            set { mStockID = value; }
        }


        public StockHistory() { }
        public StockHistory(int oID, double p, DateTime from, DateTime to, int i)
        {
            StockHistory_id = oID;
            Price = p;
            pFrom = from;
            pTo = to;
            StockID = i;
        }
    }

}

