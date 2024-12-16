using System;
using System.Collections.Generic;
using ESP.HumanResource.BusinessLogic;
using System.Web;
using System.Data.SqlClient;
using ESP.HumanResource.Common;
using System.Data;
using System.Text;
using ESP.HumanResource.Entity;

namespace ESP.HumanResource.DataAccess
{
    public class EmployeeBaseDataProvider
    {
        public EmployeeBaseDataProvider()
        { }
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(EmployeeBaseInfo model, SqlTransaction stran)
        {


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sep_Employees(");
            strSql.Append("UserID,IM,EmergencyContact,EmergencyContactPhone,Address,City,Province,Country,PostCode,Adress2,City2,Code,Province2,Country2,PostCode2,WorkAddress,");
            strSql.Append("WorkCity,WorkProvince,WorkCountry,WorkPostCode,MaritalStatus,Gender,TypeID,Birthday,BirthPlace,DomicilePlace,IDNumber,Photo,Degree,Education,GraduatedFrom,");
            strSql.Append("Major,GraduatedDate,Phone1,Health,DiseaseInSixMonths,DiseaseInSixMonthsInfo,WorkExperience,WorkSpecialty,ThisYearSalary,Status,Memo,BaseInfoOK,ContractInfoOK,Phone2,");
            strSql.Append("InsuranceInfoOK,ArchiveInfoOK,Creator,CreatedTime,LastModifier,LastModifiedTime,MobilePhone,HomePhone,Fax,InternalEmail,Resume,IsForeign,IsCertification,WageMonths,IPCode,");
            strSql.Append("IsSendMail,CommonName,DimissionStatus,PrivateEmail,OwnedPC,OfferLetterTemplate,OfferLetterSender,OfferLetterSendTime,IsExamen,Seniority,Pay,Performance,Residence,MateName,AdrressNow,");
            strSql.Append("PostCodeNow,FamillyDesc,Political,Nation,HasChild,EmpProperty,Appearance,Quality,Know,Equal,Motivation,FourD,EQ,workBegin,SocialSecurityType,JoinDate,BranchCode,ContractYear,ContractBeginDate,");
            strSql.Append("ContractEndDate,ContractSignDate,AnnualLeaveBase,ProbationDate,FirstContractBeginDate,FirstContractEndDate,SalaryBank,SalaryCardNo,Attendance,IDValid)");
            strSql.Append(" values (");
            strSql.Append("@UserID,@IM,@EmergencyContact,@EmergencyContactPhone,@Address,@City,@Province,@Country,@PostCode,@Adress2,@City2,@Code,@Province2,@Country2,@PostCode2,@WorkAddress,@WorkCity,@WorkProvince,@WorkCountry,");
            strSql.Append("@WorkPostCode,@MaritalStatus,@Gender,@TypeID,@Birthday,@BirthPlace,@DomicilePlace,@IDNumber,@Photo,@Degree,@Education,@GraduatedFrom,@Major,@GraduatedDate,@Phone1,@Health,@DiseaseInSixMonths,@DiseaseInSixMonthsInfo,");
            strSql.Append("@WorkExperience,@WorkSpecialty,@ThisYearSalary,@Status,@Memo,@BaseInfoOK,@ContractInfoOK,@Phone2,@InsuranceInfoOK,@ArchiveInfoOK,@Creator,@CreatedTime,@LastModifier,@LastModifiedTime,@MobilePhone,@HomePhone,@Fax,@InternalEmail,");
            strSql.Append("@Resume,@IsForeign,@IsCertification,@WageMonths,@IPCode,@IsSendMail,@CommonName,@DimissionStatus,@PrivateEmail,@OwnedPC,@OfferLetterTemplate,@OfferLetterSender,@OfferLetterSendTime,@IsExamen,@Seniority,@Pay,@Performance,@Residence,");
            strSql.Append("@MateName,@AdrressNow,@PostCodeNow,@FamillyDesc,@Political,@Nation,@HasChild,@EmpProperty,@Appearance,@Quality,@Know,@Equal,@Motivation,@FourD,@EQ,@workBegin,@SocialSecurityType,@JoinDate,@BranchCode,@ContractYear,@ContractBeginDate,");
            strSql.Append("@ContractEndDate,@ContractSignDate,@AnnualLeaveBase,@ProbationDate,@FirstContractBeginDate,@FirstContractEndDate,@SalaryBank,@SalaryCardNo,@Attendance,@IDValid)");

            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@IM", SqlDbType.NVarChar,256),
					new SqlParameter("@EmergencyContact", SqlDbType.NVarChar,256),
					new SqlParameter("@EmergencyContactPhone", SqlDbType.NVarChar,32),
					new SqlParameter("@Address", SqlDbType.NVarChar,256),
					new SqlParameter("@City", SqlDbType.NVarChar,64),
					new SqlParameter("@Province", SqlDbType.NVarChar,64),
					new SqlParameter("@Country", SqlDbType.NVarChar,64),
					new SqlParameter("@PostCode", SqlDbType.NVarChar,32),
					new SqlParameter("@Adress2", SqlDbType.NVarChar,256),
					new SqlParameter("@City2", SqlDbType.NVarChar,64),
					new SqlParameter("@Code", SqlDbType.NVarChar,128),
					new SqlParameter("@Province2", SqlDbType.NVarChar,64),
					new SqlParameter("@Country2", SqlDbType.NVarChar,64),
					new SqlParameter("@PostCode2", SqlDbType.NVarChar,32),
					new SqlParameter("@WorkAddress", SqlDbType.NVarChar,256),
					new SqlParameter("@WorkCity", SqlDbType.NVarChar,64),
					new SqlParameter("@WorkProvince", SqlDbType.NVarChar,64),
					new SqlParameter("@WorkCountry", SqlDbType.NVarChar,64),
					new SqlParameter("@WorkPostCode", SqlDbType.NVarChar,32),
					new SqlParameter("@MaritalStatus", SqlDbType.Int,4),
					new SqlParameter("@Gender", SqlDbType.Int,4),
					new SqlParameter("@TypeID", SqlDbType.Int,4),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@BirthPlace", SqlDbType.NVarChar,256),
					new SqlParameter("@DomicilePlace", SqlDbType.NVarChar,256),
					new SqlParameter("@IDNumber", SqlDbType.NVarChar,64),
					new SqlParameter("@Photo", SqlDbType.NVarChar,256),
					new SqlParameter("@Degree", SqlDbType.NVarChar,32),
					new SqlParameter("@Education", SqlDbType.NVarChar,32),
					new SqlParameter("@GraduatedFrom", SqlDbType.NVarChar,256),
					new SqlParameter("@Major", SqlDbType.NVarChar,256),
					new SqlParameter("@GraduatedDate", SqlDbType.DateTime),
					new SqlParameter("@Phone1", SqlDbType.NVarChar,32),
					new SqlParameter("@Health", SqlDbType.NVarChar,256),
					new SqlParameter("@DiseaseInSixMonths", SqlDbType.NVarChar,4),
					new SqlParameter("@DiseaseInSixMonthsInfo", SqlDbType.NVarChar,256),
					new SqlParameter("@WorkExperience", SqlDbType.NVarChar,4000),
					new SqlParameter("@WorkSpecialty", SqlDbType.NVarChar,500),
					new SqlParameter("@ThisYearSalary", SqlDbType.NVarChar,100),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Memo", SqlDbType.NVarChar,1000),
					new SqlParameter("@BaseInfoOK", SqlDbType.Bit,1),
					new SqlParameter("@ContractInfoOK", SqlDbType.Bit,1),
					new SqlParameter("@Phone2", SqlDbType.NVarChar,32),
					new SqlParameter("@InsuranceInfoOK", SqlDbType.Bit,1),
					new SqlParameter("@ArchiveInfoOK", SqlDbType.Bit,1),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreatedTime", SqlDbType.DateTime),
					new SqlParameter("@LastModifier", SqlDbType.Int,4),
					new SqlParameter("@LastModifiedTime", SqlDbType.DateTime),
					new SqlParameter("@MobilePhone", SqlDbType.NVarChar,32),
					new SqlParameter("@HomePhone", SqlDbType.NVarChar,32),
					new SqlParameter("@Fax", SqlDbType.NVarChar,32),
					new SqlParameter("@InternalEmail", SqlDbType.NVarChar,256),
                    new SqlParameter("@Resume",SqlDbType.NVarChar,100),
                    new SqlParameter("@IsForeign",SqlDbType.Bit,1),
                    new SqlParameter("@IsCertification",SqlDbType.Bit,1),
                    new SqlParameter("@WageMonths",SqlDbType.Int,4),
                    new SqlParameter("@IPCode", SqlDbType.NVarChar,32),
                    new SqlParameter("@IsSendMail",SqlDbType.Bit,1),
                    new SqlParameter("@CommonName",SqlDbType.NVarChar,255),
                    new SqlParameter("@DimissionStatus",SqlDbType.Int,4),
					new SqlParameter("@PrivateEmail", SqlDbType.NVarChar),
					new SqlParameter("@OwnedPC", SqlDbType.Bit,1),
					new SqlParameter("@OfferLetterTemplate", SqlDbType.Int,4),
                    new SqlParameter("@OfferLetterSender", SqlDbType.Int, 4),
                    new SqlParameter("@OfferLetterSendTime", SqlDbType.DateTime),
                    new SqlParameter("@IsExamen", SqlDbType.Bit, 1),
                    new SqlParameter("@Seniority", SqlDbType.Int, 4),
                    new SqlParameter("@Pay", SqlDbType.Decimal,5),
                    new SqlParameter("@Performance", SqlDbType.Decimal,5),
                    new SqlParameter("@Residence", SqlDbType.NVarChar,50),
                    new SqlParameter("@MateName", SqlDbType.NVarChar,50),
                    new SqlParameter("@AdrressNow", SqlDbType.NVarChar,500),
                    new SqlParameter("@PostCodeNow", SqlDbType.NVarChar,50),
                    new SqlParameter("@FamillyDesc", SqlDbType.NVarChar,500),
                      new SqlParameter("@Political", SqlDbType.NVarChar,50),
                        new SqlParameter("@Nation", SqlDbType.NVarChar,50),
                          new SqlParameter("@HasChild", SqlDbType.Bit),
                           new SqlParameter("@EmpProperty", SqlDbType.NVarChar,50),
                           new SqlParameter("@Appearance", SqlDbType.NVarChar,50),
                           new SqlParameter("@Quality", SqlDbType.NVarChar,50),
                           new SqlParameter("@Know", SqlDbType.NVarChar,50),
                           new SqlParameter("@Equal", SqlDbType.NVarChar,50),
                           new SqlParameter("@Motivation", SqlDbType.NVarChar,50),
                           new SqlParameter("@FourD", SqlDbType.NVarChar,50),
                            new SqlParameter("@EQ", SqlDbType.NVarChar,50),
                             new SqlParameter("@workBegin", SqlDbType.DateTime),
                             new SqlParameter("@SocialSecurityType", SqlDbType.Int),
                             new SqlParameter("@JoinDate", SqlDbType.DateTime),
                             new SqlParameter("@BranchCode", SqlDbType.NVarChar),
                             new SqlParameter("@ContractYear", SqlDbType.Int),
                             new SqlParameter("@ContractBeginDate", SqlDbType.DateTime),
                             new SqlParameter("@ContractEndDate", SqlDbType.DateTime),
                             new SqlParameter("@ContractSignDate", SqlDbType.DateTime),
                             new SqlParameter("@AnnualLeaveBase",SqlDbType.Int),
                             new SqlParameter("@ProbationDate", SqlDbType.DateTime),
                             new SqlParameter("@FirstContractBeginDate", SqlDbType.DateTime),
                             new SqlParameter("@FirstContractEndDate", SqlDbType.DateTime),
                             new SqlParameter("@SalaryBank", SqlDbType.NVarChar,100),
                             new SqlParameter("@SalaryCardNo", SqlDbType.NVarChar,50),
                             new SqlParameter("@Attendance",SqlDbType.Decimal,5),
                              new SqlParameter("@IDValid", SqlDbType.DateTime)
                                        };

            parameters[0].Value = model.UserID;
            parameters[1].Value = model.IM;
            parameters[2].Value = model.EmergencyContact;
            parameters[3].Value = model.EmergencyContactPhone;
            parameters[4].Value = model.Address;
            parameters[5].Value = model.City;
            parameters[6].Value = model.Province;
            parameters[7].Value = model.Country;
            parameters[8].Value = model.PostCode;
            parameters[9].Value = model.Address2;
            parameters[10].Value = model.City2;
            parameters[11].Value = model.Code;
            parameters[12].Value = model.Province2;
            parameters[13].Value = model.Country2;
            parameters[14].Value = model.PostCode2;
            parameters[15].Value = model.WorkAddress;
            parameters[16].Value = model.WorkCity;
            parameters[17].Value = model.WorkProvince;
            parameters[18].Value = model.WorkCountry;
            parameters[19].Value = model.WorkPostCode;
            parameters[20].Value = model.MaritalStatus;
            parameters[21].Value = model.Gender;
            parameters[22].Value = model.TypeID;
            parameters[23].Value = model.Birthday;
            parameters[24].Value = model.BirthPlace;
            parameters[25].Value = model.DomicilePlace;
            parameters[26].Value = model.IDNumber;
            parameters[27].Value = model.Photo;
            parameters[28].Value = model.Degree;
            parameters[29].Value = model.Education;
            parameters[30].Value = model.GraduateFrom;
            parameters[31].Value = model.Major;
            parameters[32].Value = model.GraduatedDate;
            parameters[33].Value = model.Phone1;
            parameters[34].Value = model.Health;
            parameters[35].Value = model.DiseaseInSixMonths;
            parameters[36].Value = model.DiseaseInSixMonthsInfo;
            parameters[37].Value = model.WorkExperience;
            parameters[38].Value = model.WorkSpecialty;
            parameters[39].Value = model.ThisYearSalary;
            parameters[40].Value = model.Status;
            parameters[41].Value = model.Memo;
            parameters[42].Value = model.BaseInfoOK;
            parameters[43].Value = model.ContractInfoOK;
            parameters[44].Value = model.Phone2;
            parameters[45].Value = model.InsuranceInfoOK;
            parameters[46].Value = model.ArchiveInfoOK;
            parameters[47].Value = model.Creator;
            parameters[48].Value = model.CreatedTime;
            parameters[49].Value = model.LastModifier;
            parameters[50].Value = model.LastModifiedTime;
            parameters[51].Value = model.MobilePhone;
            parameters[52].Value = model.HomePhone;
            parameters[53].Value = model.Fax;
            parameters[54].Value = model.InternalEmail;
            parameters[55].Value = model.Resume;
            parameters[56].Value = model.IsForeign;
            parameters[57].Value = model.IsCertification;
            parameters[58].Value = model.WageMonths;
            parameters[59].Value = model.IPCode;
            parameters[60].Value = model.IsSendMail;
            parameters[61].Value = model.CommonName;
            parameters[62].Value = model.DimissionStatus;
            parameters[63].Value = model.PrivateEmail;
            parameters[64].Value = model.OwnedPC;
            parameters[65].Value = model.OfferLetterTemplate;
            parameters[66].Value = model.OfferLetterSender;
            parameters[67].Value = model.OfferLetterSendTime;
            parameters[68].Value = model.IsExamen;
            parameters[69].Value = model.Seniority;
            parameters[70].Value = model.Pay;
            parameters[71].Value = model.Performance;
            //Residence,MateName,AdrressNow,PostCodeNow,FamillyDesc
            parameters[72].Value = model.Residence;
            parameters[73].Value = model.MateName;
            parameters[74].Value = model.AdrressNow;
            parameters[75].Value = model.PostCodeNow;
            parameters[76].Value = model.FamillyDesc;
            parameters[77].Value = model.Political;
            parameters[78].Value = model.Nation;
            parameters[79].Value = model.HasChild;
            parameters[80].Value = model.EmpProperty;

            parameters[81].Value = model.Appearance;
            parameters[82].Value = model.Quality;
            parameters[83].Value = model.Know;
            parameters[84].Value = model.Equal;
            parameters[85].Value = model.Motivation;
            parameters[86].Value = model.FourD;
            parameters[87].Value = model.EQ;
            parameters[88].Value = model.WorkBegin;
            parameters[89].Value = model.SocialSecurityType;
            parameters[90].Value = model.JoinDate;
            parameters[91].Value = model.BranchCode;

            parameters[92].Value = model.ContractYear;
            parameters[93].Value = model.ContractBeginDate;
            parameters[94].Value = model.ContractEndDate;
            parameters[95].Value = model.ContractSignDate;
            parameters[96].Value = model.AnnualLeaveBase;
            parameters[97].Value = model.ProbationDate;
            parameters[98].Value = model.FirstContractBeginDate;
            parameters[99].Value = model.FirstContractEndDate;
            parameters[100].Value = model.SalaryBank;
            parameters[101].Value = model.SalaryCardNo;
            parameters[102].Value = model.Attendance;
            parameters[103].Value = model.IDValid;
            DbHelperSQL.ExecuteSql(strSql.ToString(), stran.Connection, stran, parameters);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(EmployeeBaseInfo model, SqlTransaction stran)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sep_Employees set ");
            strSql.Append("IM=@IM,");
            strSql.Append("EmergencyContact=@EmergencyContact,");
            strSql.Append("EmergencyContactPhone=@EmergencyContactPhone,");
            strSql.Append("Address=@Address,");
            strSql.Append("City=@City,");
            strSql.Append("Province=@Province,");
            strSql.Append("Country=@Country,");
            strSql.Append("PostCode=@PostCode,");
            strSql.Append("Adress2=@Adress2,");
            strSql.Append("City2=@City2,");
            strSql.Append("Code=@Code,");
            strSql.Append("Province2=@Province2,");
            strSql.Append("Country2=@Country2,");
            strSql.Append("PostCode2=@PostCode2,");
            strSql.Append("WorkAddress=@WorkAddress,");
            strSql.Append("WorkCity=@WorkCity,");
            strSql.Append("WorkProvince=@WorkProvince,");
            strSql.Append("WorkCountry=@WorkCountry,");
            strSql.Append("WorkPostCode=@WorkPostCode,");
            strSql.Append("MaritalStatus=@MaritalStatus,");
            strSql.Append("Gender=@Gender,");
            strSql.Append("TypeID=@TypeID,");
            strSql.Append("Birthday=@Birthday,");
            strSql.Append("BirthPlace=@BirthPlace,");
            strSql.Append("DomicilePlace=@DomicilePlace,");
            strSql.Append("IDNumber=@IDNumber,");
            strSql.Append("Photo=@Photo,");
            strSql.Append("Degree=@Degree,");
            strSql.Append("Education=@Education,");
            strSql.Append("GraduatedFrom=@GraduatedFrom,");
            strSql.Append("Major=@Major,");
            strSql.Append("GraduatedDate=@GraduatedDate,");
            strSql.Append("Phone1=@Phone1,");
            strSql.Append("Health=@Health,");
            strSql.Append("DiseaseInSixMonths=@DiseaseInSixMonths,");
            strSql.Append("DiseaseInSixMonthsInfo=@DiseaseInSixMonthsInfo,");
            strSql.Append("WorkExperience=@WorkExperience,");
            strSql.Append("WorkSpecialty=@WorkSpecialty,");
            strSql.Append("ThisYearSalary=@ThisYearSalary,");
            strSql.Append("Status=@Status,");
            strSql.Append("Memo=@Memo,");
            strSql.Append("BaseInfoOK=@BaseInfoOK,");
            strSql.Append("ContractInfoOK=@ContractInfoOK,");
            strSql.Append("Phone2=@Phone2,");
            strSql.Append("InsuranceInfoOK=@InsuranceInfoOK,");
            strSql.Append("ArchiveInfoOK=@ArchiveInfoOK,");
            strSql.Append("Creator=@Creator,");
            strSql.Append("CreatedTime=@CreatedTime,");
            strSql.Append("LastModifier=@LastModifier,");
            strSql.Append("LastModifiedTime=@LastModifiedTime,");
            strSql.Append("MobilePhone=@MobilePhone,");
            strSql.Append("HomePhone=@HomePhone,");
            strSql.Append("Fax=@Fax,");
            strSql.Append("InternalEmail=@InternalEmail,");
            strSql.Append("Resume=@Resume, ");
            strSql.Append("IsForeign=@IsForeign,");
            strSql.Append("IsCertification=@IsCertification,");
            strSql.Append("WageMonths=@WageMonths, ");
            strSql.Append("IPCode=@IPCode, ");
            strSql.Append("IsSendMail=@IsSendMail, ");
            strSql.Append("CommonName=@CommonName,DimissionStatus=@DimissionStatus,");
            strSql.Append("PrivateEmail=@PrivateEmail,");
            strSql.Append("OwnedPC=@OwnedPC,");
            strSql.Append("OfferLetterTemplate=@OfferLetterTemplate,");
            strSql.Append("OfferLetterSender=@OfferLetterSender,");
            strSql.Append("OfferLetterSendTime=@OfferLetterSendTime,");
            strSql.Append("IsExamen=@IsExamen, ");
            strSql.Append("Seniority=@Seniority,");
            strSql.Append("Pay=@Pay,");
            strSql.Append("Performance=@Performance,Residence=@Residence,MateName=@MateName,AdrressNow=@AdrressNow,PostCodeNow=@PostCodeNow,");
            strSql.Append("FamillyDesc=@FamillyDesc,Political=@Political,Nation=@Nation,HasChild=@HasChild,EmpProperty=@EmpProperty,");
            strSql.Append("Appearance=@Appearance,Quality=@Quality,Know=@Know,Equal=@Equal,Motivation=@Motivation,FourD=@FourD,EQ=@EQ,");
            strSql.Append("workBegin =@workBegin,SocialSecurityType=@SocialSecurityType,JoinDate=@JoinDate,BranchCode=@BranchCode,");
            strSql.Append("ContractYear=@ContractYear,ContractBeginDate=@ContractBeginDate,ContractEndDate=@ContractEndDate,ContractSignDate=@ContractSignDate,");
            strSql.Append("AnnualLeaveBase=@AnnualLeaveBase,ProbationDate=@ProbationDate,FirstContractBeginDate=@FirstContractBeginDate,FirstContractEndDate=@FirstContractEndDate, ");
            strSql.Append("SalaryBank=@SalaryBank,SalaryCardNo=@SalaryCardNo,Attendance=@Attendance,IDValid=@IDValid ");
            strSql.Append(" WHERE UserID=@UserID ");
            
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@IM", SqlDbType.NVarChar,256),
					new SqlParameter("@EmergencyContact", SqlDbType.NVarChar,256),
					new SqlParameter("@EmergencyContactPhone", SqlDbType.NVarChar,32),
					new SqlParameter("@Address", SqlDbType.NVarChar,256),
					new SqlParameter("@City", SqlDbType.NVarChar,64),
					new SqlParameter("@Province", SqlDbType.NVarChar,64),
					new SqlParameter("@Country", SqlDbType.NVarChar,64),
					new SqlParameter("@PostCode", SqlDbType.NVarChar,32),
					new SqlParameter("@Adress2", SqlDbType.NVarChar,256),
					new SqlParameter("@City2", SqlDbType.NVarChar,64),
					new SqlParameter("@Code", SqlDbType.NVarChar,128),
					new SqlParameter("@Province2", SqlDbType.NVarChar,64),
					new SqlParameter("@Country2", SqlDbType.NVarChar,64),
					new SqlParameter("@PostCode2", SqlDbType.NVarChar,32),
					new SqlParameter("@WorkAddress", SqlDbType.NVarChar,256),
					new SqlParameter("@WorkCity", SqlDbType.NVarChar,64),
					new SqlParameter("@WorkProvince", SqlDbType.NVarChar,64),
					new SqlParameter("@WorkCountry", SqlDbType.NVarChar,64),
					new SqlParameter("@WorkPostCode", SqlDbType.NVarChar,32),
					new SqlParameter("@MaritalStatus", SqlDbType.Int,4),
					new SqlParameter("@Gender", SqlDbType.Int,4),
					new SqlParameter("@TypeID", SqlDbType.Int,4),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@BirthPlace", SqlDbType.NVarChar,256),
					new SqlParameter("@DomicilePlace", SqlDbType.NVarChar,256),
					new SqlParameter("@IDNumber", SqlDbType.NVarChar,64),
					new SqlParameter("@Photo", SqlDbType.NVarChar,256),
					new SqlParameter("@Degree", SqlDbType.NVarChar,32),
					new SqlParameter("@Education", SqlDbType.NVarChar,32),
					new SqlParameter("@GraduatedFrom", SqlDbType.NVarChar,256),
					new SqlParameter("@Major", SqlDbType.NVarChar,256),
					new SqlParameter("@GraduatedDate", SqlDbType.DateTime),
					new SqlParameter("@Phone1", SqlDbType.NVarChar,32),
					new SqlParameter("@Health", SqlDbType.NVarChar,256),
					new SqlParameter("@DiseaseInSixMonths", SqlDbType.NVarChar,4),
					new SqlParameter("@DiseaseInSixMonthsInfo", SqlDbType.NVarChar,256),
					new SqlParameter("@WorkExperience", SqlDbType.NVarChar,4000),
					new SqlParameter("@WorkSpecialty", SqlDbType.NVarChar,500),
					new SqlParameter("@ThisYearSalary", SqlDbType.NVarChar,100),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Memo", SqlDbType.NVarChar,1000),
					new SqlParameter("@BaseInfoOK", SqlDbType.Bit,1),
					new SqlParameter("@ContractInfoOK", SqlDbType.Bit,1),
					new SqlParameter("@Phone2", SqlDbType.NVarChar,32),
					new SqlParameter("@InsuranceInfoOK", SqlDbType.Bit,1),
					new SqlParameter("@ArchiveInfoOK", SqlDbType.Bit,1),
					new SqlParameter("@Creator", SqlDbType.Int,4),
					new SqlParameter("@CreatedTime", SqlDbType.DateTime),
					new SqlParameter("@LastModifier", SqlDbType.Int,4),
					new SqlParameter("@LastModifiedTime", SqlDbType.DateTime),
					new SqlParameter("@MobilePhone", SqlDbType.NVarChar,32),
					new SqlParameter("@HomePhone", SqlDbType.NVarChar,32),
					new SqlParameter("@Fax", SqlDbType.NVarChar,32),
					new SqlParameter("@InternalEmail", SqlDbType.NVarChar,256),
                    new SqlParameter("@Resume",SqlDbType.NVarChar,100),
                    new SqlParameter("@IsForeign",SqlDbType.Bit,1),
                    new SqlParameter("@IsCertification",SqlDbType.Bit,1),
                    new SqlParameter("@WageMonths",SqlDbType.Int,4),
                    new SqlParameter("@IPCode", SqlDbType.NVarChar,32),
                    new SqlParameter("@IsSendMail",SqlDbType.Bit,1),
                    new SqlParameter("@CommonName",SqlDbType.NVarChar,255),
                    new SqlParameter("@DimissionStatus",SqlDbType.Int,4),
					new SqlParameter("@PrivateEmail", SqlDbType.NVarChar),
					new SqlParameter("@OwnedPC", SqlDbType.Bit,1),
					new SqlParameter("@OfferLetterTemplate", SqlDbType.Int,4),
                    new SqlParameter("@OfferLetterSender", SqlDbType.Int, 4),
                    new SqlParameter("@OfferLetterSendTime", SqlDbType.DateTime),
                    new SqlParameter("@IsExamen", SqlDbType.Bit, 1),
                    new SqlParameter("@Seniority", SqlDbType.Int, 4),
                    new SqlParameter("@Pay", SqlDbType.Decimal,5),
                    new SqlParameter("@Performance", SqlDbType.Decimal,5),
                    new SqlParameter("@Residence", SqlDbType.NVarChar,50),
                    new SqlParameter("@MateName", SqlDbType.NVarChar,50),
                    new SqlParameter("@AdrressNow", SqlDbType.NVarChar,500),
                    new SqlParameter("@PostCodeNow", SqlDbType.NVarChar,50),
                    new SqlParameter("@FamillyDesc", SqlDbType.NVarChar,500),
                      new SqlParameter("@Political", SqlDbType.NVarChar,50),
                        new SqlParameter("@Nation", SqlDbType.NVarChar,50),
                          new SqlParameter("@HasChild", SqlDbType.Bit),
                           new SqlParameter("@EmpProperty", SqlDbType.Int),
                           new SqlParameter("@Appearance", SqlDbType.NVarChar,50),
                           new SqlParameter("@Quality", SqlDbType.NVarChar,50),
                           new SqlParameter("@Know", SqlDbType.NVarChar,50),
                           new SqlParameter("@Equal", SqlDbType.NVarChar,50),
                           new SqlParameter("@Motivation", SqlDbType.NVarChar,50),
                           new SqlParameter("@FourD", SqlDbType.NVarChar,50),
                            new SqlParameter("@EQ", SqlDbType.NVarChar,50),
                            new SqlParameter("@workBegin",SqlDbType.DateTime),
                              new SqlParameter("@SocialSecurityType",SqlDbType.Int),
                              new SqlParameter("@JoinDate",SqlDbType.DateTime),
                              new SqlParameter("@BranchCode",SqlDbType.NVarChar),
                              new SqlParameter("@ContractYear",SqlDbType.Int),
                              new SqlParameter("@ContractBeginDate",SqlDbType.DateTime),
                              new SqlParameter("@ContractEndDate",SqlDbType.DateTime),
                              new SqlParameter("@ContractSignDate",SqlDbType.DateTime),
                              new SqlParameter("@AnnualLeaveBase",SqlDbType.Int),
                              new SqlParameter("@ProbationDate",SqlDbType.DateTime),
                              new SqlParameter("@FirstContractBeginDate",SqlDbType.DateTime),
                              new SqlParameter("@FirstContractEndDate",SqlDbType.DateTime),
                              new SqlParameter("@SalaryBank", SqlDbType.NVarChar,100),
                              new SqlParameter("@SalaryCardNo", SqlDbType.NVarChar,50),
                              new SqlParameter("@Attendance",SqlDbType.Decimal,5),
                              new SqlParameter("@IDValid",SqlDbType.DateTime)
                                        };
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.IM;
            parameters[2].Value = model.EmergencyContact;
            parameters[3].Value = model.EmergencyContactPhone;
            parameters[4].Value = model.Address;
            parameters[5].Value = model.City;
            parameters[6].Value = model.Province;
            parameters[7].Value = model.Country;
            parameters[8].Value = model.PostCode;
            parameters[9].Value = model.Address2;
            parameters[10].Value = model.City2;
            parameters[11].Value = model.Code;
            parameters[12].Value = model.Province2;
            parameters[13].Value = model.Country2;
            parameters[14].Value = model.PostCode2;
            parameters[15].Value = model.WorkAddress;
            parameters[16].Value = model.WorkCity;
            parameters[17].Value = model.WorkProvince;
            parameters[18].Value = model.WorkCountry;
            parameters[19].Value = model.WorkPostCode;
            parameters[20].Value = model.MaritalStatus;
            parameters[21].Value = model.Gender;
            parameters[22].Value = model.TypeID;
            parameters[23].Value = model.Birthday;
            parameters[24].Value = model.BirthPlace;
            parameters[25].Value = model.DomicilePlace;
            parameters[26].Value = model.IDNumber;
            parameters[27].Value = model.Photo;
            parameters[28].Value = model.Degree;
            parameters[29].Value = model.Education;
            parameters[30].Value = model.GraduateFrom;
            parameters[31].Value = model.Major;
            parameters[32].Value = model.GraduatedDate;
            parameters[33].Value = model.Phone1;
            parameters[34].Value = model.Health;
            parameters[35].Value = model.DiseaseInSixMonths;
            parameters[36].Value = model.DiseaseInSixMonthsInfo;
            parameters[37].Value = model.WorkExperience;
            parameters[38].Value = model.WorkSpecialty;
            parameters[39].Value = model.ThisYearSalary;
            parameters[40].Value = model.Status;
            parameters[41].Value = model.Memo;
            parameters[42].Value = model.BaseInfoOK;
            parameters[43].Value = model.ContractInfoOK;
            parameters[44].Value = model.Phone2;
            parameters[45].Value = model.InsuranceInfoOK;
            parameters[46].Value = model.ArchiveInfoOK;
            parameters[47].Value = model.Creator;
            parameters[48].Value = model.CreatedTime;
            parameters[49].Value = model.LastModifier;
            parameters[50].Value = model.LastModifiedTime;
            parameters[51].Value = model.MobilePhone;
            parameters[52].Value = model.HomePhone;
            parameters[53].Value = model.Fax;
            parameters[54].Value = model.InternalEmail;
            parameters[55].Value = model.Resume;
            parameters[56].Value = model.IsForeign;
            parameters[57].Value = model.IsCertification;
            parameters[58].Value = model.WageMonths;
            parameters[59].Value = model.IPCode;
            parameters[60].Value = model.IsSendMail;
            parameters[61].Value = model.CommonName;
            parameters[62].Value = model.DimissionStatus;
            parameters[63].Value = model.PrivateEmail;
            parameters[64].Value = model.OwnedPC;
            parameters[65].Value = model.OfferLetterTemplate;
            parameters[66].Value = model.OfferLetterSender;
            parameters[67].Value = model.OfferLetterSendTime;
            parameters[68].Value = model.IsExamen;
            parameters[69].Value = model.Seniority;
            parameters[70].Value = model.Pay;
            parameters[71].Value = model.Performance;
            parameters[72].Value = model.Residence;
            parameters[73].Value = model.MateName;
            parameters[74].Value = model.AdrressNow;
            parameters[75].Value = model.PostCodeNow;
            parameters[76].Value = model.FamillyDesc;
            parameters[77].Value = model.Political;
            parameters[78].Value = model.Nation;
            parameters[79].Value = model.HasChild;
            parameters[80].Value = model.EmpProperty;

