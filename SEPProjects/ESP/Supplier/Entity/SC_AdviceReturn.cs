using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_AdviceReturn
    {
        private int _ID;
        private int _AdviceID;
        private string _Body;
        private DateTime _CreatedDate;
        private string _CreatedName;
        private int _CreatedUserID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int AdviceID
        {
            get { return _AdviceID; }
            set { _AdviceID = value; }
        }

        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }

        public string Body
        {
            get { return _Body; }
            set { _Body = value; }
        }

        public string CreatedName
        {
            get { return _CreatedName; }
            set { _CreatedName = value; }
        }

        public int CreatedUserID
        {
            get { return _CreatedUserID; }
            set { _CreatedUserID = value; }
        }
    }
}
