using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ESP.Framework.DataAccess.Utilities;
using ESP.Framework.Entity;
using ESP.Framework.DataAccess;

namespace ESP.Framework.SqlDataAccess
{
    /// <summary>
    /// 部门类型数据访问类
    /// </summary>
    public class DepartmentTypeDataProvider : IDepartmentTypeDataProvider
    {
        #region members of IDepartmentTypeDataProvider

        /// <summary>
        /// 获取指定ID的部门类型
        /// </summary>
        /// <param name="id">类型ID</param>
        /// <returns>部门类型信息对象</returns>
        public ESP.Framework.Entity.DepartmentTypeInfo Get(int id)
        {
            string sql = @"
---------------------------------------------------------
SELECT *
FROM sep_DepartmentTypes
WHERE DepartmentTypeID = @DepartmentTypeID
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "DepartmentTypeID", DbType.Int32, id);
            return CBO.LoadObject<DepartmentTypeInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 获取所有部门类型列表
        /// </summary>
        /// <returns>所有部门类型信息的列表</returns>
        public IList<DepartmentTypeInfo> GetAll()
        {
            string sql = @"
---------------------------------------------------------
SELECT *
FROM sep_DepartmentTypes
ORDER BY DepartmentTypeID ASC
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return CBO.LoadList<DepartmentTypeInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 创建新的部门类型
        /// </summary>
        /// <param name="departmentType">要创建的部门类型对象</param>
        public void Update(ESP.Framework.Entity.DepartmentTypeInfo departmentType)
        {
            // TODO: 检查RowVersion
            string sql = @"
---------------------------------------------------------
UPDATE sep_DepartmentTypes
   SET [DepartmentTypeName] = @DepartmentTypeName
      ,[Description] = @Description
      ,[IsSaleDepartment] = @IsSaleDepartment
      ,[IsSubCompany] = @IsSubCompany
      ,[Status] = @Status
 WHERE DepartmentTypeID=@DepartmentTypeID
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "DepartmentTypeID", DbType.Int32, departmentType.DepartmentTypeID);
            db.AddInParameter(cmd, "DepartmentTypeName", DbType.String, departmentType.DepartmentTypeName);
            db.AddInParameter(cmd, "Description", DbType.String, departmentType.Description);
            db.AddInParameter(cmd, "IsSaleDepartment", DbType.Boolean, departmentType.IsSaleDepartment);
            db.AddInParameter(cmd, "IsSubCompany", DbType.Int32, departmentType.IsSubCompany);
            db.AddInParameter(cmd, "Status", DbType.Boolean, departmentType.Status);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 更新部门类型信息
        /// </summary>
        /// <param name="departmentType">新的部门类型信息</param>
        public void Create(ESP.Framework.Entity.DepartmentTypeInfo departmentType)
        {
            string sql = @"
---------------------------------------------------------
INSERT INTO sep_DepartmentTypes
           ([DepartmentTypeName]
           ,[Description]
           ,[IsSaleDepartment]
           ,[IsSubCompany]
           ,[Status])
     VALUES
           (@DepartmentTypeName
           ,@Description
           ,@IsSaleDepartment
           ,@IsSubCompany
           ,@Status)
SELECT CAST(SCOPE_IDENTITY() AS int)
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "DepartmentTypeID", DbType.Int32, departmentType.DepartmentTypeID);
            db.AddInParameter(cmd, "DepartmentTypeName", DbType.String, departmentType.DepartmentTypeName);
            db.AddInParameter(cmd, "Description", DbType.String, departmentType.Description);
            db.AddInParameter(cmd, "IsSaleDepartment", DbType.Boolean, departmentType.IsSaleDepartment);
            db.AddInParameter(cmd, "IsSubCompany", DbType.Int32, departmentType.IsSubCompany);
            db.AddInParameter(cmd, "Status", DbType.Boolean, departmentType.Status);
            int newDeptTypeID = (int)db.ExecuteScalar(cmd);

            departmentType.DepartmentTypeID = newDeptTypeID;
        }

        /// <summary>
        /// 删除指定ID的部门类型
        /// </summary>
        /// <param name="id">要删除的部门类型的ID</param>
        public void Delete(int id)
        {
            string sql = @"
---------------------------------------------------------
DELETE sep_DepartmentTypes
WHERE DepartmentTypeID = @DepartmentTypeID
---------------------------------------------------------
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "DepartmentTypeID", DbType.Int32, id);
            db.ExecuteNonQuery(cmd);
        }

        #endregion
    }
}
