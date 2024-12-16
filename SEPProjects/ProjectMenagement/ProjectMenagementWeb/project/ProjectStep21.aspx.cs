using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;

public partial class project_ProjectStep21 : ESP.Finance.WebPage.EditPageForProject
{
    string query = string.Empty;
    int projectid = 0;
    ESP.Finance.Entity.ProjectInfo projectinfo;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Server.ScriptTimeout = 600;
        query = Request.Url.Query;
        PaymentInfo.CurrentUser = CurrentUser;
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            {
                projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
                ESP.Finance.Entity.TaxRateInfo rateModel = null;
                if (projectinfo.ContractTaxID != null && projectinfo.ContractTaxID != 0)
                    rateModel = ESP.Finance.BusinessLogic.TaxRateManager.GetModel(projectinfo.ContractTaxID.Value);

                //ViewState["ProjectModel"] = projectinfo;
                if (projectinfo.Status != (int)ESP.Finance.Utility.Status.Saved && projectinfo.Status != (int)ESP.Finance.Utility.Status.BizReject && projectinfo.Status != (int)ESP.Finance.Utility.Status.FinanceReject && projectinfo.Status != (int)ESP.Finance.Utility.Status.ContractReject)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('你没有访问该数据的权限！');window.location.href='Default.aspx';", true);
                }
                this.PaymentInfo.ProjectInfo = projectinfo;
                this.PaymentInfo.InitProjectInfo();

