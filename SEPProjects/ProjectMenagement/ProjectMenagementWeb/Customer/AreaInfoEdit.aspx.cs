using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Finance.Entity;
using ESP.Finance.BusinessLogic;
using ESP.Finance.Utility;

public partial class Customer_AreaInfoEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["areaid"]))
                BindInfo();
        }
    }

    private void BindInfo()
    {
        AreaInfo info = ESP.Finance.BusinessLogic.AreaManager.GetModel(Convert.ToInt32(Request["areaid"]));
        if (info != null)
        {
            this.txtCode.Text = info.AreaCode;
            this.txtENDescription.Text = info.Description;
            this.txtName.Text = info.AreaName;
            this.txtSearchCode.Text = info.SearchCode;
            this.txtNote.Text = info.Others;
        }
    }

    private void Save()
    {
        AreaInfo info = new AreaInfo();
        info.Others = this.txtNote.Text.Trim();
        info.AreaName = this.txtName.Text.Trim();
        info.AreaCode = this.txtCode.Text.Trim();
        info.Description = this.txtENDescription.Text.Trim();
        info.SearchCode = this.txtSearchCode.Text.Trim();

        if (!string.IsNullOrEmpty(Request["areaid"]) && ESP.Finance.BusinessLogic.AreaManager.GetModel(Convert.ToInt32(Request["areaid"])) != null)
        {
            info.AreaID = Convert.ToInt32(Request["areaid"]);
            UpdateResult operResult = ESP.Finance.BusinessLogic.AreaManager.Update(info);
            if (operResult == UpdateResult.Succeed)
            {
                //SaveImage();
                ClientScript.RegisterStartupScript(typeof(string), "", "window.location='AreaInfosList.aspx';alert('保存成功！');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('" + operResult.ToString() + "！');", true);
            }
        }
        else
        {
            int ret = ESP.Finance.BusinessLogic.AreaManager.Add(info);
            if(ret > 0)
                ClientScript.RegisterStartupScript(typeof(string), "", "window.location='AreaInfosList.aspx';alert('保存成功！');", true);
            else
                ClientScript.RegisterStartupScript(typeof(string), "", "alert('保存失败！');", true);
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Save();
        ClientScript.RegisterStartupScript(typeof(string), "", "window.location='AreaInfosList.aspx';alert('保存成功！');", true);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("AreaInfosList.aspx");
    }
}