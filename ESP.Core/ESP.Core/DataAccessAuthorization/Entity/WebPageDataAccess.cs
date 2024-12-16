using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.DataAccessAuthorization.Entity
{
    /// <summary>
    /// 数据表单操作权限描述
    /// </summary>
    public class WebPageDataAccess
    {
        #region Model
        private int _webpagedataaccessid;
        private int _webpageid;
        private int _dataaccessactionid;
        private string _workflowversion;
        private DateTime _createtime;
        private int _creator;
        private string _creatorname;
        /// <summary>
        /// 数据表单包含的操作类型序号
        /// </summary>
        public int WebPageDataAccessID
        {
            set { _webpagedataaccessid = value; }
            get { return _webpagedataaccessid; }
        }
        /// <summary>
        /// 网页序号
        /// </summary>
        public int WebPageID
        {
            set { _webpageid = value; }
            get { return _webpageid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DataAccessActionID
        {
            set { _dataaccessactionid = value; }
            get { return _dataaccessactionid; }
        }
        /// <summary>
        /// 工作流版本，暂时不起作用
        /// </summary>
        public string WorkFlowVersion
        {
            set { _workflowversion = value; }
            get { return _workflowversion; }
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
        public int Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreatorName
        {
            set { _creatorname = value; }
            get { return _creatorname; }
        }
        #endregion Model
    }
}
