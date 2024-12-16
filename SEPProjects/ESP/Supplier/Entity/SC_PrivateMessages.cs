using System;
using System.Data;

namespace ESP.Supplier.Entity
{
    public class SC_PrivateMessages
    {
        #region Model
        private int _id;
        private string _title;
        private string _body;
        //private string _fileUrl;
        private DateTime _createdDate;
        private int _createdUserID;
        private bool _isRead;
        private bool _isDel;
        private bool _isApprove;

        private int _createdUserType;
        private int _sendToUserID;
        private int _sendToUserType;
        private string _createdUserName;
        private string _sendToUserName;

        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        public string Body
        {
            set { _body = value; }
            get { return _body; }
        }
        public string CreatedUserName
        {
            set { _createdUserName = value; }
            get { return _createdUserName; }
        }
        public string SendToUserName
        {
            set { _sendToUserName = value; }
            get { return _sendToUserName; }
        }
        public int CreatedUserType
        {
            set { _createdUserType = value; }
            get { return _createdUserType; }
        }

        public DateTime CreatedDate
        {
            set { _createdDate = value; }
            get { return _createdDate; }
        }
        public int CreatedUserID
        {
            set { _createdUserID = value; }
            get { return _createdUserID; }
        }
        public int SendToUserID
        {
            set { _sendToUserID = value; }
            get { return _sendToUserID; }
        }
        public int SendToUserType
        {
            set { _sendToUserType = value; }
            get { return _sendToUserType; }
        }
        public bool IsRead
        {
            set { _isRead = value; }
            get { return _isRead; }
        }
        public bool IsDel
        {
            set { _isDel = value; }
            get { return _isDel; }
        }
        public bool IsApprove
        {
            set { _isApprove = value; }
            get { return _isApprove; }
        }
        #endregion Model

        public void PopupData(IDataReader r)
        {            
            if (null != r["id"] && r["id"].ToString() != "")
            {
                ID = int.Parse(r["id"].ToString());
            }

            Title = r["Title"].ToString();
            Body = r["Body"].ToString();
            CreatedUserName = r["CreatedUserName"].ToString();
            SendToUserName = r["SendToUserName"].ToString();

            if (null != r["CreatedDate"] && r["CreatedDate"].ToString() != "")
            {
                CreatedDate = Convert.ToDateTime(r["CreatedDate"]);
            }
            if (null != r["CreatedUserID"] && r["CreatedUserID"].ToString() != "")
            {
                CreatedUserID = int.Parse(r["CreatedUserID"].ToString());
            }
            if (null != r["SendToUserID"] && r["SendToUserID"].ToString() != "")
            {
                SendToUserID = int.Parse(r["SendToUserID"].ToString());
            }
            if (null != r["SendToUserType"] && r["SendToUserType"].ToString() != "")
            {
                SendToUserType = int.Parse(r["SendToUserType"].ToString());
            }
            if (null != r["CreatedUserType"] && r["CreatedUserType"].ToString() != "")
            {
                CreatedUserType = int.Parse(r["CreatedUserType"].ToString());
            }
            if (null != r["IsRead"] && r["IsRead"].ToString() != "")
            {
                IsRead = bool.Parse(r["IsRead"].ToString());
            }
            if (null != r["IsDel"] && r["IsDel"].ToString() != "")
            {
                IsDel = bool.Parse(r["IsDel"].ToString());
            }
            if (null != r["IsApprove"] && r["IsApprove"].ToString() != "")
            {
                IsApprove = bool.Parse(r["IsApprove"].ToString());
            }
        }
    }
}
