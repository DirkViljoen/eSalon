using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;

namespace BusinessTier
{
    class CategoryList : System.ComponentModel.BindingList<Category>
    {
        public CategoryList()
        {
            CategoryTableAdapter.Fill(ds.Category);
            //Create an object for each Category in the dataset
            //and add to list
            foreach (PrettyPawsVetDataSet.CategoryRow CategoryRow in ds.Category.Rows)
            {
                Category Category = new Category(CategoryRow.CategoryID,
                    CategoryRow.Name);
                this.Add(Category);
            }

        }

        public CategoryList(int CategoryID)
        {
            CategoryTableAdapter.Fill(ds.Category);

            //Create an object for each Category in dataset and add to list
            foreach (PrettyPawsVetDataSet.CategoryRow CategoryRow in ds.Category.Rows)
            {
                if (CategoryID == CategoryRow.CategoryID)
                {
                    Category Category =
                        new Category((int)CategoryRow["CategoryID"], 
                            (string)CategoryRow["Name"]);
                    this.Add(Category);
                    break;
                }

            }
        }

        public void Save()
        {
            PrettyPawsVetDataSet.CategoryDataTable tempDataTable = new PrettyPawsVetDataSet.CategoryDataTable();

            foreach (Category Category in this)
            {
                PrettyPawsVetDataSet.CategoryRow newCategoryRow = ds.Category.NewCategoryRow();
                newCategoryRow.CategoryID = Category.CategoryID;
                newCategoryRow.Name = Category.Name;

                tempDataTable.Rows.Add(newCategoryRow.ItemArray);
            }

            ds.Vet.Merge(tempDataTable, false);

            foreach (PrettyPawsVetDataSet.CategoryRow CategoryRow in ds.Category.Rows)
            {
                if (CategoryRow.RowState == DataRowState.Unchanged)
                {
                    CategoryRow.Delete();
                }
            }

            CategoryTableAdapter.Update(ds.Category);

        }

        public CategoryList GetCategory()
        {
            return this;
        }

        public CategoryList GetCategory(int inID)
        {
            CategoryList temp = new CategoryList(inID);
            return temp;
        }

        public void InsertCategory(Category Category)
        {
            this.Add(Category);
        }

        public void DeleteVet(Category delCategory)
        {
            int i = 0;
            int deleteIndex = -1;
            foreach (Category Category in this)
            {
                if (Category.CategoryID == delCategory.CategoryID)
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

        public void UpdateCategory(Category upConsulatncy)
        {
            foreach (Category Category in this)
            {
                if (Category.CategoryID == upConsulatncy.CategoryID)
                {
                    Category.CategoryID = upConsulatncy.CategoryID;
                    Category.Name = upConsulatncy.Name;
                }
            }
        }
    }
}
