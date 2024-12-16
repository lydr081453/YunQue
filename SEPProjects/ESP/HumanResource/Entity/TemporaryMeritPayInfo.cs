using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace ESP.HumanResource.Entity
{
    /// <summary>
    /// 实体类TemporaryMeritPay 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    
    public class TemporaryMeritPayInfo
    {
        public TemporaryMeritPayInfo()
        { }
        #region Model
        private int _id;
        private int _userid;
        private string _code;
        private string _meritpay;
        private int _implementyear;
        private int _implementmonth;
        private int _creator;
        private DateTime _createdate;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MeritPay
        {
            set { _meritpay = value; }
            get { return _meritpay; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ImplementYear
        {
            set { _implementyear = value; }
            get { return _implementyear; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ImplementMonth
        {
            set { _implementmonth = value; }
            get { return _implementmonth; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        #endregion Model

    }
}

