using System;

namespace ESP.Purchase.Entity
{
    /// <summary>
    /// 实体类T_auditLog 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class AuditLogInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditLogInfo"/> class.
        /// </summary>
        public AuditLogInfo()
        { }

        #region Model
        private int _id;
        private int _gid;
        private string _prno;
        private int _audituserid;
        private string _auditusername;
        private string _remark;
        private DateTime _remarkdate;
        private int _auditType;
        private string _IpAddress;
        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }
        /// <summary>
        /// 自增编号
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 申请单ID
        /// </summary>
        public int gid
        {
            set { _gid = value; }
            get { return _gid; }
        }

        /// <summary>
        /// 申请单号
        /// </summary>
        public string prNo
        {
            get { return _prno; }
            set { _prno = value; }
        }

        /// <summary>
        /// 审核人sysid
        /// </summary>
        public int auditUserId
        {
            get { return _audituserid; }
            set { _audituserid = value; }
        }

        /// <summary>
        /// 审核人名称
        /// </summary>
        public string auditUserName
        {
            get { return _auditusername; }
            set { _auditusername = value; }
        }

        /// <summary>
        /// 审核类型 0：驳回 1：通过
        /// </summary>
        public int auditType
        {
            get { return _auditType; }
            set { _auditType = value; }
        }

        /// <summary>
        /// 审批备注
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime remarkDate
        {
            set { _remarkdate = value; }
            get { return _remarkdate; }
        }
        #endregion Model
    }
}