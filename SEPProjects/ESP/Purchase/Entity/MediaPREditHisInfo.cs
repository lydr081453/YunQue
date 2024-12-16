using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Purchase.Entity
{
    /// <summary>
    /// 实体类T_MediaPREditHis 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class MediaPREditHisInfo
    {
        public MediaPREditHisInfo()
        { }
        #region Model
        private int _id;
        private int? _oldprid;
        private string _oldprno;
        private int? _newprid;
        private string _newprno;
        private int? _newpnid;
        private string _newpnno;
        private string _OldPaymentID;

        public string OldPaymentID
        {
            get { return _OldPaymentID; }
            set { _OldPaymentID = value; }
        }
        private int _OrderType;

        public int OrderType
        {
            get { return _OrderType; }
            set { _OrderType = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OldPRId
        {
            set { _oldprid = value; }
            get { return _oldprid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OldPRNo
        {
            set { _oldprno = value; }
            get { return _oldprno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? NewPRId
        {
            set { _newprid = value; }
            get { return _newprid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NewPRNo
        {
            set { _newprno = value; }
            get { return _newprno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? NewPNId
        {
            set { _newpnid = value; }
            get { return _newpnid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NewPNNo
        {
            set { _newpnno = value; }
            get { return _newpnno; }
        }
        #endregion Model

    }
}
