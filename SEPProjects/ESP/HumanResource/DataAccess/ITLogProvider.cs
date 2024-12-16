using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Entity;
using System.Data;
using System.Data.SqlClient;
using ESP.Administrative.Common;
using System.Security.Policy;
using Microsoft.Office.Interop.Word;
using System.Collections;

namespace ESP.HumanResource.DataAccess
{
   public class ITLogProvider
    {

        /// <summary>
        /// 根据条件获取培训活动的集合
        /// </summary>
        /// <param name="sqlWhere">sqlwhere</param>
        /// <returns></returns>
        public List<ESP.HumanResource.Entity.ITLogInfo> GetList(int itemId)
        {
            List<ITLogInfo> list = new List<ITLogInfo>();
            string sql = "select * from SEP_ITLog where ITItemId =@ITItemId ";

            sql += " order by LogTime asc";

            SqlParameter[] parameters = {					
					new SqlParameter("@ITItemId", SqlDbType.Int)         
                                        };
            parameters[0].Value = itemId;

            return CBO.FillCollection<ITLogInfo>(DbHelperSQL.Query(sql.ToString(),parameters));

        }


        /// <summary>
        /// 根据id查询指定培训活动
        /// </summary>
        /// <param name="id">活动id</param>
        /// <returns></returns>
        public ESP.HumanResource.Entity.ITLogInfo GetModel(int id)
        {
            string sql = "select * from SEP_ITLog where Id=@id";
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return CBO.FillObject<ITLogInfo>(DbHelperSQL.Query(sql.ToString(), parameters));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ITOMInfo"></param>
        /// <returns></returns>
        public int Add(ESP.HumanResource.Entity.ITLogInfo ITOMInfo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SEP_ITLog(");
            strSql.Append("ITItemId,UserId,UserName,LogTime,remark,Status)");
            strSql.Append(" values (");
            strSql.Append("@ITItemId,@UserId,@UserName,@LogTime,@remark,@Status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {					
					new SqlParameter("@ITItemId", SqlDbType.Int),
					new SqlParameter("@UserId", SqlDbType.Int),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
                    new SqlParameter("@LogTime", SqlDbType.DateTime),
                      new SqlParameter("@remark", SqlDbType.NVarChar),
                     new SqlParameter("@Status", SqlDbType.Int)             
                                        };
            parameters[0].Value = ITOMInfo.ITItemId;
            parameters[1].Value = ITOMInfo.UserId;
            parameters[2].Value = ITOMInfo.UserName;
            parameters[3].Value = ITOMInfo.LogTime;
            parameters[4].Value = ITOMInfo.remark;
            parameters[5].Value = ITOMInfo.Status;

            object obj = ESP.HumanResource.Common.DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// 根据id删除活动
        /// </summary>
        /// <param name="id">活动id</param>
        /// <returns></returns>
        public int Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SEP_ITLog ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int)
				};
            parameters[0].Value = id;
            int result = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return result;
        }

        /// <summary>
        /// 修改活动
        /// </summary>
        public int Update(ESP.HumanResource.Entity.ITLogInfo ITOMInfo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SEP_ITLog set ");
            strSql.Append("ITItemId=@ITItemId,UserId=@UserId,UserName=@UserName,LogTime=@LogTime,remark=@remark,Status=@Status");
            strSql.Append("where Id=@Id");
            SqlParameter[] parameters = {					
					new SqlParameter("@ITItemId", SqlDbType.Int),
					new SqlParameter("@UserId", SqlDbType.Int),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
                    new SqlParameter("@LogTime", SqlDbType.DateTime),
                      new SqlParameter("@remark", SqlDbType.NVarChar),
                     new SqlParameter("@Status", SqlDbType.Int),
                     new SqlParameter("@Id", SqlDbType.Int) 
                                        };
            parameters[0].Value = ITOMInfo.ITItemId;
            parameters[1].Value = ITOMInfo.UserId;
            parameters[2].Value = ITOMInfo.UserName;
            parameters[3].Value = ITOMInfo.LogTime;
            parameters[4].Value = ITOMInfo.remark;
            parameters[5].Value = ITOMInfo.Status;
            parameters[6].Value = ITOMInfo.Id;
            int result = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return result;
        }

    }
}
