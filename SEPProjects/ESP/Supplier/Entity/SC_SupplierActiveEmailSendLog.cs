using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_SupplierActiveEmailSendLog
    {
        private int _ID;
        private int _SupplierID;
        private DateTime _CreatedDate;
        private int _CreatedID;

        public int SysId { get; set; }
        public string SysName { get; set; }

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
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

        public int CreatedID
        {
            get { return _CreatedID; }
            set { _CreatedID = value; }
        }
    }
}
