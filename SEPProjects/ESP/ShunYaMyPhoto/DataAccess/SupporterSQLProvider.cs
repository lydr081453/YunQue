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
    public class SupporterSQLProvider
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [SYP_Supporter]");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parameters));
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SupporterInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [SYP_Supporter](");
            strSql.Append(@"[PhotoID]
                               ,[SupporterUserID]
                               ,[Message]
                               ,[CreatedDate],[IP])");
            strSql.Append(" values (");
            strSql.Append(@"@PhotoID
                               ,@SupporterUserID
                               ,@Message
                               ,@CreatedDate,@IP)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@PhotoID", SqlDbType.Int,8),
					new SqlParameter("@SupporterUserID", SqlDbType.Int,8),
					new SqlParameter("@Message", SqlDbType.NVarChar,500),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@IP", SqlDbType.NVarChar,50)};
            parameters[0].Value = model.PhotoID;
            parameters[1].Value = model.SupporterUserID;
            parameters[2].Value = model.Message;
            parameters[3].Value = DateTime.Now;
            parameters[4].Value = model.IP;

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
        /// </summary>
        //public int Update(McAfeeInfo.VisitLogInfo model)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("update VisitLog set ");
        //    strSql.Append("StartDate=@StartDate,");
        //    strSql.Append("EndDate=@EndDate,");
        //    strSql.Append("ArrangementsLevel=@ArrangementsLevel,");
        //    strSql.Append("Amount=@Amount,");
        //    strSql.Append("OverAmount=@OverAmount");
        //    strSql.Append(" where ID=@ID ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ID", SqlDbType.Int,4),
        //            new SqlParameter("@StartDate", SqlDbType.DateTime),
        //            new SqlParameter("@EndDate", SqlDbType.DateTime),
        //            new SqlParameter("@ArrangementsLevel", SqlDbType.NVarChar,10),
        //            new SqlParameter("@Amount", SqlDbType.Int,6),
        //            new SqlParameter("@OverAmount", SqlDbType.Int, 6)};
        //    parameters[0].Value = model.ID;
        //    parameters[1].Value = model.StartDate;
        //    parameters[2].Value = model.EndDate;
        //    parameters[3].Value = model.ArrangementsLevel;
        //    parameters[4].Value = model.Amount;
        //    parameters[5].Value = model.OverAmount;

        //    object obj = SqlHelper.ExecuteScalar(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parameters);
        //    if (obj == null)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return Convert.ToInt32(obj);
        //    }
        //    //return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        //}

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {

            string strSql = "delete SYP_Supporter  where id=" + id.ToString();
            SqlHelper.ExecuteNonQuery(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString());
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SupporterInfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  SYP_Supporter.* from SYP_Supporter ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return CBO.FillObject<SupporterInfo>(
                 SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString(), parameters));


        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<SupporterInfo> GetList(string term)
        {
            string strSql = "select *  FROM SYP_Supporter ";
            if (!string.IsNullOrEmpty(term))
            {
                strSql += " where " + term;
            }
            return CBO.FillCollection<SupporterInfo>(
                SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString()));
        }


        /// <summary>
        /// 获得数据列表TOP
        /// </summary>
        public IList<SupporterInfo> GetList(string term, int top)
        {
            string strSql = "select TOP(" + top.ToString() + ")*  FROM SYP_Supporter ";
            if (!string.IsNullOrEmpty(term))
            {
                strSql += " where " + term;
            }
            return CBO.FillCollection<SupporterInfo>(
                SqlHelper.ExecuteReader(SqlHelper.CONN_STRING, CommandType.Text, strSql.ToString()));
        }
    }
}