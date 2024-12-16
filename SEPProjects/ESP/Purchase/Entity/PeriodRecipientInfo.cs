namespace ESP.Purchase.Entity
{
    /// <summary>
    /// 实体类T_PeriodRecipient 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class PeriodRecipientInfo
    {
        public PeriodRecipientInfo()
        { }
        #region Model
        private int _id;
        private int _periodid;
        private int _recipientid;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 账期ID
        /// </summary>
        public int periodId
        {
            set { _periodid = value; }
            get { return _periodid; }
        }
        /// <summary>
        /// 收货ID
        /// </summary>
        public int recipientId
        {
            set { _recipientid = value; }
            get { return _recipientid; }
        }
        #endregion Model

    }
}

