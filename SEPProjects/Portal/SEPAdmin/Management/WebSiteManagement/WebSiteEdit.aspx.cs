using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ESP.Framework.BusinessLogic;
using ESP.Framework.Entity;

namespace SEPAdmin.WebSiteManagement
{
    public partial class WebSiteEdit : ESP.Web.UI.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string para = Request.QueryString["id"];
                int webSiteID = 0;
                if (!int.TryParse(para, out webSiteID))
                {
                    webSiteID = 0;
                }

                WebSiteInfo info = null;
                if (webSiteID > 0)
                {
                    info = WebSiteManager.Get(webSiteID);
                    if (info == null)
                    {
                        // unexpected error, return back to the list page
                        Response.Redirect("WebSiteList.aspx", true);
                        return;
                    }
                }
                else
                {
                    info = new WebSiteInfo();
                }
                this.ViewState["Entity"] = info;

                BindData(info);
            }
        }

        private void BindData(WebSiteInfo info)
        {
            this.txtWebSiteID.Text = info.WebSiteID > 0 ? info.WebSiteID.ToString() : "保存后自动生成";
            //this.txtWebSiteName.ReadOnly = info.WebSiteID > 0;
            this.txtWebSiteName.Text = info.WebSiteName;
            this.txtDescription.Text = info.Description;
            this.txtOrdinal.Text = info.Ordinal.ToString();
            this.txtUrlPrefix.Text = info.UrlPrefix;
            this.txtFramePage.Text = info.FramePagePath;

            lblAddTitle.Visible = info.WebSiteID > 0 ? false : true;
            lblEditTitle.Visible = info.WebSiteID > 0 ? true : false;
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            WebSiteInfo info = this.ViewState["Entity"] as WebSiteInfo;
            if (info == null)
            {
                // unexpected error, return back to the list page
                Response.Redirect("WebSiteList.aspx", true);
                return;
            }

            Page.Validate();

            if (!Page.IsValid)
                return;

            int ordinal = 0;
            if (!int.TryParse(txtOrdinal.Text, out ordinal) || ordinal < 0)
                ordinal = 0;

            txtOrdinal.Text = ordinal.ToString();

            info.WebSiteName = this.txtWebSiteName.Text.Trim();
            info.Description = this.txtDescription.Text.Trim();
            info.Ordinal = ordinal;
            info.UrlPrefix = this.txtUrlPrefix.Text.Trim();
            info.FramePagePath = txtFramePage.Text.Trim();
            //info.Token = this.txtToken.Text;

            info.Creator = this.UserID;
            info.CreatedTime = DateTime.Now;

            try
            {
                if (info.WebSiteID > 0)
                {
                    info.LastModifier = this.UserID;
                    info.LastModifiedTime = DateTime.Now;
                    //////
                    WebSiteManager.Update( info);
                }
                else
                {
                    info.Creator = this.UserID;
                    info.CreatedTime = DateTime.Now;
                    //////
                    WebSiteManager.Create( info);
                }
                this.ViewState["Entity"] = info;
                lblMessage.Text = "保存成功.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            catch(Exception ex)
            {
                lblMessage.Text = "保存失败." + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }

            BindData(info);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebSiteList.aspx", true);
        }

    }
}
