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


public partial class Message_skins_Input : System.Web.UI.UserControl
{



    private int userid = 0;
    public int UserID
    {
        get { return this.userid; }
        set { this.userid = value; }
    }

    ESP.Compatible.Employee currentUser;
    public ESP.Compatible.Employee User
    {
        get { return currentUser; }
        set { currentUser = value; }
    }


    int operateType = (int)ESP.Media.Access.Utilities.Global.OperateType.Add;
    public int OperateType
    {
        get { return operateType; }
        set { operateType = value; }
    }
    
    string currentPage = string.Empty;
    public string CurrentPage
    {
        get { return this.currentPage; }
        set { this.currentPage = value; }
    }

    int postid=0;
    public int PostId
    {
        get { return postid; }
        set { postid = value; }
    }

    int posttype = (int)ESP.Media.Access.Utilities.Global.PostType.Reply;
    public int PostType
    {
        get { return posttype; }
        set { posttype = value; }
    }

    int isSystemMsg = (int)ESP.Media.Access.Utilities.Global.IsSystem.Post;
    public int IsSystemMsg
    {
        get { return isSystemMsg; }
        set { isSystemMsg = value; }
    }

    int parentID = 0;
    public int ParentID
    {
        get { return parentID; }
        set { parentID = value; }
    }

    ESP.Media.Entity.PostsInfo currentPost = null;
    public ESP.Media.Entity.PostsInfo CurrentPost
    {
        get { return currentPost; }
        set
        {
            currentPost = value;
            if (currentPost != null)
            {
                this.posttype = currentPost.Type;
                this.isSystemMsg = currentPost.Issysmsg;
            }

        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.userid <= 0) return;
        currentUser = new ESP.Compatible.Employee(userid);

        if (Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentPage] != null && Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentPage].ToString().Trim().Length > 0)
        {
            this.currentPage = Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentPage].ToString();
        }
        if (Request[ESP.Media.Access.Utilities.Global.RequestKey.OperateType] != null && Request[ESP.Media.Access.Utilities.Global.RequestKey.OperateType].ToString().Trim().Length > 0)
        {
            this.OperateType = Convert.ToInt32(Request[ESP.Media.Access.Utilities.Global.RequestKey.OperateType]);
        }
        if (currentPost != null)
        {
            if (posttype == (int)ESP.Media.Access.Utilities.Global.PostType.Reply)
            {
                txtSubject.Text = "Re:" + ESP.Media.BusinessLogic.PostsManager.GetModel(currentPost.Parentid).Subject;
            }
        }
        else
        {
            if (postid > 0 && !IsPostBack)
            {
                currentPost = ESP.Media.BusinessLogic.PostsManager.GetModel(postid);
                this.txtSubject.Text = Server.HtmlDecode(currentPost.Subject);
                this.wtpNew.XMLNText = Server.HtmlDecode(currentPost.Body);
                this.operateType = (int)ESP.Media.Access.Utilities.Global.OperateType.Edit;
                if (!string.IsNullOrEmpty(currentPost.Begindate))
                {
                    this.dpStart.Text = currentPost.Begindate;
                }
                if (!string.IsNullOrEmpty(currentPost.Enddate))
                {
                    this.dpEnd.Text = currentPost.Enddate;
                }
                isSystemMsg = currentPost.Issysmsg;
                if (currentPost.Id == currentPost.Parentid)
                {
                    posttype = (int)ESP.Media.Access.Utilities.Global.PostType.Issue;
                }
            }
            else
            {
                currentPost = new ESP.Media.Entity.PostsInfo();
                currentPost.Id = 0;
                currentPost.Subject = Server.HtmlEncode( txtSubject.Text);
                currentPost.Body =Server.HtmlEncode( wtpNew.XMLNText);
                currentPost.Issysmsg = isSystemMsg;

                currentPost.Begindate = dpStart.Text;
                currentPost.Enddate = dpEnd.Text;
            }
        }

        if (posttype == (int)ESP.Media.Access.Utilities.Global.PostType.Issue && isSystemMsg == (int)ESP.Media.Access.Utilities.Global.IsSystem.SysMsg)//公告则要输入有效时间段
        {
            pTime.Visible = true;
        }
    }
    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        string subject = Server.HtmlEncode(this.txtSubject.Text);
        string content = Server.HtmlEncode(this.wtpNew.XMLNText);
        string starttime = string.IsNullOrEmpty(dpStart.Text) ? DateTime.Now.ToString() : dpStart.Text;
        string endtime = string.IsNullOrEmpty(dpEnd.Text) ? DateTime.Now.ToString() : dpEnd.Text;
        if (subject == null || subject.Length <= 0)
        {
            labMsg.Visible = true;
            labMsg.Text = "请填写主题...";
            return;
        }
        if(content == null || content.Length <= 0)
        {
            labMsg.Visible = true;
            labMsg.Text = "请填写内容...";
            return;
        }
        if (currentUser == null)
        {
            labMsg.Visible = true;
            labMsg.Text = "请登录先...";
            return;
        }
        else
        {
            if (posttype == (int)ESP.Media.Access.Utilities.Global.PostType.Issue)
            {
                currentPost = new ESP.Media.Entity.PostsInfo();
                currentPost.Subject = subject;
                currentPost.Body = content;
                currentPost.Issysmsg = isSystemMsg;
                currentPost.Begindate = starttime;
                currentPost.Enddate = endtime;

                ESP.Media.BusinessLogic.PostsManager.Issue(currentPost,userid);
            }
            else
            {
                if (operateType == (int)ESP.Media.Access.Utilities.Global.OperateType.Add)
                {
                    currentPost = new ESP.Media.Entity.PostsInfo();
                    currentPost.Subject = subject;
                    currentPost.Body = content;
                    currentPost.Parentid = this.parentID;

                    currentPost.Begindate = starttime;
                    currentPost.Enddate = endtime;

                    ESP.Media.BusinessLogic.PostsManager.Reply(currentPost,userid);
                }
                else
                {
                    currentPost = ESP.Media.BusinessLogic.PostsManager.GetModel(postid);
                    currentPost.Subject = subject;
                    currentPost.Body = content;

                    currentPost.Begindate = starttime;
                    currentPost.Enddate = endtime;

                    ESP.Media.BusinessLogic.PostsManager.modify(currentPost, Convert.ToInt32(currentUser.SysID));
                }
            }
            Response.Redirect(currentPage);
        }
    }


    protected void lbtnDel_Click(object sender, EventArgs e)
    {
        Response.Redirect(currentPage);
    }



}
