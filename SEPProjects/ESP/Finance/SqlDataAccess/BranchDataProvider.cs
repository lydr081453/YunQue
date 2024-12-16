using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using System.Collections.Generic;
using ESP.Finance.Utility;
//using Maticsoft.DBUtility;//请先添加引用
namespace ESP.Finance.DataAccess
{
    /// <summary>
    /// 数据访问类F_Branch。
    /// </summary>
    internal class BranchDataProvider : ESP.Finance.IDataAccess.IBranchDataProvider
    {

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int BranchID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_Branch");
            strSql.Append(" where BranchID=@BranchID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BranchID", SqlDbType.Int,4)};
            parameters[0].Value = BranchID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.BranchInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_Branch(");
            strSql.Append("ExchangeNo,RequestPhone,BranchCode,BranchName,Des,DBCode,DBManager,BankName,BankAccount,PhoneNo,FirstFinanceID,PaymentAccounter,ProjectAccounter,ContractAccounter,FinalAccounter,OtherFinancialUsers,DepartmentId,SalaryAccounter,BusinessCardAccounter,DimissionAuditor,GMUsers,Logo,POTerm,POStandard)");
            strSql.Append(" values (");
            strSql.Append("@ExchangeNo,@RequestPhone,@BranchCode,@BranchName,@Des,@DBCode,@DBManager,@BankName,@BankAccount,@PhoneNo,@FirstFinanceID,@PaymentAccounter,@ProjectAccounter,@ContractAccounter,@FinalAccounter,@OtherFinancialUsers,@DepartmentId,@SalaryAccounter,@BusinessCardAccounter,@DimissionAuditor,@GMUsers,@Logo,@POTerm,@POStandard)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@ExchangeNo", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@BranchCode", SqlDbType.NChar,10),
					new SqlParameter("@BranchName", SqlDbType.NVarChar,50),
					new SqlParameter("@Des", SqlDbType.NVarChar,500),
					new SqlParameter("@DBCode", SqlDbType.NVarChar,10),
					new SqlParameter("@DBManager", SqlDbType.NVarChar,10),
					new SqlParameter("@BankName", SqlDbType.NVarChar,100),
					new SqlParameter("@BankAccount", SqlDbType.NVarChar,50),
					new SqlParameter("@PhoneNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@FirstFinanceID",SqlDbType.Int,4),
                    new SqlParameter("@PaymentAccounter",SqlDbType.Int,4),
                    new SqlParameter("@ProjectAccounter",SqlDbType.Int,4),
                    new SqlParameter("@ContractAccounter",SqlDbType.Int,4),
                    new SqlParameter("@FinalAccounter",SqlDbType.Int,4),
                    new SqlParameter("@OtherFinancialUsers",SqlDbType.NVarChar,200),
                    new SqlParameter("@DepartmentId",SqlDbType.Int,4),
                    new SqlParameter("@SalaryAccounter",SqlDbType.Int,4),
                    new SqlParameter("@BusinessCardAccounter",SqlDbType.NVarChar,500),
                    new SqlParameter("@DimissionAuditor",SqlDbType.Int,4),
                    new SqlParameter("@GMUsers",SqlDbType.NVarChar,500),
                    new SqlParameter("@Logo",SqlDbType.NVarChar,500),
                    new SqlParameter("@POTerm",SqlDbType.NVarChar,500),
                    new SqlParameter("@POStandard",SqlDbType.NVarChar,500)
                                        };
            parameters[0].Value = model.ExchangeNo;
            parameters[1].Value = model.RequestPhone;
            parameters[2].Value = model.BranchCode;
            parameters[3].Value = model.BranchName;
            parameters[4].Value = model.Des;
            parameters[5].Value = model.DBCode;
            parameters[6].Value = model.DBManager;
            parameters[7].Value = model.BankName;
            parameters[8].Value = model.BankAccount;
            parameters[9].Value = model.PhoneNo;
            parameters[10].Value = model.FirstFinanceID;
            parameters[11].Value = model.PaymentAccounter;
            parameters[12].Value = model.ProjectAccounter;
            parameters[13].Value = model.ContractAccounter;
            parameters[14].Value = model.FinalAccounter;
            parameters[15].Value = model.OtherFinancialUsers;
            parameters[16].Value = model.DepartmentId;
            parameters[17].Value = model.SalaryAccounter;
            parameters[18].Value = model.BusinessCardAccounter;
            parameters[19].Value = model.DimissionAuditor;
            parameters[20].Value = model.GMUsers;
            parameters[21].Value = model.Logo;
            parameters[22].Value = model.POTerm;
            parameters[23].Value = model.POStandard;

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
        public int Update(ESP.Finance.Entity.BranchInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Branch set ");
            strSql.Append("ExchangeNo=@ExchangeNo,");
            strSql.Append("RequestPhone=@RequestPhone,");
            strSql.Append("BranchCode=@BranchCode,");
            strSql.Append("BranchName=@BranchName,");
            strSql.Append("Des=@Des,");
            strSql.Append("DBCode=@DBCode,");
            strSql.Append("DBManager=@DBManager,");
            strSql.Append("BankName=@BankName,");
            strSql.Append("BankAccount=@BankAccount,");
            strSql.Append("PhoneNo=@PhoneNo,");
            strSql.Append("FirstFinanceID=@FirstFinanceID,");
            strSql.Append("PaymentAccounter=@PaymentAccounter,");
            strSql.Append("ProjectAccounter=@ProjectAccounter,");
            strSql.Append("ContractAccounter=@ContractAccounter,");
            strSql.Append("FinalAccounter=@FinalAccounter,OtherFinancialUsers=@OtherFinancialUsers,DepartmentId=@DepartmentId,");
            strSql.Append("SalaryAccounter=@SalaryAccounter,");
            strSql.Append("BusinessCardAccounter=@BusinessCardAccounter,DimissionAuditor=@DimissionAuditor,GMUsers=@GMUsers,Logo=@Logo,POTerm=@POTerm,POStandard=@POStandard");
            strSql.Append(" where BranchID=@BranchID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BranchID", SqlDbType.Int,4),
					new SqlParameter("@ExchangeNo", SqlDbType.NVarChar,50),
					new SqlParameter("@RequestPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@BranchCode", SqlDbType.NChar,10),
					new SqlParameter("@BranchName", SqlDbType.NVarChar,50),
					new SqlParameter("@Des", SqlDbType.NVarChar,500),
					new SqlParameter("@DBCode", SqlDbType.NVarChar,10),
					new SqlParameter("@DBManager", SqlDbType.NVarChar,10),
					new SqlParameter("@BankName", SqlDbType.NVarChar,100),
					new SqlParameter("@BankAccount", SqlDbType.NVarChar,50),
					new SqlParameter("@PhoneNo", SqlDbType.NVarChar,50),
                    new SqlParameter("@FirstFinanceID",SqlDbType.Int,4) ,
                    new SqlParameter("@PaymentAccounter",SqlDbType.Int,4),
                    new SqlParameter("@ProjectAccounter",SqlDbType.Int,4),
                    new SqlParameter("@ContractAccounter",SqlDbType.Int,4),
                    new SqlParameter("@FinalAccounter",SqlDbType.Int,4),
                    new SqlParameter("@OtherFinancialUsers",SqlDbType.NVarChar,200),
                    new SqlParameter("@DepartmentId",SqlDbType.Int,4),
                    new SqlParameter("@SalaryAccounter",SqlDbType.Int,4),
                    new SqlParameter("@BusinessCardAccounter",SqlDbType.NVarChar,500),
                    new SqlParameter("@DimissionAuditor",SqlDbType.Int,4),
                    new SqlParameter("@GMUsers",SqlDbType.NVarChar,500),
                    new SqlParameter("@Logo",SqlDbType.NVarChar,500),
                    new SqlParameter("@POTerm",SqlDbType.NVarChar,500),
                    new SqlParameter("@POStandard",SqlDbType.NVarChar,500)
                                        };
            parameters[0].Value = model.BranchID;
            parameters[1].Value = model.ExchangeNo;
            parameters[2].Value = model.RequestPhone;
            parameters[3].Value = model.BranchCode;
            parameters[4].Value = model.BranchName;
            parameters[5].Value = model.Des;
            parameters[6].Value = model.DBCode;
            parameters[7].Value = model.DBManager;
            parameters[8].Value = model.BankName;
            parameters[9].Value = model.BankAccount;
            parameters[10].Value = model.PhoneNo;
            parameters[11].Value = model.FirstFinanceID;
            parameters[12].Value = model.PaymentAccounter;
            parameters[13].Value = model.ProjectAccounter;
            parameters[14].Value = model.ContractAccounter;
            parameters[15].Value = model.FinalAccounter;
            parameters[16].Value = model.OtherFinancialUsers;
            parameters[17].Value = model.DepartmentId;
            parameters[18].Value = model.SalaryAccounter;
            parameters[19].Value = model.BusinessCardAccounter;
            parameters[20].Value = model.DimissionAuditor;
            parameters[21].Value = model.GMUsers;
            parameters[22].Value = model.Logo;
            parameters[23].Value = model.POTerm;
            parameters[24].Value = model.POStandard;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int BranchID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_Branch ");
            strSql.Append(" where BranchID=@BranchID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BranchID", SqlDbType.Int,4)};
            parameters[0].Value = BranchID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public ESP.Finance.Entity.BranchInfo GetModelByCode(string Code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select  top 1 * from F_Branch ");
            strSql.Append(" where BranchCode=@BranchCode ");
            SqlParameter[] parameters = {
					new SqlParameter("@BranchCode", SqlDbType.NVarChar,10)};
            parameters[0].Value = Code;

            return CBO.FillObject<BranchInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }
        public ESP.Finance.Entity.BranchInfo GetModelByCode(string Code, System.Data.SqlClient.SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select  top 1 * from F_Branch ");
            strSql.Append(" where BranchCode=@BranchCode ");
            SqlParameter[] parameters = {
					new SqlParameter("@BranchCode", SqlDbType.NVarChar,10)};
            parameters[0].Value = Code;

            return CBO.FillObject<BranchInfo>(DbHelperSQL.Query(strSql.ToString(), trans, parameters));
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.BranchInfo GetModel(int BranchID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select  top 1 * from F_Branch ");
            strSql.Append(" where BranchID=@BranchID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BranchID", SqlDbType.Int,4)};
            parameters[0].Value = BranchID;

            return CBO.FillObject<BranchInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));

        }

        public ESP.Finance.Entity.BranchInfo GetModel(int BranchID, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select  top 1 * from F_Branch ");
            strSql.Append(" where BranchID=@BranchID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BranchID", SqlDbType.Int,4)};
            parameters[0].Value = BranchID;

            return CBO.FillObject<BranchInfo>(DbHelperSQL.Query(strSql.ToString(), trans, parameters));

        }

        public string GetLevel1Users(int departmentId)
        {
            if (departmentId == 0)
                return null;
            string ret = string.Empty;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select  distinct FirstFinanceID from F_Branch ");
            strSql.Append(" where DepartmentId=@DepartmentId ");
            SqlParameter[] parameters = {
					new SqlParameter("@DepartmentId", SqlDbType.Int,4)};
            parameters[0].Value = departmentId;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ret += ds.Tables[0].Rows[i][0].ToString().Trim() + ",";
            }
            return ret.TrimEnd(',');
        }

        public string GetLevel2Users(int departmentId)
        {
            if (departmentId == 0)
                return null;
            string ret = string.Empty;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select  distinct PaymentAccounter from F_Branch ");
            strSql.Append(" where DepartmentId=@DepartmentId ");
            SqlParameter[] parameters = {
					new SqlParameter("@DepartmentId", SqlDbType.Int,4)};
            parameters[0].Value = departmentId;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ret += ds.Tables[0].Rows[i][0].ToString().Trim() + ",";
            }
            return ret.TrimEnd(',');
        }

        public string GetLevel1Users()
        {
            string ret = string.Empty;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select  distinct FirstFinanceID from F_Branch ");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ret += ds.Tables[0].Rows[i][0].ToString().Trim() + ",";
            }
            return ret.TrimEnd(',');
        }

        public string GetDimissionAuditors(int departmentId)
        {
            string ret = string.Empty;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select  distinct DimissionAuditor from F_Branch where branchcode<>'' and departmentId= "+departmentId.ToString());
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i][0].ToString() != "" && ds.Tables[0].Rows[i][0].ToString() != "0")
                {
                    ret += ds.Tables[0].Rows[i][0].ToString().Trim() + ",";
                }
            }
            return ret.TrimEnd(',');
        }

        public string GetLevel2Users()
        {
            string ret = string.Empty;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select  distinct PaymentAccounter from F_Branch where branchcode<>''");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ret += ds.Tables[0].Rows[i][0].ToString().Trim() + ",";
            }
            return ret.TrimEnd(',');
        }

        public string GetSalaryUsers(int departmentId)
        {
            if (departmentId == 0)
                return null;
            string ret = string.Empty;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select  distinct SalaryAccounter from F_Branch ");
            strSql.Append(" where DepartmentId=@DepartmentId ");
            SqlParameter[] parameters = {
					new SqlParameter("@DepartmentId", SqlDbType.Int,4)};
            parameters[0].Value = departmentId;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ret += ds.Tables[0].Rows[i][0].ToString().Trim() + ",";
            }
            return ret.TrimEnd(',');
        }

        public string GetCardUsers(int departmentId)
        {
            if (departmentId == 0)
                return null;
            string ret = string.Empty;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select  distinct BusinessCardAccounter from F_Branch ");
            strSql.Append(" where DepartmentId=@DepartmentId ");
            SqlParameter[] parameters = {
					new SqlParameter("@DepartmentId", SqlDbType.Int,4)};
            parameters[0].Value = departmentId;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ret += ds.Tables[0].Rows[i][0].ToString().Trim() + ",";
            }
            return ret.TrimEnd(',');
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<BranchInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * FROM F_Branch ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where branchcode<>'' and " + term);
            }
            else
            {
                strSql.Append(" where branchcode<>'' ");
            }
            return CBO.FillCollection<BranchInfo>(DbHelperSQL.Query(strSql.ToString(), param));
        }
        #endregion  成员方法
    }
}