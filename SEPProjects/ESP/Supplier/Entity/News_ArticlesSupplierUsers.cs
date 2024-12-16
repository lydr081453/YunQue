using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Supply.Entity
{
    public class ArticlesSupplierUsers
    {
        private int _id;
        public int ID 
        {
            get { return _id; }
            set { _id = value; } 
        }

        private int _ArticlesID;
        public int ArticlesID
        {
            get { return _ArticlesID; }
            set { _ArticlesID = value; }
        }
        private string _SupplierUserName;
        public string SupplierUserName
        {
            get { return _SupplierUserName; }
            set { _SupplierUserName = value; }
        }
        private string _SupplierUserOffice;
        public string SupplierUserOffice
        {
            get { return _SupplierUserOffice; }
            set { _SupplierUserOffice = value; }
        }
        private string _SupplierUserIntroduction;
        public string SupplierUserIntroduction
        {
            get { return _SupplierUserIntroduction; }
            set { _SupplierUserIntroduction = value; }
        }
    }
}
