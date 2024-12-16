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
using ESP.Compatible;
using ESP.MediaLinq.Entity;

namespace MediaWeb.NewMedia.BaseData
{
    public partial class NewIndustry : ESP.Web.UI.PageBase
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
            if (!string.IsNullOrEmpty(Request["openType"]))
            {
                btnClose.Visible = true;
                btnReturn.Visible = false;
            }

        }

        #region 初始化页面信息
        /// <summary>
        /// Inits the page.
        /// </summary>
        private void InitPage()
        {
            string operate = "null";


            if (Request["Operate"] != null)
            {
                operate = Request["Operate"];
            }
            if (operate == "EDIT")
            {
                InitEdit();
            }
            else if (operate == "ADD")
            {
                this.btnAdd.Text = "保存";

            }
            else if (operate == "Del")
            {
                doDel(Request["Iid"]);
            }
        }

        /// <summary>
        /// Inits the edit.
        /// </summary>
        private void InitEdit()
        {

            this.btnAdd.Text = "保存";
            if (Request["Iid"] != null)
            {
                int id = Convert.ToInt32(Request["Iid"]);
                media_IndustriesInfo mi = ESP.MediaLinq.BusinessLogic.IndustriesManager.GetModel(id);
                if (mi != null)
                {
                    txtName.Text = mi.IndustryName;
                }
            }
        }
        #endregion

        #region 添加
        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(CurrentUserID);
            int ret = 0;
            if (Request["Operate"] == "ADD")
            {
                media_IndustriesInfo mi = new media_IndustriesInfo();
                mi.IndustryName = txtName.Text.Trim();
                //////原来写法，不知emp有什么用
              //  ret = ESP.Media.BusinessLogic.IndustriesManager.add(mi, emp);
                ret = ESP.MediaLinq.BusinessLogic.IndustriesManager.Add(mi);
            }
            else if (Request["Operate"] == "EDIT")
            {
                media_IndustriesInfo mi = ESP.MediaLinq.BusinessLogic.IndustriesManager.GetModel(Convert.ToInt32(Request["Iid"]));
                mi.IndustryName = txtName.Text.Trim();
                //////原来写法，不知emp有什么用
                ///bool b = ESP.Media.BusinessLogic.IndustriesManager.modify(mi, emp);
                bool b = ESP.MediaLinq.BusinessLogic.IndustriesManager.Update(mi);

            }
            if (!string.IsNullOrEmpty(Request["openType"]))
            {

                //ClientScript.RegisterStartupScript(typeof(string), "", "window.opener.location.href=window.opener.location.href;window.close();", true);
                ClientScript.RegisterStartupScript(typeof(string), "", "window.opener.setIndusty('" + ret + "','" + txtName.Text + "');window.close();", true);

                //Response.Write("<script>opener.document.all._ctl0_ContentPlaceHolder1_hidClientid.innerText= '" + "a" + "'</script>");


                //Response.Write(@"<script>window.close();</script>");
            }
            else { Response.Redirect("IndustryList.aspx"); }

        }
        #endregion

        /// <summary>
        /// Handles the Click event of the btnReturn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnReturn_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("IndustryList.aspx");
        }

        #region 删除
        /// <summary>
        /// Does the del.
        /// </summary>
        /// <param name="id">The id.</param>
        protected void doDel(string id)
        {
            bool ret = ESP.MediaLinq.BusinessLogic.IndustriesManager.Delete(int.Parse(id));
            if (ret)
            {

                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('删除成功');"), true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('删除失败');"), true);
            }
            Response.Redirect("IndustryList.aspx");
        }
        #endregion
    }
}
