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

public partial class Message_skins_PostItem : System.Web.UI.UserControl
{
    int postid;
    public int PostID
    {
        get { return this.postid; }
        set { this.postid = value; }
    }
    int loginUserid;

    public int LoginUserid
    {
        get { return loginUserid; }
        set { loginUserid = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ESP.Media.Entity.PostsInfo post = ESP.Media.BusinessLogic.PostsManager.GetModel(postid);
        //this.uvItem.UserID = post.Userid;
        this.pItem.UserID = post.Userid;
        this.pItem.PostId = this.postid;
        this.pItem.LoginUserID = this.loginUserid;
    }
}
