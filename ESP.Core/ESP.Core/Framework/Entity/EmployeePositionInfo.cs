using System;
using System.Collections.Generic;
using System.Text;

namespace ESP.Framework.Entity
{
    /// <summary>
    /// 员工职务信息
    /// </summary>
    [Serializable]
    public class EmployeePositionInfo
    {
        #region Fields
        int _UserID;
        string _Username;
        int _DepartmentPositionID;
        string _DepartmentPositionName;
        int _DepartmentID;
        string _DepartmentName;
        int _PositionLevel;
        bool _IsManager;
        bool _IsActing;
        string _UsernameCN;
        #endregion

        /// <summary>
        /// 员工ID
        /// </summary>
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
        /// <summary>
        /// 员工的用户名
        /// </summary>
        public string Username
        {
            get { return _Username; }
            set { _Username = value; }
        }
        /// <summary>
        /// 职务级别
        /// </summary>
        public int PositionLevel
        {
            get { return _PositionLevel; }
            set { _PositionLevel = value; }
        }
        /// <summary>
        /// 职务名称
        /// </summary>
        public string DepartmentPositionName
        {
            get { return _DepartmentPositionName; }
            set { _DepartmentPositionName = value; }
        }
        /// <summary>
        /// 职务ID
        /// </summary>
        public int DepartmentPositionID
        {
            get { return _DepartmentPositionID; }
            set { _DepartmentPositionID = value; }
        }
        /// <summary>
        /// 部门ID
        /// </summary>
        public int DepartmentID
        {
            get { return _DepartmentID; }
            set { _DepartmentID = value; }
        }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName
        {
            get { return _DepartmentName; }
            set { _DepartmentName = value; }
        }
        /// <summary>
        /// 是否是经理职务
        /// </summary>
        public bool IsManager
        {
            get { return _IsManager; }
            set { _IsManager = value; }
        }
        /// <summary>
        /// 是否是临时职务
        /// </summary>
        public bool IsActing
        {
            get { return _IsActing; }
            set { _IsActing = value; }
        }

        /// <summary>
        /// 用户中文名
        /// </summary>
        public string UsernameCN
        {
            get { return _UsernameCN; }
            set { _UsernameCN = value; }
        }
    }
}