using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace BusinessTier
{

    public class OrderLine
    {
        private int mOrderLine_id;
        private int mQuantity;
        private int mStock_ID;
        private int mOrder_ID;

        public int OrderLine_id
        {
            get { return mOrderLine_id; }
            set { mOrderLine_id = value; }
        }
        public int Quantity
        {
            get { return mQuantity; }
            set { mQuantity = value; }
        }
        public int Stock_ID
        {
            get { return mStock_ID; }
            set { mStock_ID = value; }
        }
        public int OrderID
        {
            get { return mOrder_ID; }
            set { mOrder_ID = value; }
        }
        public OrderLine()
        {
        }

        public OrderLine(int oID, int oQuan, int oSt, int oOrder)
        {
            OrderLine_id = oID;
            Quantity = oQuan;
            Stock_ID = oSt;
            OrderID = oOrder;
        }
    }
}
