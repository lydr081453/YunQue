using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Collections.Generic;

namespace ESP.Finance.DataAccess
{
    internal class TaxDetailDataProvider : ESP.Finance.IDataAccess.ITaxDetailProvider
    {

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_TaxDetail");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.TaxDetailInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_TaxDetail(ProjectId,ReturnId,Total,Tax,CurrentDate,TaxDate,UserId,UserName,Status,ProjectCode,ReturnCode,DepartmentId,DepartmentName,AuditerId,Auditer,AuditDate");
            strSql.Append(")");
            strSql.Append(" values (");
            strSql.Append("@ProjectId,@ReturnId,@Total,@Tax,@CurrentDate,@TaxDate,@UserId,@UserName,@Status,@ProjectCode,@ReturnCode,@DepartmentId,@DepartmentName,@AuditerId,@Auditer,@AuditDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectId", SqlDbType.Int),
					new SqlParameter("@ReturnId", SqlDbType.Int),
					new SqlParameter("@Total", SqlDbType.Decimal,20),
					new SqlParameter("@Tax", SqlDbType.Decimal,20),
					new SqlParameter("@CurrentDate", SqlDbType.DateTime),
                    new SqlParameter("@TaxDate", SqlDbType.DateTime),
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@ReturnCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@DepartmentId", SqlDbType.Int),
                    new SqlParameter("@DepartmentName", SqlDbType.NVarChar,50),
                    new SqlParameter("@AuditerId", SqlDbType.Int),
                    new SqlParameter("@Auditer", SqlDbType.NVarChar,50),
                    new SqlParameter("@AuditDate", SqlDbType.DateTime)
                                        };
            parameters[0].Value = model.ProjectId;
            parameters[1].Value = model.ReturnId;
            parameters[2].Value = model.Total;
            parameters[3].Value = model.Tax;
            parameters[4].Value = model.CurrentDate;

            parameters[5].Value = model.TaxDate;
            parameters[6].Value = model.UserId;
            parameters[7].Value = model.UserName;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.ProjectCode;
            parameters[10].Value = model.ReturnCode;
            parameters[11].Value = model.DepartmentId;
            parameters[12].Value = model.DepartmentName;

            parameters[13].Value = model.AuditerId;
            parameters[14].Value = model.Auditer;
            parameters[15].Value = model.AuditDate;


            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Finance.Entity.TaxDetailInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_TaxDetail set ");
            strSql.Append("ProjectId=@ProjectId,");
            strSql.Append("ReturnId=@ReturnId,");
            strSql.Append("Total=@Total,");
            strSql.Append("Tax=@Tax,");
            strSql.Append("CurrentDate=@CurrentDate,");
            strSql.Append("TaxDate=@TaxDate,");
            strSql.Append("UserId=@UserId,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("Status=@Status,");
            strSql.Append("ProjectCode=@ProjectCode,");
            strSql.Append("ReturnCode=@ReturnCode,");
            strSql.Append("DepartmentId=@DepartmentId,");
            strSql.Append("DepartmentName=@DepartmentName, ");
            strSql.Append("AuditerId=@AuditerId,");
            strSql.Append("Auditer=@Auditer,");
            strSql.Append("AuditDate=@AuditDate ");

            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectId", SqlDbType.Int),
					new SqlParameter("@ReturnId", SqlDbType.Int),
					new SqlParameter("@Total", SqlDbType.Decimal,20),
					new SqlParameter("@Tax", SqlDbType.Decimal,20),
					new SqlParameter("@CurrentDate", SqlDbType.DateTime),
                    new SqlParameter("@TaxDate", SqlDbType.DateTime),
                    new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Status", SqlDbType.Int),
                    new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@ReturnCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@DepartmentId", SqlDbType.Int),
                    new SqlParameter("@DepartmentName", SqlDbType.NVarChar,50),
                    new SqlParameter("@AuditerId", SqlDbType.Int),
                    new SqlParameter("@Auditer", SqlDbType.NVarChar,50),
                    new SqlParameter("@AuditDate", SqlDbType.DateTime),
                    new SqlParameter("@id", SqlDbType.Int)
                                        };
            parameters[0].Value = model.ProjectId;
            parameters[1].Value = model.ReturnId;
            parameters[2].Value = model.Total;
            parameters[3].Value = model.Tax;
            parameters[4].Value = model.CurrentDate;
            parameters[5].Value = model.TaxDate;
            parameters[6].Value = model.UserId;
            parameters[7].Value = model.UserName;
            parameters[8].Value = model.Status;
            parameters[9].Value = model.ProjectCode;
            parameters[10].Value = model.ReturnCode;
            parameters[11].Value = model.DepartmentId;
            parameters[12].Value = model.DepartmentName;
            parameters[13].Value = model.AuditerId;
            parameters[14].Value = model.Auditer;
            parameters[15].Value = model.AuditDate;
            parameters[16].Value = model.Id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_TaxDetail ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.TaxDetailInfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from F_TaxDetail ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return CBO.FillObject<TaxDetailInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));


        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<TaxDetailInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM F_TaxDetail ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }

            return CBO.FillCollection<TaxDetailInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }


        #endregion  成员方法
    }
}
