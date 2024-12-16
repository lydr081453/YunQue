using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Supply.Entity
{
    public class Resolution
    {
        public Resolution()
        { }
        #region Model
        private int _id;
        private string _title;
        private string _content;
        private DateTime? _createtime;
        private int? _createuserid;
        private int? _sysid;
        private string _createip;
        private int _viewcount;
        private int? _parentid;
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
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Content
        {
            set { _content = value; }
            get { return _content; }
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
        public int? createUserId
        {
            set { _createuserid = value; }
            get { return _createuserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? sysId
        {
            set { _sysid = value; }
            get { return _sysid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string createIp
        {
            set { _createip = value; }
            get { return _createip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int viewCount
        {
            set { _viewcount = value; }
            get { return _viewcount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? parentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }

        public string userName { get; set; }

        public int status { get; set; }
        #endregion Model

    }
}