                if (isViewPreButton())
                {
                    btnViewPre.Visible = true;
                    string script = @"var win = window.open('ProjectHist.aspx?" + ESP.Finance.Utility.RequestName.ProjectID + "=" + projectid + @"', null, 'height=600px,width=800px,scrollbars=yes,top=100px,left=100px');
                                      win.resizeTo(screen.availWidth * 0.8, screen.availHeight * 0.8);";
                    btnViewPre.Attributes["onclick"] = "javascript:" + script;

                    if (!ProjectManager.isNeedAudit(projectid))
                        btnSave2.Text = " 提交 ";
                }
                //40%
                //decimal taxfee = 0;
                //decimal servicefee = 0;
                //decimal profilerate = 0;
                //if (projectinfo.ContractTax != null)
                //{
                //    if (projectinfo.IsCalculateByVAT == 1)
                //        taxfee = ESP.Finance.BusinessLogic.CheckerManager.GetTaxByVAT(projectinfo, rateModel);
                //    else
                //        taxfee = ESP.Finance.BusinessLogic.CheckerManager.GetTax(projectinfo, rateModel);
                //}
                //if (projectinfo.IsCalculateByVAT == 1)
                //    servicefee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFeeByVAT(projectinfo, rateModel);
                //else
                //    servicefee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFee(projectinfo, rateModel);
                //if (projectinfo.TotalAmount > 0)
                //{
                //    profilerate = (servicefee / Convert.ToDecimal(projectinfo.TotalAmount) * 100);
                //}
                //if (profilerate < 40 && projectinfo.ContractStatusName != ESP.Finance.Utility.ProjectType.BDProject)
                //{
                //    this.lblTip.Text = "项目毛利率为" + profilerate.ToString("#,##0.00") + "%，低于40%，请说明立项原因。";
                //    this.txtReason.Text = projectinfo.ProfileReason;
                //}
                //else
                //{
                //    this.tabReason.Visible = false;
                //}
            }
        }
    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProjectStep5.aspx" + query);
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Edit/ProjectTabEdit.aspx" + query);
    }

    private bool isViewPreButton()
    {
        List<SqlParameter> paramList = new List<SqlParameter>();
        int ProjectID = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
        string term = " ProjectID=@ProjectID and Status=@Status";
        System.Data.SqlClient.SqlParameter p2 = new System.Data.SqlClient.SqlParameter("@ProjectID", System.Data.SqlDbType.Int, 4);
        p2.SqlValue = ProjectID;
        paramList.Add(p2);
        System.Data.SqlClient.SqlParameter p1 = new System.Data.SqlClient.SqlParameter("@Status", System.Data.SqlDbType.Int, 4);
        p1.SqlValue = (int)ESP.Finance.Utility.Status.FinanceAuditComplete;
        paramList.Add(p1);
        IList<ProjectHistInfo> projectList = ESP.Finance.BusinessLogic.ProjectHistManager.GetList(term, paramList);
        if (projectList != null && projectList.Count > 0)
            return true;
        else
            return false;
    }

    private int SaveProjectInfo()
    {
        decimal totalPercent = 0;
        decimal totalFee = 0;
        List<SqlParameter> paramlist = new List<SqlParameter>();
        if (projectinfo == null)
        {
            projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
            projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModel(projectid);
        }
        if (projectinfo.Status == (int)ESP.Finance.Utility.Status.Submit)
        {
            ClientScript.RegisterClientScriptBlock(typeof(string), "", "alert('该项目号已经提交完成!');", true);
            Response.Redirect("ProjectCommitList.aspx");
        }
        if (projectinfo.ContractStatusName != ProjectType.BDProject && projectinfo.CustomerCode.ToLower() != ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN.ToLower())
        {
            IList<PaymentInfo> listPayment = ESP.Finance.BusinessLogic.PaymentManager.GetList(" ProjectID = " + int.Parse(Request[ESP.Finance.Utility.RequestName.ProjectID]));
            decimal totalPay = listPayment.Sum(x=>x.PaymentBudget).Value;

            if (!projectinfo.isRecharge)
            {
                if (Convert.ToDecimal(projectinfo.TotalAmount) != totalPay)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('付款通知总额与项目合同总金额不符');", true);
                    return -1;
                }
            }
            else
            {
                if (Convert.ToDecimal(projectinfo.AccountsReceivable) != totalPay)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('付款通知总额与应收金额不符');", true);
                    return -1;
                }
            }

        }

        if (projectinfo.ContractStatusName != ProjectType.BDProject)
        {
            string condition = " projectID =@projectID AND usable=1";
            paramlist.Clear();
            paramlist.Add(new System.Data.SqlClient.SqlParameter("@projectID", projectinfo.ProjectId.ToString()));

            IList<ContractInfo> listcontract = ESP.Finance.BusinessLogic.ContractManager.GetList(condition, paramlist);
            if (listcontract == null || listcontract.Count == 0)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('请完整添加合同信息!');", true);
                return -1;
            }
        }

        foreach (ProjectScheduleInfo model in projectinfo.ProjectSchedules)
        {
            totalPercent += model.MonthPercent.Value;
            totalFee += model.Fee == null ? 0 : model.Fee.Value;
        }

        decimal servicefee = 0;
        if (projectinfo.IsCalculateByVAT == 1)
            servicefee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFeeByVAT(projectinfo, null);
        else
            servicefee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFee(projectinfo, null);

        if (totalFee != servicefee)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('预计完工百分比输入有误.');", true);
            return -1;
        }
        projectinfo.PayCycle = StringHelper.SubString(this.PaymentInfo.PayCycle, 500);
        projectinfo.IsNeedInvoice = this.PaymentInfo.Is3rdInvoice == true ? 1 : 0;
        projectinfo.CustomerRemark = StringHelper.SubString(this.PaymentInfo.CustomerRemark, 200);
        projectinfo.SubmitDate = DateTime.Now;
        if (projectinfo.Step < 7)
            projectinfo.Step = 7;
        //if (this.tabReason.Visible == true)
        //{
        //    if (string.IsNullOrEmpty(this.txtReason.Text))
        //    {
        //        ClientScript.RegisterStartupScript(typeof(string), "", "alert('请填写立项原因!');", true);
        //        return -1;
        //    }
        //    projectinfo.ProfileReason = this.txtReason.Text;
        //}
        UpdateResult result = ESP.Finance.BusinessLogic.ProjectManager.Update(projectinfo);
        if (result == UpdateResult.Succeed)
        {
            if (projectinfo.CreatorID != projectinfo.ApplicantUserID)
            {
                ESP.Finance.Entity.AuditHistoryInfo responser = new ESP.Finance.Entity.AuditHistoryInfo();
                responser.AuditorUserID = projectinfo.ApplicantUserID;
                responser.AuditorUserCode = projectinfo.ApplicantCode;
                responser.AuditorEmployeeName = projectinfo.ApplicantEmployeeName;
                responser.AuditorUserName = projectinfo.ApplicantUserName;
                responser.AuditStatus = (int)AuditHistoryStatus.UnAuditing;
                responser.AuditType = auditorType.operationAudit_Type_YS;
                responser.ProjectID = projectinfo.ProjectId;
                string term = " ProjectID=@ProjectID and AuditorUserID=@AuditorUserID and AuditStatus=@AuditStatus ";
                paramlist.Clear();
                SqlParameter p1 = new SqlParameter("@ProjectID", SqlDbType.Int, 4);
                p1.Value = projectinfo.ProjectId;
                paramlist.Add(p1);
                SqlParameter p2 = new SqlParameter("@AuditorUserID", SqlDbType.Int, 4);
                p2.Value = projectinfo.ApplicantUserID;
                paramlist.Add(p2);
                SqlParameter p3 = new SqlParameter("@AuditStatus", SqlDbType.Int, 4);
                p3.Value = (int)AuditHistoryStatus.UnAuditing;
                paramlist.Add(p3);
                if (ESP.Finance.BusinessLogic.AuditHistoryManager.GetList(term, paramlist).Count <= 0)
                    ESP.Finance.BusinessLogic.AuditHistoryManager.Add(responser);
            }
            return 1;
        }
        else if (result == UpdateResult.Iterative)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('项目名称有重复!');", true);
            return -1;
        }
        else if (result == UpdateResult.AmountOverflow)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('项目内的金额计算有误!');", true);
            return -1;
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + result.ToString() + "');", true);
            return -1;
        }
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        this.btnNext.Enabled = false;
        if (SaveProjectInfo() == 1)
        {
            Response.Redirect("ProjectStep21.aspx" + query);
        }
        this.btnNext.Enabled = true;
    }


    protected void btnSave2_Click(object sender, EventArgs e)
    {
        if (SaveProjectInfo() == 1)
        {
            if (!ProjectManager.isNeedAudit(projectid))
            {
                if (ProjectManager.CommitChangedProject(projectid, CurrentUser) == UpdateResult.Succeed)
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('提交成功！');window.location.href='projectList.aspx';", true);
                else
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('提交失败！');", true);
            }
            else
            {
                query = Request.Url.Query;
                query = query.AddParam(RequestName.BackUrl, "ProjectStep21.aspx");
                Response.Redirect("NewSetAuditor.aspx?" + query);
            }
        }
    }
}
