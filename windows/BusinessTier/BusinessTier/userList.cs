using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;

namespace BusinessTier
{
    public class userList : System.ComponentModel.BindingList<User>
    {
        public userList()
        {
            UserTableAdapter.Fill(ds.User);
            //Create an object for each User in the dataset
            //and add to list
            foreach (PrettyPawsVetDataSet.UserRow UserRow in ds.User.Rows)
            {
                User User = 
                    new User(   UserRow.UserID,
                                UserRow.Password ,
                                UserRow.RoleID,
                                UserRow.EmployeeID);
                this.Add(User);
            }

        }

        public userList(int UserID)
        {
            UserTableAdapter.Fill(ds.User);

            //Create an object for each User in dataset and add to list
            foreach (PrettyPawsVetDataSet.UserRow UserRow in ds.User.Rows)
            {
                if (UserID == UserRow.UserID)
                {
                    User User =
                        new User(   (int)UserRow["UserID"], 
                                    (string)UserRow["Password"], 
                                    (int)UserRow["RoleID"], 
                                    (int)UserRow["EmployeeID"];
                    this.Add(User);
                    break;
                }

            }
        }

        public void Save()
        {
            PrettyPawsVetDataSet.UserDataTable tempDataTable = new PrettyPawsVetDataSet.UserDataTable();

            foreach (User User in this)
            {
                PrettyPawsVetDataSet.UserRow newUserRow = ds.User.NewUserRow();

                newUserRow.UserID = User.UserID;
                newUserRow.Password = User.Password;
                newUserRow.EmployeeIDe = User.EmployeeIDe;
                newUserRow.RoleID = User.RoleID;

                tempDataTable.Rows.Add(newUserRow.ItemArray);
            }

            ds.Role.Merge(tempDataTable, false);

            foreach (PrettyPawsVetDataSet.UserRow UserRow in ds.User.Rows)
            {
                if (UserRow.RowState == DataRowState.Unchanged)
                {
                    UserRow.Delete();
                }
            }

            ds.Employee.Merge(tempDataTable, false);

            foreach (PrettyPawsVetDataSet.UserRow UserRow in ds.User.Rows)
            {
                if (UserRow.RowState == DataRowState.Unchanged)
                {
                    UserRow.Delete();
                }
            }

            UserTableAdapter.Update(ds.User);

        }

        public userList GetUser()
        {
            return this;
        }

        public userList GetUser(int inID)
        {
            userList temp = new userList(inID);
            return temp;
        }

        public void InsertUser(User User)
        {
            this.Add(User);
        }

        public void DeleteVet(User delUser)
        {
            int i = 0;
            int deleteIndex = -1;
            foreach (User User in this)
            {
                if (User.UserID == delUser.EmployeeIDe)
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

        public void UpdateUser(User upConsulatncy)
        {
            foreach (User User in this)
            {
                if (User.UserID == upConsulatncy.UserID)
                {
                    User.UserID = upConsulatncy.UserID;
                    User.Password = upConsulatncy.Password;
                    User.EmployeeIDe = upConsulatncy.EmployeeIDe;
                    User.RoleID = upConsulatncy.RoleID;
                }
            }
        }
    }
}
