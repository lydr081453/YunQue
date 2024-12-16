using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类V_GetWaitPaymentList 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class WaitPaymentListViewInfo
    {
        public WaitPaymentListViewInfo()
        { }
        #region Model
        private int _id;
        private int? _periodday;
        private int? _datetype;
        private decimal? _expectpaymentprice;
        private decimal? _expectpaymentpercent;
        private int? _status;
        private string _prno;
        private string _requestorname;
        private string _endusername;
        private int? _project_id;
        private string _project_code;
        private int? _gid;
        private DateTime? _begindate;
        private DateTime? _enddate;
        private string _periodremark;
        private decimal? _inceptprice;
        private DateTime? _inceptdate;
        private int? _periodtype;
        private int? _perioddatumpoint;


        private string _periodTypeName;
        private string _periodDatumPointName;
        private string _periodDayName;
        private string _statusName;

        public string StatusName
        {
            get { return _statusName; }
            set { _statusName = value; }
        }

        public string PeriodDayName
        {
            get { return _periodDayName; }
            set { _periodDayName = value; }
        }

        public string PeriodDatumPointName
        {
            get { return _periodDatumPointName; }
            set { _periodDatumPointName = value; }
        }


        public string PeriodTypeName
        {
            get { return _periodTypeName; }
            set { _periodTypeName = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
       
        /// <summary>
        /// 
        /// </summary>
        public int? periodDay
        {
            set { _periodday = value; }
            get { return _periodday; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? dateType
        {
            set { _datetype = value; }
            get { return _datetype; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal? expectPaymentPrice
        {
            set { _expectpaymentprice = value; }
            get { return _expectpaymentprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? expectPaymentPercent
        {
            set { _expectpaymentpercent = value; }
            get { return _expectpaymentpercent; }
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
        public string prNo
        {
            set { _prno = value; }
            get { return _prno; }
        }
   
        /// <summary>
        /// 
        /// </summary>
        public string requestorname
        {
            set { _requestorname = value; }
            get { return _requestorname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string endusername
        {
            set { _endusername = value; }
            get { return _endusername; }
        }
    
        /// <summary>
        /// 
        /// </summary>
        public int? project_id
        {
            set { _project_id = value; }
            get { return _project_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string project_code
        {
            set { _project_code = value; }
            get { return _project_code; }
        }
   
        /// <summary>
        /// 
        /// </summary>
        public int? gid
        {
            set { _gid = value; }
            get { return _gid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? beginDate
        {
            set { _begindate = value; }
            get { return _begindate; }
        }
  
        /// <summary>
        /// 
        /// </summary>
        public DateTime? endDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string periodRemark
        {
            set { _periodremark = value; }
            get { return _periodremark; }
        }
    
        /// <summary>
        /// 
        /// </summary>
        public decimal? inceptPrice
        {
            set { _inceptprice = value; }
            get { return _inceptprice; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? inceptDate
        {
            set { _inceptdate = value; }
            get { return _inceptdate; }
        }
   
        /// <summary>
        /// 
        /// </summary>
        public int? periodType
        {
            set { _periodtype = value; }
            get { return _periodtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? periodDatumPoint
        {
            set { _perioddatumpoint = value; }
            get { return _perioddatumpoint; }
        }

        #endregion Model

    }
}

