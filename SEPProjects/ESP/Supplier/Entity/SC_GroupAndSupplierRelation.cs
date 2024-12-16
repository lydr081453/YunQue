using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_GroupAndSupplierRelation
    {
        private int _ID;
        private int _GroupID;
        private int _SupplierID;
        private DateTime _CreatedDate;
        private DateTime _ModifiedDate;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int GroupID
        {
            get { return _GroupID; }
            set { _GroupID = value; }
        }

        public int SupplierID
        {
            get { return _SupplierID; }
            set { _SupplierID = value; }
        }

        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        public DateTime ModifiedDate
        {
            get { return _ModifiedDate; }
            set { _ModifiedDate = value; }
        }
    }
}
