using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_Log
    {
        public SC_Log()
        { }
        #region Model
        private int _id;
        private string _des;
        private int? _loguserid;
        private int? _logusertype;
        private string _ipaddress;
        private DateTime? _creattime;
        private DateTime? _lastupdatetime;
        private int? _type;
        private int? _status;
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
        public string Des
        {
            set { _des = value; }
            get { return _des; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? LogUserId
        {
            set { _loguserid = value; }
            get { return _loguserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? LogUserType
        {
            set { _logusertype = value; }
            get { return _logusertype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IpAddress
        {
            set { _ipaddress = value; }
            get { return _ipaddress; }
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
        #endregion Model

    }
}
