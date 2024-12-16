using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类ExpenseAccountInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ExpenseAccountDetailInfo
    {
        public ExpenseAccountDetailInfo()
        { }
        #region Model
        private int _id;
        private int? _returnid;
        private DateTime? _expensedate;
        private int? _expensetype;
        private string _expensedesc;
        private decimal? _expensemoney;
        private string _financememo;
        private int? _creater;
        private string _creatername;
        private DateTime? _createtime;
        private int? _phoneyear;
        private int? _phonemonth;
        private int? _mealfeemorning;
        private int? _mealfeenoon;
        private int? _mealfeenight;
        private int? _costDetailID;
        private int? _expenseTypeNumber;

        private string _recipient;
        private string _bankName;
        private string _bankAccountNo;

        private int? _status;
        public string City { get; set; }

        public int BoarderId { get; set; }
        /// <summary>
        /// 机票改签的原明细ID
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 机票状态，是正常/改签/退票
        /// </summary>
        public int TicketStatus { get; set; }
        /// <summary>
        /// 机票申请-出发地
        /// </summary>
        public string TicketSource { get; set; }
        /// <summary>
        /// 机票申请-目的地
        /// </summary>
        public string TicketDestination { get; set; }
        /// <summary>
        /// 机票申请-是否往返
        /// </summary>
        public bool? IsRoundTrip { get; set; }
        /// <summary>
        /// 机票申请-出发日期
        /// </summary>
        public DateTime? GoTime { get; set; }
        /// <summary>
        /// 机票申请-返回日期
        /// </summary>
        public DateTime? BackTime { get; set; }
        /// <summary>
        /// 机票申请-登机人
        /// </summary>
        public string Boarder { get; set; }
        /// <summary>
        /// 机票申请-登机人身份证号
        /// </summary>
        public string BoarderIDCard { get; set; }
        /// <summary>
        /// 往返标记
        /// </summary>
        public int TripType { get; set; }
        /// <summary>
        /// 航班号(往)
        /// </summary>
        public string GoAirNo { get; set; }
        /// <summary>
        /// 航班号(返)
        /// </summary>
        public string BackAirNo { get; set; }
        /// <summary>
        /// 机票价格（往）
        /// </summary>
        public decimal GoAmount { get; set; }

        /// <summary>
        /// 机票价格（返）
        /// </summary>
        public decimal BackAmount { get; set; }

        public string BoarderIDType { get; set; }
        public bool TicketIsUsed { get; set; }
        public bool TicketIsConfirm { get; set; }
        /// <summary>
        /// 登机人手机号
        /// </summary>
        public string BoarderMobile { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int? Status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        /// 收款人
        /// </summary>
        public string Recipient
        {
            set { _recipient = value; }
            get { return _recipient; }
        }

        /// <summary>
        /// 银行
        /// </summary>
        public string BankName
        {
            set { _bankName = value; }
            get { return _bankName; }
        }

        /// <summary>
        /// 银行账号
        /// </summary>
        public string BankAccountNo
        {
            set { _bankAccountNo = value; }
            get { return _bankAccountNo; }
        }

        /// <summary>
        /// 成本明细ID
        /// </summary>
        public int? CostDetailID
        {
            get { return _costDetailID; }
            set { _costDetailID = value; }
        }

        /// <summary>
        /// 报销类型的单位数量
        /// </summary>
        public int? ExpenseTypeNumber
        {
            set { _expenseTypeNumber = value; }
            get { return _expenseTypeNumber; }
        }

        /// <summary>
        /// 明细主键
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 报销申请单ID
        /// </summary>
        public int? ReturnID
        {
            set { _returnid = value; }
            get { return _returnid; }
        }
        /// <summary>
        /// 费用发生日期
        /// </summary>
        public DateTime? ExpenseDate
        {
            set { _expensedate = value; }
            get { return _expensedate; }
        }
        /// <summary>
        /// 费用类型
        /// </summary>
        public int? ExpenseType
        {
            set { _expensetype = value; }
            get { return _expensetype; }
        }
        /// <summary>
        /// 费用描述
        /// </summary>
        public string ExpenseDesc
        {
            set { _expensedesc = value; }
            get { return _expensedesc; }
        }
        /// <summary>
        /// 费用
        /// </summary>
        public decimal? ExpenseMoney
        {
            set { _expensemoney = value; }
            get { return _expensemoney; }
        }
        /// <summary>
        /// 财务备注
        /// </summary>
        public string FinanceMemo
        {
            set { _financememo = value; }
            get { return _financememo; }
        }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public int? Creater
        {
            set { _creater = value; }
            get { return _creater; }
        }
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreaterName
        {
            set { _creatername = value; }
            get { return _creatername; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? PhoneYear
        {
            set { _phoneyear = value; }
            get { return _phoneyear; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? PhoneMonth
        {
            set { _phonemonth = value; }
            get { return _phonemonth; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? MealFeeMorning
        {
            set { _mealfeemorning = value; }
            get { return _mealfeemorning; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? MealFeeNoon
        {
            set { _mealfeenoon = value; }
            get { return _mealfeenoon; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? MealFeeNight
        {
            set { _mealfeenight = value; }
            get { return _mealfeenight; }
        }

        public int PhoneInvoice { get; set; }

        public string PhoneInvoiceNo { get; set; }

        #endregion Model

    }
    [Serializable]
    public partial class TicketUserInfo
    {
        public string Boarder { get; set; }
        public string BoarderIDCard { get; set; }
        public string BoarderMobile { get; set; }
    }
}
