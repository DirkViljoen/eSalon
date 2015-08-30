using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;

namespace BusinessTier
{
    class StockList : System.ComponentModel.BindingList<Stock>
    {
                public StockList()
        {
            StockTableAdapter.Fill(ds.Stock);
            //Create an object for each Stock in the dataset
            //and add to list
            foreach (PrettyPawsVetDataSet.StockRow StockRow in ds.Stock.Rows)
            {
                Stock Stock = new Stock(
                    StockRow.StockID,
                    StockRow.Brand,
                    StockRow.Product,
                    StockRow.Price,
                    StockRow.Size,
                    StockRow.Active,
                    StockRow.Quantity,
                    StockRow.Barcode,
                    StockRow.CategoryID,
                    StockRow.SupplierID);

                this.Add(Stock);
            }

        }

        public StockList(int StockID)
        {
            StockTableAdapter.Fill(ds.Stock);

            //Create an object for each Stock in dataset and add to list
            foreach (PrettyPawsVetDataSet.StockRow StockRow in ds.Stock.Rows)
            {
                if (StockID == StockRow.StockID)
                {
                    Stock Stock =
                        new Stock((int)StockRow["StockID"], 
                            (string)StockRow["Brand"], 
                            (string)StockRow["Product"], 
                            (double)StockRow["Price"],
                            (int)StockRow["Size"],
                            (bool)StockRow["Active"],
                            (int)StockRow["Quantity"],
                            (string)StockRow["Barcode"],
                            (int)StockRow["CategoryID"],
                            (int)StockRow["SupplierID"]);
                    this.Add(Stock);
                    break;
                }

            }
        }

        public void Save()
        {
            //PrettyPawsVetDataSet.StockDataTable tempDataTable = new PrettyPawsVetDataSet.StockDataTable();

            foreach (Stock Stock in this)
            {
                PrettyPawsVetDataSet.StockRow newStockRow = ds.Stock.NewStockRow();

                newStockRow.StockID = Stock.StockID;
                newStockRow.Brand = Stock.Brand;
                newStockRow.Product = Stock.Product;
                newStockRow.Price = Stock.Price;
                newStockRow.Size = Stock.Size;
                newStockRow.Active = Stock.Active;
                newStockRow.Quantity = Stock.Quantity;
                newStockRow.Barcode = Stock.Barcode;
                newStockRow.CategoryID = Stock.CategoryID;
                newStockRow.SupplierID = Stock.SupplierID;

                tempDataTable.Rows.Add(newStockRow.ItemArray);
            }

            ds.Supplier.Merge(tempDataTable, false);

            foreach (PrettyPawsVetDataSet.StockRow StockRow in ds.Stock.Rows)
            {
                if (StockRow.RowState == DataRowState.Unchanged)
                {
                    StockRow.Delete();
                }
            }

            StockTableAdapter.Update(ds.Stock);

        }

        public StockList GetStock()
        {
            return this;
        }

        public StockList GetStock(int inID)
        {
            StockList temp = new StockList(inID);
            return temp;
        }

        public void InsertStock(Stock Stock)
        {
            this.Add(Stock);
        }

        public void DeleteVet(Stock delStock)
        {
            int i = 0;
            int deleteIndex = -1;
            foreach (Stock Stock in this)
            {
                if (Stock.StockID == delStock.SupplierID)
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

        public void UpdateStock(Stock upConsulatncy)
        {
            foreach (Stock Stock in this)
            {
                if (Stock.StockID == upConsulatncy.StockID)
                {
                    Stock.StockID = upConsulatncy.StockID;
                    Stock.Brand = upConsulatncy.Brand;
                    Stock.Product = upConsulatncy.Product;
                    Stock.Price = upConsulatncy.Price;
                    Stock.Size = upConsulatncy.Size;
                    Stock.Active = upConsulatncy.Active;
                    Stock.Quantity = upConsulatncy.Quantity;
                    Stock.Barcode = upConsulatncy.Barcode;
                    Stock.CategoryID = upConsulatncy.CategoryID;
                    Stock.SupplierID = upConsulatncy.SupplierID;
                }
            }
        }
    }
}
