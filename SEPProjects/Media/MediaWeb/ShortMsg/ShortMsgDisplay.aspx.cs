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
public partial class ShortMsg_ShortMsgDisplay : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["Sid"] != null)
                {
                    int id = Convert.ToInt32(Request["Sid"]);
                    ShortmsgInfo mlShortmsg = ESP.Media.BusinessLogic.ShortmsgManager.GetModel(id);
                    if (mlShortmsg != null)
                    {

                        labSubject.Text = mlShortmsg.Subject.Trim();//主题
                        labBody.Text = mlShortmsg.Body.Trim();//内容
                        
                    }
            }
    }
    override protected void OnInit(EventArgs e)
    {
        base.OnInit(e);
        int userid = CurrentUserID;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/ShortMsg/ShortMsgList.aspx");
    }
   
}
