using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using System.Data.SqlClient;
using System.Data;

public partial class project_ProjectStep2 : ESP.Finance.WebPage.EditPageForProject
{
    int projectid=0;
    int customerid = 0;
    string query=string.Empty;
    ESP.Finance.Entity.ProjectInfo projectinfo;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Server.ScriptTimeout = 600;
        CustomerInfo.CurrentUser = this.CurrentUser;
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            {
                projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
                if (projectinfo.isRecharge)
                {
                    HiddenField hidRebateRate = (HiddenField)CustomerInfo.FindControl("hidRebateRate");
                    hidRebateRate.Value = projectinfo.CustomerRebateRate.ToString();
                }
                if (projectinfo.Status != (int)ESP.Finance.Utility.Status.Saved && projectinfo.Status != (int)ESP.Finance.Utility.Status.BizReject && projectinfo.Status != (int)ESP.Finance.Utility.Status.FinanceReject && projectinfo.Status != (int)ESP.Finance.Utility.Status.ContractReject)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('你没有访问该数据的权限！');window.location.href='Default.aspx';", true);
                }
              
            }
        }
        query = Request.Url.Query;
    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
            Response.Redirect("ProjectStep11.aspx" + query);      
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Edit/ProjectTabEdit.aspx" + query);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        this.btnNext.Enabled = false   ;
        if (saveProjectInfo() > 0)
        {
            ESP.Logging.Logger.Add(string.Format("{0}对F_Project表中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, Request[RequestName.ProjectID], "创建项目号申请单第3步保存并返回列表页"), "创建项目号申请单");
           
             Response.Redirect("ProjectStep3.aspx" + query);
        }
        this.btnNext.Enabled = true;
    }

    private int SaveCustomerInfo()
    {
        UpdateResult result;
        if (projectinfo == null)
        {
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            {
                projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                projectinfo = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);

            }
        }
        CustomerInfo.setCustomerInfo();

        //客户代码、名称判重
        if (CustomerInfo.CustomerModel.CustomerID == 0)
        {
            string term = "  (CustomerCode=@CustomerCode or ShortEN=@ShortEN) and CustomerID!=@CustomerID ";// 
            List<SqlParameter> existsParam = new List<SqlParameter>();
            existsParam.Add(new SqlParameter("@CustomerCode", CustomerInfo.CustomerModel.CustomerCode));
            existsParam.Add(new SqlParameter("@ShortEN", CustomerInfo.CustomerModel.ShortEN));
            existsParam.Add(new SqlParameter("@CustomerID", CustomerInfo.CustomerModel.CustomerID));
            if (ESP.Finance.BusinessLogic.CustomerManager.Exists(term, existsParam))
            {
                ClientScript.RegisterClientScriptBlock(typeof(string), "", "javascript:alert('客户英文简称或客户名称与客户库中存在重复，请搜索客户或更改客户信息！');", true);
                return -1;
            }
        }

        if (projectinfo.Customer == null)
        {
           // IList<ESP.Finance.Entity.CustomerInfo> customerList = ESP.Finance.BusinessLogic.CustomerManager.GetList(" shortEN='"+CustomerInfo.CustomerModel.ShortEN+"'");
            customerid= ESP.Finance.BusinessLogic.CustomerTmpManager.Add(CustomerInfo.CustomerModel);
        }
        else
        {
            result = ESP.Finance.BusinessLogic.CustomerTmpManager.Update(CustomerInfo.CustomerModel);
            if (result == UpdateResult.Succeed)
            {
                customerid= CustomerInfo.CustomerModel.CustomerTmpID;
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + result.ToString() + "');", true);
                return -1;
            }
        }

        return customerid;
    }

    private int saveProjectInfo()
    {
        customerid = SaveCustomerInfo();
        if (customerid == -1)
            return -1;
        projectinfo.CustomerID = Convert.ToInt32(customerid);
        projectinfo.CustomerCode = CustomerInfo.CustomerModel.ShortEN;
        projectinfo.CustomerAttachID = CustomerInfo.CustomerAttachID;
        projectinfo.SubmitDate = DateTime.Now;
        if (projectinfo.isRecharge)
        {
            HiddenField hidRebateRate = (HiddenField)CustomerInfo.FindControl("hidRebateRate");
            projectinfo.CustomerRebateRate = decimal.Parse(string.IsNullOrEmpty(hidRebateRate.Value) ? "0" : hidRebateRate.Value);
        }
        if (projectinfo.Step < 3)
            projectinfo.Step = 3;
        UpdateResult result = ESP.Finance.BusinessLogic.ProjectManager.Update(projectinfo);
        if (result == UpdateResult.Succeed)
        {
            return 1;
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + result.ToString() + "');", true);
            return -1;
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (saveProjectInfo() > 0)
        {

            Response.Redirect("ProjectStep2.aspx" + query);
        }
    }
}
