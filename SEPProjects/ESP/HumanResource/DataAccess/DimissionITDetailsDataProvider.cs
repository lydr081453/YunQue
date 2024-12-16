using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ESP.HumanResource.Common;

namespace ESP.HumanResource.DataAccess
{
    public class DimissionITDetailsDataProvider
    {
        public DimissionITDetailsDataProvider()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int DimissionITDetailId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SEP_DimissionITDetails");
            strSql.Append(" where DimissionITDetailId= @DimissionITDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionITDetailId", SqlDbType.Int,4)
				};
            parameters[0].Value = DimissionITDetailId;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.HumanResource.Entity.DimissionITDetailsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SEP_DimissionITDetails(");
            strSql.Append("DimissionId,Email,EmailIsDelete,EmailSaveLastDay,AccountIsDelete,AccountSaveLastDay,PCCode,PCUsedDes,OtherDes,OwnPCCode,PrincipalID,PrincipalName)");
            strSql.Append(" values (");
            strSql.Append("@DimissionId,@Email,@EmailIsDelete,@EmailSaveLastDay,@AccountIsDelete,@AccountSaveLastDay,@PCCode,@PCUsedDes,@OtherDes,@OwnPCCode,@PrincipalID,@PrincipalName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@Email", SqlDbType.NVarChar),
					new SqlParameter("@EmailIsDelete", SqlDbType.Bit,1),
					new SqlParameter("@EmailSaveLastDay", SqlDbType.DateTime),
					new SqlParameter("@AccountIsDelete", SqlDbType.Bit,1),
					new SqlParameter("@AccountSaveLastDay", SqlDbType.DateTime),
					new SqlParameter("@PCCode", SqlDbType.NVarChar),
					new SqlParameter("@PCUsedDes", SqlDbType.NVarChar),
					new SqlParameter("@OtherDes", SqlDbType.NVarChar),
					new SqlParameter("@OwnPCCode", SqlDbType.NVarChar),
					new SqlParameter("@PrincipalID", SqlDbType.Int,4),
					new SqlParameter("@PrincipalName", SqlDbType.NVarChar)};
            parameters[0].Value = model.DimissionId;
            parameters[1].Value = model.Email;
            parameters[2].Value = model.EmailIsDelete;
            parameters[3].Value = model.EmailSaveLastDay;
            parameters[4].Value = model.AccountIsDelete;
            parameters[5].Value = model.AccountSaveLastDay;
            parameters[6].Value = model.PCCode;
            parameters[7].Value = model.PCUsedDes;
            parameters[8].Value = model.OtherDes;
            parameters[9].Value = model.OwnPCCode;
            parameters[10].Value = model.PrincipalID;
            parameters[11].Value = model.PrincipalName;

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
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.HumanResource.Entity.DimissionITDetailsInfo model, SqlTransaction stran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SEP_DimissionITDetails(");
            strSql.Append("DimissionId,Email,EmailIsDelete,EmailSaveLastDay,AccountIsDelete,AccountSaveLastDay,PCCode,PCUsedDes,OtherDes,OwnPCCode,PrincipalID,PrincipalName)");
            strSql.Append(" values (");
            strSql.Append("@DimissionId,@Email,@EmailIsDelete,@EmailSaveLastDay,@AccountIsDelete,@AccountSaveLastDay,@PCCode,@PCUsedDes,@OtherDes,@OwnPCCode,@PrincipalID,@PrincipalName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@Email", SqlDbType.NVarChar),
					new SqlParameter("@EmailIsDelete", SqlDbType.Bit,1),
					new SqlParameter("@EmailSaveLastDay", SqlDbType.DateTime),
					new SqlParameter("@AccountIsDelete", SqlDbType.Bit,1),
					new SqlParameter("@AccountSaveLastDay", SqlDbType.DateTime),
					new SqlParameter("@PCCode", SqlDbType.NVarChar),
					new SqlParameter("@PCUsedDes", SqlDbType.NVarChar),
					new SqlParameter("@OtherDes", SqlDbType.NVarChar),
					new SqlParameter("@OwnPCCode", SqlDbType.NVarChar),
					new SqlParameter("@PrincipalID", SqlDbType.Int,4),
					new SqlParameter("@PrincipalName", SqlDbType.NVarChar)};
            parameters[0].Value = model.DimissionId;
            parameters[1].Value = model.Email;
            parameters[2].Value = model.EmailIsDelete;
            parameters[3].Value = model.EmailSaveLastDay;
            parameters[4].Value = model.AccountIsDelete;
            parameters[5].Value = model.AccountSaveLastDay;
            parameters[6].Value = model.PCCode;
            parameters[7].Value = model.PCUsedDes;
            parameters[8].Value = model.OtherDes;
            parameters[9].Value = model.OwnPCCode;
            parameters[10].Value = model.PrincipalID;
            parameters[11].Value = model.PrincipalName;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), stran.Connection, stran, parameters);
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
        public void Update(ESP.HumanResource.Entity.DimissionITDetailsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SEP_DimissionITDetails set ");
            strSql.Append("DimissionId=@DimissionId,");
            strSql.Append("Email=@Email,");
            strSql.Append("EmailIsDelete=@EmailIsDelete,");
            strSql.Append("EmailSaveLastDay=@EmailSaveLastDay,");
            strSql.Append("AccountIsDelete=@AccountIsDelete,");
            strSql.Append("AccountSaveLastDay=@AccountSaveLastDay,");
            strSql.Append("PCCode=@PCCode,");
            strSql.Append("PCUsedDes=@PCUsedDes,");
            strSql.Append("OtherDes=@OtherDes,");
            strSql.Append("OwnPCCode=@OwnPCCode,");
            strSql.Append("PrincipalID=@PrincipalID,");
            strSql.Append("PrincipalName=@PrincipalName");
            strSql.Append(" where DimissionITDetailId=@DimissionITDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionITDetailId", SqlDbType.Int,4),
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@Email", SqlDbType.NVarChar),
					new SqlParameter("@EmailIsDelete", SqlDbType.Bit,1),
					new SqlParameter("@EmailSaveLastDay", SqlDbType.DateTime),
					new SqlParameter("@AccountIsDelete", SqlDbType.Bit,1),
					new SqlParameter("@AccountSaveLastDay", SqlDbType.DateTime),
					new SqlParameter("@PCCode", SqlDbType.NVarChar),
					new SqlParameter("@PCUsedDes", SqlDbType.NVarChar),
					new SqlParameter("@OtherDes", SqlDbType.NVarChar),
					new SqlParameter("@OwnPCCode", SqlDbType.NVarChar),
					new SqlParameter("@PrincipalID", SqlDbType.Int,4),
					new SqlParameter("@PrincipalName", SqlDbType.NVarChar)};
            parameters[0].Value = model.DimissionITDetailId;
            parameters[1].Value = model.DimissionId;
            parameters[2].Value = model.Email;
            parameters[3].Value = model.EmailIsDelete;
            parameters[4].Value = model.EmailSaveLastDay;
            parameters[5].Value = model.AccountIsDelete;
            parameters[6].Value = model.AccountSaveLastDay;
            parameters[7].Value = model.PCCode;
            parameters[8].Value = model.PCUsedDes;
            parameters[9].Value = model.OtherDes;
            parameters[10].Value = model.OwnPCCode;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(ESP.HumanResource.Entity.DimissionITDetailsInfo model, SqlTransaction stran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SEP_DimissionITDetails set ");
            strSql.Append("DimissionId=@DimissionId,");
            strSql.Append("Email=@Email,");
            strSql.Append("EmailIsDelete=@EmailIsDelete,");
            strSql.Append("EmailSaveLastDay=@EmailSaveLastDay,");
            strSql.Append("AccountIsDelete=@AccountIsDelete,");
            strSql.Append("AccountSaveLastDay=@AccountSaveLastDay,");
            strSql.Append("PCCode=@PCCode,");
            strSql.Append("PCUsedDes=@PCUsedDes,");
            strSql.Append("OtherDes=@OtherDes,");
            strSql.Append("OwnPCCode=@OwnPCCode,");
            strSql.Append("PrincipalID=@PrincipalID,");
            strSql.Append("PrincipalName=@PrincipalName");
            strSql.Append(" where DimissionITDetailId=@DimissionITDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionITDetailId", SqlDbType.Int,4),
					new SqlParameter("@DimissionId", SqlDbType.Int,4),
					new SqlParameter("@Email", SqlDbType.NVarChar),
					new SqlParameter("@EmailIsDelete", SqlDbType.Bit,1),
					new SqlParameter("@EmailSaveLastDay", SqlDbType.DateTime),
					new SqlParameter("@AccountIsDelete", SqlDbType.Bit,1),
					new SqlParameter("@AccountSaveLastDay", SqlDbType.DateTime),
					new SqlParameter("@PCCode", SqlDbType.NVarChar),
					new SqlParameter("@PCUsedDes", SqlDbType.NVarChar),
					new SqlParameter("@OtherDes", SqlDbType.NVarChar),
					new SqlParameter("@OwnPCCode", SqlDbType.NVarChar),
					new SqlParameter("@PrincipalID", SqlDbType.Int,4),
					new SqlParameter("@PrincipalName", SqlDbType.NVarChar)};
            parameters[0].Value = model.DimissionITDetailId;
            parameters[1].Value = model.DimissionId;
            parameters[2].Value = model.Email;
            parameters[3].Value = model.EmailIsDelete;
            parameters[4].Value = model.EmailSaveLastDay;
            parameters[5].Value = model.AccountIsDelete;
            parameters[6].Value = model.AccountSaveLastDay;
            parameters[7].Value = model.PCCode;
            parameters[8].Value = model.PCUsedDes;
            parameters[9].Value = model.OtherDes;
            parameters[10].Value = model.OwnPCCode;

            DbHelperSQL.ExecuteSql(strSql.ToString(), stran.Connection, stran, parameters);
        }
        
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int DimissionITDetailId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SEP_DimissionITDetails ");
            strSql.Append(" where DimissionITDetailId=@DimissionITDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionITDetailId", SqlDbType.Int,4)
				};
            parameters[0].Value = DimissionITDetailId;
            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.HumanResource.Entity.DimissionITDetailsInfo GetModel(int DimissionITDetailId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from SEP_DimissionITDetails ");
            strSql.Append(" where DimissionITDetailId=@DimissionITDetailId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionITDetailId", SqlDbType.Int,4)};
            parameters[0].Value = DimissionITDetailId;
            ESP.HumanResource.Entity.DimissionITDetailsInfo model = new ESP.HumanResource.Entity.DimissionITDetailsInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            model.DimissionITDetailId = DimissionITDetailId;
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
            strSql.Append(" FROM SEP_DimissionITDetails ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.HumanResource.Entity.DimissionITDetailsInfo GetITDetailInfo(int dimissionId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM SEP_DimissionITDetails ");
            strSql.Append(" WHERE DimissionId=@DimissionId");
            SqlParameter[] parameters = {
					new SqlParameter("@DimissionId", SqlDbType.Int,4)};
            parameters[0].Value = dimissionId;
            ESP.HumanResource.Entity.DimissionITDetailsInfo model = new ESP.HumanResource.Entity.DimissionITDetailsInfo();
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