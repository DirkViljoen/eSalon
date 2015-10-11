using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BusinessTier
{
    public class Globals
    {
        public static string constring = "server=localhost;user=root;pwd=root;database=esalon;";
        public static StockList s = new StockList();
        public static OrderList o = new OrderList();
        public static SupplierList sp = new SupplierList();

    }
}
