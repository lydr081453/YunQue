using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_SupplierEvaluation
    {
        public SC_SupplierEvaluation()
        { }
        #region Model
        private int _id;
        private int _supplierid;
        private int _questionid;
        private string _questionnum;
        private string _evaluation;
        private DateTime? _createtime;
        private string _creator;
        private string _createip;
        private DateTime? _lastupdatetime;
        private string _lastupdateman;
        private string _lastupdateip;
        private int? _type;
        private int? _status;
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
        public int supplierid
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int questionid
        {
            set { _questionid = value; }
            get { return _questionid; }
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
        public string Evaluation
        {
            set { _evaluation = value; }
            get { return _evaluation; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? createtime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string createip
        {
            set { _createip = value; }
            get { return _createip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? lastupdatetime
        {
            set { _lastupdatetime = value; }
            get { return _lastupdatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string lastupdateman
        {
            set { _lastupdateman = value; }
            get { return _lastupdateman; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string lastupdateip
        {
            set { _lastupdateip = value; }
            get { return _lastupdateip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? status
        {
            set { _status = value; }
            get { return _status; }
        }
        #endregion Model
    }
}
