using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.HumanResource.Entity
{
    [Serializable]
    public class EmployeesInPositionsInfo
    {
        public EmployeesInPositionsInfo()
        { }
        #region Model
        private int _userid;
        private int _departmentpositionid;
        private bool _ismanager;
        private bool _isacting;
        private Byte[] _rowversion;
        private int _departmentid;

        private string _userName;
        private string _usercode;

        public string UserCode
        {
            get { return _usercode; }
            set { _usercode = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        private string _departmentName;

        public string DepartmentName
        {
            get { return _departmentName; }
            set { _departmentName = value; }
        }
        private string _departmentPositionName;

        public DateTime BeginDate { get; set; }
        public string DepartmentPositionName
        {
            get { return _departmentPositionName; }
            set { _departmentPositionName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DepartmentPositionID
        {
            set { _departmentpositionid = value; }
            get { return _departmentpositionid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsManager
        {
            set { _ismanager = value; }
            get { return _ismanager; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsActing
        {
            set { _isacting = value; }
            get { return _isacting; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Byte[] RowVersion
        {
            set { _rowversion = value; }
            get { return _rowversion; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DepartmentID
        {
            set { _departmentid = value; }
            get { return _departmentid; }
        }

        private int _companyID;

        public int CompanyID
        {
            set { _companyID = value; }
            get { return _companyID; }
        }

        private string _companyName;

        public string CompanyName
        {
            set { _companyName = value; }
            get { return _companyName; }
        }

        private int _groupID;
        public int GroupID
        {
            set { _groupID = value; }
            get { return _groupID; }
        }

        private string _groupName;
        public string GroupName
        {
            set { _groupName = value; }
            get { return _groupName; }
        }

        private string _workcity;
        /// <summary>
        /// 办公公司，存的是ID
        /// </summary>
        public string WorkCity
        {
            set { _workcity = value; }
            get { return _workcity; }
        }

        private string _workcountry;
        /// <summary>
        /// 办公部门，存的是ID
        /// </summary>
        public string WorkCountry
        {
            set { _workcountry = value; }
            get { return _workcountry; }
        }

        private string _workaddress;
        /// <summary>
        /// 办公组别，存的是ID
        /// </summary>
        public string WorkAddress
        {
            set { _workaddress = value; }
            get { return _workaddress; }
        }

        private string _email;
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        public string LevelName { get; set; }
        #endregion Model

    }
}
