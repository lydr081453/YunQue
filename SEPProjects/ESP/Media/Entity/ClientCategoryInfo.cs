using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Media.Entity
{
   public class ClientCategoryInfo
    {
        private int categoryID;

        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }
        protected string cname;

        public string CategoryName
        {
            get { return cname; }
            set { cname = value; }
        }
        private int sortid;

        public int SortID
        {
            get { return sortid; }
            set { sortid = value; }
        }
        public ClientCategoryInfo()
       {
           categoryID = 0;
           cname = string.Empty;
           sortid = 0;
       }


    }
}
