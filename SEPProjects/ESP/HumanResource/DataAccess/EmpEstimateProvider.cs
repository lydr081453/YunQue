using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using System.Data;
using ESP.HumanResource.Entity;
using ESP.HumanResource.Utilities;

namespace ESP.HumanResource.DataAccess
{
    public class EmpEstimateProvider
    {
        public EmpEstimateProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.HumanResource.Entity.EmpEstimateInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO sep_EmpEstimate(");
            strSql.Append("UserId,EstimateType,EstimateDate,Result,Remark,CreatorId,Creator,Status)");
            strSql.Append(" VALUES (");
            strSql.Append("@UserId,@EstimateType,@EstimateDate,@Result,@Remark,@CreatorId,@Creator,@Status)");
            strSql.Append(";SELECT @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@EstimateType", SqlDbType.NVarChar),
					new SqlParameter("@EstimateDate", SqlDbType.DateTime),
					new SqlParameter("@Result", SqlDbType.NVarChar),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
                    new SqlParameter("@CreatorId", SqlDbType.Int,4),
					new SqlParameter("@Creator", SqlDbType.NVarChar),
                    new SqlParameter("@Status", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.EstimateType;
            parameters[2].Value = model.EstimateDate;
            parameters[3].Value = model.Result;
            parameters[4].Value = model.Remark;
            parameters[5].Value = model.CreatorId;
            parameters[6].Value = model.Creator;
            parameters[7].Value = model.Status;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.HumanResource.Entity.EmpEstimateInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE sep_EmpEstimate SET ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("EstimateType=@EstimateType,EstimateDate=@EstimateDate,Result=@Result,Remark=@Remark,CreatorId=@CreatorId,Creator=@Creator,Status=@Status where EstimateId=@EstimateId");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@EstimateType", SqlDbType.NVarChar),
					new SqlParameter("@EstimateDate", SqlDbType.DateTime),
					new SqlParameter("@Result", SqlDbType.NVarChar),
					new SqlParameter("@Remark", SqlDbType.NVarChar),
					new SqlParameter("@CreatorId", SqlDbType.Int,4),
                    new SqlParameter("@Creator", SqlDbType.NVarChar),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                        new SqlParameter("@EstimateId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.EstimateType;
            parameters[2].Value = model.EstimateDate;
            parameters[3].Value = model.Result;
            parameters[4].Value = model.Remark;
            parameters[5].Value = model.CreatorId;
            parameters[6].Value = model.Creator;
            parameters[7].Value = model.Status;
            parameters[8].Value = model.EstimateId;
         
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int EstimateId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete sep_EmpEstimate ");
            strSql.Append(" where EstimateId=@EstimateId");
            SqlParameter[] parameters = {
					new SqlParameter("@EstimateId", SqlDbType.Int,4)
				};
            parameters[0].Value = EstimateId;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.HumanResource.Entity.EmpEstimateInfo GetModel(int EstimateId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM sep_EmpEstimate ");
            strSql.Append(" WHERE EstimateId=@EstimateId");
            SqlParameter[] parameters = {
					new SqlParameter("@EstimateId", SqlDbType.Int,4)};
            parameters[0].Value = EstimateId;
            ESP.HumanResource.Entity.EmpEstimateInfo model = new ESP.HumanResource.Entity.EmpEstimateInfo();
            return CBO.FillObject<EmpEstimateInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
            
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ESP.HumanResource.Entity.EmpEstimateInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * ");
            strSql.Append(" FROM sep_EmpEstimate ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return CBO.FillCollection<EmpEstimateInfo>(DbHelperSQL.Query(strSql.ToString()));
        }
        #endregion  成员方法
    }
}
