using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Web.UI.WebControls;
using ESP.Administrative.Common;
using ESP.Administrative.Entity;
using ESP.HumanResource.BusinessLogic;
using System.Net.Mail;
using System.Data.SqlClient;

public partial class DimissionAuditEdit : ESP.Web.UI.PageBase
{
    private int dimissionid = 0;
    public int depId = 0;
    public bool IsProject = false;
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
        dimissionid = int.Parse(Request["dimissionid"]);
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["dimissionid"]))
            {
                if (int.TryParse(Request["dimissionid"], out dimissionid))
                    InitPage(dimissionid);
            }
        }
    }

    private int GetUsers()
    {
        var currentUserId = ESP.Framework.BusinessLogic.UserManager.GetCurrentUserID();
        List<int> users = new List<int>();
        var model = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(currentUserId).FirstOrDefault();
        if (model != null)
            return model.UserID;
        else return 0;
    }

    /// <summary>
    /// 初始化页面信息
    /// </summary>
    private void InitPage(int dimissionid)
    {
        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionid);  // 获得当前登录用户的离职单信息
        List<ESP.HumanResource.Entity.EmployeesInPositionsInfo> departments = EmployeesInPositionsManager.GetModelList(" a.UserID=" + dimissionFormInfo.UserId);  // 用户部门信息
        ESP.HumanResource.Entity.EmployeeBaseInfo employeeModel = EmployeeBaseManager.GetModel(dimissionFormInfo.UserId);  // 用户基本信息

        ESP.Framework.Entity.OperationAuditManageInfo manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(dimissionFormInfo.UserId);
        if (manageModel == null)
            manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(dimissionFormInfo.DepartmentId);

        ESP.Framework.Entity.DepartmentInfo departmentInfo =
