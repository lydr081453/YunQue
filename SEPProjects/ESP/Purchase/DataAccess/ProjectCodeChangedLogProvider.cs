using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ESP.Purchase.DataAccess
{
    public class ProjectCodeChangedLogProvider
    {

        public static void Add(Entity.ProjectCodeChangedInfo Entity)
        {
            Add(Entity, null);
        }
        /// <summary>
        /// 插入项目号变更记录
        /// </summary>
        /// <param name="Entity"></param>
        /// <param name="trans"></param>
        public static void Add(Entity.ProjectCodeChangedInfo Entity,SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"INSERT INTO [T_ProjectCodeChangedLog]
                           ([dataType],[dataId],[changedUserId],[changedUserName],[changedTime],[oldProjectCode])
                     VALUES
                           (@dataType,@dataId,@changedUserId,@changedUserName,@changedTime,@oldProjectCode)");
            SqlParameter[] parms = { 
                        new SqlParameter("@dataType",SqlDbType.Int,4),
                        new SqlParameter("@dataId",SqlDbType.Int,4),
                        new SqlParameter("@changedUserId",SqlDbType.Int,4),
                        new SqlParameter("@changedUserName",SqlDbType.NVarChar,50),
                        new SqlParameter("@changedTime",SqlDbType.DateTime,8),
                        new SqlParameter("@oldProjectCode",SqlDbType.VarChar,30)};
            parms[0].Value = Entity.DataType;
            parms[1].Value = Entity.DataId;
            parms[2].Value = Entity.ChangedUserId;
            parms[3].Value = Entity.ChangedUserName;
            parms[4].Value = DateTime.Now;
            parms[5].Value = Entity.OldProjectCode;
            if(trans == null)
                Common.DbHelperSQL.ExecuteSql(strSql.ToString(), parms);
            else
                Common.DbHelperSQL.ExecuteSql(strSql.ToString(),trans.Connection,trans, parms);
        }

        /// <summary>
        /// 项目项目号变更列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static List<Entity.ProjectCodeChangedInfo> GetChangedListForProject(int projectId)
        {
            return GetChangedList(projectId, (int)Common.State.DataType.Project);
        }

        /// <summary>
        /// 申请单项目号变更列表
        /// </summary>
        /// <param name="purchaseId"></param>
        /// <returns></returns>
        public static List<Entity.ProjectCodeChangedInfo> GetChangedListForPurchase(int purchaseId)
        {
            return GetChangedList(purchaseId, (int)Common.State.DataType.GR);
        }

        /// <summary>
        /// 项目变更信息列表
        /// </summary>
        /// <param name="dataId">数据ID</param>
        /// <param name="dataType">数据类型</param>
        /// <returns></returns>
        private static List<Entity.ProjectCodeChangedInfo> GetChangedList(int dataId, int dataType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM T_ProjectCodeChangedLog WHERE dataType=@dataType and dataId=@dataId ORDER BY ID DESC");
            SqlParameter[] parms = { 
                        new SqlParameter("@dataType",SqlDbType.Int,4),
                        new SqlParameter("@dataId",SqlDbType.Int,4)};
            parms[0].Value = dataType;
            parms[1].Value = dataId;
            return ESP.Finance.Utility.CBO.FillCollection<Entity.ProjectCodeChangedInfo>(Common.DbHelperSQL.Query(strSql.ToString(), parms));
        }
    }
}
