using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Supplier.Entity
{
    public class SC_QuestionAnswer
    {
        public SC_QuestionAnswer()
        { }
        #region Model
        private int _id;
        private int _supplierid;
        private int _questionid;
        private string _questionnum;
        private string _answercontent;
        private int _answerlength;
        private string _attachcontent;
        private DateTime _creattime;
        private string _creatip;
        private DateTime _lastupdatetime;
        private string _lastupdateip;
        private int _type;
        private int _status;
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
        public int QuestionId
        {
            set { _questionid = value; }
            get { return _questionid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string QuestionNum
        {
            set { _questionnum = value; }
            get { return _questionnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AnswerContent
        {
            set { _answercontent = value; }
            get { return _answercontent; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int AnswerLength
        {
            set { _answerlength = value; }
            get { return _answerlength; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AttachContent
        {
            set { _attachcontent = value; }
            get { return _attachcontent; }
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
