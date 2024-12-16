using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_VendeeMessageCanView
    {
        #region Model
        private int _id;
        private int _messageID;
        private int _vendeeID;
        private DateTime _createdDate;
        private bool _isUserselfSelected;

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

        public int VendeeID
        {
            set { _vendeeID = value; }
            get { return _vendeeID; }
        }

        public DateTime CreatedDate
        {
            set { _createdDate = value; }
            get { return _createdDate; }
        }

        public bool IsUserselfSelected
        {
            set { _isUserselfSelected = value; }
            get { return _isUserselfSelected; }
        }
        #endregion Model
    }
}
