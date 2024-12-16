using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using ESP.Framework.Entity;

namespace ESP.Framework.SqlDataAccess
{
    /// <summary>
    /// 审核人管理
    /// </summary>
    public class OperationAuditorDataProvider : ESP.Framework.DataAccess.IOperationAuditorDataProvider
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public bool Exists(int id)
        {
            string sql = @"
SELECT COUNT(*) FROM sep_OperationAuditManage WHERE Id=@Id 
";

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "Id", System.Data.DbType.Int32, id);
            object obj = db.ExecuteScalar(cmd);
            int count = (obj is int) ? (int)obj : 0;
            return count > 0;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int Add(OperationAuditorInfo model)
        {
            string sql = @"
INSERT INTO sep_OperationAuditManage(DepId,DirectorId,DirectorName,ManagerId,ManagerName,CEOId,CEOName,AttendanceId,AttendanceName,HRId,HRName,FAId,FAName,Hrattendanceid,Hrattendancename, ADManagerID, ADManagerName)
VALUES (@DepId,@DirectorId,@DirectorName,@ManagerId,@ManagerName,@CEOId,@CEOName,@AttendanceId,@AttendanceName,@HRId,@HRName,@FAId,@FAName,@Hrattendanceid,@Hrattendancename, @ADManagerID, @ADManagerName)
SELECT CAST(SCOPE_IDENTITY() AS int)
";

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@DepId", System.Data.DbType.Int32, model.DepId);
            db.AddInParameter(cmd, "@DirectorId", System.Data.DbType.Int32, model.DirectorId);
            db.AddInParameter(cmd, "@DirectorName", System.Data.DbType.String, model.DirectorName);
            db.AddInParameter(cmd, "@ManagerId", System.Data.DbType.Int32, model.ManagerId);
            db.AddInParameter(cmd, "@ManagerName", System.Data.DbType.String, model.ManagerName);
            db.AddInParameter(cmd, "@CEOId", System.Data.DbType.Int32, model.CEOId);
            db.AddInParameter(cmd, "@CEOName", System.Data.DbType.String, model.CEOName);
            db.AddInParameter(cmd, "@AttendanceId", System.Data.DbType.Int32, model.AttendanceId);
            db.AddInParameter(cmd, "@AttendanceName", System.Data.DbType.String, model.AttendanceName);
            db.AddInParameter(cmd, "@HRId", System.Data.DbType.Int32, model.HRId);
            db.AddInParameter(cmd, "@HRName", System.Data.DbType.String, model.HRName);
            db.AddInParameter(cmd, "@FAId", System.Data.DbType.Int32, model.FAId);
            db.AddInParameter(cmd, "@FAName", System.Data.DbType.String, model.FAName);
            db.AddInParameter(cmd, "@Hrattendanceid", System.Data.DbType.Int32, model.Hrattendanceid);
            db.AddInParameter(cmd, "@Hrattendancename", System.Data.DbType.String, model.Hrattendancename);
            db.AddInParameter(cmd, "@ADManagerID", System.Data.DbType.Int32, model.ADManagerID);
            db.AddInParameter(cmd, "@ADManagerName", System.Data.DbType.String, model.ADManagerName);

            object obj = db.ExecuteScalar(cmd);
            int id = (obj is int) ? (int)obj : 0;
            return id;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(OperationAuditorInfo model)
        {
            string sql = @"
UPDATE sep_OperationAuditManage SET
    DepId=@DepId,
    DirectorId=@DirectorId,
    DirectorName=@DirectorName,
    ManagerId=@ManagerId,
    ManagerName=@ManagerName,
    CEOId=@CEOId,
    CEOName=@CEOName,
    AttendanceId=@AttendanceId,
    AttendanceName=@AttendanceName,
    HRId=@HRId,
    HRName=@HRName,
    FAId=@FAId,
    FAName=@FAName,
    Hrattendanceid=@Hrattendanceid,
    Hrattendancename=@Hrattendancename,
    ADManagerID=@ADManagerID,
    ADManagerName=@ADManagerName
WHERE Id=@Id 
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", System.Data.DbType.Int32, model.Id);
            db.AddInParameter(cmd, "@DepId", System.Data.DbType.Int32, model.DepId);
            db.AddInParameter(cmd, "@DirectorId", System.Data.DbType.Int32, model.DirectorId);
            db.AddInParameter(cmd, "@DirectorName", System.Data.DbType.String, model.DirectorName);
            db.AddInParameter(cmd, "@ManagerId", System.Data.DbType.Int32, model.ManagerId);
            db.AddInParameter(cmd, "@ManagerName", System.Data.DbType.String, model.ManagerName);
            db.AddInParameter(cmd, "@CEOId", System.Data.DbType.Int32, model.CEOId);
            db.AddInParameter(cmd, "@CEOName", System.Data.DbType.String, model.CEOName);
            db.AddInParameter(cmd, "@AttendanceId", System.Data.DbType.Int32, model.AttendanceId);
            db.AddInParameter(cmd, "@AttendanceName", System.Data.DbType.String, model.AttendanceName);
            db.AddInParameter(cmd, "@HRId", System.Data.DbType.Int32, model.HRId);
            db.AddInParameter(cmd, "@HRName", System.Data.DbType.String, model.HRName);
            db.AddInParameter(cmd, "@FAId", System.Data.DbType.Int32, model.FAId);
            db.AddInParameter(cmd, "@FAName", System.Data.DbType.String, model.FAName);
            db.AddInParameter(cmd, "@Hrattendanceid", System.Data.DbType.Int32, model.Hrattendanceid);
            db.AddInParameter(cmd, "@Hrattendancename", System.Data.DbType.String, model.Hrattendancename);
            db.AddInParameter(cmd, "@ADManagerID", System.Data.DbType.Int32, model.ADManagerID);
            db.AddInParameter(cmd, "@ADManagerName", System.Data.DbType.String, model.ADManagerName);

            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id">The id.</param>
        public void Delete(int id)
        {
            string sql = @"
DELETE sep_OperationAuditManage WHERE Id=@Id
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", System.Data.DbType.Int32, id);

            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public OperationAuditorInfo GetModel(int id)
        {
            string sql = @"
SELECT  TOP 1 * FROM sep_OperationAuditManage WHERE Id=@Id
";

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", System.Data.DbType.Int32, id);

            return ESP.Framework.DataAccess.Utilities.CBO.LoadObject<OperationAuditorInfo>(db.ExecuteReader(cmd));
        }

        /// <summary>
        /// 根据部门ID获得一个对象实体
        /// </summary>
        /// <param name="departmentId">The dep id.</param>
        /// <returns></returns>
        public OperationAuditorInfo GetModelByDepId(int departmentId)
        {
            string sql = @"
SELECT  TOP 1 * FROM sep_OperationAuditManage WHERE DepId=@DepId
";

            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@DepId", System.Data.DbType.Int32, departmentId);

            return ESP.Framework.DataAccess.Utilities.CBO.LoadObject<OperationAuditorInfo>(db.ExecuteReader(cmd));
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <returns></returns>
        public IList<OperationAuditorInfo> GetList(string strWhere)
        {
            string sql = @"SELECT * FROM sep_OperationAuditManage";
            if (strWhere != null)
                strWhere = strWhere.Trim();
            if (strWhere != null || strWhere.Length == 0)
            {
                sql += " WHERE " + strWhere;
            }
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return ESP.Framework.DataAccess.Utilities.CBO.LoadList<OperationAuditorInfo>(db.ExecuteReader(cmd));
        }


        /// <summary>
        /// 获得总监的sysids
        /// </summary>
        /// <returns></returns>
        public int[] GetAllDirectorIds()
        {
            string sql = @"
SELECT DISTINCT DirectorId FROM sep_OperationAuditManage
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return ESP.Framework.DataAccess.Utilities.CBO.LoadScalarList<Int32>(db.ExecuteReader(cmd), false).ToArray();
        }



        /// <summary>
        /// 获得总经理的sysids
        /// </summary>
        /// <returns></returns>
        public int[] GetAllManagerIds()
        {
            string sql = @"
SELECT DISTINCT ManagerId FROM sep_OperationAuditManage
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return ESP.Framework.DataAccess.Utilities.CBO.LoadScalarList<Int32>(db.ExecuteReader(cmd), false).ToArray();
        }
        

        /// <summary>
        /// 获得考勤审批人的sysids
        /// </summary>
        /// <returns></returns>
        public int[] GetAllAttendanceIds()
        {
            string sql = @"
SELECT DISTINCT AttendanceId FROM sep_OperationAuditManage
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return ESP.Framework.DataAccess.Utilities.CBO.LoadScalarList<Int32>(db.ExecuteReader(cmd), false).ToArray();
        }


        /// <summary>
        /// 获得CEO的sysids
        /// </summary>
        /// <returns></returns>
        public int[] GetAllCEOIds()
        {
            string sql = @"
SELECT DISTINCT CEOID FROM sep_OperationAuditManage
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return ESP.Framework.DataAccess.Utilities.CBO.LoadScalarList<Int32>(db.ExecuteReader(cmd), false).ToArray();
        }


        /// <summary>
        /// 获得HR审批人的sysids
        /// </summary>
        /// <returns></returns>
        public int[] GetAllHRIds()
        {
            string sql = @"
SELECT DISTINCT HRId FROM sep_OperationAuditManage
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return ESP.Framework.DataAccess.Utilities.CBO.LoadScalarList<Int32>(db.ExecuteReader(cmd), false).ToArray();
        }


        /// <summary>
        /// 获得行政管理员的sysids
        /// </summary>
        /// <returns></returns>
        public int[] GetAllADManagerIds()
        {
            string sql = @"
SELECT DISTINCT ADManagerID FROM sep_OperationAuditManage
";
            Database db = DatabaseFactory.CreateDatabase(ESP.Configuration.ConfigurationManager.ConnectionStringName);
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return ESP.Framework.DataAccess.Utilities.CBO.LoadScalarList<Int32>(db.ExecuteReader(cmd), false).ToArray();
        }

        #endregion  成员方法
    }
}
