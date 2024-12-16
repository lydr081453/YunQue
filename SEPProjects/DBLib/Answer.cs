using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DBLib
{
    public class Answer
    {
        public DataSet AnswerSelect(int ID)
        {
            Database dbObj = DatabaseFactory.CreateDatabase();
            string strProcName = "[dbo].[sp_E_Result_Select]";
            DbCommand dbCommand = dbObj.GetStoredProcCommand(strProcName);
            dbObj.AddInParameter(dbCommand, "ID", DbType.Int32, ID);
            DataSet ds = dbObj.ExecuteDataSet(dbCommand);
            dbCommand.Dispose();
            return ds;

        }

        public DataSet AnswerSelectBySQL(string SqlStr)
        {
            //SELECT * FROM E_Result where 1=1
            Database dbObj = DatabaseFactory.CreateDatabase();
            string strProcName = "[dbo].[sp_E_Result_Select_SelectBySQL]";
            DbCommand dbCommand = dbObj.GetStoredProcCommand(strProcName);
            dbObj.AddInParameter(dbCommand, "SqlStr", DbType.String, SqlStr);
            DataSet ds = dbObj.ExecuteDataSet(dbCommand);
            dbCommand.Dispose();
            return ds;

        }

        public int GetAnswerCount(int userId,int examtime)
        {
            int ret = 0;
            Database dbObj = DatabaseFactory.CreateDatabase();
            string strProcName = "select * from E_Result where userid=@UserID";
            DbCommand dbCommand = dbObj.GetSqlStringCommand(strProcName);
            dbObj.AddInParameter(dbCommand, "@UserID", DbType.Int32, userId);
            DataTable  dt = dbObj.ExecuteDataSet(dbCommand).Tables[0];
            dbCommand.Dispose();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DateTime beginDate = (DateTime)dt.Rows[i]["CreateDate"];
                    DateTime commitDate = DateTime.Now;
                    if (dt.Rows[i]["CommitDate"] != DBNull.Value && (DateTime)dt.Rows[i]["CommitDate"] != new DateTime(1900, 1, 1))
                    {
                        commitDate = (DateTime)dt.Rows[i]["CommitDate"];
                    }
                    TimeSpan differ = commitDate.Subtract(beginDate);
                    int differValue = (int)differ.TotalSeconds;
                    int result = (examtime * 60 - differValue);
                    if (result <= 0 || int.Parse(dt.Rows[i]["Status"].ToString())==1)
                    {
                        ret++;
                    }
                 }
            }
            return ret;
        }

        public int GetUserLevel(string userId)
        {
            string directs = "," + ESP.Framework.BusinessLogic.OperationAuditManageManager.GetDirectorIds() + "," + ESP.Framework.BusinessLogic.OperationAuditManageManager.GetManagerIds() + ",";
            string financeExamA = "," + ESP.Configuration.ConfigurationManager.SafeAppSettings["FinanceExamA"] + ",";
            string financeExamB = "," + ESP.Configuration.ConfigurationManager.SafeAppSettings["FinanceExamB"] + ",";
            string financeExamC = "," + ESP.Configuration.ConfigurationManager.SafeAppSettings["FinanceExamC"] + ",";
            string PurchaseExam = "," + ESP.Configuration.ConfigurationManager.SafeAppSettings["PurchaseExam"] + ",";

            if (directs.IndexOf("," + userId + ",") < 0)
            {
                if (financeExamA.IndexOf("," + userId + ",") >= 0)//财务任媛戴琼
                    return 3;
                else if (financeExamB.IndexOf("," + userId + ",") >= 0)//财务付款
                    return 4;
                else if (financeExamC.IndexOf("," + userId + ",") >= 0)//财务分公司和FA
                    return 5;
                else if (PurchaseExam.IndexOf("," + userId + ",") >= 0)//采购北京
                    return 6;
                else //其他业务
                    return 1;
            }
            else//总监
                return 2;
        }

        public DataTable  GetResultDetail(string term)
        {
            Database dbObj = DatabaseFactory.CreateDatabase();
            string strProcName = "select * from E_ResultDetail where 1=1 " + term;
            DbCommand dbCommand = dbObj.GetSqlStringCommand(strProcName);
            DataSet  ds = dbObj.ExecuteDataSet(dbCommand);
            dbCommand.Dispose();
            return ds.Tables[0];

        }

        public DataSet AnswerSelectByUser(int ID)
        {
            Database dbObj = DatabaseFactory.CreateDatabase();
            string strProcName = "[dbo].[sp_E_Result_SelectByUser]";
            DbCommand dbCommand = dbObj.GetStoredProcCommand(strProcName);
            dbObj.AddInParameter(dbCommand, "ID", DbType.Int32, ID);
            DataSet ds = dbObj.ExecuteDataSet(dbCommand);
            dbCommand.Dispose();
            return ds;

        }

        public DataSet AnswerChoice(int ID)
        {
            Database dbObj = DatabaseFactory.CreateDatabase();
            string strProcName = "[dbo].[sp_E_ResultDetail_Select]";
            DbCommand dbCommand = dbObj.GetStoredProcCommand(strProcName);
            dbObj.AddInParameter(dbCommand, "ID", DbType.Int32, ID);
            DataSet ds = dbObj.ExecuteDataSet(dbCommand);
            dbCommand.Dispose();
            return ds;

        }


        public bool AnswerInsert(int UserID, string UserName, int DepartmentID, string DepartmentName, int GroupID, int TotalPoint,string ip, DateTime CreateDate, DateTime CommitDate,int status, ref int ID)
        {
            Database dbObj = DatabaseFactory.CreateDatabase();
            using (IDbConnection IDBConn = dbObj.CreateConnection())
            {
                IDBConn.Open();
                IDbTransaction IDBTrans = IDBConn.BeginTransaction();
                try
                {
                    string strProcName = "[dbo].[sp_E_Result_Insert]";
                    DbCommand dbCommand = dbObj.GetStoredProcCommand(strProcName);
                    dbObj.AddInParameter(dbCommand, "UserID", DbType.Int32, UserID);
                    dbObj.AddInParameter(dbCommand, "UserName", DbType.String, UserName);
                    dbObj.AddInParameter(dbCommand, "DepartmentID", DbType.Int32, DepartmentID);
                    dbObj.AddInParameter(dbCommand, "DepartmentName", DbType.String, DepartmentName);
                    dbObj.AddInParameter(dbCommand, "GroupID", DbType.Int32, GroupID);
                    dbObj.AddInParameter(dbCommand, "TotalPoint", DbType.Int32, TotalPoint);
                    dbObj.AddInParameter(dbCommand, "CreateDate", DbType.DateTime, CreateDate);
                    dbObj.AddInParameter(dbCommand, "CommitDate", DbType.DateTime, CommitDate);
                    dbObj.AddInParameter(dbCommand, "IP", DbType.String,ip);
                    dbObj.AddInParameter(dbCommand, "Status", DbType.Int32, status);
                    dbObj.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
                    dbObj.ExecuteNonQuery(dbCommand);
                    ID = int.Parse(dbObj.GetParameterValue(dbCommand, "ID").ToString());
                    dbCommand.Dispose();
                    IDBTrans.Commit();
                }
                catch (Exception ex)
                {
                    IDBTrans.Rollback();
                    return false;
                }
                finally
                {
                    if (IDBConn.State != ConnectionState.Closed)
                    {
                        IDBConn.Close();
                    }
                }
            }
            return true;
        }

        public bool AnswerUpdate(int ID, int TotalPoint,int status,int userid)
        {
            Database dbObj = DatabaseFactory.CreateDatabase();
            using (IDbConnection IDBConn = dbObj.CreateConnection())
            {
                IDBConn.Open();
                IDbTransaction IDBTrans = IDBConn.BeginTransaction();
                try
                {
                    string strProcName = "[dbo].[sp_E_Result_Update]";
                    DbCommand dbCommand = dbObj.GetStoredProcCommand(strProcName);
                    dbObj.AddInParameter(dbCommand, "ID", DbType.Int32, ID);
                    dbObj.AddInParameter(dbCommand, "TotalPoint", DbType.Int32, TotalPoint);
                    dbObj.AddInParameter(dbCommand, "Status", DbType.Int32, status);
                    dbObj.AddInParameter(dbCommand, "CommitDate", DbType.DateTime, DateTime.Now);
                    dbObj.AddInParameter(dbCommand, "UserID", DbType.Int32,userid);
                    dbObj.ExecuteNonQuery(dbCommand);
                    IDBTrans.Commit();
                }
                catch (Exception ex)
                {
                    IDBTrans.Rollback();
                    return false;
                }
                finally
                {
                    if (IDBConn.State != ConnectionState.Closed)
                    {
                        IDBConn.Close();
                    }
                }
            }
            return true;
        }

        public bool AnswerResultInsert(int QuestionID,string Title,string CorrectResult,string AnswerStr,int ResultID ,ref int ID)
        {
            Database dbObj = DatabaseFactory.CreateDatabase();
            using (IDbConnection IDBConn = dbObj.CreateConnection())
            {
                IDBConn.Open();
                IDbTransaction IDBTrans = IDBConn.BeginTransaction();
                try
                {
                    string strProcName = "[dbo].[sp_E_ResultDetail_Insert]";
                    DbCommand dbCommand = dbObj.GetStoredProcCommand(strProcName);
                    dbObj.AddInParameter(dbCommand, "QID", DbType.Int32, QuestionID);
                    dbObj.AddInParameter(dbCommand, "Title", DbType.String, Title);
                    dbObj.AddInParameter(dbCommand, "CorrectResult", DbType.String, CorrectResult);
                    dbObj.AddInParameter(dbCommand, "Answer", DbType.String, AnswerStr);
                    dbObj.AddInParameter(dbCommand, "ResultID", DbType.Int32, ResultID);
                    dbObj.AddOutParameter(dbCommand, "ID", DbType.Int32, 4);
                    dbObj.ExecuteNonQuery(dbCommand);
                    ID = int.Parse(dbObj.GetParameterValue(dbCommand, "ID").ToString());
                    dbCommand.Dispose();
                    IDBTrans.Commit();
                }
                catch (Exception ex)
                {
                    IDBTrans.Rollback();
                    return false;
                }
                finally
                {
                    if (IDBConn.State != ConnectionState.Closed)
                    {
                        IDBConn.Close();
                    }
                }
            }
            return true;
        }

    }
}
