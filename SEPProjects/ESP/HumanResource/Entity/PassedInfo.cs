using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace ESP.HumanResource.Entity
{
    public class PassedInfo
    {
        public PassedInfo()
        { }
        #region Model
        private int _id;
        private int _sysid;
        private string _groupname;
        private string _sysusername;
        private DateTime _joinjobdate = DateTime.Parse("1900-1-1 0:00:00");
        private int _companyid;
        private string _companyname;
        private int _departmentid;
        private string _departmentname;
        private int _groupid;
        private string _currenttitle;
        private int _salarydetailid;
        private DateTime _operationdate = DateTime.Parse("1900-1-1 0:00:00");
        private string _memo;
        private int _creater;
        private string _creatername;
        private DateTime _createdate = DateTime.Parse("1900-1-1 0:00:00");
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int sysid
        {
            set { _sysid = value; }
            get { return _sysid; }
        }
        /// <summary>
        /// 所属团队
        /// </summary>
        public string groupName
        {
            set { _groupname = value; }
            get { return _groupname; }
        }
        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string sysUserName
        {
            set { _sysusername = value; }
            get { return _sysusername; }
        }
        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime joinJobDate
        {
            set { _joinjobdate = value; }
            get { return _joinjobdate; }
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
        /// 职位
        /// </summary>
        public string currentTitle
        {
            set { _currenttitle = value; }
            get { return _currenttitle; }
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
        /// 
        /// </summary>
        public string memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int creater
        {
            set { _creater = value; }
            get { return _creater; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string createrName
        {
            set { _creatername = value; }
            get { return _creatername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime createDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        #endregion Model

        public void PopupData(IDataReader r)
        {
            _id = int.Parse(r["id"].ToString());
            if (r["sysid"].ToString() != "")
            {
                _sysid = int.Parse(r["sysid"].ToString());
            }
            _groupname = r["groupName"].ToString();
            _sysusername = r["sysUserName"].ToString();
            if (r["joinJobDate"].ToString() != "")
            {
                _joinjobdate = DateTime.Parse(r["joinJobDate"].ToString());
            }
            if (r["companyID"].ToString() != "")
            {
                _companyid = int.Parse(r["companyID"].ToString());
            }
            _companyname = r["companyName"].ToString();
            if (r["departmentID"].ToString() != "")
            {
                _departmentid = int.Parse(r["departmentID"].ToString());
            }
            _departmentname = r["departmentName"].ToString();
            if (r["groupID"].ToString() != "")
            {
                _groupid = int.Parse(r["groupID"].ToString());
            }
            _currenttitle = r["currentTitle"].ToString();
            if (r["salaryDetailID"].ToString() != "")
            {
                _salarydetailid = int.Parse(r["salaryDetailID"].ToString());
            }
            if (r["operationDate"].ToString() != "")
            {
                _operationdate = DateTime.Parse(r["operationDate"].ToString());
            }
            _memo = r["memo"].ToString();
            if (r["creater"].ToString() != "")
            {
                _creater = int.Parse(r["creater"].ToString());
            }
            _creatername = r["createrName"].ToString();
            if (r["createDate"].ToString() != "")
            {
                _createdate = DateTime.Parse(r["createDate"].ToString());
            }
        }
    }
}
