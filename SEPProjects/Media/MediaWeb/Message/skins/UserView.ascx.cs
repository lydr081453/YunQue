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


public partial class Message_skins_UserView : System.Web.UI.UserControl
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
    const string constImgUrl = @"\UserIHeadmage\unKnow.jpg";
    string imgUrl = @"\UserHeadImage\unKnow.jpg";
    public string ImgUrl
    {
        get { return imgUrl; }
        set { imgUrl = value; this.imgUser.ImageUrl = this.imgUrl; }
    }

    string nickname = string.Empty;
    public string NickName
    {
        get { return nickname; }
        set { nickname = value; this.labUserNickName.Text = this.nickname; }
    }
    string sex = string.Empty;
    public string Sex
    {
        get { return sex; }
        set { sex = value; this.labSex.Text = this.sex; }
    }
    string status = string.Empty;
    public string Status
    {
        get { return status; }
        set { status = value; this.labStatus.Text = this.status; }
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.userid <= 0) return;
        currentUser = new ESP.Compatible.Employee(userid);
        if (currentUser != null)
        {
            try
            {
                //if (currentUser.Image.Length <= 0)
                {
                    this.imgUser.ImageUrl = this.imgUrl;
                }
                this.labNickName.Text = currentUser.Name;
                this.labSex.Text = "";
                this.labStatus.Text = currentUser.ITCode.ToString();
            }
            catch { }
        }
        else
        {
            this.imgUser.ImageUrl = constImgUrl;
            this.labNickName.Text = "游客";
            this.labSex.Text = "保密";
            this.labStatus.Text = "未登录";
        }
    }
}
