using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Collections.Generic;
namespace ESP.Finance.DataAccess
{
    internal class SalaryProvider : ESP.Finance.IDataAccess.ISalaryDataProvider
    {

        public int Add(ESP.Finance.Entity.SalaryInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_Salary(");
            strSql.Append("UserId,UserCode,NameCN,NameEN,SalaryYear,SalaryMonth,IDNumber,EmailPassword,EmailSendTime,LastYearAdded,LastYearAward,LastYearOthers,SalaryTotal,SalaryPaid,Remark,SalaryBased,SalaryPerformance,SalaryDonation,LateCut,AbsenceCut,NoteBook,PhoneAllowance,MealAllowance,Income,Retirement,Medical,Housing,UnEmp,InsuranceTotal,SalaryPretax,ChildEDU,ContinuingEDU,HomeLoan,HomeRent,OlderSupport,SpecialTotal,Tax1,Tax2,Tax3,Tax4,TaxedCut,BranchCode,Importer,ImportTime,AffairCut,SickCut,ClockCut,KaoqinTotal,OtherCut,OtherIncome)");
            strSql.Append(" values (@UserId,@UserCode,@NameCN,@NameEN,@SalaryYear,@SalaryMonth,@IDNumber,@EmailPassword,@EmailSendTime,@LastYearAdded,@LastYearAward,@LastYearOthers,@SalaryTotal,@SalaryPaid,@Remark,@SalaryBased,@SalaryPerformance,@SalaryDonation,@LateCut,@AbsenceCut,@NoteBook,@PhoneAllowance,@MealAllowance,@Income,@Retirement,@Medical,@Housing,@UnEmp,@InsuranceTotal,@SalaryPretax,@ChildEDU,@ContinuingEDU,@HomeLoan,@HomeRent,@OlderSupport,@SpecialTotal,@Tax1,@Tax2,@Tax3,@Tax4,@TaxedCut,@BranchCode,@Importer,@ImportTime,@AffairCut,@SickCut,@ClockCut,@KaoqinTotal,@OtherCut,@OtherIncome)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int),
					new SqlParameter("@UserCode", SqlDbType.NVarChar,50),
					new SqlParameter("@NameCN", SqlDbType.NVarChar,50),
					new SqlParameter("@NameEN", SqlDbType.NVarChar,50),
                    new SqlParameter("@SalaryYear", SqlDbType.Int),
                    new SqlParameter("@SalaryMonth", SqlDbType.Int),
                    new SqlParameter("@IDNumber", SqlDbType.NVarChar,50),
                    new SqlParameter("@EmailPassword", SqlDbType.NVarChar,50),
                    new SqlParameter("@EmailSendTime", SqlDbType.DateTime),
                    new SqlParameter("@LastYearAdded", SqlDbType.NVarChar,500),
                    new SqlParameter("@LastYearAward", SqlDbType.NVarChar,500),
                    new SqlParameter("@LastYearOthers", SqlDbType.NVarChar,500),
                    new SqlParameter("@SalaryTotal", SqlDbType.NVarChar,500),
                    new SqlParameter("@SalaryPaid", SqlDbType.NVarChar,500),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,500),
                    new SqlParameter("@SalaryBased", SqlDbType.NVarChar,500),
                    new SqlParameter("@SalaryPerformance", SqlDbType.NVarChar,500),
                    new SqlParameter("@SalaryDonation", SqlDbType.NVarChar,500),
                    new SqlParameter("@LateCut", SqlDbType.NVarChar,500),
                    new SqlParameter("@AbsenceCut", SqlDbType.NVarChar,500),
                    new SqlParameter("@NoteBook", SqlDbType.NVarChar,500),
                    new SqlParameter("@PhoneAllowance", SqlDbType.NVarChar,500),
                    new SqlParameter("@MealAllowance", SqlDbType.NVarChar,500),
                    new SqlParameter("@Income", SqlDbType.NVarChar,500),
                    new SqlParameter("@Retirement", SqlDbType.NVarChar,500),
                    new SqlParameter("@Medical", SqlDbType.NVarChar,500),
                    new SqlParameter("@Housing", SqlDbType.NVarChar,500),
                    new SqlParameter("@UnEmp", SqlDbType.NVarChar,500),
                    new SqlParameter("@InsuranceTotal", SqlDbType.NVarChar,500),
                    new SqlParameter("@SalaryPretax", SqlDbType.NVarChar,500),
                    new SqlParameter("@ChildEDU", SqlDbType.NVarChar,500),
                    new SqlParameter("@ContinuingEDU", SqlDbType.NVarChar,500),
                     new SqlParameter("@HomeLoan", SqlDbType.NVarChar,500),
                     new SqlParameter("@HomeRent", SqlDbType.NVarChar,500),
                     new SqlParameter("@OlderSupport", SqlDbType.NVarChar,500),
                     new SqlParameter("@SpecialTotal", SqlDbType.NVarChar,500),
                     new SqlParameter("@Tax1", SqlDbType.NVarChar,500),
                     new SqlParameter("@Tax2", SqlDbType.NVarChar,500),
                     new SqlParameter("@Tax3", SqlDbType.NVarChar,500),
                     new SqlParameter("@Tax4", SqlDbType.NVarChar,500),
                     new SqlParameter("@TaxedCut", SqlDbType.NVarChar,500),
                     new SqlParameter("@BranchCode", SqlDbType.NVarChar,50),
                     new SqlParameter("@Importer", SqlDbType.Int),
                     new SqlParameter("@ImportTime", SqlDbType.DateTime),
                     new SqlParameter("@AffairCut", SqlDbType.NVarChar,50),
                     new SqlParameter("@SickCut", SqlDbType.NVarChar,50),
                     new SqlParameter("@ClockCut", SqlDbType.NVarChar,50),
                     new SqlParameter("@KaoqinTotal", SqlDbType.NVarChar,50),
                     new SqlParameter("@OtherCut", SqlDbType.NVarChar,50),
                     new SqlParameter("@OtherIncome", SqlDbType.NVarChar,50)


                                        };
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.UserCode;
            parameters[2].Value = model.NameCN;
            parameters[3].Value = model.NameEN;
            parameters[4].Value = model.SalaryYear;
            parameters[5].Value = model.SalaryMonth;
            parameters[6].Value = model.IDNumber;
            parameters[7].Value = model.EmailPassword;
            parameters[8].Value = model.EmailSendTime;

            parameters[9].Value = model.LastYearAdded;
            parameters[10].Value = model.LastYearAward;
            parameters[11].Value = model.LastYearOthers;
            parameters[12].Value = model.SalaryTotal;
            parameters[13].Value = model.SalaryPaid;
            parameters[14].Value = model.Remark;

            parameters[15].Value = model.SalaryBased;
            parameters[16].Value = model.SalaryPerformance;
            parameters[17].Value = model.SalaryDonation;
            parameters[18].Value = model.LateCut;
            parameters[19].Value = model.AbsenceCut;
            parameters[20].Value = model.NoteBook;
            parameters[21].Value = model.PhoneAllowance;
            parameters[22].Value = model.MealAllowance;
            parameters[23].Value = model.Income;
            parameters[24].Value = model.Retirement;
            parameters[25].Value = model.Medical;
            parameters[26].Value = model.Housing;
            parameters[27].Value = model.UnEmp;
            parameters[28].Value = model.InsuranceTotal;
            parameters[29].Value = model.SalaryPretax;
            parameters[30].Value = model.ChildEDU;
            parameters[31].Value = model.ContinuingEDU;

            parameters[32].Value = model.HomeLoan;
            parameters[33].Value = model.HomeRent;
            parameters[34].Value = model.OlderSupport;
            parameters[35].Value = model.SpecialTotal;
            parameters[36].Value = model.Tax1;
            parameters[37].Value = model.Tax2;
            parameters[38].Value = model.Tax3;
            parameters[39].Value = model.Tax4;
            parameters[40].Value = model.TaxedCut;
            parameters[41].Value = model.BranchCode;
            parameters[42].Value = model.Importer;
            parameters[43].Value = model.ImportTime;

            parameters[44].Value = model.AffairCut;
            parameters[45].Value = model.SickCut;
            parameters[46].Value = model.ClockCut;
            parameters[47].Value = model.KaoqinTotal;
            parameters[48].Value = model.OtherCut;
            parameters[49].Value = model.OtherIncome;

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

        public int Delete(int year, int month, int importer)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_Salary ");
            strSql.Append(" where  SalaryYear=@SalaryYear and SalaryMonth=@SalaryMonth and Importer=@Importer");
            SqlParameter[] parameters = {
                    new SqlParameter("@SalaryYear", SqlDbType.Int),
                    new SqlParameter("@SalaryMonth", SqlDbType.Int),
                    new SqlParameter("@Importer", SqlDbType.Int)
                                        };
            parameters[0].Value = year;
            parameters[1].Value = month;
            parameters[2].Value = importer;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        public int UpdatePassword(int userid, int year, int month, string pwd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Salary set ");
            strSql.Append(" EmailPassword=@EmailPassword,EmailSendTime=@EmailSendTime ");
            if (month == 0)
            {
                strSql.Append(" where UserId =@UserId and ((SalaryYear=@SalaryYear and SalaryMonth in(1,2,3,4,5,6,7,8,9,10,11,12)) or (SalaryYear=@SalaryYear-1 and SalaryMonth=13)) and SalaryMonth<>@SalaryMonth");
            }
            else
                strSql.Append(" where UserId =@UserId and SalaryYear=@SalaryYear and SalaryMonth=@SalaryMonth");

            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int),
					new SqlParameter("@SalaryYear", SqlDbType.Int),
                    new SqlParameter("@SalaryMonth", SqlDbType.Int),
                    new SqlParameter("@EmailPassword", SqlDbType.NVarChar,50),
                     new SqlParameter("@EmailSendTime", SqlDbType.DateTime)
                                        };
            parameters[0].Value = userid;
            parameters[1].Value = year;
            parameters[2].Value = month;
            parameters[3].Value = pwd;
            parameters[4].Value = DateTime.Now;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);

        }

        public ESP.Finance.Entity.SalaryInfo GetModel(int userid, int year, int month)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 * from F_Salary ");
            strSql.Append(" where userid=@userid and SalaryYear=@SalaryYear and SalaryMonth=@SalaryMonth order by id desc");

            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.Int),
                    new SqlParameter("@SalaryYear", SqlDbType.Int),
                     new SqlParameter("@SalaryMonth", SqlDbType.Int)
                                        };
            parameters[0].Value = userid;
            parameters[1].Value = year;
            parameters[2].Value = month;
            return CBO.FillObject<SalaryInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }
        public IList<ESP.Finance.Entity.SalaryInfo> GetList(string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM F_Salary ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            strSql.Append(" order by salaryYear desc,salaryMonth desc");
            return CBO.FillCollection<SalaryInfo>(DbHelperSQL.Query(strSql.ToString(), (param == null ? null : param.ToArray())));

        }

    }
}
