using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_Type
    {
        public SC_Type()
        { }
        #region Model
        private int _id;
        private string _typename;
        private string _typeshortname;
        private string _path;
        private int? _parentid;
        private int? _typelevel;
        private DateTime? _creattime;
        private DateTime? _lastupdatetime;
        private int? _type;
        private int? _status;
        private int? _bjauditorid;
        private string _bjauditor;
        private int? _shauditorid;
        private string _shauditor;
        private int? _gzauditorid;
        private string _gzauditor;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TypeName
        {
            set { _typename = value; }
            get { return _typename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TypeShortName
        {
            set { _typeshortname = value; }
            get { return _typeshortname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Path
        {
            set { _path = value; }
            get { return _path; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? parentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 物料类别级别
        /// </summary>
        public int? typelevel
        {
            set { _typelevel = value; }
            get { return _typelevel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreatTime
        {
            set { _creattime = value; }
            get { return _creattime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateTime
        {
            set { _lastupdatetime = value; }
            get { return _lastupdatetime; }
        }
        /// <summary>
        /// 联系人类型
        /// </summary>
        public int? Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 联系人状态
        /// </summary>
        public int? Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? BJAuditorId
        {
            set { _bjauditorid = value; }
            get { return _bjauditorid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BJAuditor
        {
            set { _bjauditor = value; }
            get { return _bjauditor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SHAuditorId
        {
            set { _shauditorid = value; }
            get { return _shauditorid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SHAuditor
        {
            set { _shauditor = value; }
            get { return _shauditor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? GZAuditorId
        {
            set { _gzauditorid = value; }
            get { return _gzauditorid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GZAuditor
        {
            set { _gzauditor = value; }
            get { return _gzauditor; }
        }
        #endregion Model

    }
}
