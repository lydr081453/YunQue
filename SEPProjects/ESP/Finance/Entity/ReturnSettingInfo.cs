using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Finance.Entity
{
    /// <summary>
    /// 实体类ReturnSettingInfo 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class ReturnSettingInfo
    {
        public ReturnSettingInfo()
        { }
        #region Model
        private int _auditbizid;
        private int? _returnid;
        private int? _auditid;
        private int? _audittype;
        /// <summary>
        /// 
        /// </summary>
        public int AuditBizID
        {
            set { _auditbizid = value; }
            get { return _auditbizid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ReturnID
        {
            set { _returnid = value; }
            get { return _returnid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AuditID
        {
            set { _auditid = value; }
            get { return _auditid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AuditType
        {
            set { _audittype = value; }
            get { return _audittype; }
        }
        #endregion Model

    }
}
