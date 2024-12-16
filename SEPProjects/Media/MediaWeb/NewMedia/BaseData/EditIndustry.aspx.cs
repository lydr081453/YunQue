using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ESP.MediaLinq.Entity;

namespace MediaWeb.NewMedia.BaseData
{
    public partial class EditIndustry : ESP.Web.UI.PageBase
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        /// <summary>
        /// Raises the <see cref="E:Init"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            int userid = CurrentUserID;
        }

        #region 初始化页面信息
        /// <summary>
        /// Inits the page.
        /// </summary>
        private void InitPage()
        {
            string operate = "null";
            this.btnOk.Enabled = false;
            if (Request["Operate"] != null)
            {
                operate = Request["Operate"];
            }
            if (operate == "EDIT")
            {
                InitEdit();
            }
            else if (operate == "DEL")
            {
                int mid = Convert.ToInt32(Request["Mid"]);
                bool ret = false;//ESP.Media.BusinessLogic.IndustriesManager.Del(GetObject().Industryid);
                if (ret)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ReporterList.aspx?Mid=" + Request["Mid"] + "';alert('删除成功！');", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='ReporterList.aspx';alert('删除失败');", true);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("IndustryList.aspx");
        }

        /// <summary>
        /// Inits the edit.
        /// </summary>
        private void InitEdit()
        {
            this.Title = "编辑记者";
            this.btnOk.Text = "保存";
            this.labHeading.Text = "编辑记者";
            if (Request["Iid"] != null)
            {
                int id = Convert.ToInt32(Request["Iid"]);
                media_IndustriesInfo mlIndustry = ESP.MediaLinq.BusinessLogic.IndustriesManager.GetModel(id);
                if (mlIndustry != null)
                {
                    txtName.Text = mlIndustry.IndustryName.Trim();
                }
            }
        }
        #endregion

        #region 获得对象
        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <returns></returns>
        private media_IndustriesInfo GetObject()
        {
            media_IndustriesInfo mlIndustry = null;
            if (Request["Iid"] != null)
            {
                mlIndustry = ESP.MediaLinq.BusinessLogic.IndustriesManager.GetModel(Convert.ToInt32(Request["Iid"]));
            }
            else
            {
                mlIndustry = new media_IndustriesInfo();
            }
            if (Request["Iid"] != null)
            {
                mlIndustry.IndustryID = Convert.ToInt32(Request["Iid"]);
            }
            mlIndustry.IndustryName = txtName.Text.Trim();
            return mlIndustry;
        }
        #endregion

        #region 确认
        /// <summary>
        /// Handles the Click event of the btnOk control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnOk_Click(object sender, EventArgs e)
        {
            //bool ret;
            //int iid;
            //string errmeg;
            //if (Request["Iid"] != null)
            //{
            //    iid = Convert.ToInt32(Request["Iid"]);
            //}
            //else
            //{
            //    return;
            //}
            //if (Request["Rid"] != null)
            //{
            //    ret = ESP.Media.BusinessLogic.IndustriesManager.modify(GetObject(), CurrentUser);
            //    if (ret > 0)
            //    {
            //        if (string.IsNullOrEmpty(Request["alert"]))
            //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.location='ReporterDisplay.aspx?Rid={0}';alert('修改成功！');", Request["Rid"]), true);
            //        else
            //            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.opener.location.reload();alert('修改成功！');window.close();", ret), true);
            //    }
            //    else
            //    {
            //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmeg), true);
            //    }
            //}
        }
        #endregion
    }
}
