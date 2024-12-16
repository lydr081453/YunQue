using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using ESP.Finance.Utility;
using ESP.Finance.BusinessLogic;
public partial class Dialogs_FrameDlg : System.Web.UI.Page
{
    int customerid = 0;
    int attachId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if(!string.IsNullOrEmpty(Request[ESP.Finance.Utility.RequestName.CustomerID]))
            {
                 customerid=Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.CustomerID]);
            }
            if (!string.IsNullOrEmpty(Request["attachId"]))
            {
                attachId = Convert.ToInt32(Request["attachId"]);
                bindInfo();
            }
        }
    }

    private void bindInfo()
    {
        ESP.Finance.Entity.CustomerAttachInfo model = CustomerAttachManager.GetModel(attachId);
        if (model != null)
        {
            txtFrameDesc.Text = model.FrameContractTitle;
            txtRemark.Text = model.Description;
            txtBeginDate.Text = model.FrameBeginDate.Value.ToString("yyyy-MM-dd");
            txtEndDate.Text = model.FrameEndDate.Value.ToString("yyyy-MM-dd");
            hidFile.Value = lnkFile.NavigateUrl = model.Attachment;
            lnkFile.Text = "<a target='_blank' href='/Dialogs/CustomerFileDownload.aspx?" + ESP.Finance.Utility.RequestName.CustomerAttachID + "=" + model.AttachID.ToString() + "'><img src='/images/ico_04.gif' border='0' /></a>";
        }
    }

    private int saveFrame()
    {
        ESP.Finance.Entity.CustomerAttachInfo model = new ESP.Finance.Entity.CustomerAttachInfo();
        if (!string.IsNullOrEmpty(Request["attachId"]) && Request["attachId"] != "0")
        {
            model = CustomerAttachManager.GetModel(int.Parse(Request["attachId"]));
        }
        else
        {
            model.Status = Common.CustomerAttachStatus.Saved;
            model.ProjectId = string.IsNullOrEmpty(Request["pid"]) ? 0 : int.Parse(Request["pid"]);
            if (model.ProjectId == 0)
            {
                model.Status = Common.CustomerAttachStatus.Used;
            }
        }
            
        string fileName = string.Empty;
        if (this.fileupContract.FileName != string.Empty)
        {
            string selectedpath = this.fileupContract.FileName;
            if (selectedpath.LastIndexOf(".") != -1)
            {
                selectedpath = selectedpath.Substring(selectedpath.LastIndexOf("."));
            }
            else
                selectedpath = string.Empty;
            fileName = "Customer__" + Guid.NewGuid().ToString() + "__" + this.fileupContract.FileName;
            this.fileupContract.SaveAs(ESP.Configuration.ConfigurationManager.SafeAppSettings["CustomerAttachPath"] + "\\" + fileName);
            model.Attachment = fileName;
        }
        customerid = Convert.ToInt32(Request[ESP.Finance.Utility.RequestName.CustomerID]);
        model.CustomerID = customerid;
        model.FrameContractTitle = this.txtFrameDesc.Text;
        model.FrameBeginDate = Convert.ToDateTime(this.txtBeginDate.Text);
        model.FrameEndDate = Convert.ToDateTime(this.txtEndDate.Text);
        model.Description = txtRemark.Text;


        if (model.AttachID == 0)
            return ESP.Finance.BusinessLogic.CustomerAttachManager.Add(model);
        else
        {
            ESP.Finance.BusinessLogic.CustomerAttachManager.Update(model);
            return model.AttachID;
        }
}
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string location = string.Empty;
        int frameid=saveFrame() ;
        if (frameid > 0)
        {
            if (!string.IsNullOrEmpty(Request["s"]) && Request["s"] == "fc")
                location = "opener.bindFrame();window.close();";
            else
                location = string.Format("alert('框架协议保存成功.');opener.location='/Customer/CustomerInfoEdit.aspx?{0}={1}';window.close();", RequestName.CustomerID, Request[ESP.Finance.Utility.RequestName.CustomerID]);
            ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), location, true);
        }
        else
        {
            location = string.Format("alert('框架协议保存失败.');");
            ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), location, true);
        }


    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        string location = "";
        if (!string.IsNullOrEmpty(Request["s"]) && Request["s"] == "fc")
            location = "window.close();";
        else
            location = string.Format("opener.location='/Customer/CustomerInfoEdit.aspx?{0}={1}';window.close();", RequestName.CustomerID, Request[ESP.Finance.Utility.RequestName.CustomerID]);
        //string location = string.Format("opener.location.reload();window.close();");
        ScriptManager.RegisterStartupScript(this, Page.GetType(), new Guid().ToString(), location, true);
    }
}
