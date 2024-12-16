using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.Supplier.Common;
using ESP.Supplier.Entity;
using System.Data;
using System.Data.SqlClient;

namespace ESP.Supplier.DataAccess
{
    public class SC_PrivateMessagesDataProvider
    {
        public SC_PrivateMessagesDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 添加一条数据
        /// </summary>
        public void Add(SC_PrivateMessages model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_SupplierAndVendeeMessage ( ");
            strSql.Append("Title,");
            strSql.Append("Body,");
            strSql.Append("CreatedDate,");
            strSql.Append("CreatedUserID,");
            strSql.Append("CreatedUserType,");
            strSql.Append("SendToUserID,");
            strSql.Append("SendToUserType,");
            strSql.Append("CreatedUserName,");
            strSql.Append("SendToUserName,");
            strSql.Append("IsDel,");
            strSql.Append("IsRead,");
            strSql.Append("IsApprove)");
            strSql.Append(" Values (");
            strSql.Append("@Title,");
            strSql.Append("@Body,");
            strSql.Append("@CreatedDate,");
            strSql.Append("@CreatedUserID,");
            strSql.Append("@CreatedUserType,");
            strSql.Append("@SendToUserID,");
            strSql.Append("@SendToUserType,");
            strSql.Append("@CreatedUserName,");
            strSql.Append("@SendToUserName,");
            strSql.Append("@IsDel,");
            strSql.Append("@IsRead,");
            strSql.Append("@IsApprove");
            strSql.Append(" )");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.NVarChar),
					new SqlParameter("@Body", SqlDbType.NVarChar),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@CreatedUserType", SqlDbType.Int,4),
					new SqlParameter("@SendToUserID", SqlDbType.Int,4),
					new SqlParameter("@SendToUserType", SqlDbType.Int,4),
					new SqlParameter("@CreatedUserName", SqlDbType.NVarChar),
					new SqlParameter("@IsDel", SqlDbType.Bit),
					new SqlParameter("@IsRead", SqlDbType.Bit),
					new SqlParameter("@IsApprove", SqlDbType.Bit),
					new SqlParameter("@SendToUserName", SqlDbType.NVarChar)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.Body;
            parameters[3].Value = DateTime.Now;
            parameters[4].Value = model.CreatedUserID;
            parameters[5].Value = model.CreatedUserType;
            parameters[6].Value = model.SendToUserID;
            parameters[7].Value = model.SendToUserType;
            parameters[8].Value = model.CreatedUserName;
            parameters[9].Value = false;
            parameters[10].Value = false;
            parameters[11].Value = true;
            parameters[12].Value = model.SendToUserName;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(SC_PrivateMessages model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_SupplierAndVendeeMessage set ");
            strSql.Append("Title=@Title,");
            strSql.Append("Body=@Body,");
            strSql.Append("IsDel=@IsDel,");
            strSql.Append("IsApprove=@IsApprove");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.NVarChar),
					new SqlParameter("@Body", SqlDbType.NVarChar),
					new SqlParameter("@IsDel", SqlDbType.Bit),
					new SqlParameter("@IsApprove", SqlDbType.Bit)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.Body;
            parameters[3].Value = model.IsDel;
            parameters[4].Value = model.IsApprove;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_SupplierAndVendeeMessage ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
            parameters[0].Value = ID;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SC_PrivateMessages GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SC_SupplierAndVendeeMessage ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            return ESP.ConfigCommon.CBO.FillObject<SC_PrivateMessages>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_SupplierAndVendeeMessage ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<SC_PrivateMessages> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_SupplierAndVendeeMessage ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_PrivateMessages>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }


        public List<SC_PrivateMessages> GetAllLists()
        {
            return ESP.ConfigCommon.CBO.FillCollection<SC_PrivateMessages>(GetList(""));
        }

        #endregion  成员方法
    }
}
