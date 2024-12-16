using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;

namespace ESP.Finance.DataAccess
{
    internal class SupportHistoryDataProvider : ESP.Finance.IDataAccess.ISupportHistoryProvider
    {
        #region ISupportHistoryProvider 成员

        public int Add(SupportHistoryInfo model)
        {
            return Add(model, null);
        }

        public int Add(SupportHistoryInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_supporthistory(");
            strSql.Append("SupportId,CommitDate,HistoryData)");
            strSql.Append(" values (");
            strSql.Append("@SupportId,@CommitDate,@HistoryData)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@SupportId", SqlDbType.Int,4),
					new SqlParameter("@CommitDate", SqlDbType.DateTime,8),
                    new SqlParameter("@HistoryData",SqlDbType.Image)
                                        };
            parameters[0].Value = model.SupportId;
            parameters[1].Value = model.CommitDate;
            parameters[2].Value = model.HistoryData;


            object obj = DbHelperSQL.GetSingle(strSql.ToString(), trans, parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public SupportHistoryInfo GetModel(int historyId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 SupportId,CommitDate,HistoryData from F_SupportHistory ");
            strSql.Append(" where HistoryId=@HistoryId ");
            SqlParameter[] parameters = {
					new SqlParameter("@HistoryId", SqlDbType.Int,4)};
            parameters[0].Value = historyId;
            return CBO.FillObject<SupportHistoryInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public IList<SupportHistoryInfo> GetListBySupport(int supportId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SupportId,CommitDate,HistoryData ");
            strSql.Append(" FROM F_SupportHistory where supportId=@supportId ");
            SqlParameter[] parameters = {
					new SqlParameter("@SupportId", SqlDbType.Int,4)};
            parameters[0].Value = supportId;

            return CBO.FillCollection<SupportHistoryInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }


        #endregion
    }
}
