using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessTier
{
    public class OrderLine
    {
        private int mOrderLineLID;
        private int mQuantity;
        private int mStockID;
        private int mOrderID;

        public OrderLine()
        {

        }

        public OrderLine(int oID, int oQuan, int oSt, int oOrder)
        {
            mOrderLineLID = oID;
            mQuantity = oQuan;
            mStockID = oSt;
            mOrderID = oOrder;
        }

        public int OrderLineID
        {
            get { return OrderLineID; }
            set { OrderLineID = value; }
        }
        public int Quantity
        {
            get { return mQuantity; }
            set { mQuantity = value; }
        }
        public int StockID
        {
            get { return mStockID; }
            set { mStockID = value; }
        }
        public int OrderID
        {
            get { return mOrderID }
            set { mOrderID = value; }
        }
    }
}
