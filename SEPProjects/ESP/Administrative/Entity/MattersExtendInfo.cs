using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Framework.Entity;
using ESP.Administrative.Common;

namespace ESP.Administrative.Entity
{
    /// <summary>
    /// 考勤事宜信息表
    /// </summary>
    public class MattersExtendInfo : MattersInfo
    {
        public MattersExtendInfo()
        { }
        #region Model

        private decimal _overtimehours;
        private string _overtimecause;
        private int _overtimetype;
        private int _state;
        private string _approveremark;
        private int _operateordept;
        private int _sort;
        private string _usercode;
        private string _employeename;
        private DateTime _apptime;
        private int _approvestate;
        private decimal _remaininghours;
        private int _formtype;


        /// <summary>
        /// 员工编号
        /// </summary>
        public string UserCode
        {
            set { _usercode = value; }
            get { return _usercode; }
        }
        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string EmployeeName
        {
            set { _employeename = value; }
            get { return _employeename; }
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime AppTime
        {
            set { _apptime = value; }
            get { return _apptime; }
        }
        /// <summary>
        /// OT小时数
        /// </summary>
        public decimal OverTimeHours
        {
            set { _overtimehours = value; }
            get { return _overtimehours; }
        }
        /// <summary>
        /// OT事由
        /// </summary>
        public string OverTimeCause
        {
            set { _overtimecause = value; }
            get { return _overtimecause; }
        }
        /// <summary>
        /// OT类型，1工作日,2节假日
        /// </summary>
        public int OverTimeType
        {
            set { _overtimetype = value; }
            get { return _overtimetype; }
        }




        /// <summary>
        /// OT单剩余小时数
        /// </summary>
        public decimal RemainingHours
        {
            set { _remaininghours = value; }
            get { return _remaininghours; }
        }

        /// <summary>
        /// 总监审批批示
        /// </summary>
        public string ApproveRemark
        {
            set { _approveremark = value; }
            get { return _approveremark; }
        }
        
        
        /// <summary>
        /// 操作人部门
        /// </summary>
        public int OperateorDept
        {
            set { _operateordept = value; }
            get { return _operateordept; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort
        {
            set { _sort = value; }
            get { return _sort; }
        }
        /// <summary>
        /// OT单状态
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }

        /// <summary>
        /// 单据类型
        /// </summary>
        public int FormType
        {
            set { _formtype = value; }
            get { return _formtype; }
        }
        /// <summary>
        /// 审批状态
        /// </summary>
        public int ApproveState
        {
            set { _approvestate = value; }
            get { return _approvestate; }

        }
        #endregion Model


    }
}