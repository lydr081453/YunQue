using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Entity;
using AjaxPro;
using ESP.Finance.Utility;

public partial class ForeGift_addForegift : ESP.Web.UI.PageBase
{
    int returnId = 0;
    int gid = 0;
    ESP.Purchase.Entity.GeneralInfo generalModel=null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.ReturnID]))
        {
            returnId = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.ReturnID]);
        }
        if (!string.IsNullOrEmpty(Request[ESP.Purchase.Common.RequestName.GeneralID]))
        {
            gid = Convert.ToInt32(Request[ESP.Purchase.Common.RequestName.GeneralID]);
            generalModel = ESP.Purchase.BusinessLogic.GeneralInfoManager.GetModel(gid);
        }

        if (!IsPostBack)
        {
            BindInfo();
        }
    }

    private void BindInfo()
    {
        IList<PaymentTypeInfo> paylist = ESP.Finance.BusinessLogic.PaymentTypeManager.GetList(null, null);
        ddlPaymentType.DataSource = paylist;
        ddlPaymentType.DataTextField = "paymenttypename";
        ddlPaymentType.DataValueField = "paymenttypeid";
        ddlPaymentType.DataBind();

        if (gid > 0)
        {
            //根据PR单绑定信息
            hidPrId.Value = generalModel.id.ToString();
            hidProjectId.Value = generalModel.Project_id.ToString();
            lblPRNo.Text = generalModel.PrNo;
            lblProjectCode.Text = generalModel.project_code;
            lblApplicant.Text = generalModel.requestorname;
            hidApplicant.Value = generalModel.requestor.ToString();
            txtSupplierName.Text = generalModel.account_name;
            txtSupplierBank.Text = generalModel.account_bank;
            txtSupplierAccount.Text = generalModel.account_number;
            lblForegift.Text = generalModel.Foregift.ToString("#,##0.00");//押金
            lblInceptDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        else
        {
            if (returnId > 0)
            {
                ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
                if (returnModel != null)
                {
                    TopMessage.Model = generalModel;
                }
                if (returnModel.ReturnStatus != (int)ESP.Finance.Utility.PaymentStatus.Save)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('你没有访问该数据的权限！');window.location.href='Default.aspx';", true);
                }
                hidPrId.Value = returnModel.PRID.Value.ToString();
                hidProjectId.Value = returnModel.ProjectID.Value.ToString();
                lblPRNo.Text = returnModel.PRNo;
                lblProjectCode.Text = returnModel.ProjectCode;
                lblApplicant.Text = returnModel.RequestEmployeeName;
                hidApplicant.Value = returnModel.RequestorID.Value.ToString();
                txtSupplierName.Text = returnModel.SupplierName;
                txtSupplierBank.Text = returnModel.SupplierBankName;
                txtSupplierAccount.Text = returnModel.SupplierBankAccount;
                lblForegift.Text = returnModel.PreFee.Value.ToString("#,##0.00");//押金
                lblReturnCode.Text = returnModel.ReturnCode;
                txtReturnContent.Text = returnModel.ReturnContent;
                lblInceptDate.Text = returnModel.ReturnPreDate == null ? "" : returnModel.ReturnPreDate.Value.ToString("yyyy-MM-dd");
                lblStatus.Text = ESP.Finance.Utility.ReturnPaymentType.ReturnStatusString(returnModel.ReturnStatus.Value,0,returnModel.IsDiscount);
                txtBeginDate.Text = returnModel.PreBeginDate == null ? "" : returnModel.PreBeginDate.Value.ToString("yyyy-MM-dd");
                txtEndDate.Text = returnModel.PreEndDate == null ? "" : returnModel.PreEndDate.Value.ToString("yyyy-MM-dd");//预计归还时间
                ddlPaymentType.SelectedValue = returnModel.PaymentTypeID.Value.ToString();
            }
        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("/Edit/ReturnTabEdit.aspx");
    }

    private int SaveModel()
    {
        int returnValue = 0;
        ESP.Finance.Entity.ReturnInfo returnModel = ESP.Finance.BusinessLogic.ReturnManager.GetModel(returnId);
        if (returnModel == null)
            returnModel = new ReturnInfo();
        returnModel.PRID = int.Parse(hidPrId.Value);
        returnModel.ProjectID = int.Parse(hidProjectId.Value);
        returnModel.PRNo = lblPRNo.Text.Trim();
        returnModel.ProjectCode = lblProjectCode.Text.Trim();
        returnModel.SupplierName = txtSupplierName.Text.Trim();
        returnModel.SupplierBankName = txtSupplierBank.Text.Trim();
        returnModel.SupplierBankAccount = txtSupplierAccount.Text.Trim();
        returnModel.PreFee = decimal.Parse(lblForegift.Text.Trim());
        returnModel.ReturnContent = txtReturnContent.Text.Trim();
        returnModel.ReturnType = (int)ESP.Purchase.Common.PRTYpe.PN_ForeGift;
        returnModel.PreBeginDate = DateTime.Parse(txtBeginDate.Text);
        returnModel.PreEndDate = DateTime.Parse(txtEndDate.Text);//预计归还时间
        returnModel.PaymentTypeID = int.Parse(ddlPaymentType.SelectedValue);
        returnModel.PaymentTypeName = ddlPaymentType.SelectedItem.Text;
        returnModel.DepartmentID = generalModel.Departmentid;
        returnModel.DepartmentName = generalModel.Department;
        if (returnModel.ReturnID == 0)
        {
            //创建，生成时间、ReturnCode
            returnModel.ReturnPreDate = DateTime.Now;
            returnModel.RequestorID = int.Parse(CurrentUser.SysID);
            returnModel.RequestUserCode = CurrentUser.ITCode;
            returnModel.RequestUserName = CurrentUser.ITCode;
            returnModel.RequestEmployeeName = CurrentUser.Name;
            returnModel.RequestDate = DateTime.Now;
            returnModel.ReturnStatus = (int)PaymentStatus.Save;
            returnValue = ESP.Finance.BusinessLogic.ReturnManager.CreateReturnInFinance(returnModel);
        }
        else
        {
            returnValue = returnModel.ReturnID;
            ESP.Finance.BusinessLogic.ReturnManager.Update(returnModel);
        }
        return returnValue;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if(SaveModel() > 0)
            ShowCompleteMessage("保存成功！", "/Edit/ReturnTabEdit.aspx");
    }

    protected void btnSetting_Click(object sender, EventArgs e)
    {
        returnId = SaveModel();
        if (returnId > 0)
            Response.Redirect("SetAuditor.aspx?" + RequestName.ReturnID + "=" + returnId);
    }
}
