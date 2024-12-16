using System;
using System.Data;

namespace ESP.Purchase.Entity
{
    /// <summary>
    /// 实体类T_OperationAuditLog 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    public class OperationAuditLogInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationAuditLogInfo"/> class.
        /// </summary>
        public OperationAuditLogInfo()
        { }

        #region Model
        private int _id;
        private int _gid;
        private int _auditorid;
        private string _auditorname;
        private DateTime _audittime;
        private int _audtistatus;
        private string _auditremark;
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
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 申请单ID
        /// </summary>
        /// <value>The gid.</value>
        public int Gid
        {
            set { _gid = value; }
            get { return _gid; }
        }

        /// <summary>
        /// 审核人
        /// </summary>
        /// <value>The auditor id.</value>
        public int auditorId
        {
            set { _auditorid = value; }
            get { return _auditorid; }
        }

        /// <summary>
        /// 审核人名称
        /// </summary>
        /// <value>The name of the auditor.</value>
        public string auditorName
        {
            set { _auditorname = value; }
            get { return _auditorname; }
        }

        /// <summary>
        /// 审核时间
        /// </summary>
        /// <value>The audit time.</value>
        public DateTime auditTime
        {
            set { _audittime = value; }
            get { return _audittime; }
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        /// <value>The audti status.</value>
        public int audtiStatus
        {
            set { _audtistatus = value; }
            get { return _audtistatus; }
        }

        /// <summary>
        /// 审核备注
        /// </summary>
        /// <value>The audit remark.</value>
        public string auditRemark
        {
            set { _auditremark = value; }
            get { return _auditremark; }
        }

        /// <summary>
        /// Popups the data.
        /// </summary>
        /// <param name="r">The r.</param>
        public void PopupData(IDataReader r)
        {
            if (null != r["ID"] && r["ID"].ToString() != "")
            {
                _id = int.Parse(r["ID"].ToString());
            }
            if (null != r["Gid"] && r["Gid"].ToString() != "")
            {
                _gid = int.Parse(r["Gid"].ToString());
            }
            if (null != r["auditorid"] && r["auditorid"].ToString() != "")
            {
                _auditorid = int.Parse(r["auditorid"].ToString());
            }
            _auditorname = r["auditorname"].ToString();

            if (null != r["audittime"] && r["audittime"].ToString() != "")
            {
                _audittime = DateTime.Parse(r["audittime"].ToString());
            }
            
            if (null != r["audtistatus"] && r["audtistatus"].ToString() != "")
            {
                _audtistatus = int.Parse(r["audtistatus"].ToString());
            }
            _auditremark = r["auditremark"].ToString();
            _IpAddress = r["IpAddress"] == DBNull.Value ? "" : r["IpAddress"].ToString();
        }
        #endregion Model
    }
}