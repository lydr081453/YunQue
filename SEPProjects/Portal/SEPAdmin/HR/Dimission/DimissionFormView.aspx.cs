using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using ESP.Administrative.Common;
using ESP.Administrative.Entity;
using ESP.HumanResource.BusinessLogic;

public partial class DimissionFormView : ESP.Web.UI.PageBase
{
    protected int DimissionFormId = 0;
    public int depId = 0;
    public int ViewUserId { get; set; }

    /// <summary>
    /// 离职单据信息
    /// </summary>
    public Dictionary<string, ESP.HumanResource.Entity.DimissionDetailsInfo> DicDimission
    {
        get
        {
            return this.ViewState["DicDimission"] == null ? null :
                (Dictionary<string, ESP.HumanResource.Entity.DimissionDetailsInfo>)this.ViewState["DicDimission"];
        }
        set
        {
            this.ViewState["DicDimission"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["userid"]))
            {
                ViewUserId = int.Parse(Request["userid"]);
            }
            else
                ViewUserId = UserID;
            InitPage();
        }
    }

    /// <summary>
    /// 初始化页面信息
    /// </summary>
    private void InitPage()
    {
        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModelByUserid(ViewUserId);  // 获得当前登录用户的离职单信息
        List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> departments = EmployeesInPositionsManager.GetModelList(" a.UserID=" + dimissionFormInfo.UserId);  // 用户部门信息
        ESP.HumanResource.Entity.EmployeeBaseInfo employeeModel = EmployeeBaseManager.GetModel(dimissionFormInfo.UserId);  // 用户基本信息

        labCode.Text = employeeModel.Code;
        txtPhone.Text = employeeModel.Phone1;
        if (departments != null && departments.Count > 0)
        {
            List<ESP.Framework.Entity.DepartmentInfo> deps = new List<ESP.Framework.Entity.DepartmentInfo>();
            ESP.Framework.BusinessLogic.DepartmentManager.GetDepartmentListByID(departments[0].GroupID, deps);
            if (deps.Count == 3)
            {
                foreach (ESP.Framework.Entity.DepartmentInfo department in deps)
                {
                    if (department.DepartmentLevel == 1)
                    {
                        txtcompanyName.Text = department.DepartmentName;
                        hidcompanyId.Value = department.DepartmentID.ToString();
                    }
                    else if (department.DepartmentLevel == 2)
                    {
                        txtdepartmentName.Text = department.DepartmentName;
                        hiddepartmentId.Value = department.DepartmentID.ToString();
                    }
                    else if (department.DepartmentLevel == 3)
                    {
                        txtgroupName.Text = department.DepartmentName;
                        hidgroupId.Value = department.DepartmentID.ToString();
                    }
                }
            }
            else if (deps.Count == 2)
            {
                foreach (ESP.Framework.Entity.DepartmentInfo department in deps)
                {
                    if (department.DepartmentLevel == 2)
                    {
                        txtdepartmentName.Text = department.DepartmentName;
                        hiddepartmentId.Value = department.DepartmentID.ToString();
                    }
                }
            }
            txtPosition.Text = departments[0].DepartmentPositionName;
        }

        //入职日期
        ESP.HumanResource.Entity.EmployeeJobInfo employeeJobModel = EmployeeJobManager.getModelBySysId(dimissionFormInfo.UserId);
        txtjoinJobDate.Text = employeeModel == null ? "1900-01-01" : employeeJobModel.joinDate.ToString("yyyy-MM-dd");

        ESP.Framework.Entity.UserInfo userinfo = ESP.Framework.BusinessLogic.UserManager.Get(dimissionFormInfo.UserId);
        this.txtuserName.Text = userinfo.FullNameCN;
        // 判断离职单信息是否不为空
        #region 根据离职单状态判断页面显示内容
        if (dimissionFormInfo != null)
        {
            hidDimissionFormID.Value = dimissionFormInfo.DimissionId.ToString();
            DimissionFormId = dimissionFormInfo.DimissionId;
            txtdimissionCause.Text = dimissionFormInfo.Reason;
            txtMobilePhone.Text = dimissionFormInfo.MobilePhone;
            txtEmail.Text = dimissionFormInfo.PrivateMail;

            string strStatus = "未提交";
            switch (dimissionFormInfo.Status)
            {
                case (int)ESP.HumanResource.Common.DimissionFormStatus.NotSubmit:
                    strStatus = "未提交";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitDirector:
                    strStatus = "待总监审批";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitReceiver:
                    strStatus = "待交接人确认";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitManager:
                    strStatus = "待总经理审批";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitGroupHR:
                    strStatus = "待团队行政审批";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHR1:
                    strStatus = "待集团人力资源审批";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHRIT:
                    strStatus = "待集团人力资源、IT部审批";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitFinance:
                    strStatus = "待财务审批";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitIT:
                    strStatus = "待IT确认";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitAdministration:
                    strStatus = "待集团行政审批";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHR2:
                    strStatus = "待集团人力资源审批";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.AuditComplete:
                    strStatus = "审批通过";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.Overrule:
                    strStatus = "审批驳回";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHRDirector:
                    strStatus = "待集团人力总监审批";
                    break;
                case (int)ESP.HumanResource.Common.DimissionFormStatus.WaitPreAuditor:
                    strStatus = "待预审";
                    break;
                default:
                    strStatus = "未提交";
                    break;
            }
            labStatus.Text = strStatus;
            lblHRReason.Text = dimissionFormInfo.HRReason;
            lblHRReasonRemark.Text = dimissionFormInfo.HRReasonRemark;


            txtdimissionDate2.Text = dimissionFormInfo.HopeLastDay == null ? "" : dimissionFormInfo.HopeLastDay.Value.ToString("yyyy-MM-dd");
            txtdimissionDate.Text = dimissionFormInfo.LastDay == null ? "" : dimissionFormInfo.LastDay.Value.ToString("yyyy-MM-dd");

        }

        List<ESP.HumanResource.Entity.DimissionDetailsInfo> list = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.GetDimissionData(dimissionFormInfo.UserId, dimissionFormInfo.DimissionId);
        SetDimissionInfo(list);

        if (dimissionFormInfo.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitDirector || dimissionFormInfo.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitPreAuditor)
        {
            pnlManageAudit.Visible = false;
            pnlGroupAudit.Visible = false;
            pnlHRAudit1.Visible = false;
            pnlFinanceAudit.Visible = false;
            pnlITAudit.Visible = false;
            pnlADAudit.Visible = false;
            pnlHRAudit2.Visible = false;

            //gvDetailList.DataSource = list;
            //gvDetailList.DataBind();
            //labAllNum.Text = labAllNumT.Text = gvDetailList.Rows.Count.ToString();
            pnlPerformance.Visible = false;
            radIsNormalPerTure.Enabled = false;
            radIsNormalPerFalse.Enabled = false;
            txtSumPerformance.Enabled = false;
        }
        else if (dimissionFormInfo.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitReceiver)
        {
            txtMobilePhone.Enabled = false;
            txtPhone.Enabled = false;
            txtEmail.Enabled = false;
            txtdimissionDate2.Enabled = false;
            txtdimissionCause.Enabled = false;

            pnlManageAudit.Visible = true;
            pnlGroupAudit.Visible = false;
            pnlHRAudit1.Visible = false;
            pnlFinanceAudit.Visible = false;
            pnlITAudit.Visible = false;
            pnlADAudit.Visible = false;
            pnlHRAudit2.Visible = false;

            gvDetailView.DataSource = list;
            gvDetailView.DataBind();
            labManNumberB.Text = labManNumberT.Text = gvDetailView.Rows.Count.ToString();
            pnlPerformance.Visible = false;
            radIsNormalPerTure.Enabled = false;
            radIsNormalPerFalse.Enabled = false;
            txtSumPerformance.Enabled = false;
        }
        else if (dimissionFormInfo.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitManager)
        {
            txtMobilePhone.Enabled = false;
            txtPhone.Enabled = false;
            txtEmail.Enabled = false;
            txtdimissionDate2.Enabled = false;
            txtdimissionCause.Enabled = false;

            pnlManageAudit.Visible = true;
            pnlGroupAudit.Visible = false;
            pnlHRAudit1.Visible = false;
            pnlFinanceAudit.Visible = false;
            pnlITAudit.Visible = false;
            pnlADAudit.Visible = false;
            pnlHRAudit2.Visible = false;

            gvDetailView.DataSource = list;
            gvDetailView.DataBind();
            labManNumberB.Text = labManNumberT.Text = gvDetailView.Rows.Count.ToString();
            pnlPerformance.Visible = false;
            radIsNormalPerTure.Enabled = false;
            radIsNormalPerFalse.Enabled = false;
            txtSumPerformance.Enabled = false;
        }
        else if (dimissionFormInfo.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitGroupHR)
        {
            txtMobilePhone.Enabled = false;
            txtPhone.Enabled = false;
            txtEmail.Enabled = false;
            txtdimissionDate2.Enabled = false;
            txtdimissionCause.Enabled = false;

            pnlManageAudit.Visible = true;
            pnlGroupAudit.Visible = false;
            pnlHRAudit1.Visible = false;
            pnlFinanceAudit.Visible = false;
            pnlITAudit.Visible = false;
            pnlADAudit.Visible = false;
            pnlHRAudit2.Visible = false;

            gvDetailView.DataSource = list;
            gvDetailView.DataBind();
            labManNumberB.Text = labManNumberT.Text = gvDetailView.Rows.Count.ToString();
            SetMonthStat(dimissionFormInfo.UserId, dimissionFormInfo.LastDay.Value);
            SetAnnualLeaveInfo(dimissionFormInfo.UserId, dimissionFormInfo.LastDay.Value, dimissionFormInfo.DimissionId);
            pnlPerformance.Visible = true;
            radIsNormalPerTure.Enabled = false;
            radIsNormalPerFalse.Enabled = false;
            txtSumPerformance.Enabled = false;
        }
        else if (dimissionFormInfo.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHRIT)  // 等待集团人力资源、IT审批
        {
            txtMobilePhone.Enabled = false;
            txtPhone.Enabled = false;
            txtEmail.Enabled = false;
            txtdimissionDate2.Enabled = false;
            txtdimissionCause.Enabled = false;

            pnlManageAudit.Visible = true;
            pnlGroupAudit.Visible = true;
            pnlHRAudit1.Visible = false;
            pnlFinanceAudit.Visible = false;
            pnlITAudit.Visible = false;
            pnlADAudit.Visible = false;
            pnlHRAudit2.Visible = false;

            gvDetailView.DataSource = list;
            gvDetailView.DataBind();
            labManNumberB.Text = labManNumberT.Text = gvDetailView.Rows.Count.ToString();
            SetMonthStat(dimissionFormInfo.UserId, dimissionFormInfo.LastDay.Value);
            SetAnnualLeaveInfo(dimissionFormInfo.UserId, dimissionFormInfo.LastDay.Value, dimissionFormInfo.DimissionId);

            //SetInsDateTime(dimissionFormInfo.DimissionId);
            pnlPerformance.Visible = true;
            radIsNormalPerTure.Enabled = false;
            radIsNormalPerFalse.Enabled = false;
            txtSumPerformance.Enabled = false;
        }
        else if (dimissionFormInfo.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitFinance) // 财务审批权限
        {
            if (dimissionFormInfo.FinanceAuditStatus == 1) // 财务一级审批
            {
                pnlFinanceAuditLevel1.Visible = true;
                //pnlFinanceAuditLevel2.Visible = false;
                pnlFinanceAuditLevel3.Visible = false;
                pnlFinanceAuditLevel4.Visible = false;
            }
            else if (dimissionFormInfo.FinanceAuditStatus == 2) // 财务二级审批
            {
                pnlFinanceAuditLevel1.Visible = true;
                //pnlFinanceAuditLevel2.Visible = true;
                pnlFinanceAuditLevel3.Visible = false;
                pnlFinanceAuditLevel4.Visible = false;
            }
            else if (dimissionFormInfo.FinanceAuditStatus == 3) // 财务三级审批（工资结算）
            {
                pnlFinanceAuditLevel1.Visible = true;
                //pnlFinanceAuditLevel2.Visible = true;
                pnlFinanceAuditLevel3.Visible = true;
                pnlFinanceAuditLevel4.Visible = false;
            }
            else if (dimissionFormInfo.FinanceAuditStatus == 4) // IT部审批权限
            {
                pnlFinanceAuditLevel1.Visible = true;
                //pnlFinanceAuditLevel2.Visible = true;
                pnlFinanceAuditLevel3.Visible = true;
                pnlFinanceAuditLevel4.Visible = true;
            }

            txtMobilePhone.Enabled = false;
            txtPhone.Enabled = false;
            txtEmail.Enabled = false;
            txtdimissionDate2.Enabled = false;
            txtdimissionCause.Enabled = false;

            pnlManageAudit.Visible = true;
            pnlGroupAudit.Visible = true;
            pnlHRAudit1.Visible = true;
            pnlITAudit.Visible = true;
            pnlFinanceAudit.Visible = false;
            pnlADAudit.Visible = false;
            pnlHRAudit2.Visible = false;

            // IT部审批信息
            txtCompanyEmail.Enabled = false;
            radEmailIsDeleteTrue.Enabled = false;
            radEmailIsDeleteFalse.Enabled = false;
            txtEmailSaveLastDay.Enabled = false;
            txtPCCode.Enabled = false;
            txtPCUsedDes.Enabled = false;
            txtITOther.Enabled = false;
            txtOwnPCCode.Enabled = false;
            SetITInfo(dimissionFormInfo.DimissionId);

            txtHRRemark.Enabled = false;
            SetInsDateTime(dimissionFormInfo.DimissionId);

            gvDetailView.DataSource = list;
            gvDetailView.DataBind();
            labManNumberB.Text = labManNumberT.Text = gvDetailView.Rows.Count.ToString();
            SetMonthStat(dimissionFormInfo.UserId, dimissionFormInfo.LastDay.Value);
            SetAnnualLeaveInfo(dimissionFormInfo.UserId, dimissionFormInfo.LastDay.Value, dimissionFormInfo.DimissionId);
            pnlPerformance.Visible = true;
            radIsNormalPerTure.Enabled = false;
            radIsNormalPerFalse.Enabled = false;
            txtSumPerformance.Enabled = false;
        }
        else if (dimissionFormInfo.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitAdministration)  // 集团行政审批权限
        {
            txtMobilePhone.Enabled = false;
            txtPhone.Enabled = false;
            txtEmail.Enabled = false;
            txtdimissionDate2.Enabled = false;
            txtdimissionCause.Enabled = false;

            pnlManageAudit.Visible = true;
            pnlGroupAudit.Visible = true;
            pnlHRAudit1.Visible = true;
            pnlFinanceAudit.Visible = true;
            pnlITAudit.Visible = true;
            pnlADAudit.Visible = false;
            pnlHRAudit2.Visible = false;

            ESP.Administrative.BusinessLogic.UserAttBasicInfoManager userAttBasicManager = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager();
            ESP.Administrative.Entity.UserAttBasicInfo userAttBasicInfo = userAttBasicManager.GetEnableCardByUserid(dimissionFormInfo.UserId);
            if (userAttBasicInfo != null)
                txtDoorCard.Text = userAttBasicInfo.CardNo;

            SetADInfo(dimissionFormInfo.DimissionId);
            SetMonthStat(dimissionFormInfo.UserId, dimissionFormInfo.LastDay.Value);
            SetAnnualLeaveInfo(dimissionFormInfo.UserId, dimissionFormInfo.LastDay.Value, dimissionFormInfo.DimissionId);
            SetInsDateTime(dimissionFormInfo.DimissionId);

            SetFinanceInfo(dimissionFormInfo.DimissionId);
            SetITInfo(dimissionFormInfo.DimissionId);

            pnlPerformance.Visible = true;
            radIsNormalPerTure.Enabled = false;
            radIsNormalPerFalse.Enabled = false;
            txtSumPerformance.Enabled = false;
        }
        else if (dimissionFormInfo.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHR2)
        {
            txtMobilePhone.Enabled = false;
            txtPhone.Enabled = false;
            txtEmail.Enabled = false;
            txtdimissionDate2.Enabled = false;
            txtdimissionCause.Enabled = false;

            pnlManageAudit.Visible = true;
            pnlGroupAudit.Visible = true;
            pnlHRAudit1.Visible = true;
            pnlFinanceAudit.Visible = true;
            pnlITAudit.Visible = true;
            pnlADAudit.Visible = true;
            pnlHRAudit2.Visible = false;

            gvDetailView.DataSource = list;
            gvDetailView.DataBind();
            labManNumberB.Text = labManNumberT.Text = gvDetailView.Rows.Count.ToString();
            SetMonthStat(dimissionFormInfo.UserId, dimissionFormInfo.LastDay.Value);
            SetAnnualLeaveInfo(dimissionFormInfo.UserId, dimissionFormInfo.LastDay.Value, dimissionFormInfo.DimissionId);

            SetInsDateTime(dimissionFormInfo.DimissionId);

            SetFinanceInfo(dimissionFormInfo.DimissionId);
            SetITInfo(dimissionFormInfo.DimissionId);
            SetADInfo(dimissionFormInfo.DimissionId);

            //txtLoan.Enabled = false;
            txtBusinessCard.Enabled = false;
            //txtAccountsPayable.Enabled = false;
            txtOther.Enabled = false;

            txtCompanyEmail.Enabled = false;
            radEmailIsDeleteTrue.Enabled = false;
            radEmailIsDeleteFalse.Enabled = false;
            txtEmailSaveLastDay.Enabled = false;
            //radAccountIsDeleteTrue.Enabled = false;
            //radAccountIsDeleteFalse.Enabled = false;
            //txtAccountSaveLastDay.Enabled = false;
            txtPCCode.Enabled = false;
            txtPCUsedDes.Enabled = false;
            txtITOther.Enabled = false;
            txtOwnPCCode.Enabled = false;

            txtDoorCard.Enabled = false;
            txtLibraryManage.Enabled = false;
            pnlPerformance.Visible = true;
            radIsNormalPerTure.Enabled = false;
            radIsNormalPerFalse.Enabled = false;
            txtSumPerformance.Enabled = false;
        }
        else if (dimissionFormInfo.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.AuditComplete)
        {
            txtMobilePhone.Enabled = false;
            txtPhone.Enabled = false;
            txtEmail.Enabled = false;
            txtdimissionDate2.Enabled = false;
            txtdimissionCause.Enabled = false;

            pnlManageAudit.Visible = true;
            pnlGroupAudit.Visible = true;
            pnlHRAudit1.Visible = true;
            pnlFinanceAudit.Visible = true;
            pnlITAudit.Visible = true;
            pnlADAudit.Visible = true;
            pnlHRAudit2.Visible = true;

            gvDetailView.DataSource = list;
            gvDetailView.DataBind();
            labManNumberB.Text = labManNumberT.Text = gvDetailView.Rows.Count.ToString();
            SetMonthStat(dimissionFormInfo.UserId, dimissionFormInfo.LastDay.Value);
            SetAnnualLeaveInfo(dimissionFormInfo.UserId, dimissionFormInfo.LastDay.Value, dimissionFormInfo.DimissionId);

            SetInsDateTime(dimissionFormInfo.DimissionId);

            SetFinanceInfo(dimissionFormInfo.DimissionId);
            SetITInfo(dimissionFormInfo.DimissionId);
            SetADInfo(dimissionFormInfo.DimissionId);

            txtHRRemark.Enabled = false;

            //txtLoan.Enabled = false;
            txtBusinessCard.Enabled = false;
            //txtAccountsPayable.Enabled = false;
            txtOther.Enabled = false;

            txtCompanyEmail.Enabled = false;
            radEmailIsDeleteTrue.Enabled = false;
            radEmailIsDeleteFalse.Enabled = false;
            txtEmailSaveLastDay.Enabled = false;
            //radAccountIsDeleteTrue.Enabled = false;
            //radAccountIsDeleteFalse.Enabled = false;
            //txtAccountSaveLastDay.Enabled = false;
            txtPCCode.Enabled = false;
            txtPCUsedDes.Enabled = false;
            txtITOther.Enabled = false;
            txtOwnPCCode.Enabled = false;

            txtDoorCard.Enabled = false;
            txtLibraryManage.Enabled = false;
            pnlPerformance.Visible = true;
            radIsNormalPerTure.Enabled = false;
            radIsNormalPerFalse.Enabled = false;
            txtSumPerformance.Enabled = false;
        }
        else
        {
            pnlManageAudit.Visible = true;
            pnlGroupAudit.Visible = true;
            gvDetailView.DataSource = list;
            gvDetailView.DataBind();
            labManNumberB.Text = labManNumberT.Text = gvDetailView.Rows.Count.ToString();
            pnlPerformance.Visible = false;
            radIsNormalPerTure.Enabled = false;
            radIsNormalPerFalse.Enabled = false;
            txtSumPerformance.Enabled = false;
        }
        #endregion

        // 报销单据
        DataSet oopDataSet = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.GetDimissionOOP(dimissionFormInfo.UserId);
        gvOOP2.DataSource = oopDataSet;
        gvOOP2.DataBind();
        labOOP2B.Text = labOOP2T.Text = gvOOP2.Rows.Count.ToString();

        // 审批日志信息
        string strAuditLog = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetStrAuditLogInfos(dimissionFormInfo.DimissionId,
            (int)ESP.HumanResource.Common.HRFormType.DimissionForm);
        labAuditLog.Text = strAuditLog;

        int cardCount = ESP.Finance.BusinessLogic.BusinessCardManager.IsHaveCard(ViewUserId);
        if (dimissionFormInfo.LastDay == null)
        {
            string tipStr = "";
            if (cardCount > 0)
                tipStr = "1.请在Last Day的一周前归还“商务卡”到财务；<br />"
                + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2";
            else
                tipStr = "1";



            string itTipStr = @"1.如是自带笔记本请在Last Day将网线交IT部；<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.如是使用公司电脑请在Last Day整理个人数据并与IT部联系；<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.如有借用公司固定资产（相机、录音笔等）请在Last Day交还于IT部。<br />";
            labTip.Text = "<font style=\"font-weight: bolder; color: Red;\">财务提示：" + tipStr + ".请在Last Day的两天前提交报销、冲销、发票。<br />IT提示：" + itTipStr + "</font>";
        }
        else
        {
            string tipStr = "";
            if (cardCount > 0)
                tipStr = "1.请在" + dimissionFormInfo.LastDay.Value.AddDays(-7).ToString("yyyy-MM-dd") + "前归还“商务卡”到财务；"
                + "<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2";
            else
                tipStr = "1";

            // 计算剩余年假天数和预支年假天数
            //double remainAnnual = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetAnnualLeaveInfo(dimissionFormInfo.UserId, dimissionFormInfo.LastDay.Value);
            //string annualStr = "0";
            //string advanceAnnualStr = "0";
            //if (remainAnnual > 0)
            //{
            //    annualStr = remainAnnual.ToString();
            //    advanceAnnualStr = "0";
            //}
            //else if (remainAnnual == 0)
            //{
            //    annualStr = "0";
            //    advanceAnnualStr = "0";
            //}
            //else
            //{
            //    annualStr = "0";
            //    advanceAnnualStr = Math.Abs(remainAnnual).ToString();
            //}
            //&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;4.截止到离职当天，您年假余 " + annualStr + " 天，预支年假假 " + advanceAnnualStr + " 天";

            string itTipStr = @"1.如是自带笔记本请在Last Day将网线交IT部；<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.如是使用公司电脑请在Last Day整理个人数据并与IT部联系；<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.如有借用公司固定资产（相机、录音笔等）请在Last Day交还于IT部。<br />";
            labTip.Text = "<font style=\"font-weight: bolder; color: Red;\">财务提示：" + tipStr + ".请在" + dimissionFormInfo.LastDay.Value.AddDays(-2).ToString("yyyy-MM-dd") + "前提交报销、冲销、发票。<br />IT提示：" + itTipStr + "</font>";
        }
        SetCashInfo(dimissionFormInfo.UserId);
    }

    /// <summary>
    /// 设置离职单据信息
    /// </summary>
    protected void SetDimissionInfo(List<ESP.HumanResource.Entity.DimissionDetailsInfo> list)
    {
        if (list != null && list.Count > 0)
        {
            Dictionary<string, ESP.HumanResource.Entity.DimissionDetailsInfo> dimissionInfo = new Dictionary<string, ESP.HumanResource.Entity.DimissionDetailsInfo>();
            foreach (ESP.HumanResource.Entity.DimissionDetailsInfo dimissionDetailInfo in list)
            {
                if (!dimissionInfo.ContainsKey(dimissionDetailInfo.FormId + "-" + dimissionDetailInfo.FormType))
                {
                    dimissionInfo.Add(dimissionDetailInfo.FormId + "-" + dimissionDetailInfo.FormType, dimissionDetailInfo);
                }
            }
            DicDimission = dimissionInfo;
        }
    }

    protected void gvDetailList_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            Label lab = e.Row.FindControl("labReceiverName") as Label;
            HiddenField hid = e.Row.FindControl("hidReceiverName") as HiddenField;
            ESP.HumanResource.Entity.DimissionDetailsInfo detailsInfo = e.Row.DataItem as ESP.HumanResource.Entity.DimissionDetailsInfo;
            lab.Text = detailsInfo.ReceiverName;
            hid.Value = detailsInfo.ReceiverId.ToString();

        }
    }

    protected void gvDetailView_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            Label lab = e.Row.FindControl("labReceiverStatus") as Label;

            Label labReceiver = e.Row.FindControl("labReceiverName") as Label;
            HiddenField hid = e.Row.FindControl("hidReceiverName") as HiddenField;

            ESP.HumanResource.Entity.DimissionDetailsInfo detailsInfo = e.Row.DataItem as ESP.HumanResource.Entity.DimissionDetailsInfo;
            if (detailsInfo.Status == (int)ESP.HumanResource.Common.AuditStatus.NotAudit)
                lab.Text = "未确认";
            else if (detailsInfo.Status == (int)ESP.HumanResource.Common.AuditStatus.Audited)
                lab.Text = "已确认";
            else
                lab.Text = "已驳回";

            labReceiver.Text = detailsInfo.ReceiverName;
            hid.Value = detailsInfo.ReceiverId.ToString();
        }
    }

    /// <summary>
    /// 设置当前用户所选择的月份的考情统计信息
    /// </summary>
    protected void SetMonthStat(int dimissionUserID, DateTime dimissionDate)
    {
        ESP.Administrative.BusinessLogic.UserAttBasicInfoManager userBasicManager = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager();
        ESP.Administrative.BusinessLogic.AttendanceManager attMan = new ESP.Administrative.BusinessLogic.AttendanceManager();
        UserAttBasicInfo userBasicModel = userBasicManager.GetModelByUserid(dimissionUserID);
        if (userBasicModel != null)
        {
            DateTime preMonth = dimissionDate.AddMonths(-1);
            // 员工上月考勤信息
            //AttendanceDataInfo attdatainfo = attMan.GetMonthStat(dimissionUserID,
            //    preMonth.Year, preMonth.Month, null, null, null);
            ESP.Administrative.BusinessLogic.MonthStatManager monthStat = new ESP.Administrative.BusinessLogic.MonthStatManager();
            ESP.Administrative.Entity.MonthStatInfo preMonthStatInfo = monthStat.GetMonthStatInfoApprove(dimissionUserID, preMonth.Year, preMonth.Month);

            // 员工当月考勤信息
            //AttendanceDataInfo CurAttdatainfo = attMan.GetMonthStat(dimissionUserID,
            //    dimissionDate.Year, dimissionDate.Month, null, null, null);
            ESP.Administrative.Entity.MonthStatInfo curMonthStatInfo = monthStat.GetMonthStatInfoApprove(dimissionUserID, dimissionDate.Year, dimissionDate.Month);

            #region 上月考勤
            if (preMonthStatInfo != null)
            {
                if (preMonthStatInfo.LateCount > 0)
                    labLate.Text = preMonthStatInfo.LateCount + "";    // 迟到
                else
                    labLate.Text = "";    // 迟到

                if (preMonthStatInfo.LeaveEarlyCount > 0)
                    labLeaveEarly.Text = preMonthStatInfo.LeaveEarlyCount + "";   // 早退
                else
                    labLeaveEarly.Text = "";

                if (preMonthStatInfo.AbsentDays > 0)
                    labAbsent.Text = string.Format("{0:F1}", preMonthStatInfo.AbsentDays / Status.WorkingHours) + "D";    // 旷工
                else
                    labAbsent.Text = "";

                if (preMonthStatInfo.OverTimeHours > 0)
                    labOverTime.Text = string.Format("{0:F1}", preMonthStatInfo.OverTimeHours) + "H";   // OT
                else
                    labOverTime.Text = "";

                if (preMonthStatInfo.HolidayOverTimeHours > 0)
                    labHolidayOverTime.Text = string.Format("{0:F1}", preMonthStatInfo.HolidayOverTimeHours) + "H";   // 节假日OT
                else
                    labHolidayOverTime.Text = "";

                if (preMonthStatInfo.EvectionDays > 0)
                    labEvection.Text = string.Format("{0:F1}", preMonthStatInfo.EvectionDays / Status.WorkingHours) + "D";  // 出差
                else
                    labEvection.Text = "";

                if (preMonthStatInfo.EgressHours > 0)
                    labEgress.Text = string.Format("{0:F3}", preMonthStatInfo.EgressHours) + "H";   // 外出
                else
                    labEgress.Text = "";

                if (preMonthStatInfo.SickLeaveHours > 0)
                    labSickLeave.Text = string.Format("{0:F3}", preMonthStatInfo.SickLeaveHours) + "H"; // 病假
                else
                    labSickLeave.Text = "";

                // 年度累计病假
                decimal userAllYearSickLeave = new ESP.Administrative.BusinessLogic.MonthStatManager().GetUserAllYearSickLeaveDay(dimissionUserID,
                    preMonth.Year, Status.AttendanceStatItem.SickLeaveHours);
                int userAllYearSickLeaveDays = (int)(userAllYearSickLeave / Status.WorkingHours);
                int userAllYearSickLeaveHours = (int)(userAllYearSickLeave % Status.WorkingHours);
                string tempUserAllYearSickLeaveHours = (userAllYearSickLeaveDays == 0 ? "" : userAllYearSickLeaveDays + "D") + (userAllYearSickLeaveHours == 0 ? "" : userAllYearSickLeaveHours + "H");
                labAddupSickLeave.Text = tempUserAllYearSickLeaveHours;

                if (preMonthStatInfo.AffairLeaveHours > 0)
                    labAffiairLeave.Text = string.Format("{0:F3}", preMonthStatInfo.AffairLeaveHours) + "H";  // 事假
                else
                    labAffiairLeave.Text = "";

                if (preMonthStatInfo.AnnualLeaveDays > 0)
                    labAnnualLeave.Text = string.Format("{0:F3}", preMonthStatInfo.AnnualLeaveDays / Status.WorkingHours) + "D";  // 年假
                else
                    labAnnualLeave.Text = "";

                if (preMonthStatInfo.FuneralLeaveHours > 0)
                    labFuneralLeave.Text = string.Format("{0:F1}", preMonthStatInfo.FuneralLeaveHours / Status.WorkingHours) + "D";   // 丧假
                else
                    labFuneralLeave.Text = "";

                if (preMonthStatInfo.MarriageLeaveHours > 0)
                    labMarriageLeave.Text = string.Format("{0:F1}", preMonthStatInfo.MarriageLeaveHours / Status.WorkingHours) + "D";  // 婚假
                else
                    labMarriageLeave.Text = "";

                if (preMonthStatInfo.MaternityLeaveHours > 0)
                    labMaternityLeave.Text = string.Format("{0:F1}", preMonthStatInfo.MaternityLeaveHours / Status.WorkingHours) + "D";  // 产假
                else
                    labMaternityLeave.Text = "";

                if (preMonthStatInfo.PrenatalCheckHours > 0)
                    labPrenatalCheck.Text = string.Format("{0:F3}", preMonthStatInfo.PrenatalCheckHours / Status.WorkingHours) + "D";   // 产前检查
                else
                    labPrenatalCheck.Text = "";

                //if (preMonthStatInfo.IncentiveHours > 0)
                //    labIncentive.Text = string.Format("{0:F1}", preMonthStatInfo.IncentiveHours / Status.WorkingHours) + "D";      // 奖励假
                //else
                //    labIncentive.Text = "";

                if (preMonthStatInfo.PCRefundAmount > 0)
                    labPCRefund.Text = preMonthStatInfo.PCRefundAmount.ToString();
                else
                    labPCRefund.Text = "";
            }

            if (preMonthStatInfo != null)
            {
                if (preMonthStatInfo.State == Status.MonthStatAppState_NoSubmit)
                {
                    labAttPreAuditStatus.Text = "<font style=\"font-weight: bolder; color: Red;\">未提交</font>";
                }
                else if (preMonthStatInfo.State == Status.MonthStatAppState_WaitDirector)
                {
                    labAttPreAuditStatus.Text = "等待总监审批";
                }
                else if (preMonthStatInfo.State == Status.MonthStatAppState_WaitStatisticians)
                {
                    labAttPreAuditStatus.Text = "等待考勤统计员审批";
                }
                else if (preMonthStatInfo.State == Status.MonthStatAppState_Passed)
                {
                    labAttPreAuditStatus.Text = "审批通过";
                }
                else if (preMonthStatInfo.State == Status.MonthStatAppState_WaitADAdmin)
                {
                    labAttPreAuditStatus.Text = "等待考勤管理员确认";
                }
                else if (preMonthStatInfo.State == Status.MonthStatAppState_WaitHRAdmin)
                {
                    labAttPreAuditStatus.Text = "等待团队HR审批";
                }
                else if (preMonthStatInfo.State == Status.MonthStatAppState_WaitManager)
                {
                    labAttPreAuditStatus.Text = "等待团队总经理审批";
                }
                else if (preMonthStatInfo.State == Status.MonthStatAppState_Overrule)
                {
                    labAttPreAuditStatus.Text = "审批驳回";
                }
            }
            else
            {
                labAttPreAuditStatus.Text = "<font style=\"font-weight: bolder; color: Red;\">未提交</font>";
            }
            #endregion

            #region 当月考勤
            if (curMonthStatInfo != null)
            {
                if (curMonthStatInfo.LateCount > 0)
                    labLateCur.Text = curMonthStatInfo.LateCount + "";    // 迟到
                else
                    labLateCur.Text = "";    // 迟到

                if (curMonthStatInfo.LeaveEarlyCount > 0)
                    labLeaveEarlyCur.Text = curMonthStatInfo.LeaveEarlyCount + "";   // 早退
                else
                    labLeaveEarlyCur.Text = "";

                if (curMonthStatInfo.AbsentDays > 0)
                    labAbsentCur.Text = string.Format("{0:F1}", curMonthStatInfo.AbsentDays / Status.WorkingHours) + "D";    // 旷工
                else
                    labAbsentCur.Text = "";

                if (curMonthStatInfo.OverTimeHours > 0)
                    labOverTimeCur.Text = string.Format("{0:F1}", curMonthStatInfo.OverTimeHours) + "H";   // OT
                else
                    labOverTimeCur.Text = "";

                if (curMonthStatInfo.HolidayOverTimeHours > 0)
                    labHolidayOverTimeCur.Text = string.Format("{0:F1}", curMonthStatInfo.HolidayOverTimeHours) + "H";   // 节假日OT
                else
                    labHolidayOverTimeCur.Text = "";

                if (curMonthStatInfo.EvectionDays > 0)
                    labEvectionCur.Text = string.Format("{0:F1}", curMonthStatInfo.EvectionDays / Status.WorkingHours) + "D";  // 出差
                else
                    labEvectionCur.Text = "";

                if (curMonthStatInfo.EgressHours > 0)
                    labEgressCur.Text = string.Format("{0:F3}", curMonthStatInfo.EgressHours) + "H";   // 外出
                else
                    labEgressCur.Text = "";

                if (curMonthStatInfo.SickLeaveHours > 0)
                    labSickLeaveCur.Text = string.Format("{0:F3}", curMonthStatInfo.SickLeaveHours) + "H"; // 病假
                else
                    labSickLeaveCur.Text = "";

                // 年度累计病假
                decimal userAllYearSickLeave = new ESP.Administrative.BusinessLogic.MonthStatManager().GetUserAllYearSickLeaveDay(dimissionUserID,
                    dimissionDate.Year, Status.AttendanceStatItem.SickLeaveHours);
                int userAllYearSickLeaveDays = (int)(userAllYearSickLeave / Status.WorkingHours);
                int userAllYearSickLeaveHours = (int)(userAllYearSickLeave % Status.WorkingHours);
                string tempUserAllYearSickLeaveHours = (userAllYearSickLeaveDays == 0 ? "" : userAllYearSickLeaveDays + "D") + (userAllYearSickLeaveHours == 0 ? "" : userAllYearSickLeaveHours + "H");
                labAddupSickLeaveCur.Text = tempUserAllYearSickLeaveHours;

                if (curMonthStatInfo.AffairLeaveHours > 0)
                    labAffiairLeaveCur.Text = string.Format("{0:F3}", curMonthStatInfo.AffairLeaveHours) + "H";  // 事假
                else
                    labAffiairLeaveCur.Text = "";

                if (curMonthStatInfo.AnnualLeaveDays > 0)
                    labAnnualLeaveCur.Text = string.Format("{0:F3}", curMonthStatInfo.AnnualLeaveDays / Status.WorkingHours) + "D";  // 年假
                else
                    labAnnualLeaveCur.Text = "";

                if (curMonthStatInfo.FuneralLeaveHours > 0)
                    labFuneralLeaveCur.Text = string.Format("{0:F1}", curMonthStatInfo.FuneralLeaveHours / Status.WorkingHours) + "D";   // 丧假
                else
                    labFuneralLeaveCur.Text = "";

                if (curMonthStatInfo.MarriageLeaveHours > 0)
                    labMarriageLeaveCur.Text = string.Format("{0:F1}", curMonthStatInfo.MarriageLeaveHours / Status.WorkingHours) + "D";  // 婚假
                else
                    labMarriageLeaveCur.Text = "";

                if (curMonthStatInfo.MaternityLeaveHours > 0)
                    labMaternityLeaveCur.Text = string.Format("{0:F1}", curMonthStatInfo.MaternityLeaveHours / Status.WorkingHours) + "D";  // 产假
                else
                    labMaternityLeaveCur.Text = "";

                if (curMonthStatInfo.PrenatalCheckHours > 0)
                    labPrenatalCheckCur.Text = string.Format("{0:F3}", curMonthStatInfo.PrenatalCheckHours / Status.WorkingHours) + "D";   // 产前检查
                else
                    labPrenatalCheckCur.Text = "";

                //if (curMonthStatInfo.IncentiveHours > 0)
                //    labIncentiveCur.Text = string.Format("{0:F1}", curMonthStatInfo.IncentiveHours / Status.WorkingHours) + "D";      // 奖励假
                //else
                //    labIncentiveCur.Text = "";

                if (curMonthStatInfo.PCRefundAmount > 0)
                    labPCRefundCur.Text = curMonthStatInfo.PCRefundAmount.ToString();
                else
                    labPCRefundCur.Text = "";
            }

            if (curMonthStatInfo != null)
            {
                if (curMonthStatInfo.State == Status.MonthStatAppState_NoSubmit)
                {
                    labAttCurAuditStatus.Text = "<font style=\"font-weight: bolder; color: Red;\">未提交</font>";
                }
                else if (curMonthStatInfo.State == Status.MonthStatAppState_WaitDirector)
                {
                    labAttCurAuditStatus.Text = "等待总监审批";
                }
                else if (curMonthStatInfo.State == Status.MonthStatAppState_WaitStatisticians)
                {
                    labAttCurAuditStatus.Text = "等待考勤统计员审批";
                }
                else if (curMonthStatInfo.State == Status.MonthStatAppState_Passed)
                {
                    labAttCurAuditStatus.Text = "审批通过";
                }
                else if (curMonthStatInfo.State == Status.MonthStatAppState_WaitADAdmin)
                {
                    labAttCurAuditStatus.Text = "等待考勤管理员确认";
                }
                else if (curMonthStatInfo.State == Status.MonthStatAppState_WaitHRAdmin)
                {
                    labAttCurAuditStatus.Text = "等待团队HR审批";
                }
                else if (curMonthStatInfo.State == Status.MonthStatAppState_WaitManager)
                {
                    labAttCurAuditStatus.Text = "等待团队总经理审批";
                }
                else if (curMonthStatInfo.State == Status.MonthStatAppState_Overrule)
                {
                    labAttCurAuditStatus.Text = "审批驳回";
                }
            }
            else
            {
                labAttCurAuditStatus.Text = "<font style=\"font-weight: bolder; color: Red;\">未提交</font>";
            }
            #endregion
        }
    }

    /// <summary>
    /// 设置年假信息
    /// </summary>
    /// <param name="dimissionUserID">离职员工ID</param>
    /// <param name="dimissionDate">离职日期</param>
    protected void SetAnnualLeaveInfo(int dimissionUserID, DateTime dimissionDate, int dimissionId)
    {
        ESP.HumanResource.Entity.DimissionGrougHRDetailsInfo groupHRDetailInfo =
            ESP.HumanResource.BusinessLogic.DimissionGrougHRDetailsManager.GetGroupHRDetailInfo(dimissionId);
        if (groupHRDetailInfo != null)
        {
            //labAnnual.Text = groupHRDetailInfo.RemainAnnual.ToString();
            //labAdvanceAnnual.Text = groupHRDetailInfo.AdvanceAnnual.ToString();
            txtOfficeSupplies.Text = groupHRDetailInfo.FixedAssets;
            txtOfficeSupplies.Enabled = false;
        }

        double annualBase = 0;
        double rewardBase = 0;

        double remainAnnual = 0;
        double canUseAnnual = 0;
        double usedAnnual = 0;

        double remainReward = 0;
        double canUseReward = 0;
        double usedReward = 0;
        try
        {
            remainAnnual = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetAnnualLeaveInfo(dimissionUserID, dimissionDate, out canUseAnnual, out usedAnnual, out annualBase);
            remainReward = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetRewardLeaveInfo(dimissionUserID, dimissionDate, out canUseReward, out usedReward, out rewardBase);
        }
        catch
        {
            remainAnnual = 0;
            remainReward = 0;
        }
        //
        double canUseTotal = (canUseAnnual + canUseReward);//2.5+2.5

        int tempdays = (int)canUseTotal;
        //if ((tempdays + 0.5) >= canUseTotal)
        //    canUseTotal = tempdays;
        //else
        //    canUseTotal = tempdays + 0.5;

        canUseTotal = tempdays;


        double yuzhiTotal = (usedAnnual + usedReward) - canUseTotal > 0 ? (usedAnnual + usedReward) - canUseTotal : 0;//1
        double yuzhiAnnual = 0;// yuzhiTotal - usedReward > 0 ? yuzhiTotal - usedReward : 0;//0
        double yuzhiReward = 0;// usedReward - canUseReward > 0 ? usedReward - canUseReward : 0;//0
        //6.5-3.5
        if (canUseTotal >= annualBase)
            yuzhiReward = yuzhiTotal;
        else
        {
            if (yuzhiTotal <= annualBase)
            {
                if (usedAnnual + usedReward > annualBase)
                {
                    yuzhiReward = usedReward;
                    yuzhiAnnual = yuzhiTotal - usedReward;
                }
                else
                {
                    yuzhiAnnual = yuzhiTotal;
                    yuzhiReward = 0;
                }
            }
            else
            {
                yuzhiReward = usedReward;
                yuzhiAnnual = yuzhiTotal - usedReward;
            }
        }
        //年假余

        double remainTotal = remainAnnual;// +remainReward;
        if (remainTotal <1 && remainAnnual <= 0)
            remainTotal = 0;
        labAnnual.Text = remainTotal < 0 ? "0" : ((int)remainTotal).ToString("#,##0.000");
        labOverDraft.Text = yuzhiTotal.ToString("#,##0.000");
        labOverAnnual.Text = yuzhiAnnual.ToString("#,##0.000");

        labOverReward.Text = yuzhiReward.ToString("#,##0.000");

    }

    /// <summary>
    /// 设置IT部审批信息
    /// </summary>
    /// <param name="dimissionId">离职单编号</param>
    protected void SetITInfo(int dimissionId)
    {
        ESP.HumanResource.Entity.DimissionITDetailsInfo itDetailInfo =
            ESP.HumanResource.BusinessLogic.DimissionITDetailsManager.GetITDetailInfo(dimissionId);
        if (itDetailInfo != null)
        {
            txtCompanyEmail.Text = itDetailInfo.Email;
            if (itDetailInfo.EmailIsDelete)
            {
                radEmailIsDeleteTrue.Checked = true;
            }
            else
            {
                radEmailIsDeleteFalse.Checked = true;
                txtEmailSaveLastDay.Text = itDetailInfo.EmailSaveLastDay.Value.ToString("yyyy-MM-dd");
            }
            //if (itDetailInfo.AccountIsDelete)
            //{
            //    radAccountIsDeleteTrue.Checked = true;
            //}
            //else
            //{
            //    radAccountIsDeleteFalse.Checked = true;
            //    txtAccountSaveLastDay.Text = itDetailInfo.AccountSaveLastDay.Value.ToString("yyyy-MM-dd");
            //}

            txtPCCode.Text = itDetailInfo.PCCode;
            txtPCUsedDes.Text = itDetailInfo.PCUsedDes;
            txtITOther.Text = itDetailInfo.OtherDes;
            txtOwnPCCode.Text = itDetailInfo.OwnPCCode;
        }
    }

    /// <summary>
    /// 设置集团人力资源审批时社保交费截止时间
    /// </summary>
    /// <param name="dimissionId">离职单编号</param>
    protected void SetInsDateTime(int dimissionId)
    {
        ESP.HumanResource.Entity.DimissionHRDetailsInfo hrDetailInfo = ESP.HumanResource.BusinessLogic.DimissionHRDetailsManager.GetHRDetailInfo(dimissionId);
        int year = DateTime.Now.Year - 5;

        drpAddedMedicalInsY.Items.Insert(0, new ListItem("", ""));  // 补充医疗保险
        for (int i = 0; i < 10; i++)
        {
            drpSocialInsY.Items.Insert(i, new ListItem((year + i).ToString(), (year + i).ToString()));  // 社保
            drpMedicalInsY.Items.Insert(i, new ListItem((year + i).ToString(), (year + i).ToString()));  // 医疗
            drpCapitalReserveY.Items.Insert(i, new ListItem((year + i).ToString(), (year + i).ToString()));  // 住房公积金
            drpAddedMedicalInsY.Items.Insert(i + 1, new ListItem((year + i).ToString(), (year + i).ToString()));  // 补充医疗保险
        }
        try
        {
            drpSocialInsY.SelectedValue = hrDetailInfo.SocialInsLastMonth.Year.ToString();
            drpSocialInsY.Enabled = false;
        }
        catch
        {
            drpSocialInsY.SelectedValue = DateTime.Now.Year.ToString();
        }
        try
        {
            drpMedicalInsY.SelectedValue = hrDetailInfo.MedicalInsLastMonth.Year.ToString();
            drpMedicalInsY.Enabled = false;
        }
        catch
        {
            drpMedicalInsY.SelectedValue = DateTime.Now.Year.ToString();
        }
        try
        {
            drpCapitalReserveY.SelectedValue = hrDetailInfo.CapitalReserveLastMonth.Year.ToString();
            drpCapitalReserveY.Enabled = false;

            // 是否有补充医疗保险
            chkComplementaryMedical.Checked = hrDetailInfo.IsComplementaryMedical;
            chkComplementaryMedical.Enabled = false;
            if (hrDetailInfo.IsComplementaryMedical)
            {
                drpAddedMedicalInsY.SelectedValue =
                    hrDetailInfo.AddedMedicalInsLastMonth != null
                    ? hrDetailInfo.AddedMedicalInsLastMonth.Value.Year.ToString() : "";
                drpAddedMedicalInsY.Enabled = false;
            }
            else
            {
                drpAddedMedicalInsY.Enabled = false;
            }
        }
        catch
        {
            drpCapitalReserveY.SelectedValue = DateTime.Now.Year.ToString();
        }

        drpAddedMedicalInsM.Items.Insert(0, new ListItem("", ""));  // 补充医疗保险
        for (int i = 1; i <= 12; i++)
        {
            drpSocialInsM.Items.Insert(i - 1, new ListItem((i).ToString("00"), (i).ToString("00")));  // 社保
            drpMedicalInsM.Items.Insert(i - 1, new ListItem((i).ToString("00"), (i).ToString("00")));  // 医疗
            drpCapitalReserveM.Items.Insert(i - 1, new ListItem((i).ToString("00"), (i).ToString("00")));  // 住房公积金
            drpAddedMedicalInsM.Items.Insert(i, new ListItem((i).ToString("00"), (i).ToString("00")));  // 补充医疗保险
        }
        try
        {
            drpSocialInsM.SelectedValue = hrDetailInfo.SocialInsLastMonth.Month.ToString("00");
            drpSocialInsM.Enabled = false;
        }
        catch
        {
            drpSocialInsM.SelectedValue = DateTime.Now.Month.ToString("00");
        }
        try
        {
            drpMedicalInsM.SelectedValue = hrDetailInfo.MedicalInsLastMonth.Month.ToString("00");
            drpMedicalInsM.Enabled = false;
        }
        catch
        {
            drpMedicalInsM.SelectedValue = DateTime.Now.Month.ToString("00");
        }
        try
        {
            drpCapitalReserveM.SelectedValue = hrDetailInfo.CapitalReserveLastMonth.Month.ToString("00");
            drpCapitalReserveM.Enabled = false;


            if (hrDetailInfo.IsComplementaryMedical)
            {
                drpAddedMedicalInsM.SelectedValue =
                    hrDetailInfo.AddedMedicalInsLastMonth != null
                    ? hrDetailInfo.AddedMedicalInsLastMonth.Value.Month.ToString() : "";
                drpAddedMedicalInsM.Enabled = false;
            }
            else
            {
                drpAddedMedicalInsM.Enabled = false;
            }
        }
        catch
        {
            drpCapitalReserveM.SelectedValue = DateTime.Now.Month.ToString("00");
        }

        if (hrDetailInfo != null && !string.IsNullOrEmpty(hrDetailInfo.Remark))
            txtHRRemark.Text = hrDetailInfo.Remark;
    }

    /// <summary>
    /// 设置财务部审批信息
    /// </summary>
    /// <param name="dimissionId">离职单编号</param>
    protected void SetFinanceInfo(int dimissionId)
    {
        ESP.HumanResource.Entity.DimissionFinanceDetailsInfo financeDetailInfo =
            ESP.HumanResource.BusinessLogic.DimissionFinanceDetailsManager.GetFinanceDetailInfo(dimissionId);
        if (financeDetailInfo != null)
        {
            labLoan.Text = financeDetailInfo.Loan.Replace("\r\n", "<br />");
            txtBusinessCard.Text = financeDetailInfo.BusinessCard;
            //labAccountsPayable.Text = financeDetailInfo.AccountsPayable.Replace("\r\n", "<br />"); ;
            txtOther.Text = financeDetailInfo.Other;

            if (financeDetailInfo.SalaryTellerId != 0)
            {
                txtSalary.Text = financeDetailInfo.Salary;
                radSalary1.Enabled = false;
                radSalary2.Enabled = false;
                radSalary2.Checked = true;
                txtSalary2.Text = financeDetailInfo.Salary2;
            }
            else
            {
                txtSalary.Text = financeDetailInfo.Salary;
                radSalary1.Enabled = false;
                radSalary2.Enabled = false;
                radSalary1.Checked = true;
                //txtSalary2.Text = financeDetailInfo.Salary2;
            }
        }
    }

    /// <summary>
    /// 设置集团行政审批信息
    /// </summary>
    /// <param name="dimissionId">离职单编号</param>
    protected void SetADInfo(int dimissionId)
    {
        ESP.HumanResource.Entity.DimissionADDetailsInfo adDetailInfo =
            ESP.HumanResource.BusinessLogic.DimissionADDetailsManager.GetADDetailInfo(dimissionId);
        if (adDetailInfo != null)
        {
            txtDoorCard.Text = adDetailInfo.DoorCard;
            txtLibraryManage.Text = adDetailInfo.LibraryManage;
        }
    }

    /// <summary>
    /// 显示用户未处理的单据信息
    /// </summary>
    protected void SetCashInfo(int dimissionUserId)
    {
        DataSet trafficFeeDs = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.GetDimissionTrafficFee(dimissionUserId);
        if (trafficFeeDs != null && trafficFeeDs.Tables.Count > 0 && trafficFeeDs.Tables[0].Rows.Count > 0)
        {
            pnlCash.Visible = true;
            gvCashList.DataSource = trafficFeeDs;
            gvCashList.DataBind();
        }
        else
        {
            pnlCash.Visible = false;
        }
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request["backUrl"]))
        {
            Response.Redirect(Request["backUrl"]);
        }
        else
        {
            Response.Redirect("DimissionAuditList.aspx");
        }
    }
}