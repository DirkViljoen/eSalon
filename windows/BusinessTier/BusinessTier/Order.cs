using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessTier
{
    public class Order
    {
        private int orderID;
        private DateTime Placed;
        private DateTime Received;
        private int supplierID;

        public Order()
        {

        }

        public Order(int oID, DateTime oPlace, DateTime oRec, int oSuppID)
        {
            orderID = oID;
            Placed = oPlace;
            Received = oRec;
            supplierID = oSuppID;
        }

        public int OrderID
        {
            get { return orderID; }
            set { orderID = value; }
        }
        public DateTime Place
        {
            get { return Placed; }
            set { Placed = value; }
        }
        public DateTime Receive
        {
            get { return Received; }
            set { Received = value; }
        }
        public int SupplierID
        {
            get { return supplierID; }
            set { supplierID = value; }
        }

    }

   
}
