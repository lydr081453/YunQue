using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类F_PaymentExtension 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class PaymentExtensionInfo
    {
        public PaymentExtensionInfo()
        { }
        #region Model
        private int _id;
        private int _paymentid;
        private DateTime _paiddate;
        private string _paidremark;
        private DateTime _logdate;
        private int _loguserid;
        private DateTime _oldpaiddate;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public int PaymentId
        {
            get { return _paymentid; }
            set { _paymentid = value; }
        }

        public int LogUserId
        {
            get { return _loguserid; }
            set { _loguserid = value; }
        }

        public DateTime PaidDate
        {
            get { return _paiddate; }
            set { _paiddate = value; }
        }

        public DateTime LogDate
        {
            get { return _logdate; }
            set { _logdate = value; }
        }

        public string PaidRemark
        {
            get { return _paidremark; }
            set { _paidremark = value; }
        }

        public DateTime OldPaidDate
        {
            get { return _oldpaiddate; }
            set { _oldpaiddate = value; }
        }
        #endregion Model

    }
      
}