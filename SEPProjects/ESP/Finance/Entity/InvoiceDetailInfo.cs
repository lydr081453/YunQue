using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类InvoiceDetailInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class InvoiceDetailInfo
    {
        public InvoiceDetailInfo()
        { }
        #region Model
        private int _invoicedetailid;
        private int? _paymentid;
        private string _paymentcode;
        private int? _invoiceid;
        private string _invoiceno;
        private decimal? _amounts;
        private decimal? _usddiffer;
        private int? _responseuserid;
        private string _responseusername;
        private string _responsecode;
        private string _responseemployeename;
        private int? _projectid;
        private string _projectcode;
        private DateTime? _createdate;
        private string _description;
        private string _remark;
        /// <summary>
        /// 
        /// </summary>
        public int InvoiceDetailID
        {
            set { _invoicedetailid = value; }
            get { return _invoicedetailid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? PaymentID
        {
            set { _paymentid = value; }
            get { return _paymentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PaymentCode
        {
            set { _paymentcode = value; }
            get { return _paymentcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? InvoiceID
        {
            set { _invoiceid = value; }
            get { return _invoiceid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string InvoiceNo
        {
            set { _invoiceno = value; }
            get { return _invoiceno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Amounts
        {
            set { _amounts = value; }
            get { return _amounts; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? USDDiffer
        {
            set { _usddiffer = value; }
            get { return _usddiffer; }
        }
        /// <summary>
        /// 负责人ID自增长
        /// </summary>
        public int? ResponseUserID
        {
            set { _responseuserid = value; }
            get { return _responseuserid; }
        }
        /// <summary>
        /// 负责人登录帐号
        /// </summary>
        public string ResponseUserName
        {
            set { _responseusername = value; }
            get { return _responseusername; }
        }
        /// <summary>
        /// 负责人内部编号
        /// </summary>
        public string ResponseCode
        {
            set { _responsecode = value; }
            get { return _responsecode; }
        }
        /// <summary>
        /// 负责人真实姓名
        /// </summary>
        public string ResponseEmployeeName
        {
            set { _responseemployeename = value; }
            get { return _responseemployeename; }
        }
        /// <summary>
        /// 项目ID
        /// </summary>
        public int? ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// 项目号
        /// </summary>
        public string ProjectCode
        {
            set { _projectcode = value; }
            get { return _projectcode; }
        }
        /// <summary>
        /// 开票日期
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

    }
}

