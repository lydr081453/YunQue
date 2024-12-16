using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_ReturnMessageWillRead
    {
        #region Model
        private int _id;
        private int _returnMsgID;
        private int _userID;
        private int _msgUserType;
        private bool _isWillRead;
        private DateTime _createdDate;
        private string _createdUserName;
        

        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        public int ReturnMsgID
        {
            set { _returnMsgID = value; }
            get { return _returnMsgID; }
        }

        public int UserID
        {
            set { _userID = value; }
            get { return _userID; }
        }
        public int MsgUserType
        {
            set { _msgUserType = value; }
            get { return _msgUserType; }
        }
        public DateTime CreatedDate
        {
            set { _createdDate = value; }
            get { return _createdDate; }
        }

        public bool IsWillRead
        {
            set { _isWillRead = value; }
            get { return _isWillRead; }
        }
        public string CreatedUserName
        {
            set { _createdUserName = value; }
            get { return _createdUserName; }
        }
        #endregion Model
    }
}
