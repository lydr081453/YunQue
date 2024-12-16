using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ESP.Compatible;

namespace MediaWeb.NewMedia.BaseData
{
    public partial class NewMedia : ESP.Web.UI.PageBase
    {
        string source = string.Empty;
        string errmsg = null;
        //string backurl = "#";

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.Media.BusinessLogic.MediaitemsManager));         
            //if (Request["Source"] != null)
            //{
            //    source = Request["Source"];
            //    this.hidUrl.Value = Request["Source"];
            //}
            //else if (Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] != null)
            //{
            //    this.hidUrl.Value = Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage].ToString();
            //}
            //else
            //{
            //    btnBack.Visible = false;
            //    Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] = ESP.Media.Access.Utilities.Global.Url.NewMedia;
            //    this.hidUrl.Value = "";
            //}
            if (Request["listadd"] != null)
            {
                this.btnBack.Visible = true;
                //backurl = Request["backurl"];
            }
            else
            {
                this.btnBack.Visible = false;
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AuditedMediaList.aspx");
        }

        /// <summary>
        /// Handles the Click event of the btnNext control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnNext_Click(object sender, EventArgs e)
        {
            //MediaitemsInfo media = new MediaitemsInfo();

            //media.Createddate = DateTime.Now.ToString();
            //media.Createdbyuserid = int.Parse(CurrentUser.SysID);
            //media.Lastmodifieddate = DateTime.Now.ToString();
            //media.Lastmodifiedbyuserid = int.Parse(CurrentUser.SysID);

            //media.Mediacname = this.txtPlaneName.Text.Trim();//媒体中文名称
            //media.Mediaename = txtPlaneEngName.Text.Trim();//媒体英文名称
            //media.Cshortname = txtPlaneHTCName.Text.Trim();//媒体中文简称
            //media.Eshortname = txtPlaneEngHTCName.Text.Trim();//媒体英文简称
            //if (ddlNewMediaList.SelectedValue == "plane")
            //{
            //    media.Mediaitemtype = ESP.Media.Access.Utilities.Global.MediaItemType_Plane;

            //}
            //else if (ddlNewMediaList.SelectedValue == "tv")
            //{
            //    media.Mediaitemtype = ESP.Media.Access.Utilities.Global.MediaItemType_Tv;
            //}
            //else if (ddlNewMediaList.SelectedValue == "web")
            //{
            //    media.Mediaitemtype = ESP.Media.Access.Utilities.Global.MediaItemType_Web;
            //}
            //else
            //{
            //    media.Mediaitemtype = ESP.Media.Access.Utilities.Global.MediaItemType_Dab;
            //}
            //media.Mediumsort = ESP.Media.Access.Utilities.Global.MediaItemTypeName[media.Mediaitemtype].ToString();
            //int Mid = ESP.Media.BusinessLogic.MediaitemsManager.Add(media, new ESP.Media.BusinessLogic.MediaAttach(), out errmsg, CurrentUserID);
            //if(Mid > 0)
            //    Response.Redirect("MediaAddAndEdit.aspx?Operate=EDIT&Mid=" + Mid);
            //else
            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);
            //Response.Redirect(string.Format("MediaAddAndEdit.aspx?MediaType={0}&Operate={1}",ddlNewMediaList.SelectedValue,"ADD"));
        }

        protected void btnPlane_Click(object sender, EventArgs e)
        {
            string param = Request.Url.Query;
            param = ESP.MediaLinq.Utilities.Global.AddParam(param, "MediaType", "plane");
            param = ESP.MediaLinq.Utilities.Global.AddParam(param, "Operate", "ADD");

            Response.Redirect(string.Format("MediaAddAndEdit.aspx?{0}", param));
        }

        protected void btnTv_Click(object sender, EventArgs e)
        {
            string param = Request.Url.Query;
            param = ESP.MediaLinq.Utilities.Global.AddParam(param, "MediaType", "tv");
            param = ESP.MediaLinq.Utilities.Global.AddParam(param, "Operate", "ADD");
            Response.Redirect(string.Format("MediaAddAndEdit.aspx?{0}", param));
        }

        protected void btnWeb_Click(object sender, EventArgs e)
        {
            string param = Request.Url.Query;
            param = ESP.MediaLinq.Utilities.Global.AddParam(param, "MediaType", "web");
            param = ESP.MediaLinq.Utilities.Global.AddParam(param, "Operate", "ADD");
            Response.Redirect(string.Format("MediaAddAndEdit.aspx?{0}", param));
        }

        protected void btnDab_Click(object sender, EventArgs e)
        {
            string param = Request.Url.Query;
            param = ESP.MediaLinq.Utilities.Global.AddParam(param, "MediaType", "dab");
            param = ESP.MediaLinq.Utilities.Global.AddParam(param, "Operate", "ADD");
            Response.Redirect(string.Format("MediaAddAndEdit.aspx?{0}", param));
        }
    }
}
