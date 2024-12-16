using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_SupplierScore
    {
        public SC_SupplierScore()
        { }

        #region Model
        private int _id;
        private int _supplierid;
        private decimal _score;
        private int _scoretype;
        private string _scoredes;
        private DateTime _creattime;
        private string _creatip;
        private DateTime _lastupdatetime;
        private string _lastupdateip;
        private int _type;
        private int _status;
        private int _sourceid;
        private int _pirmarykey;

        /// <summary>
        /// 唯一标识
        /// </summary>
        public int PirmaryKey
        {
            set { _pirmarykey = value; }
            get { return _pirmarykey; }
        }
        /// <summary>
        /// 题源ID
        /// </summary>
        public int SourceId
        {
            set { _sourceid = value; }
            get { return _sourceid; }
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
        public int SupplierId
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Score
        {
            set { _score = value; }
            get { return _score; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ScoreType
        {
            set { _scoretype = value; }
            get { return _scoretype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ScoreDes
        {
            set { _scoredes = value; }
            get { return _scoredes; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreatTime
        {
            set { _creattime = value; }
            get { return _creattime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreatIP
        {
            set { _creatip = value; }
            get { return _creatip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastUpdateTime
        {
            set { _lastupdatetime = value; }
            get { return _lastupdatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LastUpdateIP
        {
            set { _lastupdateip = value; }
            get { return _lastupdateip; }
        }
        /// <summary>
        /// 联系人类型
        /// </summary>
        public int Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 联系人状态
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        #endregion Model
    }
}
