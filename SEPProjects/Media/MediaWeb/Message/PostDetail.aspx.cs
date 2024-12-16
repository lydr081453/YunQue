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
using MediaLib;
using System.Text;
public partial class Message_PostDetail : ESP.Web.UI.PageBase
{
    int postid = 1;
    DataTable dtData;
    //ESP.Media.Entity.Users currentUser;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] != null)
            this.hidUrl.Value = Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage].ToString();
        else
            this.hidUrl.Value = "../Message/PostList.aspx";

       // InitDataGridColumn();
        if (Request[ESP.Media.Access.Utilities.Global.RequestKey.PostID] != null && Convert.ToInt32(Request[ESP.Media.Access.Utilities.Global.RequestKey.PostID]) > 0)
        {
            postid = Convert.ToInt32(Request[ESP.Media.Access.Utilities.Global.RequestKey.PostID]);
            newReply.ParentID = postid;
        }
        newReply.UserID = CurrentUserID;
       
        Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentPage] = ESP.Media.Access.Utilities.Global.Url.PostDetail + "?" + ESP.Media.Access.Utilities.Global.RequestKey.PostID + "=" + postid;
        if (Request[ESP.Media.Access.Utilities.Global.RequestKey.EditPostID] != null && Convert.ToInt32(Request[ESP.Media.Access.Utilities.Global.RequestKey.EditPostID]) > 0)
        {
            newReply.PostId = Convert.ToInt32(Request[ESP.Media.Access.Utilities.Global.RequestKey.EditPostID]);
            newReply.OperateType = (int)ESP.Media.Access.Utilities.Global.OperateType.Edit;
            Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentPage] += string.Format("&{0}={1}",ESP.Media.Access.Utilities.Global.RequestKey.OperateType,(int)ESP.Media.Access.Utilities.Global.OperateType.Edit);
        }
        //if (!IsPostBack)
        {
            IssuePost.PostID = postid;
            IssuePost.LoginUserid = CurrentUserID;
        }
        postListBind();

    }

    protected override void OnInit(EventArgs e)
    {
        InitDataGridColumn();
        base.OnInit(e);
    }

    #region 绑定列头
     ///<summary>
     ///初始化表格
     ///</summary>
    private void InitDataGridColumn()
    {
        string strColumn = "id";
        string strHeader = "回复";
        string strH = "center";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, null,strH, this.gvList);
        //gvList.Columns[0].HeaderStyle.Width = new Unit("30%");
        //gvList.Columns[1].HeaderStyle.Width = new Unit("70%");
        
    }
    #endregion

    void  postListBind()
    {
        dtData = ESP.Media.BusinessLogic.PostsManager.GetReplyPostByPostId(postid);
        if (dtData == null)
        {
            dtData = new DataTable();
        }
        gvList.DataSource = dtData.DefaultView;        
    }



    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Message_skins_UserView ucView = this.LoadControl("skins/UserView.ascx") as Message_skins_UserView;
            //ucView.UserID = Convert.ToInt32(e.Row.Cells[0].Text);

            //Message_skins_Post ucPost = this.LoadControl("skins/Post.ascx") as Message_skins_Post;
            //ucPost.UserID = Convert.ToInt32(e.Row.Cells[0].Text);
            //ucPost.PostId = Convert.ToInt32(e.Row.Cells[1].Text);
            //ucPost.LoginUserID = CurrentUserID;


            //e.Row.Cells[0].Controls.Add(ucView);
            //e.Row.Cells[1].Controls.Add(ucPost);
            Message_skins_PostItem pitem = this.LoadControl("skins/PostItem.ascx") as Message_skins_PostItem;
            pitem.PostID = Convert.ToInt32(e.Row.Cells[0].Text);
            pitem.LoginUserid = CurrentUserID;
            e.Row.Cells[0].Controls.Add(pitem);

        }
    }
}
