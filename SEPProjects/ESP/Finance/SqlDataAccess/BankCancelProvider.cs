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
        /// <summary>
        /// 数据访问类BankCancelProvider。
        /// </summary>
        /// 

    public class BankCancelProvider : ESP.Finance.IDataAccess.IBankCancelProvider
        {
            public BankCancelProvider()
            { }
            #region  成员方法
            /// <summary>
            /// 是否存在该记录
            /// </summary>
            public bool Exists(int LogID)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select count(1) from F_BankCancel");
                strSql.Append(" where LogID=@LogID ");
                SqlParameter[] parameters = {
					new SqlParameter("@LogID", SqlDbType.Int,4)};
                parameters[0].Value = LogID;

                return DbHelperSQL.Exists(strSql.ToString(), parameters);
            }

            public int Add(ESP.Finance.Entity.BankCancelInfo model)
            {
                return Add(model, null);
            }

            /// <summary>
            /// 增加一条数据
            /// </summary>
            public int Add(ESP.Finance.Entity.BankCancelInfo model,SqlTransaction trans)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into F_BankCancel(");
                strSql.Append("OldBankName,NewBankAccountName,NewBankAccount,NewBankName,CancelDate,CommitDate,LastUpdateTime,Integral,OperationID,OperationCode,ReturnID,OperationName,OperationEmpName,RequestorID,RequestorCode,RequestorName,RequestorEmpName,ReturnCode,OldBankAccountName,OldBankAccount,RePaymentSuggestion,OrderType)");
                strSql.Append(" values (");
                strSql.Append("@OldBankName,@NewBankAccountName,@NewBankAccount,@NewBankName,@CancelDate,@CommitDate,@LastUpdateTime,@Integral,@OperationID,@OperationCode,@ReturnID,@OperationName,@OperationEmpName,@RequestorID,@RequestorCode,@RequestorName,@RequestorEmpName,@ReturnCode,@OldBankAccountName,@OldBankAccount,@RePaymentSuggestion,@OrderType)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
					new SqlParameter("@OldBankName", SqlDbType.NVarChar,200),
					new SqlParameter("@NewBankAccountName", SqlDbType.NVarChar,200),
					new SqlParameter("@NewBankAccount", SqlDbType.NVarChar,50),
					new SqlParameter("@NewBankName", SqlDbType.NVarChar,200),
					new SqlParameter("@CancelDate", SqlDbType.DateTime),
					new SqlParameter("@CommitDate", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Integral", SqlDbType.Int,4),
					new SqlParameter("@OperationID", SqlDbType.Int,4),
					new SqlParameter("@OperationCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ReturnID", SqlDbType.Int,4),
					new SqlParameter("@OperationName", SqlDbType.NVarChar,50),
					new SqlParameter("@OperationEmpName", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestorID", SqlDbType.Int,4),
					new SqlParameter("@RequestorCode", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestorName", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestorEmpName", SqlDbType.NVarChar,50),
					new SqlParameter("@ReturnCode", SqlDbType.NVarChar,50),
					new SqlParameter("@OldBankAccountName", SqlDbType.NVarChar,200),
					new SqlParameter("@OldBankAccount", SqlDbType.NVarChar,50),
                    new SqlParameter("@RePaymentSuggestion", SqlDbType.NVarChar,500),
                    new SqlParameter("@OrderType",SqlDbType.Int,4)
                                            };
                parameters[0].Value = model.OldBankName;
                parameters[1].Value = model.NewBankAccountName;
                parameters[2].Value = model.NewBankAccount;
                parameters[3].Value = model.NewBankName;
                parameters[4].Value = model.CancelDate;
                parameters[5].Value = model.CommitDate;
                parameters[6].Value = model.LastUpdateTime;
                parameters[7].Value = model.Integral;
                parameters[8].Value = model.OperationID;
                parameters[9].Value = model.OperationCode;
                parameters[10].Value = model.ReturnID;
                parameters[11].Value = model.OperationName;
                parameters[12].Value = model.OperationEmpName;
                parameters[13].Value = model.RequestorID;
                parameters[14].Value = model.RequestorCode;
                parameters[15].Value = model.RequestorName;
                parameters[16].Value = model.RequestorEmpName;
                parameters[17].Value = model.ReturnCode;
                parameters[18].Value = model.OldBankAccountName;
                parameters[19].Value = model.OldBankAccount;
                parameters[20].Value = model.RePaymentSuggestion;
                parameters[21].Value = model.OrderType;

                object obj = DbHelperSQL.GetSingle(strSql.ToString(),trans, parameters);
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
            public int Update(ESP.Finance.Entity.BankCancelInfo model)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update F_BankCancel set ");
                strSql.Append("OldBankName=@OldBankName,");
                strSql.Append("NewBankAccountName=@NewBankAccountName,");
                strSql.Append("NewBankAccount=@NewBankAccount,");
                strSql.Append("NewBankName=@NewBankName,");
                strSql.Append("CancelDate=@CancelDate,");
                strSql.Append("CommitDate=@CommitDate,");
                strSql.Append("LastUpdateTime=@LastUpdateTime,");
                strSql.Append("Integral=@Integral,");
                strSql.Append("OperationID=@OperationID,");
                strSql.Append("OperationCode=@OperationCode,");
                strSql.Append("ReturnID=@ReturnID,");
                strSql.Append("OperationName=@OperationName,");
                strSql.Append("OperationEmpName=@OperationEmpName,");
                strSql.Append("RequestorID=@RequestorID,");
                strSql.Append("RequestorCode=@RequestorCode,");
                strSql.Append("RequestorName=@RequestorName,");
                strSql.Append("RequestorEmpName=@RequestorEmpName,");
                strSql.Append("ReturnCode=@ReturnCode,");
                strSql.Append("OldBankAccountName=@OldBankAccountName,");
                strSql.Append("OldBankAccount=@OldBankAccount,");
                strSql.Append("RePaymentSuggestion=@RePaymentSuggestion,");
                strSql.Append("OrderType=@OrderType");
                strSql.Append(" where LogID=@LogID ");
                SqlParameter[] parameters = {
					new SqlParameter("@LogID", SqlDbType.Int,4),
					new SqlParameter("@OldBankName", SqlDbType.NVarChar,200),
					new SqlParameter("@NewBankAccountName", SqlDbType.NVarChar,200),
					new SqlParameter("@NewBankAccount", SqlDbType.NVarChar,50),
					new SqlParameter("@NewBankName", SqlDbType.NVarChar,200),
					new SqlParameter("@CancelDate", SqlDbType.DateTime),
					new SqlParameter("@CommitDate", SqlDbType.DateTime),
					new SqlParameter("@LastUpdateTime", SqlDbType.DateTime),
					new SqlParameter("@Integral", SqlDbType.Int,4),
					new SqlParameter("@OperationID", SqlDbType.Int,4),
					new SqlParameter("@OperationCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ReturnID", SqlDbType.Int,4),
					new SqlParameter("@OperationName", SqlDbType.NVarChar,50),
					new SqlParameter("@OperationEmpName", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestorID", SqlDbType.Int,4),
					new SqlParameter("@RequestorCode", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestorName", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestorEmpName", SqlDbType.NVarChar,50),
					new SqlParameter("@ReturnCode", SqlDbType.NVarChar,50),
					new SqlParameter("@OldBankAccountName", SqlDbType.NVarChar,200),
					new SqlParameter("@OldBankAccount", SqlDbType.NVarChar,50),
                    new SqlParameter("@RePaymentSuggestion", SqlDbType.NVarChar,500),
                    new SqlParameter("@OrderType",SqlDbType.Int,4)
                                            };
                parameters[0].Value = model.LogID;
                parameters[1].Value = model.OldBankName;
                parameters[2].Value = model.NewBankAccountName;
                parameters[3].Value = model.NewBankAccount;
                parameters[4].Value = model.NewBankName;
                parameters[5].Value = model.CancelDate;
                parameters[6].Value = model.CommitDate;
                parameters[7].Value = model.LastUpdateTime;
                parameters[8].Value = model.Integral;
                parameters[9].Value = model.OperationID;
                parameters[10].Value = model.OperationCode;
                parameters[11].Value = model.ReturnID;
                parameters[12].Value = model.OperationName;
                parameters[13].Value = model.OperationEmpName;
                parameters[14].Value = model.RequestorID;
                parameters[15].Value = model.RequestorCode;
                parameters[16].Value = model.RequestorName;
                parameters[17].Value = model.RequestorEmpName;
                parameters[18].Value = model.ReturnCode;
                parameters[19].Value = model.OldBankAccountName;
                parameters[20].Value = model.OldBankAccount;
                parameters[21].Value = model.RePaymentSuggestion;
                parameters[22].Value = model.OrderType;
                return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            }

            /// <summary>
            /// 删除一条数据
            /// </summary>
            public int Delete(int LogID)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete F_BankCancel ");
                strSql.Append(" where LogID=@LogID ");
                SqlParameter[] parameters = {
					new SqlParameter("@LogID", SqlDbType.Int,4)};
                parameters[0].Value = LogID;

               return  DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            }


            /// <summary>
            /// 得到一个对象实体
            /// </summary>
            public ESP.Finance.Entity.BankCancelInfo GetModel(int LogID)
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("select  top 1 LogID,OldBankName,NewankAccountName,NewBankAccount,NewBankName,CancelDate,CommitDate,LastUpdateTime,Integral,OperationID,OperationCode,ReturnID,OperationName,OperationEmpName,RequestorID,RequestorCode,RequestorName,RequestorEmpName,ReturnCode,OldBankAccountName,OldBankAccount,RePaymentSuggestion,OrderType from F_BankCancel ");
                strSql.Append(" where LogID=@LogID ");
                SqlParameter[] parameters = {
					new SqlParameter("@LogID", SqlDbType.Int,4)};
                parameters[0].Value = LogID;
                return CBO.FillObject<ESP.Finance.Entity.BankCancelInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
            }

            /// <summary>
            /// 获得数据列表
            /// </summary>
            public IList<ESP.Finance.Entity.BankCancelInfo> GetList(string term, List<SqlParameter> paramList)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select LogID,OldBankName,NewBankAccountName,NewBankAccount,NewBankName,CancelDate,CommitDate,LastUpdateTime,Integral,OperationID,OperationCode,ReturnID,OperationName,OperationEmpName,RequestorID,RequestorCode,RequestorName,RequestorEmpName,ReturnCode,OldBankAccountName,OldBankAccount,RePaymentSuggestion ");
                strSql.Append(" FROM F_BankCancel where ordertype=1 ");
                if (!string.IsNullOrEmpty(term))
                {
                    if (!term.Trim().StartsWith("and"))
                    {
                        strSql.Append(" and " + term);
                    }
                    else
                    {
                        strSql.Append(term);
                    }
                }
                return CBO.FillCollection<ESP.Finance.Entity.BankCancelInfo>(DbHelperSQL.Query(strSql.ToString(), paramList));
            }

            public IList<ESP.Finance.Entity.BankCancelInfo> GetBatchList(string term, List<SqlParameter> paramList)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select LogID,OldBankName,NewBankAccountName,NewBankAccount,NewBankName,CancelDate,CommitDate,LastUpdateTime,Integral,OperationID,OperationCode,ReturnID,OperationName,OperationEmpName,RequestorID,RequestorCode,RequestorName,RequestorEmpName,ReturnCode,OldBankAccountName,OldBankAccount,RePaymentSuggestion ");
                strSql.Append(" FROM F_BankCancel where ordertype=2 ");
                if (!string.IsNullOrEmpty(term))
                {
                    if (!term.Trim().StartsWith("and"))
                    {
                        strSql.Append(" and " + term);
                    }
                    else
                    {
                        strSql.Append(term);
                    }
                }
                return CBO.FillCollection<ESP.Finance.Entity.BankCancelInfo>(DbHelperSQL.Query(strSql.ToString(), paramList));
            }

            #endregion  成员方法
        }
    }

