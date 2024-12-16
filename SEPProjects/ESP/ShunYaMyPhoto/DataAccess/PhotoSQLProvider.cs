using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using MyPhotoUtility;
using Microsoft.ApplicationBlocks.Data;
using MyPhotoInfo;

namespace MyPhotoSQLServerDAL
{
    public class PhotoSQLProvider
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [SYP_Photos]");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parameters));
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(PhotoInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [SYP_Photos](");
            strSql.Append(@"[UserID]
                           ,[FileName]
                           ,[SmallFileName]
                           ,[PhotoName]
                           ,[Description]
                           ,[SystemAmount]
                           ,[BrowseAmount]
                           ,[CreatedDate]
                           ,[ModifiedDate]
                           ,[IsValidate],[MiddelFileName])");
            strSql.Append(" values (");
            strSql.Append(@"@UserID
                           ,@FileName
                           ,@SmallFileName
                           ,@PhotoName
                           ,@Description
                           ,@SystemAmount
                           ,@BrowseAmount
                           ,@CreatedDate
                           ,@ModifiedDate
                           ,@IsValidate,@MiddelFileName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int, 6),
					new SqlParameter("@FileName", SqlDbType.NVarChar,500),
					new SqlParameter("@SmallFileName", SqlDbType.NVarChar,500),
					new SqlParameter("@PhotoName", SqlDbType.NVarChar,500),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@SystemAmount", SqlDbType.Int, 6),
					new SqlParameter("@BrowseAmount", SqlDbType.Int, 6),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@IsValidate", SqlDbType.Bit),
					new SqlParameter("@MiddelFileName", SqlDbType.NVarChar,500)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.FileName;
            parameters[2].Value = model.SmallfileName;
            parameters[3].Value = model.PhotoName;
            parameters[4].Value = model.Description;
            parameters[5].Value = model.SystemAmount;
            parameters[6].Value = model.BrowseAmount;
            parameters[7].Value = DateTime.Now;
            parameters[8].Value = DateTime.Now;
            parameters[9].Value = 1;
            parameters[10].Value = model.MiddelFileName;

            object obj = SqlHelper.ExecuteScalar(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parameters);
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
        ///</summary>
        public int Update(PhotoInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [SYP_Photos] set ");

            strSql.Append("UserID=@UserID,");
            strSql.Append("FileName=@FileName,");
            strSql.Append("SmallFileName=@SmallFileName,");
            strSql.Append("PhotoName=@PhotoName,");
            strSql.Append("Description=@Description,");
            strSql.Append("ModifiedDate=@ModifiedDate,");
            strSql.Append("ModifiedUserID=@ModifiedUserID,");
            strSql.Append("SystemAmount=@SystemAmount,");
            strSql.Append("BrowseAmount=@BrowseAmount,");
            strSql.Append("IsValidate=@IsValidate,");
            strSql.Append("MiddelFileName=@MiddelFileName");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,6),
					new SqlParameter("@FileName", SqlDbType.NVarChar,500),
					new SqlParameter("@SmallFileName", SqlDbType.NVarChar,500),
					new SqlParameter("@PhotoName", SqlDbType.NVarChar,500),
					new SqlParameter("@Description", SqlDbType.NVarChar,500),
					new SqlParameter("@ModifiedDate", SqlDbType.DateTime),
					new SqlParameter("@SystemAmount", SqlDbType.Int, 6),
					new SqlParameter("@BrowseAmount", SqlDbType.Int, 6),
					new SqlParameter("@IsValidate", SqlDbType.Bit),
					new SqlParameter("@ModifiedUserID", SqlDbType.Int, 6),
					new SqlParameter("@ID", SqlDbType.Int, 6),
					new SqlParameter("@MiddelFileName", SqlDbType.NVarChar,500)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.FileName;
            parameters[2].Value = model.SmallfileName;
            parameters[3].Value = model.PhotoName;
            parameters[4].Value = model.Description;
            parameters[5].Value = DateTime.Now;
            parameters[6].Value = model.SystemAmount;
            parameters[7].Value = model.BrowseAmount;
            parameters[8].Value = model.IsValidate;
            parameters[9].Value = model.ModifiedUserID;
            parameters[10].Value = model.ID;
            parameters[11].Value = model.MiddelFileName;

            object obj = SqlHelper.ExecuteScalar(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
            //return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {

            string strSql = "delete SYP_Photos  where id=" + id.ToString();
            SqlHelper.ExecuteNonQuery(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString());
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public PhotoInfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  SYP_Photos.* from SYP_Photos ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return CBO.FillObject<PhotoInfo>(
                 SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parameters));


        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<PhotoInfo> GetList(string term)
        {
            string strSql = "select *  FROM SYP_Photos ";
            if (!string.IsNullOrEmpty(term))
            {
                strSql += " where " + term;
            }
            return CBO.FillCollection<PhotoInfo>(
                SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString()));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<PhotoInfo> GetList(string term, string orderby)
        {
            string strSql = "select *  FROM SYP_Photos ";
            if (!string.IsNullOrEmpty(term))
            {
                strSql += " where " + term;
            }
            if (!string.IsNullOrEmpty(orderby))
            {
                strSql += " Order By " + orderby;
            }
            return CBO.FillCollection<PhotoInfo>(
                SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString()));
        }

        public DataSet GetDataSetList(string term, string orderby)
        {
            string strSql = "select *  FROM SYP_Photos ";
            if (!string.IsNullOrEmpty(term))
            {
                strSql += " where " + term;
            }
            if (!string.IsNullOrEmpty(orderby))
            {
                strSql += " Order By " + orderby;
            }
            return SqlHelper.ExecuteDataset(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString());
        }


        public DataSet GetDataSetList(string sql)
        {
            string strSql = @"select distinct u.*, isnull(sl.c,0) + isnull(u.SystemAmount,0) as Total
                 from dbo.SYP_Photos u left join 
                (
                select PhotoId, isnull(count(*),0) as c from SYP_Supporter group by PhotoId
                ) sl on u.Id = sl.PhotoId 
                where  u.IsValidate='1' "+sql+
                " order by Total desc";
            return SqlHelper.ExecuteDataset(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString());
        }


        public DataSet GetDataSetListForTop(string sql, int top)
        {
            string strSql = "select distinct top(" + top + ") u.*, isnull(sl.c,0) + isnull(u.SystemAmount,0) as Total "
                 + "from dbo.SYP_Photos u left join "
                + "("
                + "select PhotoId, isnull(count(*),0) as c from SYP_SupporterForSelected group by PhotoId "
                + ") sl on u.Id = sl.PhotoId "
                + "where  u.IsValidate='1' " + sql
                +"order by Total desc";
            return SqlHelper.ExecuteDataset(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString());
        }

        public DataSet GetDataSetListForSelecedTop(string sql, int top)
        {
            string strSql = "select distinct top(" + top + ") u.*, isnull(sl.c,0) + isnull(u.SystemAmount,0) as Total, isnull(sld.c,0) + isnull(u.SystemAmount,0) as TotalSD "
                 + "from dbo.SYP_Photos u left join "
                + "("
                + "select PhotoId, isnull(count(*),0) as c from SYP_Supporter group by PhotoId "
                + ") sl on u.Id = sl.PhotoId  left join "
                + "("
                + "select PhotoId, isnull(count(*),0) as c from SYP_SupporterForSelected group by PhotoId "
                + ") sld on u.Id = sld.PhotoId"
                + " where  u.IsValidate='1' " + sql
                + "order by TotalSD desc,Total desc";
            return SqlHelper.ExecuteDataset(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString());
        }

        public DataSet GetDataSetListForExp(string sql)
        {
            return SqlHelper.ExecuteDataset(SqlHelper.CONN_STRING, CommandType.Text, sql.ToString());
        }

        /// <summary>
        /// 获得数据列表TOP
        /// </summary>
        public IList<PhotoInfo> GetList(string term, int top)
        {
            string strSql = "select TOP(" + top.ToString() + ")*  FROM SYP_Photos ";
            if (!string.IsNullOrEmpty(term))
            {
                strSql += " where " + term;
            }
            return CBO.FillCollection<PhotoInfo>(
                SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString()));
        }
    }
}