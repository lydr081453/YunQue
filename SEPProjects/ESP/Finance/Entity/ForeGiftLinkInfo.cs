using System;
namespace ESP.Finance.Entity
{
	/// <summary>
	/// 实体类ForeGiftLinkInfo 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
    public class ForeGiftLinkInfo
    {
        public ForeGiftLinkInfo()
        { }
        #region Model
        private int _linkid;
        private int _foregiftprid;
        private int _foregiftreturnid;
        private int _killforegiftprid;
        private int _killforegiftreturnid;
        private int _linker;
        private decimal _killPrice;
        private DateTime _linkdate;
        private string _killremark;
        /// <summary>
        /// 自增编号
        /// </summary>
        public int linkId
        {
            set { _linkid = value; }
            get { return _linkid; }
        }
        /// <summary>
        /// 押金的PrId
        /// </summary>
        public int foregiftPrId
        {
            set { _foregiftprid = value; }
            get { return _foregiftprid; }
        }
        /// <summary>
        /// 押金的returnId
        /// </summary>
        public int foregiftReturnId
        {
            set { _foregiftreturnid = value; }
            get { return _foregiftreturnid; }
        }
        /// <summary>
        /// 抵押金的PrId
        /// </summary>
        public int killforegiftPrId
        {
            set { _killforegiftprid = value; }
            get { return _killforegiftprid; }
        }
        /// <summary>
        /// 抵押金的returnId
        /// </summary>
        public int killforegiftReturnId
        {
            set { _killforegiftreturnid = value; }
            get { return _killforegiftreturnid; }
        }
        /// <summary>
        /// 操作人
        /// </summary>
        public int linker
        {
            set { _linker = value; }
            get { return _linker; }
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime linkDate
        {
            set { _linkdate = value; }
            get { return _linkdate; }
        }
        /// <summary>
        /// 抵押金描述
        /// </summary>
        public string killRemark
        {
            set { _killremark = value; }
            get { return _killremark; }
        }

        /// <summary>
        /// 抵消金额
        /// </summary>
        public decimal killPrice
        {
            get { return _killPrice; }
            set { _killPrice = value; }
        }
        #endregion Model

    }
}

