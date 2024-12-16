using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类F_returnInvoice 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class ReturnInvoiceInfo
    {
        public ReturnInvoiceInfo()
        { }
        #region Model
        private int _invid;
        private int? _status;
        private int? _returnid;
        private string _invoicecode;
        private int? _requestorid;
        private string _requestremark;
        private int? _faid;
        private string _faremark;
        private int? _financeid;
        private string _financeremark;
        /// <summary>
        /// 
        /// </summary>
        public int InvID
        {
            set { _invid = value; }
            get { return _invid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ReturnID
        {
            set { _returnid = value; }
            get { return _returnid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InvoiceCode
        {
            set { _invoicecode = value; }
            get { return _invoicecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RequestorID
        {
            set { _requestorid = value; }
            get { return _requestorid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RequestRemark
        {
            set { _requestremark = value; }
            get { return _requestremark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FAID
        {
            set { _faid = value; }
            get { return _faid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FARemark
        {
            set { _faremark = value; }
            get { return _faremark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FinanceID
        {
            set { _financeid = value; }
            get { return _financeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FinanceRemark
        {
            set { _financeremark = value; }
            get { return _financeremark; }
        }
        #endregion Model

    }
}
