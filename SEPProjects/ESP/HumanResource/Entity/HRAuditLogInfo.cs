using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class HRAuditLogInfo
    {
        public HRAuditLogInfo()
        { }
        #region Model
        private int _auditlogid;
        private int _formid;
        private int _formtype;
        private string _requesition;
        private int _auditorid;
        private string _auditorname;
        private DateTime? _auditdate;
        private int _auditstatus;
        private int _auditlevel;
        /// <summary>
        /// 审批日志编号
        /// </summary>
        public int AuditLogId
        {
            set { _auditlogid = value; }
            get { return _auditlogid; }
        }
        /// <summary>
        /// 单据编号（如离职单编号）
        /// </summary>
        public int FormId
        {
            set { _formid = value; }
            get { return _formid; }
        }
        /// <summary>
        /// 单据类型（如离职单、入职单等）
        /// </summary>
        public int FormType
        {
            set { _formtype = value; }
            get { return _formtype; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Requesition
        {
            set { _requesition = value; }
            get { return _requesition; }
        }
        /// <summary>
        /// 审批人ID
        /// </summary>
        public int AuditorId
        {
            set { _auditorid = value; }
            get { return _auditorid; }
        }
        /// <summary>
        /// 审批人姓名
        /// </summary>
        public string AuditorName
        {
            set { _auditorname = value; }
            get { return _auditorname; }
        }
        /// <summary>
        /// 审批人日期
        /// </summary>
        public DateTime? AuditDate
        {
            set { _auditdate = value; }
            get { return _auditdate; }
        }
        /// <summary>
        /// 审批状态（通过、驳回）
        /// </summary>
        public int AuditStatus
        {
            set { _auditstatus = value; }
            get { return _auditstatus; }
        }
        /// <summary>
        /// 表示审批日志是属于哪个级别（和单据的状态值相对应）
        /// </summary>
        public int AuditLevel
        {
            set { _auditlevel = value; }
            get { return _auditlevel; }
        }
        #endregion Model

        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.IDataReader r)
        {
            if (r["AuditLogId"].ToString() != "")
            {
                _auditlogid = int.Parse(r["AuditLogId"].ToString());
            }
            if (r["FormId"].ToString() != "")
            {
                _formid = int.Parse(r["FormId"].ToString());
            }
            if (r["FormType"].ToString() != "")
            {
                _formtype = int.Parse(r["FormType"].ToString());
            }
            _requesition = r["Requesition"].ToString();
            if (r["AuditorId"].ToString() != "")
            {
                _auditorid = int.Parse(r["AuditorId"].ToString());
            }
            _auditorname = r["AuditorName"].ToString();
            if (r["AuditDate"].ToString() != "")
            {
                _auditdate = DateTime.Parse(r["AuditDate"].ToString());
            }
            if (r["AuditStatus"].ToString() != "")
            {
                _auditstatus = int.Parse(r["AuditStatus"].ToString());
            }
            if (r["AuditLevel"].ToString() != "")
            {
                _auditlevel = int.Parse(r["AuditLevel"].ToString());
            }
        }

        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.DataRow r)
        {
            if (r["AuditLogId"].ToString() != "")
            {
                _auditlogid = int.Parse(r["AuditLogId"].ToString());
            }
            if (r["FormId"].ToString() != "")
            {
                _formid = int.Parse(r["FormId"].ToString());
            }
            if (r["FormType"].ToString() != "")
            {
                _formtype = int.Parse(r["FormType"].ToString());
            }
            _requesition = r["Requesition"].ToString();
            if (r["AuditorId"].ToString() != "")
            {
                _auditorid = int.Parse(r["AuditorId"].ToString());
            }
            _auditorname = r["AuditorName"].ToString();
            if (r["AuditDate"].ToString() != "")
            {
                _auditdate = DateTime.Parse(r["AuditDate"].ToString());
            }
            if (r["AuditStatus"].ToString() != "")
            {
                _auditstatus = int.Parse(r["AuditStatus"].ToString());
            }
            if (r["AuditLevel"].ToString() != "")
            {
                _auditlevel = int.Parse(r["AuditLevel"].ToString());
            }
        }
    }
}
