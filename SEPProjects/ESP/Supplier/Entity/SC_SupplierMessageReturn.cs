using System;
using System.Data;

namespace ESP.Supplier.Entity
{
    public class SC_SupplierMessageReturn
    {
        #region Model
        private int _id;
        private int _suppliserMessageID;
        private string _title;
        private string _body;
        private DateTime _createdDate;
        private int _createdUserID;
        private int _type;
        private bool _isReaded;
        private bool _isDel;
        private string _userName;
        private int _userType;
        private int _refid;
        private string _FileUrl;
        private int _subsidiaryUsersID;

        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        public int SuppliserMessageID
        {
            set { _suppliserMessageID = value; }
            get { return _suppliserMessageID; }
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
        public string UserName
        {
            set { _userName = value; }
            get { return _userName; }
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
        public int Type
        {
            set { _type = value; }
            get { return _type; }
        }
        public int RefID
        {
            set { _refid = value; }
            get { return _refid; }
        }
        public int UserType
        {
            set { _userType = value; }
            get { return _userType; }
        }
        public bool IsReaded
        {
            set { _isReaded = value; }
            get { return _isReaded; }
        }
        public bool IsDel
        {
            set { _isDel = value; }
            get { return _isDel; }
        }
        public string FileUrl
        {
            set { _FileUrl = value; }
            get { return _FileUrl; }
        }
        public int SubsidiaryUsersID
        {
            set { _subsidiaryUsersID = value; }
            get { return _subsidiaryUsersID; }
        }
        #endregion Model

        public void PopupData(IDataReader r)
        {
            if (null != r["id"] && r["id"].ToString() != "")
            {
                ID = int.Parse(r["id"].ToString());
            }
            if (null != r["SuppliserMessageID"] && r["SuppliserMessageID"].ToString() != "")
            {
                SuppliserMessageID = int.Parse(r["SuppliserMessageID"].ToString());
            }
            if (null != r["RefID"] && r["RefID"].ToString() != "")
            {
                RefID = int.Parse(r["RefID"].ToString());
            }

            Title = r["Title"].ToString();
            Body = r["Body"].ToString();
            UserName = r["UserName"].ToString();

            if (null != r["CreatedDate"] && r["CreatedDate"].ToString() != "")
            {
                CreatedDate = Convert.ToDateTime(r["CreatedDate"]);
            }
            if (null != r["CreatedUserID"] && r["CreatedUserID"].ToString() != "")
            {
                CreatedUserID = int.Parse(r["CreatedUserID"].ToString());
            }
            if (null != r["Type"] && r["Type"].ToString() != "")
            {
                Type = int.Parse(r["Type"].ToString());
            }
            if (null != r["UserType"] && r["UserType"].ToString() != "")
            {
                UserType = int.Parse(r["UserType"].ToString());
            }
            if (null != r["IsReaded"] && r["IsReaded"].ToString() != "")
            {
                IsReaded = bool.Parse(r["IsReaded"].ToString());
            }
            if (null != r["IsDel"] && r["IsDel"].ToString() != "")
            {
                IsDel = bool.Parse(r["IsDel"].ToString());
            }
            FileUrl = r["FileUrl"].ToString();
            if (null != r["SubsidiaryUsersID"] && r["SubsidiaryUsersID"].ToString() != "")
            {
                SubsidiaryUsersID = int.Parse(r["SubsidiaryUsersID"].ToString());
            }
        }
    }
}