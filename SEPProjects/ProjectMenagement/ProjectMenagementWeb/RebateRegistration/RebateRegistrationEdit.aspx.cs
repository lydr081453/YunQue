using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Utility;
using ESP.Finance.Entity;
public partial class RebateRegistration_RebateRegistrationEdit : ESP.Web.UI.PageBase
{
    int rrid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["rrid"]))
        {
            rrid = int.Parse(Request["rrid"]);
        }
        if (!IsPostBack)
        {
            bindInfo();
        }
    }

    /// <summary>
    /// 绑定页面信息
    /// </summary>
    private void bindInfo()
    {
        if (rrid > 0)
        {
            ESP.Finance.Entity.RebateRegistrationInfo model = ESP.Finance.BusinessLogic.RebateRegistrationManager.GetModel(rrid);
            if (model != null)
            {
                if (model.Project != null)
                {
                    hidProjectId.Value = model.Project.ProjectId.ToString();
                    txtProjectCode.Text = model.Project.ProjectCode;
                }
                hidMediaId.Value = model.Supplier.id.ToString();
                txtMediaName.Text = model.Supplier.supplier_name;
                txtRebateAmount.Text = model.RebateAmount.ToString();
                txtCreditedDate.Text = model.CreditedDate;
                txtRemark.Text = model.Remark;
            }
        }
        else
            txtCreditedDate.Text = DateTime.Now.ToString("yyyy-MM");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (rrid > 0)
        {
            if (ESP.Finance.Utility.UpdateResult.Succeed == ESP.Finance.BusinessLogic.RebateRegistrationManager.Update(getBankModel()))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存成功！');window.location='RebateRegistrationList.aspx';", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存失败！');", true);
            }
        }
        else
        {
            RebateRegistrationInfo model = getBankModel();
            model.CreateDate = DateTime.Now;
            model.CreateUserId = CurrentUserID;
            model.Status = Common.RebateRegistration_Status.Saved;
            if (ESP.Finance.BusinessLogic.RebateRegistrationManager.Add(model) > 0)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存成功！');window.location='RebateRegistrationList.aspx';", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('保存失败！');", true);
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("RebateRegistrationList.aspx");
    }

    private ESP.Finance.Entity.RebateRegistrationInfo getBankModel()
    {
        ESP.Finance.Entity.RebateRegistrationInfo model = null;
        if (rrid > 0)
        {
            model = ESP.Finance.BusinessLogic.RebateRegistrationManager.GetModel(rrid);
        }
        else
        {
            model = new ESP.Finance.Entity.RebateRegistrationInfo();
        }
        if(hidProjectId.Value != "")
            model.ProjectId = int.Parse(hidProjectId.Value);
        model.SupplierId = int.Parse(hidMediaId.Value);
        model.RebateAmount = decimal.Parse(txtRebateAmount.Text);
        model.CreditedDate = txtCreditedDate.Text;
        model.Remark = txtRemark.Text.Trim();

        return model;
    }
}