            parameters[81].Value = model.Appearance;
            parameters[82].Value = model.Quality;
            parameters[83].Value = model.Know;
            parameters[84].Value = model.Equal;
            parameters[85].Value = model.Motivation;
            parameters[86].Value = model.FourD;
            parameters[87].Value = model.EQ;
            parameters[88].Value = model.WorkBegin;
            parameters[89].Value = model.SocialSecurityType;
            parameters[90].Value = model.JoinDate;
            parameters[91].Value = model.BranchCode;
            parameters[92].Value = model.ContractYear;
            parameters[93].Value = model.ContractBeginDate;
            parameters[94].Value = model.ContractEndDate;
            parameters[95].Value = model.ContractSignDate;
            parameters[96].Value = model.AnnualLeaveBase;
            parameters[97].Value = model.ProbationDate;
            parameters[98].Value = model.FirstContractBeginDate;
            parameters[99].Value = model.FirstContractEndDate;
            parameters[100].Value = model.SalaryBank;
            parameters[101].Value = model.SalaryCardNo;
            parameters[102].Value = model.Attendance;
            parameters[103].Value = model.IDValid;
            DbHelperSQL.ExecuteSql(strSql.ToString(), stran.Connection, stran, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int UserID, SqlTransaction stran)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete sep_Employees ");
            strSql.Append(" where UserID=@UserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters[0].Value = UserID;

            if (stran == null)
                DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            else
                DbHelperSQL.ExecuteSql(strSql.ToString(), stran.Connection, stran, parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EmployeeBaseInfo GetModel(int UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT a.*, b.Username, b.FirstNameCN, b.LastNameCN, b.FirstNameEN,b.LastNameCN+b.FirstNameCN as FullNameCn,");
            strSql.Append(" b.LastNameEN, b.Email, b.CreatedDate, b.LastActivityDate, b.Password, b.PasswordSalt, b.IsApproved, b.IsLockedOut, b.LastLoginDate,");
            strSql.Append(" b.LastPasswordChangedDate, b.LastLockoutDate, b.FailedPasswordAttemptCount, b.FailedPasswordAttemptWindowStart, b.Comment,b.ResetPasswordCode, b.IsDeleted ");
            strSql.Append(" FROM sep_Employees a left join sep_Users b on a.UserID=b.UserID ");

            strSql.Append(" WHERE a.UserID=@UserID ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters[0].Value = UserID;

            EmployeeBaseInfo model = new EmployeeBaseInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.EmployeeJobInfo = EmployeeJobManager.getModelBySysId(UserID);
                if (model.EmployeeJobInfo == null)
                {
                    model.EmployeeJobInfo = new EmployeeJobInfo();
                }
                model.EmployeeWelfareInfo = EmployeeWelfareManager.getModelBySysId(UserID);
                if (model.EmployeeWelfareInfo == null)
                {
                    model.EmployeeWelfareInfo = new EmployeeWelfareInfo();
                }
                if (ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                model.LastNameCN = ds.Tables[0].Rows[0]["LastNameCN"].ToString();
                model.FirstNameCN = ds.Tables[0].Rows[0]["FirstNameCN"].ToString();
                model.Username = ds.Tables[0].Rows[0]["Username"].ToString();
                model.IM = ds.Tables[0].Rows[0]["IM"].ToString();
                model.EmergencyContact = ds.Tables[0].Rows[0]["EmergencyContact"].ToString();
                model.EmergencyContactPhone = ds.Tables[0].Rows[0]["EmergencyContactPhone"].ToString();
                model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                model.City = ds.Tables[0].Rows[0]["City"].ToString();
                model.Province = ds.Tables[0].Rows[0]["Province"].ToString();
                model.Country = ds.Tables[0].Rows[0]["Country"].ToString();
                model.PostCode = ds.Tables[0].Rows[0]["PostCode"].ToString();
                model.Address2 = ds.Tables[0].Rows[0]["Adress2"].ToString();
                model.City2 = ds.Tables[0].Rows[0]["City2"].ToString();
                model.Code = ds.Tables[0].Rows[0]["Code"].ToString();
                model.Province2 = ds.Tables[0].Rows[0]["Province2"].ToString();
                model.Country2 = ds.Tables[0].Rows[0]["Country2"].ToString();
                model.PostCode2 = ds.Tables[0].Rows[0]["PostCode2"].ToString();
                model.WorkAddress = ds.Tables[0].Rows[0]["WorkAddress"].ToString();
                model.WorkCity = ds.Tables[0].Rows[0]["WorkCity"].ToString();
                model.WorkProvince = ds.Tables[0].Rows[0]["WorkProvince"].ToString();
                model.WorkCountry = ds.Tables[0].Rows[0]["WorkCountry"].ToString();
                model.WorkPostCode = ds.Tables[0].Rows[0]["WorkPostCode"].ToString();
                if (ds.Tables[0].Rows[0]["MaritalStatus"].ToString() != "")
                {
                    model.MaritalStatus = int.Parse(ds.Tables[0].Rows[0]["MaritalStatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Gender"].ToString() != "")
                {
                    model.Gender = int.Parse(ds.Tables[0].Rows[0]["Gender"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TypeID"].ToString() != "")
                {
                    model.TypeID = int.Parse(ds.Tables[0].Rows[0]["TypeID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Birthday"].ToString() != "")
                {
                    model.Birthday = DateTime.Parse(ds.Tables[0].Rows[0]["Birthday"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Pay"].ToString() != "")
                {
                    model.Pay = decimal.Parse(ds.Tables[0].Rows[0]["Pay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Performance"].ToString() != "")
                {
                    model.Performance = decimal.Parse(ds.Tables[0].Rows[0]["Performance"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Attendance"].ToString() != "")
                {
                    model.Attendance = decimal.Parse(ds.Tables[0].Rows[0]["Attendance"].ToString());
                }
                model.BirthPlace = ds.Tables[0].Rows[0]["BirthPlace"].ToString();
                model.IDNumber = ds.Tables[0].Rows[0]["IDNumber"].ToString();
                model.DomicilePlace = ds.Tables[0].Rows[0]["DomicilePlace"].ToString();
                model.Photo = ds.Tables[0].Rows[0]["Photo"].ToString();
                model.Degree = ds.Tables[0].Rows[0]["Degree"].ToString();
                model.Education = ds.Tables[0].Rows[0]["Education"].ToString();
                model.GraduateFrom = ds.Tables[0].Rows[0]["GraduatedFrom"].ToString();
                model.Major = ds.Tables[0].Rows[0]["Major"].ToString();
                if (ds.Tables[0].Rows[0]["GraduatedDate"].ToString() != "")
                {
                    model.GraduatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["GraduatedDate"].ToString());
                }
                model.Health = ds.Tables[0].Rows[0]["Health"].ToString();
                model.Phone1 = ds.Tables[0].Rows[0]["Phone1"].ToString();
                model.DiseaseInSixMonths = ds.Tables[0].Rows[0]["DiseaseInSixMonths"].ToString();
                model.DiseaseInSixMonthsInfo = ds.Tables[0].Rows[0]["DiseaseInSixMonthsInfo"].ToString();
                model.WorkExperience = ds.Tables[0].Rows[0]["WorkExperience"].ToString();
                model.WorkSpecialty = ds.Tables[0].Rows[0]["WorkSpecialty"].ToString();
                model.ThisYearSalary = ds.Tables[0].Rows[0]["ThisYearSalary"].ToString();

                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                if (ds.Tables[0].Rows[0]["BaseInfoOK"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["BaseInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[0]["BaseInfoOK"].ToString().ToLower() == "true"))
                    {
                        model.BaseInfoOK = true;
                    }
                    else
                    {
                        model.BaseInfoOK = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["ContractInfoOK"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["ContractInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[0]["ContractInfoOK"].ToString().ToLower() == "true"))
                    {
                        model.ContractInfoOK = true;
                    }
                    else
                    {
                        model.ContractInfoOK = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["InsuranceInfoOK"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["InsuranceInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[0]["InsuranceInfoOK"].ToString().ToLower() == "true"))
                    {
                        model.InsuranceInfoOK = true;
                    }
                    else
                    {
                        model.InsuranceInfoOK = false;
                    }
                }
                model.Phone2 = ds.Tables[0].Rows[0]["Phone2"].ToString();
                if (ds.Tables[0].Rows[0]["ArchiveInfoOK"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["ArchiveInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[0]["ArchiveInfoOK"].ToString().ToLower() == "true"))
                    {
                        model.ArchiveInfoOK = true;
                    }
                    else
                    {
                        model.ArchiveInfoOK = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Creator"].ToString() != "")
                {
                    model.Creator = int.Parse(ds.Tables[0].Rows[0]["Creator"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedTime"].ToString() != "")
                {
                    model.CreatedTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastModifier"].ToString() != "")
                {
                    model.LastModifier = int.Parse(ds.Tables[0].Rows[0]["LastModifier"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastModifiedTime"].ToString() != "")
                {
                    model.LastModifiedTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastModifiedTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RowVersion"].ToString() != "")
                {
                    model.RowVersion = (byte[])ds.Tables[0].Rows[0]["RowVersion"];
                }
                model.MobilePhone = ds.Tables[0].Rows[0]["MobilePhone"].ToString();
                model.HomePhone = ds.Tables[0].Rows[0]["HomePhone"].ToString();
                model.Fax = ds.Tables[0].Rows[0]["Fax"].ToString();
                model.InternalEmail = ds.Tables[0].Rows[0]["InternalEmail"].ToString();
                model.Resume = ds.Tables[0].Rows[0]["Resume"].ToString();
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
                model.IPCode = ds.Tables[0].Rows[0]["IPCode"].ToString();
                if (ds.Tables[0].Rows[0]["IsSendMail"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsSendMail"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsSendMail"].ToString().ToLower() == "true"))
                    {
                        model.IsSendMail = true;
                    }
                    else
                    {
                        model.IsSendMail = false;
                    }
                }
                model.CommonName = ds.Tables[0].Rows[0]["CommonName"].ToString();
                if (ds.Tables[0].Rows[0]["DimissionStatus"] != DBNull.Value)
                    model.DimissionStatus = int.Parse(ds.Tables[0].Rows[0]["DimissionStatus"].ToString());
                else
                    model.DimissionStatus = 0;
                model.PrivateEmail = ds.Tables[0].Rows[0]["PrivateEmail"].ToString();
                if (ds.Tables[0].Rows[0]["OwnedPC"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["OwnedPC"].ToString() == "1") || (ds.Tables[0].Rows[0]["OwnedPC"].ToString().ToLower() == "true"))
                    {
                        model.OwnedPC = true;
                    }
                    else
                    {
                        model.OwnedPC = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["OfferLetterTemplate"].ToString() != "")
                {
                    model.OfferLetterTemplate = int.Parse(ds.Tables[0].Rows[0]["OfferLetterTemplate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OfferLetterSender"].ToString() != "")
                {
                    model.OfferLetterSender = int.Parse(ds.Tables[0].Rows[0]["OfferLetterSender"].ToString());
                }
                var objOfferLetterSendTime = ds.Tables[0].Rows[0]["OfferLetterSendTime"];
                if (!(objOfferLetterSendTime is DBNull))
                {
                    model.OfferLetterSendTime = (DateTime)objOfferLetterSendTime;
                }
                if (ds.Tables[0].Rows[0]["IsExamen"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsExamen"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsExamen"].ToString().ToLower() == "true"))
                    {
                        model.IsExamen = true;
                    }
                    else
                    {
                        model.IsExamen = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Seniority"].ToString() != "")
                {
                    model.Seniority = int.Parse(ds.Tables[0].Rows[0]["Seniority"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EmpProperty"].ToString() != "")
                {
                    model.EmpProperty = int.Parse(ds.Tables[0].Rows[0]["EmpProperty"].ToString());
                }

                //Residence,MateName,AdrressNow,PostCodeNow,FamillyDesc
                model.Residence = ds.Tables[0].Rows[0]["Residence"].ToString();
                model.MateName = ds.Tables[0].Rows[0]["MateName"].ToString();
                model.AdrressNow = ds.Tables[0].Rows[0]["AdrressNow"].ToString();
                model.PostCodeNow = ds.Tables[0].Rows[0]["PostCodeNow"].ToString();
                model.FamillyDesc = ds.Tables[0].Rows[0]["FamillyDesc"].ToString();
                model.Political = ds.Tables[0].Rows[0]["Political"].ToString();
                model.Nation = ds.Tables[0].Rows[0]["Nation"].ToString();
                //Appearance,Quality,Know,Equal,Motivation,FourD,EQ
                model.Appearance = ds.Tables[0].Rows[0]["Appearance"].ToString();
                model.Quality = ds.Tables[0].Rows[0]["Quality"].ToString();
                model.Know = ds.Tables[0].Rows[0]["Know"].ToString();
                model.Equal = ds.Tables[0].Rows[0]["Equal"].ToString();
                model.Motivation = ds.Tables[0].Rows[0]["Motivation"].ToString();
                model.FourD = ds.Tables[0].Rows[0]["FourD"].ToString();
                model.EQ = ds.Tables[0].Rows[0]["EQ"].ToString();
                if (ds.Tables[0].Rows[0]["workBegin"].ToString() != "")
                {
                    model.WorkBegin = DateTime.Parse(ds.Tables[0].Rows[0]["workBegin"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JoinDate"].ToString() != "")
                {
                    model.JoinDate = DateTime.Parse(ds.Tables[0].Rows[0]["JoinDate"].ToString());
                }

                model.BranchCode = ds.Tables[0].Rows[0]["BranchCode"].ToString();

                if (ds.Tables[0].Rows[0]["ContractYear"].ToString() != "")
                {
                    model.ContractYear = int.Parse(ds.Tables[0].Rows[0]["ContractYear"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ContractBeginDate"].ToString() != "")
                {
                    model.ContractBeginDate = DateTime.Parse(ds.Tables[0].Rows[0]["ContractBeginDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ContractEndDate"].ToString() != "")
                {
                    model.ContractEndDate = DateTime.Parse(ds.Tables[0].Rows[0]["ContractEndDate"].ToString());
                }

                if (ds.Tables[0].Rows[0]["FirstContractBeginDate"].ToString() != "")
                {
                    model.FirstContractBeginDate = DateTime.Parse(ds.Tables[0].Rows[0]["FirstContractBeginDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FirstContractEndDate"].ToString() != "")
                {
                    model.FirstContractEndDate = DateTime.Parse(ds.Tables[0].Rows[0]["FirstContractEndDate"].ToString());
                }

                if (ds.Tables[0].Rows[0]["ContractSignDate"].ToString() != "")
                {
                    model.ContractSignDate = DateTime.Parse(ds.Tables[0].Rows[0]["ContractSignDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AnnualLeaveBase"].ToString() != "")
                {
                    model.AnnualLeaveBase = int.Parse(ds.Tables[0].Rows[0]["AnnualLeaveBase"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProbationDate"].ToString() != "")
                {
                    model.ProbationDate = DateTime.Parse(ds.Tables[0].Rows[0]["ProbationDate"].ToString());
                }

                if (ds.Tables[0].Rows[0]["SocialSecurityType"].ToString() != "")
                {
                    model.SocialSecurityType = int.Parse(ds.Tables[0].Rows[0]["SocialSecurityType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HasChild"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["HasChild"].ToString() == "1") || (ds.Tables[0].Rows[0]["HasChild"].ToString().ToLower() == "true"))
                    {
                        model.HasChild = true;
                    }
                    else
                    {
                        model.HasChild = false;
                    }
                }
                model.SalaryBank = ds.Tables[0].Rows[0]["SalaryBank"].ToString();
                model.SalaryCardNo = ds.Tables[0].Rows[0]["SalaryCardNo"].ToString();

                if (ds.Tables[0].Rows[0]["IDValid"].ToString() != "")
                {
                    model.IDValid = DateTime.Parse(ds.Tables[0].Rows[0]["IDValid"].ToString());
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
            strSql.Append(" SELECT a.*, b.Username, b.FirstNameCN, b.LastNameCN, b.FirstNameEN,b.LastNameCN+b.FirstNameCN as FullNameCn,");
            strSql.Append(" b.LastNameEN, b.Email, b.CreatedDate, b.LastActivityDate, b.Password, b.PasswordSalt, b.IsApproved, b.IsLockedOut, b.LastLoginDate,");
            strSql.Append(" b.LastPasswordChangedDate, b.LastLockoutDate, b.FailedPasswordAttemptCount, b.FailedPasswordAttemptWindowStart, b.Comment,b.ResetPasswordCode, b.IsDeleted,c.ContractCompany, ");
            strSql.Append(" j.companyName, j.companyID, j.departmentName,j.departmentID,j.groupName,j.groupID,j.joinJob,di.lastday");
            strSql.Append(" FROM sep_Employees a left join sep_Users b on a.UserID=b.UserID left join sep_EmployeeWelfareInfo c on a.UserID=c.sysid left join sep_EmployeeJobInfo j on a.UserID=j.sysid  ");
            strSql.Append(" left join sep_dimissionform di on a.userid = di.userid");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            strSql.Append(" order by a.joinDate desc");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetList(string strWhere,string serverstring)
        {
            string newconnstring = ESP.Configuration.ConfigurationManager.SafeAppSettings[serverstring];

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT a.*, b.Username, b.FirstNameCN, b.LastNameCN, b.FirstNameEN,b.LastNameCN+b.FirstNameCN as FullNameCn,");
            strSql.Append(" b.LastNameEN, b.Email, b.CreatedDate, b.LastActivityDate, b.Password, b.PasswordSalt, b.IsApproved, b.IsLockedOut, b.LastLoginDate,");
            strSql.Append(" b.LastPasswordChangedDate, b.LastLockoutDate, b.FailedPasswordAttemptCount, b.FailedPasswordAttemptWindowStart, b.Comment,b.ResetPasswordCode, b.IsDeleted,c.ContractCompany, ");
            strSql.Append(" j.companyName, j.companyID, j.departmentName,j.departmentID,j.groupName,j.groupID,j.joinJob,di.lastday");
            strSql.Append(" FROM sep_Employees a left join sep_Users b on a.UserID=b.UserID left join sep_EmployeeWelfareInfo c on a.UserID=c.sysid left join sep_EmployeeJobInfo j on a.UserID=j.sysid  ");
            strSql.Append(" left join sep_dimissionform di on a.userid = di.userid");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            strSql.Append(" order by a.joinDate desc");
            return DbHelperSQL.Query(strSql.ToString(), newconnstring);
        }


        public DataSet GetListForExport(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT a.*, b.Username, b.FirstNameCN, b.LastNameCN, b.FirstNameEN,b.LastNameCN+b.FirstNameCN as FullNameCn,");
            strSql.Append(" b.LastNameEN, b.Email, b.CreatedDate, b.LastActivityDate, b.Password, b.PasswordSalt, b.IsApproved, b.IsLockedOut, b.LastLoginDate,");
            strSql.Append(" b.LastPasswordChangedDate, b.LastLockoutDate, b.FailedPasswordAttemptCount, b.FailedPasswordAttemptWindowStart, b.Comment,b.ResetPasswordCode, b.IsDeleted,c.ContractCompany, ");
            strSql.Append("  v.level1 CompanyName, v.level1Id CompanyID, v.level2 DepartmentName, v.level2Id DepartmentID, v.level3 GroupName, v.level3Id GroupID,dp.DepartmentPositionName joinJob,di.lastday");
            strSql.Append(" FROM sep_Employees a left join sep_Users b on a.UserID=b.UserID left join sep_EmployeeWelfareInfo c on a.UserID=c.sysid left join sep_EmployeeJobInfo j on a.UserID=j.sysid  ");
            strSql.Append(" left join sep_EmployeesInPositions p on a.UserID=p.UserID left join V_Department v on p.DepartmentID=v.level3Id");
            strSql.Append(" left join sep_DepartmentPositions dp on p.DepartmentPositionID=dp.DepartmentPositionID left join sep_dimissionform di on a.userid = di.userid");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            strSql.Append(" order by a.joinDate desc");
            return DbHelperSQL.Query(strSql.ToString());
        }


        public DataSet GetListForExpress(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT a.userid,a.code, (b.LastNameCN+b.FirstNameCN) NameCN,");
            strSql.Append("  v.level1+'-'+v.level2+'-'+v.level3 GroupName ");
            strSql.Append(" FROM sep_Employees a left join sep_Users b on a.UserID=b.UserID ");
            strSql.Append(" left join sep_EmployeesInPositions p on a.UserID=p.UserID left join V_Department v on p.DepartmentID=v.level3Id");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            strSql.Append(" order by a.userid desc");
            return DbHelperSQL.Query(strSql.ToString());
        }


        public DataSet GetListForHC(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT a.*, b.Username, b.FirstNameCN, b.LastNameCN, b.FirstNameEN,b.LastNameCN+b.FirstNameCN as FullNameCn,");
            strSql.Append(" b.LastNameEN, b.Email, b.CreatedDate, b.LastActivityDate, b.Password, b.PasswordSalt, b.IsApproved, b.IsLockedOut, b.LastLoginDate,");
            strSql.Append(" b.LastPasswordChangedDate, b.LastLockoutDate, b.FailedPasswordAttemptCount, b.FailedPasswordAttemptWindowStart, b.Comment,b.ResetPasswordCode, b.IsDeleted,c.ContractCompany, ");
            strSql.Append(" j.companyName, j.companyID, j.departmentName,j.departmentID,j.groupName,j.groupID,j.joinJob,di.lastday ");
            strSql.Append(" FROM sep_Employees a left join sep_Users b on a.UserID=b.UserID left join sep_EmployeeWelfareInfo c on a.UserID=c.sysid left join sep_EmployeeJobInfo j on a.UserID=j.sysid  left join sep_EmployeesInPositions h on a.UserID=h.UserID");
            strSql.Append(" left join sep_dimissionform di on a.userid = di.userid");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            strSql.Append(" order by a.joinDate desc");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetWaitEntryList(int userid, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT a.*, b.Username, b.FirstNameCN, b.LastNameCN, b.FirstNameEN,b.LastNameCN+b.FirstNameCN as FullNameCn,");
            strSql.Append(" b.LastNameEN, b.Email, b.CreatedDate, b.LastActivityDate, b.Password, b.PasswordSalt, b.IsApproved, b.IsLockedOut, b.LastLoginDate,");
            strSql.Append(" b.LastPasswordChangedDate, b.LastLockoutDate, b.FailedPasswordAttemptCount, b.FailedPasswordAttemptWindowStart, b.Comment,b.ResetPasswordCode, b.IsDeleted,c.ContractCompany, ");
            strSql.Append(" j.companyName, j.companyID, j.departmentName,j.departmentID,j.groupName,j.groupID,j.joinJob ");
            strSql.Append(" FROM sep_Employees a LEFT JOIN sep_Users b ON a.UserID=b.UserID LEFT JOIN sep_EmployeeWelfareInfo c ON a.UserID=c.sysid LEFT JOIN sep_EmployeeJobInfo j ON a.UserID=j.sysid left join sep_OperationAuditManage o on j.groupid =o.DepId  ");
            if (userid == 0)
            {
                if (!string.IsNullOrEmpty(strWhere))
                    strSql.Append(" WHERE 1=1 " + strWhere);
            }
            else
                strSql.Append(" WHERE (o.HRId =" + userid + " or o.HRAttendanceId =" + userid + ") " + strWhere);

            strSql.Append(" ORDER BY j.companyid DESC, PATINDEX('%'+CONVERT(VARCHAR(10),a.joinDate,120)+'%', CONVERT(VARCHAR(10),GETDATE(),120)) DESC, a.isSendMail, a.joinDate DESC");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetUserListYesWeCan()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT a.phone1,a.mobilephone,b.Username,b.LastNameCN+b.FirstNameCN as FullNameCn,");
            strSql.Append(" b.Email,dp.DepartmentPositionName,v.level1+'-'+v.level2+'-'+v.level3 as departmentname");
            strSql.Append(" FROM sep_Employees a ");
            strSql.Append(" left join sep_Users b on a.UserID=b.UserID ");
            strSql.Append(" left join sep_EmployeesInPositions p on a.UserID=p.userid ");
            strSql.Append(" left join  sep_DepartmentPositions dp on p.DepartmentPositionID=dp.DepartmentPositionID");
            strSql.Append(" left join v_department v on p.DepartmentID=level3id");
            strSql.Append(" where b.IsApproved=1 and b.IsDeleted=0");

            return DbHelperSQL.Query(strSql.ToString());
        }

        //public List<EmployeeBaseInfo> GetAllInfoList(string strWhere)
        //{
        //    List<EmployeeBaseInfo> list = new List<EmployeeBaseInfo>();
        //    DataSet ds = GetList(strWhere);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            EmployeeBaseInfo model = new EmployeeBaseInfo();

        //            if (ds.Tables[0].Rows[i]["UserID"].ToString() != "")
        //            {
        //                model.UserID = int.Parse(ds.Tables[0].Rows[i]["UserID"].ToString());
        //                model.EmployeeJobInfo = EmployeeJobManager.getModelBySysId(model.UserID);
        //                model.EmployeeWelfareInfo = EmployeeWelfareManager.getModelBySysId(model.UserID);
        //            }
        //            model.IM = ds.Tables[0].Rows[i]["IM"].ToString();
        //            model.EmergencyContact = ds.Tables[0].Rows[i]["EmergencyContact"].ToString();
        //            model.EmergencyContactPhone = ds.Tables[0].Rows[i]["EmergencyContactPhone"].ToString();
        //            model.Address = ds.Tables[0].Rows[i]["Address"].ToString();
        //            model.City = ds.Tables[0].Rows[i]["City"].ToString();
        //            model.Province = ds.Tables[0].Rows[i]["Province"].ToString();
        //            model.Country = ds.Tables[0].Rows[i]["Country"].ToString();
        //            model.PostCode = ds.Tables[0].Rows[i]["PostCode"].ToString();
        //            model.Address2 = ds.Tables[0].Rows[i]["Adress2"].ToString();
        //            model.City2 = ds.Tables[0].Rows[i]["City2"].ToString();
        //            model.Code = ds.Tables[0].Rows[i]["Code"].ToString();
        //            model.Province2 = ds.Tables[0].Rows[i]["Province2"].ToString();
        //            model.Country2 = ds.Tables[0].Rows[i]["Country2"].ToString();
        //            model.PostCode2 = ds.Tables[0].Rows[i]["PostCode2"].ToString();
        //            model.WorkAddress = ds.Tables[0].Rows[i]["WorkAddress"].ToString();
        //            model.WorkCity = ds.Tables[0].Rows[i]["WorkCity"].ToString();
        //            model.WorkProvince = ds.Tables[0].Rows[i]["WorkProvince"].ToString();
        //            model.WorkCountry = ds.Tables[0].Rows[i]["WorkCountry"].ToString();
        //            model.WorkPostCode = ds.Tables[0].Rows[i]["WorkPostCode"].ToString();
        //            if (ds.Tables[0].Rows[i]["MaritalStatus"].ToString() != "")
        //            {
        //                model.MaritalStatus = int.Parse(ds.Tables[0].Rows[i]["MaritalStatus"].ToString());
        //            }
        //            if (ds.Tables[0].Rows[i]["Gender"].ToString() != "")
        //            {
        //                model.Gender = int.Parse(ds.Tables[0].Rows[i]["Gender"].ToString());
        //            }
        //            if (ds.Tables[0].Rows[i]["TypeID"].ToString() != "")
        //            {
        //                model.TypeID = int.Parse(ds.Tables[0].Rows[i]["TypeID"].ToString());
        //            }
        //            if (ds.Tables[0].Rows[i]["Birthday"].ToString() != "")
        //            {
        //                model.Birthday = DateTime.Parse(ds.Tables[0].Rows[i]["Birthday"].ToString());
        //            }
        //            model.BirthPlace = ds.Tables[0].Rows[i]["BirthPlace"].ToString();
        //            model.DomicilePlace = ds.Tables[0].Rows[i]["DomicilePlace"].ToString();
        //            model.Photo = ds.Tables[0].Rows[i]["Photo"].ToString();
        //            model.Degree = ds.Tables[0].Rows[i]["Degree"].ToString();
        //            model.Education = ds.Tables[0].Rows[i]["Education"].ToString();
        //            model.GraduateFrom = ds.Tables[0].Rows[i]["GraduatedFrom"].ToString();
        //            model.Major = ds.Tables[0].Rows[i]["Major"].ToString();
        //            if (ds.Tables[0].Rows[i]["GraduatedDate"].ToString() != "")
        //            {
        //                model.GraduatedDate = DateTime.Parse(ds.Tables[0].Rows[i]["GraduatedDate"].ToString());
        //            }
        //            model.Health = ds.Tables[0].Rows[i]["Health"].ToString();
        //            model.IDNumber = ds.Tables[0].Rows[i]["IDNumber"].ToString();
        //            model.Phone1 = ds.Tables[0].Rows[i]["Phone1"].ToString();
        //            model.DiseaseInSixMonths = ds.Tables[0].Rows[i]["DiseaseInSixMonths"].ToString();
        //            model.DiseaseInSixMonthsInfo = ds.Tables[0].Rows[i]["DiseaseInSixMonthsInfo"].ToString();
        //            model.WorkExperience = ds.Tables[0].Rows[i]["WorkExperience"].ToString();
        //            model.WorkSpecialty = ds.Tables[0].Rows[i]["WorkSpecialty"].ToString();
        //            model.ThisYearSalary = ds.Tables[0].Rows[i]["ThisYearSalary"].ToString();

        //            if (ds.Tables[0].Rows[i]["Status"].ToString() != "")
        //            {
        //                model.Status = int.Parse(ds.Tables[0].Rows[i]["Status"].ToString());
        //            }
        //            model.Memo = ds.Tables[0].Rows[i]["Memo"].ToString();
        //            if (ds.Tables[0].Rows[i]["BaseInfoOK"].ToString() != "")
        //            {
        //                if ((ds.Tables[0].Rows[i]["BaseInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[i]["BaseInfoOK"].ToString().ToLower() == "true"))
        //                {
        //                    model.BaseInfoOK = true;
        //                }
        //                else
        //                {
        //                    model.BaseInfoOK = false;
        //                }
        //            }
        //            if (ds.Tables[0].Rows[i]["ContractInfoOK"].ToString() != "")
        //            {
        //                if ((ds.Tables[0].Rows[i]["ContractInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[i]["ContractInfoOK"].ToString().ToLower() == "true"))
        //                {
        //                    model.ContractInfoOK = true;
        //                }
        //                else
        //                {
        //                    model.ContractInfoOK = false;
        //                }
        //            }
        //            if (ds.Tables[0].Rows[i]["InsuranceInfoOK"].ToString() != "")
        //            {
        //                if ((ds.Tables[0].Rows[i]["InsuranceInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[i]["InsuranceInfoOK"].ToString().ToLower() == "true"))
        //                {
        //                    model.InsuranceInfoOK = true;
        //                }
        //                else
        //                {
        //                    model.InsuranceInfoOK = false;
        //                }
        //            }
        //            model.Phone2 = ds.Tables[0].Rows[i]["Phone2"].ToString();
        //            if (ds.Tables[0].Rows[i]["ArchiveInfoOK"].ToString() != "")
        //            {
        //                if ((ds.Tables[0].Rows[i]["ArchiveInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[i]["ArchiveInfoOK"].ToString().ToLower() == "true"))
        //                {
        //                    model.ArchiveInfoOK = true;
        //                }
        //                else
        //                {
        //                    model.ArchiveInfoOK = false;
        //                }
        //            }
        //            if (ds.Tables[0].Rows[i]["Creator"].ToString() != "")
        //            {
        //                model.Creator = int.Parse(ds.Tables[0].Rows[i]["Creator"].ToString());
        //            }
        //            if (ds.Tables[0].Rows[i]["CreatedTime"].ToString() != "")
        //            {
        //                model.CreatedTime = DateTime.Parse(ds.Tables[0].Rows[i]["CreatedTime"].ToString());
        //            }
        //            if (ds.Tables[0].Rows[i]["LastModifier"].ToString() != "")
        //            {
        //                model.LastModifier = int.Parse(ds.Tables[0].Rows[i]["LastModifier"].ToString());
        //            }
        //            if (ds.Tables[0].Rows[i]["LastModifiedTime"].ToString() != "")
        //            {
        //                model.LastModifiedTime = DateTime.Parse(ds.Tables[0].Rows[i]["LastModifiedTime"].ToString());
        //            }
        //            if (ds.Tables[0].Rows[i]["RowVersion"].ToString() != "")
        //            {
        //                model.RowVersion = (byte[])ds.Tables[0].Rows[i]["RowVersion"];
        //            }
        //            model.MobilePhone = ds.Tables[0].Rows[i]["MobilePhone"].ToString();
        //            model.HomePhone = ds.Tables[0].Rows[i]["HomePhone"].ToString();
        //            model.Fax = ds.Tables[0].Rows[i]["Fax"].ToString();
        //            model.InternalEmail = ds.Tables[0].Rows[i]["InternalEmail"].ToString();
        //            model.Resume = ds.Tables[0].Rows[i]["Resume"].ToString();



        //            model.Username = ds.Tables[0].Rows[i]["Username"].ToString();
        //            model.FirstNameCN = ds.Tables[0].Rows[i]["FirstNameCN"].ToString();
        //            model.LastNameCN = ds.Tables[0].Rows[i]["LastNameCN"].ToString();
        //            model.FirstNameEN = ds.Tables[0].Rows[i]["FirstNameEN"].ToString();
        //            model.LastNameEN = ds.Tables[0].Rows[i]["LastNameEN"].ToString();
        //            if (ds.Tables[0].Rows[i]["IsForeign"].ToString() != "")
        //            {
        //                if ((ds.Tables[0].Rows[i]["IsForeign"].ToString() == "1") || (ds.Tables[0].Rows[i]["IsForeign"].ToString().ToLower() == "true"))
        //                {
        //                    model.IsForeign = true;
        //                }
        //                else
        //                {
        //                    model.IsForeign = false;
        //                }
        //            }
        //            if (ds.Tables[0].Rows[i]["IsCertification"].ToString() != "")
        //            {
        //                if ((ds.Tables[0].Rows[i]["IsCertification"].ToString() == "1") || (ds.Tables[0].Rows[i]["IsCertification"].ToString().ToLower() == "true"))
        //                {
        //                    model.IsCertification = true;
        //                }
        //                else
        //                {
        //                    model.IsCertification = false;
        //                }
        //            }
        //            if (ds.Tables[0].Rows[i]["WageMonths"].ToString() != "")
        //            {
        //                model.WageMonths = int.Parse(ds.Tables[0].Rows[i]["WageMonths"].ToString());
        //            }
        //            model.IPCode = ds.Tables[0].Rows[i]["IPCode"].ToString();
        //            if (ds.Tables[0].Rows[i]["IsSendMail"].ToString() != "")
        //            {
        //                if ((ds.Tables[0].Rows[i]["IsSendMail"].ToString() == "1") || (ds.Tables[0].Rows[i]["IsSendMail"].ToString().ToLower() == "true"))
        //                {
        //                    model.IsSendMail = true;
        //                }
        //                else
        //                {
        //                    model.IsSendMail = false;
        //                }
        //            }
        //            model.CommonName = ds.Tables[0].Rows[i]["CommonName"].ToString();
        //            if (ds.Tables[0].Rows[i]["DimissionStatus"] != DBNull.Value)
        //                model.DimissionStatus = int.Parse(ds.Tables[0].Rows[i]["DimissionStatus"].ToString());
        //            else
        //                model.DimissionStatus = 0;
        //            model.PrivateEmail = ds.Tables[0].Rows[i]["PrivateEmail"].ToString();
        //            if (ds.Tables[0].Rows[i]["OwnedPC"].ToString() != "")
        //            {
        //                if ((ds.Tables[0].Rows[i]["OwnedPC"].ToString() == "1") || (ds.Tables[0].Rows[i]["OwnedPC"].ToString().ToLower() == "true"))
        //                {
        //                    model.OwnedPC = true;
        //                }
        //                else
        //                {
        //                    model.OwnedPC = false;
        //                }
        //            }

        //            if (ds.Tables[0].Rows[i]["OfferLetterTemplate"].ToString() != "")
        //            {
        //                model.OfferLetterTemplate = int.Parse(ds.Tables[0].Rows[i]["OfferLetterTemplate"].ToString());
        //            }
        //            if (ds.Tables[0].Rows[i]["OfferLetterSender"].ToString() != "")
        //            {
        //                model.OfferLetterSender = int.Parse(ds.Tables[0].Rows[i]["OfferLetterSender"].ToString());
        //            }
        //            var objOfferLetterSendTime = ds.Tables[0].Rows[i]["OfferLetterSendTime"];
        //            if (!(objOfferLetterSendTime is DBNull))
        //            {
        //                model.OfferLetterSendTime = (DateTime)objOfferLetterSendTime;
        //            }
        //            if (ds.Tables[0].Rows[i]["IsExamen"].ToString() != "")
        //            {
        //                if ((ds.Tables[0].Rows[i]["IsExamen"].ToString() == "1") || (ds.Tables[0].Rows[i]["IsExamen"].ToString().ToLower() == "true"))
        //                {
        //                    model.IsExamen = true;
        //                }
        //                else
        //                {
        //                    model.IsExamen = false;
        //                }
        //            }
        //            if (ds.Tables[0].Rows[i]["Seniority"].ToString() != "")
        //            {
        //                model.Seniority = int.Parse(ds.Tables[0].Rows[i]["Seniority"].ToString());
        //            }
        //            if (ds.Tables[0].Rows[i]["EmpProperty"].ToString() != "")
        //            {
        //                model.EmpProperty = int.Parse(ds.Tables[0].Rows[i]["EmpProperty"].ToString());
        //            }
        //            model.Residence = ds.Tables[0].Rows[i]["Residence"].ToString();
        //            model.MateName = ds.Tables[0].Rows[i]["MateName"].ToString();
        //            model.AdrressNow = ds.Tables[0].Rows[i]["AdrressNow"].ToString();
        //            model.PostCodeNow = ds.Tables[0].Rows[i]["PostCodeNow"].ToString();
        //            model.FamillyDesc = ds.Tables[0].Rows[i]["FamillyDesc"].ToString();
        //            model.Political = ds.Tables[0].Rows[i]["Political"].ToString();
        //            model.Nation = ds.Tables[0].Rows[i]["Nation"].ToString();

        //            model.Appearance = ds.Tables[0].Rows[i]["Appearance"].ToString();
        //            model.Quality = ds.Tables[0].Rows[i]["Quality"].ToString();
        //            model.Know = ds.Tables[0].Rows[i]["Know"].ToString();
        //            model.Equal = ds.Tables[0].Rows[i]["Equal"].ToString();
        //            model.Motivation = ds.Tables[0].Rows[i]["Motivation"].ToString();
        //            model.FourD = ds.Tables[0].Rows[i]["FourD"].ToString();
        //            model.EQ = ds.Tables[0].Rows[i]["EQ"].ToString();
        //            if (ds.Tables[0].Rows[i]["workBegin"].ToString() != "")
        //            {
        //                model.WorkBegin = DateTime.Parse(ds.Tables[0].Rows[i]["workBegin"].ToString());
        //            }
        //            if (ds.Tables[0].Rows[i]["SocialSecurityType"].ToString() != "")
        //            {
        //                model.SocialSecurityType = int.Parse(ds.Tables[0].Rows[i]["SocialSecurityType"].ToString());
        //            }
        //            if (ds.Tables[0].Rows[i]["JoinDate"].ToString() != "")
        //            {
        //                model.JoinDate = DateTime.Parse(ds.Tables[0].Rows[i]["JoinDate"].ToString());
        //            }

        //            model.BranchCode = ds.Tables[0].Rows[i]["BranchCode"].ToString();

        //            if (ds.Tables[0].Rows[i]["ContractYear"].ToString() != "")
        //            {
        //                model.ContractYear = int.Parse(ds.Tables[0].Rows[i]["ContractYear"].ToString());
        //            }
        //            if (ds.Tables[0].Rows[i]["ContractBeginDate"].ToString() != "")
        //            {
        //                model.ContractBeginDate = DateTime.Parse(ds.Tables[0].Rows[i]["ContractBeginDate"].ToString());
        //            }
        //            if (ds.Tables[0].Rows[i]["ContractEndDate"].ToString() != "")
        //            {
        //                model.ContractEndDate = DateTime.Parse(ds.Tables[0].Rows[i]["ContractEndDate"].ToString());
        //            }

        //            if (ds.Tables[0].Rows[i]["FirstContractBeginDate"].ToString() != "")
        //            {
        //                model.FirstContractBeginDate = DateTime.Parse(ds.Tables[0].Rows[i]["FirstContractBeginDate"].ToString());
        //            }
        //            if (ds.Tables[0].Rows[i]["FirstContractEndDate"].ToString() != "")
        //            {
        //                model.FirstContractEndDate = DateTime.Parse(ds.Tables[0].Rows[i]["FirstContractEndDate"].ToString());
        //            }

        //            if (ds.Tables[0].Rows[i]["ContractSignDate"].ToString() != "")
        //            {
        //                model.ContractSignDate = DateTime.Parse(ds.Tables[0].Rows[i]["ContractSignDate"].ToString());
        //            }
        //            if (ds.Tables[0].Rows[i]["AnnualLeaveBase"].ToString() != "")
        //            {
        //                model.AnnualLeaveBase = int.Parse(ds.Tables[0].Rows[i]["AnnualLeaveBase"].ToString());
        //            }
        //            if (ds.Tables[0].Rows[i]["ProbationDate"].ToString() != "")
        //            {
        //                model.ProbationDate = DateTime.Parse(ds.Tables[0].Rows[i]["ProbationDate"].ToString());
        //            }

        //            if (ds.Tables[0].Rows[i]["HasChild"].ToString() != "")
        //            {
        //                if ((ds.Tables[0].Rows[i]["HasChild"].ToString() == "1") || (ds.Tables[0].Rows[i]["HasChild"].ToString().ToLower() == "true"))
        //                {
        //                    model.HasChild = true;
        //                }
        //                else
        //                {
        //                    model.HasChild = false;
        //                }
        //            }
        //            model.SalaryBank = ds.Tables[0].Rows[i]["SalaryBank"].ToString();
        //            model.SalaryCardNo = ds.Tables[0].Rows[i]["SalaryCardNo"].ToString();

        //            if (ds.Tables[0].Rows[i]["Pay"].ToString() != "")
        //            {
        //                model.Pay = decimal.Parse(ds.Tables[0].Rows[i]["Pay"].ToString());
        //            }
        //            if (ds.Tables[0].Rows[i]["Performance"].ToString() != "")
        //            {
        //                model.Performance = decimal.Parse(ds.Tables[0].Rows[i]["Performance"].ToString());
        //            }
        //            if (ds.Tables[0].Rows[i]["Attendance"].ToString() != "")
        //            {
        //                model.Attendance = decimal.Parse(ds.Tables[0].Rows[i]["Attendance"].ToString());
        //            }

        //            if (ds.Tables[0].Rows[i]["IDValid"].ToString() != "")
        //            {
        //                model.IDValid = DateTime.Parse(ds.Tables[0].Rows[i]["IDValid"].ToString());
        //            }

        //            list.Add(model);
        //        }
        //    }
        //    return list;
        //}

        public int updateStatus(int id, int status, SqlTransaction trans)
        {
            string sql = string.Format(" update sep_Employees set Status={0} where UserID = {1}", status, id);
            return DbHelperSQL.ExecuteSql(sql, trans.Connection, trans, null);
        }

        public void updateUserPhoto(int id, string photofilename)
        {
            string sql = string.Format(" update sep_Employees set photo='{0}' where UserID = {1}", photofilename, id);
            DbHelperSQL.ExecuteSql(sql);
        }

        public bool checkUserCodeExists(string username, int userid)
        {
            bool isExists = false;
            string sql = "select userid from sep_Users where rtrim(username) = '" + username + "' and userid <> " + userid;
            if ((int)DbHelperSQL.Query(sql).Tables[0].Rows.Count != 0)
            {
                return true;
            }
            return isExists;
        }

        public bool checkUserCodeExists(string username)
        {
            bool isExists = false;
            string sql = "select userid from sep_employees where InternalEmail = '" + username + "@xc-ch.com'";
            if ((int)DbHelperSQL.Query(sql).Tables[0].Rows.Count != 0)
            {
                return true;
            }
            return isExists;
        }

        /// <summary>
        /// 用户Code得到一个对象实体
        /// </summary>
        public EmployeeBaseInfo GetModel(string Code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TOP 1 * FROM sep_Employees ");
            strSql.Append(" WHERE Code=@Code and status in(1,3)");
            SqlParameter[] parameters = {
					new SqlParameter("@Code", SqlDbType.NVarChar,128)};
            parameters[0].Value = Code;

            EmployeeBaseInfo model = new EmployeeBaseInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.EmployeeJobInfo = EmployeeJobManager.getModelBySysId(int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString()));
                if (model.EmployeeJobInfo == null)
                {
                    model.EmployeeJobInfo = new EmployeeJobInfo();
                }
                model.EmployeeWelfareInfo = EmployeeWelfareManager.getModelBySysId(int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString()));
                if (model.EmployeeWelfareInfo == null)
                {
                    model.EmployeeWelfareInfo = new EmployeeWelfareInfo();
                }
                if (ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                // model.Username = ds.Tables[0].Rows[0]["Username"].ToString();
                model.Code = ds.Tables[0].Rows[0]["Code"].ToString();
                model.IM = ds.Tables[0].Rows[0]["IM"].ToString();
                model.EmergencyContact = ds.Tables[0].Rows[0]["EmergencyContact"].ToString();
                model.EmergencyContactPhone = ds.Tables[0].Rows[0]["EmergencyContactPhone"].ToString();
                model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                model.City = ds.Tables[0].Rows[0]["City"].ToString();
                model.Province = ds.Tables[0].Rows[0]["Province"].ToString();
                model.Country = ds.Tables[0].Rows[0]["Country"].ToString();
                model.PostCode = ds.Tables[0].Rows[0]["PostCode"].ToString();
                model.Address2 = ds.Tables[0].Rows[0]["Adress2"].ToString();
                model.City2 = ds.Tables[0].Rows[0]["City2"].ToString();
                model.Code = ds.Tables[0].Rows[0]["Code"].ToString();
                model.Province2 = ds.Tables[0].Rows[0]["Province2"].ToString();
                model.Country2 = ds.Tables[0].Rows[0]["Country2"].ToString();
                model.PostCode2 = ds.Tables[0].Rows[0]["PostCode2"].ToString();
                model.WorkAddress = ds.Tables[0].Rows[0]["WorkAddress"].ToString();
                model.WorkCity = ds.Tables[0].Rows[0]["WorkCity"].ToString();
                model.WorkProvince = ds.Tables[0].Rows[0]["WorkProvince"].ToString();
                model.WorkCountry = ds.Tables[0].Rows[0]["WorkCountry"].ToString();
                model.WorkPostCode = ds.Tables[0].Rows[0]["WorkPostCode"].ToString();
                if (ds.Tables[0].Rows[0]["MaritalStatus"].ToString() != "")
                {
                    model.MaritalStatus = int.Parse(ds.Tables[0].Rows[0]["MaritalStatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Gender"].ToString() != "")
                {
                    model.Gender = int.Parse(ds.Tables[0].Rows[0]["Gender"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TypeID"].ToString() != "")
                {
                    model.TypeID = int.Parse(ds.Tables[0].Rows[0]["TypeID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Birthday"].ToString() != "")
                {
                    model.Birthday = DateTime.Parse(ds.Tables[0].Rows[0]["Birthday"].ToString());
                }
                model.BirthPlace = ds.Tables[0].Rows[0]["BirthPlace"].ToString();
                model.IDNumber = ds.Tables[0].Rows[0]["IDNumber"].ToString();
                model.DomicilePlace = ds.Tables[0].Rows[0]["DomicilePlace"].ToString();
                model.Photo = ds.Tables[0].Rows[0]["Photo"].ToString();
                model.Degree = ds.Tables[0].Rows[0]["Degree"].ToString();
                model.Education = ds.Tables[0].Rows[0]["Education"].ToString();
                model.GraduateFrom = ds.Tables[0].Rows[0]["GraduatedFrom"].ToString();
                model.Major = ds.Tables[0].Rows[0]["Major"].ToString();
                if (ds.Tables[0].Rows[0]["GraduatedDate"].ToString() != "")
                {
                    model.GraduatedDate = DateTime.Parse(ds.Tables[0].Rows[0]["GraduatedDate"].ToString());
                }
                model.Health = ds.Tables[0].Rows[0]["Health"].ToString();
                model.Phone1 = ds.Tables[0].Rows[0]["Phone1"].ToString();
                model.DiseaseInSixMonths = ds.Tables[0].Rows[0]["DiseaseInSixMonths"].ToString();
                model.DiseaseInSixMonthsInfo = ds.Tables[0].Rows[0]["DiseaseInSixMonthsInfo"].ToString();
                model.WorkExperience = ds.Tables[0].Rows[0]["WorkExperience"].ToString();
                model.WorkSpecialty = ds.Tables[0].Rows[0]["WorkSpecialty"].ToString();
                model.ThisYearSalary = ds.Tables[0].Rows[0]["ThisYearSalary"].ToString();

                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                model.Memo = ds.Tables[0].Rows[0]["Memo"].ToString();
                if (ds.Tables[0].Rows[0]["BaseInfoOK"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["BaseInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[0]["BaseInfoOK"].ToString().ToLower() == "true"))
                    {
                        model.BaseInfoOK = true;
                    }
                    else
                    {
                        model.BaseInfoOK = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["ContractInfoOK"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["ContractInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[0]["ContractInfoOK"].ToString().ToLower() == "true"))
                    {
                        model.ContractInfoOK = true;
                    }
                    else
                    {
                        model.ContractInfoOK = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["InsuranceInfoOK"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["InsuranceInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[0]["InsuranceInfoOK"].ToString().ToLower() == "true"))
                    {
                        model.InsuranceInfoOK = true;
                    }
                    else
                    {
                        model.InsuranceInfoOK = false;
                    }
                }
                model.Phone2 = ds.Tables[0].Rows[0]["Phone2"].ToString();
                if (ds.Tables[0].Rows[0]["ArchiveInfoOK"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["ArchiveInfoOK"].ToString() == "1") || (ds.Tables[0].Rows[0]["ArchiveInfoOK"].ToString().ToLower() == "true"))
                    {
                        model.ArchiveInfoOK = true;
                    }
                    else
                    {
                        model.ArchiveInfoOK = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Creator"].ToString() != "")
                {
                    model.Creator = int.Parse(ds.Tables[0].Rows[0]["Creator"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreatedTime"].ToString() != "")
                {
                    model.CreatedTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreatedTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastModifier"].ToString() != "")
                {
                    model.LastModifier = int.Parse(ds.Tables[0].Rows[0]["LastModifier"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastModifiedTime"].ToString() != "")
                {
                    model.LastModifiedTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastModifiedTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RowVersion"].ToString() != "")
                {
                    model.RowVersion = (byte[])ds.Tables[0].Rows[0]["RowVersion"];
                }
                model.MobilePhone = ds.Tables[0].Rows[0]["MobilePhone"].ToString();
                model.HomePhone = ds.Tables[0].Rows[0]["HomePhone"].ToString();
                model.Fax = ds.Tables[0].Rows[0]["Fax"].ToString();
                model.InternalEmail = ds.Tables[0].Rows[0]["InternalEmail"].ToString();
                model.Resume = ds.Tables[0].Rows[0]["Resume"].ToString();
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
                model.IPCode = ds.Tables[0].Rows[0]["IPCode"].ToString();
                if (ds.Tables[0].Rows[0]["IsSendMail"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsSendMail"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsSendMail"].ToString().ToLower() == "true"))
                    {
                        model.IsSendMail = true;
                    }
                    else
                    {
                        model.IsSendMail = false;
                    }
                }
                model.CommonName = ds.Tables[0].Rows[0]["CommonName"].ToString();

                if (ds.Tables[0].Rows[0]["DimissionStatus"] != DBNull.Value)
                    model.DimissionStatus = int.Parse(ds.Tables[0].Rows[0]["DimissionStatus"].ToString());
                else
                    model.DimissionStatus = 0;
                model.PrivateEmail = ds.Tables[0].Rows[0]["PrivateEmail"].ToString();
                if (ds.Tables[0].Rows[0]["OwnedPC"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["OwnedPC"].ToString() == "1") || (ds.Tables[0].Rows[0]["OwnedPC"].ToString().ToLower() == "true"))
                    {
                        model.OwnedPC = true;
                    }
                    else
                    {
                        model.OwnedPC = false;
                    }
                }

                if (ds.Tables[0].Rows[0]["OfferLetterTemplate"].ToString() != "")
                {
                    model.OfferLetterTemplate = int.Parse(ds.Tables[0].Rows[0]["OfferLetterTemplate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OfferLetterSender"].ToString() != "")
                {
                    model.OfferLetterSender = int.Parse(ds.Tables[0].Rows[0]["OfferLetterSender"].ToString());
                }
                var objOfferLetterSendTime = ds.Tables[0].Rows[0]["OfferLetterSendTime"];
                if (!(objOfferLetterSendTime is DBNull))
                {
                    model.OfferLetterSendTime = (DateTime)objOfferLetterSendTime;
                }
                if (ds.Tables[0].Rows[0]["IsExamen"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsExamen"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsExamen"].ToString().ToLower() == "true"))
                    {
                        model.IsExamen = true;
                    }
                    else
                    {
                        model.IsExamen = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["Seniority"].ToString() != "")
                {
                    model.Seniority = int.Parse(ds.Tables[0].Rows[0]["Seniority"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EmpProperty"].ToString() != "")
                {
                    model.EmpProperty = int.Parse(ds.Tables[0].Rows[0]["EmpProperty"].ToString());
                }
                model.Residence = ds.Tables[0].Rows[0]["Residence"].ToString();
                model.MateName = ds.Tables[0].Rows[0]["MateName"].ToString();
                model.AdrressNow = ds.Tables[0].Rows[0]["AdrressNow"].ToString();
                model.PostCodeNow = ds.Tables[0].Rows[0]["PostCodeNow"].ToString();
                model.FamillyDesc = ds.Tables[0].Rows[0]["FamillyDesc"].ToString();
                model.Political = ds.Tables[0].Rows[0]["Political"].ToString();
                model.Nation = ds.Tables[0].Rows[0]["Nation"].ToString();

                model.Appearance = ds.Tables[0].Rows[0]["Appearance"].ToString();
                model.Quality = ds.Tables[0].Rows[0]["Quality"].ToString();
                model.Know = ds.Tables[0].Rows[0]["Know"].ToString();
                model.Equal = ds.Tables[0].Rows[0]["Equal"].ToString();
                model.Motivation = ds.Tables[0].Rows[0]["Motivation"].ToString();
                model.FourD = ds.Tables[0].Rows[0]["FourD"].ToString();
                model.EQ = ds.Tables[0].Rows[0]["EQ"].ToString();

                if (ds.Tables[0].Rows[0]["workBegin"].ToString() != "")
                {
                    model.WorkBegin = DateTime.Parse(ds.Tables[0].Rows[0]["workBegin"].ToString());
                }

                if (ds.Tables[0].Rows[0]["JoinDate"].ToString() != "")
                {
                    model.JoinDate = DateTime.Parse(ds.Tables[0].Rows[0]["JoinDate"].ToString());
                }

                model.BranchCode = ds.Tables[0].Rows[0]["BranchCode"].ToString();

                if (ds.Tables[0].Rows[0]["ContractYear"].ToString() != "")
                {
                    model.ContractYear = int.Parse(ds.Tables[0].Rows[0]["ContractYear"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ContractBeginDate"].ToString() != "")
                {
                    model.ContractBeginDate = DateTime.Parse(ds.Tables[0].Rows[0]["ContractBeginDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ContractEndDate"].ToString() != "")
                {
                    model.ContractEndDate = DateTime.Parse(ds.Tables[0].Rows[0]["ContractEndDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FirstContractBeginDate"].ToString() != "")
                {
                    model.FirstContractBeginDate = DateTime.Parse(ds.Tables[0].Rows[0]["FirstContractBeginDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FirstContractEndDate"].ToString() != "")
                {
                    model.FirstContractEndDate = DateTime.Parse(ds.Tables[0].Rows[0]["FirstContractEndDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ContractSignDate"].ToString() != "")
                {
                    model.ContractSignDate = DateTime.Parse(ds.Tables[0].Rows[0]["ContractSignDate"].ToString());
                }


                if (ds.Tables[0].Rows[0]["SocialSecurityType"].ToString() != "")
                {
                    model.SocialSecurityType = int.Parse(ds.Tables[0].Rows[0]["SocialSecurityType"].ToString());
                }

                if (ds.Tables[0].Rows[0]["AnnualLeaveBase"].ToString() != "")
                {
                    model.AnnualLeaveBase = int.Parse(ds.Tables[0].Rows[0]["AnnualLeaveBase"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ProbationDate"].ToString() != "")
                {
                    model.ProbationDate = DateTime.Parse(ds.Tables[0].Rows[0]["ProbationDate"].ToString());
                }

                if (ds.Tables[0].Rows[0]["HasChild"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["HasChild"].ToString() == "1") || (ds.Tables[0].Rows[0]["HasChild"].ToString().ToLower() == "true"))
                    {
                        model.HasChild = true;
                    }
                    else
                    {
                        model.HasChild = false;
                    }
                }

                model.SalaryBank = ds.Tables[0].Rows[0]["SalaryBank"].ToString();
                model.SalaryCardNo = ds.Tables[0].Rows[0]["SalaryCardNo"].ToString();

                if (ds.Tables[0].Rows[0]["Pay"].ToString() != "")
                {
                    model.Pay = decimal.Parse(ds.Tables[0].Rows[0]["Pay"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Performance"].ToString() != "")
                {
                    model.Performance = decimal.Parse(ds.Tables[0].Rows[0]["Performance"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Attendance"].ToString() != "")
                {
                    model.Attendance = decimal.Parse(ds.Tables[0].Rows[0]["Attendance"].ToString());
                }

                if (ds.Tables[0].Rows[0]["IDValid"].ToString() != "")
                {
                    model.IDValid = DateTime.Parse(ds.Tables[0].Rows[0]["IDValid"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 用户Code得到一个对象实体
        /// </summary>
        public int GetModelByMobile(string mobile)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT count(*) FROM sep_Employees ");
            strSql.Append(" WHERE replace(replace(Phone2,'-',''),' ','')=@mobile and status in(1,3) ");
            SqlParameter[] parameters = {
					new SqlParameter("@mobile", SqlDbType.NVarChar,32)};
            parameters[0].Value = mobile;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            return int.Parse(ds.Tables[0].Rows[0][0].ToString());

        }

        /// <summary>
        /// 获得用户的工作地部门ID数组
        /// </summary>
        /// <param name="userids">用户ID字符串</param>
        /// <returns></returns>
        public string[] GetUserWorkDepartmentID(string userids)
        {
            string[] deps;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select WorkCountry ");
            strSql.Append("FROM sep_Employees ");

            if (userids.Trim() != "")
            {
                strSql.Append(" where userid in (" + userids + ") ");
            }
            strSql.Append(" group by WorkCountry");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                deps = new string[ds.Tables[0].Rows.Count];
                for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
                {
                    deps[n] = ds.Tables[0].Rows[n]["WorkCountry"].ToString();
                }
            }
            else
            {
                deps = new string[0];
            }
            return deps;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetEmployeeList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT a.*, b.Username, b.FirstNameCN, b.LastNameCN, b.FirstNameEN,b.LastNameCN+b.FirstNameCN as FullNameCn,");
            strSql.Append(" b.LastNameEN, b.Email, b.CreatedDate, b.LastActivityDate, b.Password, b.PasswordSalt, b.IsApproved, b.IsLockedOut, b.LastLoginDate,");
            strSql.Append(" b.LastPasswordChangedDate, b.LastLockoutDate, b.FailedPasswordAttemptCount, b.FailedPasswordAttemptWindowStart, b.Comment,b.ResetPasswordCode, b.IsDeleted,c.ContractCompany, ");
            strSql.Append(" j.companyName, j.companyID, j.departmentName,j.departmentID,j.groupName,j.groupID,j.joinJob");
            strSql.Append(" FROM sep_Employees a left join sep_Users b on a.UserID=b.UserID left join sep_EmployeeWelfareInfo c on a.UserID=c.sysid left join sep_EmployeeJobInfo j on a.UserID=j.sysid  ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where 1=1 " + strWhere);
            }
            strSql.Append(" order by a.joinDate desc");
            return DbHelperSQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAddressBookList(int userid)
        {
            string sql = @"
SELECT u.userid,e.code,u.username,u.lastnamecn+u.firstnamecn as cnname,e.Phone1,e.MobilePhone,P.DepartmentPositionName,u.Email,
	d.level1id,d.level1,d.level2id,d.level2,d.level3id,d.level3,
	dep.DepartmentID,dep.DepartmentName,e.workCity
FROM sep_users u
LEFT JOIN sep_employees e on u.userid=e.userid  
LEFT JOIN sep_employeejobinfo ej on u.userid=ej.sysid
LEFT JOIN SEP_EmployeesInPositions eip ON u.userid = eip.userid
LEFT JOIN SEP_DepartmentPositions p ON p.DepartmentPositionid=eip.Departmentpositionid
LEFT JOIN V_Department d ON eip.Departmentid = d.level3Id
LEFT JOIN sep_Departments dep ON dep.DepartmentID = d.level3Id 
LEFT JOIN ad_OperationAuditManage au ON u.userid =au.userid
WHERE e.status in (1,2,3,4,7,8) AND dep.DepartmentName not like '%作废%'";
            if (userid != 0)
                sql += " and au.hradminid = " + userid.ToString();
            sql += " ORDER BY dep.DepartmentCode asc, p.PositionLevel asc";
            return DbHelperSQL.Query(sql);
        }
        #endregion  成员方法
    }
}
