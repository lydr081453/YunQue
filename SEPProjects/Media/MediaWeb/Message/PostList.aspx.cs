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

public partial class Message_PostList : ESP.Web.UI.PageBase
{
    protected override void OnInit(EventArgs e)
    {
        InitDataGridColumn();
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentPage] = ESP.Media.Access.Utilities.Global.Url.PostList;
        Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] = ESP.Media.Access.Utilities.Global.Url.PostList;
        databind();
    }

    void InitLeft()
    {
  
    }

    /// <summary>
    /// 初始化表格
    /// </summary>
    private void InitDataGridColumn()
    {
        string strColumn = "id#subject#userid#issuedate#lastreplydate#userid";
        string strHeader = "id#主题#发布人#发表时间#最后回复时间#userid";
        string strSort = "id#subject##issuedate#lastreplydate#userid";
        string strH = "##center##center#";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader,strSort,strH, this.dgPost);
    }

    void databind()
    {
        DataTable dtCommon = ESP.Media.BusinessLogic.PostsManager.GetPostMsg();
        dgPost.DataSource = dtCommon.DefaultView;
        dgPost.HideColumns(new int[] { 0,5 });
    }


    void itmEdit_DoSomething(object sender, string value)
    {
    }

    /// <summary>
    /// 发布新帖
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnNewGong_Click(object sender, EventArgs e)
    {
        Response.Redirect(ESP.Media.Access.Utilities.Global.Url.NewPost + "?" + ESP.Media.Access.Utilities.Global.RequestKey.IsSysMsg + "=" + (int)ESP.Media.Access.Utilities.Global.IsSystem.Post);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentPage] = ESP.Media.Access.Utilities.Global.Url.PostList;
        Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] = ESP.Media.Access.Utilities.Global.Url.PostList;
        dgPost.Columns.Clear();
        InitDataGridColumn();
        
        DataTable dtCommon = ESP.Media.BusinessLogic.PostsManager.GetPostMsg(this.txtKey.Text.Trim());
        dgPost.DataSource = dtCommon.DefaultView;
        dgPost.HideColumns(new int[] { 0, 5 });
    }

    protected void dgPost_ItemDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = "<a style='color:Blue;' href = '" + ESP.Media.Access.Utilities.Global.Url.PostDetail + "?" + ESP.Media.Access.Utilities.Global.RequestKey.PostID + "=" + e.Row.Cells[0].Text.Trim() + "'>" + Server.HtmlDecode(e.Row.Cells[1].Text) + "</a>";
            if (!string.IsNullOrEmpty(e.Row.Cells[5].Text))
            {
                ESP.HumanResource.Entity.UsersInfo user = ESP.HumanResource.BusinessLogic.UsersManager.GetModel(Convert.ToInt32(e.Row.Cells[5].Text));
                //"javascript:ShowMsg('" + ESP.Web.UI.PageBase.GetUserInfo(Model.requestor) + "');";
                e.Row.Cells[2].Text = user.LastNameCN + user.FirstNameCN;
            }
            e.Row.Cells[3].Text = e.Row.Cells[3].Text;
            e.Row.Cells[4].Text = e.Row.Cells[4].Text;
        }
    }
}
