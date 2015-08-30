using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;

namespace BusinessTier
{
    class RoleList : System.ComponentModel.BindingList<Role>
    {
                public RoleList()
        {
            RoleTableAdapter.Fill(ds.Role);
            //Create an object for each Role in the dataset
            //and add to list
            foreach (PrettyPawsVetDataSet.RoleRow RoleRow in ds.Role.Rows)
            {
                Role Role = new Role(RoleRow.RoleID,
                    RoleRow.Name);
                this.Add(Role);
            }

        }

        public RoleList(int RoleID)
        {
            RoleTableAdapter.Fill(ds.Role);

            //Create an object for each Role in dataset and add to list
            foreach (PrettyPawsVetDataSet.RoleRow RoleRow in ds.Role.Rows)
            {
                if (RoleID == RoleRow.RoleID)
                {
                    Role Role =
                        new Role((int)RoleRow["RoleID"], 
                            (DateTime)RoleRow["Name"]);
                    this.Add(Role);
                    break;
                }

            }
        }

        public void Save()
        {
            PrettyPawsVetDataSet.RoleDataTable tempDataTable = new PrettyPawsVetDataSet.RoleDataTable();

            foreach (Role Role in this)
            {
                PrettyPawsVetDataSet.RoleRow newRoleRow = ds.Role.NewRoleRow();
                newRoleRow.RoleID = Role.RoleID;
                newRoleRow.Name = Role.Name;

                tempDataTable.Rows.Add(newRoleRow.ItemArray);
            }

            ds.Vet.Merge(tempDataTable, false);

            foreach (PrettyPawsVetDataSet.RoleRow RoleRow in ds.Role.Rows)
            {
                if (RoleRow.RowState == DataRowState.Unchanged)
                {
                    RoleRow.Delete();
                }
            }

            RoleTableAdapter.Update(ds.Role);

        }

        public RoleList GetRole()
        {
            return this;
        }

        public RoleList GetRole(int inID)
        {
            RoleList temp = new RoleList(inID);
            return temp;
        }

        public void InsertRole(Role Role)
        {
            this.Add(Role);
        }

        public void DeleteVet(Role delRole)
        {
            int i = 0;
            int deleteIndex = -1;
            foreach (Role Role in this)
            {
                if (Role.RoleID == delRole.RoleID)
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

        public void UpdateRole(Role upConsulatncy)
        {
            foreach (Role Role in this)
            {
                if (Role.RoleID == upConsulatncy.RoleID)
                {
                    Role.RoleID = upConsulatncy.RoleID;
                    Role.Name = upConsulatncy.Name;
                }
            }
        }
    }
}
