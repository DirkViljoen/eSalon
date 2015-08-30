using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;

namespace BusinessTier
{
    class SupplierList : System.ComponentModel.BindingList<Supplier>
    {
        public SupplierList()
        {
           // SupplierTableAdapter.Fill(ds.Supplier);
            //Create an object for each Supplier in the dataset
            //and add to list
            /*foreach (PrettyPawsVetDataSet.SupplierRow SupplierRow in ds.Supplier.Rows)
            {
                Supplier Supplier = new Supplier(SupplierRow.SupplierID,
                    SupplierRow.OrderLID,
                    SupplierRow.Quantity,
                    SupplierRow.StockID,
                    SupplierRow.OrderID);
                this.Add(Supplier);
            }*/

        }

        public SupplierList(int SupplierID)
        {
           // SupplierTableAdapter.Fill(ds.Supplier);

            //Create an object for each Supplier in dataset and add to list
            /*foreach (PrettyPawsVetDataSet.SupplierRow SupplierRow in ds.Supplier.Rows)
            {
                if (SupplierID == SupplierRow.SupplierID)
                {
                    Supplier Supplier =
                        new Supplier((int)SupplierRow["SupplierID"], 
                            (DateTime)SupplierRow["SupplierDate"], 
                            (int)SupplierRow["VetID"], 
                            (int)SupplierRow["PetID"], 
                            (int)SupplierRow["RoomID"]);
                    this.Add(Supplier);
                    break;
                }

            }*/
        }

        public void Save()
        {
            PrettyPawsVetDataSet.SupplierDataTable tempDataTable = new PrettyPawsVetDataSet.SupplierDataTable();

            foreach (Supplier Supplier in this)
            {
                PrettyPawsVetDataSet.SupplierRow newSupplierRow = ds.Supplier.NewSupplierRow();

                newSupplierRow.SupplierID = Supplier.SupplierID;
                newSupplierRow.Name = Supplier.Name;
                newSupplierRow.Contact = Supplier.Contact;
                newSupplierRow.Email = Supplier.Email;

                tempDataTable.Rows.Add(newSupplierRow.ItemArray);
            }

            ds.Vet.Merge(tempDataTable, false);

            foreach (PrettyPawsVetDataSet.SupplierRow SupplierRow in ds.Supplier.Rows)
            {
                if (SupplierRow.RowState == DataRowState.Unchanged)
                {
                    SupplierRow.Delete();
                }
            }

            SupplierTableAdapter.Update(ds.Supplier);

        }

        public SupplierList GetSupplier()
        {
            return this;
        }

        public SupplierList GetSupplier(int inID)
        {
            SupplierList temp = new SupplierList(inID);
            return temp;
        }

        public void InsertSupplier(Supplier Supplier)
        {
            this.Add(Supplier);
        }

        public void DeleteVet(Supplier delSupplier)
        {
            int i = 0;
            int deleteIndex = -1;
            foreach (Supplier Supplier in this)
            {
                if (Supplier.SupplierID == delSupplier.SupplierID)
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

        public void UpdateSupplier(Supplier upConsulatncy)
        {
            foreach (Supplier Supplier in this)
            {
                if (Supplier.SupplierID == upConsulatncy.SupplierID)
                {
                    Supplier.SupplierID = upConsulatncy.SupplierID;
                    Supplier.Name = upConsulatncy.Name;
                    Supplier.Contact = upConsulatncy.Contact;
                    Supplier.Email = upConsulatncy.Email;
                }
            }
        }
    }
}
