using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessTier
{
    public class User
    {
        private int userID;
        private string password;
        private int employeeID;
        private int roleID;
    
        public User()
        {

        }

        public User(int uID, string upass, int uempUD, int uRoleID)
            {
                userID = uID;
                password = upass;
                employeeID = uempUD;
                roleID = uRoleID;
            }

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public int EmployeeIDe
        {
            get { return employeeID; }
            set { employeeID = value; }
        }
        public int RoleID
            {
                get { return roleID; }
                set { roleID = value; }
            }
    
    
    
    }
}
