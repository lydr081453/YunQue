using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Administrative.Entity
{
    public partial class TSFeedBackInfo
    {
        public TSFeedBackInfo()
        { }
        #region Model
        private int _id;
        private int _userid;
        private string _username;
        private string _feedback;
        private DateTime _createtime;
        private string _createip;
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
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FeedBack
        {
            set { _feedback = value; }
            get { return _feedback; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreateIp
        {
            set { _createip = value; }
            get { return _createip; }
        }
        #endregion Model

    }
}
