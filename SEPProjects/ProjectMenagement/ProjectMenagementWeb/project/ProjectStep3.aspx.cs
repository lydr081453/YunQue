using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;

public partial class project_ProjectStep3 : ESP.Finance.WebPage.EditPageForProject
{
    string query = string.Empty;
    int projectid = 0;
    ESP.Finance.Entity.ProjectInfo projectmodel;

    protected void Page_Load(object sender, EventArgs e)
    {
        query = Request.Url.Query;
        this.ProjectInfo.CurrentUser = CurrentUser;
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ProjectID]))
            {
                projectid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ProjectID]);
                projectmodel = ESP.Finance.BusinessLogic.ProjectManager.GetModelWithOutDetailList(projectid);
                if (projectmodel.Status != (int)ESP.Finance.Utility.Status.Saved && projectmodel.Status != (int)ESP.Finance.Utility.Status.BizReject && projectmodel.Status != (int)ESP.Finance.Utility.Status.FinanceReject && projectmodel.Status != (int)ESP.Finance.Utility.Status.ContractReject)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('你没有访问该数据的权限！');window.location.href='Default.aspx';", true);
                }
            }
        }
    }
    protected void btnPre_Click(object sender, EventArgs e)
    {
            Response.Redirect("ProjectStep2.aspx"+query);
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Edit/ProjectTabEdit.aspx" + query);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        this.btnNext.Enabled = false   ;
        if (SaveProjectInfo() == 1)
        {
            ESP.Logging.Logger.Add(string.Format("{0}对F_Project表中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, Request[RequestName.ProjectID], "创建项目号申请单第5步保存并返回列表页"), "创建项目号申请单");
           
            Response.Redirect("ProjectStep4.aspx" + query);
        }
        this.btnNext.Enabled = true;
    }
    private int SaveProjectInfo()
    {
        projectmodel = this.ProjectInfo.GetProject();

        if (projectmodel == null)
            return -1;
        if (projectmodel.ContractStatusName != ProjectType.BDProject && projectmodel.CustomerCode.ToLower() != ESP.Finance.Configuration.ConfigurationManager.ShunYaShortEN.ToLower())
        {
            List<System.Data.SqlClient.SqlParameter> paramlist = new List<System.Data.SqlClient.SqlParameter>();
            string condition = " projectID =@projectID AND usable=1";
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
            //if (projectmodel.TotalAmount != null && Convert.ToDecimal(projectmodel.TotalAmount) != total)
            //{
            //    ClientScript.RegisterStartupScript(typeof(string), "", "alert('各合同明细金额累计与合同总金额不符!');", true);
            //    return 0;
            //}
            if (projectmodel.ContractStatusName != ESP.Finance.Utility.ProjectType.BDProject && (projectmodel.ContractTaxID == null || projectmodel.ContractTaxID.Value == 0))
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('请选择合同税率!');", true);
                return 0;
            }
        }
        if (projectmodel.Step < 5)
            projectmodel.Step = 5;
        projectmodel.SubmitDate = DateTime.Now;
        UpdateResult result;
        if (projectmodel.isRecharge)
        {
            decimal recharge = decimal.Parse(((TextBox)this.ProjectInfo.FindControl("txtRechargeAmount")).Text);
            result = ESP.Finance.BusinessLogic.ProjectManager.UpdateAndSaveRecharge(projectmodel, recharge);
        }
        else
        {
            result = ESP.Finance.BusinessLogic.ProjectManager.Update(projectmodel);
        }
      if (result == UpdateResult.Succeed)
      {
          return 1;
      }
      else
      {
          ClientScript.RegisterStartupScript(typeof(string), "", "alert('"+result.ToString()+"');", true);
          return -1;
      }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int result = SaveProjectInfo();
        if (result==1)
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据保存成功!');", true);
        }
        else if (result == -1)
        {
            return;
        }
        else
        {
            ClientScript.RegisterStartupScript(typeof(string), "", "alert('数据保存失败!');", true);
        }
    }
}
