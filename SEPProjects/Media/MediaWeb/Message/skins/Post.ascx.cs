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

public partial class Message_skins_Post : System.Web.UI.UserControl
{
    //ESP.Media.Entity.Users currentUser;
    ESP.Compatible.Employee currentUser;
    string currentPage = string.Empty;
    public string CurrentPage
    {
        get { return this.currentPage; }
        set { this.currentPage = value; }
    }

    int userid = 0;
    public int UserID
    {
        get { return this.userid; }
        set 
        { 
            this.userid = value;
            currentUser = new ESP.Compatible.Employee(userid);
        }
    }

    public ESP.Compatible.Employee CurrentUser
    {
        get { return this.currentUser; }
        set { this.currentUser = value; }
    }


    ESP.Compatible.Employee loginuser;

    int loginuserid = 0;
    public int LoginUserID
    {
        get { return this.loginuserid; }
        set
        {
            this.loginuserid = value;
            loginuser = new ESP.Compatible.Employee(loginuserid);
        }
    }

    public ESP.Compatible.Employee LoginUser
    {
        get { return this.loginuser; }
        set { this.loginuser = value; }
    }

    ESP.Media.Entity.PostsInfo currentPost = new ESP.Media.Entity.PostsInfo();
    public ESP.Media.Entity.PostsInfo CurrentPost
    {
        get { return this.currentPost; }
        set { this.currentPost = value; }
    }


    int postid = 0;
    public int test = 1;
    public int PostId
    {
        get { return this.postid; }
        set { this.postid = value; }
    }
    string subject = string.Empty;
    public string Subject
    {
        get { return this.subject; }
        set { this.subject = value; }
    }
    string content = string.Empty;
    public string Content      
    {
        get { return this.content; }
        set { this.content = value; }
    }



    protected void Page_Load(object sender, EventArgs e)
    {

        if (postid > 0)
        {
            currentPost = ESP.Media.BusinessLogic.PostsManager.GetModel(postid);
        }

        this.lbtnEdit.Visible = false;
        this.lbtnDel.Visible = false;
        if (loginuserid  > 0)
        {
            if (loginuserid == currentPost.Userid)
            {
                this.lbtnEdit.Visible = true;
                this.lbtnDel.Visible = true;
            }
            //else if(currentUser)//如果是管理员,则有删帖的权利
        }
        if (Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentPage] != null && Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentPage].ToString().Trim().Length > 0)
        {
            this.currentPage = Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentPage].ToString();
        }
        if (currentUser != null && currentUser.ID != null && currentUser.Name !=null)
        {
            this.litSub.Text = string.Format("<label class='heading' style='width:100%'>{0}</label>", currentUser.ID.ToString() + " " + currentUser.Name + ": " + Server.HtmlDecode(currentPost.Subject));
        }
        else
        {
            this.litSub.Text = string.Format("<label class='heading' style='width:100%'>{0}</label>", "unkownuser" + ": " + Server.HtmlDecode(currentPost.Subject));
        }
        this.litBody.Text = string.Format("<label style='width:100%;wrap:true'>{0}</label>", Server.HtmlDecode(currentPost.Body));
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(currentPage + string.Format("&{0}={1}", ESP.Media.Access.Utilities.Global.RequestKey.EditPostID, this.postid.ToString()));
        }
        catch (Exception ex) { string err = ex.Message; }
    }


    protected void lbtnDel_Click(object sender, EventArgs e)
    {
        try
        {
            currentPost.Del = 1;
            ESP.Media.BusinessLogic.PostsManager.del(currentPost, userid);//此出为置位
            Response.Redirect(currentPage);
        }
        catch { }
    }

}
