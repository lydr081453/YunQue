using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    /// <summary>
    /// 离职未处理单据信息
    /// </summary>
    [Serializable]
    public class DimissionDetailsInfo
    {
        public DimissionDetailsInfo()
        { }

        #region Model
        private int _dimissiondetailid;
        private int _dimissionid;
        private int _formid;
        private string _formcode;
        private string _formtype;
        private int _userid;
        private string _username;
        private int _projectid;
        private string _projectcode;
        private string _description;
        private decimal _totalprice;
        private int _formstatus;
        private int _receiverid;
        private string _receivername;
        private int _receiverdepartmentid;
        private string _receiverdepartmentname;
        private int _status;
        private string _remark;
        private DateTime _createtime;
        private DateTime? _receivertime;
        private string _website;
        private string _url;
        private int _updatestatus = 0;
        /// <summary>
        /// 离职明细单编号
        /// </summary>
        public int DimissionDetailId
        {
            set { _dimissiondetailid = value; }
            get { return _dimissiondetailid; }
        }
        /// <summary>
        /// 离职单编号
        /// </summary>
        public int DimissionId
        {
            set { _dimissionid = value; }
            get { return _dimissionid; }
        }
        /// <summary>
        /// 单据编号
        /// </summary>
        public int FormId
        {
            set { _formid = value; }
            get { return _formid; }
        }
        /// <summary>
        /// 单据号（项目号、PR单号）
        /// </summary>
        public string FormCode
        {
            set { _formcode = value; }
            get { return _formcode; }
        }
        /// <summary>
        /// 单据类型
        /// </summary>
        public string FormType
        {
            set { _formtype = value; }
            get { return _formtype; }
        }
        /// <summary>
        /// 单据负责人编号
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 单据负责人名称
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 项目号ID
        /// </summary>
        public int ProjectId
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// 项目号
        /// </summary>
        public string ProjectCode
        {
            set { _projectcode = value; }
            get { return _projectcode; }
        }
        /// <summary>
        /// 项目号描述
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalPrice
        {
            set { _totalprice = value; }
            get { return _totalprice; }
        }
        /// <summary>
        /// 单据状态
        /// </summary>
        public int FormStatus
        {
            set { _formstatus = value; }
            get { return _formstatus; }
        }
        /// <summary>
        /// 接受人编号
        /// </summary>
        public int ReceiverId
        {
            set { _receiverid = value; }
            get { return _receiverid; }
        }
        /// <summary>
        /// 接受人姓名
        /// </summary>
        public string ReceiverName
        {
            set { _receivername = value; }
            get { return _receivername; }
        }
        /// <summary>
        /// 接受人部门编号
        /// </summary>
        public int ReceiverDepartmentId
        {
            set { _receiverdepartmentid = value; }
            get { return _receiverdepartmentid; }
        }
        /// <summary>
        /// 接受人部门名称
        /// </summary>
        public string ReceiverDepartmentName
        {
            set { _receiverdepartmentname = value; }
            get { return _receiverdepartmentname; }
        }
        /// <summary>
        /// 该项单据审批状态
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 交接时间
        /// </summary>
        public DateTime? ReceiverTime
        {
            set { _receivertime = value; }
            get { return _receivertime; }
        }
        /// <summary>
        /// 站点
        /// </summary>
        public string Website
        {
            set { _website = value; }
            get { return _website; }
        }
        /// <summary>
        /// 查看链接
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 单据更新状态
        /// </summary>
        public int UpdateStatus
        {
            set { _updatestatus = value; }
            get { return _updatestatus; }
        }
        #endregion Model

        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.IDataReader r)
        {
            if (r["DimissionDetailId"].ToString() != "")
            {
                _dimissiondetailid = int.Parse(r["DimissionDetailId"].ToString());
            }
            if (r["DimissionId"].ToString() != "")
            {
                _dimissionid = int.Parse(r["DimissionId"].ToString());
            }
            if (r["FormId"].ToString() != "")
            {
                _formid = int.Parse(r["FormId"].ToString());
            }
            _formcode = r["FormCode"].ToString();
            _formtype = r["FormType"].ToString();
            if (r["UserId"].ToString() != "")
            {
                _userid = int.Parse(r["UserId"].ToString());
            }
            _username = r["UserName"].ToString();
            if (r["ProjectId"].ToString() != "")
            {
                _projectid = int.Parse(r["ProjectId"].ToString());
            }
            _projectcode = r["ProjectCode"].ToString();
            _description = r["Description"].ToString();
            if (r["TotalPrice"].ToString() != "")
            {
                _totalprice = decimal.Parse(r["TotalPrice"].ToString());
            }
            if (r["FormStatus"].ToString() != "")
            {
                _formstatus = int.Parse(r["FormStatus"].ToString());
            }
            if (r["ReceiverId"].ToString() != "")
            {
                _receiverid = int.Parse(r["ReceiverId"].ToString());
            }
            _receivername = r["ReceiverName"].ToString();
            if (r["ReceiverDepartmentId"].ToString() != "")
            {
                _receiverdepartmentid = int.Parse(r["ReceiverDepartmentId"].ToString());
            }
            _receiverdepartmentname = r["ReceiverDepartmentName"].ToString();
            if (r["Status"].ToString() != "")
            {
                _status = int.Parse(r["Status"].ToString());
            }
            _remark = r["Remark"].ToString();
            if (r["CreateTime"].ToString() != "")
            {
                _createtime = DateTime.Parse(r["CreateTime"].ToString());
            }
            if (r["ReceiverTime"].ToString() != "")
            {
                _receivertime = DateTime.Parse(r["ReceiverTime"].ToString());
            }
            _website = r["Website"].ToString();
            _url = r["Url"].ToString();
            if (r["UpdateStatus"].ToString() != "")
            {
                _updatestatus = int.Parse(r["UpdateStatus"].ToString());
            }
        }

        /// <summary>
        /// 格式化数据对象
        /// </summary>
        /// <param name="r"></param>
        public void PopupData(System.Data.DataRow r)
        {
            if (r["DimissionDetailId"].ToString() != "")
            {
                _dimissiondetailid = int.Parse(r["DimissionDetailId"].ToString());
            }
            if (r["DimissionId"].ToString() != "")
            {
                _dimissionid = int.Parse(r["DimissionId"].ToString());
            }
            if (r["FormId"].ToString() != "")
            {
                _formid = int.Parse(r["FormId"].ToString());
            }
            _formcode = r["FormCode"].ToString();
            _formtype = r["FormType"].ToString();
            if (r["UserId"].ToString() != "")
            {
                _userid = int.Parse(r["UserId"].ToString());
            }
            _username = r["UserName"].ToString();
            if (r["ProjectId"].ToString() != "")
            {
                _projectid = int.Parse(r["ProjectId"].ToString());
            }
            _projectcode = r["ProjectCode"].ToString();
            _description = r["Description"].ToString();
            if (r["TotalPrice"].ToString() != "")
            {
                _totalprice = decimal.Parse(r["TotalPrice"].ToString());
            }
            if (r["FormStatus"].ToString() != "")
            {
                _formstatus = int.Parse(r["FormStatus"].ToString());
            }
            if (r["ReceiverId"].ToString() != "")
            {
                _receiverid = int.Parse(r["ReceiverId"].ToString());
            }
            _receivername = r["ReceiverName"].ToString();
            if (r["ReceiverDepartmentId"].ToString() != "")
            {
                _receiverdepartmentid = int.Parse(r["ReceiverDepartmentId"].ToString());
            }
            _receiverdepartmentname = r["ReceiverDepartmentName"].ToString();
            if (r["Status"].ToString() != "")
            {
                _status = int.Parse(r["Status"].ToString());
            }
            _remark = r["Remark"].ToString();
            if (r["CreateTime"].ToString() != "")
            {
                _createtime = DateTime.Parse(r["CreateTime"].ToString());
            }
            if (r["ReceiverTime"].ToString() != "")
            {
                _receivertime = DateTime.Parse(r["ReceiverTime"].ToString());
            }
            _website = r["Website"].ToString();
            _url = r["Url"].ToString();
            if (r["UpdateStatus"].ToString() != "")
            {
                _updatestatus = int.Parse(r["UpdateStatus"].ToString());
            }
        }
    }
}
