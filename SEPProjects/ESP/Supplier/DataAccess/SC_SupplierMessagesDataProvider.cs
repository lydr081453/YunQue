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
    public class SC_SupplierMessagesDataProvider
    {
        public SC_SupplierMessagesDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 添加一条数据
        /// </summary>
        public int Add(SC_SupplierMessages model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SC_SupplierMessages ( ");
            strSql.Append("Title,");
            strSql.Append("Body,");
            strSql.Append("CreatedDate,");
            strSql.Append("CreatedUserID,");
            strSql.Append("UserType,");
            strSql.Append("Type,");
            strSql.Append("FileUrl,");
            strSql.Append("UserName,");
            strSql.Append("IsDel,");
            strSql.Append("IsReaded,");
            strSql.Append("IsApporved)");
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
            strSql.Append("@IsReaded,");
            strSql.Append("@IsApporved");
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
					new SqlParameter("@IsReaded", SqlDbType.Bit),
                    new SqlParameter("@IsApporved", SqlDbType.Bit)};
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
            parameters[10].Value = true;
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
        public void Update(SC_SupplierMessages model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SC_SupplierMessages set ");
            strSql.Append("Title=@Title,");
            strSql.Append("Body=@Body,");
            strSql.Append("FileUrl=@FileUrl,");
            strSql.Append("IsDel=@IsDel,");
            strSql.Append("ProductTypeIDs=@ProductTypeIDs ,");
            strSql.Append("InfoID=@InfoID,");
            strSql.Append("IsApporved=@IsApporved");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.NVarChar),
					new SqlParameter("@Body", SqlDbType.NVarChar),
					new SqlParameter("@FileUrl", SqlDbType.NVarChar),
					new SqlParameter("@IsDel", SqlDbType.Bit),
					new SqlParameter("@ProductTypeIDs", SqlDbType.NVarChar),
					new SqlParameter("@InfoID", SqlDbType.Int, 8),
					new SqlParameter("@IsApporved", SqlDbType.Bit)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.Body;
            parameters[3].Value = model.FileUrl;
            parameters[4].Value = model.IsDel;
            parameters[5].Value = model.ProductTypeIDs;
            parameters[6].Value = model.InfoID;
            parameters[7].Value = model.IsApporved;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SC_SupplierMessages ");
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
        public SC_SupplierMessages GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SC_SupplierMessages ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;
            return ESP.ConfigCommon.CBO.FillObject<SC_SupplierMessages>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_SupplierMessages ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        public List<SC_SupplierMessages> GetList(string strWhere, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SC_SupplierMessages ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            return ESP.ConfigCommon.CBO.FillCollection<SC_SupplierMessages>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public DataTable GetTable(string strWhere, SqlParameter[] parms)
        {
            string sql = @"select a.*,b.realname,c.supplier_name,d.guestname from sc_suppliermessages as a
                            left join sc_vendee as b on a.createduserid=b.id
                            left join sc_supplier as c on a.createduserid=c.id
                            left join sc_guests as d on a.createduserid=d.id";
            if (strWhere.Trim() != "")
            {
                sql += " where 1=1 " + strWhere;
            }
            return DbHelperSQL.Query(sql.ToString(), parms).Tables[0];
        }


        public List<SC_SupplierMessages> GetAllLists()
        {
            return ESP.ConfigCommon.CBO.FillCollection<SC_SupplierMessages>(GetList(""));
        }

        #endregion  成员方法
    }
}
