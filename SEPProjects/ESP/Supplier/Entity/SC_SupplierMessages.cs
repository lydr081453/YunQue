using System;
using System.Data;

namespace ESP.Supplier.Entity
{
    public class SC_SupplierMessages
    {
        #region Model
        private int _id;
        private string _title;
        private string _body;
        private string _fileUrl;
        private DateTime _createdDate;
        private int _createdUserID;
        private int _type;
        private bool _isReaded;
        private bool _isDel;
        private string _productTypeIDs;
        private int _infoID;
        private string _userName;
        private int _userType;
        private bool _isAnSupView;
        private bool _isApporved;
        private bool _isMustRef;
        private bool _isOneToOne;
        private DateTime _endDate;
        private int _subsidiaryUsersID;

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
        public string FileUrl
        {
            set { _fileUrl = value; }
            get { return _fileUrl; }
        }
        public string ProductTypeIDs
        {
            set { _productTypeIDs = value; }
            get { return _productTypeIDs; }
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
        public int InfoID
        {
            set { _infoID = value; }
            get { return _infoID; }
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
        public bool IsAnSupView
        {
            set { _isAnSupView = value; }
            get { return _isAnSupView; }
        }
        public bool IsApporved
        {
            set { _isApporved = value; }
            get { return _isApporved; }
        }
        public bool IsMustRef
        {
            set { _isMustRef = value; }
            get { return _isMustRef; }
        }
        public bool IsOneToOne
        {
            set { _isOneToOne = value; }
            get { return _isOneToOne; }
        }

        public DateTime EndDate
        {
            set { _endDate = value; }
            get { return _endDate; }
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

            Title = r["Title"].ToString();
            Body = r["Body"].ToString();
            FileUrl = r["FileUrl"].ToString();
            ProductTypeIDs = r["ProductTypeIDs"].ToString();
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
            if (null != r["InfoID"] && r["InfoID"].ToString() != "")
            {
                InfoID = int.Parse(r["InfoID"].ToString());
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

            if (null != r["IsAnSupView"] && r["IsAnSupView"].ToString() != "")
            {
                IsAnSupView = bool.Parse(r["IsAnSupView"].ToString());
            }
            if (null != r["IsApporved"] && r["IsApporved"].ToString() != "")
            {
                IsApporved = bool.Parse(r["IsApporved"].ToString());
            }
            if (null != r["IsMustRef"] && r["IsMustRef"].ToString() != "")
            {
                IsApporved = bool.Parse(r["IsMustRef"].ToString());
            }
            if (null != r["IsOneToOne"] && r["IsOneToOne"].ToString() != "")
            {
                IsApporved = bool.Parse(r["IsOneToOne"].ToString());
            }
            if (null != r["EndDate"] && r["EndDate"].ToString() != "")
            {
                EndDate = Convert.ToDateTime(r["EndDate"]);
            }
            if (null != r["SubsidiaryUsersID"] && r["SubsidiaryUsersID"].ToString() != "")
            {
                SubsidiaryUsersID = int.Parse(r["SubsidiaryUsersID"].ToString());
            }
        }
    }
}
