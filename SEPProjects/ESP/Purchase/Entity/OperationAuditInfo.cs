using System;

namespace ESP.Purchase.Entity
{
    /// <summary>
    /// 实体类T_OperationAudit 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class OperationAuditInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationAuditInfo"/> class.
        /// </summary>
        public OperationAuditInfo()
        { }
        #region Model
        private int _id;
        private int _general_id;
        private int _auditorid;
        private int _aduittype;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 申请单ID
        /// </summary>
        public int general_id
        {
            set { _general_id = value; }
            get { return _general_id; }
        }
        /// <summary>
        /// 项目负责人
        /// </summary>
        public int auditorId
        {
            set { _auditorid = value; }
            get { return _auditorid; }
        }
        /// <summary>
        /// 审核人类型
        /// </summary>
        public int aduitType
        {
            set { _aduittype = value; }
            get { return _aduittype; }
        }
        #endregion Model
    }
}