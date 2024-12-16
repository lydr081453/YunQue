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

public partial class Message_NewPost : ESP.Web.UI.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //ucUser.UserID = CurrentUserID;

        newPost.UserID = CurrentUserID;
       
        newPost.PostType = (int)ESP.Media.Access.Utilities.Global.PostType.Issue;

        if (Request[ESP.Media.Access.Utilities.Global.RequestKey.IsSysMsg] != null && Request[ESP.Media.Access.Utilities.Global.RequestKey.IsSysMsg].ToString() != null)
        {
            int IsSysMsg = Convert.ToInt32(Request[ESP.Media.Access.Utilities.Global.RequestKey.IsSysMsg]);
            newPost.IsSystemMsg = IsSysMsg;
        }

    }

}
