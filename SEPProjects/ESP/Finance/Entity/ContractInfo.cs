using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类ContractInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ContractInfo
    {
        public ContractInfo()
        { }
        #region Model
        private int? _contractid;
        private int _projectid;
        private string _projectcode;
        private string _description;
        private decimal _totalamounts;
        private decimal? _cost;
        private bool _Del;
        public bool? IsDelay { get; set; }
        public bool Del
        {
            get { return _Del; }
            set { _Del = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ContractID
        {
            set { _contractid = value; }
            get { return _contractid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string projectCode
        {
            set { _projectcode = value; }
            get { return _projectcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal TotalAmounts
        {
            set { _totalamounts = value; }
            get { return _totalamounts; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Cost
        {
            set { _cost = value; }
            get { return _cost; }
        }
        #endregion Model

        /// <summary>
        /// 创建人UserId
        /// </summary>
        public int? CreatorUserId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 证据链审核状态。项目初始合同为NULL
        /// </summary>
        public int? Status { get; set; }

        public int? ContractType { get; set; }
    }


    public partial class ContractInfo
    {

        decimal? _fee;//利润
        public decimal? Fee
        {
            get { return _fee; }
            set { _fee = value; }
        }

        string _attachment;//附件
        public string Attachment
        {
            get { return _attachment; }
            set { _attachment = value; }
        }


        string _remark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        int? _version;
        /// <summary>
        /// 当前版本号
        /// </summary>
        public int? Version
        {
            get { return _version; }
            set { _version = value; }
        }

        int? _parentID;
        /// <summary>
        /// 关联父合同ID
        /// </summary>
        public int? ParentID
        {
            get { return _parentID; }
            set { _parentID = value; }
        }

        ContractInfo _parentContract;
        /// <summary>
        /// 关联父合同
        /// </summary>
        public ContractInfo ParentContract
        {
            get { return _parentContract; }
            set { _parentContract = value; }
        }


        bool? _usable;
        /// <summary>
        /// 是否当前正在使用
        /// </summary>
        public bool? Usable
        {
            get { return _usable; }
            set { _usable = value; }
        }



        int? _customerPOID;

        /// <summary>
        /// POID
        /// </summary>
        public int? CustomerPOID
        {
            get { return _customerPOID; }
            set { _customerPOID = value; }
        }


        string _poCode;

        /// <summary>
        /// POCode
        /// </summary>
        public string POCode
        {
            get { return _poCode; }
            set { _poCode = value; }
        }

    }
}