using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Purchase.Common;
using ESP.Purchase.Entity;
using ESP.ConfigCommon;

namespace ESP.Purchase.DataAccess
{
    public class PRAuthorizationProvider
    {
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PRAuthorizationInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_PRAuthorization(");
            strSql.Append("UserId,UserName,Status,CreateDate,CloseDate,CreateUserId,CreateUserName,Remark,TypeId,TypeName)");
            strSql.Append(" values (");
            strSql.Append("@UserId,@UserName,@Status,@CreateDate,@CloseDate,@CreateUserId,@CreateUserName,@Remark,@TypeId,@TypeName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CloseDate", SqlDbType.DateTime),
                    new SqlParameter("@CreateUserId",SqlDbType.Int,4),
                    new SqlParameter("@CreateUserName",SqlDbType.NVarChar,50),
                    new SqlParameter("@Remark",SqlDbType.NVarChar,500),
                    new SqlParameter("@TypeId",  SqlDbType.NVarChar,500),
                    new SqlParameter("@TypeName",  SqlDbType.NVarChar,500)
                                        };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.UserName;
            parameters[2].Value = model.Status;
            parameters[3].Value = model.CreateDate;
            parameters[4].Value = model.CloseDate;
            parameters[5].Value = model.CreateUserId;
            parameters[6].Value = model.CreateUserName;
            parameters[7].Value = model.Remark;
            parameters[8].Value = model.TypeId;
            parameters[9].Value = model.TypeName;

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
        public void Update(PRAuthorizationInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_PRAuthorization set ");
            strSql.Append("UserId=@UserId,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("Status=@Status,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("CloseDate=@CloseDate,CreateUserId=@CreateUserId,CreateUserName=@CreateUserName,Remark=@Remark,TypeId=@TypeId,TypeName=@TypeName");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@CloseDate", SqlDbType.DateTime),
                    new SqlParameter("@CreateUserId",SqlDbType.Int,4),
                    new SqlParameter("@CreateUserName",SqlDbType.NVarChar,50),
                    new SqlParameter("@Remark",SqlDbType.NVarChar,500),
                    new SqlParameter("@TypeId",  SqlDbType.NVarChar,500),
                    new SqlParameter("@TypeName",  SqlDbType.NVarChar,500)
                                        };
            parameters[0].Value = model.Id;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.Status;
            parameters[4].Value = model.CreateDate;
            parameters[5].Value = model.CloseDate;
            parameters[6].Value = model.CreateUserId;
            parameters[7].Value = model.CreateUserName;
            parameters[8].Value = model.Remark;
            parameters[9].Value = model.TypeId;
            parameters[10].Value = model.TypeName;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_PRAuthorization ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public PRAuthorizationInfo GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from T_PRAuthorization ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = Id;

            return CBO.FillObject<PRAuthorizationInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public PRAuthorizationInfo GetUsedModel(int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from T_PRAuthorization ");
            strSql.Append(" where userId=@userId and status=" + (int)Common.State.PRAuthorizationStatus.PRAuthorizationStatus_Use);
            SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int,4)};
            parameters[0].Value = userId;

            return CBO.FillObject<PRAuthorizationInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public bool isHaveUsed(int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from T_PRAuthorization ");
            strSql.Append(" where userId=@userId and status="+(int)Common.State.PRAuthorizationStatus.PRAuthorizationStatus_Use);
            SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int,4)};
            parameters[0].Value = userId;

            return DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0].Rows.Count > 0;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<PRAuthorizationInfo> GetList(string strWhere,List<SqlParameter> parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM T_PRAuthorization where 1=1");
            strSql.Append(strWhere);
            return CBO.FillCollection<PRAuthorizationInfo>(DbHelperSQL.Query(strSql.ToString(),parms.ToArray()));
        }
        #endregion
    }
}
