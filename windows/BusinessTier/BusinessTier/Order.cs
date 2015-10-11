using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Collections;
using MySql.Data.MySqlClient;

namespace BusinessTier
{

    public class Order
    {
        private int mOrder_id;
        private string mDatePlaced;
        private string mDateReceived;
        private int mSupplier_ID;

        public int OrderID
        {
            get { return mOrder_id; }
            set { mOrder_id = value; }
        }

        public string DatePlaced
        {
            get { return mDatePlaced; }
            set { mDatePlaced = value; }
        }

        public string DateReceived
        {
            get { return mDateReceived; }
            set { mDateReceived = value; }
        }

        public int Supplier_ID
        {
            get { return mSupplier_ID; }
            set { mSupplier_ID = value; }
        }

        public Order() { }
        public Order(int oID, string oPlace, string oRec, int oSuppID)
        {
            OrderID = oID;
            DatePlaced = oPlace;
            DateReceived = oRec;
            Supplier_ID = oSuppID;
        }

        
    }

}
