using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class SalaryInfo
    {
        public SalaryInfo()
        { }
        #region Model
        private int _id;
        private string _creatername;
        private DateTime _createdate = DateTime.Parse("1900-1-1 0:00:00");
        private string _memo;
        private int _declarestatus;
        private int _declarer;
        private string _declarername;
        private DateTime _declaredate = DateTime.Parse("1900-1-1 0:00:00");
        private int _groupapprovestatus;
        private int _groupapprover;
        private string _groupapprovername;
        private int _sysid;
        private DateTime _groupapprovedate = DateTime.Parse("1900-1-1 0:00:00");
        private int _grouphrstatus;
        private int _grouphr;
        private string _grouphrname;
        private DateTime _grouphrdate = DateTime.Parse("1900-1-1 0:00:00");
        private string _usercode;
        private string _sysusername;
        private string _job;
        private int _paychange;
        private int _salarydetailid;
        private DateTime _operationdate = DateTime.Parse("1900-1-1 0:00:00");
        private int _creater;
        private int _companyid;
        private string _companyname;
        private int _departmentid;
        private string _departmentname;
        private int _groupid;
        private string _groupname;

        private SnapshotsInfo _Snapshots;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string createrName
        {
            set { _creatername = value; }
            get { return _creatername; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime createDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        /// <summary>
        /// 申报状态
        /// </summary>
        public int declareStatus
        {
            set { _declarestatus = value; }
            get { return _declarestatus; }
        }
        /// <summary>
        /// 申报人（总经理）
        /// </summary>
        public int declarer
        {
            set { _declarer = value; }
            get { return _declarer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string declarerName
        {
            set { _declarername = value; }
            get { return _declarername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime declareDate
        {
            set { _declaredate = value; }
            get { return _declaredate; }
        }
        /// <summary>
        /// 集团审批状态
        /// </summary>
        public int groupApproveStatus
        {
            set { _groupapprovestatus = value; }
            get { return _groupapprovestatus; }
        }
        /// <summary>
        /// 集团核准（总裁/执行副总裁）
        /// </summary>
        public int groupApprover
        {
            set { _groupapprover = value; }
            get { return _groupapprover; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string groupApproverName
        {
            set { _groupapprovername = value; }
            get { return _groupapprovername; }
        }
        /// <summary>
        /// 员工编号
        /// </summary>
        public int sysid
        {
            set { _sysid = value; }
            get { return _sysid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime groupApproveDate
        {
            set { _groupapprovedate = value; }
            get { return _groupapprovedate; }
        }
        /// <summary>
        /// 集团人事审批状态
        /// </summary>
        public int groupHrStatus
        {
            set { _grouphrstatus = value; }
            get { return _grouphrstatus; }
        }
        /// <summary>
        /// 集团人事部（主管人）
        /// </summary>
        public int groupHr
        {
            set { _grouphr = value; }
            get { return _grouphr; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string groupHrName
        {
            set { _grouphrname = value; }
            get { return _grouphrname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime groupHrDate
        {
            set { _grouphrdate = value; }
            get { return _grouphrdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string userCode
        {
            set { _usercode = value; }
            get { return _usercode; }
        }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string sysUserName
        {
            set { _sysusername = value; }
            get { return _sysusername; }
        }
        /// <summary>
        /// 职位
        /// </summary>
        public string job
        {
            set { _job = value; }
            get { return _job; }
        }
        /// <summary>
        /// 薪资变动（上调、下调、持平）
        /// </summary>
        public int payChange
        {
            set { _paychange = value; }
            get { return _paychange; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int salaryDetailID
        {
            set { _salarydetailid = value; }
            get { return _salarydetailid; }
        }
        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime operationDate
        {
            set { _operationdate = value; }
            get { return _operationdate; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public int creater
        {
            set { _creater = value; }
            get { return _creater; }
        }

        private string _nowBasePay;
        public string nowBasePay
        {
            set { _nowBasePay = value; }
            get { return _nowBasePay; }
        }

        private string _nowMeritPay;
        public string nowMeritPay
        {
            set { _nowMeritPay = value; }
            get { return _nowMeritPay; }
        }

        private string _newBasePay;
        public string newBasePay
        {
            set { _newBasePay = value; }
            get { return _newBasePay; }
        }

        private string _newMeritPay;
        public string newMeritPay
        {
            set { _newMeritPay = value; }
            get { return _newMeritPay; }
        }

        /// <summary>
        /// 公司
        /// </summary>
        public int companyID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 公司
        /// </summary>
        public string companyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// 所在部门
        /// </summary>
        public int departmentID
        {
            set { _departmentid = value; }
            get { return _departmentid; }
        }
        /// <summary>
        /// 所在部门
        /// </summary>
        public string departmentName
        {
            set { _departmentname = value; }
            get { return _departmentname; }
        }
        /// <summary>
        /// 所属团队
        /// </summary>
        public int groupID
        {
            set { _groupid = value; }
            get { return _groupid; }
        }
        /// <summary>
        /// 所属团队
        /// </summary>
        public string groupName
        {
            set { _groupname = value; }
            get { return _groupname; }
        }

        public SnapshotsInfo Snapshots
        {
            set { _Snapshots = value; }
            get { return _Snapshots; }
        }
        #endregion Model

    }
}