new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager().GetRootDepartmentID(dimissionFormInfo.UserId);


        string terms = " usercode=@usercode and status=@status";
        List<SqlParameter> parms = new List<SqlParameter>();
        parms.Add(new SqlParameter("@usercode", dimissionFormInfo.UserCode));
        parms.Add(new SqlParameter("@status", (int)ESP.Finance.Utility.FixedAssetStatus.Received));

        IList<ESP.Finance.Entity.ITAssetReceivingInfo> assetlist = ESP.Finance.BusinessLogic.ITAssetReceivingManager.GetList(terms, parms);
        this.gvAsset.DataSource = assetlist;
        this.gvAsset.DataBind();

        // 判断该离职单是否是处于当前登录人审批
        bool b = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.CheckDimissionData(UserID, dimissionid);
        bool c = false;
        int userback = GetUsers();
        if (userback != 0)
            c = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.CheckDimissionData(userback, dimissionid);
        if (!b)
        {
            if (!c)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                    "alert('你没有权限操作此离职单！');window.location='DimissionAuditList.aspx';", true);
                return;
            }
        }

        labCode.Text = employeeModel.Code;
        txtWorkCity.Text = employeeModel.WorkCity;
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
                        depId = department.DepartmentID;
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
            // 离职单基本信息
            hidDimissionFormID.Value = dimissionFormInfo.DimissionId.ToString();
            txtdimissionCause.Text = dimissionFormInfo.Reason;
            txtMobilePhone.Text = dimissionFormInfo.MobilePhone;
            txtEmail.Text = dimissionFormInfo.PrivateMail;

            if (dimissionFormInfo.HRReason == "个人原因")
                ddlReason.SelectedIndex = 1;
            else if (dimissionFormInfo.HRReason == "合同到期，甲方不续订")
                ddlReason.SelectedIndex = 2;
            else if (dimissionFormInfo.HRReason == "合同到期，乙方不续订")
                ddlReason.SelectedIndex = 3;
            else if (dimissionFormInfo.HRReason == "因双方约定的应由乙方完成的工作达到预定的标准，终止劳动合同")
                ddlReason.SelectedIndex = 4;
            else if (dimissionFormInfo.HRReason == "公司辞退")
                ddlReason.SelectedIndex = 5;
            else
                ddlReason.SelectedIndex = 0;

            txtReason.Text = dimissionFormInfo.HRReasonRemark;

            List<ESP.HumanResource.Entity.DimissionDetailsInfo> list = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.GetDimissionData(dimissionFormInfo.UserId, dimissionFormInfo.DimissionId);
            //增加线下交接

            SetDimissionInfo(list);

            #region 预审显示内容
            if (dimissionFormInfo.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitPreAuditor)
            {
                txtdimissionDate2.Text = dimissionFormInfo.HopeLastDay == null ? "" : dimissionFormInfo.HopeLastDay.Value.ToString("yyyy-MM-dd");
                pnlDimissionDate1.Visible = true;
                pnlDimissionDate2.Visible = false;
                pnlButtonDirAudit.Visible = true;
                pnlButtonManAudit.Visible = false;
                pnlDirLastDay.Visible = true;
                pnlButtonGroupHRAudit.Visible = false;
                pnlButtonHRAudit.Visible = false;
                pnlButtonFinanceAudit.Visible = false;
                pnlButtonADAudit.Visible = false;
                pnlButtonITAudit.Visible = false;
                pnlButtonHR2Audit.Visible = false;

                txtMobilePhone.Enabled = false;
                txtWorkCity.Enabled = false;
                txtEmail.Enabled = false;
                txtdimissionDate2.Enabled = true;
                txtdimissionCause.Enabled = false;

                pnlDirectorAudit.Visible = true;
                pnlManageAudit.Visible = false;
                pnlGroupAudit.Visible = false;
                pnlHRAudit1.Visible = false;
                pnlFinanceAudit.Visible = false;
                pnlITAudit.Visible = false;
                pnlADAudit.Visible = false;
                pnlHRAudit2.Visible = false;
                pnlHRDirectorAudit.Visible = false;

                gvDetailList.DataSource = list;
                gvDetailList.DataBind();
                labAllNum.Text = labAllNumT.Text = gvDetailList.Rows.Count.ToString();
                // 判断总监和总经理是否同一个人，如果是则显示绩效审批意见
                if (dimissionFormInfo.DirectorId == dimissionFormInfo.ManagerId)
                {
                    pnlPerformance.Visible = true;
                }
            }
            #endregion
            #region 总监总经理级审批显示内容
            else if (dimissionFormInfo.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitManager || dimissionFormInfo.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitDirector)
            {
                txtdimissionDate2.Text = dimissionFormInfo.LastDay == null ? "" : dimissionFormInfo.LastDay.Value.ToString("yyyy-MM-dd");
                pnlDimissionDate1.Visible = false;
                pnlDimissionDate2.Visible = true;
                pnlButtonDirAudit.Visible = false;
                pnlButtonManAudit.Visible = true;
                pnlDirLastDay.Visible = false;
                pnlButtonGroupHRAudit.Visible = false;
                pnlButtonHRAudit.Visible = false;
                pnlButtonFinanceAudit.Visible = false;
                pnlButtonADAudit.Visible = false;
                pnlButtonITAudit.Visible = false;
                pnlButtonHR2Audit.Visible = false;

                txtMobilePhone.Enabled = false;
                txtWorkCity.Enabled = false;
                txtEmail.Enabled = false;
                txtdimissionDate2.Enabled = true;
                txtdimissionCause.Enabled = false;

                pnlDirectorAudit.Visible = false;
                pnlManageAudit.Visible = true;
                pnlGroupAudit.Visible = false;
                pnlHRAudit1.Visible = false;
                pnlFinanceAudit.Visible = false;
                pnlITAudit.Visible = false;
                pnlADAudit.Visible = false;
                pnlHRAudit2.Visible = false;
                pnlPerformance.Visible = true;  // 显示绩效审批意见
                pnlHRDirectorAudit.Visible = false;

                gvDetailView.DataSource = list;
                gvDetailView.DataBind();
                labManNumberB.Text = labManNumberT.Text = gvDetailView.Rows.Count.ToString();

                radIsNormalPerFalse.Checked = !dimissionFormInfo.IsNormalPer; // 是否按实际工作日期结算。
                radIsNormalPerTure.Checked = dimissionFormInfo.IsNormalPer;
                if (radIsNormalPerFalse.Checked)
                    txtSumPerformance.Text = dimissionFormInfo.SumPerformance.ToString();  // 离职时绩效金额数。
            }
            #endregion
            #region 团队行政审批内容
            else if (dimissionFormInfo.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitGroupHR)
            {
                if (dimissionFormInfo.LastDay.Value > DateTime.Now)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                        "alert('离职当天才可以对此离职单进行审批，请确认！');window.location='DimissionAuditList.aspx';", true);
                    return;
                }
                txtdimissionDate2.Text = dimissionFormInfo.LastDay == null ? "" : dimissionFormInfo.LastDay.Value.ToString("yyyy-MM-dd");
                pnlDimissionDate1.Visible = false;
                pnlDimissionDate2.Visible = true;
                pnlButtonDirAudit.Visible = false;
                pnlButtonManAudit.Visible = false;
                pnlDirLastDay.Visible = false;
                pnlButtonGroupHRAudit.Visible = true;
                pnlButtonHRAudit.Visible = false;
                pnlButtonFinanceAudit.Visible = false;
                pnlButtonADAudit.Visible = false;
                pnlButtonITAudit.Visible = false;
                pnlButtonHR2Audit.Visible = false;

                txtMobilePhone.Enabled = false;
                txtWorkCity.Enabled = false;
                txtEmail.Enabled = false;
                txtdimissionDate2.Enabled = true;
                txtdimissionCause.Enabled = false;

                pnlDirectorAudit.Visible = false;
                pnlManageAudit.Visible = true;
                pnlGroupAudit.Visible = true;
                pnlHRAudit1.Visible = false;
                pnlFinanceAudit.Visible = false;
                pnlITAudit.Visible = false;
                pnlADAudit.Visible = false;
                pnlHRAudit2.Visible = false;
                pnlHRDirectorAudit.Visible = false;
                pnlGroupAddIndemnityAudit.Visible = true;



                gvDetailView.DataSource = list;
                gvDetailView.DataBind();
                labManNumberB.Text = labManNumberT.Text = gvDetailView.Rows.Count.ToString();
                SetMonthStat(dimissionFormInfo.UserId, dimissionFormInfo.LastDay.Value);
                SetAnnualLeaveInfo(dimissionFormInfo.UserId, dimissionFormInfo.LastDay.Value, dimissionFormInfo.DimissionId);
            }
            #endregion
            #region 集团人力资源/IT审批显示内容
            else if (dimissionFormInfo.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHRIT)  // 等待集团人力资源
            {
                if (CurrentUserID == manageModel.ITOperatorId) // IT部审批权限
                {
                    txtdimissionDate2.Text = dimissionFormInfo.LastDay == null ? "" : dimissionFormInfo.LastDay.Value.ToString("yyyy-MM-dd");
                    pnlDimissionDate1.Visible = false;
                    pnlDimissionDate2.Visible = true;
                    pnlButtonDirAudit.Visible = false;
                    pnlButtonManAudit.Visible = false;
                    pnlDirLastDay.Visible = false;
                    pnlButtonGroupHRAudit.Visible = false;
                    pnlButtonHRAudit.Visible = false;
                    pnlButtonFinanceAudit.Visible = false;
                    pnlButtonADAudit.Visible = false;
                    pnlButtonITAudit.Visible = true;
                    pnlButtonHR2Audit.Visible = false;

                    txtMobilePhone.Enabled = false;
                    txtWorkCity.Enabled = false;
                    txtEmail.Enabled = false;
                    txtdimissionDate2.Enabled = true;
                    txtdimissionCause.Enabled = false;

                    pnlDirectorAudit.Visible = false;
                    pnlManageAudit.Visible = false;
                    pnlGroupAudit.Visible = false;
                    pnlHRAudit1.Visible = false;
                    pnlFinanceAudit.Visible = false;
                    pnlITAudit.Visible = true;
                    pnlADAudit.Visible = false;
                    pnlHRAudit2.Visible = false;
                    pnlHRDirectorAudit.Visible = false;
                    pnlITAddIndemnityAudit.Visible = true;

                    txtCompanyEmail.Text = userinfo.Email;  //员工邮箱

                    SetITInfo(dimissionFormInfo.DimissionId);
                }
                #region 集团人力资源审批显示内容
                //if (ESP.Framework.BusinessLogic.PermissionManager.HasModulePermission("003", this.ModuleInfo.ModuleID, this.UserID))
                else
                {
                    txtdimissionDate2.Text = dimissionFormInfo.LastDay == null ? "" : dimissionFormInfo.LastDay.Value.ToString("yyyy-MM-dd");
                    pnlDimissionDate1.Visible = false;
                    pnlDimissionDate2.Visible = true;
                    pnlButtonDirAudit.Visible = false;
                    pnlButtonManAudit.Visible = false;
                    pnlDirLastDay.Visible = false;
                    // 判断是否是上海广州用户。
                    pnlButtonGroupHRAudit.Visible = false;
                    pnlButtonHRAudit.Visible = true;
                    pnlButtonFinanceAudit.Visible = false;
                    pnlButtonADAudit.Visible = false;
                    pnlButtonITAudit.Visible = false;
                    pnlButtonHR2Audit.Visible = false;

                    txtMobilePhone.Enabled = false;
                    txtWorkCity.Enabled = false;
                    txtEmail.Enabled = false;
                    txtdimissionDate2.Enabled = true;
                    txtdimissionCause.Enabled = false;

                    pnlDirectorAudit.Visible = false;
                    pnlManageAudit.Visible = true;
                    pnlGroupAudit.Visible = true;
                    pnlHRAudit1.Visible = true;
                    pnlFinanceAudit.Visible = false;
                    pnlITAudit.Visible = true;
                    pnlADAudit.Visible = false;
                    pnlHRAudit2.Visible = false;
                    pnlHRDirectorAudit.Visible = true;
                    pnlHRDirectorAuditAmount.Visible = true;
                    pnlITAddIndemnityAudit.Visible = false;
                    gvDetailView.DataSource = list;
                    gvDetailView.DataBind();
                    labManNumberB.Text = labManNumberT.Text = gvDetailView.Rows.Count.ToString();
                    SetMonthStat(dimissionFormInfo.UserId, dimissionFormInfo.LastDay.Value);

                    txtCompanyEmail.Text = userinfo.Email;  //员工邮箱

                    SetITInfo(dimissionFormInfo.DimissionId);

                    SetAnnualLeaveInfo(dimissionFormInfo.UserId, dimissionFormInfo.LastDay.Value, dimissionFormInfo.DimissionId);
                    SetDimissionIndemnity(dimissionFormInfo.DimissionId, dimissionFormInfo.TotalIndemnityAmount);
                    SetInsDateTime(dimissionFormInfo.DimissionId);
                }
                #endregion
            }
            #endregion
            #region 集团人力资源总监审批
            else if (dimissionFormInfo.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHRDirector)  // 等待集团人力总监审批
            {
                txtdimissionDate2.Text = dimissionFormInfo.LastDay == null ? "" : dimissionFormInfo.LastDay.Value.ToString("yyyy-MM-dd");
                pnlDimissionDate1.Visible = false;
                pnlDimissionDate2.Visible = true;
                pnlButtonDirAudit.Visible = false;
                pnlButtonManAudit.Visible = false;
                pnlDirLastDay.Visible = false;
                pnlButtonGroupHRAudit.Visible = false;
                pnlButtonHRAudit.Visible = false;
                pnlButtonFinanceAudit.Visible = false;
                pnlButtonADAudit.Visible = false;
                pnlButtonITAudit.Visible = false;
                pnlButtonHR2Audit.Visible = false;
                pnlButtonHRDirAudit.Visible = true;

                txtMobilePhone.Enabled = false;
                txtWorkCity.Enabled = false;
                txtEmail.Enabled = false;
                txtdimissionDate2.Enabled = true;
                txtdimissionCause.Enabled = false;

                pnlDirectorAudit.Visible = false;
                pnlManageAudit.Visible = false;
                pnlGroupAudit.Visible = false;
                pnlHRAudit1.Visible = false;
                pnlFinanceAudit.Visible = false;
                pnlITAudit.Visible = false;
                pnlADAudit.Visible = false;
                pnlHRAudit2.Visible = false;
                pnlHRDirectorAudit.Visible = true;
                pnlHRDirectorAuditAmount.Visible = true;

                SetDimissionIndemnity(dimissionFormInfo.DimissionId, dimissionFormInfo.TotalIndemnityAmount);
            }
            #endregion
            #region 财务部审批显示内容
            else if (dimissionFormInfo.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitFinance) // 待财务审批
            {
                txtdimissionDate2.Text = dimissionFormInfo.LastDay == null ? "" : dimissionFormInfo.LastDay.Value.ToString("yyyy-MM-dd");
                pnlDimissionDate1.Visible = false;
                pnlDimissionDate2.Visible = true;
                pnlButtonDirAudit.Visible = false;
                pnlButtonManAudit.Visible = false;
                pnlDirLastDay.Visible = false;
                pnlButtonGroupHRAudit.Visible = false;
                pnlButtonHRAudit.Visible = false;
                pnlButtonFinanceAudit.Visible = true;
                pnlButtonADAudit.Visible = false;
                pnlButtonITAudit.Visible = false;
                pnlButtonHR2Audit.Visible = false;

                string financeLevel1Users = "," + ESP.Finance.BusinessLogic.BranchManager.GetDimissionAuditors(departmentInfo.DepartmentID) + ",";
                if (userback != 0 && financeLevel1Users.IndexOf("," + userback + ",") >= 0)
                    financeLevel1Users += UserID + ",";

                SetFinanceInfo(dimissionFormInfo.DimissionId);

                if (dimissionFormInfo.FinanceAuditStatus == 1) // 财务一级审批
                {
                    if (financeLevel1Users.IndexOf("," + UserID.ToString() + ",") != -1)
                    {
                        pnlFinanceAuditLevel1.Visible = true;
                        //pnlFinanceAuditLevel2.Visible = false;
                        pnlFinanceAuditLevel3.Visible = false;
                        pnlFinanceAuditLevel4.Visible = false;
                        pnlFinanceAuditLevel5.Visible = false;
                        pnlFinanceAuditLevel6.Visible = false;
                        int firstfinanceid = UserID;

                        IList<ESP.Finance.Entity.BranchInfo> branchList = ESP.Finance.BusinessLogic.BranchManager.GetBranchByDimission(firstfinanceid);
                        IList<ESP.Finance.Entity.BranchInfo> backBranchList = null;
                        if (userback != 0)
                        {
                            backBranchList = ESP.Finance.BusinessLogic.BranchManager.GetBranchByDimission(userback);
                        }
                        txtLoanX.Enabled = false;
                        txtLoanS.Enabled = false;
                        txtLoanK.Enabled = false;
                        txtLoanD.Enabled = false;
                        txtLoanC.Enabled = false;
                        txtLoanZ.Enabled = false;

                        StringBuilder branchStr = new StringBuilder();

                        if (branchList != null && branchList.Count > 0)
                        {
                            foreach (ESP.Finance.Entity.BranchInfo branch in branchList)
                            {
                                switch (branch.BranchCode)
                                {
                                    case "X":
                                        txtLoanX.Enabled = true;
                                        break;
                                    case "S":
                                        txtLoanS.Enabled = true;
                                        break;
                                    case "K":
                                        txtLoanK.Enabled = true;
                                        break;
                                    case "D":
                                        txtLoanD.Enabled = true;
                                        break;
                                    case "Z":
                                        txtLoanZ.Enabled = true;
                                        break;
                                    case "C":
                                        txtLoanC.Enabled = true;
                                        break;
                                    default:
                                        break;
                                }
                                branchStr.Append(branch.BranchCode + "、");
                            }
                        }

                        if (backBranchList != null && backBranchList.Count > 0)
                        {
                            foreach (ESP.Finance.Entity.BranchInfo branch in backBranchList)
                            {
                                switch (branch.BranchCode)
                                {
                                    case "X":
                                        txtLoanX.Enabled = true;
                                        break;
                                    case "S":
                                        txtLoanS.Enabled = true;
                                        break;
                                    case "K":
                                        txtLoanK.Enabled = true;
                                        break;
                                    case "D":
                                        txtLoanD.Enabled = true;
                                        break;
                                    case "Z":
                                        txtLoanZ.Enabled = true;
                                        break;
                                    case "C":
                                        txtLoanC.Enabled = true;
                                        break;
                                    default:
                                        break;
                                }
                                branchStr.Append(branch.BranchCode + "、");
                            }
                        }
                        labBranchInfo.Text = "您当前所负责的分公司有：" + branchStr.ToString().TrimEnd('、');

                        pnlGroupAudit.Visible = false;
                        pnlHRAudit1.Visible = false;
                        pnlITAudit.Visible = false;
                    }
                }
                else if (dimissionFormInfo.FinanceAuditStatus == 2) // 财务二级审批
                {
                    pnlFinanceAuditLevel1.Visible = false;
                    //pnlFinanceAuditLevel2.Visible = false;
                    pnlFinanceAuditLevel3.Visible = true;
                    pnlFinanceAuditLevel4.Visible = false;
                    pnlFinanceAuditLevel5.Visible = false;
                    pnlFinanceAuditLevel6.Visible = false;

                    pnlGroupAudit.Visible = false;
                    pnlHRAudit1.Visible = false;
                    pnlITAudit.Visible = false;
                }
                else if (dimissionFormInfo.FinanceAuditStatus == 3) // 财务三级审批（工资结算）
                {
                    pnlFinanceAuditLevel1.Visible = false;
                    //pnlFinanceAuditLevel2.Visible = false;
                    pnlFinanceAuditLevel3.Visible = false;
                    pnlFinanceAuditLevel4.Visible = true;
                    pnlFinanceAuditLevel5.Visible = false;
                    pnlFinanceAuditLevel6.Visible = false;

                    IList<ESP.Finance.Entity.BranchInfo> branchlist = ESP.Finance.BusinessLogic.BranchManager.GetAllList();
                    ESP.Finance.Entity.BranchInfo branchDefault = new ESP.Finance.Entity.BranchInfo();
                    branchDefault.BranchID = 0;
                    branchDefault.BranchCode = "请选择...";
                    branchlist.Insert(0, branchDefault);
                    drpBranchList.DataSource = branchlist;
                    drpBranchList.DataTextField = "BranchCode";
                    drpBranchList.DataValueField = "BranchId";
                    drpBranchList.DataBind();
                    drpBranchList.SelectedIndex = 0;

                    radSalary1.Enabled = true;
                    radSalary2.Enabled = true;
                    drpBranchList.SelectedIndex = 0;

                    pnlGroupAudit.Visible = true;
                    pnlHRAudit1.Visible = true;
                    pnlITAudit.Visible = true;
                    if (dimissionFormInfo.IsNormalPer)
                        radIsNormalPerTure.Checked = true;
                    else
                    {
                        radIsNormalPerFalse.Checked = true;
                        txtSumPerformance.Text = dimissionFormInfo.SumPerformance.ToString();
                    }
                    radIsNormalPerTure.Enabled = false;
                    radIsNormalPerFalse.Enabled = false;
                    txtSumPerformance.Enabled = false;
                    pnlPerformance.Visible = true;
                    // 赔偿详细信息
                    pnlHRDirectorAudit.Visible = true;
                    pnlFinanceAuditAmount.Visible = true;
                    SetDimissionIndemnity(dimissionFormInfo.DimissionId, dimissionFormInfo.TotalIndemnityAmount);

                    SetMobileInfo(dimissionFormInfo.UserId);
                }
                else if (dimissionFormInfo.FinanceAuditStatus == 4) // 财务总监审批
                {
                    pnlFinanceAuditLevel1.Visible = true;
                    pnlLoan.Visible = false;
                    //pnlFinanceAuditLevel2.Visible = true;
                    //pnlAccountsPayable.Visible = false;
                    //pnlFinanceAuditLevel3.Visible = true;
                    pnlFinanceAuditLevel4.Visible = true;
                    pnlFinanceAuditLevel5.Visible = true;
                    //pnlFinanceAuditLevel6.Visible = true;


                    ESP.HumanResource.Entity.DimissionFinanceDetailsInfo financeDetailInfo =
                        ESP.HumanResource.BusinessLogic.DimissionFinanceDetailsManager.GetFinanceDetailInfo(dimissionid);
                    if (financeDetailInfo != null)
                    {
                        if (string.IsNullOrEmpty(financeDetailInfo.BusinessCardAuditIds))
                        {
                            pnlFinanceAuditLevel3.Visible = false;
                        }
                        if (financeDetailInfo.SalaryTellerId != 0)
                        {
                            pnlFinanceAuditLevel6.Visible = true;

                            IList<ESP.Finance.Entity.BranchInfo> branchlist = ESP.Finance.BusinessLogic.BranchManager.GetAllList();
                            ESP.Finance.Entity.BranchInfo branchDefault = new ESP.Finance.Entity.BranchInfo();
                            branchDefault.BranchID = 0;
                            branchDefault.BranchCode = "请选择...";
                            branchlist.Insert(0, branchDefault);
                            drpBranchList.DataSource = branchlist;
                            drpBranchList.DataTextField = "BranchCode";
                            drpBranchList.DataValueField = "BranchId";
                            drpBranchList.DataBind();
                            drpBranchList.SelectedIndex = 0;

                            drpBranchList.SelectedValue = financeDetailInfo.SalaryBranch.ToString();

                            radSalary1.Enabled = false;
                            radSalary2.Enabled = false;
                            radSalary2.Checked = true;
                        }
                        else
                        {
                            pnlFinanceAuditLevel6.Visible = false;
                            radSalary1.Enabled = false;
                            radSalary2.Enabled = false;
                            radSalary1.Checked = true;
                        }
                    }
                    txtSalary.Enabled = false;
                    txtSalary2.Enabled = false;
                    txtBusinessCard.Enabled = false;

                    pnlGroupAudit.Visible = true;
                    pnlHRAudit1.Visible = true;
                    pnlITAudit.Visible = true;
                    if (dimissionFormInfo.IsNormalPer)
                        radIsNormalPerTure.Checked = true;
                    else
                    {
                        radIsNormalPerFalse.Checked = true;
                        txtSumPerformance.Text = dimissionFormInfo.SumPerformance.ToString();
                    }
                    radIsNormalPerTure.Enabled = false;
                    radIsNormalPerFalse.Enabled = false;
                    txtSumPerformance.Enabled = false;
                    pnlPerformance.Visible = true;

                    // 赔偿详细信息
                    pnlHRDirectorAudit.Visible = true;
                    pnlFinanceAuditAmount.Visible = true;
                    SetDimissionIndemnity(dimissionFormInfo.DimissionId, dimissionFormInfo.TotalIndemnityAmount);

                    SetMobileInfo(dimissionFormInfo.UserId);
                }
                else if (dimissionFormInfo.FinanceAuditStatus == 5)  // 工资结算后出纳负责收款
                {
                    ESP.HumanResource.Entity.DimissionFinanceDetailsInfo financeDetailInfo =
                        ESP.HumanResource.BusinessLogic.DimissionFinanceDetailsManager.GetFinanceDetailInfo(dimissionid);

                    pnlFinanceAuditLevel1.Visible = false;
                    //pnlFinanceAuditLevel2.Visible = false;
                    pnlFinanceAuditLevel3.Visible = false;
                    pnlFinanceAuditLevel4.Visible = true;
                    pnlFinanceAuditLevel5.Visible = false;
                    pnlFinanceAuditLevel6.Visible = true;

                    IList<ESP.Finance.Entity.BranchInfo> branchlist = ESP.Finance.BusinessLogic.BranchManager.GetAllList();
                    ESP.Finance.Entity.BranchInfo branchDefault = new ESP.Finance.Entity.BranchInfo();
                    branchDefault.BranchID = 0;
                    branchDefault.BranchCode = "请选择...";
                    branchlist.Insert(0, branchDefault);
                    drpBranchList.DataSource = branchlist;
                    drpBranchList.DataTextField = "BranchCode";
                    drpBranchList.DataValueField = "BranchId";
                    drpBranchList.DataBind();
                    drpBranchList.SelectedIndex = 0;

                    drpBranchList.SelectedValue = financeDetailInfo.SalaryBranch.ToString();

                    radSalary1.Enabled = false;
                    radSalary2.Enabled = false;
                    radSalary2.Checked = true;

                    pnlGroupAudit.Visible = false;
                    pnlHRAudit1.Visible = false;
                    pnlITAudit.Visible = false;
                }

                // 离职基本信息
                txtMobilePhone.Enabled = false;
                txtWorkCity.Enabled = false;
                txtEmail.Enabled = false;
                txtdimissionDate2.Enabled = false;
                txtdimissionCause.Enabled = false;

                // IT部审批信息
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
                SetITInfo(dimissionFormInfo.DimissionId);

                txtHRRemark.Enabled = false;

                pnlDirectorAudit.Visible = false;
                pnlManageAudit.Visible = true;
                pnlFinanceAudit.Visible = true;
                pnlADAudit.Visible = false;
                pnlHRAudit2.Visible = false;

                gvDetailView.DataSource = list;
                gvDetailView.DataBind();
                labManNumberB.Text = labManNumberT.Text = gvDetailView.Rows.Count.ToString();
                SetMonthStat(dimissionFormInfo.UserId, dimissionFormInfo.LastDay.Value);
                SetAnnualLeaveInfo(dimissionFormInfo.UserId, dimissionFormInfo.LastDay.Value, dimissionFormInfo.DimissionId);
                SetInsDateTime(dimissionFormInfo.DimissionId);
            }
            #endregion
            #region 集团行政审批显示内容
            else if (dimissionFormInfo.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitAdministration)  // 待集团行政审批
            {
                txtdimissionDate2.Text = dimissionFormInfo.LastDay == null ? "" : dimissionFormInfo.LastDay.Value.ToString("yyyy-MM-dd");
                pnlDimissionDate1.Visible = false;
                pnlDimissionDate2.Visible = true;
                pnlButtonDirAudit.Visible = false;
                pnlButtonManAudit.Visible = false;
                pnlDirLastDay.Visible = false;
                pnlButtonGroupHRAudit.Visible = false;
                pnlButtonHRAudit.Visible = false;
                pnlButtonFinanceAudit.Visible = false;
                pnlButtonADAudit.Visible = true;
                pnlButtonITAudit.Visible = false;
                pnlButtonHR2Audit.Visible = false;

                txtMobilePhone.Enabled = false;
                txtWorkCity.Enabled = false;
                txtEmail.Enabled = false;
                txtdimissionDate2.Enabled = true;
                txtdimissionCause.Enabled = false;

                pnlDirectorAudit.Visible = false;
                pnlManageAudit.Visible = false;
                pnlGroupAudit.Visible = false;
                pnlHRAudit1.Visible = false;
                pnlFinanceAudit.Visible = false;
                pnlITAudit.Visible = false;
                pnlADAudit.Visible = true;
                pnlHRAudit2.Visible = false;
                pnlHRDirectorAudit.Visible = false;


                ESP.Administrative.BusinessLogic.UserAttBasicInfoManager userAttBasicManager = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager();
                ESP.Administrative.Entity.UserAttBasicInfo userAttBasicInfo = userAttBasicManager.GetEnableCardByUserid(dimissionFormInfo.UserId);
                if (userAttBasicInfo != null)
                    txtDoorCard.Text = userAttBasicInfo.CardNo;

                SetADInfo(dimissionFormInfo.DimissionId);
            }
            #endregion
            #region 集团人力审批显示内容
            else if (dimissionFormInfo.Status == (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHR2)  // 集团人力资源审批
            {
                txtdimissionDate2.Text = dimissionFormInfo.LastDay == null ? "" : dimissionFormInfo.LastDay.Value.ToString("yyyy-MM-dd");
                pnlDimissionDate1.Visible = false;
                pnlDimissionDate2.Visible = true;
                pnlButtonDirAudit.Visible = false;
                pnlButtonManAudit.Visible = false;
                pnlDirLastDay.Visible = false;
                pnlButtonGroupHRAudit.Visible = false;
                pnlButtonHRAudit.Visible = false;
                pnlButtonFinanceAudit.Visible = false;

                pnlButtonADAudit.Visible = false;

                pnlButtonITAudit.Visible = false;
                pnlButtonHR2Audit.Visible = true;

                txtMobilePhone.Enabled = false;
                txtWorkCity.Enabled = false;
                txtEmail.Enabled = false;
                txtdimissionDate2.Enabled = true;
                txtdimissionCause.Enabled = false;

                pnlDirectorAudit.Visible = false;
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

                // 初始化分公司列表
                IList<ESP.Finance.Entity.BranchInfo> branchlist = ESP.Finance.BusinessLogic.BranchManager.GetAllList();
                ESP.Finance.Entity.BranchInfo branchDefault = new ESP.Finance.Entity.BranchInfo();
                branchDefault.BranchID = 0;
                branchDefault.BranchCode = "请选择...";
                branchlist.Insert(0, branchDefault);
                drpBranchList2.DataSource = branchlist;
                drpBranchList2.DataTextField = "BranchCode";
                drpBranchList2.DataValueField = "BranchId";
                drpBranchList2.DataBind();
                drpBranchList2.SelectedIndex = 0;

                drpBranchList.DataSource = branchlist;
                drpBranchList.DataTextField = "BranchCode";
                drpBranchList.DataValueField = "BranchId";
                drpBranchList.DataBind();
                drpBranchList.SelectedIndex = 0;
                ESP.HumanResource.Entity.DimissionFinanceDetailsInfo financeDetailInfo = ESP.HumanResource.BusinessLogic.DimissionFinanceDetailsManager.GetFinanceDetailInfo(dimissionid);
                drpBranchList.SelectedValue = financeDetailInfo.SalaryBranch.ToString();
                drpBranchList.Enabled = false;

                txtBusinessCard.Enabled = false;
                txtOther.Enabled = false;
                pnlLoan.Visible = false;
                //pnlAccountsPayable.Visible = false;
                txtSalary2.Enabled = false;
                txtSalary.Enabled = false;

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
                //txtLibraryManage.Enabled = false;

                txtHRRemark.Enabled = false;

                // 赔偿详细信息
                pnlHRDirectorAudit.Visible = true;
                pnlFinanceAuditAmount.Visible = true;
                SetDimissionIndemnity(dimissionFormInfo.DimissionId, dimissionFormInfo.TotalIndemnityAmount);

                SetMobileInfo(dimissionFormInfo.UserId);
            }
            #endregion
            #region 其他状态审批显示内容
            else
            {
                txtdimissionDate2.Text = dimissionFormInfo.LastDay == null ? "" : dimissionFormInfo.LastDay.Value.ToString("yyyy-MM-dd");
                pnlDimissionDate1.Visible = false;
                pnlDimissionDate2.Visible = true;
                pnlButtonDirAudit.Visible = false;
                pnlButtonManAudit.Visible = true;
                pnlDirLastDay.Visible = false;
                pnlButtonGroupHRAudit.Visible = false;
                pnlButtonHRAudit.Visible = false;
                pnlButtonFinanceAudit.Visible = false;
                pnlButtonADAudit.Visible = false;
                pnlButtonITAudit.Visible = false;
                pnlButtonHR2Audit.Visible = false;

                pnlDirectorAudit.Visible = false;
                pnlManageAudit.Visible = true;
                pnlGroupAudit.Visible = true;
                gvDetailView.DataSource = list;
                gvDetailView.DataBind();
                labManNumberB.Text = labManNumberT.Text = gvDetailView.Rows.Count.ToString();
            }
            #endregion
        }
        #endregion

        // 报销单据信息
        DataSet oopDataSet = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.GetDimissionOOP(dimissionFormInfo.UserId);
        if (oopDataSet != null && oopDataSet.Tables.Count > 0 && oopDataSet.Tables[0].Rows.Count > 0)
        {
            gvOOP1.DataSource = oopDataSet;
            gvOOP1.DataBind();
            labOOP1B.Text = labOOP1T.Text = gvOOP1.Rows.Count.ToString();
        }
        else
        {
            pnlOOP.Visible = false;
        }

        if (gvDetailList.Rows.Count > 0)
        {
            pnlBottom.Visible = true;
            pnlTop.Visible = true;
        }
        else
        {
            pnlBottom.Visible = false;
            pnlTop.Visible = false;
        }

        // 审批日志信息
        string strAuditLog = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetStrAuditLogInfos(dimissionFormInfo.DimissionId,
            (int)ESP.HumanResource.Common.HRFormType.DimissionForm);
        labAuditLog.Text = strAuditLog;
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
                if (dimissionDetailInfo.FormType == "项目号")
                {
                    IsProject = true;
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
    /// 从当前页收集选中项的情况
    /// </summary>
    protected void CollectSelected()
    {
        for (int i = 0; i < this.gvDetailList.Rows.Count; i++)
        {
            int formId = 0;
            if (!int.TryParse(gvDetailList.DataKeys[i].Values["FormId"].ToString(), out formId))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                    "alert('系统出现错误，请与系统管理员联系');", true);
                ESP.Logging.Logger.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + gvDetailList.DataKeys[i].Value + "转换成int型数据时出现错误(登录人：" + UserInfo.Username + ")",
                        "人事管理", ESP.Logging.LogLevel.Information);
                return;
            }
            string formType = gvDetailList.DataKeys[i].Values["FormType"].ToString();
            if (DicDimission.ContainsKey(formId + "-" + formType))
            {
                HiddenField hid = this.gvDetailList.Rows[i].FindControl("hidReceiverName") as HiddenField;
                string val = hid.Value;
                int receiverId = int.Parse(val);
                ESP.HumanResource.Entity.DimissionDetailsInfo dimissionDetailInfo = DicDimission[formId + "-" + formType];
                dimissionDetailInfo.ReceiverId = receiverId;
                dimissionDetailInfo.ReceiverName = ESP.Framework.BusinessLogic.UserManager.Get(receiverId).FullNameCN;
                IList<ESP.Framework.Entity.EmployeePositionInfo> empPositionList = ESP.Framework.BusinessLogic.DepartmentPositionManager.GetEmployeePositions(receiverId);
                if (empPositionList != null && empPositionList.Count > 0)
                {
                    dimissionDetailInfo.ReceiverDepartmentId = empPositionList[0].DepartmentID;
                    dimissionDetailInfo.ReceiverDepartmentName = empPositionList[0].DepartmentName;
                }
            }
        }
    }

    #region 预审审批
    /// <summary>
    /// 审批通过
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPass_Click(object sender, EventArgs e)
    {
        int dimissionFormId = 0;
        if (!int.TryParse(hidDimissionFormID.Value, out dimissionFormId))
            dimissionFormId = 0;

        CollectSelected();

        // 判断该离职单是否是处于当前登录人审批
        bool b = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.CheckDimissionData(UserID, dimissionFormId);
        if (!b)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('你没有权限操作此离职单！');", true);
            return;
        }

        // 离职单信息
        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionFormId);

        ESP.HumanResource.Entity.HRAuditLogInfo hrauditLogInfo = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(UserID,
            dimissionFormInfo.DimissionId, (int)ESP.HumanResource.Common.HRFormType.DimissionForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);
        hrauditLogInfo.AuditDate = DateTime.Now;
        hrauditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Audited;
        hrauditLogInfo.Requesition = txtRes.Text.Trim();

        dimissionFormInfo.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitReceiver;
        if (!string.IsNullOrEmpty(txtdimissionDate.Text))
            dimissionFormInfo.LastDay = DateTime.Parse(txtdimissionDate.Text);
        //dimissionFormInfo.DirectorId = UserID;
        //dimissionFormInfo.DirectorName = UserInfo.FullNameCN;
        if (pnlPerformance.Visible == true)
        {
            dimissionFormInfo.IsNormalPer = radIsNormalPerFalse.Checked ? false : true;
            if (radIsNormalPerFalse.Checked)
                dimissionFormInfo.SumPerformance = decimal.Parse(txtSumPerformance.Text);
        }

        bool bl = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.SavaDimissionDetailsInfo(dimissionFormInfo, DicDimission, hrauditLogInfo);
        if (!bl)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('系统出现错误，请与系统管理员联系。');", true);
            return;
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('离职申请单审批通过。');window.location='DimissionAuditList.aspx';", true);

            List<int> receiverUserId = new List<int>();
            List<MailAddress> mailAddressList = new List<MailAddress>();
            int mailType = 1;
            // 保存交接单据信息
            if (DicDimission != null && DicDimission.Count > 0)
            {
                foreach (KeyValuePair<string, ESP.HumanResource.Entity.DimissionDetailsInfo> pair in DicDimission)
                {
                    if (!receiverUserId.Contains(pair.Value.ReceiverId))
                    {
                        receiverUserId.Add(pair.Value.ReceiverId);

                        ESP.Framework.Entity.UserInfo userInfo = ESP.Framework.BusinessLogic.UserManager.Get(pair.Value.ReceiverId);
                        if (userInfo != null && !string.IsNullOrEmpty(userInfo.Email) && userInfo.Status == 1)
                            mailAddressList.Add(new MailAddress(userInfo.Email));
                    }
                }
                mailType = 1;
            }
            else
            {
                ESP.Framework.Entity.OperationAuditManageInfo manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(dimissionFormInfo.UserId);
                if (manageModel == null)
                    manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(dimissionFormInfo.DepartmentId);

                if (dimissionFormInfo.PreAuditorId == dimissionFormInfo.ManagerId)
                {
                    ESP.Administrative.BusinessLogic.UserAttBasicInfoManager userAtt = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager();
                    ESP.Framework.Entity.DepartmentInfo companyDep = userAtt.GetRootDepartmentID(dimissionFormInfo.UserId);
                    List<ESP.HumanResource.Entity.UsersInfo> list = UsersManager.GetUserList(companyDep.DepartmentID, ESP.HumanResource.Common.Status.DimissionSendMail);
                    if (list != null && list.Count > 0)
                    {
                        foreach (ESP.HumanResource.Entity.UsersInfo userModel in list)
                        {
                            mailAddressList.Add(new MailAddress(userModel.Email));
                        }
                    }

                    ESP.Framework.Entity.UserInfo userInfo = ESP.Framework.BusinessLogic.UserManager.Get(manageModel.HRId);
                    if (userInfo != null && !string.IsNullOrEmpty(userInfo.Email) && userInfo.Status == 1)
                        mailAddressList.Add(new MailAddress(userInfo.Email));

                    mailType = 3;
                }
                else if (dimissionFormInfo.PreAuditorId == dimissionFormInfo.DirectorId)
                {
                    ESP.Framework.Entity.UserInfo userInfo = ESP.Framework.BusinessLogic.UserManager.Get(manageModel.DimissionManagerId);
                    if (userInfo != null && !string.IsNullOrEmpty(userInfo.Email) && userInfo.Status == 1)
                        mailAddressList.Add(new MailAddress(userInfo.Email));
                    mailType = 2;
                }
                else
                {
                    ESP.Framework.Entity.UserInfo userInfo = ESP.Framework.BusinessLogic.UserManager.Get(manageModel.DimissionDirectorid);
                    if (userInfo != null && !string.IsNullOrEmpty(userInfo.Email) && userInfo.Status == 1)
                        mailAddressList.Add(new MailAddress(userInfo.Email));
                    mailType = 1;
                }
            }
            if (mailAddressList != null && mailAddressList.Count > 0)
            {
                string url = "http://" + Request.Url.Authority + "/HR/Print/NewDimissionMail.aspx?dimissionId=" + dimissionFormInfo.DimissionId + "&type=" + mailType;
                string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                try
                {
                    ESP.Mail.MailManager.Send("离职管理", body, true, mailAddressList.ToArray());
                }
                catch
                { }
            }

            ESP.Framework.Entity.UserInfo dimissionUserInfo = ESP.Framework.BusinessLogic.UserManager.Get(dimissionFormInfo.UserId);
            try
            {
                if (mailType == 1 || mailType == 2)
                {
                    ESP.Mail.MailManager.Send("离职管理--业务审批中", "" + dimissionUserInfo.FullNameCN + "您好，您的离职申请业务审批通过，等待下一级审批。", false, new MailAddress[] { new MailAddress(dimissionUserInfo.Email) });
                }
                else
                {
                    ESP.Mail.MailManager.Send("离职管理--业务审批通过", "" + dimissionUserInfo.FullNameCN + "您好，您的离职申请总经理已审批通过。请提前与财务人员协调办理商务卡结算、现金冲销等事宜。并于last day当天登录内网系统办理离职手续。", false, new MailAddress[] { new MailAddress(dimissionUserInfo.Email) });
                }
            }
            catch
            { }

            return;
        }
    }

    /// <summary>
    /// 审批驳回
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOverrule_Click(object sender, EventArgs e)
    {
        int dimissionFormId = 0;
        if (!int.TryParse(hidDimissionFormID.Value, out dimissionFormId))
            dimissionFormId = 0;

        // 判断该离职单是否是处于当前登录人审批
        bool b = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.CheckDimissionData(UserID, dimissionFormId);
        if (!b)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('你没有权限操作此离职单！');", true);
            return;
        }

        // 离职单信息
        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionFormId);
        dimissionFormInfo.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.Overrule;
        if (!string.IsNullOrEmpty(txtdimissionDate.Text))
            dimissionFormInfo.LastDay = DateTime.Parse(txtdimissionDate.Text);

        // 审批日志
        ESP.HumanResource.Entity.HRAuditLogInfo hrauditLogInfo = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(UserID,
            dimissionFormInfo.DimissionId, (int)ESP.HumanResource.Common.HRFormType.DimissionForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);
        hrauditLogInfo.AuditDate = DateTime.Now;
        hrauditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Overrule;
        hrauditLogInfo.Requesition = txtRes.Text.Trim();

        bool bl = ESP.HumanResource.BusinessLogic.DimissionFormManager.DimissionOverrule(dimissionFormInfo, hrauditLogInfo);
        if (!bl)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('系统出现错误，请与系统管理员联系。');", true);
            return;
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('离职申请单审批驳回成功。');window.location='DimissionAuditList.aspx';", true);
            try
            {
                ESP.Mail.MailManager.Send("离职管理--总监审批驳回", "您好，您的离职申请总监已审批驳回，请登录系统进行修改。", false, new MailAddress[] { new MailAddress(ESP.Framework.BusinessLogic.UserManager.Get(dimissionFormInfo.UserId).Email) });
            }
            catch
            { }
            return;
        }
    }
    #endregion

    #region 总监总经理级审批
    /// <summary>
    /// 审批通过
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnManAuditPass_Click(object sender, EventArgs e)
    {
        int dimissionFormId = 0;

        List<MailAddress> mailAddressList = new List<MailAddress>();

        if (!int.TryParse(hidDimissionFormID.Value, out dimissionFormId))
            dimissionFormId = 0;

        // 判断该离职单是否是处于当前登录人审批
        bool b = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.CheckDimissionData(UserID, dimissionFormId);
        if (!b)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('你没有权限操作此离职单！');", true);
            return;
        }

        // 离职单信息
        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionFormId);

        ESP.HumanResource.Entity.HRAuditLogInfo hrauditLogInfo = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(UserID,
            dimissionFormInfo.DimissionId, (int)ESP.HumanResource.Common.HRFormType.DimissionForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);
        hrauditLogInfo.AuditDate = DateTime.Now;
        hrauditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Audited;
        hrauditLogInfo.Requesition = txtRes.Text.Trim();

        // 获得离职员工所在部门的审批信息
        ESP.Framework.Entity.OperationAuditManageInfo manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(dimissionFormInfo.UserId);
        if (manageModel == null)
            manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(dimissionFormInfo.DepartmentId);

        List<ESP.HumanResource.Entity.HRAuditLogInfo> newHRAuditLogList = new List<ESP.HumanResource.Entity.HRAuditLogInfo>();

        ESP.Framework.Entity.DepartmentInfo departmentInfo =
    new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager().GetRootDepartmentID(dimissionFormInfo.UserId);


        if (hrauditLogInfo.AuditorId == dimissionFormInfo.DirectorId && dimissionFormInfo.DirectorId != dimissionFormInfo.ManagerId)
        {
            dimissionFormInfo.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitManager;
            ESP.Framework.Entity.UserInfo userModel = ESP.Framework.BusinessLogic.UserManager.Get(dimissionFormInfo.ManagerId);

            ESP.HumanResource.Entity.HRAuditLogInfo newHRAuditLogInfo = new ESP.HumanResource.Entity.HRAuditLogInfo();
            newHRAuditLogInfo.AuditorId = userModel.UserID;
            newHRAuditLogInfo.AuditorName = userModel.FullNameCN;
            newHRAuditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
            newHRAuditLogInfo.FormId = dimissionFormInfo.DimissionId;
            newHRAuditLogInfo.FormType = (int)ESP.HumanResource.Common.HRFormType.DimissionForm;
            newHRAuditLogInfo.AuditLevel = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitManager;

            mailAddressList.Add(new MailAddress(userModel.Email));

            newHRAuditLogList.Add(newHRAuditLogInfo);


        }
        else
        {
            dimissionFormInfo.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitFinance;
            dimissionFormInfo.FinanceAuditStatus = 1; // 一级审批

            string financeLevel1Users = "," + ESP.Finance.BusinessLogic.BranchManager.GetDimissionAuditors(departmentInfo.DepartmentID) + ",";
            string[] financeLeve1UserArr = financeLevel1Users.Split(',');
            foreach (string str in financeLeve1UserArr)
            {
                int level1UserId = 0;
                if (int.TryParse(str, out level1UserId))
                {
                    ESP.Framework.Entity.UserInfo userModel = ESP.Framework.BusinessLogic.UserManager.Get(level1UserId);
                    if (userModel != null)
                    {
                        ESP.HumanResource.Entity.HRAuditLogInfo newHRAuditLogInfo = new ESP.HumanResource.Entity.HRAuditLogInfo();
                        newHRAuditLogInfo.AuditorId = userModel.UserID;
                        newHRAuditLogInfo.AuditorName = userModel.FullNameCN;
                        newHRAuditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
                        newHRAuditLogInfo.FormId = dimissionFormInfo.DimissionId;
                        newHRAuditLogInfo.FormType = (int)ESP.HumanResource.Common.HRFormType.DimissionForm;
                        newHRAuditLogInfo.AuditLevel = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitFinance;

                        mailAddressList.Add(new MailAddress(userModel.Email));

                        newHRAuditLogList.Add(newHRAuditLogInfo);
                    }
                }
            }



        }

        dimissionFormInfo.IsNormalPer = radIsNormalPerFalse.Checked ? false : true;  // 是否按实际工作日期结算。
        if (radIsNormalPerFalse.Checked)
            dimissionFormInfo.SumPerformance = decimal.Parse(txtSumPerformance.Text);  // 离职时绩效金额数。
        if (!string.IsNullOrEmpty(txtdimissionDate2.Text))
            dimissionFormInfo.LastDay = DateTime.Parse(txtdimissionDate2.Text);  // 确认最后离职日期。






        bool bl = ESP.HumanResource.BusinessLogic.DimissionFormManager.Update(dimissionFormInfo, hrauditLogInfo, newHRAuditLogList);
        if (!bl)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('系统出现错误，请与系统管理员联系。');", true);
            return;
        }
        else
        {

            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('离职申请单审批通过。');window.location='DimissionAuditList.aspx';", true);

            ESP.Administrative.BusinessLogic.UserAttBasicInfoManager userAtt = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager();
            ESP.Framework.Entity.DepartmentInfo companyDep = userAtt.GetRootDepartmentID(dimissionFormInfo.UserId);
            List<ESP.HumanResource.Entity.UsersInfo> list = UsersManager.GetUserList(companyDep.DepartmentID, ESP.HumanResource.Common.Status.DimissionSendMail);
            if (list != null && list.Count > 0)
            {
                foreach (ESP.HumanResource.Entity.UsersInfo userModel in list)
                {
                    mailAddressList.Add(new MailAddress(userModel.Email));
                }
            }


            ESP.Framework.Entity.UserInfo userInfo = ESP.Framework.BusinessLogic.UserManager.Get(manageModel.HRId);
            if (userInfo != null && !string.IsNullOrEmpty(userInfo.Email) && userInfo.Status == 1)
                mailAddressList.Add(new MailAddress(userInfo.Email));

            if (mailAddressList != null && mailAddressList.Count > 0)
            {
                try
                {
                    string url = "http://" + Request.Url.Authority + "/HR/Print/NewDimissionMail.aspx?dimissionId=" + dimissionFormInfo.DimissionId + "&type=2";
                    string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                    ESP.Mail.MailManager.Send("离职管理", body, true, mailAddressList.ToArray());
                }
                catch
                { }
            }

            ESP.Framework.Entity.UserInfo dimissionUserInfo = ESP.Framework.BusinessLogic.UserManager.Get(dimissionFormInfo.UserId);
            try
            {

                ESP.Mail.MailManager.Send("离职管理--业务审批通过", "" + dimissionUserInfo.FullNameCN + "您好，您的离职申请业务已审批通过。请提前与财务人员协调办理商务卡结算、现金冲销等事宜。并于last day当天登录内网系统办理离职手续。", false, new MailAddress[] { new MailAddress(dimissionUserInfo.Email) });

            }
            catch
            { }
            return;
        }
    }

    /// <summary>
    /// 审批驳回
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnManAuditOverrule_Click(object sender, EventArgs e)
    {
        int dimissionFormId = 0;
        if (!int.TryParse(hidDimissionFormID.Value, out dimissionFormId))
            dimissionFormId = 0;

        // 判断该离职单是否是处于当前登录人审批
        bool b = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.CheckDimissionData(UserID, dimissionFormId);
        if (!b)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('你没有权限操作此离职单！');", true);
            return;
        }

        // 离职单信息
        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionFormId);
        dimissionFormInfo.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.Overrule;
        if (!string.IsNullOrEmpty(txtdimissionDate.Text))
            dimissionFormInfo.LastDay = DateTime.Parse(txtdimissionDate.Text);

        // 审批日志
        ESP.HumanResource.Entity.HRAuditLogInfo hrauditLogInfo = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(UserID,
            dimissionFormInfo.DimissionId, (int)ESP.HumanResource.Common.HRFormType.DimissionForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);
        hrauditLogInfo.AuditDate = DateTime.Now;
        hrauditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Overrule;
        hrauditLogInfo.Requesition = txtRes.Text.Trim();

        bool bl = ESP.HumanResource.BusinessLogic.DimissionFormManager.DimissionOverrule(dimissionFormInfo, hrauditLogInfo);
        if (!bl)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('系统出现错误，请与系统管理员联系。');", true);
            return;
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('离职申请单审批驳回成功。');window.location='DimissionAuditList.aspx';", true);
            try
            {
                ESP.Mail.MailManager.Send("离职管理--总经理审批驳回", "您好，您的离职申请总监已审批驳回，请登录系统进行修改。", false, new MailAddress[] { new MailAddress(ESP.Framework.BusinessLogic.UserManager.Get(dimissionFormInfo.UserId).Email) });
            }
            catch
            { }
            return;
        }
    }
    #endregion

    #region 团队行政审批
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
                    labEgress.Text = string.Format("{0:F1}", preMonthStatInfo.EgressHours) + "H";   // 外出
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

            txtOfficeSupplies.Text = groupHRDetailInfo.FixedAssets;
            txtOfficeSupplies.Enabled = false;
        }
        double annualBase = 0;
        double remainAnnual = 0;
        double canUseAnnual = 0;
        double usedAnnual = 0;

        try
        {
            remainAnnual = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetAnnualLeaveInfo(dimissionUserID, dimissionDate, out canUseAnnual, out usedAnnual, out annualBase);
        }
        catch
        {
            remainAnnual = 0;
        }

        int tempdays = (int)canUseAnnual;

        canUseAnnual = tempdays;

        double yuzhiTotal = (usedAnnual - canUseAnnual) > 0 ? (usedAnnual - canUseAnnual) : 0;

        labAnnual.Text = remainAnnual <= 0 ? "0" : ((int)remainAnnual).ToString("#,##0.000");
        labOverDraft.Text = yuzhiTotal.ToString("#,##0.000");

    }

    /// <summary>
    /// 审批通过
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPassGroupHR_Click(object sender, EventArgs e)
    {
        if (ddlReason.SelectedIndex == 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                  "alert('请选择离职原因！');", true);
            return;
        }
        if (string.IsNullOrEmpty(txtOfficeSupplies.Text))
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('请填写办公用品清单，工位牌人名标签，推柜钥匙交换情况！');", true);
            return;
        }

        int dimissionFormId = 0;
        if (!int.TryParse(hidDimissionFormID.Value, out dimissionFormId))
            dimissionFormId = 0;

        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionFormId);

        ESP.HumanResource.Entity.EmailClosingInfo mailModel = null;



        CollectSelected();

        // 判断该离职单是否是处于当前登录人审批
        bool b = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.CheckDimissionData(UserID, dimissionFormId);
        if (!b)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('你没有权限操作此离职单！');", true);
            return;
        }

        ESP.Framework.Entity.OperationAuditManageInfo manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(dimissionFormInfo.UserId);
        if (manageModel == null)
            manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(dimissionFormInfo.DepartmentId);

        // 离职单信息
        dimissionFormInfo.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHRIT;  // 待人力资源、IT部审批
        dimissionFormInfo.ITAuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;   //2024.7.30新增IT审批
        dimissionFormInfo.HRAuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
        if (!string.IsNullOrEmpty(txtdimissionDate2.Text))
            dimissionFormInfo.LastDay = DateTime.Parse(txtdimissionDate2.Text);  // 确认最后离职日期。

        // 团队HR审批日志信息
        ESP.HumanResource.Entity.HRAuditLogInfo oldHRauditLogInfo = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(UserID,
            dimissionFormInfo.DimissionId, (int)ESP.HumanResource.Common.HRFormType.DimissionForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);
        oldHRauditLogInfo.AuditDate = DateTime.Now;
        oldHRauditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Audited;
        oldHRauditLogInfo.Requesition = txtRes.Text.Trim();
        oldHRauditLogInfo.AuditorId = UserID;
        oldHRauditLogInfo.AuditorName = UserInfo.FullNameCN;

        // 集团人力资源部审批信息
        ESP.HumanResource.Entity.HRAuditLogInfo newHRAuditLogInfo = new ESP.HumanResource.Entity.HRAuditLogInfo();
        //变更组织机构需要调整 
        newHRAuditLogInfo.AuditorId = manageModel.HRId;
        newHRAuditLogInfo.AuditorName = manageModel.HRName;
        newHRAuditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
        newHRAuditLogInfo.FormId = dimissionFormInfo.DimissionId;
        newHRAuditLogInfo.FormType = (int)ESP.HumanResource.Common.HRFormType.DimissionForm;
        newHRAuditLogInfo.AuditLevel = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHRIT;

        // IT部审批日志
        ESP.HumanResource.Entity.HRAuditLogInfo newITAuditLogInfo = new ESP.HumanResource.Entity.HRAuditLogInfo();
        newITAuditLogInfo.AuditorId = manageModel.ITOperatorId;
        newITAuditLogInfo.AuditorName = manageModel.ITOperator;
        newITAuditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
        newITAuditLogInfo.FormId = dimissionFormInfo.DimissionId;
        newITAuditLogInfo.FormType = (int)ESP.HumanResource.Common.HRFormType.DimissionForm;
        newITAuditLogInfo.AuditLevel = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHRIT;

        ESP.HumanResource.Entity.DimissionGrougHRDetailsInfo groupHRDetailInfo = new ESP.HumanResource.Entity.DimissionGrougHRDetailsInfo();
        groupHRDetailInfo.AdvanceAnnual = decimal.Parse(labOverDraft.Text);
        groupHRDetailInfo.DimissionId = dimissionFormInfo.DimissionId;
        groupHRDetailInfo.FixedAssets = txtOfficeSupplies.Text;
        groupHRDetailInfo.RemainAnnual = decimal.Parse(labAnnual.Text);
        groupHRDetailInfo.PrincipalID = UserID;
        groupHRDetailInfo.PrincipalName = UserInfo.FullNameCN;


        //邮箱关闭提醒
        mailModel = new ESP.HumanResource.Entity.EmailClosingInfo();
        mailModel.UserId = dimissionFormInfo.UserId;
        mailModel.UserCode = dimissionFormInfo.UserCode;
        mailModel.Status = 0;
        mailModel.Postion = "";
        mailModel.OperatorId = 0;
        mailModel.NameCN = dimissionFormInfo.UserName;
        if (radEmailIsDeleteFalse.Checked)
            mailModel.KeepDate = DateTime.Parse(txtEmailSaveLastDay.Text);
        else
            mailModel.KeepDate = dimissionFormInfo.LastDay.Value;
        mailModel.Email = txtCompanyEmail.Text;
        mailModel.DeptName = dimissionFormInfo.DepartmentName;
        mailModel.CloseDate = dimissionFormInfo.LastDay.Value;


        #region 添加赔款项
        string[] indemnityItem = Request.Form.GetValues("txtIndemnityItem");       // 赔款项
        string[] indemnityAmount = Request.Form.GetValues("txtIndemnityAmount");   // 赔款金额
        string[] indemnityDesc = Request.Form.GetValues("txtIndemnityDesc");       // 描述

        List<ESP.HumanResource.Entity.DimissionIndemnityInfo> indemnityInfoList = new List<ESP.HumanResource.Entity.DimissionIndemnityInfo>();
        if (indemnityItem != null && indemnityItem.Length > 0)
        {
            for (int i = 0; i < indemnityItem.Length; i++)
            {
                ESP.HumanResource.Entity.DimissionIndemnityInfo indemnityInfo = new ESP.HumanResource.Entity.DimissionIndemnityInfo();
                indemnityInfo.IndemnityItem = indemnityItem[i];
                indemnityInfo.IndemnityAmount = decimal.Parse(indemnityAmount[i]);
                indemnityInfo.IndemnityDesc = indemnityDesc[i];
                indemnityInfo.CreateTime = DateTime.Now;
                indemnityInfo.CreateUserid = UserID;
                indemnityInfo.UpdateTime = DateTime.Now;
                indemnityInfo.DimissionId = dimissionFormInfo.DimissionId;
                indemnityInfoList.Add(indemnityInfo);
            }
        }
        #endregion


        if (ddlReason.SelectedIndex != 0)
        {
            dimissionFormInfo.HRReason = ddlReason.SelectedItem.Text;
            dimissionFormInfo.HRReasonRemark = txtReason.Text;
        }
        bool bl = ESP.HumanResource.BusinessLogic.DimissionFormManager.Update(dimissionFormInfo, oldHRauditLogInfo,
            newHRAuditLogInfo, newITAuditLogInfo, groupHRDetailInfo, null, null, null, indemnityInfoList);

        if (!bl)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('系统出现错误，请与系统管理员联系。');", true);
            return;
        }
        else
        {
            ESP.HumanResource.BusinessLogic.EmailClosingManager.Add(mailModel);

            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('离职申请单审批通过。');window.location='DimissionAuditList.aspx';", true);

            List<MailAddress> mailAddressList = new List<MailAddress>();

            ESP.Administrative.BusinessLogic.UserAttBasicInfoManager userAtt = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager();
            ESP.Framework.Entity.DepartmentInfo companyDep = userAtt.GetRootDepartmentID(dimissionFormInfo.UserId);
            List<ESP.HumanResource.Entity.UsersInfo> list = UsersManager.GetUserList(companyDep.DepartmentID, ESP.HumanResource.Common.Status.DimissionSendMailIT);
            if (list != null && list.Count > 0)
            {
                foreach (ESP.HumanResource.Entity.UsersInfo userModel in list)
                {
                    mailAddressList.Add(new MailAddress(userModel.Email));
                }
            }



            if (mailAddressList != null && mailAddressList.Count > 0)
            {
                try
                {
                    string url = "http://" + Request.Url.Authority + "/HR/Print/NewDimissionMail.aspx?dimissionId=" + dimissionFormInfo.DimissionId + "&type=1";
                    string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                    ESP.Mail.MailManager.Send("离职管理", body, true, mailAddressList.ToArray());
                }
                catch
                { }
            }
            ESP.Framework.Entity.UserInfo dimissionUserInfo = ESP.Framework.BusinessLogic.UserManager.Get(dimissionFormInfo.UserId);

            try
            {
                ESP.Mail.MailManager.Send("离职管理--团队行政审批通过", "" + dimissionUserInfo.FullNameCN + "您好，您的离职申请团队行政已审批通过，等待下一级审批。", false, new MailAddress[] { new MailAddress(dimissionUserInfo.Email) });
            }
            catch
            { }
            return;
        }
    }

    /// <summary>
    /// 审批驳回
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOverruleGroupHR_Click(object sender, EventArgs e)
    {
        int dimissionFormId = 0;
        if (!int.TryParse(hidDimissionFormID.Value, out dimissionFormId))
            dimissionFormId = 0;

        // 判断该离职单是否是处于当前登录人审批
        bool b = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.CheckDimissionData(UserID, dimissionFormId);
        if (!b)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('你没有权限操作此离职单！');", true);
            return;
        }

        // 离职单信息
        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionFormId);
        dimissionFormInfo.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.Overrule;
        if (!string.IsNullOrEmpty(txtdimissionDate.Text))
            dimissionFormInfo.LastDay = DateTime.Parse(txtdimissionDate.Text);

        // 审批日志
        ESP.HumanResource.Entity.HRAuditLogInfo hrauditLogInfo = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(UserID,
            dimissionFormInfo.DimissionId, (int)ESP.HumanResource.Common.HRFormType.DimissionForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);
        hrauditLogInfo.AuditDate = DateTime.Now;
        hrauditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Overrule;
        hrauditLogInfo.Requesition = txtRes.Text.Trim();

        bool bl = ESP.HumanResource.BusinessLogic.DimissionFormManager.DimissionOverrule(dimissionFormInfo, hrauditLogInfo);
        if (!bl)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('系统出现错误，请与系统管理员联系。');", true);
            return;
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('离职申请单审批驳回成功。');window.location='DimissionAuditList.aspx';", true);
            return;
        }
    }
    #endregion

    #region 集团人力资源审批
    /// <summary>
    /// 设置集团人力资源审批时社保交费截止时间
    /// </summary>
    /// <param name="dimissionId">离职单编号</param>
    protected void SetInsDateTime(int dimissionId)
    {
        ESP.HumanResource.Entity.DimissionHRDetailsInfo hrDetailInfo = ESP.HumanResource.BusinessLogic.DimissionHRDetailsManager.GetHRDetailInfo(dimissionid);
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
        }
        catch
        {
            drpCapitalReserveY.SelectedValue = DateTime.Now.Year.ToString();
        }

        if (hrDetailInfo == null)
        {
            chkComplementaryMedical.Checked = false;
            drpAddedMedicalInsY.Enabled = false;
            drpAddedMedicalInsM.Enabled = false;
        }
        else
        {
            chkComplementaryMedical.Enabled = false;
            if (hrDetailInfo.IsComplementaryMedical)
            {
                chkComplementaryMedical.Checked = hrDetailInfo.IsComplementaryMedical;
            }
            else
            {
                drpAddedMedicalInsY.Enabled = false;
                drpAddedMedicalInsM.Enabled = false;
            }
        }

        //if (hrDetailInfo.IsComplementaryMedical)
        //{
        //    drpAddedMedicalInsY.SelectedValue =
        //        hrDetailInfo.AddedMedicalInsLastMonth != null
        //        ? hrDetailInfo.AddedMedicalInsLastMonth.Value.Year.ToString() : "";
        //    drpAddedMedicalInsY.Enabled = false;
        //}
        //else
        //{
        //    drpAddedMedicalInsY.SelectedValue = DateTime.Now.Year.ToString();
        //    drpAddedMedicalInsY.Enabled = false;
        //}
        try
        {
            drpAddedMedicalInsY.SelectedValue =
                hrDetailInfo.AddedMedicalInsLastMonth != null ?
                hrDetailInfo.AddedMedicalInsLastMonth.Value.Year.ToString() : "";
            drpAddedMedicalInsY.Enabled = false;
        }
        catch
        {
            drpAddedMedicalInsY.SelectedValue = DateTime.Now.Year.ToString();
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
        }
        catch
        {
            drpCapitalReserveM.SelectedValue = DateTime.Now.Month.ToString("00");
        }

        //if (hrDetailInfo.IsComplementaryMedical)
        //{
        //    drpAddedMedicalInsM.SelectedValue =
        //        hrDetailInfo.AddedMedicalInsLastMonth != null
        //        ? hrDetailInfo.AddedMedicalInsLastMonth.Value.Year.ToString() : DateTime.Now.Year.ToString();
        //    drpAddedMedicalInsM.Enabled = false;
        //}
        //else
        //{
        //    drpAddedMedicalInsM.SelectedValue = DateTime.Now.Month.ToString("00");
        //    drpAddedMedicalInsM.Enabled = false;
        //}
        try
        {
            drpAddedMedicalInsM.SelectedValue =
                hrDetailInfo.AddedMedicalInsLastMonth != null ?
                hrDetailInfo.AddedMedicalInsLastMonth.Value.Month.ToString("00") : "";
            drpAddedMedicalInsM.Enabled = false;
        }
        catch
        {
            drpAddedMedicalInsM.SelectedValue = DateTime.Now.Month.ToString("00");
        }



        if (hrDetailInfo != null && !string.IsNullOrEmpty(hrDetailInfo.Remark))
            txtHRRemark.Text = hrDetailInfo.Remark;
    }

    /// <summary>
    /// 审批通过
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPassHR_Click(object sender, EventArgs e)
    {
        int dimissionFormId = 0;
        if (!int.TryParse(hidDimissionFormID.Value, out dimissionFormId))
            dimissionFormId = 0;

        CollectSelected();

        // 判断该离职单是否是处于当前登录人审批
        bool b = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.CheckDimissionData(UserID, dimissionFormId);
        if (!b)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('你没有权限操作此离职单！');", true);
            return;
        }

        // 离职单信息

        List<ESP.HumanResource.Entity.HRAuditLogInfo> newHRAuditLogList = new List<ESP.HumanResource.Entity.HRAuditLogInfo>();
        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionFormId);
        if (!string.IsNullOrEmpty(txtdimissionDate2.Text))
            dimissionFormInfo.LastDay = DateTime.Parse(txtdimissionDate2.Text);  // 确认最后离职日期。
        #region 添加赔款项
        string[] indemnityItem = Request.Form.GetValues("txtIndemnityItem");       // 赔款项
        string[] indemnityAmount = Request.Form.GetValues("txtIndemnityAmount");   // 赔款金额
        string[] indemnityDesc = Request.Form.GetValues("txtIndemnityDesc");       // 描述

        List<ESP.HumanResource.Entity.DimissionIndemnityInfo> indemnityInfoList = new List<ESP.HumanResource.Entity.DimissionIndemnityInfo>();
        if (indemnityItem != null && indemnityItem.Length > 0)
        {
            for (int i = 0; i < indemnityItem.Length; i++)
            {
                ESP.HumanResource.Entity.DimissionIndemnityInfo indemnityInfo = new ESP.HumanResource.Entity.DimissionIndemnityInfo();
                indemnityInfo.IndemnityItem = indemnityItem[i];
                indemnityInfo.IndemnityAmount = decimal.Parse(indemnityAmount[i]);
                indemnityInfo.IndemnityDesc = indemnityDesc[i];
                indemnityInfo.CreateTime = DateTime.Now;
                indemnityInfo.CreateUserid = UserID;
                indemnityInfo.UpdateTime = DateTime.Now;
                indemnityInfo.DimissionId = dimissionFormInfo.DimissionId;
                indemnityInfoList.Add(indemnityInfo);
            }
        }
        #endregion

        // IT部审批信息
        ESP.HumanResource.Entity.DimissionITDetailsInfo itDetailInfo = new ESP.HumanResource.Entity.DimissionITDetailsInfo();
        itDetailInfo.DimissionId = dimissionFormInfo.DimissionId;

        itDetailInfo.Email = txtCompanyEmail.Text;
        itDetailInfo.EmailIsDelete = radEmailIsDeleteFalse.Checked ? false : true;  // 公司邮箱
        if (radEmailIsDeleteFalse.Checked)
            itDetailInfo.EmailSaveLastDay = DateTime.Parse(txtEmailSaveLastDay.Text);

        itDetailInfo.OtherDes = txtITOther.Text;
        itDetailInfo.OwnPCCode = txtOwnPCCode.Text;
        itDetailInfo.PCCode = txtPCCode.Text;
        itDetailInfo.PCUsedDes = txtPCUsedDes.Text;
        itDetailInfo.PrincipalID = UserID;
        itDetailInfo.PrincipalName = UserInfo.FullNameCN;

        dimissionFormInfo.HRAuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Audited;


        // 审批日志信息
        ESP.HumanResource.Entity.HRAuditLogInfo oldHRauditLogInfo = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(UserID,
            dimissionFormInfo.DimissionId, (int)ESP.HumanResource.Common.HRFormType.DimissionForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);
        oldHRauditLogInfo.AuditDate = DateTime.Now;
        oldHRauditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Audited;
        oldHRauditLogInfo.Requesition = txtRes.Text.Trim();
        oldHRauditLogInfo.AuditorId = UserID;
        oldHRauditLogInfo.AuditorName = UserInfo.FullNameCN;

        ESP.HumanResource.Entity.DimissionHRDetailsInfo hrDetailInfo = new ESP.HumanResource.Entity.DimissionHRDetailsInfo();
        hrDetailInfo.DimissionId = dimissionFormInfo.DimissionId;
        hrDetailInfo.SocialInsLastMonth = new DateTime(int.Parse(drpSocialInsY.SelectedValue), int.Parse(drpSocialInsM.SelectedValue), 1);
        hrDetailInfo.MedicalInsLastMonth = new DateTime(int.Parse(drpMedicalInsY.SelectedValue), int.Parse(drpMedicalInsM.SelectedValue), 1);
        hrDetailInfo.CapitalReserveLastMonth = new DateTime(int.Parse(drpCapitalReserveY.SelectedValue), int.Parse(drpCapitalReserveM.SelectedValue), 1);
        hrDetailInfo.IsComplementaryMedical = chkComplementaryMedical.Checked;// 是否有补充医疗保险
        if (!chkComplementaryMedical.Checked)
            hrDetailInfo.AddedMedicalInsLastMonth = null;
        else
            hrDetailInfo.AddedMedicalInsLastMonth = new DateTime(int.Parse(drpAddedMedicalInsY.SelectedValue), int.Parse(drpAddedMedicalInsM.SelectedValue), 1);
        hrDetailInfo.Principal1ID = UserID;
        hrDetailInfo.Principal1Name = UserInfo.FullNameCN;
        hrDetailInfo.Remark = txtHRRemark.Text.Trim();

        ESP.HumanResource.Entity.DimissionGrougHRDetailsInfo groupHRDetailInfo = null;

        bool bl = ESP.HumanResource.BusinessLogic.DimissionFormManager.Update(dimissionFormInfo, oldHRauditLogInfo, newHRAuditLogList, hrDetailInfo, groupHRDetailInfo, itDetailInfo, indemnityInfoList);

        if (!bl)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('系统出现错误，请与系统管理员联系。');", true);
            return;
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('离职申请单审批通过。');window.location='DimissionAuditList.aspx';", true);

            List<MailAddress> mailAddressList = new List<MailAddress>();
            if (newHRAuditLogList != null && newHRAuditLogList.Count > 0)
            {
                foreach (ESP.HumanResource.Entity.HRAuditLogInfo loginfo in newHRAuditLogList)
                {
                    ESP.Framework.Entity.UserInfo userInfo = ESP.Framework.BusinessLogic.UserManager.Get(loginfo.AuditorId);
                    if (userInfo != null && !string.IsNullOrEmpty(userInfo.Email) && userInfo.Status == 1)
                        mailAddressList.Add(new MailAddress(userInfo.Email));
                }
            }
            ESP.Administrative.BusinessLogic.UserAttBasicInfoManager userAtt = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager();
            ESP.Framework.Entity.DepartmentInfo companyDep = userAtt.GetRootDepartmentID(dimissionFormInfo.UserId);
            List<ESP.HumanResource.Entity.UsersInfo> list = UsersManager.GetUserList(companyDep.DepartmentID, ESP.HumanResource.Common.Status.DimissionSendMailIT);
            if (list != null && list.Count > 0)
            {
                foreach (ESP.HumanResource.Entity.UsersInfo userModel in list)
                {
                    mailAddressList.Add(new MailAddress(userModel.Email));
                }
            }



            if (mailAddressList != null && mailAddressList.Count > 0)
            {
                try
                {
                    string url = "http://" + Request.Url.Authority + "/HR/Print/NewDimissionMail.aspx?dimissionId=" + dimissionFormInfo.DimissionId + "&type=1";
                    string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                    ESP.Mail.MailManager.Send("离职管理", body, true, mailAddressList.ToArray());
                }
                catch
                { }
            }
            ESP.Framework.Entity.UserInfo dimissionUserInfo = ESP.Framework.BusinessLogic.UserManager.Get(dimissionFormInfo.UserId);
            try
            {
                ESP.Mail.MailManager.Send("离职管理--集团人力资源审批通过", dimissionUserInfo.FullNameCN + "您好，您的离职申请集团人力资源已审批通过，等待下一级审批。", false, new MailAddress[] { new MailAddress(dimissionUserInfo.Email) });
            }
            catch
            { }
            return;
        }
    }

    /// <summary>
    /// 审批驳回
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOverruleHR_Click(object sender, EventArgs e)
    {
        int dimissionFormId = 0;
        if (!int.TryParse(hidDimissionFormID.Value, out dimissionFormId))
            dimissionFormId = 0;

        // 判断该离职单是否是处于当前登录人审批
        bool b = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.CheckDimissionData(UserID, dimissionFormId);
        if (!b)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('你没有权限操作此离职单！');", true);
            return;
        }

        // 离职单信息
        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionFormId);
        dimissionFormInfo.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.Overrule;
        if (!string.IsNullOrEmpty(txtdimissionDate.Text))
            dimissionFormInfo.LastDay = DateTime.Parse(txtdimissionDate.Text);

        // 审批日志
        ESP.HumanResource.Entity.HRAuditLogInfo hrauditLogInfo = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(UserID,
            dimissionFormInfo.DimissionId, (int)ESP.HumanResource.Common.HRFormType.DimissionForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);
        hrauditLogInfo.AuditDate = DateTime.Now;
        hrauditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Overrule;
        hrauditLogInfo.Requesition = txtRes.Text.Trim();

        bool bl = ESP.HumanResource.BusinessLogic.DimissionFormManager.DimissionOverrule(dimissionFormInfo, hrauditLogInfo);
        if (!bl)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('系统出现错误，请与系统管理员联系。');", true);
            return;
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('离职申请单审批驳回成功。');window.location='DimissionAuditList.aspx';", true);
            return;
        }
    }

    /// <summary>
    /// 审批通过
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPassHR2_Click(object sender, EventArgs e)
    {
        int dimissionFormId = 0;
        if (!int.TryParse(hidDimissionFormID.Value, out dimissionFormId))
            dimissionFormId = 0;

        CollectSelected();

        // 判断该离职单是否是处于当前登录人审批
        bool b = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.CheckDimissionData(UserID, dimissionFormId);
        if (!b)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('你没有权限操作此离职单！');", true);
            return;
        }
        if (drpBranchList2.SelectedValue == "0")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                  "alert('请选择所属公司！');", true);
            return;
        }

        // 离职单信息
        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionFormId);
        dimissionFormInfo.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.AuditComplete;
        if (!string.IsNullOrEmpty(txtdimissionDate2.Text))
            dimissionFormInfo.LastDay = DateTime.Parse(txtdimissionDate2.Text);  // 确认最后离职日期。

        // 审批日志信息
        ESP.HumanResource.Entity.HRAuditLogInfo oldHRauditLogInfo = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(UserID,
            dimissionFormInfo.DimissionId, (int)ESP.HumanResource.Common.HRFormType.DimissionForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);
        oldHRauditLogInfo.AuditDate = DateTime.Now;
        oldHRauditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Audited;
        oldHRauditLogInfo.Requesition = txtRes.Text.Trim();
        oldHRauditLogInfo.AuditorId = UserID;
        oldHRauditLogInfo.AuditorName = UserInfo.FullNameCN;

        // 集团人力审批内容信息
        ESP.HumanResource.Entity.DimissionHRDetailsInfo hrDetailInfo = ESP.HumanResource.BusinessLogic.DimissionHRDetailsManager.GetHRDetailInfo(dimissionFormInfo.DimissionId);
        hrDetailInfo.IsArchives = radIsHaveArchivesFalse.Checked ? false : true;
        if (radIsHaveArchivesTrue.Checked)
            hrDetailInfo.TurnAroundDate = DateTime.Parse(txtTurnAroundDate.Text);
        hrDetailInfo.Principal2ID = UserID;
        hrDetailInfo.Principal2Name = UserInfo.FullNameCN;
        int branchId = 0;
        if (!int.TryParse(drpBranchList2.SelectedValue, out branchId))
            branchId = 0;
        hrDetailInfo.BranchId = branchId;
        hrDetailInfo.IsShowPosition = radIsShowPositionFalse.Checked ? false : true;

        ESP.HumanResource.Entity.EmployeeBaseInfo employeeBase = EmployeeBaseManager.GetModel(dimissionFormInfo.UserId);
        employeeBase.Status = ESP.HumanResource.Common.Status.Dimission;
        employeeBase.InternalEmail = "$" + employeeBase.InternalEmail;
        employeeBase.IDNumber = "$" + employeeBase.IDNumber;
        ESP.HumanResource.Entity.DimissionInfo dimissionModel = ESP.HumanResource.BusinessLogic.DimissionManager.GetModelByUserID(dimissionFormInfo.UserId);

        if (dimissionModel == null)
            dimissionModel = new ESP.HumanResource.Entity.DimissionInfo();

        dimissionModel.userCode = dimissionFormInfo.UserCode;
        dimissionModel.userId = dimissionFormInfo.UserId;  //申请人sysid
        dimissionModel.userName = dimissionFormInfo.UserName;
        dimissionModel.joinJobDate = DateTime.Parse(txtjoinJobDate.Text.Trim());
        ESP.Framework.Entity.DepartmentInfo depModel = ESP.Framework.BusinessLogic.DepartmentManager.Get(dimissionFormInfo.DepartmentId);
        ESP.Framework.Entity.DepartmentInfo depParentModel = ESP.Framework.BusinessLogic.DepartmentManager.Get(depModel.ParentID);
        ESP.Framework.Entity.DepartmentInfo depRootModel = ESP.Framework.BusinessLogic.DepartmentManager.Get(depParentModel.ParentID);
        dimissionModel.groupID = dimissionFormInfo.DepartmentId;
        dimissionModel.groupName = depModel.DepartmentName;
        dimissionModel.departmentID = depParentModel.DepartmentID;
        dimissionModel.departmentName = depParentModel.DepartmentName;
        dimissionModel.companyID = depRootModel.DepartmentID;
        dimissionModel.companyName = depRootModel.DepartmentName;
        dimissionModel.dimissionDate = dimissionFormInfo.LastDay.Value;
        dimissionModel.dimissionCause = dimissionFormInfo.Reason;
        dimissionModel.createDate = DateTime.Now;
        dimissionModel.creater = UserID;

        ESP.HumanResource.Entity.DimissionADDetailsInfo adDetailInfo = null;


        bool bl = ESP.HumanResource.BusinessLogic.DimissionFormManager.Update(dimissionFormInfo, oldHRauditLogInfo,
            null, null, null, hrDetailInfo, employeeBase, dimissionModel, adDetailInfo);
        if (!bl)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('系统出现错误，请与系统管理员联系。');", true);
            return;
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('离职申请单审批通过。');window.location='DimissionAuditList.aspx';", true);
            ESP.Framework.Entity.UserInfo dimissionUserInfo = ESP.Framework.BusinessLogic.UserManager.Get(dimissionFormInfo.UserId);
            try
            {
                ESP.Mail.MailManager.Send("离职管理--审批通过", dimissionUserInfo.FullNameCN + "您好，您的离职手续已办完，如若需要离职证明，请来公司签字打印，请联系人力资源部。再次感谢您为星言云汇所做的贡献，希望有机会再次与您共事！", false, new MailAddress[] { new MailAddress(dimissionFormInfo.PrivateMail) });
            }
            catch
            { }
            return;
        }
    }

    /// <summary>
    /// 审批驳回
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOverruleHR2_Click(object sender, EventArgs e)
    {
        int dimissionFormId = 0;
        if (!int.TryParse(hidDimissionFormID.Value, out dimissionFormId))
            dimissionFormId = 0;

        // 判断该离职单是否是处于当前登录人审批
        bool b = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.CheckDimissionData(UserID, dimissionFormId);
        if (!b)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('你没有权限操作此离职单！');", true);
            return;
        }

        // 离职单信息
        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionFormId);
        dimissionFormInfo.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.Overrule;
        if (!string.IsNullOrEmpty(txtdimissionDate.Text))
            dimissionFormInfo.LastDay = DateTime.Parse(txtdimissionDate.Text);

        // 审批日志
        ESP.HumanResource.Entity.HRAuditLogInfo hrauditLogInfo = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(UserID,
            dimissionFormInfo.DimissionId, (int)ESP.HumanResource.Common.HRFormType.DimissionForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);
        hrauditLogInfo.AuditDate = DateTime.Now;
        hrauditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Overrule;
        hrauditLogInfo.Requesition = txtRes.Text.Trim();

        bool bl = ESP.HumanResource.BusinessLogic.DimissionFormManager.DimissionOverrule(dimissionFormInfo, hrauditLogInfo);
        if (!bl)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('系统出现错误，请与系统管理员联系。');", true);
            return;
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('离职申请单审批驳回成功。');window.location='DimissionAuditList.aspx';", true);
            return;
        }
    }
    #endregion

    #region IT部审批
    protected void SetITInfo(int dimissionId)
    {
        ESP.HumanResource.Entity.DimissionITDetailsInfo itDetailInfo =
            ESP.HumanResource.BusinessLogic.DimissionITDetailsManager.GetITDetailInfo(dimissionid);
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

            txtPCCode.Text = itDetailInfo.PCCode;
            txtPCUsedDes.Text = itDetailInfo.PCUsedDes;
            txtITOther.Text = itDetailInfo.OtherDes;
            txtOwnPCCode.Text = itDetailInfo.OwnPCCode;
        }
    }

    /// <summary>
    /// 审批通过
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPassIT_Click(object sender, EventArgs e)
    {
        int dimissionFormId = 0;
        if (!int.TryParse(hidDimissionFormID.Value, out dimissionFormId))
            dimissionFormId = 0;

        CollectSelected();

        // 判断该离职单是否是处于当前登录人审批
        bool b = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.CheckDimissionData(UserID, dimissionFormId);
        if (!b)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('你没有权限操作此离职单！');", true);
            return;
        }

        // 离职单信息
        List<ESP.HumanResource.Entity.HRAuditLogInfo> newHRAuditLogList = new List<ESP.HumanResource.Entity.HRAuditLogInfo>();
        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionFormId);

        if (dimissionFormInfo.HRAuditStatus == (int)ESP.HumanResource.Common.AuditStatus.NotAudit)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                  "alert('请等待HR录入邮箱处置、固定资产清理等相关信息！');", true);
            return;
        }

        ESP.Framework.Entity.DepartmentInfo departmentInfo =
