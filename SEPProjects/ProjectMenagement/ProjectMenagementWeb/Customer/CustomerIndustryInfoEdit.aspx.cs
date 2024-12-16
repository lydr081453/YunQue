using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Utility;

public partial class Customer_BizTypeInfoEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["industryid"]))
                BindInfo();
        }
    }

    private void BindInfo()
    {
        CustomerIndustryInfo info = ESP.Finance.BusinessLogic.CustomerIndustryManager.GetModel(Convert.ToInt32(Request["industryid"]));
        if (info != null)
        {
            this.txtCode.Text = info.IndustryCode;
            this.txtName.Text = info.CategoryName;
            this.txtNote.Text = info.Description;
        }
    }

    private void Save()
    {
        CustomerIndustryInfo info = new CustomerIndustryInfo();
        info.Description = this.txtNote.Text.Trim();
        info.CategoryName = this.txtName.Text.Trim();
        info.IndustryCode = this.txtCode.Text.Trim();

        if (!string.IsNullOrEmpty(Request["industryid"]) && ESP.Finance.BusinessLogic.CustomerIndustryManager.GetModel(Convert.ToInt32(Request["industryid"])) != null)
        {
            info.IndustryID = Convert.ToInt32(Request["industryid"]);

            UpdateResult operResult = ESP.Finance.BusinessLogic.CustomerIndustryManager.Update(info);
            if (operResult == UpdateResult.Succeed)
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "window.location='CustomerIndustryInfosList.aspx';alert('保存成功！');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + operResult.ToString() + "！');", true);
            }
        }
        else
        {
            int ret = ESP.Finance.BusinessLogic.CustomerIndustryManager.Add(info);
            if (ret > 0)
                ClientScript.RegisterStartupScript(typeof(string), "", "window.location='CustomerIndustryInfosList.aspx';alert('保存成功！');", true);
            else
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存失败！');", true);
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Save();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomerIndustryInfosList.aspx");
    }
}