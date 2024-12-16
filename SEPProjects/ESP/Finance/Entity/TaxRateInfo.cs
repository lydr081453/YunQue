using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类TaxRateInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TaxRateInfo
    {
        public TaxRateInfo()
        { }
        #region Model
        private int _taxrateid;
        private string _biztypename;
        private int? _branchid;
        private string _branchcode;
        private string _branchname;
        private int _customerid;
        private string _customername;
        private string _customershortname;
        private decimal? _taxrate;
        private int? _biztypeid;


        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? InvoiceRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TaxRateID
        {
            set { _taxrateid = value; }
            get { return _taxrateid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BizTypeName
        {
            set { _biztypename = value; }
            get { return _biztypename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? BranchID
        {
            set { _branchid = value; }
            get { return _branchid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BranchCode
        {
            set { _branchcode = value; }
            get { return _branchcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BranchName
        {
            set { _branchname = value; }
            get { return _branchname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CustomerID
        {
            set { _customerid = value; }
            get { return _customerid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CustomerName
        {
            set { _customername = value; }
            get { return _customername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CustomerShortName
        {
            set { _customershortname = value; }
            get { return _customershortname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TaxRate
        {
            set { _taxrate = value; }
            get { return _taxrate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? BizTypeID
        {
            set { _biztypeid = value; }
            get { return _biztypeid; }
        }


        string _remark;

        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        public decimal VATParam1 { get; set; }
        public decimal VATParam2 { get; set; }
        public decimal VATParam3 { get; set; }
        public decimal VATParam4 { get; set; }
        public decimal VATParam5 { get; set; }
        #endregion Model

    }
}