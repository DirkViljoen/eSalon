using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace BusinessTier
{
    public class Supplier
    {
        private int mSupplier_id;
        private string mName;
        private string mEmail;

        public int Supplier_id
        {
            get { return mSupplier_id; }
            set { mSupplier_id = value; }
        }
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }
        public string Email
        {
            get { return mEmail; }
            set { mEmail = value; }
        }

        public Supplier() { }

        public Supplier(int sID, string sName, string sEm)
        {
            Supplier_id = sID;
            Name = sName;
            Email = sEm;
        }
    }

}

