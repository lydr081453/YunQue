using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ESP.Purchase.Entity
{
    public class OrderMsg
    {
        #region Model
        private int _id;
        private int _generalId;
        private int _orderId;
        private string _msgId;
        private string _msgReturnId;
        private DateTime _creatTime;
        private int _creatUserId;
        private DateTime _updateTime;
        private int _updateUserId;

        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        public int GeneralId
        {
            set { _generalId = value; }
            get { return _generalId; }
        }
        public int OrderId
        {
            set { _orderId = value; }
            get { return _orderId; }
        }
        public string MsgId
        {
            set { _msgId = value; }
            get { return _msgId; }
        }
        public string MsgReturnId
        {
            set { _msgReturnId = value; }
            get { return _msgReturnId; }
        }

        public DateTime CreatTime
        {
            set { _creatTime = value; }
            get { return _creatTime; }
        }
        public int CreatUserId
        {
            set { _creatUserId = value; }
            get { return _creatUserId; }
        }
        public DateTime UpdateTime
        {
            set { _updateTime = value; }
            get { return _updateTime; }
        }
        public int UpdateUserId
        {
            set { _updateUserId = value; }
            get { return _updateUserId; }
        }
        #endregion Model

        public void PopupData(IDataReader r)
        {
            if (null != r["id"] && r["id"].ToString() != "")
            {
                ID = int.Parse(r["id"].ToString());
            }
            if (null != r["GeneralId"] && r["GeneralId"].ToString() != "")
            {
                GeneralId = int.Parse(r["GeneralId"].ToString());
            }
            if (null != r["OrderId"] && r["OrderId"].ToString() != "")
            {
                OrderId = int.Parse(r["OrderId"].ToString());
            }

            MsgId = r["MsgId"].ToString();
            MsgReturnId = r["MsgReturnId"].ToString();

            if (null != r["CreatTime"] && r["CreatTime"].ToString() != "")
            {
                CreatTime = Convert.ToDateTime(r["CreatTime"]);
            }
            if (null != r["CreatUserId"] && r["CreatUserId"].ToString() != "")
            {
                CreatUserId = int.Parse(r["CreatUserId"].ToString());
            }
            if (null != r["UpdateTime"] && r["UpdateTime"].ToString() != "")
            {
                UpdateTime = Convert.ToDateTime(r["UpdateTime"]);
            }
            if (null != r["UpdateUserId"] && r["UpdateUserId"].ToString() != "")
            {
                UpdateUserId = int.Parse(r["UpdateUserId"].ToString());
            }
        }
    }
}
