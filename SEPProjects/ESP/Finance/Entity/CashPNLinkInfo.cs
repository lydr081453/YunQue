using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    public class CashPNLinkInfo
    {
        #region Model
        private int _cashpnlinkid;
        private int _returnid;
        private decimal _oldPrice;
        private decimal _returncashprice;
        private int _linkreturnid;
        private string _linkremark;
        private int _linker;
        private DateTime _linkdate;
        private int _linkType;
        /// <summary>
        /// 自增编号
        /// </summary>
        public int CashPNLinkId
        {
            set { _cashpnlinkid = value; }
            get { return _cashpnlinkid; }
        }
        /// <summary>
        /// 付款申请的ID
        /// </summary>
        public int returnId
        {
            set { _returnid = value; }
            get { return _returnid; }
        }

        /// <summary>
        /// 原始PN的实际金额
        /// </summary>
        public decimal oldPrice
        {
            get { return _oldPrice; }
            set { _oldPrice = value; }
        }

        /// <summary>
        /// 偿还金额
        /// </summary>
        public decimal returnCashPrice
        {
            set { _returncashprice = value; }
            get { return _returncashprice; }
        }
        /// <summary>
        /// 关联的付款申请ID
        /// </summary>
        public int linkReturnId
        {
            set { _linkreturnid = value; }
            get { return _linkreturnid; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string linkRemark
        {
            set { _linkremark = value; }
            get { return _linkremark; }
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
        /// 类型（1：小于等于原始PN金额 2：大于原始金额小于等于原始金额+10% 3：大于原始金额+10%
        /// </summary>
        public int linkType
        {
            get { return _linkType; }
            set { _linkType = value; }
        }

        #endregion Model
    }
}
