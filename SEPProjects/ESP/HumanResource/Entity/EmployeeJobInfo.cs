using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    public class EmployeeJobInfo
    {
        public EmployeeJobInfo()
        { }

        #region Model
        private int _id;
        private int _sysid;
        private int _companyid;
        private string _companyname;
        private int _departmentid;
        private string _departmentname;
        private int _groupid;
        private string _groupname;
        private string _workplace;
        private DateTime _joindate = DateTime.Parse("1900-1-1 0:00:00");
        private string _joinjob;
        private string _directorname;
        private string _directorjob;
        private int _joinjobid;
        private string _memo;
        private string _selfintroduction;
        private string _objective;
        private string _workingexperience;
        private string _educationalbackground;
        private string _languagesanddialect;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
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
        public int companyid
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 所属公司
        /// </summary>
        public string companyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int departmentid
        {
            set { _departmentid = value; }
            get { return _departmentid; }
        }
        /// <summary>
        /// 部门
        /// </summary>
        public string departmentName
        {
            set { _departmentname = value; }
            get { return _departmentname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int groupid
        {
            set { _groupid = value; }
            get { return _groupid; }
        }
        /// <summary>
        /// 组别
        /// </summary>
        public string groupName
        {
            set { _groupname = value; }
            get { return _groupname; }
        }
        /// <summary>
        /// 工作地点
        /// </summary>
        public string workPlace
        {
            set { _workplace = value; }
            get { return _workplace; }
        }
        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime joinDate
        {
            set { _joindate = value; }
            get { return _joindate; }
        }
        /// <summary>
        /// 入职职位
        /// </summary>
        public string joinJob
        {
            set { _joinjob = value; }
            get { return _joinjob; }
        }
        /// <summary>
        /// 上级主管姓名
        /// </summary>
        public string directorName
        {
            set { _directorname = value; }
            get { return _directorname; }
        }
        /// <summary>
        /// 上级主管职位
        /// </summary>
        public string directorJob
        {
            set { _directorjob = value; }
            get { return _directorjob; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int joinjobID
        {
            set { _joinjobid = value; }
            get { return _joinjobid; }
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
        /// 自我介绍
        /// </summary>
        public string selfIntroduction
        {
            set { _selfintroduction = value; }
            get { return _selfintroduction; }
        }
        /// <summary>
        /// 求职意向
        /// </summary>
        public string objective
        {
            set { _objective = value; }
            get { return _objective; }
        }
        /// <summary>
        /// 工作经验
        /// </summary>
        public string workingExperience
        {
            set { _workingexperience = value; }
            get { return _workingexperience; }
        }
        /// <summary>
        /// 教育背景
        /// </summary>
        public string educationalBackground
        {
            set { _educationalbackground = value; }
            get { return _educationalbackground; }
        }
        /// <summary>
        /// 语言及方言
        /// </summary>
        public string languagesAndDialect
        {
            set { _languagesanddialect = value; }
            get { return _languagesanddialect; }
        }
        #endregion Model

    }
}
