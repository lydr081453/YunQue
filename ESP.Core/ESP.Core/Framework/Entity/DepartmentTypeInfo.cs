using System;
namespace ESP.Framework.Entity
{
    ///<summary>
    ///部门分类
    ///</summary>
    [Serializable]
    public class DepartmentTypeInfo
    {
        #region "variables"
        private Int32 _DepartmentTypeID;
        private String _DepartmentTypeName;
        private String _Description;
        private Boolean _IsSaleDepartment;
        private Boolean _IsSubCompany;
        private Int32 _Status;
        private Byte[] _RowVersion;
        #endregion

		
        #region "Properties"
        ///<summary>
        ///标识列
        ///</summary>
        public Int32 DepartmentTypeID
        {
            get{return _DepartmentTypeID;}
            set{_DepartmentTypeID = value;}
        }
        ///<summary>
        ///部门类别名称
        ///</summary>
        public String DepartmentTypeName
        {
            get{return _DepartmentTypeName;}
            set{_DepartmentTypeName = value;}
        }
        ///<summary>
        ///描述
        ///</summary>
        public String Description
        {
            get{return _Description;}
            set{_Description = value;}
        }
        ///<summary>
        ///是否是销售部门
        ///</summary>
        public Boolean IsSaleDepartment
        {
            get{return _IsSaleDepartment;}
            set{_IsSaleDepartment = value;}
        }
        ///<summary>
        ///是否是子公司
        ///</summary>
        public Boolean IsSubCompany
        {
            get{return _IsSubCompany;}
            set{_IsSubCompany = value;}
        }
        ///<summary>
        ///状态
        ///</summary>
        public Int32 Status
        {
            get{return _Status;}
            set{_Status = value;}
        }

        /// <summary>
        /// 记录版本
        /// </summary>
        public Byte[] RowVersion
        {
            get{return _RowVersion;}
            set{_RowVersion = value;}
        }
        #endregion
    }
}
