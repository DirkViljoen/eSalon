using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessTier
{
    public class Category
    {
        private int mCategoryID;
        private string mName;

        public Category()
        {

        }

        public Category(int oID, string name)
        {
            mCategoryID = oID;
            mName = name;
        }

        public int CategoryID
        {
            get { return CategoryID; }
            set { CategoryID = value; }
        }
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }
    }
}
