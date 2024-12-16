using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_SupplierType
    {
        public SC_SupplierType()
        { }
        #region Model
        private int _id;
        private int _supplierid;
        private int _typeid;
        private string _createdip;
        private DateTime _creattime;
        private string _lastmodifiedip;
        private DateTime _lastupdatetime;
        private int _type;
        private int _status;
        private int _state;
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
        public int SupplierId
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TypeId
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreatedIP
        {
            set { _createdip = value; }
            get { return _createdip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatTime
        {
            set { _creattime = value; }
            get { return _creattime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LastModifiedIP
        {
            set { _lastmodifiedip = value; }
            get { return _lastmodifiedip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastUpdateTime
        {
            set { _lastupdatetime = value; }
            get { return _lastupdatetime; }
        }
        /// <summary>
        /// 联系人类型
        /// </summary>
        public int Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 联系人状态
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }

        public int TypeLV { get; set; }

        public int state
        {
            set { _state = value; }
            get { return _state; }
        }
        #endregion Model
    }
}
