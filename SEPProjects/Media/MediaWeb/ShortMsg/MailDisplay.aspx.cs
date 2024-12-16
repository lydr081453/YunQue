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
public partial class ShortMsg_MailDisplay : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["Sid"] != null)
        {
            int id = Convert.ToInt32(Request["Sid"]);
            MailmsgInfo mlMailmsg = ESP.Media.BusinessLogic.MailmsgManager.GetModel(id);
            if (mlMailmsg != null)
            {

                labSubject.Text = Server.HtmlDecode(mlMailmsg.Subject.Trim());//主题
                labBody.Text = Server.HtmlDecode( mlMailmsg.Body.Trim());//内容
                
                string str2 = string.Empty;
                if (mlMailmsg.Attachmentspath.Trim().Length > 0 || !string.IsNullOrEmpty(mlMailmsg.Attachmentspath.Trim()))
                {
                    string[] str = mlMailmsg.Attachmentspath.Trim().Split('/');
                    str2 = str[str.Length - 1].ToString();
                }
                else
                {
                    str2 = "没有附件";
                }
                labAnnx.Text = str2;

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
        Response.Redirect("/ShortMsg/MailList.aspx");
    }
   
}