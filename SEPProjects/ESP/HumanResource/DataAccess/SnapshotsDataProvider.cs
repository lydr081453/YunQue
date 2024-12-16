using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.DataAccess
{    
    public class SnapshotsDataProvider
    {
        public SnapshotsDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SnapshotsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sep_Snapshots(");
            strSql.Append("UserID,Code,TypeID,MaritalStatus,Gender,Birthday,Degree,Education,GraduatedFrom,Major,ThisYearSalary,Status,nowBasePay,nowMeritPay,newBasePay,newMeritPay,Creator,CreatedTime,UserName,CreatorName,endowmentInsuranceStarTime,endowmentInsuranceEndTime,unemploymentInsuranceStarTime,unemploymentInsuranceEndTime,birthInsuranceStarTime,birthInsuranceEndTime,compoInsuranceStarTime,compoInsuranceEndTime,medicalInsuranceStarTime,medicalInsuranceEndTime,publicReserveFundsStarTime,publicReserveFundsEndTime,contractStartDate,contractEndDate,probationEndDate,socialInsuranceBase,medicalInsuranceBase,publicReserveFundsBase,isArchive,contractSignInfo,InsurancePlace,IsForeign,IsCertification,WageMonths,EIProportionOfFirms,EIProportionOfIndividuals,UIProportionOfFirms,UIProportionOfIndividuals,BIProportionOfFirms,BIProportionOfIndividuals,CIProportionOfFirms,CIProportionOfIndividuals,MIProportionOfFirms,MIProportionOfIndividuals,MIBigProportionOfIndividuals,PRFProportionOfFirms,PRFProportionOfIndividuals,EIFirmsCosts,EIIndividualsCosts,UIFirmsCosts,UIIndividualsCosts,BIFirmsCosts,BIIndividualsCosts,CIFirmsCosts,CIIndividualsCosts,MIFirmsCosts,MIIndividualsCosts,PRFirmsCosts,PRIIndividualsCosts,socialInsuranceCompany,CommonName)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@Code,@TypeID,@MaritalStatus,@Gender,@Birthday,@Degree,@Education,@GraduatedFrom,@Major,@ThisYearSalary,@Status,@nowBasePay,@nowMeritPay,@newBasePay,@newMeritPay,@Creator,@CreatedTime,@UserName,@CreatorName,@endowmentInsuranceStarTime,@endowmentInsuranceEndTime,@unemploymentInsuranceStarTime,@unemploymentInsuranceEndTime,@birthInsuranceStarTime,@birthInsuranceEndTime,@compoInsuranceStarTime,@compoInsuranceEndTime,@medicalInsuranceStarTime,@medicalInsuranceEndTime,@publicReserveFundsStarTime,@publicReserveFundsEndTime,@contractStartDate,@contractEndDate,@probationEndDate,@socialInsuranceBase,@medicalInsuranceBase,@publicReserveFundsBase,@isArchive,@contractSignInfo,@InsurancePlace,@IsForeign,@IsCertification,@WageMonths,@EIProportionOfFirms,@EIProportionOfIndividuals,@UIProportionOfFirms,@UIProportionOfIndividuals,@BIProportionOfFirms,@BIProportionOfIndividuals,@CIProportionOfFirms,@CIProportionOfIndividuals,@MIProportionOfFirms,@MIProportionOfIndividuals,@MIBigProportionOfIndividuals,@PRFProportionOfFirms,@PRFProportionOfIndividuals,@EIFirmsCosts,@EIIndividualsCosts,@UIFirmsCosts,@UIIndividualsCosts,@BIFirmsCosts,@BIIndividualsCosts,@CIFirmsCosts,@CIIndividualsCosts,@MIFirmsCosts,@MIIndividualsCosts,@PRFirmsCosts,@PRIIndividualsCosts,@socialInsuranceCompany,@CommonName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Code", SqlDbType.NVarChar,128),
					new SqlParameter("@TypeID", SqlDbType.Int,4),
					new SqlParameter("@MaritalStatus", SqlDbType.Int,4),
					new SqlParameter("@Gender", SqlDbType.Int,4),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@Degree", SqlDbType.NVarChar,32),
					new SqlParameter("@Education", SqlDbType.NVarChar,32),
					new SqlParameter("@GraduatedFrom", SqlDbType.NVarChar,256),
					new SqlParameter("@Major", SqlDbType.NVarChar,256),
					new SqlParameter("@ThisYearSalary", SqlDbType.NVarChar,100),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@nowBasePay", SqlDbType.NVarChar,200),
					new SqlParameter("@nowMeritPay", SqlDbType.NVarChar,200),
					new SqlParameter("@newBasePay", SqlDbType.NVarChar,200),
					new SqlParameter("@newMeritPay", SqlDbType.NVarChar,200),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreatedTime", SqlDbType.DateTime),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorName", SqlDbType.NVarChar,50),
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
					new SqlParameter("@contractStartDate", SqlDbType.SmallDateTime),
					new SqlParameter("@contractEndDate", SqlDbType.SmallDateTime),
					new SqlParameter("@probationEndDate", SqlDbType.SmallDateTime),
					new SqlParameter("@socialInsuranceBase", SqlDbType.NVarChar,200),
					new SqlParameter("@medicalInsuranceBase", SqlDbType.NVarChar,200),
					new SqlParameter("@publicReserveFundsBase", SqlDbType.NVarChar,200),
					new SqlParameter("@isArchive", SqlDbType.Bit,1),
					new SqlParameter("@contractSignInfo", SqlDbType.NVarChar,50),
					new SqlParameter("@InsurancePlace", SqlDbType.NVarChar,50),
					new SqlParameter("@IsForeign", SqlDbType.Bit,1),
					new SqlParameter("@IsCertification", SqlDbType.Bit,1),
					new SqlParameter("@WageMonths", SqlDbType.Int,4),
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
                    new SqlParameter("@socialInsuranceCompany",SqlDbType.NVarChar,200),
                    new SqlParameter("@CommonName",SqlDbType.NVarChar,255)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.Code;
            parameters[2].Value = model.TypeID;
            parameters[3].Value = model.MaritalStatus;
            parameters[4].Value = model.Gender;
            parameters[5].Value = model.Birthday;
            parameters[6].Value = model.Degree;
            parameters[7].Value = model.Education;
            parameters[8].Value = model.GraduatedFrom;
            parameters[9].Value = model.Major;
            parameters[10].Value = model.ThisYearSalary;
            parameters[11].Value = model.Status;
            parameters[12].Value = model.nowBasePay;
            parameters[13].Value = model.nowMeritPay;
            parameters[14].Value = model.newBasePay;
            parameters[15].Value = model.newMeritPay;
            parameters[16].Value = model.Creator;
            parameters[17].Value = model.CreatedTime;
            parameters[18].Value = model.UserName;
            parameters[19].Value = model.CreatorName;
            parameters[20].Value = model.endowmentInsuranceStarTime;
            parameters[21].Value = model.endowmentInsuranceEndTime;
            parameters[22].Value = model.unemploymentInsuranceStarTime;
            parameters[23].Value = model.unemploymentInsuranceEndTime;
            parameters[24].Value = model.birthInsuranceStarTime;
            parameters[25].Value = model.birthInsuranceEndTime;
            parameters[26].Value = model.compoInsuranceStarTime;
            parameters[27].Value = model.compoInsuranceEndTime;
            parameters[28].Value = model.medicalInsuranceStarTime;
            parameters[29].Value = model.medicalInsuranceEndTime;
            parameters[30].Value = model.publicReserveFundsStarTime;
            parameters[31].Value = model.publicReserveFundsEndTime;
            parameters[32].Value = model.contractStartDate;
            parameters[33].Value = model.contractEndDate;
            parameters[34].Value = model.probationEndDate;
            parameters[35].Value = model.socialInsuranceBase;
            parameters[36].Value = model.medicalInsuranceBase;
            parameters[37].Value = model.publicReserveFundsBase;
            parameters[38].Value = model.isArchive;
            parameters[39].Value = model.contractSignInfo;
            parameters[40].Value = model.InsurancePlace;
            parameters[41].Value = model.IsForeign;
            parameters[42].Value = model.IsCertification;
            parameters[43].Value = model.WageMonths;
            parameters[44].Value = model.EIProportionOfFirms;
            parameters[45].Value = model.EIProportionOfIndividuals;
            parameters[46].Value = model.UIProportionOfFirms;
            parameters[47].Value = model.UIProportionOfIndividuals;
            parameters[48].Value = model.BIProportionOfFirms;
            parameters[49].Value = model.BIProportionOfIndividuals;
            parameters[50].Value = model.CIProportionOfFirms;
            parameters[51].Value = model.CIProportionOfIndividuals;
            parameters[52].Value = model.MIProportionOfFirms;
            parameters[53].Value = model.MIProportionOfIndividuals;
            parameters[54].Value = model.MIBigProportionOfIndividuals;
            parameters[55].Value = model.PRFProportionOfFirms;
            parameters[56].Value = model.PRFProportionOfIndividuals;
            parameters[57].Value = model.EIFirmsCosts;
            parameters[58].Value = model.EIIndividualsCosts;
            parameters[59].Value = model.UIFirmsCosts;
            parameters[60].Value = model.UIIndividualsCosts;
            parameters[61].Value = model.BIFirmsCosts;
            parameters[62].Value = model.BIIndividualsCosts;
            parameters[63].Value = model.CIFirmsCosts;
            parameters[64].Value = model.CIIndividualsCosts;
            parameters[65].Value = model.MIFirmsCosts;
            parameters[66].Value = model.MIIndividualsCosts;
            parameters[67].Value = model.PRFirmsCosts;
            parameters[68].Value = model.PRIIndividualsCosts;
            parameters[69].Value = model.socialInsuranceCompany;
            parameters[70].Value = model.CommonName;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        public void Update(SnapshotsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sep_Snapshots set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("Code=@Code,");
            strSql.Append("TypeID=@TypeID,");
            strSql.Append("MaritalStatus=@MaritalStatus,");
            strSql.Append("Gender=@Gender,");
            strSql.Append("Birthday=@Birthday,");
            strSql.Append("Degree=@Degree,");
            strSql.Append("Education=@Education,");
            strSql.Append("GraduatedFrom=@GraduatedFrom,");
            strSql.Append("Major=@Major,");
            strSql.Append("ThisYearSalary=@ThisYearSalary,");
            strSql.Append("Status=@Status,");
            strSql.Append("nowBasePay=@nowBasePay,");
            strSql.Append("nowMeritPay=@nowMeritPay,");
            strSql.Append("newBasePay=@newBasePay,");
            strSql.Append("newMeritPay=@newMeritPay,");
            strSql.Append("Creator=@Creator,");
            strSql.Append("CreatedTime=@CreatedTime,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("CreatorName=@CreatorName,");
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
            strSql.Append("contractStartDate=@contractStartDate,");
            strSql.Append("contractEndDate=@contractEndDate,");
            strSql.Append("probationEndDate=@probationEndDate,");
            strSql.Append("socialInsuranceBase=@socialInsuranceBase,");
            strSql.Append("medicalInsuranceBase=@medicalInsuranceBase,");
            strSql.Append("publicReserveFundsBase=@publicReserveFundsBase,");
            strSql.Append("isArchive=@isArchive,");
            strSql.Append("contractSignInfo=@contractSignInfo,");
            strSql.Append("InsurancePlace=@InsurancePlace,");
            strSql.Append("IsForeign=@IsForeign,");
            strSql.Append("IsCertification=@IsCertification,");
            strSql.Append("WageMonths=@WageMonths,");
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
            strSql.Append("socialInsuranceCompany=@socialInsuranceCompany, ");
            strSql.Append("CommonName=@CommonName ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Code", SqlDbType.NVarChar,128),
					new SqlParameter("@TypeID", SqlDbType.Int,4),
					new SqlParameter("@MaritalStatus", SqlDbType.Int,4),
					new SqlParameter("@Gender", SqlDbType.Int,4),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@Degree", SqlDbType.NVarChar,32),
					new SqlParameter("@Education", SqlDbType.NVarChar,32),
					new SqlParameter("@GraduatedFrom", SqlDbType.NVarChar,256),
					new SqlParameter("@Major", SqlDbType.NVarChar,256),
					new SqlParameter("@ThisYearSalary", SqlDbType.NVarChar,100),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@nowBasePay", SqlDbType.NVarChar,200),
					new SqlParameter("@nowMeritPay", SqlDbType.NVarChar,200),
					new SqlParameter("@newBasePay", SqlDbType.NVarChar,200),
					new SqlParameter("@newMeritPay", SqlDbType.NVarChar,200),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreatedTime", SqlDbType.DateTime),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorName", SqlDbType.NVarChar,50),
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
					new SqlParameter("@contractStartDate", SqlDbType.SmallDateTime),
					new SqlParameter("@contractEndDate", SqlDbType.SmallDateTime),
					new SqlParameter("@probationEndDate", SqlDbType.SmallDateTime),
					new SqlParameter("@socialInsuranceBase", SqlDbType.NVarChar,200),
					new SqlParameter("@medicalInsuranceBase", SqlDbType.NVarChar,200),
					new SqlParameter("@publicReserveFundsBase", SqlDbType.NVarChar,200),
					new SqlParameter("@isArchive", SqlDbType.Bit,1),
					new SqlParameter("@contractSignInfo", SqlDbType.NVarChar,50),
					new SqlParameter("@InsurancePlace", SqlDbType.NVarChar,50),
					new SqlParameter("@IsForeign", SqlDbType.Bit,1),
					new SqlParameter("@IsCertification", SqlDbType.Bit,1),
					new SqlParameter("@WageMonths", SqlDbType.Int,4),
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
                    new SqlParameter("@socialInsuranceCompany",SqlDbType.NVarChar,200),
                    new SqlParameter("@CommonName",SqlDbType.NVarChar,255)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.Code;
            parameters[3].Value = model.TypeID;
            parameters[4].Value = model.MaritalStatus;
            parameters[5].Value = model.Gender;
            parameters[6].Value = model.Birthday;
            parameters[7].Value = model.Degree;
            parameters[8].Value = model.Education;
            parameters[9].Value = model.GraduatedFrom;
            parameters[10].Value = model.Major;
            parameters[11].Value = model.ThisYearSalary;
            parameters[12].Value = model.Status;
            parameters[13].Value = model.nowBasePay;
            parameters[14].Value = model.nowMeritPay;
            parameters[15].Value = model.newBasePay;
            parameters[16].Value = model.newMeritPay;
            parameters[17].Value = model.Creator;
            parameters[18].Value = model.CreatedTime;
            parameters[19].Value = model.UserName;
            parameters[20].Value = model.CreatorName;
            parameters[21].Value = model.endowmentInsuranceStarTime;
            parameters[22].Value = model.endowmentInsuranceEndTime;
            parameters[23].Value = model.unemploymentInsuranceStarTime;
            parameters[24].Value = model.unemploymentInsuranceEndTime;
            parameters[25].Value = model.birthInsuranceStarTime;
            parameters[26].Value = model.birthInsuranceEndTime;
            parameters[27].Value = model.compoInsuranceStarTime;
            parameters[28].Value = model.compoInsuranceEndTime;
            parameters[29].Value = model.medicalInsuranceStarTime;
            parameters[30].Value = model.medicalInsuranceEndTime;
            parameters[31].Value = model.publicReserveFundsStarTime;
            parameters[32].Value = model.publicReserveFundsEndTime;
            parameters[33].Value = model.contractStartDate;
            parameters[34].Value = model.contractEndDate;
            parameters[35].Value = model.probationEndDate;
            parameters[36].Value = model.socialInsuranceBase;
            parameters[37].Value = model.medicalInsuranceBase;
            parameters[38].Value = model.publicReserveFundsBase;
            parameters[39].Value = model.isArchive;
            parameters[40].Value = model.contractSignInfo;
            parameters[41].Value = model.InsurancePlace;
            parameters[42].Value = model.IsForeign;
            parameters[43].Value = model.IsCertification;
            parameters[44].Value = model.WageMonths;
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
            parameters[70].Value = model.socialInsuranceCompany;
            parameters[71].Value = model.CommonName;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete sep_Snapshots ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        ///

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SnapshotsInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sep_Snapshots(");
            strSql.Append("UserID,Code,TypeID,MaritalStatus,Gender,Birthday,Degree,Education,GraduatedFrom,Major,ThisYearSalary,Status,nowBasePay,nowMeritPay,newBasePay,newMeritPay,Creator,CreatedTime,UserName,CreatorName,endowmentInsuranceStarTime,endowmentInsuranceEndTime,unemploymentInsuranceStarTime,unemploymentInsuranceEndTime,birthInsuranceStarTime,birthInsuranceEndTime,compoInsuranceStarTime,compoInsuranceEndTime,medicalInsuranceStarTime,medicalInsuranceEndTime,publicReserveFundsStarTime,publicReserveFundsEndTime,contractStartDate,contractEndDate,probationEndDate,socialInsuranceBase,medicalInsuranceBase,publicReserveFundsBase,isArchive,contractSignInfo,InsurancePlace,IsForeign,IsCertification,WageMonths,EIProportionOfFirms,EIProportionOfIndividuals,UIProportionOfFirms,UIProportionOfIndividuals,BIProportionOfFirms,BIProportionOfIndividuals,CIProportionOfFirms,CIProportionOfIndividuals,MIProportionOfFirms,MIProportionOfIndividuals,MIBigProportionOfIndividuals,PRFProportionOfFirms,PRFProportionOfIndividuals,EIFirmsCosts,EIIndividualsCosts,UIFirmsCosts,UIIndividualsCosts,BIFirmsCosts,BIIndividualsCosts,CIFirmsCosts,CIIndividualsCosts,MIFirmsCosts,MIIndividualsCosts,PRFirmsCosts,PRIIndividualsCosts,socialInsuranceCompany,CommonName)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@Code,@TypeID,@MaritalStatus,@Gender,@Birthday,@Degree,@Education,@GraduatedFrom,@Major,@ThisYearSalary,@Status,@nowBasePay,@nowMeritPay,@newBasePay,@newMeritPay,@Creator,@CreatedTime,@UserName,@CreatorName,@endowmentInsuranceStarTime,@endowmentInsuranceEndTime,@unemploymentInsuranceStarTime,@unemploymentInsuranceEndTime,@birthInsuranceStarTime,@birthInsuranceEndTime,@compoInsuranceStarTime,@compoInsuranceEndTime,@medicalInsuranceStarTime,@medicalInsuranceEndTime,@publicReserveFundsStarTime,@publicReserveFundsEndTime,@contractStartDate,@contractEndDate,@probationEndDate,@socialInsuranceBase,@medicalInsuranceBase,@publicReserveFundsBase,@isArchive,@contractSignInfo,@InsurancePlace,@IsForeign,@IsCertification,@WageMonths,@EIProportionOfFirms,@EIProportionOfIndividuals,@UIProportionOfFirms,@UIProportionOfIndividuals,@BIProportionOfFirms,@BIProportionOfIndividuals,@CIProportionOfFirms,@CIProportionOfIndividuals,@MIProportionOfFirms,@MIProportionOfIndividuals,@MIBigProportionOfIndividuals,@PRFProportionOfFirms,@PRFProportionOfIndividuals,@EIFirmsCosts,@EIIndividualsCosts,@UIFirmsCosts,@UIIndividualsCosts,@BIFirmsCosts,@BIIndividualsCosts,@CIFirmsCosts,@CIIndividualsCosts,@MIFirmsCosts,@MIIndividualsCosts,@PRFirmsCosts,@PRIIndividualsCosts,@socialInsuranceCompany,@CommonName)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Code", SqlDbType.NVarChar,128),
					new SqlParameter("@TypeID", SqlDbType.Int,4),
					new SqlParameter("@MaritalStatus", SqlDbType.Int,4),
					new SqlParameter("@Gender", SqlDbType.Int,4),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@Degree", SqlDbType.NVarChar,32),
					new SqlParameter("@Education", SqlDbType.NVarChar,32),
					new SqlParameter("@GraduatedFrom", SqlDbType.NVarChar,256),
					new SqlParameter("@Major", SqlDbType.NVarChar,256),
					new SqlParameter("@ThisYearSalary", SqlDbType.NVarChar,100),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@nowBasePay", SqlDbType.NVarChar,200),
					new SqlParameter("@nowMeritPay", SqlDbType.NVarChar,200),
					new SqlParameter("@newBasePay", SqlDbType.NVarChar,200),
					new SqlParameter("@newMeritPay", SqlDbType.NVarChar,200),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreatedTime", SqlDbType.DateTime),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorName", SqlDbType.NVarChar,50),
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
					new SqlParameter("@contractStartDate", SqlDbType.SmallDateTime),
					new SqlParameter("@contractEndDate", SqlDbType.SmallDateTime),
					new SqlParameter("@probationEndDate", SqlDbType.SmallDateTime),
					new SqlParameter("@socialInsuranceBase", SqlDbType.NVarChar,200),
					new SqlParameter("@medicalInsuranceBase", SqlDbType.NVarChar,200),
					new SqlParameter("@publicReserveFundsBase", SqlDbType.NVarChar,200),
					new SqlParameter("@isArchive", SqlDbType.Bit,1),
					new SqlParameter("@contractSignInfo", SqlDbType.NVarChar,50),
					new SqlParameter("@InsurancePlace", SqlDbType.NVarChar,50),
					new SqlParameter("@IsForeign", SqlDbType.Bit,1),
					new SqlParameter("@IsCertification", SqlDbType.Bit,1),
					new SqlParameter("@WageMonths", SqlDbType.Int,4),
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
                    new SqlParameter("@socialInsuranceCompany",SqlDbType.NVarChar,200),
                    new SqlParameter("@CommonName",SqlDbType.NVarChar,255)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.Code;
            parameters[2].Value = model.TypeID;
            parameters[3].Value = model.MaritalStatus;
            parameters[4].Value = model.Gender;
            parameters[5].Value = model.Birthday;
            parameters[6].Value = model.Degree;
            parameters[7].Value = model.Education;
            parameters[8].Value = model.GraduatedFrom;
            parameters[9].Value = model.Major;
            parameters[10].Value = model.ThisYearSalary;
            parameters[11].Value = model.Status;
            parameters[12].Value = model.nowBasePay;
            parameters[13].Value = model.nowMeritPay;
            parameters[14].Value = model.newBasePay;
            parameters[15].Value = model.newMeritPay;
            parameters[16].Value = model.Creator;
            parameters[17].Value = model.CreatedTime;
            parameters[18].Value = model.UserName;
            parameters[19].Value = model.CreatorName;
            parameters[20].Value = model.endowmentInsuranceStarTime;
            parameters[21].Value = model.endowmentInsuranceEndTime;
            parameters[22].Value = model.unemploymentInsuranceStarTime;
            parameters[23].Value = model.unemploymentInsuranceEndTime;
            parameters[24].Value = model.birthInsuranceStarTime;
            parameters[25].Value = model.birthInsuranceEndTime;
            parameters[26].Value = model.compoInsuranceStarTime;
            parameters[27].Value = model.compoInsuranceEndTime;
            parameters[28].Value = model.medicalInsuranceStarTime;
            parameters[29].Value = model.medicalInsuranceEndTime;
            parameters[30].Value = model.publicReserveFundsStarTime;
            parameters[31].Value = model.publicReserveFundsEndTime;
            parameters[32].Value = model.contractStartDate;
            parameters[33].Value = model.contractEndDate;
            parameters[34].Value = model.probationEndDate;
            parameters[35].Value = model.socialInsuranceBase;
            parameters[36].Value = model.medicalInsuranceBase;
            parameters[37].Value = model.publicReserveFundsBase;
            parameters[38].Value = model.isArchive;
            parameters[39].Value = model.contractSignInfo;
            parameters[40].Value = model.InsurancePlace;
            parameters[41].Value = model.IsForeign;
            parameters[42].Value = model.IsCertification;
            parameters[43].Value = model.WageMonths;
            parameters[44].Value = model.EIProportionOfFirms;
            parameters[45].Value = model.EIProportionOfIndividuals;
            parameters[46].Value = model.UIProportionOfFirms;
            parameters[47].Value = model.UIProportionOfIndividuals;
            parameters[48].Value = model.BIProportionOfFirms;
            parameters[49].Value = model.BIProportionOfIndividuals;
            parameters[50].Value = model.CIProportionOfFirms;
            parameters[51].Value = model.CIProportionOfIndividuals;
            parameters[52].Value = model.MIProportionOfFirms;
            parameters[53].Value = model.MIProportionOfIndividuals;
            parameters[54].Value = model.MIBigProportionOfIndividuals;
            parameters[55].Value = model.PRFProportionOfFirms;
            parameters[56].Value = model.PRFProportionOfIndividuals;
            parameters[57].Value = model.EIFirmsCosts;
            parameters[58].Value = model.EIIndividualsCosts;
            parameters[59].Value = model.UIFirmsCosts;
            parameters[60].Value = model.UIIndividualsCosts;
            parameters[61].Value = model.BIFirmsCosts;
            parameters[62].Value = model.BIIndividualsCosts;
            parameters[63].Value = model.CIFirmsCosts;
            parameters[64].Value = model.CIIndividualsCosts;
            parameters[65].Value = model.MIFirmsCosts;
            parameters[66].Value = model.MIIndividualsCosts;
            parameters[67].Value = model.PRFirmsCosts;
            parameters[68].Value = model.PRIIndividualsCosts;
            parameters[69].Value = model.socialInsuranceCompany;
            parameters[70].Value = model.CommonName;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), trans.Connection, trans, parameters);
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
        public void Update(SnapshotsInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sep_Snapshots set ");
            strSql.Append("UserID=@UserID,");
            strSql.Append("Code=@Code,");
            strSql.Append("TypeID=@TypeID,");
            strSql.Append("MaritalStatus=@MaritalStatus,");
            strSql.Append("Gender=@Gender,");
            strSql.Append("Birthday=@Birthday,");
            strSql.Append("Degree=@Degree,");
            strSql.Append("Education=@Education,");
            strSql.Append("GraduatedFrom=@GraduatedFrom,");
            strSql.Append("Major=@Major,");
            strSql.Append("ThisYearSalary=@ThisYearSalary,");
            strSql.Append("Status=@Status,");
            strSql.Append("nowBasePay=@nowBasePay,");
            strSql.Append("nowMeritPay=@nowMeritPay,");
            strSql.Append("newBasePay=@newBasePay,");
            strSql.Append("newMeritPay=@newMeritPay,");
            strSql.Append("Creator=@Creator,");
            strSql.Append("CreatedTime=@CreatedTime,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("CreatorName=@CreatorName,");
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
            strSql.Append("contractStartDate=@contractStartDate,");
            strSql.Append("contractEndDate=@contractEndDate,");
            strSql.Append("probationEndDate=@probationEndDate,");
            strSql.Append("socialInsuranceBase=@socialInsuranceBase,");
            strSql.Append("medicalInsuranceBase=@medicalInsuranceBase,");
            strSql.Append("publicReserveFundsBase=@publicReserveFundsBase,");
            strSql.Append("isArchive=@isArchive,");
            strSql.Append("contractSignInfo=@contractSignInfo,");
            strSql.Append("InsurancePlace=@InsurancePlace,");
            strSql.Append("IsForeign=@IsForeign,");
            strSql.Append("IsCertification=@IsCertification,");
            strSql.Append("WageMonths=@WageMonths,");
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
            strSql.Append("socialInsuranceCompany=@socialInsuranceCompany, ");
            strSql.Append("CommonName=@CommonName ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Code", SqlDbType.NVarChar,128),
					new SqlParameter("@TypeID", SqlDbType.Int,4),
					new SqlParameter("@MaritalStatus", SqlDbType.Int,4),
					new SqlParameter("@Gender", SqlDbType.Int,4),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@Degree", SqlDbType.NVarChar,32),
					new SqlParameter("@Education", SqlDbType.NVarChar,32),
					new SqlParameter("@GraduatedFrom", SqlDbType.NVarChar,256),
					new SqlParameter("@Major", SqlDbType.NVarChar,256),
					new SqlParameter("@ThisYearSalary", SqlDbType.NVarChar,100),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@nowBasePay", SqlDbType.NVarChar,200),
					new SqlParameter("@nowMeritPay", SqlDbType.NVarChar,200),
					new SqlParameter("@newBasePay", SqlDbType.NVarChar,200),
					new SqlParameter("@newMeritPay", SqlDbType.NVarChar,200),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreatedTime", SqlDbType.DateTime),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorName", SqlDbType.NVarChar,50),
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
					new SqlParameter("@contractStartDate", SqlDbType.SmallDateTime),
					new SqlParameter("@contractEndDate", SqlDbType.SmallDateTime),
					new SqlParameter("@probationEndDate", SqlDbType.SmallDateTime),
					new SqlParameter("@socialInsuranceBase", SqlDbType.NVarChar,200),
					new SqlParameter("@medicalInsuranceBase", SqlDbType.NVarChar,200),
					new SqlParameter("@publicReserveFundsBase", SqlDbType.NVarChar,200),
					new SqlParameter("@isArchive", SqlDbType.Bit,1),
					new SqlParameter("@contractSignInfo", SqlDbType.NVarChar,50),
					new SqlParameter("@InsurancePlace", SqlDbType.NVarChar,50),
					new SqlParameter("@IsForeign", SqlDbType.Bit,1),
					new SqlParameter("@IsCertification", SqlDbType.Bit,1),
					new SqlParameter("@WageMonths", SqlDbType.Int,4),
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
                    new SqlParameter("@socialInsuranceCompany",SqlDbType.NVarChar,200),
                    new SqlParameter("@CommonName",SqlDbType.NVarChar,255)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.Code;
            parameters[3].Value = model.TypeID;
            parameters[4].Value = model.MaritalStatus;
            parameters[5].Value = model.Gender;
            parameters[6].Value = model.Birthday;
            parameters[7].Value = model.Degree;
            parameters[8].Value = model.Education;
            parameters[9].Value = model.GraduatedFrom;
            parameters[10].Value = model.Major;
            parameters[11].Value = model.ThisYearSalary;
            parameters[12].Value = model.Status;
            parameters[13].Value = model.nowBasePay;
            parameters[14].Value = model.nowMeritPay;
            parameters[15].Value = model.newBasePay;
            parameters[16].Value = model.newMeritPay;
            parameters[17].Value = model.Creator;
            parameters[18].Value = model.CreatedTime;
            parameters[19].Value = model.UserName;
            parameters[20].Value = model.CreatorName;
            parameters[21].Value = model.endowmentInsuranceStarTime;
            parameters[22].Value = model.endowmentInsuranceEndTime;
            parameters[23].Value = model.unemploymentInsuranceStarTime;
            parameters[24].Value = model.unemploymentInsuranceEndTime;
            parameters[25].Value = model.birthInsuranceStarTime;
            parameters[26].Value = model.birthInsuranceEndTime;
            parameters[27].Value = model.compoInsuranceStarTime;
            parameters[28].Value = model.compoInsuranceEndTime;
            parameters[29].Value = model.medicalInsuranceStarTime;
            parameters[30].Value = model.medicalInsuranceEndTime;
            parameters[31].Value = model.publicReserveFundsStarTime;
            parameters[32].Value = model.publicReserveFundsEndTime;
            parameters[33].Value = model.contractStartDate;
            parameters[34].Value = model.contractEndDate;
            parameters[35].Value = model.probationEndDate;
            parameters[36].Value = model.socialInsuranceBase;
            parameters[37].Value = model.medicalInsuranceBase;
            parameters[38].Value = model.publicReserveFundsBase;
            parameters[39].Value = model.isArchive;
            parameters[40].Value = model.contractSignInfo;
            parameters[41].Value = model.InsurancePlace;
            parameters[42].Value = model.IsForeign;
            parameters[43].Value = model.IsCertification;
            parameters[44].Value = model.WageMonths;
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
            parameters[70].Value = model.socialInsuranceCompany;
            parameters[71].Value = model.CommonName;
            DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int id, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete sep_Snapshots ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), trans.Connection, trans, parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SnapshotsInfo GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,UserID,Code,TypeID,MaritalStatus,Gender,Birthday,Degree,Education,GraduatedFrom,Major,ThisYearSalary,Status,nowBasePay,nowMeritPay,newBasePay,newMeritPay,Creator,CreatedTime,UserName,CreatorName,endowmentInsuranceStarTime,endowmentInsuranceEndTime,unemploymentInsuranceStarTime,unemploymentInsuranceEndTime,birthInsuranceStarTime,birthInsuranceEndTime,compoInsuranceStarTime,compoInsuranceEndTime,medicalInsuranceStarTime,medicalInsuranceEndTime,publicReserveFundsStarTime,publicReserveFundsEndTime,contractStartDate,contractEndDate,probationEndDate,socialInsuranceBase,medicalInsuranceBase,publicReserveFundsBase,isArchive,contractSignInfo,InsurancePlace,IsForeign,IsCertification,WageMonths,EIProportionOfFirms,EIProportionOfIndividuals,UIProportionOfFirms,UIProportionOfIndividuals,BIProportionOfFirms,BIProportionOfIndividuals,CIProportionOfFirms,CIProportionOfIndividuals,MIProportionOfFirms,MIProportionOfIndividuals,MIBigProportionOfIndividuals,PRFProportionOfFirms,PRFProportionOfIndividuals,EIFirmsCosts,EIIndividualsCosts,UIFirmsCosts,UIIndividualsCosts,BIFirmsCosts,BIIndividualsCosts,CIFirmsCosts,CIIndividualsCosts,MIFirmsCosts,MIIndividualsCosts,PRFirmsCosts,PRIIndividualsCosts,socialInsuranceCompany,CommonName from sep_Snapshots ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            SnapshotsInfo model = new SnapshotsInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                model.Code = ds.Tables[0].Rows[0]["Code"].ToString();
                if (ds.Tables[0].Rows[0]["TypeID"].ToString() != "")
                {
                    model.TypeID = int.Parse(ds.Tables[0].Rows[0]["TypeID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MaritalStatus"].ToString() != "")
                    {
                        model.MaritalStatus = int.Parse(ds.Tables[0].Rows[0]["MaritalStatus"].ToString());
                    }
                    
                        model.socialInsuranceBase = (ds.Tables[0].Rows[0]["socialInsuranceBase"].ToString());
                    
                        model.medicalInsuranceBase =(ds.Tables[0].Rows[0]["medicalInsuranceBase"].ToString());
                    
                        model.publicReserveFundsBase = (ds.Tables[0].Rows[0]["publicReserveFundsBase"].ToString());
                    
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
                if (ds.Tables[0].Rows[0]["Birthday"].ToString() != "")
                {
                    model.Birthday = DateTime.Parse(ds.Tables[0].Rows[0]["Birthday"].ToString());
                }
                model.Degree = ds.Tables[0].Rows[0]["Degree"].ToString();
                model.Education = ds.Tables[0].Rows[0]["Education"].ToString();
                model.GraduatedFrom = ds.Tables[0].Rows[0]["GraduatedFrom"].ToString();
                model.Major = ds.Tables[0].Rows[0]["Major"].ToString();
                model.ThisYearSalary = ds.Tables[0].Rows[0]["ThisYearSalary"].ToString();
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                model.nowBasePay = ds.Tables[0].Rows[0]["nowBasePay"].ToString();
                model.nowMeritPay = ds.Tables[0].Rows[0]["nowMeritPay"].ToString();
                model.newBasePay = ds.Tables[0].Rows[0]["newBasePay"].ToString();
                model.newMeritPay = ds.Tables[0].Rows[0]["newMeritPay"].ToString();
                if (ds.Tables[0].Rows[0]["Creator"].ToString() != "")
                {
                    model.Creator = int.Parse(ds.Tables[0].Rows[0]["Creator"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedTime"].ToString() != "")
                {
                    model.CreatedTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedTime"].ToString());
                }
                model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                model.CreatorName = ds.Tables[0].Rows[0]["CreatorName"].ToString();
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
                if (ds.Tables[0].Rows[0]["contractStartDate"].ToString() != "")
                {
                    model.contractStartDate = DateTime.Parse(ds.Tables[0].Rows[0]["contractStartDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["contractEndDate"].ToString() != "")
                {
                    model.contractEndDate = DateTime.Parse(ds.Tables[0].Rows[0]["contractEndDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["probationEndDate"].ToString() != "")
                {
                    model.probationEndDate = DateTime.Parse(ds.Tables[0].Rows[0]["probationEndDate"].ToString());
                }
                model.socialInsuranceBase = ds.Tables[0].Rows[0]["socialInsuranceBase"].ToString();
                model.medicalInsuranceBase = ds.Tables[0].Rows[0]["medicalInsuranceBase"].ToString();
                model.publicReserveFundsBase = ds.Tables[0].Rows[0]["publicReserveFundsBase"].ToString();
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
                model.contractSignInfo = ds.Tables[0].Rows[0]["contractSignInfo"].ToString();
                model.InsurancePlace = ds.Tables[0].Rows[0]["InsurancePlace"].ToString();
                if (ds.Tables[0].Rows[0]["IsForeign"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsForeign"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsForeign"].ToString().ToLower() == "true"))
                    {
                        model.IsForeign = true;
                    }
                    else
                    {
                        model.IsForeign = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["IsCertification"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsCertification"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsCertification"].ToString().ToLower() == "true"))
                    {
                        model.IsCertification = true;
                    }
                    else
                    {
                        model.IsCertification = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["WageMonths"].ToString() != "")
                {
                    model.WageMonths = int.Parse(ds.Tables[0].Rows[0]["WageMonths"].ToString());
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
                model.socialInsuranceCompany = ds.Tables[0].Rows[0]["socialInsuranceCompany"].ToString();
                model.CommonName = ds.Tables[0].Rows[0]["CommonName"].ToString();
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
            strSql.Append(" FROM sep_Snapshots ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得最新的薪资对象
        /// </summary>
        /// <returns></returns>
        public SnapshotsInfo GetTopModel(int sysId)
        {
            string strSql = "select top 1 * from SEP_Snapshots where UserID=" + sysId;
            strSql += " order by id desc";
            SnapshotsInfo model = null;
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql))
            {
                while (r.Read())
                {
                    model = new SnapshotsInfo();
                    model.PopupData(r);
                }
                r.Close();
            }
            return model;
        }

        /// <summary>
        /// 获得最新的薪资对象
        /// </summary>
        /// <returns></returns>
        public SnapshotsInfo GetTopModel(int sysId,string sqlWhere)
        {
            string strSql = "select top 1 * from SEP_Snapshots where UserID=" + sysId;
            if (sqlWhere.Trim() != "")
            {
                strSql +=  sqlWhere;
            }
            strSql += " order by id desc";
            SnapshotsInfo model = null;
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql))
            {
                while (r.Read())
                {
                    model = new SnapshotsInfo();
                    model.PopupData(r);
                }
                r.Close();
            }
            return model;
        }

        /// <summary>
        /// 获得最新的薪资对象
        /// </summary>
        /// <returns></returns>
        public SnapshotsInfo GetTopModel(string UserCode)
        {
            string strSql = "select top 1 * from SEP_Snapshots where Code='" + UserCode + "'";
            strSql += " order by id desc";
            SnapshotsInfo model = null;
            using (SqlDataReader r = DbHelperSQL.ExecuteReader(strSql))
            {
                while (r.Read())
                {
                    model = new SnapshotsInfo();
                    model.PopupData(r);
                }
                r.Close();
            }
            return model;
        }

        #endregion  成员方法
    }
}
