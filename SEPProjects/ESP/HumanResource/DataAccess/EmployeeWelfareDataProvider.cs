using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using ESP.HumanResource.Entity;
namespace ESP.HumanResource.DataAccess
{    
    public class EmployeeWelfareDataProvider
    {
        public EmployeeWelfareDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(EmployeeWelfareInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sep_EmployeeWelfareInfo(");
            strSql.Append("sysid,contractType,contractTerm,contractCompany,contractStartDate,contractEndDate,probationPeriod,probationPeriodDeadLine,probationEndDate,endowmentInsurance,unemploymentInsurance,birthInsurance,compoInsurance,medicalInsurance,socialInsuranceCompany,socialInsuranceAddress,socialInsuranceCode,medicalInsuranceCode,socialInsuranceBase,medicalInsuranceBase,publicReserveFundsCompany,publicReserveFundsAddress,publicReserveFundsBase,publicReserveFundsCode,isArchive,ArchiveCode,ArchiveDate,memo,publicReserveFunds,contractRenewalCount,contractSignInfo,ArchivePlace,InsurancePlace,endowmentInsuranceStarTime,endowmentInsuranceEndTime,unemploymentInsuranceStarTime,unemploymentInsuranceEndTime,birthInsuranceStarTime,birthInsuranceEndTime,compoInsuranceStarTime,compoInsuranceEndTime,medicalInsuranceStarTime,medicalInsuranceEndTime,publicReserveFundsStarTime,publicReserveFundsEndTime,EIProportionOfFirms,EIProportionOfIndividuals,UIProportionOfFirms,UIProportionOfIndividuals,BIProportionOfFirms,BIProportionOfIndividuals,CIProportionOfFirms,CIProportionOfIndividuals,MIProportionOfFirms,MIProportionOfIndividuals,MIBigProportionOfIndividuals,PRFProportionOfFirms,PRFProportionOfIndividuals,EIFirmsCosts,EIIndividualsCosts,UIFirmsCosts,UIIndividualsCosts,BIFirmsCosts,BIIndividualsCosts,CIFirmsCosts,CIIndividualsCosts,MIFirmsCosts,MIIndividualsCosts,PRFirmsCosts,PRIIndividualsCosts,ComplementaryMedicalCosts,ComplementaryMedical,AccidentInsurance,AccidentInsuranceBeginTime,AccidentInsuranceEndTime)");
            strSql.Append(" values (");
            strSql.Append("@sysid,@contractType,@contractTerm,@contractCompany,@contractStartDate,@contractEndDate,@probationPeriod,@probationPeriodDeadLine,@probationEndDate,@endowmentInsurance,@unemploymentInsurance,@birthInsurance,@compoInsurance,@medicalInsurance,@socialInsuranceCompany,@socialInsuranceAddress,@socialInsuranceCode,@medicalInsuranceCode,@socialInsuranceBase,@medicalInsuranceBase,@publicReserveFundsCompany,@publicReserveFundsAddress,@publicReserveFundsBase,@publicReserveFundsCode,@isArchive,@ArchiveCode,@ArchiveDate,@memo,@publicReserveFunds,@contractRenewalCount,@contractSignInfo,@ArchivePlace,@InsurancePlace,@endowmentInsuranceStarTime,@endowmentInsuranceEndTime,@unemploymentInsuranceStarTime,@unemploymentInsuranceEndTime,@birthInsuranceStarTime,@birthInsuranceEndTime,@compoInsuranceStarTime,@compoInsuranceEndTime,@medicalInsuranceStarTime,@medicalInsuranceEndTime,@publicReserveFundsStarTime,@publicReserveFundsEndTime,@EIProportionOfFirms,@EIProportionOfIndividuals,@UIProportionOfFirms,@UIProportionOfIndividuals,@BIProportionOfFirms,@BIProportionOfIndividuals,@CIProportionOfFirms,@CIProportionOfIndividuals,@MIProportionOfFirms,@MIProportionOfIndividuals,@MIBigProportionOfIndividuals,@PRFProportionOfFirms,@PRFProportionOfIndividuals,@EIFirmsCosts,@EIIndividualsCosts,@UIFirmsCosts,@UIIndividualsCosts,@BIFirmsCosts,@BIIndividualsCosts,@CIFirmsCosts,@CIIndividualsCosts,@MIFirmsCosts,@MIIndividualsCosts,@PRFirmsCosts,@PRIIndividualsCosts,@ComplementaryMedicalCosts,@ComplementaryMedical,@AccidentInsurance,@AccidentInsuranceBeginTime,@AccidentInsuranceEndTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@sysid", SqlDbType.Int,4),
					new SqlParameter("@contractType", SqlDbType.NVarChar,50),
					new SqlParameter("@contractTerm", SqlDbType.NVarChar,50),
					new SqlParameter("@contractCompany", SqlDbType.NVarChar,50),
					new SqlParameter("@contractStartDate", SqlDbType.SmallDateTime),
					new SqlParameter("@contractEndDate", SqlDbType.SmallDateTime),
					new SqlParameter("@probationPeriod", SqlDbType.NVarChar,50),
					new SqlParameter("@probationPeriodDeadLine", SqlDbType.SmallDateTime),
					new SqlParameter("@probationEndDate", SqlDbType.SmallDateTime),
					new SqlParameter("@endowmentInsurance", SqlDbType.Bit,1),
					new SqlParameter("@unemploymentInsurance", SqlDbType.Bit,1),
					new SqlParameter("@birthInsurance", SqlDbType.Bit,1),
					new SqlParameter("@compoInsurance", SqlDbType.Bit,1),
					new SqlParameter("@medicalInsurance", SqlDbType.Bit,1),
					new SqlParameter("@socialInsuranceCompany", SqlDbType.NVarChar,50),
					new SqlParameter("@socialInsuranceAddress", SqlDbType.NVarChar,50),
					new SqlParameter("@socialInsuranceCode", SqlDbType.NVarChar,50),
					new SqlParameter("@medicalInsuranceCode", SqlDbType.NVarChar,50),
					new SqlParameter("@socialInsuranceBase", SqlDbType.NVarChar,200),
					new SqlParameter("@medicalInsuranceBase", SqlDbType.NVarChar,200),
					new SqlParameter("@publicReserveFundsCompany", SqlDbType.NVarChar,50),
					new SqlParameter("@publicReserveFundsAddress", SqlDbType.NVarChar,50),
					new SqlParameter("@publicReserveFundsBase", SqlDbType.NVarChar,200),
					new SqlParameter("@publicReserveFundsCode", SqlDbType.NVarChar,50),
					new SqlParameter("@isArchive", SqlDbType.Bit,1),
					new SqlParameter("@ArchiveCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ArchiveDate", SqlDbType.NVarChar,50),
					new SqlParameter("@memo", SqlDbType.NVarChar,2000),
					new SqlParameter("@publicReserveFunds", SqlDbType.Bit,1),
					new SqlParameter("@contractRenewalCount", SqlDbType.Int,4),
					new SqlParameter("@contractSignInfo", SqlDbType.NVarChar,50),
					new SqlParameter("@ArchivePlace", SqlDbType.NVarChar,50),
					new SqlParameter("@InsurancePlace", SqlDbType.NVarChar,50),
					new SqlParameter("@endowmentInsuranceStarTime", SqlDbType.SmallDateTime),
					new SqlParameter("@endowmentInsuranceEndTime", SqlDbType.SmallDateTime),
					new SqlParameter("@unemploymentInsuranceStarTime", SqlDbType.SmallDateTime),
					new SqlParameter("@unemploymentInsuranceEndTime", SqlDbType.SmallDateTime),
					new SqlParameter("@birthInsuranceStarTime", SqlDbType.SmallDateTime),
					new SqlParameter("@birthInsuranceEndTime", SqlDbType.SmallDateTime),
					new SqlParameter("@compoInsuranceStarTime", SqlDbType.SmallDateTime),
					new SqlParameter("@compoInsuranceEndTime", SqlDbType.SmallDateTime),
					new SqlParameter("@medicalInsuranceStarTime", SqlDbType.SmallDateTime),
					new SqlParameter("@medicalInsuranceEndTime", SqlDbType.SmallDateTime),
					new SqlParameter("@publicReserveFundsStarTime", SqlDbType.SmallDateTime),
					new SqlParameter("@publicReserveFundsEndTime", SqlDbType.SmallDateTime),
					new SqlParameter("@EIProportionOfFirms", SqlDbType.Decimal,5),
					new SqlParameter("@EIProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@UIProportionOfFirms", SqlDbType.Decimal,5),
					new SqlParameter("@UIProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@BIProportionOfFirms", SqlDbType.Decimal,5),
					new SqlParameter("@BIProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@CIProportionOfFirms", SqlDbType.Decimal,5),
					new SqlParameter("@CIProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@MIProportionOfFirms", SqlDbType.Decimal,5),
					new SqlParameter("@MIProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@MIBigProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@PRFProportionOfFirms", SqlDbType.Decimal,5),
					new SqlParameter("@PRFProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@EIFirmsCosts", SqlDbType.NVarChar,200),
					new SqlParameter("@EIIndividualsCosts", SqlDbType.NVarChar,200),
					new SqlParameter("@UIFirmsCosts", SqlDbType.NVarChar,200),
					new SqlParameter("@UIIndividualsCosts", SqlDbType.NVarChar,200),
					new SqlParameter("@BIFirmsCosts", SqlDbType.NVarChar,200),
					new SqlParameter("@BIIndividualsCosts", SqlDbType.NVarChar,200),
					new SqlParameter("@CIFirmsCosts", SqlDbType.NVarChar,200),
					new SqlParameter("@CIIndividualsCosts", SqlDbType.NVarChar,200),
					new SqlParameter("@MIFirmsCosts", SqlDbType.NVarChar,200),
					new SqlParameter("@MIIndividualsCosts", SqlDbType.NVarChar,200),
					new SqlParameter("@PRFirmsCosts", SqlDbType.NVarChar,200),
					new SqlParameter("@PRIIndividualsCosts", SqlDbType.NVarChar,200),
                    new SqlParameter("@ComplementaryMedicalCosts",SqlDbType.Decimal,5),
                    new SqlParameter("@ComplementaryMedical",SqlDbType.Bit,1),
                    new SqlParameter("@AccidentInsurance",SqlDbType.Bit,1),
                    new SqlParameter("@AccidentInsuranceBeginTime",SqlDbType.SmallDateTime),
                    new SqlParameter("@AccidentInsuranceEndTime",SqlDbType.SmallDateTime)
                    };
            parameters[0].Value = model.sysid;
            parameters[1].Value = model.contractType;
            parameters[2].Value = model.contractTerm;
            parameters[3].Value = model.contractCompany;
            parameters[4].Value = model.contractStartDate;
            parameters[5].Value = model.contractEndDate;
            parameters[6].Value = model.probationPeriod;
            parameters[7].Value = model.probationPeriodDeadLine;
            parameters[8].Value = model.probationEndDate;
            parameters[9].Value = model.endowmentInsurance;
            parameters[10].Value = model.unemploymentInsurance;
            parameters[11].Value = model.birthInsurance;
            parameters[12].Value = model.compoInsurance;
            parameters[13].Value = model.medicalInsurance;
            parameters[14].Value = model.socialInsuranceCompany;
            parameters[15].Value = model.socialInsuranceAddress;
            parameters[16].Value = model.socialInsuranceCode;
            parameters[17].Value = model.medicalInsuranceCode;
            parameters[18].Value = model.socialInsuranceBase;
            parameters[19].Value = model.medicalInsuranceBase;
            parameters[20].Value = model.publicReserveFundsCompany;
            parameters[21].Value = model.publicReserveFundsAddress;
            parameters[22].Value = model.publicReserveFundsBase;
            parameters[23].Value = model.publicReserveFundsCode;
            parameters[24].Value = model.isArchive;
            parameters[25].Value = model.ArchiveCode;
            parameters[26].Value = model.ArchiveDate;
            parameters[27].Value = model.memo;
            parameters[28].Value = model.publicReserveFunds;
            parameters[29].Value = model.contractRenewalCount;
            parameters[30].Value = model.contractSignInfo;
            parameters[31].Value = model.ArchivePlace;
            parameters[32].Value = model.InsurancePlace;
            parameters[33].Value = model.endowmentInsuranceStarTime;
            parameters[34].Value = model.endowmentInsuranceEndTime;
            parameters[35].Value = model.unemploymentInsuranceStarTime;
            parameters[36].Value = model.unemploymentInsuranceEndTime;
            parameters[37].Value = model.birthInsuranceStarTime;
            parameters[38].Value = model.birthInsuranceEndTime;
            parameters[39].Value = model.compoInsuranceStarTime;
            parameters[40].Value = model.compoInsuranceEndTime;
            parameters[41].Value = model.medicalInsuranceStarTime;
            parameters[42].Value = model.medicalInsuranceEndTime;
            parameters[43].Value = model.publicReserveFundsStarTime;
            parameters[44].Value = model.publicReserveFundsEndTime;
            parameters[45].Value = model.EIProportionOfFirms;
            parameters[46].Value = model.EIProportionOfIndividuals;
            parameters[47].Value = model.UIProportionOfFirms;
            parameters[48].Value = model.UIProportionOfIndividuals;
            parameters[49].Value = model.BIProportionOfFirms;
            parameters[50].Value = model.BIProportionOfIndividuals;
            parameters[51].Value = model.CIProportionOfFirms;
            parameters[52].Value = model.CIProportionOfIndividuals;
            parameters[53].Value = model.MIProportionOfFirms;
            parameters[54].Value = model.MIProportionOfIndividuals;
            parameters[55].Value = model.MIBigProportionOfIndividuals;
            parameters[56].Value = model.PRFProportionOfFirms;
            parameters[57].Value = model.PRFProportionOfIndividuals;
            parameters[58].Value = model.EIFirmsCosts;
            parameters[59].Value = model.EIIndividualsCosts;
            parameters[60].Value = model.UIFirmsCosts;
            parameters[61].Value = model.UIIndividualsCosts;
            parameters[62].Value = model.BIFirmsCosts;
            parameters[63].Value = model.BIIndividualsCosts;
            parameters[64].Value = model.CIFirmsCosts;
            parameters[65].Value = model.CIIndividualsCosts;
            parameters[66].Value = model.MIFirmsCosts;
            parameters[67].Value = model.MIIndividualsCosts;
            parameters[68].Value = model.PRFirmsCosts;
            parameters[69].Value = model.PRIIndividualsCosts;
            parameters[70].Value = model.ComplementaryMedicalCosts;
            parameters[71].Value = model.ComplementaryMedical;
            parameters[72].Value = model.AccidentInsurance;
            parameters[73].Value = model.AccidentInsuranceBeginTime;
            parameters[74].Value = model.AccidentInsuranceEndTime;

            object obj;
            if (trans == null)
                obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            else
            {
                obj = DbHelperSQL.GetSingle(strSql.ToString(), trans.Connection, trans, parameters);
            }
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
        public void Update(EmployeeWelfareInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sep_EmployeeWelfareInfo set ");
            strSql.Append("sysid=@sysid,");
            strSql.Append("contractType=@contractType,");
            strSql.Append("contractTerm=@contractTerm,");
            strSql.Append("contractCompany=@contractCompany,");
            strSql.Append("contractStartDate=@contractStartDate,");
            strSql.Append("contractEndDate=@contractEndDate,");
            strSql.Append("probationPeriod=@probationPeriod,");
            strSql.Append("probationPeriodDeadLine=@probationPeriodDeadLine,");
            strSql.Append("probationEndDate=@probationEndDate,");
            strSql.Append("endowmentInsurance=@endowmentInsurance,");
            strSql.Append("unemploymentInsurance=@unemploymentInsurance,");
            strSql.Append("birthInsurance=@birthInsurance,");
            strSql.Append("compoInsurance=@compoInsurance,");
            strSql.Append("medicalInsurance=@medicalInsurance,");
            strSql.Append("socialInsuranceCompany=@socialInsuranceCompany,");
            strSql.Append("socialInsuranceAddress=@socialInsuranceAddress,");
            strSql.Append("socialInsuranceCode=@socialInsuranceCode,");
            strSql.Append("medicalInsuranceCode=@medicalInsuranceCode,");
            strSql.Append("socialInsuranceBase=@socialInsuranceBase,");
            strSql.Append("medicalInsuranceBase=@medicalInsuranceBase,");
            strSql.Append("publicReserveFundsCompany=@publicReserveFundsCompany,");
            strSql.Append("publicReserveFundsAddress=@publicReserveFundsAddress,");
            strSql.Append("publicReserveFundsBase=@publicReserveFundsBase,");
            strSql.Append("publicReserveFundsCode=@publicReserveFundsCode,");
            strSql.Append("isArchive=@isArchive,");
            strSql.Append("ArchiveCode=@ArchiveCode,");
            strSql.Append("ArchiveDate=@ArchiveDate,");
            strSql.Append("memo=@memo,");
            strSql.Append("publicReserveFunds=@publicReserveFunds,");
            strSql.Append("contractRenewalCount=@contractRenewalCount,");
            strSql.Append("contractSignInfo=@contractSignInfo,");
            strSql.Append("ArchivePlace=@ArchivePlace,");
            strSql.Append("InsurancePlace=@InsurancePlace,");
            strSql.Append("endowmentInsuranceStarTime=@endowmentInsuranceStarTime,");
            strSql.Append("endowmentInsuranceEndTime=@endowmentInsuranceEndTime,");
            strSql.Append("unemploymentInsuranceStarTime=@unemploymentInsuranceStarTime,");
            strSql.Append("unemploymentInsuranceEndTime=@unemploymentInsuranceEndTime,");
            strSql.Append("birthInsuranceStarTime=@birthInsuranceStarTime,");
            strSql.Append("birthInsuranceEndTime=@birthInsuranceEndTime,");
            strSql.Append("compoInsuranceStarTime=@compoInsuranceStarTime,");
            strSql.Append("compoInsuranceEndTime=@compoInsuranceEndTime,");
            strSql.Append("medicalInsuranceStarTime=@medicalInsuranceStarTime,");
            strSql.Append("medicalInsuranceEndTime=@medicalInsuranceEndTime,");
            strSql.Append("publicReserveFundsStarTime=@publicReserveFundsStarTime,");
            strSql.Append("publicReserveFundsEndTime=@publicReserveFundsEndTime,");
            strSql.Append("EIProportionOfFirms=@EIProportionOfFirms,");
            strSql.Append("EIProportionOfIndividuals=@EIProportionOfIndividuals,");
            strSql.Append("UIProportionOfFirms=@UIProportionOfFirms,");
            strSql.Append("UIProportionOfIndividuals=@UIProportionOfIndividuals,");
            strSql.Append("BIProportionOfFirms=@BIProportionOfFirms,");
            strSql.Append("BIProportionOfIndividuals=@BIProportionOfIndividuals,");
            strSql.Append("CIProportionOfFirms=@CIProportionOfFirms,");
            strSql.Append("CIProportionOfIndividuals=@CIProportionOfIndividuals,");
            strSql.Append("MIProportionOfFirms=@MIProportionOfFirms,");
            strSql.Append("MIProportionOfIndividuals=@MIProportionOfIndividuals,");
            strSql.Append("MIBigProportionOfIndividuals=@MIBigProportionOfIndividuals,");
            strSql.Append("PRFProportionOfFirms=@PRFProportionOfFirms,");
            strSql.Append("PRFProportionOfIndividuals=@PRFProportionOfIndividuals,");
            strSql.Append("EIFirmsCosts=@EIFirmsCosts,");
            strSql.Append("EIIndividualsCosts=@EIIndividualsCosts,");
            strSql.Append("UIFirmsCosts=@UIFirmsCosts,");
            strSql.Append("UIIndividualsCosts=@UIIndividualsCosts,");
            strSql.Append("BIFirmsCosts=@BIFirmsCosts,");
            strSql.Append("BIIndividualsCosts=@BIIndividualsCosts,");
            strSql.Append("CIFirmsCosts=@CIFirmsCosts,");
            strSql.Append("CIIndividualsCosts=@CIIndividualsCosts,");
            strSql.Append("MIFirmsCosts=@MIFirmsCosts,");
            strSql.Append("MIIndividualsCosts=@MIIndividualsCosts,");
            strSql.Append("PRFirmsCosts=@PRFirmsCosts,");
            strSql.Append("PRIIndividualsCosts=@PRIIndividualsCosts,");
            strSql.Append("ComplementaryMedicalCosts=@ComplementaryMedicalCosts,");
            strSql.Append("ComplementaryMedical=@ComplementaryMedical,");
            strSql.Append("AccidentInsurance=@AccidentInsurance,");
            strSql.Append("AccidentInsuranceBeginTime=@AccidentInsuranceBeginTime,");
            strSql.Append("AccidentInsuranceEndTime=@AccidentInsuranceEndTime");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@sysid", SqlDbType.Int,4),
					new SqlParameter("@contractType", SqlDbType.NVarChar,50),
					new SqlParameter("@contractTerm", SqlDbType.NVarChar,50),
					new SqlParameter("@contractCompany", SqlDbType.NVarChar,50),
					new SqlParameter("@contractStartDate", SqlDbType.SmallDateTime),
					new SqlParameter("@contractEndDate", SqlDbType.SmallDateTime),
					new SqlParameter("@probationPeriod", SqlDbType.NVarChar,50),
					new SqlParameter("@probationPeriodDeadLine", SqlDbType.SmallDateTime),
					new SqlParameter("@probationEndDate", SqlDbType.SmallDateTime),
					new SqlParameter("@endowmentInsurance", SqlDbType.Bit,1),
					new SqlParameter("@unemploymentInsurance", SqlDbType.Bit,1),
					new SqlParameter("@birthInsurance", SqlDbType.Bit,1),
					new SqlParameter("@compoInsurance", SqlDbType.Bit,1),
					new SqlParameter("@medicalInsurance", SqlDbType.Bit,1),
					new SqlParameter("@socialInsuranceCompany", SqlDbType.NVarChar,50),
					new SqlParameter("@socialInsuranceAddress", SqlDbType.NVarChar,50),
					new SqlParameter("@socialInsuranceCode", SqlDbType.NVarChar,50),
					new SqlParameter("@medicalInsuranceCode", SqlDbType.NVarChar,50),
					new SqlParameter("@socialInsuranceBase", SqlDbType.NVarChar,200),
					new SqlParameter("@medicalInsuranceBase", SqlDbType.NVarChar,200),
					new SqlParameter("@publicReserveFundsCompany", SqlDbType.NVarChar,50),
					new SqlParameter("@publicReserveFundsAddress", SqlDbType.NVarChar,50),
					new SqlParameter("@publicReserveFundsBase", SqlDbType.NVarChar,200),
					new SqlParameter("@publicReserveFundsCode", SqlDbType.NVarChar,50),
					new SqlParameter("@isArchive", SqlDbType.Bit,1),
					new SqlParameter("@ArchiveCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ArchiveDate", SqlDbType.NVarChar,50),
					new SqlParameter("@memo", SqlDbType.NVarChar,2000),
					new SqlParameter("@publicReserveFunds", SqlDbType.Bit,1),
					new SqlParameter("@contractRenewalCount", SqlDbType.Int,4),
					new SqlParameter("@contractSignInfo", SqlDbType.NVarChar,50),
					new SqlParameter("@ArchivePlace", SqlDbType.NVarChar,50),
					new SqlParameter("@InsurancePlace", SqlDbType.NVarChar,50),
					new SqlParameter("@endowmentInsuranceStarTime", SqlDbType.SmallDateTime),
					new SqlParameter("@endowmentInsuranceEndTime", SqlDbType.SmallDateTime),
					new SqlParameter("@unemploymentInsuranceStarTime", SqlDbType.SmallDateTime),
					new SqlParameter("@unemploymentInsuranceEndTime", SqlDbType.SmallDateTime),
					new SqlParameter("@birthInsuranceStarTime", SqlDbType.SmallDateTime),
					new SqlParameter("@birthInsuranceEndTime", SqlDbType.SmallDateTime),
					new SqlParameter("@compoInsuranceStarTime", SqlDbType.SmallDateTime),
					new SqlParameter("@compoInsuranceEndTime", SqlDbType.SmallDateTime),
					new SqlParameter("@medicalInsuranceStarTime", SqlDbType.SmallDateTime),
					new SqlParameter("@medicalInsuranceEndTime", SqlDbType.SmallDateTime),
					new SqlParameter("@publicReserveFundsStarTime", SqlDbType.SmallDateTime),
					new SqlParameter("@publicReserveFundsEndTime", SqlDbType.SmallDateTime),
					new SqlParameter("@EIProportionOfFirms", SqlDbType.Decimal,5),
					new SqlParameter("@EIProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@UIProportionOfFirms", SqlDbType.Decimal,5),
					new SqlParameter("@UIProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@BIProportionOfFirms", SqlDbType.Decimal,5),
					new SqlParameter("@BIProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@CIProportionOfFirms", SqlDbType.Decimal,5),
					new SqlParameter("@CIProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@MIProportionOfFirms", SqlDbType.Decimal,5),
					new SqlParameter("@MIProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@MIBigProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@PRFProportionOfFirms", SqlDbType.Decimal,5),
					new SqlParameter("@PRFProportionOfIndividuals", SqlDbType.Decimal,5),
					new SqlParameter("@EIFirmsCosts", SqlDbType.NVarChar,200),
					new SqlParameter("@EIIndividualsCosts", SqlDbType.NVarChar,200),
					new SqlParameter("@UIFirmsCosts", SqlDbType.NVarChar,200),
					new SqlParameter("@UIIndividualsCosts", SqlDbType.NVarChar,200),
					new SqlParameter("@BIFirmsCosts", SqlDbType.NVarChar,200),
					new SqlParameter("@BIIndividualsCosts", SqlDbType.NVarChar,200),
					new SqlParameter("@CIFirmsCosts", SqlDbType.NVarChar,200),
					new SqlParameter("@CIIndividualsCosts", SqlDbType.NVarChar,200),
					new SqlParameter("@MIFirmsCosts", SqlDbType.NVarChar,200),
					new SqlParameter("@MIIndividualsCosts", SqlDbType.NVarChar,200),
					new SqlParameter("@PRFirmsCosts", SqlDbType.NVarChar,200),
					new SqlParameter("@PRIIndividualsCosts", SqlDbType.NVarChar,200),
                    new SqlParameter("@ComplementaryMedicalCosts",SqlDbType.Decimal,5),
                    new SqlParameter("@ComplementaryMedical",SqlDbType.Bit,1),
                    new SqlParameter("@AccidentInsurance",SqlDbType.Bit,1),
                    new SqlParameter("@AccidentInsuranceBeginTime",SqlDbType.SmallDateTime),
                    new SqlParameter("@AccidentInsuranceEndTime",SqlDbType.SmallDateTime)
                                        };
            parameters[0].Value = model.id;
            parameters[1].Value = model.sysid;
            parameters[2].Value = model.contractType;
            parameters[3].Value = model.contractTerm;
            parameters[4].Value = model.contractCompany;
            parameters[5].Value = model.contractStartDate;
            parameters[6].Value = model.contractEndDate;
            parameters[7].Value = model.probationPeriod;
            parameters[8].Value = model.probationPeriodDeadLine;
            parameters[9].Value = model.probationEndDate;
            parameters[10].Value = model.endowmentInsurance;
            parameters[11].Value = model.unemploymentInsurance;
            parameters[12].Value = model.birthInsurance;
            parameters[13].Value = model.compoInsurance;
            parameters[14].Value = model.medicalInsurance;
            parameters[15].Value = model.socialInsuranceCompany;
            parameters[16].Value = model.socialInsuranceAddress;
            parameters[17].Value = model.socialInsuranceCode;
            parameters[18].Value = model.medicalInsuranceCode;
            parameters[19].Value = model.socialInsuranceBase;
            parameters[20].Value = model.medicalInsuranceBase;
            parameters[21].Value = model.publicReserveFundsCompany;
            parameters[22].Value = model.publicReserveFundsAddress;
            parameters[23].Value = model.publicReserveFundsBase;
            parameters[24].Value = model.publicReserveFundsCode;
            parameters[25].Value = model.isArchive;
            parameters[26].Value = model.ArchiveCode;
            parameters[27].Value = model.ArchiveDate;
            parameters[28].Value = model.memo;
            parameters[29].Value = model.publicReserveFunds;
            parameters[30].Value = model.contractRenewalCount;
            parameters[31].Value = model.contractSignInfo;
            parameters[32].Value = model.ArchivePlace;
            parameters[33].Value = model.InsurancePlace;
            parameters[34].Value = model.endowmentInsuranceStarTime;
            parameters[35].Value = model.endowmentInsuranceEndTime;
            parameters[36].Value = model.unemploymentInsuranceStarTime;
            parameters[37].Value = model.unemploymentInsuranceEndTime;
            parameters[38].Value = model.birthInsuranceStarTime;
            parameters[39].Value = model.birthInsuranceEndTime;
            parameters[40].Value = model.compoInsuranceStarTime;
            parameters[41].Value = model.compoInsuranceEndTime;
            parameters[42].Value = model.medicalInsuranceStarTime;
            parameters[43].Value = model.medicalInsuranceEndTime;
            parameters[44].Value = model.publicReserveFundsStarTime;
            parameters[45].Value = model.publicReserveFundsEndTime;
            parameters[46].Value = model.EIProportionOfFirms;
            parameters[47].Value = model.EIProportionOfIndividuals;
            parameters[48].Value = model.UIProportionOfFirms;
            parameters[49].Value = model.UIProportionOfIndividuals;
            parameters[50].Value = model.BIProportionOfFirms;
            parameters[51].Value = model.BIProportionOfIndividuals;
            parameters[52].Value = model.CIProportionOfFirms;
            parameters[53].Value = model.CIProportionOfIndividuals;
            parameters[54].Value = model.MIProportionOfFirms;
            parameters[55].Value = model.MIProportionOfIndividuals;
            parameters[56].Value = model.MIBigProportionOfIndividuals;
            parameters[57].Value = model.PRFProportionOfFirms;
            parameters[58].Value = model.PRFProportionOfIndividuals;
            parameters[59].Value = model.EIFirmsCosts;
            parameters[60].Value = model.EIIndividualsCosts;
            parameters[61].Value = model.UIFirmsCosts;
            parameters[62].Value = model.UIIndividualsCosts;
            parameters[63].Value = model.BIFirmsCosts;
            parameters[64].Value = model.BIIndividualsCosts;
            parameters[65].Value = model.CIFirmsCosts;
            parameters[66].Value = model.CIIndividualsCosts;
            parameters[67].Value = model.MIFirmsCosts;
            parameters[68].Value = model.MIIndividualsCosts;
            parameters[69].Value = model.PRFirmsCosts;
            parameters[70].Value = model.PRIIndividualsCosts;
            parameters[71].Value = model.ComplementaryMedicalCosts;
            parameters[72].Value = model.ComplementaryMedical;
            parameters[73].Value = model.AccidentInsurance;
            parameters[74].Value = model.AccidentInsuranceBeginTime;
            parameters[75].Value = model.AccidentInsuranceEndTime;

            if (trans == null)
                DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            else
                DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SEP_EmployeeWelfareInfo ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public void DeleteBySysUserID(int sysid, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SEP_EmployeeWelfareInfo ");
            strSql.Append(" where sysid=@sysid ");
            SqlParameter[] parameters = {
					new SqlParameter("@sysid", SqlDbType.Int,4)};
            parameters[0].Value = sysid;

            DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EmployeeWelfareInfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,sysid,contractType,contractTerm,contractCompany,contractStartDate,contractEndDate,probationPeriod,probationPeriodDeadLine,probationEndDate,endowmentInsurance,unemploymentInsurance,birthInsurance,compoInsurance,medicalInsurance,socialInsuranceCompany,socialInsuranceAddress,socialInsuranceCode,medicalInsuranceCode,socialInsuranceBase,medicalInsuranceBase,publicReserveFundsCompany,publicReserveFundsAddress,publicReserveFundsBase,publicReserveFundsCode,isArchive,ArchiveCode,ArchiveDate,memo,publicReserveFunds,contractRenewalCount,contractSignInfo,ArchivePlace,InsurancePlace,endowmentInsuranceStarTime,endowmentInsuranceEndTime,unemploymentInsuranceStarTime,unemploymentInsuranceEndTime,birthInsuranceStarTime,birthInsuranceEndTime,compoInsuranceStarTime,compoInsuranceEndTime,medicalInsuranceStarTime,medicalInsuranceEndTime,publicReserveFundsStarTime,publicReserveFundsEndTime,EIProportionOfFirms,EIProportionOfIndividuals,UIProportionOfFirms,UIProportionOfIndividuals,BIProportionOfFirms,BIProportionOfIndividuals,CIProportionOfFirms,CIProportionOfIndividuals,MIProportionOfFirms,MIProportionOfIndividuals,MIBigProportionOfIndividuals,PRFProportionOfFirms,PRFProportionOfIndividuals,EIFirmsCosts,EIIndividualsCosts,UIFirmsCosts,UIIndividualsCosts,BIFirmsCosts,BIIndividualsCosts,CIFirmsCosts,CIIndividualsCosts,MIFirmsCosts,MIIndividualsCosts,PRFirmsCosts,PRIIndividualsCosts,ComplementaryMedicalCosts,ComplementaryMedical,AccidentInsurance,AccidentInsuranceBeginTime,AccidentInsuranceEndTime from sep_EmployeeWelfareInfo ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            EmployeeWelfareInfo model = new EmployeeWelfareInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sysid"].ToString() != "")
                {
                    model.sysid = int.Parse(ds.Tables[0].Rows[0]["sysid"].ToString());
                }
                model.contractType = ds.Tables[0].Rows[0]["contractType"].ToString();
                model.contractTerm = ds.Tables[0].Rows[0]["contractTerm"].ToString();
                model.contractCompany = ds.Tables[0].Rows[0]["contractCompany"].ToString();
                if (ds.Tables[0].Rows[0]["contractStartDate"].ToString() != "")
                {
                    model.contractStartDate = DateTime.Parse(ds.Tables[0].Rows[0]["contractStartDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["contractEndDate"].ToString() != "")
                {
                    model.contractEndDate = DateTime.Parse(ds.Tables[0].Rows[0]["contractEndDate"].ToString());
                }
                model.probationPeriod = ds.Tables[0].Rows[0]["probationPeriod"].ToString();
                if (ds.Tables[0].Rows[0]["probationPeriodDeadLine"].ToString() != "")
                {
                    model.probationPeriodDeadLine = DateTime.Parse(ds.Tables[0].Rows[0]["probationPeriodDeadLine"].ToString());
                }
                if (ds.Tables[0].Rows[0]["probationEndDate"].ToString() != "")
                {
                    model.probationEndDate = DateTime.Parse(ds.Tables[0].Rows[0]["probationEndDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["endowmentInsurance"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["endowmentInsurance"].ToString() == "1") || (ds.Tables[0].Rows[0]["endowmentInsurance"].ToString().ToLower() == "true"))
                    {
                        model.endowmentInsurance = true;
                    }
                    else
                    {
                        model.endowmentInsurance = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["unemploymentInsurance"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["unemploymentInsurance"].ToString() == "1") || (ds.Tables[0].Rows[0]["unemploymentInsurance"].ToString().ToLower() == "true"))
                    {
                        model.unemploymentInsurance = true;
                    }
                    else
                    {
                        model.unemploymentInsurance = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["birthInsurance"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["birthInsurance"].ToString() == "1") || (ds.Tables[0].Rows[0]["birthInsurance"].ToString().ToLower() == "true"))
                    {
                        model.birthInsurance = true;
                    }
                    else
                    {
                        model.birthInsurance = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["compoInsurance"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["compoInsurance"].ToString() == "1") || (ds.Tables[0].Rows[0]["compoInsurance"].ToString().ToLower() == "true"))
                    {
                        model.compoInsurance = true;
                    }
                    else
                    {
                        model.compoInsurance = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["medicalInsurance"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["medicalInsurance"].ToString() == "1") || (ds.Tables[0].Rows[0]["medicalInsurance"].ToString().ToLower() == "true"))
                    {
                        model.medicalInsurance = true;
                    }
                    else
                    {
                        model.medicalInsurance = false;
                    }
                }
                model.socialInsuranceCompany = ds.Tables[0].Rows[0]["socialInsuranceCompany"].ToString();
                model.socialInsuranceAddress = ds.Tables[0].Rows[0]["socialInsuranceAddress"].ToString();
                model.socialInsuranceCode = ds.Tables[0].Rows[0]["socialInsuranceCode"].ToString();
                model.medicalInsuranceCode = ds.Tables[0].Rows[0]["medicalInsuranceCode"].ToString();
                model.socialInsuranceBase = ds.Tables[0].Rows[0]["socialInsuranceBase"].ToString();
                model.medicalInsuranceBase = ds.Tables[0].Rows[0]["medicalInsuranceBase"].ToString();
                model.publicReserveFundsCompany = ds.Tables[0].Rows[0]["publicReserveFundsCompany"].ToString();
                model.publicReserveFundsAddress = ds.Tables[0].Rows[0]["publicReserveFundsAddress"].ToString();
                model.publicReserveFundsBase = ds.Tables[0].Rows[0]["publicReserveFundsBase"].ToString();
                model.publicReserveFundsCode = ds.Tables[0].Rows[0]["publicReserveFundsCode"].ToString();
                if (ds.Tables[0].Rows[0]["isArchive"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["isArchive"].ToString() == "1") || (ds.Tables[0].Rows[0]["isArchive"].ToString().ToLower() == "true"))
                    {
                        model.isArchive = true;
                    }
                    else
                    {
                        model.isArchive = false;
                    }
                }
                model.ArchiveCode = ds.Tables[0].Rows[0]["ArchiveCode"].ToString();
                model.ArchiveDate = ds.Tables[0].Rows[0]["ArchiveDate"].ToString();
                model.memo = ds.Tables[0].Rows[0]["memo"].ToString();
                if (ds.Tables[0].Rows[0]["publicReserveFunds"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["publicReserveFunds"].ToString() == "1") || (ds.Tables[0].Rows[0]["publicReserveFunds"].ToString().ToLower() == "true"))
                    {
                        model.publicReserveFunds = true;
                    }
                    else
                    {
                        model.publicReserveFunds = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["contractRenewalCount"].ToString() != "")
                {
                    model.contractRenewalCount = int.Parse(ds.Tables[0].Rows[0]["contractRenewalCount"].ToString());
                }
                model.contractSignInfo = ds.Tables[0].Rows[0]["contractSignInfo"].ToString();
                model.ArchivePlace = ds.Tables[0].Rows[0]["ArchivePlace"].ToString();
                model.InsurancePlace = ds.Tables[0].Rows[0]["InsurancePlace"].ToString();
                if (ds.Tables[0].Rows[0]["endowmentInsuranceStarTime"].ToString() != "")
                {
                    model.endowmentInsuranceStarTime = DateTime.Parse(ds.Tables[0].Rows[0]["endowmentInsuranceStarTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["endowmentInsuranceEndTime"].ToString() != "")
                {
                    model.endowmentInsuranceEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["endowmentInsuranceEndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["unemploymentInsuranceStarTime"].ToString() != "")
                {
                    model.unemploymentInsuranceStarTime = DateTime.Parse(ds.Tables[0].Rows[0]["unemploymentInsuranceStarTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["unemploymentInsuranceEndTime"].ToString() != "")
                {
                    model.unemploymentInsuranceEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["unemploymentInsuranceEndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["birthInsuranceStarTime"].ToString() != "")
                {
                    model.birthInsuranceStarTime = DateTime.Parse(ds.Tables[0].Rows[0]["birthInsuranceStarTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["birthInsuranceEndTime"].ToString() != "")
                {
                    model.birthInsuranceEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["birthInsuranceEndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["compoInsuranceStarTime"].ToString() != "")
                {
                    model.compoInsuranceStarTime = DateTime.Parse(ds.Tables[0].Rows[0]["compoInsuranceStarTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["compoInsuranceEndTime"].ToString() != "")
                {
                    model.compoInsuranceEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["compoInsuranceEndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["medicalInsuranceStarTime"].ToString() != "")
                {
                    model.medicalInsuranceStarTime = DateTime.Parse(ds.Tables[0].Rows[0]["medicalInsuranceStarTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["medicalInsuranceEndTime"].ToString() != "")
                {
                    model.medicalInsuranceEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["medicalInsuranceEndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["publicReserveFundsStarTime"].ToString() != "")
                {
                    model.publicReserveFundsStarTime = DateTime.Parse(ds.Tables[0].Rows[0]["publicReserveFundsStarTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["publicReserveFundsEndTime"].ToString() != "")
                {
                    model.publicReserveFundsEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["publicReserveFundsEndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EIProportionOfFirms"].ToString() != "")
                {
                    model.EIProportionOfFirms = decimal.Parse(ds.Tables[0].Rows[0]["EIProportionOfFirms"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EIProportionOfIndividuals"].ToString() != "")
                {
                    model.EIProportionOfIndividuals = decimal.Parse(ds.Tables[0].Rows[0]["EIProportionOfIndividuals"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UIProportionOfFirms"].ToString() != "")
                {
                    model.UIProportionOfFirms = decimal.Parse(ds.Tables[0].Rows[0]["UIProportionOfFirms"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UIProportionOfIndividuals"].ToString() != "")
                {
                    model.UIProportionOfIndividuals = decimal.Parse(ds.Tables[0].Rows[0]["UIProportionOfIndividuals"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BIProportionOfFirms"].ToString() != "")
                {
                    model.BIProportionOfFirms = decimal.Parse(ds.Tables[0].Rows[0]["BIProportionOfFirms"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BIProportionOfIndividuals"].ToString() != "")
                {
                    model.BIProportionOfIndividuals = decimal.Parse(ds.Tables[0].Rows[0]["BIProportionOfIndividuals"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CIProportionOfFirms"].ToString() != "")
                {
                    model.CIProportionOfFirms = decimal.Parse(ds.Tables[0].Rows[0]["CIProportionOfFirms"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CIProportionOfIndividuals"].ToString() != "")
                {
                    model.CIProportionOfIndividuals = decimal.Parse(ds.Tables[0].Rows[0]["CIProportionOfIndividuals"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MIProportionOfFirms"].ToString() != "")
                {
                    model.MIProportionOfFirms = decimal.Parse(ds.Tables[0].Rows[0]["MIProportionOfFirms"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MIProportionOfIndividuals"].ToString() != "")
                {
                    model.MIProportionOfIndividuals = decimal.Parse(ds.Tables[0].Rows[0]["MIProportionOfIndividuals"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MIBigProportionOfIndividuals"].ToString() != "")
                {
                    model.MIBigProportionOfIndividuals = decimal.Parse(ds.Tables[0].Rows[0]["MIBigProportionOfIndividuals"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PRFProportionOfFirms"].ToString() != "")
                {
                    model.PRFProportionOfFirms = decimal.Parse(ds.Tables[0].Rows[0]["PRFProportionOfFirms"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PRFProportionOfIndividuals"].ToString() != "")
                {
                    model.PRFProportionOfIndividuals = decimal.Parse(ds.Tables[0].Rows[0]["PRFProportionOfIndividuals"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EIFirmsCosts"].ToString() != "")
                {
                    model.EIFirmsCosts = (ds.Tables[0].Rows[0]["EIFirmsCosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EIIndividualsCosts"].ToString() != "")
                {
                    model.EIIndividualsCosts = (ds.Tables[0].Rows[0]["EIIndividualsCosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UIFirmsCosts"].ToString() != "")
                {
                    model.UIFirmsCosts = (ds.Tables[0].Rows[0]["UIFirmsCosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UIIndividualsCosts"].ToString() != "")
                {
                    model.UIIndividualsCosts = (ds.Tables[0].Rows[0]["UIIndividualsCosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BIFirmsCosts"].ToString() != "")
                {
                    model.BIFirmsCosts = (ds.Tables[0].Rows[0]["BIFirmsCosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BIIndividualsCosts"].ToString() != "")
                {
                    model.BIIndividualsCosts = (ds.Tables[0].Rows[0]["BIIndividualsCosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CIFirmsCosts"].ToString() != "")
                {
                    model.CIFirmsCosts = (ds.Tables[0].Rows[0]["CIFirmsCosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CIIndividualsCosts"].ToString() != "")
                {
                    model.CIIndividualsCosts = (ds.Tables[0].Rows[0]["CIIndividualsCosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MIFirmsCosts"].ToString() != "")
                {
                    model.MIFirmsCosts = (ds.Tables[0].Rows[0]["MIFirmsCosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MIIndividualsCosts"].ToString() != "")
                {
                    model.MIIndividualsCosts = (ds.Tables[0].Rows[0]["MIIndividualsCosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PRFirmsCosts"].ToString() != "")
                {
                    model.PRFirmsCosts = (ds.Tables[0].Rows[0]["PRFirmsCosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PRIIndividualsCosts"].ToString() != "")
                {
                    model.PRIIndividualsCosts = (ds.Tables[0].Rows[0]["PRIIndividualsCosts"].ToString());
                }

                if (ds.Tables[0].Rows[0]["ComplementaryMedicalCosts"].ToString() != "")
                {
                    model.ComplementaryMedicalCosts = decimal.Parse(ds.Tables[0].Rows[0]["ComplementaryMedicalCosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ComplementaryMedical"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["ComplementaryMedical"].ToString() == "1") || (ds.Tables[0].Rows[0]["ComplementaryMedical"].ToString().ToLower() == "true"))
                    {
                        model.ComplementaryMedical = true;
                    }
                    else
                    {
                        model.ComplementaryMedical = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["AccidentInsurance"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["AccidentInsurance"].ToString() == "1") || (ds.Tables[0].Rows[0]["AccidentInsurance"].ToString().ToLower() == "true"))
                    {
                        model.AccidentInsurance = true;
                    }
                    else
                    {
                        model.AccidentInsurance = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["AccidentInsuranceBeginTime"].ToString() != "")
                {
                    model.AccidentInsuranceBeginTime = DateTime.Parse(ds.Tables[0].Rows[0]["AccidentInsuranceBeginTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AccidentInsuranceEndTime"].ToString() != "")
                {
                    model.AccidentInsuranceEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["AccidentInsuranceEndTime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        public EmployeeWelfareInfo getModelBySysId(int sysid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,sysid,contractType,contractTerm,contractCompany,contractStartDate,contractEndDate,probationPeriod,probationPeriodDeadLine,probationEndDate,endowmentInsurance,unemploymentInsurance,birthInsurance,compoInsurance,medicalInsurance,socialInsuranceCompany,socialInsuranceAddress,socialInsuranceCode,medicalInsuranceCode,socialInsuranceBase,medicalInsuranceBase,publicReserveFundsCompany,publicReserveFundsAddress,publicReserveFundsBase,publicReserveFundsCode,isArchive,ArchiveCode,ArchiveDate,memo,publicReserveFunds,contractRenewalCount,contractSignInfo,ArchivePlace,InsurancePlace,endowmentInsuranceStarTime,endowmentInsuranceEndTime,unemploymentInsuranceStarTime,unemploymentInsuranceEndTime,birthInsuranceStarTime,birthInsuranceEndTime,compoInsuranceStarTime,compoInsuranceEndTime,medicalInsuranceStarTime,medicalInsuranceEndTime,publicReserveFundsStarTime,publicReserveFundsEndTime,EIProportionOfFirms,EIProportionOfIndividuals,UIProportionOfFirms,UIProportionOfIndividuals,BIProportionOfFirms,BIProportionOfIndividuals,CIProportionOfFirms,CIProportionOfIndividuals,MIProportionOfFirms,MIProportionOfIndividuals,MIBigProportionOfIndividuals,PRFProportionOfFirms,PRFProportionOfIndividuals,EIFirmsCosts,EIIndividualsCosts,UIFirmsCosts,UIIndividualsCosts,BIFirmsCosts,BIIndividualsCosts,CIFirmsCosts,CIIndividualsCosts,MIFirmsCosts,MIIndividualsCosts,PRFirmsCosts,PRIIndividualsCosts,ComplementaryMedicalCosts,ComplementaryMedical,AccidentInsurance,AccidentInsuranceBeginTime,AccidentInsuranceEndTime from sep_EmployeeWelfareInfo ");
            strSql.Append(" where sysid=@sysid ");
            SqlParameter[] parameters = {
					new SqlParameter("@sysid", SqlDbType.Int,4)};
            parameters[0].Value = sysid;

            EmployeeWelfareInfo model = new EmployeeWelfareInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sysid"].ToString() != "")
                {
                    model.sysid = int.Parse(ds.Tables[0].Rows[0]["sysid"].ToString());
                }
                model.contractType = ds.Tables[0].Rows[0]["contractType"].ToString();
                model.contractTerm = ds.Tables[0].Rows[0]["contractTerm"].ToString();
                model.contractCompany = ds.Tables[0].Rows[0]["contractCompany"].ToString();
                if (ds.Tables[0].Rows[0]["contractStartDate"].ToString() != "")
                {
                    model.contractStartDate = DateTime.Parse(ds.Tables[0].Rows[0]["contractStartDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["contractEndDate"].ToString() != "")
                {
                    model.contractEndDate = DateTime.Parse(ds.Tables[0].Rows[0]["contractEndDate"].ToString());
                }
                model.probationPeriod = ds.Tables[0].Rows[0]["probationPeriod"].ToString();
                if (ds.Tables[0].Rows[0]["probationPeriodDeadLine"].ToString() != "")
                {
                    model.probationPeriodDeadLine = DateTime.Parse(ds.Tables[0].Rows[0]["probationPeriodDeadLine"].ToString());
                }
                if (ds.Tables[0].Rows[0]["probationEndDate"].ToString() != "")
                {
                    model.probationEndDate = DateTime.Parse(ds.Tables[0].Rows[0]["probationEndDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["endowmentInsurance"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["endowmentInsurance"].ToString() == "1") || (ds.Tables[0].Rows[0]["endowmentInsurance"].ToString().ToLower() == "true"))
                    {
                        model.endowmentInsurance = true;
                    }
                    else
                    {
                        model.endowmentInsurance = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["unemploymentInsurance"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["unemploymentInsurance"].ToString() == "1") || (ds.Tables[0].Rows[0]["unemploymentInsurance"].ToString().ToLower() == "true"))
                    {
                        model.unemploymentInsurance = true;
                    }
                    else
                    {
                        model.unemploymentInsurance = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["birthInsurance"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["birthInsurance"].ToString() == "1") || (ds.Tables[0].Rows[0]["birthInsurance"].ToString().ToLower() == "true"))
                    {
                        model.birthInsurance = true;
                    }
                    else
                    {
                        model.birthInsurance = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["compoInsurance"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["compoInsurance"].ToString() == "1") || (ds.Tables[0].Rows[0]["compoInsurance"].ToString().ToLower() == "true"))
                    {
                        model.compoInsurance = true;
                    }
                    else
                    {
                        model.compoInsurance = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["medicalInsurance"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["medicalInsurance"].ToString() == "1") || (ds.Tables[0].Rows[0]["medicalInsurance"].ToString().ToLower() == "true"))
                    {
                        model.medicalInsurance = true;
                    }
                    else
                    {
                        model.medicalInsurance = false;
                    }
                }
                model.socialInsuranceCompany = ds.Tables[0].Rows[0]["socialInsuranceCompany"].ToString();
                model.socialInsuranceAddress = ds.Tables[0].Rows[0]["socialInsuranceAddress"].ToString();
                model.socialInsuranceCode = ds.Tables[0].Rows[0]["socialInsuranceCode"].ToString();
                model.medicalInsuranceCode = ds.Tables[0].Rows[0]["medicalInsuranceCode"].ToString();
                model.socialInsuranceBase = ds.Tables[0].Rows[0]["socialInsuranceBase"].ToString();
                model.medicalInsuranceBase = ds.Tables[0].Rows[0]["medicalInsuranceBase"].ToString();
                model.publicReserveFundsCompany = ds.Tables[0].Rows[0]["publicReserveFundsCompany"].ToString();
                model.publicReserveFundsAddress = ds.Tables[0].Rows[0]["publicReserveFundsAddress"].ToString();
                model.publicReserveFundsBase = ds.Tables[0].Rows[0]["publicReserveFundsBase"].ToString();
                model.publicReserveFundsCode = ds.Tables[0].Rows[0]["publicReserveFundsCode"].ToString();
                if (ds.Tables[0].Rows[0]["isArchive"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["isArchive"].ToString() == "1") || (ds.Tables[0].Rows[0]["isArchive"].ToString().ToLower() == "true"))
                    {
                        model.isArchive = true;
                    }
                    else
                    {
                        model.isArchive = false;
                    }
                }
                model.ArchiveCode = ds.Tables[0].Rows[0]["ArchiveCode"].ToString();
                model.ArchiveDate = ds.Tables[0].Rows[0]["ArchiveDate"].ToString();
                model.memo = ds.Tables[0].Rows[0]["memo"].ToString();
                if (ds.Tables[0].Rows[0]["publicReserveFunds"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["publicReserveFunds"].ToString() == "1") || (ds.Tables[0].Rows[0]["publicReserveFunds"].ToString().ToLower() == "true"))
                    {
                        model.publicReserveFunds = true;
                    }
                    else
                    {
                        model.publicReserveFunds = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["contractRenewalCount"].ToString() != "")
                {
                    model.contractRenewalCount = int.Parse(ds.Tables[0].Rows[0]["contractRenewalCount"].ToString());
                }
                model.contractSignInfo = ds.Tables[0].Rows[0]["contractSignInfo"].ToString();
                model.ArchivePlace = ds.Tables[0].Rows[0]["ArchivePlace"].ToString();
                model.InsurancePlace = ds.Tables[0].Rows[0]["InsurancePlace"].ToString();
                if (ds.Tables[0].Rows[0]["endowmentInsuranceStarTime"].ToString() != "")
                {
                    model.endowmentInsuranceStarTime = DateTime.Parse(ds.Tables[0].Rows[0]["endowmentInsuranceStarTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["endowmentInsuranceEndTime"].ToString() != "")
                {
                    model.endowmentInsuranceEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["endowmentInsuranceEndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["unemploymentInsuranceStarTime"].ToString() != "")
                {
                    model.unemploymentInsuranceStarTime = DateTime.Parse(ds.Tables[0].Rows[0]["unemploymentInsuranceStarTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["unemploymentInsuranceEndTime"].ToString() != "")
                {
                    model.unemploymentInsuranceEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["unemploymentInsuranceEndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["birthInsuranceStarTime"].ToString() != "")
                {
                    model.birthInsuranceStarTime = DateTime.Parse(ds.Tables[0].Rows[0]["birthInsuranceStarTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["birthInsuranceEndTime"].ToString() != "")
                {
                    model.birthInsuranceEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["birthInsuranceEndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["compoInsuranceStarTime"].ToString() != "")
                {
                    model.compoInsuranceStarTime = DateTime.Parse(ds.Tables[0].Rows[0]["compoInsuranceStarTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["compoInsuranceEndTime"].ToString() != "")
                {
                    model.compoInsuranceEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["compoInsuranceEndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["medicalInsuranceStarTime"].ToString() != "")
                {
                    model.medicalInsuranceStarTime = DateTime.Parse(ds.Tables[0].Rows[0]["medicalInsuranceStarTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["medicalInsuranceEndTime"].ToString() != "")
                {
                    model.medicalInsuranceEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["medicalInsuranceEndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["publicReserveFundsStarTime"].ToString() != "")
                {
                    model.publicReserveFundsStarTime = DateTime.Parse(ds.Tables[0].Rows[0]["publicReserveFundsStarTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["publicReserveFundsEndTime"].ToString() != "")
                {
                    model.publicReserveFundsEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["publicReserveFundsEndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EIProportionOfFirms"].ToString() != "")
                {
                    model.EIProportionOfFirms = decimal.Parse(ds.Tables[0].Rows[0]["EIProportionOfFirms"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EIProportionOfIndividuals"].ToString() != "")
                {
                    model.EIProportionOfIndividuals = decimal.Parse(ds.Tables[0].Rows[0]["EIProportionOfIndividuals"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UIProportionOfFirms"].ToString() != "")
                {
                    model.UIProportionOfFirms = decimal.Parse(ds.Tables[0].Rows[0]["UIProportionOfFirms"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UIProportionOfIndividuals"].ToString() != "")
                {
                    model.UIProportionOfIndividuals = decimal.Parse(ds.Tables[0].Rows[0]["UIProportionOfIndividuals"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BIProportionOfFirms"].ToString() != "")
                {
                    model.BIProportionOfFirms = decimal.Parse(ds.Tables[0].Rows[0]["BIProportionOfFirms"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BIProportionOfIndividuals"].ToString() != "")
                {
                    model.BIProportionOfIndividuals = decimal.Parse(ds.Tables[0].Rows[0]["BIProportionOfIndividuals"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CIProportionOfFirms"].ToString() != "")
                {
                    model.CIProportionOfFirms = decimal.Parse(ds.Tables[0].Rows[0]["CIProportionOfFirms"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CIProportionOfIndividuals"].ToString() != "")
                {
                    model.CIProportionOfIndividuals = decimal.Parse(ds.Tables[0].Rows[0]["CIProportionOfIndividuals"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MIProportionOfFirms"].ToString() != "")
                {
                    model.MIProportionOfFirms = decimal.Parse(ds.Tables[0].Rows[0]["MIProportionOfFirms"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MIProportionOfIndividuals"].ToString() != "")
                {
                    model.MIProportionOfIndividuals = decimal.Parse(ds.Tables[0].Rows[0]["MIProportionOfIndividuals"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MIBigProportionOfIndividuals"].ToString() != "")
                {
                    model.MIBigProportionOfIndividuals = decimal.Parse(ds.Tables[0].Rows[0]["MIBigProportionOfIndividuals"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PRFProportionOfFirms"].ToString() != "")
                {
                    model.PRFProportionOfFirms = decimal.Parse(ds.Tables[0].Rows[0]["PRFProportionOfFirms"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PRFProportionOfIndividuals"].ToString() != "")
                {
                    model.PRFProportionOfIndividuals = decimal.Parse(ds.Tables[0].Rows[0]["PRFProportionOfIndividuals"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EIFirmsCosts"].ToString() != "")
                {
                    model.EIFirmsCosts = (ds.Tables[0].Rows[0]["EIFirmsCosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EIIndividualsCosts"].ToString() != "")
                {
                    model.EIIndividualsCosts = (ds.Tables[0].Rows[0]["EIIndividualsCosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UIFirmsCosts"].ToString() != "")
                {
                    model.UIFirmsCosts = (ds.Tables[0].Rows[0]["UIFirmsCosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UIIndividualsCosts"].ToString() != "")
                {
                    model.UIIndividualsCosts = (ds.Tables[0].Rows[0]["UIIndividualsCosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BIFirmsCosts"].ToString() != "")
                {
                    model.BIFirmsCosts = (ds.Tables[0].Rows[0]["BIFirmsCosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BIIndividualsCosts"].ToString() != "")
                {
                    model.BIIndividualsCosts = (ds.Tables[0].Rows[0]["BIIndividualsCosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CIFirmsCosts"].ToString() != "")
                {
                    model.CIFirmsCosts = (ds.Tables[0].Rows[0]["CIFirmsCosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CIIndividualsCosts"].ToString() != "")
                {
                    model.CIIndividualsCosts = (ds.Tables[0].Rows[0]["CIIndividualsCosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MIFirmsCosts"].ToString() != "")
                {
                    model.MIFirmsCosts = (ds.Tables[0].Rows[0]["MIFirmsCosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MIIndividualsCosts"].ToString() != "")
                {
                    model.MIIndividualsCosts = (ds.Tables[0].Rows[0]["MIIndividualsCosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PRFirmsCosts"].ToString() != "")
                {
                    model.PRFirmsCosts = (ds.Tables[0].Rows[0]["PRFirmsCosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PRIIndividualsCosts"].ToString() != "")
                {
                    model.PRIIndividualsCosts = (ds.Tables[0].Rows[0]["PRIIndividualsCosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ComplementaryMedicalCosts"].ToString() != "")
                {
                    model.ComplementaryMedicalCosts = decimal.Parse(ds.Tables[0].Rows[0]["ComplementaryMedicalCosts"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ComplementaryMedical"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["ComplementaryMedical"].ToString() == "1") || (ds.Tables[0].Rows[0]["ComplementaryMedical"].ToString().ToLower() == "true"))
                    {
                        model.ComplementaryMedical = true;
                    }
                    else
                    {
                        model.ComplementaryMedical = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["AccidentInsurance"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["AccidentInsurance"].ToString() == "1") || (ds.Tables[0].Rows[0]["AccidentInsurance"].ToString().ToLower() == "true"))
                    {
                        model.AccidentInsurance = true;
                    }
                    else
                    {
                        model.AccidentInsurance = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["AccidentInsuranceBeginTime"].ToString() != "")
                {
                    model.AccidentInsuranceBeginTime = DateTime.Parse(ds.Tables[0].Rows[0]["AccidentInsuranceBeginTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AccidentInsuranceEndTime"].ToString() != "")
                {
                    model.AccidentInsuranceEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["AccidentInsuranceEndTime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM SEP_EmployeeWelfareInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  成员方法
    }
}
