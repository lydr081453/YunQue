using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;
using ESP.Finance.DataAccess;

namespace ESP.Finance.DataAccess
{
   internal class BusinessCardProvider: ESP.Finance.IDataAccess.IBusinessCardProvider
    {
        #region IBusinessCardProvider 成员

        public int Add(BusinessCardInfo model)
        {
            StringBuilder strSql = new StringBuilder();
          strSql.Append("INSERT INTO F_BusinessCard ");
           strSql.Append("([BusinessCardNo]");
           strSql.Append(",[UserId]");
           strSql.Append(",[UserCode]");
           strSql.Append(",[UserName]");
           strSql.Append(",[CardStatus]");
           strSql.Append(",[HouseholdNo]");
           strSql.Append(",[LineOfCredit]");
           strSql.Append(",[AvailableCredit]");
           strSql.Append(",[BeginTime]");
           strSql.Append(",[EndTime]");
           strSql.Append(",[DrawStatus]");
           strSql.Append(",[BranchId]");
           strSql.Append(",[BranchCode]");
           strSql.Append(",[CancellationDate]");
           strSql.Append(",[CreateTime]");
           strSql.Append(",[UpdateTime]) ");
     strSql.Append("VALUES ");
           strSql.Append("(@BusinessCardNo,");
           strSql.Append("@UserId,");
           strSql.Append("@UserCode,");
           strSql.Append("@UserName,");
           strSql.Append("@CardStatus,");
           strSql.Append("@HouseholdNo,");
           strSql.Append("@LineOfCredit,");
           strSql.Append("@AvailableCredit,");
           strSql.Append("@BeginTime,");
           strSql.Append("@EndTime,");
           strSql.Append("@DrawStatus,");
           strSql.Append("@BranchId,");
           strSql.Append("@BranchCode,");
           strSql.Append("@CancellationDate,");
           strSql.Append("@CreateTime,");
           strSql.Append("@UpdateTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@BusinessCardNo", SqlDbType.NVarChar,50),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@CardStatus", SqlDbType.Int,4),
					new SqlParameter("@HouseholdNo", SqlDbType.NVarChar,50),
					new SqlParameter("@LineOfCredit", SqlDbType.Decimal,20),
					new SqlParameter("@AvailableCredit", SqlDbType.Decimal,20),
					new SqlParameter("@BeginTime", SqlDbType.DateTime,8),
					new SqlParameter("@EndTime", SqlDbType.DateTime,8),
                    new SqlParameter("@DrawStatus",SqlDbType.Int,4),
                    new SqlParameter("@BranchId",SqlDbType.Int,4),
                    new SqlParameter("@BranchCode",SqlDbType.NVarChar,5),
                    new SqlParameter("@CancellationDate",SqlDbType.DateTime,8),
                    new SqlParameter("@CreateTime",SqlDbType.DateTime,8),
                    new SqlParameter("@UpdateTime",SqlDbType.DateTime,8)
                                        };
            parameters[0].Value = model.BusinessCardNo;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.UserName;
            parameters[4].Value = model.CardStatus;
            parameters[5].Value = model.HouseholdNo;
            parameters[6].Value = model.LineOfCredit;
            parameters[7].Value = model.AvailableCredit;
            parameters[8].Value = model.BeginTime;
            parameters[9].Value = model.EndTime;
            parameters[10].Value = model.DrawStatus;
            parameters[11].Value = model.BranchId;
            parameters[12].Value = model.BranchCode;
            parameters[13].Value = model.CancellationDate;
            parameters[14].Value = model.CreateTime;
            parameters[15].Value = model.UpdateTime;
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

