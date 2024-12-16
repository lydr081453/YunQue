using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Collections.Generic;
namespace ESP.Finance.DataAccess
{
    /// <summary>
    /// 数据访问类F_Area。
    /// </summary>
    internal class CostReportSavingDataProvider : ESP.Finance.IDataAccess.ICostReportSavingProvider
    {
        public int Add(ESP.Finance.Entity.CostReportSavingInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_CostReportSaving(");
            strSql.Append("Creator,CreatTime,ProjectCode,ProjectContent,ApplicantName,GroupName,TotalAmount,TotalCost,CostUsed,CostBalance,CostPercent,ServiceFee,TotalPaid,PaymentCash,PaymentBill,BeginDate,EndDate)");
            strSql.Append(" values (");
            strSql.Append("@Creator,@CreatTime,@ProjectCode,@ProjectContent,@ApplicantName,@GroupName,@TotalAmount,@TotalCost,@CostUsed,@CostBalance,@CostPercent,@ServiceFee,@TotalPaid,@PaymentCash,@PaymentBill,@BeginDate,@EndDate)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@Creator", SqlDbType.Int),
					new SqlParameter("@CreatTime", SqlDbType.DateTime),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectContent", SqlDbType.NVarChar,500),
					new SqlParameter("@ApplicantName", SqlDbType.NVarChar,50),
                    new SqlParameter("@GroupName", SqlDbType.NVarChar,50),                                        
 new SqlParameter("@TotalAmount", SqlDbType.Decimal,20),
  new SqlParameter("@TotalCost", SqlDbType.Decimal,20),
   new SqlParameter("@CostUsed", SqlDbType.Decimal,20),
    new SqlParameter("@CostBalance", SqlDbType.Decimal,20),
     new SqlParameter("@CostPercent", SqlDbType.Decimal,20),
      new SqlParameter("@ServiceFee", SqlDbType.Decimal,20),
       new SqlParameter("@TotalPaid", SqlDbType.Decimal,20),
        new SqlParameter("@PaymentCash", SqlDbType.Decimal,20),
         new SqlParameter("@PaymentBill", SqlDbType.Decimal,20),
          new SqlParameter("@BeginDate", SqlDbType.DateTime),
           new SqlParameter("@EndDate", SqlDbType.DateTime)
                                        };
            parameters[0].Value = model.Creator;
            parameters[1].Value = model.CreatTime;
            parameters[2].Value = model.ProjectCode;
            parameters[3].Value = model.ProjectContent;
            parameters[4].Value = model.ApplicantName;

            parameters[5].Value = model.GroupName;
            parameters[6].Value = model.TotalAmount;
            parameters[7].Value = model.TotalCost;
            parameters[8].Value = model.CostUsed;
            parameters[9].Value = model.CostBalance;
            parameters[10].Value = model.CostPercent;
            parameters[11].Value = model.ServiceFee;
            parameters[12].Value = model.TotalPaid;
            parameters[13].Value = model.PaymentCash;
            parameters[14].Value = model.PaymentBill;
            parameters[15].Value = model.BeginDate;
            parameters[16].Value = model.EndDate;


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
    }
}
