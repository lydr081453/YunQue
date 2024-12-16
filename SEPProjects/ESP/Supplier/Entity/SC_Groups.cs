using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_Groups
    {
        private int _ID;
        private string _GroupNameCN;
        private string _GroupNameEN;
        private string _Phone;
        private string _Address;
        private DateTime _CreatedDate;
        private DateTime _ModifiedDate;
        private int _Status;
        private bool _IsApproved;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string GroupNameCN
        {
            get { return _GroupNameCN; }
            set { _GroupNameCN = value; }
        }

        public string GroupNameEN
        {
            get { return _GroupNameEN; }
            set { _GroupNameEN = value; }
        }

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
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

        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public bool IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
        }
    }
}
