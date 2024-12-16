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
   public class ITItemsDataProvider
    {

        /// <summary>
        /// 根据条件获取培训活动的集合
        /// </summary>
        /// <param name="sqlWhere">sqlwhere</param>
        /// <returns></returns>
       public List<ESP.HumanResource.Entity.ITItemsInfo> GetList(string term, List<SqlParameter> param,SqlTransaction trans)
        {
            List<ITItemsInfo> list = new List<ITItemsInfo>();
            string sql = "select * from SEP_ITItems where 1=1 ";
            if (!string.IsNullOrEmpty(term))
                sql += term;
            sql += " order by createTime desc";


            return CBO.FillCollection<ITItemsInfo>(DbHelperSQL.Query(sql.ToString(), trans, (param == null ? null : param.ToArray())));

        }

       public IList<ITItemsInfo> GetList(string term, List<SqlParameter> param)
       {
           return GetList(term, param, null);
       }

        /// <summary>
        /// 根据id查询指定培训活动
        /// </summary>
        /// <param name="id">活动id</param>
        /// <returns></returns>
        public ESP.HumanResource.Entity.ITItemsInfo GetModel(int id)
        {
            string sql = "select * from SEP_ITItems where Id=@id";
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return CBO.FillObject<ITItemsInfo>(DbHelperSQL.Query(sql.ToString(), parameters));
        }


/// <summary>
/// 
/// </summary>
/// <param name="ITOMInfo"></param>
/// <returns></returns>
        public int Add(ESP.HumanResource.Entity.ITItemsInfo ITOMInfo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SEP_ITItems(");
            strSql.Append("Title,Description,CreateTime,FlowId,FlowName,Status,UserId,UserName,AuditorId,Auditor)");
            strSql.Append(" values (");
            strSql.Append("@Title,@Description,@CreateTime,@FlowId,@FlowName,@Status,@UserId,@UserName,@AuditorId,@Auditor)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {					
					new SqlParameter("@Title", SqlDbType.NVarChar),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@FlowId", SqlDbType.Int),
                      new SqlParameter("@FlowName", SqlDbType.NVarChar),
                     new SqlParameter("@Status", SqlDbType.Int),
                     new SqlParameter("@UserId", SqlDbType.Int),
                     new SqlParameter("@UserName", SqlDbType.NVarChar),     
                     new SqlParameter("@AuditorId", SqlDbType.Int),
                     new SqlParameter("@Auditor", SqlDbType.NVarChar)
                                        };
            parameters[0].Value = ITOMInfo.Title;
            parameters[1].Value = ITOMInfo.Description;
            parameters[2].Value = ITOMInfo.CreateTime;
            parameters[3].Value = ITOMInfo.FlowId;
            parameters[4].Value = ITOMInfo.FlowName;
            parameters[5].Value = ITOMInfo.Status;
            parameters[6].Value = ITOMInfo.UserId;
            parameters[7].Value = ITOMInfo.UserName;
            parameters[8].Value = ITOMInfo.AuditorId;
            parameters[9].Value = ITOMInfo.Auditor;


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
            strSql.Append("delete SEP_ITItems ");
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
        public int Update(ESP.HumanResource.Entity.ITItemsInfo ITOMInfo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SEP_ITItems set ");
            strSql.Append("Title=@Title,Description=@Description,CreateTime=@CreateTime,FlowId=@FlowId,FlowName=@FlowName,AuditorId=@AuditorId,Auditor=@Auditor,");
            strSql.Append("Status=@Status,UserId=@UserId,UserName=@UserName where Id=@Id");
            SqlParameter[] parameters = {					
					new SqlParameter("@Title", SqlDbType.NVarChar),
					new SqlParameter("@Description", SqlDbType.Text),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
                    new SqlParameter("@FlowId", SqlDbType.Int),
                    new SqlParameter("@FlowName", SqlDbType.NVarChar),
                     new SqlParameter("@Status", SqlDbType.Int),
                     new SqlParameter("@UserId", SqlDbType.Int),
                     new SqlParameter("@UserName", SqlDbType.NVarChar),
                     new SqlParameter("@AuditorId", SqlDbType.Int),
                     new SqlParameter("@Auditor", SqlDbType.NVarChar),
                     new SqlParameter("@Id", SqlDbType.Int)
                                        };
            parameters[0].Value = ITOMInfo.Title;
            parameters[1].Value = ITOMInfo.Description;
            parameters[2].Value = ITOMInfo.CreateTime;
            parameters[3].Value = ITOMInfo.FlowId;
            parameters[4].Value = ITOMInfo.FlowName;
            parameters[5].Value = ITOMInfo.Status;
            parameters[6].Value = ITOMInfo.UserId;
            parameters[7].Value = ITOMInfo.UserName;
            parameters[8].Value = ITOMInfo.AuditorId;
            parameters[9].Value = ITOMInfo.Auditor;
            parameters[10].Value = ITOMInfo.Id;
            int result = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return result;
        }

    }
}
