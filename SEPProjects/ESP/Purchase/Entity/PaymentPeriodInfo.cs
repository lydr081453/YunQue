using System;

namespace ESP.Purchase.Entity
{
    /// <summary>
    /// 实体类T_PaymentPeriod 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class PaymentPeriodInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentPeriodInfo"/> class.
        /// </summary>
        public PaymentPeriodInfo()
        { }
        #region Model
        private int _id;
        private int _gid;
        private DateTime _begindate;
        private DateTime _enddate;
        private string _periodremark;
        private decimal _inceptprice;
        private DateTime _inceptdate;

        private int _periodtype;
        private int _perioddatumpoint;
        private string _periodday;
        private int _datetype;
        private decimal _expectpaymentprice;
        private decimal _expectpaymentpercent;

        private decimal _finallyPaymentPrice;
        private DateTime _finallyPaymentDate;
        private string _finallyPaymentUserId;
        private string _finallyPaymentUserName;
        private int _status;
        private int _returnId;
        private string _returnCode;

        public int TaxTypes { get; set; }

        /// <summary>
        /// 自增编号
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 申请单ID
        /// </summary>
        public int gid
        {
            set { _gid = value; }
            get { return _gid; }
        }

        /// <summary>
        /// 账期起始时间
        /// </summary>
        public DateTime beginDate
        {
            set { _begindate = value; }
            get { return _begindate; }
        }
        /// <summary>
        /// 账期结束时间
        /// </summary>
        public DateTime endDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 账期金额
        /// </summary>
        //public decimal periodPrice
        //{
        //    set { _periodprice = value; }
        //    get { return _periodprice; }
        //}
        /// <summary>
        /// 账期备注
        /// </summary>
        public string periodRemark
        {
            set { _periodremark = value; }
            get { return _periodremark; }
        }
        /// <summary>
        /// 已收金额
        /// </summary>
        public decimal inceptPrice
        {
            set { _inceptprice = value; }
            get { return _inceptprice; }
        }
        /// <summary>
        /// 已收时间
        /// </summary>
        public DateTime inceptDate
        {
            set { _inceptdate = value; }
            get { return _inceptdate; }
        }

        /// <summary>
        /// 付款账期类型 0：付款账期 1：预付款
        /// </summary>
        //public int paymentType
        //{
        //    set { _paymentType = value; }
        //    get { return _paymentType; }
        //}

        /// <summary>
        /// 账期类型
        /// </summary>
        public int periodType
        {
            set { _periodtype = value; }
            get { return _periodtype; }
        }
        /// <summary>
        /// 账期基准点
        /// </summary>
        public int periodDatumPoint
        {
            set { _perioddatumpoint = value; }
            get { return _perioddatumpoint; }
        }
        /// <summary>
        /// 账期
        /// </summary>
        public string periodDay
        {
            set { _periodday = value; }
            get { return _periodday; }
        }
        /// <summary>
        /// 日期类型
        /// </summary>
        public int dateType
        {
            set { _datetype = value; }
            get { return _datetype; }
        }
        /// <summary>
        /// 预计支付金额
        /// </summary>
        public decimal expectPaymentPrice
        {
            set { _expectpaymentprice = value; }
            get { return _expectpaymentprice; }
        }
        /// <summary>
        /// 预计支付百分比
        /// </summary>
        public decimal expectPaymentPercent
        {
            set { _expectpaymentpercent = value; }
            get { return _expectpaymentpercent; }
        }


        /// <summary>
        /// 财务部门最终支付价格
        /// </summary>
        /// <value>The finally payment price.</value>
        public decimal FinallyPaymentPrice
        {
            set { _finallyPaymentPrice = value; }
            get { return _finallyPaymentPrice; }
        }

        /// <summary>
        /// 财务部门最终支付时间
        /// </summary>
        /// <value>The finally payment date.</value>
        public DateTime FinallyPaymentDate
        {
            set { _finallyPaymentDate = value; }
            get { return _finallyPaymentDate; }
        }

        /// <summary>
        /// 财务部门最终支付人员Id
        /// </summary>
        /// <value>The finally payment user id.</value>
        public string FinallyPaymentUserId
        {
            set { _finallyPaymentUserId = value; }
            get { return _finallyPaymentUserId; }
        }

        /// <summary>
        /// 财务部门最终支付人员姓名
        /// </summary>
        /// <value>The name of the finally payment user.</value>
        public string FinallyPaymentUserName
        {
            set { _finallyPaymentUserName = value; }
            get { return _finallyPaymentUserName; }
        }

        /// <summary>
        /// 帐期状态
        /// </summary>
        /// <value>The status.</value>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }

        /// <summary>
        /// 财务系统中的F_Return表中的付款申请流水号
        /// </summary>
        /// <value>The return id.</value>
        public int ReturnId
        {
            get { return _returnId; }
            set { _returnId = value; }
        }

        /// <summary>
        /// 财务系统中的F_Return表中的付款申请号
        /// </summary>
        /// <value>The return code.</value>
        public string ReturnCode
        {
            get { return _returnCode; }
            set { _returnCode = value; }
        }
        #endregion Model
    }
}

