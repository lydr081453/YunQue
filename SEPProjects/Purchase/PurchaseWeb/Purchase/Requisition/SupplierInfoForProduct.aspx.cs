using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Purchase.Common;

public partial class Purchase_Requisition_SupplierInfoForProduct : ESP.Web.UI.PageBase
{
    private string clientId = "ctl00_ContentPlaceHolder1_supplierInfo_";
    int productType = 0;
    int generalid = 0;
    //private bool isCanSave = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["productType"]))
        {
            productType = int.Parse(Request["productType"]);
        }
        if (!string.IsNullOrEmpty(Request[RequestName.GeneralID]))
        {
            generalid = int.Parse(Request[RequestName.GeneralID]);
        }
        if (!IsPostBack)
        {
            supplierInfo.FindControl("btn").Visible = false;
            supplierInfo.FindControl("trTitle").Visible = false;
            supplierInfo.FindControl("txtfa_no").Visible = false;

            //supplierInfo.Model = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(generalid);
            //supplierInfo.BindInfo();
            supplierInfo.SetSupplyInfo();
        }
        //supplierInfo.viewControl("非协议供应商");

        //if(null != ViewState["isCanSave"] && ViewState["isCanSave"] != "")
        //{
        //    isCanSave = bool.Parse(ViewState["isCanSave"].ToString());
        //}
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ESP.Purchase.Entity.TypeInfo typeModel = null;
        if (!string.IsNullOrEmpty(Request["productType"]))
        {
            productType = int.Parse(Request["productType"]);
            typeModel = ESP.Purchase.BusinessLogic.TypeManager.GetModel(productType);
        }

        DropDownList ddlsource = (DropDownList)supplierInfo.FindControl("ddlsource");
        Label labEmailFile = (Label)supplierInfo.FindControl("labEmailFile");
        if (ddlsource.SelectedValue == "客户指定" && labEmailFile.Text == "" && this.Request.Files[0].FileName == "")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('请上传客户指定邮件附件！');", true);
            return;
        }

        ESP.Purchase.Entity.GeneralInfo general = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(generalid);
        supplierInfo.Model = general;
        supplierInfo.setModelInfo();

        ESP.Purchase.BusinessLogic.GeneralInfoManager.Update(supplierInfo.Model);

        int supplierId = 0;
        if (!string.IsNullOrEmpty(Request["supplyId"]))
        {
            //创建新的供应商并和供应链关联
            //ESP.Purchase.BusinessLogic.SupplierManager.insertSupplierAndLinkSupply(supplierInfo.GetNewSupplierModel(), int.Parse(Request["supplyId"]));
        }

        //记录操作日志
        //ESP.Logging.Logger.Add(string.Format("{0}对T_GeneralInfo表中的id={1}的数据进行 {2} 的操作", CurrentUser.Name, supplierInfo.Model.id, "编辑采购申请的供应商信息"), "供应商信息");

        if (typeModel != null && typeModel.operationflow == State.typeoperationflow_Advertisement)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.opener.GoToAD(" + general.id.ToString()+ ");window.close();", true);
        }
        else
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.opener.setSName('" + supplierInfo.Model.supplier_name + "','"+supplierId+"');window.close();", true);
    }
}
