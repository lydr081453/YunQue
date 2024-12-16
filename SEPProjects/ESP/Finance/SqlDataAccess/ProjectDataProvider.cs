using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ESP.Finance.Entity;
using ESP.Finance.Utility;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using ESP.Framework.Entity;
using System.Web;

namespace ESP.Finance.DataAccess
{
    /// <summary>
    /// 数据访问类ProjectDAL。
    /// </summary>
    public class ProjectDataProvider : ESP.Finance.IDataAccess.IProjectDataProvider
    {

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ProjectId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from F_Project");
            strSql.Append(" where ProjectId=@ProjectId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectId", SqlDbType.Int,4)};
            parameters[0].Value = ProjectId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        ///// <summary>
        ///// 是否存在该记录
        ///// </summary>
        //public bool Exists(string term, List<SqlParameter> param)
        //{
        //    return Exists(term, param, false);
        //}

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*)");
            strSql.Append(" FROM F_Project ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            if (param != null && param.Count > 0)
            {
                return DbHelperSQL.Exists(strSql.ToString(), param.ToArray());
            }
            return DbHelperSQL.Exists(term);
        }


        //public string CreateSerialCode()
        //{
        //    return CreateSerialCode(false);
        //}

        public string CreateSerialCode()
        {
            string prefix = "PA";
            string date = DateTime.Now.ToString("yyMMdd");
            string strSql = "select max(SerialCode) as maxId from F_Project as a where a.SerialCode like '" + prefix + date + "%'";

            object maxid = DbHelperSQL.GetSingle(strSql);
            int no = maxid == null ? 0 : Convert.ToInt32(maxid.ToString().Substring(8));
            no++;
            return prefix + date + no.ToString("00");
        }

        //        /// <summary>
        ///// 公司代码 + 客户缩写 + 项目类型 + 年月 + 001 - 999 
        ///// </summary>
        ///// <param name="model"></param>
        ///// <param name="isInTrans"></param>
        ///// <returns></returns>
        //public string CreateProjectCode(DateTime DeadLine, ESP.Finance.Entity.ProjectInfo model)
        //{
        //    return CreateProjectCode(DeadLine,model, false);
        //}

        /// <summary>
        /// 公司代码 + 客户缩写 + 项目类型 + 年月 + 001 - 999 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isInTrans"></param>
        /// <returns></returns>
        public string CreateProjectCode(DateTime DeadLine, ESP.Finance.Entity.ProjectInfo model)
        {
            //if (!string.IsNullOrEmpty(model.ProjectCode)) return model.ProjectCode;
            if (DeadLine <= new DateTime(1900, 1, 1)) DeadLine = DateTime.Now;
            DateTime now = DeadLine;//DateTime.Now;
            string date = now.ToString("yyyyMM");
            string branchcode = string.IsNullOrEmpty(model.BranchCode) ? "_" : model.BranchCode.Trim().Substring(0, 1);
            string customerShortEN = (model.Customer != null && !string.IsNullOrEmpty(model.Customer.ShortEN)) ? model.Customer.ShortEN.Trim().Substring(0, 4) : "____";
            string projectType = string.IsNullOrEmpty(model.ProjectTypeCode) ? "_" : model.ProjectTypeCode.Trim().Substring(0, 1);
            if (model.ContractStatusName == ESP.Finance.Utility.ProjectType.BDProject)
            {
                projectType = "P";
            }

            string pre = branchcode + "-" + customerShortEN + "-" + projectType + "-" + date;

            string strSql = "select max((substring(ProjectCode,10,9))) as maxId from F_Project as a where a.ProjectCode like '" + branchcode + "-____-_-" + date + "%'";

            object maxid = DbHelperSQL.GetSingle(strSql);
            int no = maxid == null ? 0 : Convert.ToInt32(maxid.ToString().Substring(6, 3));
            no++;
            //如果广告投放类发票
            if (model.ContractTaxID == null || model.ContractTaxID.Value == 0)
            {
                return pre + no.ToString("000") + "-V";
            }
            else
            {
                ESP.Finance.Entity.TaxRateInfo rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(model.ContractTaxID.Value);
                if (rateModel != null && rateModel.Remark == "广告投放类发票")
                    return pre + no.ToString("000") + "-D";
                else
                    return pre + no.ToString("000") + "-V";
            }

        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.ProjectInfo model)
        {
            return Add(model, null);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.ProjectInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into F_Project(");
            strSql.Append(@"BranchCode,BusinessTypeID,BusinessTypeName,GroupID,
                            GroupName,ProjectTypeID,ProjectTypeName,ProjectTypeCode,
                            BusinessDesID,BusinessDescription,ProjectCode,BeginDate,
                            EndDate,TotalAmount,ContractServiceFee,ContractTaxID,
                            ContractTax,ContractCostDetail,CustomerID,IsNeedInvoice,
                            PayCycle,ContractStatusID,OtherRequest,CreatorID,CreatorName,
                            CreatorCode,CreatorUserID,ApplicantUserID,ApplicantUserName,
                            ApplicantCode,ApplicantEmployeeName,ApplicantUserEmail,ApplicantUserPhone,ApplicantUserPosition,
                            LeaderUserID,ContractStatusName,
                            LeaderUserName,LeaderCode,LeaderEmployeeName,CreateDate,SubmitDate,
                            Status,Step,Attachment,AttachType,BDProjectID,BDProjectCode,IsFromJoint,
                            BranchID,BranchName,SerialCode,AuditorSysUserID,AuditorUserCode,
                            AuditorUserName,AuditorEmployeeName,LastAuditDate,Del,
                            CustomerRemark,Remark,OldFlag,InUse,CustomerCode,BankId,ProfileReason,IsCalculateByVAT,IsDigital,ProjectTypeLevel2ID,ProjectTypeLevel3ID,CustomerAttachID,Brands,MediaCostRate,CustomerRebateRate,MediaId,BusinessPersonId,BusinessPersonName,AdvertiserID,CustomerProjectCode,RelevanceProjectId,RelevanceProjectCode)");
            strSql.Append(" values (");
            strSql.Append(@"@BranchCode,@BusinessTypeID,@BusinessTypeName,@GroupID,
                            @GroupName,@ProjectTypeID,@ProjectTypeName,@ProjectTypeCode,
                            @BusinessDesID,@BusinessDescription,@ProjectCode,@BeginDate,
                            @EndDate,@TotalAmount,@ContractServiceFee,@ContractTaxID,
                            @ContractTax,@ContractCostDetail,@CustomerID,@IsNeedInvoice,
                            @PayCycle,@ContractStatusID,@OtherRequest,@CreatorID,@CreatorName,
                            @CreatorCode,@CreatorUserID,@ApplicantUserID,@ApplicantUserName,
                            @ApplicantCode,@ApplicantEmployeeName,@ApplicantUserEmail,@ApplicantUserPhone,@ApplicantUserPosition,
                            @LeaderUserID,@ContractStatusName,
                            @LeaderUserName,@LeaderCode,@LeaderEmployeeName,@CreateDate,@SubmitDate,
                            @Status,@Step,@Attachment,@AttachType,@BDProjectID,@BDProjectCode,@IsFromJoint,
                            @BranchID,@BranchName,@SerialCode,@AuditorSysUserID,@AuditorUserCode,
                            @AuditorUserName,@AuditorEmployeeName,@LastAuditDate,@Del,
                            @CustomerRemark,@Remark,0,@InUse,@CustomerCode,@BankId,@ProfileReason,@IsCalculateByVAT,@IsDigital,@ProjectTypeLevel2ID,@ProjectTypeLevel3ID,@CustomerAttachID,@Brands,@MediaCostRate,@CustomerRebateRate,@MediaId,@BusinessPersonId,@BusinessPersonName,@AdvertiserID,@CustomerProjectCode,@RelevanceProjectId,@RelevanceProjectCode)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@BranchCode", SqlDbType.NVarChar,10),
					new SqlParameter("@BusinessTypeID", SqlDbType.Int,4),
					new SqlParameter("@BusinessTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@GroupName", SqlDbType.NVarChar,100),
					new SqlParameter("@ProjectTypeID", SqlDbType.Int,4),
					new SqlParameter("@ProjectTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectTypeCode", SqlDbType.NVarChar,50),
					new SqlParameter("@BusinessDesID", SqlDbType.Int,4),
					new SqlParameter("@BusinessDescription", SqlDbType.NVarChar,2000),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@BeginDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@TotalAmount", SqlDbType.Decimal,9),
					new SqlParameter("@ContractServiceFee", SqlDbType.Decimal,9),
					new SqlParameter("@ContractTaxID", SqlDbType.Int,4),
					new SqlParameter("@ContractTax", SqlDbType.Decimal,9),
					new SqlParameter("@ContractCostDetail", SqlDbType.NVarChar),
					new SqlParameter("@CustomerID", SqlDbType.Int,4),
					new SqlParameter("@IsNeedInvoice", SqlDbType.Int,4),
					new SqlParameter("@PayCycle", SqlDbType.NVarChar,500),
					new SqlParameter("@ContractStatusID", SqlDbType.Int,4),
					new SqlParameter("@OtherRequest", SqlDbType.NVarChar,2000),
					new SqlParameter("@CreatorID", SqlDbType.Int,4),
					new SqlParameter("@CreatorName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorUserID", SqlDbType.NVarChar,50),
					new SqlParameter("@ApplicantUserID", SqlDbType.Int,4),
					new SqlParameter("@ApplicantUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@ApplicantCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ApplicantEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@LeaderUserID", SqlDbType.Int,4),
					new SqlParameter("@ContractStatusName", SqlDbType.NVarChar,50),
					new SqlParameter("@LeaderUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@LeaderCode", SqlDbType.NVarChar,50),
					new SqlParameter("@LeaderEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@SubmitDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Step", SqlDbType.Int,4),
					new SqlParameter("@Attachment", SqlDbType.NVarChar,500),
					new SqlParameter("@AttachType", SqlDbType.Int,4),
					//new SqlParameter("@Lastupdatetime", SqlDbType.Timestamp,8),
					new SqlParameter("@BDProjectID", SqlDbType.Int,4),
					new SqlParameter("@BDProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@IsFromJoint", SqlDbType.Bit,1),
					new SqlParameter("@BranchID", SqlDbType.Int,4),
					new SqlParameter("@BranchName", SqlDbType.NVarChar,50),
                    new SqlParameter("@SerialCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@AuditorSysUserID", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserCode", SqlDbType.NVarChar,10),
					new SqlParameter("@AuditorUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorEmployeeName", SqlDbType.NVarChar,20),
					new SqlParameter("@LastAuditDate", SqlDbType.DateTime,8),
                    new SqlParameter("@Del", SqlDbType.Int,4),
                    new SqlParameter("@CustomerRemark",SqlDbType.NVarChar,100),
                    new SqlParameter("@Remark",SqlDbType.NVarChar,100),
                    new SqlParameter("@ApplicantUserEmail", SqlDbType.NVarChar,50),
                    new SqlParameter("@ApplicantUserPhone",SqlDbType.NVarChar,50),
                    new SqlParameter("@ApplicantUserPosition",SqlDbType.NVarChar,50),
                    new SqlParameter("@InUse",SqlDbType.Int,4),
                    new SqlParameter("@CustomerCode",SqlDbType.NVarChar,50),
                    new SqlParameter("@BankId",SqlDbType.Int,4),
                     new SqlParameter("@ProfileReason",SqlDbType.NVarChar,500),
                     new SqlParameter("@IsCalculateByVAT",SqlDbType.Int,4),
                      new SqlParameter("@IsDigital",SqlDbType.Int,4),
                      new SqlParameter("@ProjectTypeLevel2ID",SqlDbType.Int,4),
                      new SqlParameter("@ProjectTypeLevel3ID",SqlDbType.Int,4),
                      new SqlParameter("@CustomerAttachID",SqlDbType.NVarChar,200),
                      new SqlParameter("@Brands",SqlDbType.NVarChar,1000),
                      new SqlParameter("@MediaCostRate",SqlDbType.Decimal,9),
                      new SqlParameter("@CustomerRebateRate",SqlDbType.Decimal,9),
                      new SqlParameter("@MediaId",SqlDbType.Int,4),
                      new SqlParameter("@BusinessPersonId",SqlDbType.Int,4),
                      new SqlParameter("@BusinessPersonName",SqlDbType.NVarChar,50),
                      new SqlParameter("@AdvertiserID",SqlDbType.NVarChar,50),
                      new SqlParameter("@CustomerProjectCode",SqlDbType.NVarChar,50),
                                           new SqlParameter("@RelevanceProjectId",SqlDbType.Int,4),
                     new SqlParameter("@RelevanceProjectCode",SqlDbType.NVarChar,50),
                                        };
            parameters[0].Value = model.BranchCode;
            parameters[1].Value = model.BusinessTypeID;
            parameters[2].Value = model.BusinessTypeName;
            parameters[3].Value = model.GroupID;
            parameters[4].Value = model.GroupName;
            parameters[5].Value = model.ProjectTypeID;
            parameters[6].Value = model.ProjectTypeName;
            parameters[7].Value = model.ProjectTypeCode;
            parameters[8].Value = model.BusinessDesID;
            parameters[9].Value = model.BusinessDescription;
            parameters[10].Value = model.ProjectCode;
            parameters[11].Value = model.BeginDate;
            parameters[12].Value = model.EndDate;
            parameters[13].Value = model.TotalAmount;
            parameters[14].Value = model.ContractServiceFee;
            parameters[15].Value = model.ContractTaxID;
            parameters[16].Value = model.ContractTax;
            parameters[17].Value = model.ContractCostDetail;
            parameters[18].Value = model.CustomerID;
            parameters[19].Value = model.IsNeedInvoice;
            parameters[20].Value = model.PayCycle;
            parameters[21].Value = model.ContractStatusID;
            parameters[22].Value = model.OtherRequest;
            parameters[23].Value = model.CreatorID;
            parameters[24].Value = model.CreatorName;
            parameters[25].Value = model.CreatorCode;
            parameters[26].Value = model.CreatorUserID;
            parameters[27].Value = model.ApplicantUserID;
            parameters[28].Value = model.ApplicantUserName;
            parameters[29].Value = model.ApplicantCode;
            parameters[30].Value = model.ApplicantEmployeeName;
            parameters[31].Value = model.LeaderUserID;
            parameters[32].Value = model.ContractStatusName;
            parameters[33].Value = model.LeaderUserName;
            parameters[34].Value = model.LeaderCode;
            parameters[35].Value = model.LeaderEmployeeName;
            parameters[36].Value = model.CreateDate;
            parameters[37].Value = model.SubmitDate;
            parameters[38].Value = model.Status;
            parameters[39].Value = model.Step;
            parameters[40].Value = model.Attachment;
            parameters[41].Value = model.AttachType;
            //parameters[42].Value =model.Lastupdatetime;
            parameters[42].Value = model.BDProjectID;
            parameters[43].Value = model.BDProjectCode;
            parameters[44].Value = model.IsFromJoint;
            parameters[45].Value = model.BranchID;
            parameters[46].Value = model.BranchName;
            parameters[47].Value = model.SerialCode;
            parameters[48].Value = model.AuditorSysUserID;
            parameters[49].Value = model.AuditorUserCode;
            parameters[50].Value = model.AuditorUserName;
            parameters[51].Value = model.AuditorEmployeeName;
            parameters[52].Value = model.LastAuditDate;
            parameters[53].Value = model.Del;
            parameters[54].Value = model.CustomerRemark;
            parameters[55].Value = model.Remark;
            parameters[56].Value = model.ApplicantUserEmail;
            parameters[57].Value = model.ApplicantUserPhone;
            parameters[58].Value = model.ApplicantUserPosition;
            parameters[59].Value = (int)ESP.Finance.Utility.ProjectInUse.Use;
            parameters[60].Value = model.CustomerCode;
            parameters[61].Value = model.BankId;
            parameters[62].Value = model.ProfileReason;
            parameters[63].Value = model.IsCalculateByVAT;
            parameters[64].Value = model.IsDigital;
            parameters[65].Value = model.ProjectTypeLevel2ID;
            parameters[66].Value = model.ProjectTypeLevel3ID;
            parameters[67].Value = model.CustomerAttachID;
            parameters[68].Value = model.Brands;
            parameters[69].Value = model.MediaCostRate;
            parameters[70].Value = model.CustomerRebateRate;
            parameters[71].Value = model.MediaId;
            parameters[72].Value = model.BusinessPersonId;
            parameters[73].Value = model.BusinessPersonName;
            parameters[74].Value = model.AdvertiserID;
            parameters[75].Value = model.CustomerProjectCode;
            parameters[76].Value = model.RelevanceProjectId;
            parameters[77].Value = model.RelevanceProjectCode;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), trans, parameters);
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
        public int Update(ESP.Finance.Entity.ProjectInfo model)
        {
            return Update(model, null);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(ESP.Finance.Entity.ProjectInfo model, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Project set ");
            strSql.Append("BranchCode=@BranchCode,");
            strSql.Append("BusinessTypeID=@BusinessTypeID,");
            strSql.Append("BusinessTypeName=@BusinessTypeName,");
            strSql.Append("GroupID=@GroupID,");
            strSql.Append("GroupName=@GroupName,");
            strSql.Append("ProjectTypeID=@ProjectTypeID,");
            strSql.Append("ProjectTypeName=@ProjectTypeName,");
            strSql.Append("ProjectTypeCode=@ProjectTypeCode,");
            strSql.Append("BusinessDesID=@BusinessDesID,");
            strSql.Append("BusinessDescription=@BusinessDescription,");
            strSql.Append("ProjectCode=@ProjectCode,");
            strSql.Append("BeginDate=@BeginDate,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("TotalAmount=@TotalAmount,");
            strSql.Append("ContractServiceFee=@ContractServiceFee,");
            strSql.Append("ContractTaxID=@ContractTaxID,");
            strSql.Append("ContractTax=@ContractTax,");
            strSql.Append("ContractCostDetail=@ContractCostDetail,");
            strSql.Append("CustomerID=@CustomerID,");
            strSql.Append("IsNeedInvoice=@IsNeedInvoice,");
            strSql.Append("PayCycle=@PayCycle,");
            strSql.Append("ContractStatusID=@ContractStatusID,");
            strSql.Append("OtherRequest=@OtherRequest,");
            strSql.Append("CreatorID=@CreatorID,");
            strSql.Append("CreatorName=@CreatorName,");
            strSql.Append("CreatorCode=@CreatorCode,");
            strSql.Append("CreatorUserID=@CreatorUserID,");
            strSql.Append("ApplicantUserID=@ApplicantUserID,");
            strSql.Append("ApplicantUserName=@ApplicantUserName,");
            strSql.Append("ApplicantCode=@ApplicantCode,");
            strSql.Append("ApplicantEmployeeName=@ApplicantEmployeeName,");
            strSql.Append("ApplicantUserEmail=@ApplicantUserEmail,");
            strSql.Append("ApplicantUserPhone=@ApplicantUserPhone,");
            strSql.Append("ApplicantUserPosition=@ApplicantUserPosition,");
            strSql.Append("LeaderUserID=@LeaderUserID,");
            strSql.Append("ContractStatusName=@ContractStatusName,");
            strSql.Append("LeaderUserName=@LeaderUserName,");
            strSql.Append("LeaderCode=@LeaderCode,");
            strSql.Append("LeaderEmployeeName=@LeaderEmployeeName,");
            strSql.Append("CreateDate=@CreateDate,");
            strSql.Append("SubmitDate=@SubmitDate,");
            strSql.Append("Status=@Status,");
            strSql.Append("Step=@Step,");
            strSql.Append("Attachment=@Attachment,");
            strSql.Append("AttachType=@AttachType,");
            strSql.Append("BDProjectID=@BDProjectID,");
            strSql.Append("BDProjectCode=@BDProjectCode,");
            strSql.Append("IsFromJoint=@IsFromJoint,");
            strSql.Append("BranchID=@BranchID,");
            strSql.Append("BranchName=@BranchName,");
            strSql.Append("SerialCode=@SerialCode, ");
            strSql.Append("AuditorSysUserID=@AuditorSysUserID,");
            strSql.Append("AuditorUserCode=@AuditorUserCode,");
            strSql.Append("AuditorUserName=@AuditorUserName,");
            strSql.Append("AuditorEmployeeName=@AuditorEmployeeName,");
            strSql.Append("LastAuditDate=@LastAuditDate,");
            strSql.Append("Del=@Del, ");
            strSql.Append("CustomerRemark=@CustomerRemark,");
            strSql.Append("Remark=@Remark,InUse=@InUse,CustomerCode=@CustomerCode,BankId=@BankId,ProfileReason=@ProfileReason,IsCalculateByVAT=@IsCalculateByVAT,IsDigital=@IsDigital,ProjectTypeLevel2ID=@ProjectTypeLevel2ID,ProjectTypeLevel3ID=@ProjectTypeLevel3ID");
            strSql.Append(",CustomerAttachID=@CustomerAttachID");
            strSql.Append(",Brands=@Brands");
            strSql.Append(",MediaCostRate=@MediaCostRate");
            strSql.Append(",CustomerRebateRate=@CustomerRebateRate,MediaId=@MediaId,RelevanceProjectId=@RelevanceProjectId,RelevanceProjectCode=@RelevanceProjectCode");
            strSql.Append(",BusinessPersonId=@BusinessPersonId,BusinessPersonName=@BusinessPersonName,AdvertiserID=@AdvertiserID,CustomerProjectCode=@CustomerProjectCode");
            strSql.Append(" where ProjectId=@ProjectId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectId", SqlDbType.Int,4),
					new SqlParameter("@BranchCode", SqlDbType.NVarChar,10),
					new SqlParameter("@BusinessTypeID", SqlDbType.Int,4),
					new SqlParameter("@BusinessTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@GroupName", SqlDbType.NVarChar,100),
					new SqlParameter("@ProjectTypeID", SqlDbType.Int,4),
					new SqlParameter("@ProjectTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectTypeCode", SqlDbType.NVarChar,50),
					new SqlParameter("@BusinessDesID", SqlDbType.Int,4),
					new SqlParameter("@BusinessDescription", SqlDbType.NVarChar,2000),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@BeginDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@TotalAmount", SqlDbType.Decimal,9),
					new SqlParameter("@ContractServiceFee", SqlDbType.Decimal,9),
					new SqlParameter("@ContractTaxID", SqlDbType.Int,4),
					new SqlParameter("@ContractTax", SqlDbType.Decimal,9),
					new SqlParameter("@ContractCostDetail", SqlDbType.NVarChar),
					new SqlParameter("@CustomerID", SqlDbType.Int,4),
					new SqlParameter("@IsNeedInvoice", SqlDbType.Int,4),
					new SqlParameter("@PayCycle", SqlDbType.NVarChar,500),
					new SqlParameter("@ContractStatusID", SqlDbType.Int,4),
					new SqlParameter("@OtherRequest", SqlDbType.NVarChar,2000),
					new SqlParameter("@CreatorID", SqlDbType.Int,4),
					new SqlParameter("@CreatorName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorUserID", SqlDbType.NVarChar,50),
					new SqlParameter("@ApplicantUserID", SqlDbType.Int,4),
					new SqlParameter("@ApplicantUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@ApplicantCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ApplicantEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@LeaderUserID", SqlDbType.Int,4),
					new SqlParameter("@ContractStatusName", SqlDbType.NVarChar,50),
					new SqlParameter("@LeaderUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@LeaderCode", SqlDbType.NVarChar,50),
					new SqlParameter("@LeaderEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@SubmitDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Step", SqlDbType.Int,4),
					new SqlParameter("@Attachment", SqlDbType.NVarChar,500),
					new SqlParameter("@AttachType", SqlDbType.Int,4),
					new SqlParameter("@BDProjectID", SqlDbType.Int,4),
					new SqlParameter("@BDProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@IsFromJoint", SqlDbType.Bit,1),
					new SqlParameter("@BranchID", SqlDbType.Int,4),
					new SqlParameter("@BranchName", SqlDbType.NVarChar,50),
                    new SqlParameter("@SerialCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@AuditorSysUserID", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserCode", SqlDbType.NVarChar,10),
					new SqlParameter("@AuditorUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorEmployeeName", SqlDbType.NVarChar,20),
					new SqlParameter("@LastAuditDate", SqlDbType.DateTime,8),
                    new SqlParameter("@Del", SqlDbType.Int,4),
                    new SqlParameter("@CustomerRemark",SqlDbType.NVarChar,100),
                    new SqlParameter("@Remark",SqlDbType.NVarChar,100),
                    new SqlParameter("@ApplicantUserEmail", SqlDbType.NVarChar,50),
                    new SqlParameter("@ApplicantUserPhone",SqlDbType.NVarChar,50),
                    new SqlParameter("@ApplicantUserPosition",SqlDbType.NVarChar,50),
                    new SqlParameter("@InUse",SqlDbType.Int,4),
                    new SqlParameter("@CustomerCode",SqlDbType.NVarChar,50),
                    new SqlParameter("@BankId",SqlDbType.Int,4),
                    new SqlParameter("@ProfileReason",SqlDbType.NVarChar,500),
                     new SqlParameter("@IsCalculateByVAT",SqlDbType.Int,4),
                     new SqlParameter("@IsDigital",SqlDbType.Int,4),
                     new SqlParameter("@ProjectTypeLevel2ID",SqlDbType.Int,4),
                     new SqlParameter("@ProjectTypeLevel3ID",SqlDbType.Int,4),
                     new SqlParameter("@CustomerAttachID",SqlDbType.NVarChar,200),
                     new SqlParameter("@Brands",SqlDbType.NVarChar,1000),
                     new SqlParameter("@MediaCostRate",SqlDbType.Decimal,9),
                     new SqlParameter("@CustomerRebateRate",SqlDbType.Decimal,9),
                     new SqlParameter("@MediaId",SqlDbType.Int,4),
                     new SqlParameter("@RelevanceProjectId",SqlDbType.Int,4),
                     new SqlParameter("@RelevanceProjectCode",SqlDbType.NVarChar,50),
                     new SqlParameter("@BusinessPersonId",SqlDbType.Int,4),
                     new SqlParameter("@BusinessPersonName",SqlDbType.NVarChar,50),
                     new SqlParameter("@AdvertiserID",SqlDbType.NVarChar,50),
                     new SqlParameter("@CustomerProjectCode",SqlDbType.NVarChar,50),

                                        };
            parameters[0].Value = model.ProjectId;
            parameters[1].Value = model.BranchCode;
            parameters[2].Value = model.BusinessTypeID;
            parameters[3].Value = model.BusinessTypeName;
            parameters[4].Value = model.GroupID;
            parameters[5].Value = model.GroupName;
            parameters[6].Value = model.ProjectTypeID;
            parameters[7].Value = model.ProjectTypeName;
            parameters[8].Value = model.ProjectTypeCode;
            parameters[9].Value = model.BusinessDesID;
            parameters[10].Value = model.BusinessDescription;
            parameters[11].Value = model.ProjectCode;
            parameters[12].Value = model.BeginDate;
            parameters[13].Value = model.EndDate;
            parameters[14].Value = model.TotalAmount;
            parameters[15].Value = model.ContractServiceFee;
            parameters[16].Value = model.ContractTaxID;
            parameters[17].Value = model.ContractTax;
            parameters[18].Value = model.ContractCostDetail;
            parameters[19].Value = model.CustomerID;
            parameters[20].Value = model.IsNeedInvoice;
            parameters[21].Value = model.PayCycle;
            parameters[22].Value = model.ContractStatusID;
            parameters[23].Value = model.OtherRequest;
            parameters[24].Value = model.CreatorID;
            parameters[25].Value = model.CreatorName;
            parameters[26].Value = model.CreatorCode;
            parameters[27].Value = model.CreatorUserID;
            parameters[28].Value = model.ApplicantUserID;
            parameters[29].Value = model.ApplicantUserName;
            parameters[30].Value = model.ApplicantCode;
            parameters[31].Value = model.ApplicantEmployeeName;
            parameters[32].Value = model.LeaderUserID;
            parameters[33].Value = model.ContractStatusName;
            parameters[34].Value = model.LeaderUserName;
            parameters[35].Value = model.LeaderCode;
            parameters[36].Value = model.LeaderEmployeeName;
            parameters[37].Value = model.CreateDate;
            parameters[38].Value = model.SubmitDate;
            parameters[39].Value = model.Status;
            parameters[40].Value = model.Step;
            parameters[41].Value = model.Attachment;
            parameters[42].Value = model.AttachType;
            parameters[43].Value = model.BDProjectID;
            parameters[44].Value = model.BDProjectCode;
            parameters[45].Value = model.IsFromJoint;
            parameters[46].Value = model.BranchID;
            parameters[47].Value = model.BranchName;
            parameters[48].Value = model.SerialCode;
            parameters[49].Value = model.AuditorSysUserID;
            parameters[50].Value = model.AuditorUserCode;
            parameters[51].Value = model.AuditorUserName;
            parameters[52].Value = model.AuditorEmployeeName;
            parameters[53].Value = model.LastAuditDate;
            parameters[54].Value = model.Del;
            parameters[55].Value = model.CustomerRemark;
            parameters[56].Value = model.Remark;
            parameters[57].Value = model.ApplicantUserEmail;
            parameters[58].Value = model.ApplicantUserPhone;
            parameters[59].Value = model.ApplicantUserPosition;
            parameters[60].Value = model.InUse;
            parameters[61].Value = model.CustomerCode;
            parameters[62].Value = model.BankId;
            parameters[63].Value = model.ProfileReason;
            parameters[64].Value = model.IsCalculateByVAT;
            parameters[65].Value = model.IsDigital;
            parameters[66].Value = model.ProjectTypeLevel2ID;
            parameters[67].Value = model.ProjectTypeLevel3ID;
            parameters[68].Value = model.CustomerAttachID;
            parameters[69].Value = model.Brands;
            parameters[70].Value = model.MediaCostRate;
            parameters[71].Value = model.CustomerRebateRate;
            parameters[72].Value = model.MediaId;
            parameters[73].Value = model.RelevanceProjectId;
            parameters[74].Value = model.RelevanceProjectCode;
            parameters[75].Value = model.BusinessPersonId;
            parameters[76].Value = model.BusinessPersonName;
            parameters[77].Value = model.AdvertiserID;
            parameters[78].Value = model.CustomerProjectCode;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, parameters);
        }


        public int UpdateWorkFlow(int projectId, int workItemID, string workItemName, int processID, int instanceID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Project set ");
            strSql.Append("WorkItemID=@WorkItemID,");
            strSql.Append("WorkItemName=@WorkItemName,");
            strSql.Append("ProcessID=@ProcessID,");
            strSql.Append("InstanceID=@InstanceID ");
            strSql.Append(" where ProjectId=@ProjectId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectId", SqlDbType.Int,4),
					new SqlParameter("@WorkItemID", SqlDbType.Int,4),
					new SqlParameter("@WorkItemName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProcessID", SqlDbType.Int,4),
					new SqlParameter("@InstanceID", SqlDbType.Int,4)
                                        };
            parameters[0].Value = projectId;
            parameters[1].Value = workItemID;
            parameters[2].Value = workItemName;
            parameters[3].Value = processID;
            parameters[4].Value = instanceID;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 检查合同
        /// </summary>
        /// <param name="prjId"></param>
        /// <param name="status"></param>
        /// <param name="isInTrans"></param>
        /// <returns></returns>
        public int ChangeCheckContractStatus(int prjId, Utility.ProjectCheckContract status)
        {
            return ChangeCheckContractStatus(prjId, status, null);
        }

        /// <summary>
        /// 检查合同
        /// </summary>
        /// <param name="prjId"></param>
        /// <param name="status"></param>
        /// <param name="isInTrans"></param>
        /// <returns></returns>
        public int ChangeCheckContractStatus(int prjId, Utility.ProjectCheckContract status, SqlTransaction trans)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update F_Project set ");
            strSql.Append("CheckContract=@CheckContract ");
            strSql.Append(" where ProjectId=@ProjectId ");
            SqlParameter[] parameters = {
					new SqlParameter("@CheckContract", SqlDbType.Int,4),
					new SqlParameter("@ProjectId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = (int)status;
            parameters[1].Value = prjId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), trans, parameters);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ProjectId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete F_Project ");
            strSql.Append(" where ProjectId=@ProjectId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectId", SqlDbType.Int,4)};
            parameters[0].Value = ProjectId;

            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.ProjectInfo GetModel(int ProjectId)
        {
            return GetModel(ProjectId, null);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ESP.Finance.Entity.ProjectInfo GetModel(int ProjectId, SqlTransaction trans)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select  top 1 * from F_Project ");
            strSql.Append(" where ProjectId=@ProjectId ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectId", SqlDbType.Int,4)};
            parameters[0].Value = ProjectId;

            return CBO.FillObject<ProjectInfo>(DbHelperSQL.Query(strSql.ToString(), trans, parameters));
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<ProjectInfo> GetList(string term, List<SqlParameter> param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * FROM F_Project where del != @del ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(term);
            }
            strSql.Append(" order by Lastupdatetime desc ");
            if (param == null)
            {
                param = new List<SqlParameter>();
            }

            SqlParameter spm = new SqlParameter("@del", (int)Utility.RecordStatus.Del);
            param.Add(spm);
            SqlParameter[] ps = param.ToArray();
            return CBO.FillCollection<ProjectInfo>(DbHelperSQL.Query(strSql.ToString(), ps));
        }

        /// <summary>
        /// 根据项目号获得对象
        /// </summary>
        /// <param name="projectCode"></param>
        /// <returns></returns>
        public ESP.Finance.Entity.ProjectInfo GetModelByProjectCode(string projectCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select  top 1 * from F_Project ");
            strSql.Append(" where ProjectCode=@ProjectCode ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50)};
            parameters[0].Value = projectCode;

            return CBO.FillObject<ProjectInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));
        }

        /// <summary>
        /// 与userid相关的项目号信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataTable GetProjectListJoinHist(int userId)
        {
            string strSql = @"select * from f_project as a
                                left join F_AuditHistory as b on a.projectid=b.projectid and b.auditstatus=0";
            strSql += " where a.status not in (34)";
            strSql += string.Format(" and (a.applicantUserId={0} or b.auditoruserid={0})", userId);
            return DbHelperSQL.Query(strSql).Tables[0];
        }

        /// <summary>
        /// 变更负责人
        /// </summary>
        /// <param name="returnIds"></param>
        /// <param name="oldUserId"></param>
        /// <param name="newUserId"></param>
        /// <returns></returns>
        public int changeApplicant(string projectIds, int oldUserId, int newUserId, ESP.Compatible.Employee currentUser)
        {
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(newUserId);
            ESP.Compatible.Employee emp1 = new ESP.Compatible.Employee(oldUserId);
            string strSql = " update f_project set ApplicantUserID={0}, ApplicantUserName='{1}', ApplicantCode='{2}', ApplicantEmployeeName='{3}', ApplicantUserEmail='{4}', ApplicantUserPhone='{5}', ApplicantUserPosition='{6}'";
            strSql = string.Format(strSql, emp.SysID, emp.ID, emp.ITCode, emp.Name, emp.EMail, emp.Telephone, emp.PositionDescription);
            strSql += " where projectId=@projectId and ApplicantUserID=" + oldUserId;
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int count = 0;
                    string[] Ids = projectIds.Split(',');
                    foreach (string id in Ids)
                    {
                        SqlParameter parm = new SqlParameter("@projectId", id);
                        if (DbHelperSQL.ExecuteSql(strSql, trans, parm) > 0)
                        {
                            addPermission(id, newUserId, trans);
                            ESP.Finance.Entity.AuditLogInfo log = new AuditLogInfo();
                            log.AuditDate = DateTime.Now;
                            log.AuditorEmployeeName = currentUser.Name;
                            log.AuditorSysID = int.Parse(currentUser.SysID);
                            log.AuditorUserCode = currentUser.ID;
                            log.AuditorUserName = currentUser.ITCode;
                            log.AuditStatus = 1;
                            log.FormID = int.Parse(id);
                            log.FormType = (int)ESP.Finance.Utility.FormType.Project;
                            log.Suggestion = "负责人" + emp1.Name + "变更为" + emp.Name;
                            ESP.Finance.BusinessLogic.AuditLogManager.Add(log, trans);
                            count++;
                        }
                    }
                    trans.Commit();
                    return count;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
        }

        /// <summary>
        /// 变更审核人
        /// </summary>
        /// <param name="projectIds"></param>
        /// <param name="oldUserId"></param>
        /// <param name="newUserId"></param>
        /// <returns></returns>
        public int changAuditor(string projectIds, int oldUserId, int newUserId, ESP.Compatible.Employee currentUser)
        {
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(newUserId);
            ESP.Compatible.Employee emp1 = new ESP.Compatible.Employee(oldUserId);
            string strSql = " update F_AuditHistory set  AuditorUserID={0}, AuditorUserName='{1}', AuditorUserCode='{2}', AuditorEmployeeName='{3}'";
            strSql = string.Format(strSql, emp.SysID, emp.ITCode, emp.ID, emp.Name);
            strSql += " where projectId=@projectId and AuditorUserID=" + oldUserId + " and auditestatus=0";
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    int count = 0;
                    string[] Ids = projectIds.Split(',');
                    foreach (string id in Ids)
                    {
                        SqlParameter parm = new SqlParameter("@projectId", id);
                        if (DbHelperSQL.ExecuteSql(strSql, parm) > 0)
                        {
                            addPermission(id, newUserId, trans);
                            ESP.Finance.Entity.AuditLogInfo log = new AuditLogInfo();
                            log.AuditDate = DateTime.Now;
                            log.AuditorEmployeeName = currentUser.Name;
                            log.AuditorSysID = int.Parse(currentUser.SysID);
                            log.AuditorUserCode = currentUser.ID;
                            log.AuditorUserName = currentUser.ITCode;
                            log.AuditStatus = 1;
                            log.FormID = int.Parse(id);
                            log.FormType = (int)ESP.Finance.Utility.FormType.Project;
                            log.Suggestion = "审核人" + emp1.Name + "变更为" + emp.Name;
                            ESP.Finance.BusinessLogic.AuditLogManager.Add(log, trans);

                            ESP.Finance.Entity.ProjectInfo Model = GetModel(int.Parse(id), trans);
                            if (Model.ProcessID != null && Model.InstanceID != null)
                            {
                                WorkFlowDAO.ProcessInstanceDao instanceDao = new WorkFlowDAO.ProcessInstanceDao();
                                instanceDao.UpdateRoleWhenLastDay("PA", Model.ProcessID.Value, Model.InstanceID.Value, oldUserId, newUserId, emp1.Name, emp.Name, trans);
                            }
                            count++;
                        }
                    }
                    trans.Commit();
                    return count;
                }
                catch
                {
                    trans.Rollback();
                    return 0;
                }
            }
        }

        private void addPermission(string projectId, int newUserId, SqlTransaction trans)
        {
            ESP.Purchase.Entity.DataInfo dataModel = new ESP.Purchase.DataAccess.DataPermissionProvider().GetDataInfoModel((int)ESP.Purchase.Common.State.DataType.Project, int.Parse(projectId), trans);
            if (dataModel == null)
            {
                dataModel = new ESP.Purchase.Entity.DataInfo();
                dataModel.DataType = (int)ESP.Purchase.Common.State.DataType.Project;
                dataModel.DataId = int.Parse(projectId);
            }
            ESP.Purchase.Entity.DataPermissionInfo perission = new ESP.Purchase.Entity.DataPermissionInfo();
            perission.UserId = newUserId;
            perission.IsEditor = true;
            perission.IsViewer = true;
            if (dataModel.Id > 0)
                new ESP.Purchase.DataAccess.DataPermissionProvider().AppendDataPermission(dataModel, new List<ESP.Purchase.Entity.DataPermissionInfo>() { perission }, trans);
            else
                new ESP.Purchase.DataAccess.DataPermissionProvider().AddDataPermission(dataModel, new List<ESP.Purchase.Entity.DataPermissionInfo>() { perission }, trans);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TaskItemInfo> GetTaskItems(string userIds)
        {
            List<TaskItemInfo> list = new List<TaskItemInfo>();
            System.Data.DataTable dt = DbHelperSQL.RunProcedure("P_GetAllTaskItems", new IDataParameter[1], "Ta").Tables[0];
            DataRow[] rows = dt.Select(" approverid in (" + userIds.TrimEnd(',') + ")", "operationType desc");
            System.Collections.ArrayList typeNames = new System.Collections.ArrayList(new string[] { 
                "待审批项目号", "待审批支持方" ,"待审批付款申请","待审批付款通知","PR现金借款冲销","第三方报销单","借款冲销单","商务卡报销单","报销单","支票/电汇付款单","现金借款单","行政报销"});
            for (int i = 0; i < rows.Length; i++)
            {

                if (!typeNames.Contains(rows[i]["operationtype"].ToString()))
                    continue;
                TaskItemInfo model = new TaskItemInfo();

                model.ApplicantID = Convert.ToInt32(rows[i]["ApplicantID"].ToString());
                model.ApplicantName = rows[i]["ApplicantName"].ToString();
                object obj = rows[i]["AppliedTime"];
                if (obj == null || obj == DBNull.Value)
                    model.AppliedTime = new DateTime(1900, 1, 1);
                else
                    model.AppliedTime = Convert.ToDateTime(obj);

                model.AppliedTime = model.AppliedTime.ToUniversalTime();

                model.Description = rows[i]["Description"].ToString();
                model.FormID = Convert.ToInt32(rows[i]["FromID"]);
                model.FormNumber = rows[i]["FormNumber"].ToString();
                model.FormType = rows[i]["FormType"].ToString();
                model.ApproverID = Convert.ToInt32(rows[i]["ApproverID"].ToString());
                model.ApproverName = rows[i]["ApproverName"].ToString();
                object audittype = rows[i]["audittype"].ToString();

                switch (rows[i]["operationtype"].ToString())
                {
                    case "待审批项目号":
                        if (rows[i]["FormNumber"].ToString() == "PA09041001")
                        {
                        }
                        if (Convert.ToInt32(audittype) < 10)
                        {
                            model.Url = string.Format(Common.ProjectBizUrl, model.FormID);
                        }
                        else
                        {
                            model.Url = string.Format(Common.ProjectFinanceUrl, model.FormID);
                        }
                        model.ApproversUrl = string.Format(Common.AuditUrl, "project", model.FormID);
                        break;
                    case "待审批支持方":

                        if (Convert.ToInt32(audittype) < 10)
                        {
                            model.Url = string.Format(Common.SupporterBizUrl, model.FormID,
                                                                rows[i]["Url"].ToString());
                        }
                        else
                        {
                            model.Url = string.Format(Common.SupporterFinanceUrl, model.FormID,
                                                                rows[i]["Url"].ToString());
                        }
                        model.ApproversUrl = string.Format(Common.AuditUrl, "supporter", model.FormID);
                        break;
                    case "待审批付款申请":
                        if (Convert.ToInt32(audittype) < 10)
                        {
                            model.Url = string.Format(Common.PaymentBizUrl, model.FormID);
                        }
                        else
                        {
                            model.Url = string.Format(Common.PaymentFinanceUrl, model.FormID);
                        }
                        model.ApproversUrl = string.Format(Common.AuditUrl, "return", model.FormID);
                        break;
                    case "待审批付款通知":

                        if (Convert.ToInt32(audittype) < 10)
                        {
                            model.Url = string.Format(Common.NotifyBizUrl, model.FormID);
                        }
                        else
                        {
                            model.Url = string.Format(Common.NotifyFinanceUrl, model.FormID);
                        }
                        model.ApproversUrl = string.Format(Common.AuditUrl, "payment", model.FormID);
                        break;
                    case "PR现金借款冲销":
                    case "第三方报销单":
                    case "借款冲销单":
                    case "商务卡报销单":
                    case "报销单":
                    case "支票/电汇付款单":
                    case "现金借款单":
                    case "行政报销":
                        model.Url = rows[i]["Url"].ToString(); ;
                        break;
                    default:
                        break;
                }
                list.Add(model);
            }
            return list;
        }

        public IList<ProjectInfo> GetWaitAuditList(string userIds, string strTerms, List<SqlParameter> parms)
        {
            System.Data.DataTable dt = DbHelperSQL.RunProcedure("P_GetAllTaskItems", new IDataParameter[1], "Ta").Tables[0];
            DataRow[] rows = dt.Select(" approverid in (" + userIds.TrimEnd(',') + ") and operationType='待审批项目号'");
            string projectIds = "";
            for (int i = 0; i < rows.Length; i++)
            {
                projectIds += rows[i]["FromID"].ToString() + ",";
            }
            strTerms += projectIds == "" ? " and projectId=0" : " and projectId in (" + projectIds.TrimEnd(',') + ")";
            return GetList(strTerms, parms);
        }


        public IList<ProjectInfo> GetWaitAuditList(int[] userIds)
        {
            if (userIds == null || userIds.Length == 0)
                return new List<ProjectInfo>();

            StringBuilder sql = new StringBuilder(@"
select distinct a.*

from F_Project as a 
inner join (
        select a.* 
        from f_auditHistory as a 
        inner join (select min(squencelevel) as squencelevel,projectid from f_auditHistory where (auditstatus=0) group by projectid) as am
        on a.squencelevel=am.squencelevel and a.projectid=am.projectid
    ) as b 
    on a.ProjectId = b.projectid
where  (Status not in(0,10,19,20,30,32,33,34) or (status=32 and checkcontract=1))
 and AuditorUserID in (").Append(userIds[0]);

            for (var i = 1; i < userIds.Length; i++)
            {
                sql.Append(",").Append(userIds[i]);
            }

            sql.Append(")");


            using (var reader = ESP.Finance.DataAccess.DbHelperSQL.ExecuteReader(sql.ToString()))
            {
                return MyPhotoUtility.CBO.FillCollection<ProjectInfo>(reader);
            }
        }

        public DataTable GetMaterialCost(string begindate, string enddate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select level3id, deptname,Description,costtypeid,sum(cost) as cost,sum(cost1) as cost1,sum(cost2) as cost2,sum(cost3) as cost3,
sum(cost4) as cost4,sum(cost5) as cost5,sum(cost6) as cost6
from (
select level3id, deptname,projectcode,Description,costtypeid ,sum(cost) as cost,
(select sum(inceptprice) from (select *,(select top 1 producttype from t_orderinfo where general_id =t_generalinfo.id) as producttype from t_generalinfo) a left join t_paymentperiod b on a.id=b.gid 
 where a.prtype not in(1,6,4,8) and a.status not in(-1,0,2,4) and project_code=aaa.projectcode
 and producttype in(select typeid from t_type where parentid =aaa.costtypeid) and a.departmentid=aaa.level3id
 ) as cost1,
(select sum(expectpaymentprice) from (select *,(select top 1 producttype from t_orderinfo where general_id =t_generalinfo.id) as producttype from t_generalinfo) a left join t_paymentperiod b on a.id=b.gid 
where departmentid=aaa.level3id and a.prtype not in(1,4,6,8) and a.status not in(-1,0,2,4) and (b.inceptprice is null or b.inceptprice=0)
and project_code =aaa.projectcode
 and producttype in(select typeid from t_type where parentid =aaa.costtypeid)
) as cost2,
(select sum(expensemoney) from f_expenseaccountdetail where returnid in(select returnid from f_return where returnstatus not in(-1,0,1) and returntype in(30,31,20,32,33,35,37) and departmentid=aaa.level3id and projectcode =aaa.projectcode) and costdetailid=aaa.costtypeid)  as cost3,
(select sum(expensemoney) from f_expenseaccountdetail where returnid in(select returnid from f_return where returnstatus not in(-1,0,1) and returntype in(40) and departmentid=aaa.level3id and projectcode =aaa.projectcode) and costdetailid=aaa.costtypeid and ticketstatus=0 )  as cost4,
(select sum(prefee) from f_return a join (select *,(select top 1 producttype from t_orderinfo where general_id =t_generalinfo.id) as producttype from t_generalinfo) b on a.prid=b.id 
where  project_code=aaa.projectcode
and b.departmentid=aaa.level3id and b.prtype in(1,6) and b.status not in(0,-1,2,4)
and producttype in(select typeid from t_type where parentid =aaa.costtypeid)
) as cost5,0 as cost6
from (
select level3id,a.projectcode,
(c.level1+'-'+c.level2+'-'+c.level3) as deptname,
b.Description,cost,costtypeid  from f_project a 
left join  f_contractcost b on a.projectid =b.projectid 
left join V_Department c on a.groupid =c.level3id
where a.projectcode <>'' and (begindate between '{0}' and '{1}') 
) aaa 
where costtypeid is not null
group by deptname,level3id,projectcode,Description,costtypeid
) abc
group by deptname,level3id,Description,costtypeid
order by deptname,Description

                            ");
            string str = string.Format(strSql.ToString(), begindate, enddate);
            return DbHelperSQL.Query(str).Tables[0];
        }

        /// <summary>
        /// 待审核证据链的项目列表
        /// </summary>
        /// <returns></returns>
        public IList<ProjectInfo> GetContractAuditingProjectList()
        {
            string sql = @"select distinct a.* from F_Project as a 
                            inner join F_Contract as b on a.ProjectId = b.ProjectID
                            where b.del=0 and b.Status="+(int)ESP.Finance.Utility.ContractStatus.Status.Auditing;
            using (var reader = ESP.Finance.DataAccess.DbHelperSQL.ExecuteReader(sql.ToString()))
            {
                return MyPhotoUtility.CBO.FillCollection<ProjectInfo>(reader);
            }
        }

        public IList<ProjectInfo> GetApplyForInvioceAuditingProjectList()
        {
            string sql = @"select distinct a.* from F_Project as a 
                            inner join F_ApplyForInvioce as b on a.ProjectId = b.ProjectID
                            where b.Status="+(int)ESP.Finance.Utility.ApplyForInvioceStatus.Status.Auditing;
            using (var reader = ESP.Finance.DataAccess.DbHelperSQL.ExecuteReader(sql.ToString()))
            {
                return MyPhotoUtility.CBO.FillCollection<ProjectInfo>(reader);
            }
        }
        #endregion  成员方法
    }
}