        public int Update(BusinessCardInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_BusinessCard  set ");
            strSql.Append("[BusinessCardNo]=@BusinessCardNo");
            strSql.Append(",[UserId]=@UserId");
            strSql.Append(",[UserCode]=@UserCode");
            strSql.Append(",[UserName]=@UserName");
            strSql.Append(",[CardStatus]=@CardStatus");
            strSql.Append(",[HouseholdNo]=@HouseholdNo");
            strSql.Append(",[LineOfCredit]=@LineOfCredit");
            strSql.Append(",[AvailableCredit]=@AvailableCredit");
            strSql.Append(",[BeginTime]=@BeginTime");
            strSql.Append(",[EndTime]=@EndTime");
            strSql.Append(",[DrawStatus]=@DrawStatus");
            strSql.Append(",[BranchId]=@BranchId");
            strSql.Append(",[BranchCode]=@BranchCode");
            strSql.Append(",[CancellationDate]=@CancellationDate");
            strSql.Append(",[CreateTime]=@CreateTime");
            strSql.Append(",[UpdateTime]=@UpdateTime where BusinessCardId=@BusinessCardId");
            SqlParameter[] parameters = {
					new SqlParameter("@BusinessCardNo", SqlDbType.NVarChar,50),
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@CardStatus", SqlDbType.Int,4),
					new SqlParameter("@HouseholdNo", SqlDbType.NVarChar,50),
					new SqlParameter("@LineOfCredit", SqlDbType.Decimal,20),
					new SqlParameter("@AvailableCredit", SqlDbType.Decimal,20),
					new SqlParameter("@BeginTime", SqlDbType.DateTime,8),
					new SqlParameter("@EndTime", SqlDbType.DateTime,8),
                    new SqlParameter("@DrawStatus",SqlDbType.Int,4),
                    new SqlParameter("@BranchId",SqlDbType.Int,4),
                    new SqlParameter("@BranchCode",SqlDbType.NVarChar,5),
                    new SqlParameter("@CancellationDate",SqlDbType.DateTime,8),
                    new SqlParameter("@CreateTime",SqlDbType.DateTime,8),
                    new SqlParameter("@UpdateTime",SqlDbType.DateTime,8),
                    new SqlParameter("@BusinessCardId",SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.BusinessCardNo;
            parameters[1].Value = model.UserId;
            parameters[2].Value = model.UserCode;
            parameters[3].Value = model.UserName;
            parameters[4].Value = model.CardStatus;
            parameters[5].Value = model.HouseholdNo;
            parameters[6].Value = model.LineOfCredit;
            parameters[7].Value = model.AvailableCredit;
            parameters[8].Value = model.BeginTime;
            parameters[9].Value = model.EndTime;
            parameters[10].Value = model.DrawStatus;
            parameters[11].Value = model.BranchId;
            parameters[12].Value = model.BranchCode;
            parameters[13].Value = model.CancellationDate;
            parameters[14].Value = model.CreateTime;
            parameters[15].Value = model.UpdateTime;
            parameters[16].Value = model.BusinessCardId;
            object obj = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public int Delete(int bcid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_BusinessCard ");
            strSql.Append(" where BusinessCardId=@BusinessCardId ");
            SqlParameter[] parameters = {
					new SqlParameter("@BusinessCardId", SqlDbType.Int,4)};
            parameters[0].Value = bcid;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public BusinessCardInfo GetModel(int bcid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT [BusinessCardId]
                          ,[BusinessCardNo]
                          ,[UserId]
                          ,[UserCode]
                          ,[UserName]
                          ,[CardStatus]
                          ,[HouseholdNo]
                          ,[LineOfCredit]
                          ,[AvailableCredit]
                          ,[BeginTime]
                          ,[EndTime]
                          ,[DrawStatus]
                          ,[BranchId]
                          ,[BranchCode]
                          ,[CancellationDate]
                          ,[CreateTime]
                          ,[UpdateTime]
                      FROM [ESP].[dbo].[F_BusinessCard]");
            strSql.Append(" where BusinessCardId=@BusinessCardId ");
            SqlParameter[] parameters = {
					new SqlParameter("@BusinessCardId", SqlDbType.Int,4)};
            parameters[0].Value = bcid;

            return CBO.FillObject<BusinessCardInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        public IList<BusinessCardInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT [BusinessCardId]
                          ,[BusinessCardNo]
                          ,[UserId]
                          ,[UserCode]
                          ,[UserName]
                          ,[CardStatus]
                          ,[HouseholdNo]
                          ,[LineOfCredit]
                          ,[AvailableCredit]
                          ,[BeginTime]
                          ,[EndTime]
                          ,[DrawStatus]
                          ,[BranchId]
                          ,[BranchCode]
                          ,[CancellationDate]
                          ,[CreateTime]
                          ,[UpdateTime]
                      FROM [ESP].[dbo].[F_BusinessCard]");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            return CBO.FillCollection<BusinessCardInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }

        public int IsHaveCard(int userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT count(*)
                      FROM F_BusinessCard where userid=@UserId");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4)
                                          };
            parameters[0].Value = userid;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        }

        public BusinessCardInfo GetBusinessCard(int userid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT top 1 * FROM F_BusinessCard where userid=@UserId");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4)};
            parameters[0].Value = userid;

            return CBO.FillObject<BusinessCardInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        #endregion
    }
}
