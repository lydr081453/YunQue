using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ESP.Administrative.Common;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.DataAccess
{
    public class EmailClosingProvider
    {


        public EmailClosingProvider()
        { }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(EmailClosingInfo model)
        {


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SEP_EmailClosing(");
            strSql.Append("UserId,NameCN,UserCode,DeptName,Postion,Email,OperatorId,KeepDate,CloseDate,Status)");
            strSql.Append(" values (");
            strSql.Append("@UserId,@NameCN,@UserCode,@DeptName,@Postion,@Email,@OperatorId,@KeepDate,@CloseDate,@Status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int),
                    new SqlParameter("@NameCN", SqlDbType.NVarChar,50),
                    new SqlParameter("@UserCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@DeptName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Postion", SqlDbType.NVarChar,50),
                    
                    new SqlParameter("@Email", SqlDbType.NVarChar,50),
                    new SqlParameter("@OperatorId", SqlDbType.Int),
                    new SqlParameter("@KeepDate", SqlDbType.DateTime),
                    new SqlParameter("@CloseDate", SqlDbType.DateTime),
                    new SqlParameter("@Status", SqlDbType.Int)
                                        };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.NameCN;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.DeptName;
            parameters[4].Value = model.Postion;
            parameters[5].Value = model.Email;
            parameters[6].Value = model.OperatorId;
            parameters[7].Value = model.KeepDate;
            parameters[8].Value = model.CloseDate;
            parameters[9].Value = model.Status;

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
        public bool Update(EmailClosingInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SEP_EmailClosing set ");
            strSql.Append("Status=@Status,OperatorId=@OperatorId,CloseDate=@CloseDate");
            strSql.Append(" where UserId=@UserId");
            SqlParameter[] parameters = {
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@OperatorId", SqlDbType.Int,4),
                    new SqlParameter("@CloseDate", SqlDbType.DateTime),
					new SqlParameter("@UserId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.Status;
            parameters[1].Value = model.OperatorId;
            parameters[2].Value = model.CloseDate;
            parameters[3].Value = model.UserId;
     
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据 by userid
        /// </summary>
        public bool Delete(int userId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from SEP_EmailClosing ");
            strSql.Append(" where userId=@userId");
            SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int,4)
			};
            parameters[0].Value = userId;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体 by userid
        /// </summary>
        public EmailClosingInfo GetModel(int userId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  * from SEP_EmailClosing ");
            strSql.Append(" where userId=@userId");
            SqlParameter[] parameters = {
					new SqlParameter("@userId", SqlDbType.Int,4)
			};
            parameters[0].Value = userId;

            EmailClosingInfo model = new EmailClosingInfo();
            return CBO.FillObject<EmailClosingInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<EmailClosingInfo> GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SEP_EmailClosing ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return CBO.FillCollection<EmailClosingInfo>(DbHelperSQL.Query(strSql.ToString()));
        }

    }
}
