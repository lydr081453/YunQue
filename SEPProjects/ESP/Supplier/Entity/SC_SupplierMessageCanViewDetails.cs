using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_SupplierMessageCanViewDetails
    {
        #region Model
        private int _id;
        private int _messageID;
        private int _supplierUserID;
        private DateTime _createdDate;

        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        public int MessageID
        {
            set { _messageID = value; }
            get { return _messageID; }
        }

        public int SupplierUserID
        {
            set { _supplierUserID = value; }
            get { return _supplierUserID; }
        }

        public DateTime CreatedDate
        {
            set { _createdDate = value; }
            get { return _createdDate; }
        }
        #endregion Model
    }
}
