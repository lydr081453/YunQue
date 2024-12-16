using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using ESP.Compatible;

public partial class project_ProjectEdit : ESP.Finance.WebPage.EditPageForProject
{
    string query = string.Empty;
    int projectid = 0;
    int customerid = 0;
    string bakurl = string.Empty;
    ESP.Finance.Entity.ProjectInfo projectmodel;

    private void SetUserControlsCurrentUser()
    {
        PrepareInfo.CurrentUser = CurrentUser;
        ProjectMember.CurrentUser = CurrentUser;
        CustomerInfo.CurrentUser = CurrentUser;
        PaymentInfo.CurrentUser = CurrentUser;
        ProjectInfo.CurrentUser = CurrentUser;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Server.ScriptTimeout = 600;
        query = Request.Url.Query;
        SetUserControlsCurrentUser();
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            {
                projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                projectmodel = ESP.Finance.BusinessLogic.ProjectManager.GetModel(projectid);
                TopMessage.ProjectModel = projectmodel;
                this.PaymentInfo.ProjectInfo = projectmodel;
                this.PaymentInfo.InitProjectInfo();
            }
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {

        if (!string.IsNullOrEmpty(Request[RequestName.Operate]))
        {
            switch (Request[RequestName.Operate])
            {
                case "FinancialAudit":
                    bakurl = "/project/FinancialAuditOperation.aspx";
                    break;
                case "BizAudit":
                    bakurl = "/project/AuditOperation.aspx";
                    break;
            }

        }

        Response.Redirect(bakurl + query);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        UpdateResult result;

        if (!string.IsNullOrEmpty(Request[RequestName.Operate]))
        {
            switch (Request[RequestName.Operate])
            {
                case "FinancialAudit":
                    bakurl = "/project/FinancialAuditOperation.aspx";
                    break;
                case "BizAudit":
                    bakurl = "/project/AuditOperation.aspx";
                    break;
            }

        }
        //更新准备信息
        this.PrepareInfo.setProjectModel();
        ESP.Finance.BusinessLogic.ProjectManager.Update(this.PrepareInfo.ProjectModel);

        projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
        projectmodel = ESP.Finance.BusinessLogic.ProjectManager.GetModel(projectid);

        //更新customer
        this.CustomerInfo.setCustomerInfo();
        if (projectmodel.Customer == null)
        {
            customerid = ESP.Finance.BusinessLogic.CustomerTmpManager.Add(CustomerInfo.CustomerModel);
        }
        else
        {
            result = ESP.Finance.BusinessLogic.CustomerTmpManager.Update(CustomerInfo.CustomerModel);
            if (result == UpdateResult.Succeed)
            {
                customerid = CustomerInfo.CustomerModel.CustomerTmpID;
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + result.ToString() + "');", true);
                return;
            }
        }
        projectmodel.CustomerID = customerid;
        //付款信息
        projectmodel.PayCycle = this.PaymentInfo.PayCycle;
        projectmodel.IsNeedInvoice = this.PaymentInfo.Is3rdInvoice == true ? 1 : 0;
        projectmodel = this.ProjectInfo.GetProject(projectmodel);
        ESP.Finance.BusinessLogic.ProjectManager.Update(projectmodel);

        //更新projectinfo

        if (SaveProjectInfo(projectmodel) == 1)
        {
            ESP.Logging.Logger.Add(string.Format("{0}对F_Project表中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, Request[RequestName.ProjectID], "编辑项目号申请单"), "编辑项目号申请单");
            //query = query.RemoveParam(RequestName.BackUrl);
            Response.Redirect(bakurl + query);
        }

    }

    private int SaveProjectInfo(ESP.Finance.Entity.ProjectInfo projectmodel)
    {
        decimal totalPercent = 0;
        decimal totalFee = 0;
        decimal totalPay = 0;
        //projectmodel = this.ProjectInfo.GetProject();
        if (projectmodel.ContractStatusName != ProjectType.BDProject)
        {
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
            string condition = " projectID =@projectID and usable=1";

            paramlist.Add(new System.Data.SqlClient.SqlParameter("@projectID", projectmodel.ProjectId.ToString()));

            IList<ContractInfo> listcontract = ESP.Finance.BusinessLogic.ContractManager.GetList(condition, paramlist);
            if (listcontract == null || listcontract.Count == 0)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('请完整添加合同信息!');", true);
                return 0;
            }
            decimal total = 0;
            foreach (ContractInfo contract in listcontract)
            {
                total += contract.TotalAmounts;
            }

            //付款通知
            IList<PaymentInfo> listPayment = ESP.Finance.BusinessLogic.PaymentManager.GetList(" ProjectID = " + projectmodel.ProjectId.ToString());

            foreach (PaymentInfo payment in listPayment)
            {
                totalPay += Convert.ToDecimal(payment.PaymentBudget);
            }
            if (!projectmodel.isRecharge)
            {
                if (projectmodel.TotalAmount.Value != totalPay)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('付款通知总额与项目合同总金额不符');", true);
                    return -1;
                }
            }
            else
            {
                if (Convert.ToDecimal(projectmodel.AccountsReceivable) != totalPay)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "", "alert('付款通知总额与应收金额不符');", true);
                    return -1;
                }
            }

            //预计完工百分比
            IList<ESP.Finance.Entity.ProjectScheduleInfo> ProjectSchedules = ESP.Finance.BusinessLogic.ProjectScheduleManager.GetList("ProjectID=" + projectmodel.ProjectId.ToString());

            foreach (ProjectScheduleInfo model in ProjectSchedules)
            {
                totalPercent += model.MonthPercent.Value;
                totalFee += model.Fee == null ? 0 : model.Fee.Value;
            }

            decimal servicefee = 0;
            if (projectmodel.IsCalculateByVAT == 1)
                servicefee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFeeByVAT(projectmodel, null);
            else
                servicefee = ESP.Finance.BusinessLogic.CheckerManager.GetServiceFee(projectmodel, null);


            if (totalFee != servicefee)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('预计完工百分比输入有误.');", true);
                return 0;
            }
        }
        projectmodel.CustomerAttachID = CustomerInfo.CustomerAttachID;
        UpdateResult result = ESP.Finance.BusinessLogic.ProjectManager.Update(projectmodel);
        if (result == UpdateResult.Succeed)
        {
            return 1;
        }
        else if (result == UpdateResult.Iterative)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('项目号有重复,请检查!');", true);
            return -2;
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + result.ToString() + "');", true);
            return -1;
        }
    }
}
