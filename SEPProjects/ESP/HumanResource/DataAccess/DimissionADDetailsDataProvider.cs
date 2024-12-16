using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESP.HumanResource.Common;
using System.Data.SqlClient;
using System.Data;

namespace ESP.HumanResource.DataAccess
{
    public class DimissionADDetailsDataProvider
    {
        public DimissionADDetailsDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("DimissionADDetailId", "SEP_DimissionADDetails");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int DimissionADDetailId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SEP_DimissionADDetails");
            strSql.Append(" where DimissionADDetailId= @DimissionADDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionADDetailId", SqlDbType.Int,4)
				};
            parameters[0].Value = DimissionADDetailId;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.HumanResource.Entity.DimissionADDetailsInfo model)
        {
            //model.DimissionADDetailId=GetMaxId();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SEP_DimissionADDetails(");
            strSql.Append("DimissionId,DoorCard,LibraryManage,PrincipalID,PrincipalName)");
            strSql.Append(" values (");
            strSql.Append("@DimissionId,@DoorCard,@LibraryManage,@PrincipalID,@PrincipalName)");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@DoorCard", SqlDbType.NVarChar),
					new SqlParameter("@LibraryManage", SqlDbType.NVarChar),
					new SqlParameter("@PrincipalID", SqlDbType.Int,4),
					new SqlParameter("@PrincipalName", SqlDbType.NVarChar)};            
            parameters[0].Value = model.DimissionId;
            parameters[1].Value = model.DoorCard;
            parameters[2].Value = model.LibraryManage;
            parameters[3].Value = model.PrincipalID;
            parameters[4].Value = model.PrincipalName;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            return model.DimissionADDetailId;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.HumanResource.Entity.DimissionADDetailsInfo model, SqlTransaction stran)
        {
            //model.DimissionADDetailId=GetMaxId();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO SEP_DimissionADDetails(");
            strSql.Append("DimissionId,DoorCard,LibraryManage,PrincipalID,PrincipalName)");
            strSql.Append(" VALUES (");
            strSql.Append("@DimissionId,@DoorCard,@LibraryManage,@PrincipalID,@PrincipalName)");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@DoorCard", SqlDbType.NVarChar),
					new SqlParameter("@LibraryManage", SqlDbType.NVarChar),
					new SqlParameter("@PrincipalID", SqlDbType.Int,4),
					new SqlParameter("@PrincipalName", SqlDbType.NVarChar)};
            parameters[0].Value = model.DimissionId;
            parameters[1].Value = model.DoorCard;
            parameters[2].Value = model.LibraryManage;
            parameters[3].Value = model.PrincipalID;
            parameters[4].Value = model.PrincipalName;

            DbHelperSQL.ExecuteSql(strSql.ToString(), stran.Connection, stran, parameters);
            return model.DimissionADDetailId;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.HumanResource.Entity.DimissionADDetailsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE SEP_DimissionADDetails SET ");
            strSql.Append("DimissionId=@DimissionId,");
            strSql.Append("DoorCard=@DoorCard,");
            strSql.Append("LibraryManage=@LibraryManage,");
            strSql.Append("PrincipalID=@PrincipalID,");
            strSql.Append("PrincipalName=@PrincipalName ");
            strSql.Append(" WHERE DimissionADDetailId=@DimissionADDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionADDetailId", SqlDbType.Int,4),
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@DoorCard", SqlDbType.NVarChar),
					new SqlParameter("@LibraryManage", SqlDbType.NVarChar),
					new SqlParameter("@PrincipalID", SqlDbType.Int,4),
					new SqlParameter("@PrincipalName", SqlDbType.NVarChar)};
            parameters[0].Value = model.DimissionADDetailId;
            parameters[1].Value = model.DimissionId;
            parameters[2].Value = model.DoorCard;
            parameters[3].Value = model.LibraryManage;
            parameters[4].Value = model.PrincipalID;
            parameters[5].Value = model.PrincipalName;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.HumanResource.Entity.DimissionADDetailsInfo model, SqlTransaction stran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE SEP_DimissionADDetails SET ");
            strSql.Append("DimissionId=@DimissionId,");
            strSql.Append("DoorCard=@DoorCard,");
            strSql.Append("LibraryManage=@LibraryManage,");
            strSql.Append("PrincipalID=@PrincipalID,");
            strSql.Append("PrincipalName=@PrincipalName ");
            strSql.Append(" WHERE DimissionADDetailId=@DimissionADDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionADDetailId", SqlDbType.Int,4),
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@DoorCard", SqlDbType.NVarChar),
					new SqlParameter("@LibraryManage", SqlDbType.NVarChar),
					new SqlParameter("@PrincipalID", SqlDbType.Int,4),
					new SqlParameter("@PrincipalName", SqlDbType.NVarChar)};
            parameters[0].Value = model.DimissionADDetailId;
            parameters[1].Value = model.DimissionId;
            parameters[2].Value = model.DoorCard;
            parameters[3].Value = model.LibraryManage;
            parameters[4].Value = model.PrincipalID;
            parameters[5].Value = model.PrincipalName;

            DbHelperSQL.ExecuteSql(strSql.ToString(), stran.Connection, stran, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int DimissionADDetailId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE SEP_DimissionADDetails ");
            strSql.Append(" WHERE DimissionADDetailId=@DimissionADDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionADDetailId", SqlDbType.Int,4)
				};
            parameters[0].Value = DimissionADDetailId;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.HumanResource.Entity.DimissionADDetailsInfo GetModel(int DimissionADDetailId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_DimissionADDetails ");
            strSql.Append(" WHERE DimissionADDetailId=@DimissionADDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionADDetailId", SqlDbType.Int,4)};
            parameters[0].Value = DimissionADDetailId;
            ESP.HumanResource.Entity.DimissionADDetailsInfo model = new ESP.HumanResource.Entity.DimissionADDetailsInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.DimissionADDetailId = DimissionADDetailId;
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.PopupData(ds.Tables[0].Rows[0]);
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * ");
            strSql.Append(" FROM SEP_DimissionADDetails ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.HumanResource.Entity.DimissionADDetailsInfo GetADDetailInfo(int dimissionId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_DimissionADDetails ");
            strSql.Append(" WHERE DimissionId=@DimissionId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionId", SqlDbType.Int,4)};
            parameters[0].Value = dimissionId;
            ESP.HumanResource.Entity.DimissionADDetailsInfo model = new ESP.HumanResource.Entity.DimissionADDetailsInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.PopupData(ds.Tables[0].Rows[0]);
                return model;
            }
            else
            {
                return null;
            }
        }
        #endregion  成员方法
    }
}

