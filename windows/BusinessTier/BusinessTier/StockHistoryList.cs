using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;

namespace BusinessTier
{
    public class StockHistoryList : System.ComponentModel.BindingList<StockHistory>
    {
               public StockHistoryList()
        {
            StockHistoryTableAdapter.Fill(ds.StockHistory);
            //Create an object for each StockHistory in the dataset
            //and add to list
            foreach (PrettyPawsVetDataSet.StockHistoryRow StockHistoryRow in ds.StockHistory.Rows)
            {
                StockHistory StockHistory = new StockHistory(StockHistoryRow.StockHistoryID,
                    StockHistoryRow.Price,
                    StockHistoryRow.From,
                    StockHistoryRow.To,
                    StockHistoryRow.StockID);
                this.Add(StockHistory);
            }

        }

        public StockHistoryList(int StockHistoryID)
        {
            StockHistoryTableAdapter.Fill(ds.StockHistory);

            //Create an object for each StockHistory in dataset and add to list
            foreach (PrettyPawsVetDataSet.StockHistoryRow StockHistoryRow in ds.StockHistory.Rows)
            {
                if (StockHistoryID == StockHistoryRow.StockHistoryID)
                {
                    StockHistory StockHistory =
                        new StockHistory((int)StockHistoryRow["StockHistoryID"], 
                            (double)StockHistoryRow["Price"], 
                            (DateTime)StockHistoryRow["From"], 
                            (DateTime)StockHistoryRow["To"], 
                            (int)StockHistoryRow["StockID"]);
                    this.Add(StockHistory);
                    break;
                }

            }
        }

        public void Save()
        {
            //PrettyPawsVetDataSet.StockHistoryDataTable tempDataTable = new PrettyPawsVetDataSet.StockHistoryDataTable();

            foreach (StockHistory StockHistory in this)
            {
                PrettyPawsVetDataSet.StockHistoryRow newStockHistoryRow = ds.StockHistory.NewStockHistoryRow();
                newStockHistoryRow.StockHistoryID = StockHistory.StockHistoryID;
                newStockHistoryRow.Price = StockHistory.Price;
                newStockHistoryRow.From = StockHistory.From;
                newStockHistoryRow.To = StockHistory.To;
                newStockHistoryRow.StockID = StockHistory.StockID;

                tempDataTable.Rows.Add(newStockHistoryRow.ItemArray);
            }

            ds.Stock.Merge(tempDataTable, false);

            foreach (PrettyPawsVetDataSet.StockHistoryRow StockHistoryRow in ds.StockHistory.Rows)
            {
                if (StockHistoryRow.RowState == DataRowState.Unchanged)
                {
                    StockHistoryRow.Delete();
                }
            }

            StockHistoryTableAdapter.Update(ds.StockHistory);

        }

        public StockHistoryList GetStockHistory()
        {
            return this;
        }

        public StockHistoryList GetStockHistory(int inID)
        {
            StockHistoryList temp = new StockHistoryList(inID);
            return temp;
        }

        public void InsertStockHistory(StockHistory StockHistory)
        {
            this.Add(StockHistory);
        }

        public void DeleteVet(StockHistory delStockHistory)
        {
            int i = 0;
            int deleteIndex = -1;
            foreach (StockHistory StockHistory in this)
            {
                if (StockHistory.StockHistoryID == delStockHistory.StockID)
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

        public void UpdateStockHistory(StockHistory upConsulatncy)
        {
            foreach (StockHistory StockHistory in this)
            {
                if (StockHistory.StockHistoryID == upConsulatncy.StockHistoryID)
                {
                    StockHistory.StockHistoryID = upConsulatncy.StockHistoryID;
                    StockHistory.Price = upConsulatncy.Price;
                    StockHistory.From = upConsulatncy.From;
                    StockHistory.To = upConsulatncy.To;
                    StockHistory.StockID = upConsulatncy.StockID;
                }
            }
        }
    }
}
