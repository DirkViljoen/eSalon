using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;

namespace BusinessTier
{
    public class OrderLineList : System.ComponentModel.BindingList<OrderLine>
    {
        public OrderLineList()
        {
            OrderLineTableAdapter.Fill(ds.OrderLine);
            //Create an object for each OrderLine in the dataset
            //and add to list
            foreach (PrettyPawsVetDataSet.OrderLineRow OrderLineRow in ds.OrderLine.Rows)
            {
                OrderLine OrderLine = new OrderLine(OrderLineRow.OrderLineID,
                    OrderLineRow.Quantity,
                    OrderLineRow.StockID,
                    OrderLineRow.OrderID);
                this.Add(OrderLine);
            }

        }

        public OrderLineList(int OrderLineID)
        {
            OrderLineTableAdapter.Fill(ds.OrderLine);

            //Create an object for each OrderLine in dataset and add to list
            foreach (PrettyPawsVetDataSet.OrderLineRow OrderLineRow in ds.OrderLine.Rows)
            {
                if (OrderLineID == OrderLineRow.OrderLineID)
                {
                    OrderLine OrderLine =
                        new OrderLine((int)OrderLineRow["OrderLineID"],
                            (int)OrderLineRow["Quantity"],
                            (int)OrderLineRow["StockID"],
                            (int)OrderLineRow["OrderID"]);
                    this.Add(OrderLine);
                    break;
                }

            }
        }

        public void Save()
        {
            PrettyPawsVetDataSet.OrderLineDataTable tempDataTable = new PrettyPawsVetDataSet.OrderLineDataTable();

            foreach (OrderLine OrderLine in this)
            {
                PrettyPawsVetDataSet.OrderLineRow newOrderLineRow = ds.OrderLine.NewOrderLineRow();
                
                newOrderLineRow.OrderLineID = OrderLine.OrderLineID;
                newOrderLineRow.Quantity = OrderLine.Quantity;
                newOrderLineRow.StockID = OrderLine.StockID;
                newOrderLineRow.OrderID = OrderLine.OrderID;

                tempDataTable.Rows.Add(newOrderLineRow.ItemArray);
            }

            ds.Vet.Merge(tempDataTable, false);

            foreach (PrettyPawsVetDataSet.OrderLineRow OrderLineRow in ds.OrderLine.Rows)
            {
                if (OrderLineRow.RowState == DataRowState.Unchanged)
                {
                    OrderLineRow.Delete();
                }
            }

            OrderLineTableAdapter.Update(ds.OrderLine);

        }

        public OrderLineList GetOrderLine()
        {
            return this;
        }

        public OrderLineList GetOrderLine(int inID)
        {
            OrderLineList temp = new OrderLineList(inID);
            return temp;
        }

        public void InsertOrderLine(OrderLine OrderLine)
        {
            this.Add(OrderLine);
        }

        public void DeleteVet(OrderLine delOrderLine)
        {
            int i = 0;
            int deleteIndex = -1;
            foreach (OrderLine OrderLine in this)
            {
                if (OrderLine.OrderLineID == delOrderLine.OrderLineID)
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

        public void UpdateOrderLine(OrderLine upConsulatncy)
        {
            foreach (OrderLine OrderLine in this)
            {
                if (OrderLine.OrderLineID == upConsulatncy.OrderLineID)
                {
                    OrderLine.OrderLineID = upConsulatncy.OrderLineID;
                    OrderLine.Quantity = upConsulatncy.Quantity;
                    OrderLine.StockID = upConsulatncy.StockID;
                    OrderLine.OrderID = upConsulatncy.OrderID;
                }
            }
        }
    }
}
