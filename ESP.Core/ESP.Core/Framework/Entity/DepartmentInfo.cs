using System;
namespace ESP.Framework.Entity
{
    ///<summary>
    ///部门
    ///</summary>
    [Serializable]
    public class DepartmentInfo
    {
        #region "Properties"
        ///<summary>
        ///标识列
        ///</summary>
        public Int32 DepartmentID{get;set;}

        ///<summary>
        ///部门名称
        ///</summary>
        public String DepartmentName { get; set; }


        /// <summary>
        /// 部门代码
        /// </summary>
        public String DepartmentCode { get; set; }

        ///<summary>
        ///描述
        ///</summary>
        public String Description { get; set; }
        ///<summary>
        ///上级一部门ID
        ///</summary>
        public Int32 ParentID { get; set; }
        ///<summary>
        ///部门级别
        ///</summary>
        public Int32 DepartmentLevel { get; set; }
        ///<summary>
        ///部门分类
        ///</summary>
        public Int32 DepartmentTypeID { get; set; }

        /// <summary>
        /// 部门分类名称
        /// </summary>
        public String DepartmentTypeName { get; set; }
        ///<summary>
        ///部门排序
        ///</summary>
        public Int32 Ordinal { get; set; }

        /// <summary>
        /// 是否是销售部门
        /// </summary>
        public Boolean IsSaleDepartment { get; set; }

        /// <summary>
        /// 是否是子公司
        /// </summary>
        public Boolean IsSubCompany { get; set; }


        ///<summary>
        ///部门状态
        ///</summary>
        public Int32 Status { get; set; }

        /// <summary>
        /// 行版本号
        /// </summary>
        public Byte[] RowVersion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal SalaryAmount { get; set; }
        #endregion
    }
}
