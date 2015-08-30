using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessTier
{
    private class Supplier
    {
        private int mSupplierID;
        private string mName;
        private string mContact;
        private string mEmail;

        public Supplier()
        {

        }

        public Supplier(int oID, string oPlace, string oRec, string oSuppID)
        {
            mSupplierID = oID;
            mName = oPlace;
            mContact = oRec;
            mEmail = oSuppID;
        }

        public int SupplierID
        {
            get { return mSupplierID; }
            set { mSupplierID = value; }
        }
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }
        public string Contact
        {
            get { return mContact; }
            set { mContact = value; }
        }
        public string Email
        {
            get { return mEmail; }
            set { mEmail = value; }
        }
    }
}
