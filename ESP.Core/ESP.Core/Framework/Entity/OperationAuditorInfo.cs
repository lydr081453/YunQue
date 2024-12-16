using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESP.Framework.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class OperationAuditorInfo
    {
        #region Model

        /// <summary>
        /// 流水号
        /// </summary>
        /// <value>The id.</value>
        public int Id { get; set; }

        /// <summary>
        /// 部门编号
        /// </summary>
        /// <value>The dep id.</value>
        public int DepId { get; set; }

        /// <summary>
        /// 总监编号
        /// </summary>
        /// <value>The director id.</value>
        public int DirectorId { get; set; }

        /// <summary>
        /// 总监姓名
        /// </summary>
        /// <value>The name of the director.</value>
        public string DirectorName { get; set; }

        /// <summary>
        /// 总经理编号
        /// </summary>
        /// <value>The manager id.</value>
        public int ManagerId { get; set; }

        /// <summary>
        /// 总经理姓名
        /// </summary>
        /// <value>The name of the manager.</value>
        public string ManagerName { get; set; }

        /// <summary>
        /// CEO编号
        /// </summary>
        /// <value>The CEO id.</value>
        public int CEOId { get; set; }

        /// <summary>
        /// CEO姓名
        /// </summary>
        /// <value>The name of the CEO.</value>
        public string CEOName { get; set; }

        /// <summary>
        /// 部门考勤审批人编号
        /// </summary>
        /// <value>The manager id.</value>
        public int AttendanceId { get; set; }

        /// <summary>
        /// 部门考勤审批人姓名
        /// </summary>
        /// <value>The name of the manager.</value>
        public string AttendanceName { get; set; }

        /// <summary>
        /// HR审批人编号
        /// </summary>
        /// <value>The CEO id.</value>
        public int HRId { get; set; }

        /// <summary>
        /// HR审批人姓名
        /// </summary>
        /// <value>The name of the CEO.</value>
        public string HRName { get; set; }

        /// <summary>
        /// FA审批人编号
        /// </summary>
        /// <value>The CEO id.</value>
        public int FAId { get; set; }

        /// <summary>
        /// FA审批人姓名
        /// </summary>
        /// <value>The name of the CEO.</value>
        public string FAName { get; set; }

        /// <summary>
        /// 人力考勤审批人ID
        /// </summary>
        public int Hrattendanceid { get; set; }

        /// <summary>
        /// 人力考勤审批人姓名
        /// </summary>
        public string Hrattendancename { get; set; }

        /// <summary>
        /// 行政管理员ID
        /// </summary>
        public int ADManagerID { get; set; }

        /// <summary>
        /// 行政管理员姓名
        /// </summary>
        public string ADManagerName { get; set; }

        #endregion Model

    }
}
