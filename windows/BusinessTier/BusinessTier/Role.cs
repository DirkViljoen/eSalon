using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessTier
{
    public class Role
    {
        private int mRoleID;
        private string mName;

        public Role()
        {

        }

        public Role(int oID, string oSuppID)
        {
            mRoleID = oID;
            mName = oSuppID;
        }

        public int RoleID
        {
            get { return RoleID; }
            set { RoleID = value; }
        }
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }
    }
}
