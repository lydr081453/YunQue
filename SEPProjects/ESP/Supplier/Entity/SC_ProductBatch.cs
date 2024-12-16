using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_ProductBatch
    {
        public SC_ProductBatch()
        { }
        #region Model
        private int _batchid;
        private string _batchname;
        private string _batchdes;
        private int? _supplierid;
        private int? _typeid;
        private DateTime? _begintime;
        private DateTime? _endtime;
        private DateTime? _createdate;
        private string _createip;
        private DateTime? _lastupdatedate;
        private string _lastupdateip;
        private int? _status;
        private string _remark;
        /// <summary>
        /// 
        /// </summary>
        public int batchId
        {
            set { _batchid = value; }
            get { return _batchid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string batchName
        {
            set { _batchname = value; }
            get { return _batchname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string batchDes
        {
            set { _batchdes = value; }
            get { return _batchdes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? supplierId
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? typeId
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? beginTime
        {
            set { _begintime = value; }
            get { return _begintime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? endTime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? createDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string createIp
        {
            set { _createip = value; }
            get { return _createip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? lastUpdateDate
        {
            set { _lastupdatedate = value; }
            get { return _lastupdatedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string lastUpdateIp
        {
            set { _lastupdateip = value; }
            get { return _lastupdateip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model
    }
}
