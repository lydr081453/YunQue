using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Entity;

public partial class Purchase_PolicyFlow_PolicyFlowEdit : ESP.Web.UI.PageBase
{
    int policyFlowId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["policyFlowId"]))
        {
            policyFlowId = int.Parse(Request["policyFlowId"]);
        }
        if (!IsPostBack)
        {
            InitPage();
        }
    }

    private void InitPage()
    {
        if (policyFlowId > 0)
        {
            PolicyFlowInfo model = PolicyFlowManager.GetModel(policyFlowId);
            txtTitle.Text = model.title;
            txtSynopsis.Text = model.synopsis;
            labUpload.Text = model.contents == "" ? "" : "<a target='_blank' href='../../" + model.contents + "'><img src='/images/ico_04.gif' border='0' /></a>";
            if (model.contents == "")
                chkContents.Visible = false;
        }
        else
        {
            chkContents.Visible = false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        PolicyFlowInfo model = new PolicyFlowInfo();
        if (policyFlowId > 0)
            model = PolicyFlowManager.GetModel(policyFlowId);
        model.title = txtTitle.Text.Trim();
        model.synopsis = txtSynopsis.Text.Trim();
        if (chkContents.Checked)
            model.contents = "";
        if (null != filContents.PostedFile && filContents.PostedFile.FileName != "")
        {
            string fileName = string.IsNullOrEmpty(model.contents) ? ("PolicyFlow_" + DateTime.Now.Ticks.ToString()) : model.contents.Split('\\')[1].ToString().Split('.')[0].ToString();
            model.contents = ESP.Purchase.Common.FileHelper.upFile(fileName, ESP.Purchase.Common.ServiceURL.UpFilePath, filContents);
        }

        int returnValue = 0;
        if (policyFlowId > 0)
        {
            returnValue = PolicyFlowManager.Update(model);

            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对业务流程中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, policyFlowId.ToString(), "编辑保存"), "业务流程列表");
        }
        else
        {
            returnValue = PolicyFlowManager.Add(model);

            //记录操作日志
            ESP.Logging.Logger.Add(string.Format("{0}对业务流程中的id={1}的数据完成 {2} 的操作", CurrentUser.Name, returnValue.ToString(), "新建保存"), "业务流程列表");
        }
        if (returnValue > 0)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('政策流程保存成功！');window.location.href='PolicyFlowEditList.aspx';", true);
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('政策流程保存失败！');", true);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PolicyFlowEditList.aspx");
    }
}
