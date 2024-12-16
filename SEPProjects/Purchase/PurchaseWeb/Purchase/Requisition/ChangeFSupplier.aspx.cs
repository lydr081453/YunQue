using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.Common;
using ESP.Purchase.BusinessLogic;
using ESP.Purchase.Entity;

public partial class Purchase_Requisition_ChangedFSupplier : ESP.Web.UI.PageBase
{
    int generalid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalid = int.Parse(Request[RequestName.GeneralID]);
        }
        if (!IsPostBack)
        {
            supplierInfo.FindControl("btn").Visible = false;
            supplierInfo.FindControl("trTitle").Visible = false;
            supplierInfo.FindControl("txtfa_no").Visible = false;
            GeneralInfo generalInfoModel = GeneralInfoManager.GetModel(generalid);
            supplierInfo.Model = generalInfoModel;
            supplierInfo.BindInfo();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DropDownList ddlsource = (DropDownList)supplierInfo.FindControl("ddlsource");
        Label labEmailFile = (Label)supplierInfo.FindControl("labEmailFile");
        if (ddlsource.SelectedValue == "客户指定" && labEmailFile.Text == "" && this.Request.Files[0].FileName == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请上传客户指定邮件附件！');", true);
            return;
        }

        GeneralInfo general = GeneralInfoManager.GetModel(generalid);
        supplierInfo.Model = general;
        supplierInfo.setModelInfo();
        GeneralInfoManager.Update(supplierInfo.Model);
        //记录操作日志
        ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, general.id, "编辑供应商"), "申请单");
        Response.Write("<script>opener.__doPostBack('"+Request["updateControl"].Replace('_','$')+"','');window.close();</script>");
    }
}
