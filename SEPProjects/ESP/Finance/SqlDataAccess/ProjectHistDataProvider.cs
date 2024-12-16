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
	/// 数据访问类ProjectHistDAL。
	/// </summary>
    internal class ProjectHistDataProvider : ESP.Finance.IDataAccess.IProjectHistDataProvider
	{
		
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ProjectHistID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from F_ProjectHist");
			strSql.Append(" where ProjectHistID=@ProjectHistID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ProjectHistID", SqlDbType.Int,4)};
			parameters[0].Value = ProjectHistID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


        //public int GetNewVersion(int projectId)
        //{
        //    return GetNewVersion(projectId,false);
        //}

        public int GetNewVersion(int projectId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) from F_ProjectHist");
            strSql.Append(" where ProjectID=@ProjectID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ProjectID", SqlDbType.Int,4)};
            parameters[0].Value = projectId;

            return (int)DbHelperSQL.GetSingle(strSql.ToString(),parameters)+1;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ESP.Finance.Entity.ProjectHistInfo model)
        {
            return Add(model, null);
        }


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ESP.Finance.Entity.ProjectHistInfo model,SqlTransaction trans)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into F_ProjectHist(");
            strSql.Append("BranchID,BranchName,BranchCode,BusinessTypeID,BusinessTypeName,GroupID,GroupName,ProjectTypeID,ProjectTypeName,ProjectTypeCode,VersionID,BusinessDesID,BusinessDescription,BeginDate,EndDate,TotalAmount,ContractServiceFee,ContractTaxID,ContractTax,ContractCostDetail,CustomerID,ProjectId,IsNeedInvoice,PayCycle,OtherRequest,CreatorID,CreatorName,CreatorCode,CreatorUserID,ApplicantUserID,ApplicantUserName,ApplicantCode,ProjectCode,ApplicantEmployeeName,LeaderUserID,LeaderUserName,LeaderCode,LeaderEmployeeName,CreateDate,SubmitDate,Status,Step,Attachment,ContractStatusID,AttachType,ChangeUserID,ChangeUserName,ChangeCode,ChangeEmployeeName,ChangeDes,ChangeTime,ContractStatusName,BDProjectID,BDProjectCode,IsFromJoint,SerialCode,AuditorSysUserID,AuditorUserCode,AuditorUserName,AuditorEmployeeName,LastAuditDate,CustomerRemark,Remark,WorkItemID,WorkItemName,ProcessID,InstanceID,ProjectModel)");
			strSql.Append(" values (");
            strSql.Append("@BranchID,@BranchName,@BranchCode,@BusinessTypeID,@BusinessTypeName,@GroupID,@GroupName,@ProjectTypeID,@ProjectTypeName,@ProjectTypeCode,@VersionID,@BusinessDesID,@BusinessDescription,@BeginDate,@EndDate,@TotalAmount,@ContractServiceFee,@ContractTaxID,@ContractTax,@ContractCostDetail,@CustomerID,@ProjectId,@IsNeedInvoice,@PayCycle,@OtherRequest,@CreatorID,@CreatorName,@CreatorCode,@CreatorUserID,@ApplicantUserID,@ApplicantUserName,@ApplicantCode,@ProjectCode,@ApplicantEmployeeName,@LeaderUserID,@LeaderUserName,@LeaderCode,@LeaderEmployeeName,@CreateDate,@SubmitDate,@Status,@Step,@Attachment,@ContractStatusID,@AttachType,@ChangeUserID,@ChangeUserName,@ChangeCode,@ChangeEmployeeName,@ChangeDes,@ChangeTime,@ContractStatusName,@BDProjectID,@BDProjectCode,@IsFromJoint,@SerialCode,@AuditorSysUserID,@AuditorUserCode,@AuditorUserName,@AuditorEmployeeName,@LastAuditDate,@CustomerRemark,@Remark,@WorkItemID,@WorkItemName,@ProcessID,@InstanceID,@ProjectModel)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@BranchID", SqlDbType.Int,4),
					new SqlParameter("@BranchName", SqlDbType.NVarChar,50),
					new SqlParameter("@BranchCode", SqlDbType.NVarChar,10),
					new SqlParameter("@BusinessTypeID", SqlDbType.Int,4),
					new SqlParameter("@BusinessTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@GroupName", SqlDbType.NVarChar,100),
					new SqlParameter("@ProjectTypeID", SqlDbType.Int,4),
					new SqlParameter("@ProjectTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectTypeCode", SqlDbType.NVarChar,50),
					new SqlParameter("@VersionID", SqlDbType.Int,4),
					new SqlParameter("@BusinessDesID", SqlDbType.Int,4),
					new SqlParameter("@BusinessDescription", SqlDbType.NVarChar,2000),
					new SqlParameter("@BeginDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@TotalAmount", SqlDbType.Decimal,9),
					new SqlParameter("@ContractServiceFee", SqlDbType.Decimal,9),
					new SqlParameter("@ContractTaxID", SqlDbType.Int,4),
					new SqlParameter("@ContractTax", SqlDbType.Decimal,9),
					new SqlParameter("@ContractCostDetail", SqlDbType.NVarChar),
					new SqlParameter("@CustomerID", SqlDbType.Int,4),
					new SqlParameter("@ProjectId", SqlDbType.Int,4),
					new SqlParameter("@IsNeedInvoice", SqlDbType.Int,4),
					new SqlParameter("@PayCycle", SqlDbType.NVarChar,500),
					new SqlParameter("@OtherRequest", SqlDbType.NVarChar,2000),
					new SqlParameter("@CreatorID", SqlDbType.Int,4),
					new SqlParameter("@CreatorName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorUserID", SqlDbType.NVarChar,50),
					new SqlParameter("@ApplicantUserID", SqlDbType.Int,4),
					new SqlParameter("@ApplicantUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@ApplicantCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ApplicantEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@LeaderUserID", SqlDbType.Int,4),
					new SqlParameter("@LeaderUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@LeaderCode", SqlDbType.NVarChar,50),
					new SqlParameter("@LeaderEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@SubmitDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Step", SqlDbType.Int,4),
					new SqlParameter("@Attachment", SqlDbType.NVarChar,500),
					new SqlParameter("@ContractStatusID", SqlDbType.Int,4),
					new SqlParameter("@AttachType", SqlDbType.Int,4),
					new SqlParameter("@ChangeUserID", SqlDbType.Int,4),
					new SqlParameter("@ChangeUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@ChangeCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ChangeEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@ChangeDes", SqlDbType.NVarChar,2000),
					new SqlParameter("@ChangeTime", SqlDbType.DateTime),
					//new SqlParameter("@Lastupdatetime", SqlDbType.Timestamp,8),
					new SqlParameter("@ContractStatusName", SqlDbType.NVarChar,50),
					new SqlParameter("@BDProjectID", SqlDbType.Int,4),
					new SqlParameter("@BDProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@IsFromJoint", SqlDbType.Bit,1),
                    new SqlParameter("@SerialCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@AuditorSysUserID", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserCode", SqlDbType.NVarChar,10),
					new SqlParameter("@AuditorUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorEmployeeName", SqlDbType.NVarChar,20),
					new SqlParameter("@LastAuditDate", SqlDbType.DateTime,8),
                    new SqlParameter("@WorkItemID", SqlDbType.Int,4),
					new SqlParameter("@WorkItemName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProcessID", SqlDbType.Int,4),
					new SqlParameter("@InstanceID", SqlDbType.Int,4),
                    new SqlParameter("@CustomerRemark",SqlDbType.NVarChar,100),
                    new SqlParameter("@Remark",SqlDbType.NVarChar,100),
                    new SqlParameter("@ProjectModel",SqlDbType.Image)
                                        };
			parameters[0].Value =model.BranchID;
			parameters[1].Value =model.BranchName;
			parameters[2].Value =model.BranchCode;
			parameters[3].Value =model.BusinessTypeID;
			parameters[4].Value =model.BusinessTypeName;
			parameters[5].Value =model.GroupID;
			parameters[6].Value =model.GroupName;
			parameters[7].Value =model.ProjectTypeID;
			parameters[8].Value =model.ProjectTypeName;
			parameters[9].Value =model.ProjectTypeCode;
			parameters[10].Value =model.VersionID;
			parameters[11].Value =model.BusinessDesID;
			parameters[12].Value =model.BusinessDescription;
			parameters[13].Value =model.BeginDate;
			parameters[14].Value =model.EndDate;
			parameters[15].Value =model.TotalAmount;
			parameters[16].Value =model.ContractServiceFee;
			parameters[17].Value =model.ContractTaxID;
			parameters[18].Value =model.ContractTax;
			parameters[19].Value =model.ContractCostDetail;
			parameters[20].Value =model.CustomerID;
			parameters[21].Value =model.ProjectId;
			parameters[22].Value =model.IsNeedInvoice;
			parameters[23].Value =model.PayCycle;
			parameters[24].Value =model.OtherRequest;
			parameters[25].Value =model.CreatorID;
			parameters[26].Value =model.CreatorName;
			parameters[27].Value =model.CreatorCode;
			parameters[28].Value =model.CreatorUserID;
			parameters[29].Value =model.ApplicantUserID;
			parameters[30].Value =model.ApplicantUserName;
			parameters[31].Value =model.ApplicantCode;
			parameters[32].Value =model.ProjectCode;
			parameters[33].Value =model.ApplicantEmployeeName;
			parameters[34].Value =model.LeaderUserID;
			parameters[35].Value =model.LeaderUserName;
			parameters[36].Value =model.LeaderCode;
			parameters[37].Value =model.LeaderEmployeeName;
			parameters[38].Value =model.CreateDate;
			parameters[39].Value =model.SubmitDate;
			parameters[40].Value =model.Status;
			parameters[41].Value =model.Step;
			parameters[42].Value =model.Attachment;
			parameters[43].Value =model.ContractStatusID;
			parameters[44].Value =model.AttachType;
			parameters[45].Value =model.ChangeUserID;
			parameters[46].Value =model.ChangeUserName;
			parameters[47].Value =model.ChangeCode;
			parameters[48].Value =model.ChangeEmployeeName;
			parameters[49].Value =model.ChangeDes;
			parameters[50].Value =model.ChangeTime;
			//parameters[51].Value =model.Lastupdatetime;
			parameters[51].Value =model.ContractStatusName;
			parameters[52].Value =model.BDProjectID;
			parameters[53].Value =model.BDProjectCode;
			parameters[54].Value =model.IsFromJoint;
            parameters[55].Value =model.SerialCode;

            parameters[56].Value =model.AuditorSysUserID;
            parameters[57].Value =model.AuditorUserCode;
            parameters[58].Value =model.AuditorUserName;
            parameters[59].Value =model.AuditorEmployeeName;
            parameters[60].Value =model.LastAuditDate;

            parameters[61].Value =model.WorkItemID;
            parameters[62].Value =model.WorkItemName;
            parameters[63].Value =model.ProcessID;
            parameters[64].Value =model.InstanceID;

            parameters[65].Value =model.CustomerRemark;
            parameters[66].Value =model.Remark;
            parameters[67].Value =model.ProjectModel;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),trans,parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}

        ///// <summary>
        ///// 更新一条数据
        ///// </summary>
        //public int Update(ESP.Finance.Entity.ProjectHistInfo model)
        //{
        //    return Update(model, false);
        //}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public int Update(ESP.Finance.Entity.ProjectHistInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update F_ProjectHist set ");
			strSql.Append("BranchID=@BranchID,");
			strSql.Append("BranchName=@BranchName,");
			strSql.Append("BranchCode=@BranchCode,");
			strSql.Append("BusinessTypeID=@BusinessTypeID,");
			strSql.Append("BusinessTypeName=@BusinessTypeName,");
			strSql.Append("GroupID=@GroupID,");
			strSql.Append("GroupName=@GroupName,");
			strSql.Append("ProjectTypeID=@ProjectTypeID,");
			strSql.Append("ProjectTypeName=@ProjectTypeName,");
			strSql.Append("ProjectTypeCode=@ProjectTypeCode,");
			strSql.Append("VersionID=@VersionID,");
			strSql.Append("BusinessDesID=@BusinessDesID,");
			strSql.Append("BusinessDescription=@BusinessDescription,");
			strSql.Append("BeginDate=@BeginDate,");
			strSql.Append("EndDate=@EndDate,");
			strSql.Append("TotalAmount=@TotalAmount,");
			strSql.Append("ContractServiceFee=@ContractServiceFee,");
			strSql.Append("ContractTaxID=@ContractTaxID,");
			strSql.Append("ContractTax=@ContractTax,");
			strSql.Append("ContractCostDetail=@ContractCostDetail,");
			strSql.Append("CustomerID=@CustomerID,");
			strSql.Append("ProjectId=@ProjectId,");
			strSql.Append("IsNeedInvoice=@IsNeedInvoice,");
			strSql.Append("PayCycle=@PayCycle,");
			strSql.Append("OtherRequest=@OtherRequest,");
			strSql.Append("CreatorID=@CreatorID,");
			strSql.Append("CreatorName=@CreatorName,");
			strSql.Append("CreatorCode=@CreatorCode,");
			strSql.Append("CreatorUserID=@CreatorUserID,");
			strSql.Append("ApplicantUserID=@ApplicantUserID,");
			strSql.Append("ApplicantUserName=@ApplicantUserName,");
			strSql.Append("ApplicantCode=@ApplicantCode,");
			strSql.Append("ProjectCode=@ProjectCode,");
			strSql.Append("ApplicantEmployeeName=@ApplicantEmployeeName,");
			strSql.Append("LeaderUserID=@LeaderUserID,");
			strSql.Append("LeaderUserName=@LeaderUserName,");
			strSql.Append("LeaderCode=@LeaderCode,");
			strSql.Append("LeaderEmployeeName=@LeaderEmployeeName,");
			strSql.Append("CreateDate=@CreateDate,");
			strSql.Append("SubmitDate=@SubmitDate,");
			strSql.Append("Status=@Status,");
			strSql.Append("Step=@Step,");
			strSql.Append("Attachment=@Attachment,");
			strSql.Append("ContractStatusID=@ContractStatusID,");
			strSql.Append("AttachType=@AttachType,");
			strSql.Append("ChangeUserID=@ChangeUserID,");
			strSql.Append("ChangeUserName=@ChangeUserName,");
			strSql.Append("ChangeCode=@ChangeCode,");
			strSql.Append("ChangeEmployeeName=@ChangeEmployeeName,");
			strSql.Append("ChangeDes=@ChangeDes,");
			strSql.Append("ChangeTime=@ChangeTime,");
			//strSql.Append("Lastupdatetime=@Lastupdatetime,");
			strSql.Append("ContractStatusName=@ContractStatusName,");
			strSql.Append("BDProjectID=@BDProjectID,");
			strSql.Append("BDProjectCode=@BDProjectCode,");
			strSql.Append("IsFromJoint=@IsFromJoint,");
            strSql.Append("SerialCode=@SerialCode, ");
            strSql.Append("AuditorSysUserID=@AuditorSysUserID,");
            strSql.Append("AuditorUserCode=@AuditorUserCode,");
            strSql.Append("AuditorUserName=@AuditorUserName,");
            strSql.Append("AuditorEmployeeName=@AuditorEmployeeName,");
            strSql.Append("LastAuditDate=@LastAuditDate, ");
            strSql.Append("CustomerRemark=@CustomerRemark,");
            strSql.Append("Remark=@Remark, ");
            strSql.Append("ProjectModel=@ProjectModel ");
            strSql.Append(" where ProjectHistID=@ProjectHistID and @Lastupdatetime >= Lastupdatetime ");
			SqlParameter[] parameters = {
					new SqlParameter("@ProjectHistID", SqlDbType.Int,4),
					new SqlParameter("@BranchID", SqlDbType.Int,4),
					new SqlParameter("@BranchName", SqlDbType.NVarChar,50),
					new SqlParameter("@BranchCode", SqlDbType.NVarChar,10),
					new SqlParameter("@BusinessTypeID", SqlDbType.Int,4),
					new SqlParameter("@BusinessTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@GroupName", SqlDbType.NVarChar,100),
					new SqlParameter("@ProjectTypeID", SqlDbType.Int,4),
					new SqlParameter("@ProjectTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectTypeCode", SqlDbType.NVarChar,50),
					new SqlParameter("@VersionID", SqlDbType.Int,4),
					new SqlParameter("@BusinessDesID", SqlDbType.Int,4),
					new SqlParameter("@BusinessDescription", SqlDbType.NVarChar,2000),
					new SqlParameter("@BeginDate", SqlDbType.DateTime),
					new SqlParameter("@EndDate", SqlDbType.DateTime),
					new SqlParameter("@TotalAmount", SqlDbType.Decimal,9),
					new SqlParameter("@ContractServiceFee", SqlDbType.Decimal,9),
					new SqlParameter("@ContractTaxID", SqlDbType.Int,4),
					new SqlParameter("@ContractTax", SqlDbType.Decimal,9),
					new SqlParameter("@ContractCostDetail", SqlDbType.NVarChar),
					new SqlParameter("@CustomerID", SqlDbType.Int,4),
					new SqlParameter("@ProjectId", SqlDbType.Int,4),
					new SqlParameter("@IsNeedInvoice", SqlDbType.Int,4),
					new SqlParameter("@PayCycle", SqlDbType.NVarChar,500),
					new SqlParameter("@OtherRequest", SqlDbType.NVarChar,2000),
					new SqlParameter("@CreatorID", SqlDbType.Int,4),
					new SqlParameter("@CreatorName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorCode", SqlDbType.NVarChar,50),
					new SqlParameter("@CreatorUserID", SqlDbType.NVarChar,50),
					new SqlParameter("@ApplicantUserID", SqlDbType.Int,4),
					new SqlParameter("@ApplicantUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@ApplicantCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ApplicantEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@LeaderUserID", SqlDbType.Int,4),
					new SqlParameter("@LeaderUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@LeaderCode", SqlDbType.NVarChar,50),
					new SqlParameter("@LeaderEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@CreateDate", SqlDbType.DateTime),
					new SqlParameter("@SubmitDate", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@Step", SqlDbType.Int,4),
					new SqlParameter("@Attachment", SqlDbType.NVarChar,500),
					new SqlParameter("@ContractStatusID", SqlDbType.Int,4),
					new SqlParameter("@AttachType", SqlDbType.Int,4),
					new SqlParameter("@ChangeUserID", SqlDbType.Int,4),
					new SqlParameter("@ChangeUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@ChangeCode", SqlDbType.NVarChar,50),
					new SqlParameter("@ChangeEmployeeName", SqlDbType.NVarChar,50),
					new SqlParameter("@ChangeDes", SqlDbType.NVarChar,2000),
					new SqlParameter("@ChangeTime", SqlDbType.DateTime),
					new SqlParameter("@ContractStatusName", SqlDbType.NVarChar,50),
					new SqlParameter("@BDProjectID", SqlDbType.Int,4),
					new SqlParameter("@BDProjectCode", SqlDbType.NVarChar,50),
					new SqlParameter("@IsFromJoint", SqlDbType.Bit,1),
                    new SqlParameter("@Lastupdatetime", SqlDbType.Timestamp,8),
                    new SqlParameter("@SerialCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@AuditorSysUserID", SqlDbType.Int,4),
					new SqlParameter("@AuditorUserCode", SqlDbType.NVarChar,10),
					new SqlParameter("@AuditorUserName", SqlDbType.NVarChar,50),
					new SqlParameter("@AuditorEmployeeName", SqlDbType.NVarChar,20),
					new SqlParameter("@LastAuditDate", SqlDbType.DateTime,8),
                    new SqlParameter("@CustomerRemark",SqlDbType.NVarChar,100),
                    new SqlParameter("@Remark",SqlDbType.NVarChar,100) ,
                    new SqlParameter("@ProjectModel",SqlDbType.Image)
                                        };
			parameters[0].Value =model.ProjectHistID;
			parameters[1].Value =model.BranchID;
			parameters[2].Value =model.BranchName;
			parameters[3].Value =model.BranchCode;
			parameters[4].Value =model.BusinessTypeID;
			parameters[5].Value =model.BusinessTypeName;
			parameters[6].Value =model.GroupID;
			parameters[7].Value =model.GroupName;
			parameters[8].Value =model.ProjectTypeID;
			parameters[9].Value =model.ProjectTypeName;
			parameters[10].Value =model.ProjectTypeCode;
			parameters[11].Value =model.VersionID;
			parameters[12].Value =model.BusinessDesID;
			parameters[13].Value =model.BusinessDescription;
			parameters[14].Value =model.BeginDate;
			parameters[15].Value =model.EndDate;
			parameters[16].Value =model.TotalAmount;
			parameters[17].Value =model.ContractServiceFee;
			parameters[18].Value =model.ContractTaxID;
			parameters[19].Value =model.ContractTax;
			parameters[20].Value =model.ContractCostDetail;
			parameters[21].Value =model.CustomerID;
			parameters[22].Value =model.ProjectId;
			parameters[23].Value =model.IsNeedInvoice;
			parameters[24].Value =model.PayCycle;
			parameters[25].Value =model.OtherRequest;
			parameters[26].Value =model.CreatorID;
			parameters[27].Value =model.CreatorName;
			parameters[28].Value =model.CreatorCode;
			parameters[29].Value =model.CreatorUserID;
			parameters[30].Value =model.ApplicantUserID;
			parameters[31].Value =model.ApplicantUserName;
			parameters[32].Value =model.ApplicantCode;
			parameters[33].Value =model.ProjectCode;
			parameters[34].Value =model.ApplicantEmployeeName;
			parameters[35].Value =model.LeaderUserID;
			parameters[36].Value =model.LeaderUserName;
			parameters[37].Value =model.LeaderCode;
			parameters[38].Value =model.LeaderEmployeeName;
			parameters[39].Value =model.CreateDate;
			parameters[40].Value =model.SubmitDate;
			parameters[41].Value =model.Status;
			parameters[42].Value =model.Step;
			parameters[43].Value =model.Attachment;
			parameters[44].Value =model.ContractStatusID;
			parameters[45].Value =model.AttachType;
			parameters[46].Value =model.ChangeUserID;
			parameters[47].Value =model.ChangeUserName;
			parameters[48].Value =model.ChangeCode;
			parameters[49].Value =model.ChangeEmployeeName;
			parameters[50].Value =model.ChangeDes;
			parameters[51].Value =model.ChangeTime;
			parameters[52].Value =model.ContractStatusName;
			parameters[53].Value =model.BDProjectID;
			parameters[54].Value =model.BDProjectCode;
			parameters[55].Value =model.IsFromJoint;
            parameters[56].Value =model.Lastupdatetime;
            parameters[57].Value =model.SerialCode;

            parameters[58].Value =model.AuditorSysUserID;
            parameters[59].Value =model.AuditorUserCode;
            parameters[60].Value =model.AuditorUserName;
            parameters[61].Value =model.AuditorEmployeeName;
            parameters[62].Value =model.LastAuditDate;
            parameters[63].Value =model.CustomerRemark;
            parameters[64].Value =model.Remark;
            parameters[65].Value =model.ProjectModel;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}

        //        /// <summary>
        ///// 删除一条数据
        ///// </summary>
        //public int Delete(int ProjectHistID)
        //{
        //    return Delete(ProjectHistID, false);
        //}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public int Delete(int ProjectHistID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete F_ProjectHist ");
			strSql.Append(" where ProjectHistID=@ProjectHistID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ProjectHistID", SqlDbType.Int,4)};
			parameters[0].Value = ProjectHistID;

			return DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ESP.Finance.Entity.ProjectHistInfo GetModel(int ProjectHistID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select  top 1 ProjectHistID,BranchID,BranchName,BranchCode,BusinessTypeID,BusinessTypeName,GroupID,GroupName,ProjectTypeID,ProjectTypeName,ProjectTypeCode,VersionID,BusinessDesID,BusinessDescription,BeginDate,EndDate,TotalAmount,ContractServiceFee,ContractTaxID,ContractTax,ContractCostDetail,CustomerID,ProjectId,IsNeedInvoice,PayCycle,OtherRequest,CreatorID,CreatorName,CreatorCode,CreatorUserID,ApplicantUserID,ApplicantUserName,ApplicantCode,ProjectCode,ApplicantEmployeeName,LeaderUserID,LeaderUserName,LeaderCode,LeaderEmployeeName,CreateDate,SubmitDate,Status,Step,Attachment,ContractStatusID,AttachType,ChangeUserID,ChangeUserName,ChangeCode,ChangeEmployeeName,ChangeDes,ChangeTime,Lastupdatetime,ContractStatusName,BDProjectID,BDProjectCode,IsFromJoint,SerialCode,AuditorSysUserID,AuditorUserCode,AuditorUserName,AuditorEmployeeName,LastAuditDate,WorkItemID,WorkItemName,ProcessID,InstanceID,CustomerRemark,Remark,ProjectModel from F_ProjectHist ");
			strSql.Append(" where ProjectHistID=@ProjectHistID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ProjectHistID", SqlDbType.Int,4)};
			parameters[0].Value = ProjectHistID;
            return CBO.FillObject<ProjectHistInfo>(DbHelperSQL.Query(strSql.ToString(), parameters));

		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        public IList<ProjectHistInfo> GetList(string term, List<SqlParameter> param)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select ProjectHistID,BranchID,BranchName,BranchCode,BusinessTypeID,BusinessTypeName,GroupID,GroupName,ProjectTypeID,ProjectTypeName,ProjectTypeCode,VersionID,BusinessDesID,BusinessDescription,BeginDate,EndDate,TotalAmount,ContractServiceFee,ContractTaxID,ContractTax,ContractCostDetail,CustomerID,ProjectId,IsNeedInvoice,PayCycle,OtherRequest,CreatorID,CreatorName,CreatorCode,CreatorUserID,ApplicantUserID,ApplicantUserName,ApplicantCode,ProjectCode,ApplicantEmployeeName,LeaderUserID,LeaderUserName,LeaderCode,LeaderEmployeeName,CreateDate,SubmitDate,Status,Step,Attachment,ContractStatusID,AttachType,ChangeUserID,ChangeUserName,ChangeCode,ChangeEmployeeName,ChangeDes,ChangeTime,Lastupdatetime,ContractStatusName,BDProjectID,BDProjectCode,IsFromJoint,SerialCode,AuditorSysUserID,AuditorUserCode,AuditorUserName,AuditorEmployeeName,LastAuditDate,WorkItemID,WorkItemName,ProcessID,InstanceID,CustomerRemark,Remark,ProjectModel ");
			strSql.Append(" FROM F_ProjectHist ");
            if (!string.IsNullOrEmpty(term))
            {
                strSql.Append(" where " + term);
            }
            //if (param != null && param.Count > 0)
            //{
            //    SqlParameter[] ps = param.ToArray();

            //    return CBO.FillCollection<F_ProjectHist>(DbHelperSQL.ExecuteReader(strSql.ToString(), ps));
            //}
            return CBO.FillCollection<ProjectHistInfo>(DbHelperSQL.Query(strSql.ToString(),param));
		}

        public IList<ESP.Finance.Entity.ProjectHistInfo> GetListByProject(int projectId, string term, List<System.Data.SqlClient.SqlParameter> param)
        {
            if (string.IsNullOrEmpty(term))
            {
                term = " 1=1 ";
            }
            if (param == null)
            {
                param = new List<SqlParameter>();
            }

            term += " and ProjectID = @ProjectID";
            SqlParameter pm = new SqlParameter("@ProjectID", SqlDbType.Int, 4);
            pm.Value = projectId;

            param.Add(pm);

            return GetList(term, param);
        }

		#endregion  成员方法
	}
}