new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager().GetRootDepartmentID(dimissionFormInfo.UserId);

        ESP.HumanResource.Entity.DimissionFinanceDetailsInfo financeDetailInfo =
ESP.HumanResource.BusinessLogic.DimissionFinanceDetailsManager.GetFinanceDetailInfo(dimissionFormId);
        if (financeDetailInfo == null)
            financeDetailInfo = new ESP.HumanResource.Entity.DimissionFinanceDetailsInfo();

        dimissionFormInfo.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitFinance;
        dimissionFormInfo.ITAuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Audited;

        string salaryUsers = ESP.Finance.BusinessLogic.BranchManager.GetSalaryUsers(departmentInfo.DepartmentID);
        string[] salaryUsersArr = salaryUsers.Split(',');
        foreach (string str in salaryUsersArr)
        {
            int salaryUserId = 0;
            if (int.TryParse(str, out salaryUserId))
            {
                ESP.Framework.Entity.UserInfo userModel = ESP.Framework.BusinessLogic.UserManager.Get(salaryUserId);
                if (userModel != null)
                {
                    ESP.HumanResource.Entity.HRAuditLogInfo newHRAuditLogInfo = new ESP.HumanResource.Entity.HRAuditLogInfo();
                    newHRAuditLogInfo.AuditorId = userModel.UserID;
                    newHRAuditLogInfo.AuditorName = userModel.FullNameCN;
                    newHRAuditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
                    newHRAuditLogInfo.FormId = dimissionFormInfo.DimissionId;
                    newHRAuditLogInfo.FormType = (int)ESP.HumanResource.Common.HRFormType.DimissionForm;
                    newHRAuditLogInfo.AuditLevel = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitFinance;
                    newHRAuditLogList.Add(newHRAuditLogInfo);
                }
            }
        }
        dimissionFormInfo.FinanceAuditStatus = 3;
        financeDetailInfo.BusinessCard = "无商务卡";
        if (!string.IsNullOrEmpty(txtTotalIndemnityAmount.Text.Trim()))
            dimissionFormInfo.TotalIndemnityAmount = decimal.Parse(txtTotalIndemnityAmount.Text.Trim());


        // 更新审批日志信息
        ESP.HumanResource.Entity.HRAuditLogInfo oldHRauditLogInfo = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(UserID,
            dimissionFormInfo.DimissionId, (int)ESP.HumanResource.Common.HRFormType.DimissionForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);
        oldHRauditLogInfo.AuditDate = DateTime.Now;
        oldHRauditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Audited;
        oldHRauditLogInfo.Requesition = txtRes.Text.Trim();
        oldHRauditLogInfo.AuditorId = UserID;
        oldHRauditLogInfo.AuditorName = UserInfo.FullNameCN;

        // IT部审批信息
        ESP.HumanResource.Entity.DimissionITDetailsInfo itDetailInfo = ESP.HumanResource.BusinessLogic.DimissionITDetailsManager.GetITDetailInfo(dimissionFormInfo.DimissionId);


        bool bl = ESP.HumanResource.BusinessLogic.DimissionFormManager.Update(dimissionFormInfo, oldHRauditLogInfo, newHRAuditLogList, itDetailInfo, null);

        if (!bl)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('系统出现错误，请与系统管理员联系。');", true);
            return;
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('离职申请单审批通过。');window.location='DimissionAuditList.aspx';", true);

            List<MailAddress> mailAddressList = new List<MailAddress>();
            if (newHRAuditLogList != null && newHRAuditLogList.Count > 0)
            {
                foreach (ESP.HumanResource.Entity.HRAuditLogInfo loginfo in newHRAuditLogList)
                {
                    ESP.Framework.Entity.UserInfo userInfo = ESP.Framework.BusinessLogic.UserManager.Get(loginfo.AuditorId);
                    if (userInfo != null && !string.IsNullOrEmpty(userInfo.Email) && userInfo.Status == 1)
                        mailAddressList.Add(new MailAddress(userInfo.Email));
                }

            }


            if (mailAddressList != null && mailAddressList.Count > 0)
            {
                try
                {
                    string url = "http://" + Request.Url.Authority + "/HR/Print/NewDimissionMail.aspx?dimissionId=" + dimissionFormInfo.DimissionId + "&type=1";
                    string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                    ESP.Mail.MailManager.Send("离职管理", body, true, mailAddressList.ToArray());
                }
                catch
                { }
            }

            return;
        }
    }

    /// <summary>
    /// 审批驳回
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOverruleIT_Click(object sender, EventArgs e)
    {
        int dimissionFormId = 0;
        if (!int.TryParse(hidDimissionFormID.Value, out dimissionFormId))
            dimissionFormId = 0;

        // 判断该离职单是否是处于当前登录人审批
        bool b = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.CheckDimissionData(UserID, dimissionFormId);
        if (!b)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('你没有权限操作此离职单！');", true);
            return;
        }

        // 离职单信息
        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionFormId);
        dimissionFormInfo.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.Overrule;
        if (!string.IsNullOrEmpty(txtdimissionDate.Text))
            dimissionFormInfo.LastDay = DateTime.Parse(txtdimissionDate.Text);

        // 审批日志
        ESP.HumanResource.Entity.HRAuditLogInfo hrauditLogInfo = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(UserID,
            dimissionFormInfo.DimissionId, (int)ESP.HumanResource.Common.HRFormType.DimissionForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);
        hrauditLogInfo.AuditDate = DateTime.Now;
        hrauditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Overrule;
        hrauditLogInfo.Requesition = txtRes.Text.Trim();

        bool bl = ESP.HumanResource.BusinessLogic.DimissionFormManager.DimissionOverrule(dimissionFormInfo, hrauditLogInfo);
        if (!bl)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('系统出现错误，请与系统管理员联系。');", true);
            return;
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('离职申请单审批驳回成功。');window.location='DimissionAuditList.aspx';", true);
            return;
        }
    }
    #endregion

    #region 财务审批
    protected void SetFinanceInfo(int dimissionId)
    {
        ESP.HumanResource.Entity.DimissionFinanceDetailsInfo financeDetailInfo =
            ESP.HumanResource.BusinessLogic.DimissionFinanceDetailsManager.GetFinanceDetailInfo(dimissionid);
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
            if (financeDetailInfo.Salary.Contains("需补缴"))
            {
                radSalary1.Checked = false;
                radSalary2.Checked = true;
                txtSalary.Text = txtSalary.Text.Replace("需补缴", "");
            }
        }
    }

    /// <summary>
    /// 显示iPhone体验计划。
    /// </summary>
    /// <param name="dimissionUserid"></param>
    protected void SetMobileInfo(int dimissionUserid)
    {
        ESP.Finance.Entity.MobileListInfo mobileListInfo = ESP.Finance.BusinessLogic.MobileListManager.GetModel(dimissionUserid);
        ESP.Finance.Entity.AssetInfo model = ESP.Finance.BusinessLogic.AssetManager.GetModel(dimissionUserid);
        if (mobileListInfo != null)
        {
            labMobileInfo.Text = "从 " + mobileListInfo.EndDate.Year + "年" + mobileListInfo.EndDate.Month + "月起享受话费补贴";

        }
        if (model != null)
        {
            labAppleBook.Text = "苹果笔记本(" + model.AssetCode + "):领用月份(" + model.appDate + "),停止电脑补贴月份(" + model.StopAllowanceDate + ")";
        }
    }

    /// <summary>
    /// 审批通过
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPassFinance_Click(object sender, EventArgs e)
    {
        int dimissionFormId = 0;

        List<MailAddress> mailAddressList = new List<MailAddress>();

        if (!int.TryParse(hidDimissionFormID.Value, out dimissionFormId))
            dimissionFormId = 0;

        CollectSelected();
        // 判断该离职单是否是处于当前登录人审批
        int backUpUserId = 0;
        bool b = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.CheckDimissionData(UserID, dimissionFormId);
        if (!b)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('你没有权限操作此离职单！');", true);
            return;
        }
        IList<ESP.Framework.Entity.AuditBackUpInfo> backUpUserlist = ESP.Framework.BusinessLogic.AuditBackUpManager.GetModelsByBackUpUserID(UserID);
        if (backUpUserlist != null && backUpUserlist.Count > 0)
            backUpUserId = backUpUserlist[0].BackupUserID;

        // 离职单信息
        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionFormId);

        ESP.Framework.Entity.OperationAuditManageInfo manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByUserId(dimissionFormInfo.UserId);
        if (manageModel == null)
            manageModel = ESP.Framework.BusinessLogic.OperationAuditManageManager.GetModelByDepId(dimissionFormInfo.DepartmentId);

        List<ESP.HumanResource.Entity.HRAuditLogInfo> newAuditLogList = new List<ESP.HumanResource.Entity.HRAuditLogInfo>();

        ESP.HumanResource.Entity.DimissionFinanceDetailsInfo financeDetailInfo =
            ESP.HumanResource.BusinessLogic.DimissionFinanceDetailsManager.GetFinanceDetailInfo(dimissionFormId);
        if (financeDetailInfo == null)
            financeDetailInfo = new ESP.HumanResource.Entity.DimissionFinanceDetailsInfo();

        financeDetailInfo.DimissionId = dimissionFormInfo.DimissionId;
        //financeDetailInfo.Loan = txtLoan.Text;
        //financeDetailInfo.AccountsPayable = txtAccountsPayable.Text;

        // 获得离职员工所在部门的审批信息
        ESP.Framework.Entity.DepartmentInfo departmentInfo =
            new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager().GetRootDepartmentID(dimissionFormInfo.UserId);

        List<ESP.HumanResource.Entity.HRAuditLogInfo> list = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(dimissionFormInfo.DimissionId,
                (int)ESP.HumanResource.Common.HRFormType.DimissionForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);
        // 更新员工离职状态信息
        ESP.HumanResource.Entity.EmployeeBaseInfo empBaseInfo = ESP.HumanResource.BusinessLogic.EmployeeBaseManager.GetModel(dimissionFormInfo.UserId);

        if (dimissionFormInfo.FinanceAuditStatus == 1)  // 财务第一级和财务第二级审批
        {
            string financeLevel1Users = "," + ESP.Finance.BusinessLogic.BranchManager.GetDimissionAuditors(departmentInfo.DepartmentID) + ",";

            if (backUpUserId != 0 && financeLevel1Users.IndexOf("," + backUpUserId + ",") >= 0)
                financeLevel1Users += UserID + ",";

            ESP.HumanResource.Entity.HRAuditLogInfo hrAuditLogInfo = new ESP.HumanResource.Entity.HRAuditLogInfo();

            if (list != null && list.Count == 1 && (list[0].AuditorId == UserID || UserID == backUpUserId))
            {
                hrAuditLogInfo.AuditorId = manageModel.Hrattendanceid;  // 团队行政审批
                hrAuditLogInfo.AuditorName = manageModel.Hrattendancename;
                hrAuditLogInfo.FormId = dimissionFormInfo.DimissionId;
                hrAuditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
                hrAuditLogInfo.FormType = (int)ESP.HumanResource.Common.HRFormType.DimissionForm;
                hrAuditLogInfo.AuditLevel = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitGroupHR;
                newAuditLogList.Add(hrAuditLogInfo);

                ESP.HumanResource.Entity.UsersInfo hrModel = ESP.HumanResource.BusinessLogic.UsersManager.GetModel(manageModel.HRId);

                mailAddressList.Add(new MailAddress(hrModel.Email));

                dimissionFormInfo.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitGroupHR;
            }

            if (financeLevel1Users.IndexOf("," + UserID.ToString() + ",") != -1)
            {
                if (string.IsNullOrEmpty(financeDetailInfo.TellerIds)
                    || (!string.IsNullOrEmpty(financeDetailInfo.TellerIds) && financeDetailInfo.TellerIds.IndexOf(UserID.ToString()) == -1))
                {
                    financeDetailInfo.TellerIds += UserID + ",";
                    financeDetailInfo.TellerNames += UserInfo.FullNameCN + ",";
                }
                // 保存借款审批信息
                //X、S、K、D、C、Z
                StringBuilder strLoan = new StringBuilder();
                //if (!string.IsNullOrEmpty(txtLoanA.Text.Trim()))
                //    strLoan.Append("A：" + txtLoanA.Text.Trim() + "，");
                if (!string.IsNullOrEmpty(txtLoanX.Text.Trim()))
                    strLoan.Append("X：" + txtLoanX.Text.Trim() + "，");
                if (!string.IsNullOrEmpty(txtLoanS.Text.Trim()))
                    strLoan.Append("S：" + txtLoanS.Text.Trim() + "，");
                if (!string.IsNullOrEmpty(txtLoanK.Text.Trim()))
                    strLoan.Append("K：" + txtLoanK.Text.Trim() + "，");
                if (!string.IsNullOrEmpty(txtLoanD.Text.Trim()))
                    strLoan.Append("D：" + txtLoanD.Text.Trim() + "，");
                if (!string.IsNullOrEmpty(txtLoanZ.Text.Trim()))
                    strLoan.Append("Z：" + txtLoanZ.Text.Trim() + "，");

                if (!string.IsNullOrEmpty(txtLoanC.Text.Trim()))
                    strLoan.Append("C：" + txtLoanC.Text.Trim() + "，");

                financeDetailInfo.Loan += UserInfo.FullNameCN + "：(" + strLoan.ToString().TrimEnd('，') + ")\r\n";
            }



        }
        else if (dimissionFormInfo.FinanceAuditStatus == 3)   // 财务工资计算审批
        {
            if (list != null && list.Count == 1 && (list[0].AuditorId == UserID || UserID == backUpUserId))
            {
                if (radSalary1.Checked)
                {
                    dimissionFormInfo.FinanceAuditStatus = 4;
                    financeDetailInfo.Salary = "综合以上意见后工资结算正常。";
                }
                else
                {
                    int branchId = 0;
                    int.TryParse(drpBranchList.SelectedValue, out branchId);
                    if (branchId != 0)
                    {
                        financeDetailInfo.Salary = "需补缴" + txtSalary.Text;
                        financeDetailInfo.SalaryBranch = branchId;
                    }
                }
            }

            ESP.HumanResource.Entity.HRAuditLogInfo newHRAuditLogInfo = new ESP.HumanResource.Entity.HRAuditLogInfo();

            // 集团行政审批日志
            newHRAuditLogInfo.AuditorId = manageModel.DimissionADAuditorId;
            newHRAuditLogInfo.AuditorName = manageModel.DimissionADAuditorName;
            newHRAuditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
            newHRAuditLogInfo.FormId = dimissionFormInfo.DimissionId;
            newHRAuditLogInfo.FormType = (int)ESP.HumanResource.Common.HRFormType.DimissionForm;
            newHRAuditLogInfo.AuditLevel = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitAdministration;
            ESP.HumanResource.Entity.UsersInfo adModel = ESP.HumanResource.BusinessLogic.UsersManager.GetModel(manageModel.DimissionADAuditorId);

            mailAddressList.Add(new MailAddress(adModel.Email));


            newAuditLogList.Add(newHRAuditLogInfo);

            dimissionFormInfo.FinanceAuditStatus = 6;

            dimissionFormInfo.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHR2;
            financeDetailInfo.DirectorId = UserID;
            financeDetailInfo.DirectorName = UserInfo.FullNameCN;

            empBaseInfo.DimissionStatus = ESP.HumanResource.Common.Status.DimissionReceiving;
            financeDetailInfo.Other = txtOther.Text;

        }


        // 原审批记录信息
        ESP.HumanResource.Entity.HRAuditLogInfo oldHRauditLogInfo = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(UserID,
            dimissionFormInfo.DimissionId, (int)ESP.HumanResource.Common.HRFormType.DimissionForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);
        if (oldHRauditLogInfo == null && backUpUserId != 0)
        {
            oldHRauditLogInfo = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(backUpUserId,
                dimissionFormInfo.DimissionId, (int)ESP.HumanResource.Common.HRFormType.DimissionForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);
        }
        oldHRauditLogInfo.AuditDate = DateTime.Now;
        oldHRauditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Audited;
        oldHRauditLogInfo.Requesition = txtRes.Text.Trim();

        bool bl = ESP.HumanResource.BusinessLogic.DimissionFormManager.Update(dimissionFormInfo, oldHRauditLogInfo, newAuditLogList, financeDetailInfo, empBaseInfo);

        if (!bl)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('系统出现错误，请与系统管理员联系。');", true);
            return;
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('离职申请单审批通过。');window.location='DimissionAuditList.aspx';", true);


            if (newAuditLogList != null && newAuditLogList.Count > 0)
            {
                foreach (ESP.HumanResource.Entity.HRAuditLogInfo loginfo in newAuditLogList)
                {
                    ESP.Framework.Entity.UserInfo userInfo = ESP.Framework.BusinessLogic.UserManager.Get(loginfo.AuditorId);
                    if (userInfo != null && !string.IsNullOrEmpty(userInfo.Email) && userInfo.Status == 1)
                        mailAddressList.Add(new MailAddress(userInfo.Email));
                }
            }
            if (mailAddressList != null && mailAddressList.Count > 0)
            {
                try
                {
                    string url = "http://" + Request.Url.Authority + "/HR/Print/NewDimissionMail.aspx?dimissionId=" + dimissionFormInfo.DimissionId + "&type=1";
                    string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                    ESP.Mail.MailManager.Send("离职管理", body, true, mailAddressList.ToArray());
                }
                catch
                { }
            }
            return;
        }
    }

    /// <summary>
    /// 审批驳回
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOverruleFinance_Click(object sender, EventArgs e)
    {
        int dimissionFormId = 0;
        if (!int.TryParse(hidDimissionFormID.Value, out dimissionFormId))
            dimissionFormId = 0;

        // 判断该离职单是否是处于当前登录人审批
        bool b = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.CheckDimissionData(UserID, dimissionFormId);
        if (!b)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('你没有权限操作此离职单！');", true);
            return;
        }

        // 离职单信息
        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionFormId);
        dimissionFormInfo.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.Overrule;
        if (!string.IsNullOrEmpty(txtdimissionDate.Text))
            dimissionFormInfo.LastDay = DateTime.Parse(txtdimissionDate.Text);

        // 审批日志
        ESP.HumanResource.Entity.HRAuditLogInfo hrauditLogInfo = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(UserID,
            dimissionFormInfo.DimissionId, (int)ESP.HumanResource.Common.HRFormType.DimissionForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);
        hrauditLogInfo.AuditDate = DateTime.Now;
        hrauditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Overrule;
        hrauditLogInfo.Requesition = txtRes.Text.Trim();

        bool bl = ESP.HumanResource.BusinessLogic.DimissionFormManager.DimissionOverrule(dimissionFormInfo, hrauditLogInfo);
        if (!bl)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('系统出现错误，请与系统管理员联系。');", true);
            return;
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('离职申请单审批驳回成功。');window.location='DimissionAuditList.aspx';", true);
            return;
        }
    }
    #endregion

    #region 集团行政审批
    protected void SetADInfo(int dimissionId)
    {
        ESP.HumanResource.Entity.DimissionADDetailsInfo adDetailInfo =
            ESP.HumanResource.BusinessLogic.DimissionADDetailsManager.GetADDetailInfo(dimissionid);
        if (adDetailInfo != null)
        {
            txtDoorCard.Text = adDetailInfo.DoorCard;
            //txtLibraryManage.Text = adDetailInfo.LibraryManage;
        }
    }

    /// <summary>
    /// 审批通过
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPassAD_Click(object sender, EventArgs e)
    {
        int dimissionFormId = 0;
        if (!int.TryParse(hidDimissionFormID.Value, out dimissionFormId))
            dimissionFormId = 0;

        CollectSelected();

        // 判断该离职单是否是处于当前登录人审批
        bool b = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.CheckDimissionData(UserID, dimissionFormId);
        if (!b)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('你没有权限操作此离职单！');", true);
            return;
        }


        // 离职单信息
        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionFormId);
        dimissionFormInfo.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHR2;
        if (!string.IsNullOrEmpty(txtdimissionDate2.Text))
            dimissionFormInfo.LastDay = DateTime.Parse(txtdimissionDate2.Text);  // 确认最后离职日期。

        ESP.HumanResource.Entity.HRAuditLogInfo newHRAuditLogInfo = new ESP.HumanResource.Entity.HRAuditLogInfo();
        // 获得离职员工所在部门的审批信息
        //变更组织机构需要调整 
        newHRAuditLogInfo.AuditorId = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetUserHRDepartment(dimissionFormInfo.DepartmentId);
        newHRAuditLogInfo.AuditorName = "人力资源部";
        newHRAuditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
        newHRAuditLogInfo.FormId = dimissionFormInfo.DimissionId;
        newHRAuditLogInfo.FormType = (int)ESP.HumanResource.Common.HRFormType.DimissionForm;
        newHRAuditLogInfo.AuditLevel = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitHR2;

        ESP.HumanResource.Entity.HRAuditLogInfo oldHRauditLogInfo = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(UserID,
            dimissionFormInfo.DimissionId, (int)ESP.HumanResource.Common.HRFormType.DimissionForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);
        oldHRauditLogInfo.AuditDate = DateTime.Now;
        oldHRauditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Audited;
        oldHRauditLogInfo.Requesition = txtRes.Text.Trim();

        ESP.HumanResource.Entity.DimissionADDetailsInfo adDetailInfo = new ESP.HumanResource.Entity.DimissionADDetailsInfo();
        adDetailInfo.DimissionId = dimissionFormInfo.DimissionId;
        adDetailInfo.DoorCard = txtDoorCard.Text;
        //adDetailInfo.LibraryManage = txtLibraryManage.Text;
        adDetailInfo.PrincipalID = UserID;
        adDetailInfo.PrincipalName = UserInfo.FullNameCN;

        ESP.HumanResource.Entity.DimissionHRDetailsInfo hrDetailInfo = null;
        ESP.HumanResource.Entity.EmployeeBaseInfo employeeBase = null;
        ESP.HumanResource.Entity.DimissionInfo dimissionModel = null;


        bool bl = ESP.HumanResource.BusinessLogic.DimissionFormManager.Update(dimissionFormInfo, oldHRauditLogInfo,
            newHRAuditLogInfo, null, null, hrDetailInfo, employeeBase, dimissionModel, adDetailInfo);
        //bool bl = ESP.HumanResource.BusinessLogic.DimissionFormManager.Update(dimissionFormInfo, oldHRauditLogInfo,
        //    newHRAuditLogInfo, adDetailInfo);

        if (!bl)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('系统出现错误，请与系统管理员联系。');", true);
            return;
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('离职申请单审批通过。');window.location='DimissionAuditList.aspx';", true);



            List<MailAddress> mailAddressList = new List<MailAddress>();
            IList<ESP.Framework.Entity.EmployeeInfo> hrEmployeeList = ESP.Framework.BusinessLogic.EmployeeManager.GetEmployeesByDepartment(ESP.HumanResource.BusinessLogic.DimissionFormManager.GetUserHRDepartment(dimissionFormInfo.DepartmentId));
            if (hrEmployeeList != null && hrEmployeeList.Count > 0)
            {
                foreach (ESP.Framework.Entity.EmployeeInfo emp in hrEmployeeList)
                {
                    ESP.Framework.Entity.UserInfo userInfo = ESP.Framework.BusinessLogic.UserManager.Get(emp.UserID);
                    if (userInfo != null && !string.IsNullOrEmpty(userInfo.Email) && userInfo.Status == 1)
                        mailAddressList.Add(new MailAddress(userInfo.Email));
                }
            }
            if (mailAddressList != null && mailAddressList.Count > 0)
            {
                try
                {
                    string url = "http://" + Request.Url.Authority + "/HR/Print/NewDimissionMail.aspx?dimissionId=" + dimissionFormInfo.DimissionId + "&type=1";
                    string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                    ESP.Mail.MailManager.Send("离职管理", body, true, mailAddressList.ToArray());
                }
                catch
                { }
            }
            //ESP.Framework.Entity.UserInfo dimissionUserInfo = ESP.Framework.BusinessLogic.UserManager.Get(dimissionFormInfo.UserId);
            //ESP.Mail.MailManager.Send("离职管理--集团行政审批通过", dimissionUserInfo.FullNameCN + "您好，您的离职申请集团行政已审批通过，等待下一级审批。", false, new MailAddress[] { new MailAddress(ESP.Framework.BusinessLogic.UserManager.Get(dimissionFormInfo.UserId).Email) });
        }
        return;

    }

    /// <summary>
    /// 审批驳回
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOverruleAD_Click(object sender, EventArgs e)
    {
        int dimissionFormId = 0;
        if (!int.TryParse(hidDimissionFormID.Value, out dimissionFormId))
            dimissionFormId = 0;

        // 判断该离职单是否是处于当前登录人审批
        bool b = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.CheckDimissionData(UserID, dimissionFormId);
        if (!b)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('你没有权限操作此离职单！');", true);
            return;
        }

        // 离职单信息
        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionFormId);
        dimissionFormInfo.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.Overrule;
        if (!string.IsNullOrEmpty(txtdimissionDate.Text))
            dimissionFormInfo.LastDay = DateTime.Parse(txtdimissionDate.Text);

        // 审批日志
        ESP.HumanResource.Entity.HRAuditLogInfo hrauditLogInfo = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(UserID,
            dimissionFormInfo.DimissionId, (int)ESP.HumanResource.Common.HRFormType.DimissionForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);
        hrauditLogInfo.AuditDate = DateTime.Now;
        hrauditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Overrule;
        hrauditLogInfo.Requesition = txtRes.Text.Trim();

        bool bl = ESP.HumanResource.BusinessLogic.DimissionFormManager.DimissionOverrule(dimissionFormInfo, hrauditLogInfo);
        if (!bl)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('系统出现错误，请与系统管理员联系。');", true);
            return;
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('离职申请单审批驳回成功。');window.location='DimissionAuditList.aspx';", true);
            return;
        }
    }
    #endregion

    #region 集团人力总监审批
    protected void SetDimissionIndemnity(int dimissionId, decimal totalIndemnityAmountAudited)
    {
        List<ESP.HumanResource.Entity.DimissionIndemnityInfo> dimissionIndemnityList = DimissionIndemnityManager.GetDimissionIndemnityInfo(dimissionId);
        gvIndemnity.DataSource = dimissionIndemnityList;
        gvIndemnity.DataBind();

        decimal totalIndemnityAmount = 0;
        foreach (ESP.HumanResource.Entity.DimissionIndemnityInfo indemnityInfo in dimissionIndemnityList)
        {
            totalIndemnityAmount += indemnityInfo.IndemnityAmount;
        }
        txtTotalIndemnityAmount.Text = totalIndemnityAmount.ToString();  // 赔偿总金额
        txtTotalIndemnityAmountFinance.Text = totalIndemnityAmountAudited.ToString();
    }

    protected void btnHRDirAuditPass_Click(object sender, EventArgs e)
    {
        int dimissionFormId = 0;
        if (!int.TryParse(hidDimissionFormID.Value, out dimissionFormId))
            dimissionFormId = 0;

        CollectSelected();


        // 判断该离职单是否是处于当前登录人审批
        bool b = ESP.HumanResource.BusinessLogic.DimissionDetailsManager.CheckDimissionData(UserID, dimissionFormId);
        if (!b)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('你没有权限操作此离职单！');", true);
            return;
        }

        ESP.HumanResource.Entity.DimissionFormInfo dimissionModel = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionFormId);

        // 离职单信息
        List<ESP.HumanResource.Entity.HRAuditLogInfo> newHRAuditLogList = new List<ESP.HumanResource.Entity.HRAuditLogInfo>();
        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionFormId);

        ESP.Framework.Entity.DepartmentInfo departmentInfo =
     new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager().GetRootDepartmentID(dimissionFormInfo.UserId);

        ESP.HumanResource.Entity.DimissionFinanceDetailsInfo financeDetailInfo =
