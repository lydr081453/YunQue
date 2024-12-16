using System;
namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类PaymentContentInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class PaymentContentInfo
    {
        public PaymentContentInfo()
        { }
        #region Model
        private int _paymentcontentid;
        private string _paymentcontent;
        /// <summary>
        /// 
        /// </summary>
        public int PaymentContentID
        {
            set { _paymentcontentid = value; }
            get { return _paymentcontentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string paymentContent
        {
            set { _paymentcontent = value; }
            get { return _paymentcontent; }
        }
        #endregion Model

    }
}