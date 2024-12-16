using System;
namespace ESP.Framework.Entity
{
    ///<summary>
    ///部门职务表
    ///</summary>
    [Serializable]
    public class DepartmentPositionInfo
    {

        #region "Properties"
        ///<summary>
        ///标识列
        ///</summary>
        public Int32 DepartmentPositionID { get; set; }
        ///<summary>
        ///职务名称
        ///</summary>
        public String DepartmentPositionName { get; set; }

        /// <summary>
        /// 部门代码
        /// </summary>
        public String PositionCode { get; set; }

        ///<summary>
        ///描述
        ///</summary>
        public String Description { get; set; }
        ///<summary>
        ///所属部门ID
        ///</summary>
        public Int32 DepartmentID { get; set; }
        /// <summary>
        /// 所属部门名称
        /// </summary>
        public String DepartmentName { get; set; }

        ///<summary>
        ///职务级别
        ///</summary>
        public Int32 PositionLevel { get; set; }
        /// <summary>
        /// 记录版本
        /// </summary>
        public Byte[] RowVersion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PositionBaseId { get; set; }
        #endregion
    }
}
