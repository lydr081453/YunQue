using System;
namespace ESP.Purchase.Entity
{
    public class MediaOrderInfo
    {
        public MediaOrderInfo()
        { }
        #region Model
        private int _meidaorderid;
        private string _tel;
        private string _mobile;
        private string _paytype;
        private decimal? _totalamount;
        private bool _isdelegate;
        private bool _isimage;
        private string _subject;
        private int? _wordlength;
        private string _writtingurl;
        private DateTime? _begindate;
        private int? _mediaid;
        private DateTime? _enddate;
        private string _attachment;
        private int? _orderid;
        private string _medianame;
        private int? _reporterid;
        private string _reportername;
        private string _cityname;
        private string _cardnumber;
        private string _bankname;
        private string _bankaccountname;
        private int _Status;
        private string _imageSize;
        private string _receiverName;
        private DateTime? _releaseDate;
        private int? _isPayment;
        private int? _paymentUserID;
        public string ReturnCode { get; set; }
        public int ReturnID { get; set; }
        public int? PaymentUserID
        {
            get { return _paymentUserID; }
            set { _paymentUserID = value; }
        }
        /// <summary>
        /// 发稿日期
        /// </summary>
        public DateTime? ReleaseDate
        {
            get { return _releaseDate; }
            set { _releaseDate = value; }
        }

        public int? IsPayment
        {
            get { return _isPayment==null?0:_isPayment.Value; }
            set { _isPayment = value; }
        }
        private bool _isTax;
        /// <summary>
        /// 对私是否需要税单
        /// </summary>
        public bool IsTax
        {
            get { return _isTax; }
            set { _isTax = value; }
        }

        /// <summary>
        /// 收款人
        /// </summary>
        public string ReceiverName
        {
            get { return _receiverName; }
            set { _receiverName = value; }
        }

        public string ImageSize
        {
            get { return _imageSize; }
            set { _imageSize = value; }
        }
        public int Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int MeidaOrderID
        {
            set { _meidaorderid = value; }
            get { return _meidaorderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PayType
        {
            set { _paytype = value; }
            get { return _paytype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TotalAmount
        {
            set { _totalamount = value; }
            get { return _totalamount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsDelegate
        {
            set { _isdelegate = value; }
            get { return _isdelegate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsImage
        {
            set { _isimage = value; }
            get { return _isimage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Subject
        {
            set { _subject = value; }
            get { return _subject; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? WordLength
        {
            set { _wordlength = value; }
            get { return _wordlength; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WrittingURL
        {
            set { _writtingurl = value; }
            get { return _writtingurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? BeginDate
        {
            set { _begindate = value; }
            get { return _begindate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? MediaID
        {
            set { _mediaid = value; }
            get { return _mediaid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Attachment
        {
            set { _attachment = value; }
            get { return _attachment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OrderID
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MediaName
        {
            set { _medianame = value; }
            get { return _medianame; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ReporterID
        {
            set { _reporterid = value; }
            get { return _reporterid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReporterName
        {
            set { _reportername = value; }
            get { return _reportername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CityName
        {
            set { _cityname = value; }
            get { return _cityname; }
        }
        /// <summary>
        /// 身份证
        /// </summary>
        public string CardNumber
        {
            set { _cardnumber = value; }
            get { return _cardnumber; }
        }
        /// <summary>
        /// 开户行
        /// </summary>
        public string BankName
        {
            set { _bankname = value; }
            get { return _bankname; }
        }
        /// <summary>
        /// 帐号
        /// </summary>
        public string BankAccountName
        {
            set { _bankaccountname = value; }
            get { return _bankaccountname; }
        }
        #endregion Model

    }
}

