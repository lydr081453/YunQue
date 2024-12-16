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
using ESP.Media.Entity;
public partial class Media_MediaContentsDisplay : ESP.Web.UI.PageBase
{

    int alertvalue = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["LMid"] != null)
        {
            MediaitemshistInfo media = ESP.Media.BusinessLogic.MediaitemsManager.GetHistModel(Convert.ToInt32(Request["LMid"]));
            UserControl uc;

            if (media != null)
            {
                if (media.Mediaitemtype == 1)
                {
                    uc = (UserControl)this.LoadControl("skins/PlaneMediaContentsDisplay.ascx");
                    panelMediaDisplay.Controls.Add(uc);
                    ((Media_skins_PlaneMediaContentsDisplay)uc).InitPage(media);
                }
                else if (media.Mediaitemtype == 3)
                {
                    uc = (UserControl)this.LoadControl("skins/TvMediaContentsDisplay.ascx");
                    panelMediaDisplay.Controls.Add(uc);
                    ((Media_skins_TvMediaContentsDisplay)uc).InitPage(media);
                }
                else if (media.Mediaitemtype == 2)
                {
                    uc = (UserControl)this.LoadControl("skins/WebMediaContentsDisplay.ascx");
                    panelMediaDisplay.Controls.Add(uc);
                    ((Media_skins_WebMediaContentsDisplay)uc).InitPage(media);
                }
                else if (media.Mediaitemtype == 4)
                {
                    uc = (UserControl)this.LoadControl("skins/DABMediaContentsDisplay.ascx");
                    panelMediaDisplay.Controls.Add(uc);
                    ((Media_skins_DABMediaContentsDisplay)uc).InitPage(media);
                }
              
            }
        }
        if (!string.IsNullOrEmpty(Request["alert"]) && Request["alert"] != "1")
        {
            btnBack.Visible = true;
            alertvalue = int.Parse(Request["alert"]);
        }
    }

    override protected void OnInit(EventArgs e)
    {
       
        base.OnInit(e);
        int userid = CurrentUserID;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        string param = Request.Url.Query;
        param = ESP.Media.Access.Utilities.Global.AddParam(param, "Mid", Request["Mid"]);
        if (alertvalue > 0)
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (alertvalue - 1).ToString());
        Response.Redirect(string.Format("{0}?{1}", Request["backurl"], param));
    }
}
