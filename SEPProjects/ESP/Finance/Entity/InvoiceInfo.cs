using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类InvoiceInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class InvoiceInfo
    {
        public InvoiceInfo()
        { }
        #region Model
        private int _invoiceid;
        private string _invoicecode;
        private DateTime? _createdate;
        private decimal? _invoiceamounts;
        private decimal? _usddiffer;
        private int? _customerid;
        private string _customershortname;
        private string _customername;
        private string _customertitle;
        private int? _invoicestatus;
        private int? _branchid;
        private string _branchcode;
        private string _branchname;
        private string _remark;
        private int? _creatorid;
        private string _creatorusercode;
        private string _creatorusername;
        private string _creatoremployeename;
        private int? _receiveruserid;
        private string _receiverusercode;
        private string _receiverusername;
        private string _receiveremployeename;


        public int ProjectId { get; set; }
        public string ProjectCode { get; set; }

        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? CancelDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int InvoiceID
        {
            set { _invoiceid = value; }
            get { return _invoiceid; }
        }
        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceCode
        {
            set { _invoicecode = value; }
            get { return _invoicecode; }
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
        /// 发票金额
        /// </summary>
        public decimal? InvoiceAmounts
        {
            set { _invoiceamounts = value; }
            get { return _invoiceamounts; }
        }
        /// <summary>
        /// 美元差额
        /// </summary>
        public decimal? USDDiffer
        {
            set { _usddiffer = value; }
            get { return _usddiffer; }
        }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int? CustomerID
        {
            set { _customerid = value; }
            get { return _customerid; }
        }
        /// <summary>
        /// 客户缩写
        /// </summary>
        public string CustomerShortName
        {
            set { _customershortname = value; }
            get { return _customershortname; }
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        {
            set { _customername = value; }
            get { return _customername; }
        }
        /// <summary>
        /// 发票抬头
        /// </summary>
        public string CustomerTitle
        {
            set { _customertitle = value; }
            get { return _customertitle; }
        }
        /// <summary>
        /// 发票状态：正常/重开/作废
        /// </summary>
        public int? InvoiceStatus
        {
            set { _invoicestatus = value; }
            get { return _invoicestatus; }
        }
        /// <summary>
        /// 分公司ID
        /// </summary>
        public int? BranchID
        {
            set { _branchid = value; }
            get { return _branchid; }
        }
        /// <summary>
        /// 分公司代码
        /// </summary>
        public string BranchCode
        {
            set { _branchcode = value; }
            get { return _branchcode; }
        }
        /// <summary>
        /// 分公司名称
        /// </summary>
        public string BranchName
        {
            set { _branchname = value; }
            get { return _branchname; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CreatorID
        {
            set { _creatorid = value; }
            get { return _creatorid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreatorUserCode
        {
            set { _creatorusercode = value; }
            get { return _creatorusercode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreatorUserName
        {
            set { _creatorusername = value; }
            get { return _creatorusername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreatorEmployeeName
        {
            set { _creatoremployeename = value; }
            get { return _creatoremployeename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ReceiverUserID
        {
            set { _receiveruserid = value; }
            get { return _receiveruserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReceiverUserCode
        {
            set { _receiverusercode = value; }
            get { return _receiverusercode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReceiverUserName
        {
            set { _receiverusername = value; }
            get { return _receiverusername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReceiverEmployeeName
        {
            set { _receiveremployeename = value; }
            get { return _receiveremployeename; }
        }
        #endregion Model

    }
}

