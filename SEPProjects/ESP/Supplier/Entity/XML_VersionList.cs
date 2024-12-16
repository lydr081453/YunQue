using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class XML_VersionList
    {
        public XML_VersionList()
        { }
        #region Model
        private int _id;
        private string _name;
        private string _tablename;
        private string _url;
        private string _content;
        private string _version;
        private string _insertuser;
        private string _inserttime;
        private string _insertip;
        private string _updateuser;
        private string _updatetime;
        private string _updateip;
        private int _classid;
        private string _xml;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TableName
        {
            set { _tablename = value; }
            get { return _tablename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Version
        {
            set { _version = value; }
            get { return _version; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InsertUser
        {
            set { _insertuser = value; }
            get { return _insertuser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InsertTime
        {
            set { _inserttime = value; }
            get { return _inserttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InsertIP
        {
            set { _insertip = value; }
            get { return _insertip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UpdateUser
        {
            set { _updateuser = value; }
            get { return _updateuser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UpdateIP
        {
            set { _updateip = value; }
            get { return _updateip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ClassID
        {
            set { _classid = value; }
            get { return _classid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string XML
        {
            set { _xml = value; }
            get { return _xml; }
        }
        #endregion Model

        private int _bjAuditorId;
        private string _bjAuditor;
        private int _shAuditorId;
        private string _shAuditor;
        private int _gzAuditorId;
        private string _gzAuditor;
        private int _type;
        private int _status;
        private int _state;


 

        public int BJAuditorId
        {
            set { _bjAuditorId = value; }
            get { return _bjAuditorId; }
        }

        public string BJAuditor
        {
            set { _bjAuditor = value; }
            get { return _bjAuditor; }
        }

        public int SHAuditorId
        {
            set { _shAuditorId = value; }
            get { return _shAuditorId; }
        }

        public string SHAuditor
        {
            set { _shAuditor = value; }
            get { return _shAuditor; }
        }

        public int GZAuditorId
        {
            set { _gzAuditorId = value; }
            get { return _gzAuditorId; }
        }

        public string GZAuditor
        {
            set { _gzAuditor = value; }
            get { return _gzAuditor; }
        }

        public int Type
        {
            set { _type = value; }
            get { return _type; }
        }
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }
    }
}
