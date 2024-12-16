using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_NoCheckSupplierScore
    {
        public SC_NoCheckSupplierScore()
        { }
        #region Model
        private int _id;
        private int? _qaid;
        private string _questionnum;
        private int? _supplierid;
        private decimal? _score;
        private int? _scoretype;
        private DateTime? _createtime;
        private string _createip;
        private string _creator;
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
        public int? qaid
        {
            set { _qaid = value; }
            get { return _qaid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string questionnum
        {
            set { _questionnum = value; }
            get { return _questionnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? supplierid
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? score
        {
            set { _score = value; }
            get { return _score; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? scoreType
        {
            set { _scoretype = value; }
            get { return _scoretype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? createTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string createIP
        {
            set { _createip = value; }
            get { return _createip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        #endregion Model

    }
}