ESP.HumanResource.BusinessLogic.DimissionFinanceDetailsManager.GetFinanceDetailInfo(dimissionFormId);
        if (financeDetailInfo == null)
            financeDetailInfo = new ESP.HumanResource.Entity.DimissionFinanceDetailsInfo();

        dimissionFormInfo.Status = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitFinance;
        dimissionFormInfo.ITAuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Audited;

        string salaryUsers = ESP.Finance.BusinessLogic.BranchManager.GetSalaryUsers(departmentInfo.DepartmentID);

        string[] salaryUsersArr = salaryUsers.Split(',');
        foreach (string str in salaryUsersArr)
        {
            int salaryUserId = 0;
            if (int.TryParse(str, out salaryUserId))
            {
                ESP.Framework.Entity.UserInfo userModel = ESP.Framework.BusinessLogic.UserManager.Get(salaryUserId);
                if (userModel != null)
                {
                    ESP.HumanResource.Entity.HRAuditLogInfo newHRAuditLogInfo = new ESP.HumanResource.Entity.HRAuditLogInfo();
                    newHRAuditLogInfo.AuditorId = userModel.UserID;
                    newHRAuditLogInfo.AuditorName = userModel.FullNameCN;
                    newHRAuditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.NotAudit;
                    newHRAuditLogInfo.FormId = dimissionFormInfo.DimissionId;
                    newHRAuditLogInfo.FormType = (int)ESP.HumanResource.Common.HRFormType.DimissionForm;
                    newHRAuditLogInfo.AuditLevel = (int)ESP.HumanResource.Common.DimissionFormStatus.WaitFinance;
                    newHRAuditLogList.Add(newHRAuditLogInfo);
                }
            }
        }
        dimissionFormInfo.FinanceAuditStatus = 3;
        financeDetailInfo.BusinessCard = "无商务卡";


        // 更新审批日志信息
        ESP.HumanResource.Entity.HRAuditLogInfo oldHRauditLogInfo = ESP.HumanResource.BusinessLogic.HRAuditLogManager.GetAuditLogInfo(UserID,
            dimissionFormInfo.DimissionId, (int)ESP.HumanResource.Common.HRFormType.DimissionForm, (int)ESP.HumanResource.Common.AuditStatus.NotAudit);
        oldHRauditLogInfo.AuditDate = DateTime.Now;
        oldHRauditLogInfo.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Audited;
        oldHRauditLogInfo.Requesition = txtRes.Text.Trim();
        oldHRauditLogInfo.AuditorId = UserID;
        oldHRauditLogInfo.AuditorName = UserInfo.FullNameCN;

        bool bl = DimissionFormManager.Update(dimissionFormInfo, oldHRauditLogInfo, newHRAuditLogList);
        if (!bl)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('系统出现错误，请与系统管理员联系。');", true);
            return;
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
                "alert('离职申请单审批通过。');window.location='DimissionAuditList.aspx';", true);

            List<MailAddress> mailAddressList = new List<MailAddress>();
            if (newHRAuditLogList != null && newHRAuditLogList.Count > 0)
            {
                ESP.Administrative.BusinessLogic.UserAttBasicInfoManager userAtt = new ESP.Administrative.BusinessLogic.UserAttBasicInfoManager();

                ESP.Framework.Entity.DepartmentInfo companyDep = userAtt.GetRootDepartmentID(dimissionFormInfo.UserId);

                List<ESP.HumanResource.Entity.UsersInfo> list = UsersManager.GetUserList(companyDep.DepartmentID, ESP.HumanResource.Common.Status.DimissionSendMail);

                if (list != null && list.Count > 0)
                {
                    foreach (ESP.HumanResource.Entity.UsersInfo userModel in list)
                    {
                        mailAddressList.Add(new MailAddress(userModel.Email));
                    }
                }
                //加HR行政IT邮件
            }
            if (mailAddressList != null && mailAddressList.Count > 0)
            {
                try
                {
                    string url = "http://" + Request.Url.Authority + "/HR/Print/NewDimissionMail.aspx?dimissionId=" + dimissionFormInfo.DimissionId + "&type=1";
                    string body = ESP.HumanResource.Common.SendMailHelper.ScreenScrapeHtml(url);
                    ESP.Mail.MailManager.Send("离职管理", body, true, mailAddressList.ToArray());
                }
                catch
                { }
            }
            //ESP.Framework.Entity.UserInfo dimissionUserInfo = ESP.Framework.BusinessLogic.UserManager.Get(dimissionFormInfo.UserId);
            //ESP.Mail.MailManager.Send("离职管理--集团人力总监审批通过", dimissionUserInfo.FullNameCN + "您好，您的离职申请集团人力总监已审批通过，等待下一级审批。", false, new MailAddress[] { new MailAddress(ESP.Framework.BusinessLogic.UserManager.Get(dimissionFormInfo.UserId).Email) });
            return;
        }

    }
    #endregion

    protected void btnTip_Click(object sender, EventArgs e)
    {
        ESP.HumanResource.Entity.DimissionFormInfo dimissionFormInfo = ESP.HumanResource.BusinessLogic.DimissionFormManager.GetModel(dimissionid);

        ESP.HumanResource.Entity.HRAuditLogInfo tipModel = new ESP.HumanResource.Entity.HRAuditLogInfo();
        tipModel.AuditDate = DateTime.Now;
        tipModel.AuditStatus = (int)ESP.HumanResource.Common.AuditStatus.Tip;
        tipModel.Requesition = txtRes.Text.Trim();
        tipModel.AuditorId = CurrentUserID;
        tipModel.AuditorName = CurrentUser.Name;
        tipModel.FormId = dimissionFormInfo.DimissionId;
        tipModel.FormType = (int)ESP.HumanResource.Common.HRFormType.DimissionForm;
        tipModel.AuditLevel = (int)ESP.HumanResource.Common.DimissionFormStatus.DimissionTip;
        int ret = ESP.HumanResource.BusinessLogic.HRAuditLogManager.Add(tipModel);

        if (ret > 0)
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(),
              "alert('留言保存成功！');window.location='DimissionAuditList.aspx';", true);
    }


}