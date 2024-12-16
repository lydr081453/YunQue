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
    public class SC_SupplierMessageReturnDataProvider
    {
        public SC_SupplierMessageReturnDataProvider()
        { }
        #region  成员方法


        /// <summary>
        /// 添加一条数据
        /// </summary>
        public int Add(SC_SupplierMessageReturn model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_SupplierMessageReturn ( ");
            strSql.Append("Title,");
            strSql.Append("Body,");
            strSql.Append("CreatedDate,");
            strSql.Append("CreatedUserID,");
            strSql.Append("UserType,");
            strSql.Append("Type,");
            strSql.Append("FileUrl,");
            strSql.Append("UserName,");
            strSql.Append("IsDel,");
            strSql.Append("IsRead,");
            strSql.Append("SuppliserMessageID)");
            strSql.Append(" Values (");
            strSql.Append("@Title,");
            strSql.Append("@Body,");
            strSql.Append("@CreatedDate,");
            strSql.Append("@CreatedUserID,");
            strSql.Append("@UserType,");
            strSql.Append("@Type,");
            strSql.Append("@FileUrl,");
            strSql.Append("@UserName,");
            strSql.Append("@IsDel,");
            strSql.Append("@IsRead,");
            strSql.Append("@SuppliserMessageID");
            strSql.Append(" )");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    //new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.NVarChar),
					new SqlParameter("@Body", SqlDbType.NVarChar),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserID", SqlDbType.Int,4),
					new SqlParameter("@UserType", SqlDbType.Int,4), 
					new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@FileUrl", SqlDbType.NVarChar),
					new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@IsDel", SqlDbType.Bit),
					new SqlParameter("@IsRead", SqlDbType.Bit),
                    new SqlParameter("@SuppliserMessageID", SqlDbType.Int,4)};
            //parameters[0].Value = model.ID;
            parameters[0].Value = model.Title;
            parameters[1].Value = model.Body;
            parameters[2].Value = DateTime.Now;
            parameters[3].Value = model.CreatedUserID;
            parameters[4].Value = model.UserType;
            parameters[5].Value = model.Type;
            parameters[6].Value = model.FileUrl;
            parameters[7].Value = model.UserName;
            parameters[8].Value = false;
            parameters[9].Value = false;
            parameters[10].Value = model.SuppliserMessageID;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
            //DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            //return model.ID;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(SC_SupplierMessageReturn model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_SupplierMessageReturn set ");
            strSql.Append("Title=@Title,");
            strSql.Append("Body=@Body,");
            strSql.Append("IsDel=@IsDel");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.NVarChar),
					new SqlParameter("@Body", SqlDbType.NVarChar),
					new SqlParameter("@IsDel", SqlDbType.Bit)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.Body;
            parameters[3].Value = model.IsDel;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_SupplierMessageReturn ");
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
        public SC_SupplierMessageReturn GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SC_SupplierMessageReturn ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            return ESP.ConfigCommon.CBO.FillObject<SC_SupplierMessageReturn>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_SupplierMessageReturn ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<SC_SupplierMessageReturn> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_SupplierMessageReturn ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_SupplierMessageReturn>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }


        public List<SC_SupplierMessageReturn> GetAllLists()
        {
            return ESP.ConfigCommon.CBO.FillCollection<SC_SupplierMessageReturn>(GetList(""));
        }

        #endregion  成员方法
    }
}