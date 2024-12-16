using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
    public class ProjectCodeChangedInfo
    {
        private int _id;
        private int _dataType;
        private int _dataId;
        private int _changedUserId;
        private string _changedUserName;
        private DateTime _changedTime;
        private string _oldProjectCode;

        public int Id {
            get { return _id; }
            set { _id = value; }
        }
        public int DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }
        public int DataId
        {
            get { return _dataId; }
            set { _dataId = value; }
        }
        public int ChangedUserId
        {
            get { return _changedUserId; }
            set { _changedUserId = value; }
        }
        public string ChangedUserName
        {
            get { return _changedUserName; }
            set { _changedUserName = value; }
        }
        public DateTime ChangedTime
        {
            get { return _changedTime; }
            set { _changedTime = value; }
        }
        public string OldProjectCode
        {
            get { return _oldProjectCode; }
            set { _oldProjectCode = value; }
        }
    }
}
