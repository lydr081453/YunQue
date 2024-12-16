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
   public class ITFlowProvider
    {

        /// <summary>
        /// 根据条件获取培训活动的集合
        /// </summary>
        /// <param name="sqlWhere">sqlwhere</param>
        /// <returns></returns>
        public List<ESP.HumanResource.Entity.ITFlowInfo> GetList(string term, List<SqlParameter> param, SqlTransaction trans)
        {
            List<ITFlowInfo> list = new List<ITFlowInfo>();
            string sql = "select * from SEP_ITFlow where 1=1 ";
            if (!string.IsNullOrEmpty(term))
                sql += term;

            return CBO.FillCollection<ITFlowInfo>(DbHelperSQL.Query(sql.ToString(), trans, (param == null ? null : param.ToArray())));

        }

        public IList<ITFlowInfo> GetList(string term, List<SqlParameter> param)
        {
            return GetList(term, param, null);
        }

        /// <summary>
        /// 根据id查询指定培训活动
        /// </summary>
        /// <param name="id">活动id</param>
        /// <returns></returns>
        public ESP.HumanResource.Entity.ITFlowInfo GetModel(int id)
        {
            string sql = "select * from SEP_ITFlow where Id=@id";
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return CBO.FillObject<ITFlowInfo>(DbHelperSQL.Query(sql.ToString(), parameters));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ITOMInfo"></param>
        /// <returns></returns>
        public int Add(ESP.HumanResource.Entity.ITFlowInfo ITOMInfo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SEP_ITFlow(");
            strSql.Append("FlowName,DirectorId,DirectorName,TestId,TestName,PublisherId,PublisherName,status)");
            strSql.Append(" values (");
            strSql.Append("@FlowName,@DirectorId,@DirectorName,@TestId,@TestName,@PublisherId,@PublisherName,@status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {					
					new SqlParameter("@FlowName", SqlDbType.NVarChar),
					new SqlParameter("@DirectorId", SqlDbType.Int),
					new SqlParameter("@DirectorName", SqlDbType.NVarChar),
                    new SqlParameter("@TestId", SqlDbType.Int),
                      new SqlParameter("@TestName", SqlDbType.NVarChar),
                     new SqlParameter("@PublisherId", SqlDbType.Int),
                     new SqlParameter("@PublisherName", SqlDbType.NVarChar),
                     new SqlParameter("@status", SqlDbType.Int)
                                        };
            parameters[0].Value = ITOMInfo.FlowName;
            parameters[1].Value = ITOMInfo.DirectorId;
            parameters[2].Value = ITOMInfo.DirectorName;
            parameters[3].Value = ITOMInfo.TestId;
            parameters[4].Value = ITOMInfo.TestName;
            parameters[5].Value = ITOMInfo.PublisherId;
            parameters[6].Value = ITOMInfo.PublisherName;

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
            strSql.Append("delete SEP_ITFlow ");
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
        public int Update(ESP.HumanResource.Entity.ITFlowInfo ITOMInfo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SEP_ITFlow set ");
            strSql.Append("FlowName,DirectorId,DirectorName,TestId,TestName,PublisherId,PublisherName,");
            strSql.Append("Status=@Status,UserId=@UserId,UserName=@UserName,status=@status where Id=@Id");
            SqlParameter[] parameters = {					
					new SqlParameter("@FlowName", SqlDbType.NVarChar),
					new SqlParameter("@DirectorId", SqlDbType.Int),
					new SqlParameter("@DirectorName", SqlDbType.NVarChar),
                    new SqlParameter("@TestId", SqlDbType.Int),
                      new SqlParameter("@TestName", SqlDbType.NVarChar),
                     new SqlParameter("@PublisherId", SqlDbType.Int),
                     new SqlParameter("@PublisherName", SqlDbType.NVarChar),
                      new SqlParameter("@status", SqlDbType.Int),
                       new SqlParameter("@Id", SqlDbType.Int)
                                        };
            parameters[0].Value = ITOMInfo.FlowName;
            parameters[1].Value = ITOMInfo.DirectorId;
            parameters[2].Value = ITOMInfo.DirectorName;
            parameters[3].Value = ITOMInfo.TestId;
            parameters[4].Value = ITOMInfo.TestName;
            parameters[5].Value = ITOMInfo.PublisherId;
            parameters[6].Value = ITOMInfo.PublisherName;
            parameters[7].Value = ITOMInfo.Status;
            parameters[8].Value = ITOMInfo.Id;
            int result = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return result;
        }

    }
}
