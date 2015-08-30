using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;

namespace BusinessTier
{
   public class OrderList : System.ComponentModel.BindingList<Order>
    {
               public OrderList()
        {
            OrderTableAdapter.Fill(ds.Order);
            //Create an object for each Order in the dataset
            //and add to list
            foreach (PrettyPawsVetDataSet.OrderRow OrderRow in ds.Order.Rows)
            {
                Order Order = new Order(OrderRow.OrderID,
                    OrderRow.Place,
                    OrderRow.Receive,
                    OrderRow.SupplierID);
                this.Add(Order);
            }

        }

        public OrderList(int OrderID)
        {
            OrderTableAdapter.Fill(ds.Order);

            //Create an object for each Order in dataset and add to list
            foreach (PrettyPawsVetDataSet.OrderRow OrderRow in ds.Order.Rows)
            {
                if (OrderID == OrderRow.OrderID)
                {
                    Order Order =
                        new Order((int)OrderRow["OrderID"],
                            (DateTime)OrderRow["Place"],
                            (DateTime)OrderRow["Receive"],
                            (int)OrderRow["SupplierID"]);
                    this.Add(Order);
                    break;
                }

            }
        }

        public void Save()
        {
            PrettyPawsVetDataSet.OrderDataTable tempDataTable = new PrettyPawsVetDataSet.OrderDataTable();

            foreach (Order Order in this)
            {
                PrettyPawsVetDataSet.OrderRow newOrderRow = ds.Order.NewOrderRow();
                
                newOrderRow.OrderID = Order.OrderID;
                newOrderRow.Place = Order.Place;
                newOrderRow.Receive = Order.Receive;
                newOrderRow.SupplierID = Order.SupplierID;

                tempDataTable.Rows.Add(newOrderRow.ItemArray);
            }

            //ds.Vet.Merge(tempDataTable, false);

            foreach (PrettyPawsVetDataSet.OrderRow OrderRow in ds.Order.Rows)
            {
                if (OrderRow.RowState == DataRowState.Unchanged)
                {
                    OrderRow.Delete();
                }
            }

            OrderTableAdapter.Update(ds.Order);

        }

        public OrderList GetOrder()
        {
            return this;
        }

        public OrderList GetOrder(int inID)
        {
            OrderList temp = new OrderList(inID);
            return temp;
        }

        public void InsertOrder(Order Order)
        {
            this.Add(Order);
        }

        public void DeleteVet(Order delOrder)
        {
            int i = 0;
            int deleteIndex = -1;
            foreach (Order Order in this)
            {
                if (Order.OrderID == delOrder.OrderID)
                {
                    deleteIndex = i;
                }
                i++;
            }
            if (deleteIndex != -1)
            {
                this.RemoveAt(deleteIndex);
            }
        }

        public void UpdateOrder(Order upConsulatncy)
        {
            foreach (Order Order in this)
            {
                if (Order.OrderID == upConsulatncy.OrderID)
                {
                    Order.OrderID = upConsulatncy.OrderID;
                    Order.Place = upConsulatncy.Place;
                    Order.Receive = upConsulatncy.Receive;
                    Order.SupplierID = upConsulatncy.SupplierID;
                }
            }
        }

    }
}
