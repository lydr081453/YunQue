using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;

namespace ESP.Finance.DataAccess
{
     
     
    internal class BankDataProvider : ESP.Finance.IDataAccess.IBankDataProvider
    {
        
        #region  成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int BankID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_Bank");
            strSql.Append(" where BankID=@BankID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BankID", SqlDbType.Int,4)};
            parameters[0].Value = BankID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.BankInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_Bank(");
            strSql.Append(@"BranchID,BranchCode,BranchName,DBCode,DBManager,
                            BankName,BankAccountName,Address,BankAccount,PhoneNo,
                            ExchangeNo,RequestPhone,WebBankCode,IsFactoring)");

            strSql.Append(" values (");
            strSql.Append(@"@BranchID,@BranchCode,@BranchName,@DBCode,@DBManager,
                            @BankName,@BankAccountName,@Address,@BankAccount,@PhoneNo,
                            @ExchangeNo,@RequestPhone,@WebBankCode,@IsFactoring)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@BranchID", SqlDbType.Int,4),
					new SqlParameter("@BranchCode", SqlDbType.NVarChar,50),
					new SqlParameter("@BranchName", SqlDbType.NVarChar,50),
					new SqlParameter("@DBCode", SqlDbType.NVarChar,10),
					new SqlParameter("@DBManager", SqlDbType.NVarChar,10),
					new SqlParameter("@BankName", SqlDbType.NVarChar,100),
					new SqlParameter("@BankAccountName", SqlDbType.NVarChar,50),
					new SqlParameter("@Address", SqlDbType.NVarChar,100),
					new SqlParameter("@BankAccount", SqlDbType.NVarChar,50),
					new SqlParameter("@PhoneNo", SqlDbType.NVarChar,50),
					new SqlParameter("@ExchangeNo", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@WebBankCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@IsFactoring", SqlDbType.Int,4)                    
                                        };
            parameters[0].Value =model.BranchID;
            parameters[1].Value =model.BranchCode;
            parameters[2].Value =model.BranchName;
            parameters[3].Value =model.DBCode;
            parameters[4].Value =model.DBManager;
            parameters[5].Value =model.BankName;
            parameters[6].Value =model.BankAccountName;
            parameters[7].Value =model.Address;
            parameters[8].Value =model.BankAccount;
            parameters[9].Value =model.PhoneNo;
            parameters[10].Value =model.ExchangeNo;
            parameters[11].Value =model.RequestPhone;
            parameters[12].Value =model.WebBankCode;
            parameters[13].Value = model.IsFactoring;
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
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Finance.Entity.BankInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Bank set ");
            strSql.Append("BranchID=@BranchID,");
            strSql.Append("BranchCode=@BranchCode,");
            strSql.Append("BranchName=@BranchName,");
            strSql.Append("DBCode=@DBCode,");
            strSql.Append("DBManager=@DBManager,");
            strSql.Append("BankName=@BankName,");
            strSql.Append("BankAccountName=@BankAccountName,");
            strSql.Append("Address=@Address,");
            strSql.Append("BankAccount=@BankAccount,");
            strSql.Append("PhoneNo=@PhoneNo,");
            strSql.Append("ExchangeNo=@ExchangeNo,");
            strSql.Append("RequestPhone=@RequestPhone,");
            strSql.Append("WebBankCode=@WebBankCode,IsFactoring=@IsFactoring");
            strSql.Append(" where BankID=@BankID  ");
            SqlParameter[] parameters = {
					new SqlParameter("@BankID", SqlDbType.Int,4),
					new SqlParameter("@BranchID", SqlDbType.Int,4),
					new SqlParameter("@BranchCode", SqlDbType.NVarChar,50),
					new SqlParameter("@BranchName", SqlDbType.NVarChar,50),
					new SqlParameter("@DBCode", SqlDbType.NVarChar,10),
					new SqlParameter("@DBManager", SqlDbType.NVarChar,10),
					new SqlParameter("@BankName", SqlDbType.NVarChar,100),
					new SqlParameter("@BankAccountName", SqlDbType.NVarChar,50),
					new SqlParameter("@Address", SqlDbType.NVarChar,100),
					new SqlParameter("@BankAccount", SqlDbType.NVarChar,50),
					new SqlParameter("@PhoneNo", SqlDbType.NVarChar,50),
					new SqlParameter("@ExchangeNo", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@WebBankCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@IsFactoring", SqlDbType.Int,4)                    
                                        };
            parameters[0].Value =model.BankID;
            parameters[1].Value =model.BranchID;
            parameters[2].Value =model.BranchCode;
            parameters[3].Value =model.BranchName;
            parameters[4].Value =model.DBCode;
            parameters[5].Value =model.DBManager;
            parameters[6].Value =model.BankName;
            parameters[7].Value =model.BankAccountName;
            parameters[8].Value =model.Address;
            parameters[9].Value =model.BankAccount;
            parameters[10].Value =model.PhoneNo;
            parameters[11].Value =model.ExchangeNo;
            parameters[12].Value =model.RequestPhone;
            parameters[13].Value =model.WebBankCode;
            parameters[14].Value = model.IsFactoring;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int BankID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_Bank ");
            strSql.Append(" where BankID=@BankID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BankID", SqlDbType.Int,4)};
            parameters[0].Value = BankID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.BankInfo GetModel(int BankID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * ");
            strSql.Append(" FROM F_Bank ");
            strSql.Append(" where BankID=@BankID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BankID", SqlDbType.Int,4)};
            parameters[0].Value = BankID;

            return CBO.FillObject<ESP.Finance.Entity.BankInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ESP.Finance.Entity.BankInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM F_Bank where 1=1 ");
            if (!string.IsNullOrEmpty(term))
            {
                if (!term.Trim().StartsWith("and"))
                {
                    strSql.Append(" and "+term);
                }
                else
                {
                    strSql.Append(term);
                }
            }
            return CBO.FillCollection<ESP.Finance.Entity.BankInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }


        #endregion  成员方法
    }
}
