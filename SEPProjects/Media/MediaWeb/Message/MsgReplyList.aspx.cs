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

public partial class Message_MsgReplyList : ESP.Web.UI.PageBase
{
    protected override void OnInit(EventArgs e)
    {
        InitDataGridColumn();
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentPage] = ESP.Media.Access.Utilities.Global.Url.MsgReplyList;
        Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] = ESP.Media.Access.Utilities.Global.Url.MsgReplyList;
        databind();
    }

    void InitLeft()
    {
    }

    /// <summary>
    /// 初始化表格
    /// </summary>
    private void InitDataGridColumn()
    {        string strColumn = "id#subject#issuedate#lastreplydate";
        string strHeader = "id#主题#发表时间#最后回复时间";
        string strSort = "id#subject#issuedate#lastreplydate";
        string strH = "##center#center";
        MyControls.GridViewOperate.Href href = new MyControls.GridViewOperate.Href();
        
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader,strSort,strH, this.dgAdmin);

        href.Url = ESP.Media.Access.Utilities.Global.Url.PostDetail;
        href.Paramname = ESP.Media.Access.Utilities.Global.RequestKey.PostID;
        href.ParamCellindex = 0;
        href.UrlCellindex = 1;
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader,strSort,strH, href, this.dgPost);
    }

    void databind()
    {
        DataTable dtAdmin = ESP.Media.BusinessLogic.PostsManager.GetSysMsg();
        DataTable dtCommon = ESP.Media.BusinessLogic.PostsManager.GetBlogMsg();
        dgAdmin.DataSource = dtAdmin.DefaultView;
        dgPost.DataSource = dtCommon.DefaultView;
        dgPost.HideColumns(new int[] { 0 });
        dgAdmin.HideColumns(new int[] { 0 });
    }

    protected void dgAdmin_ItemDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = "<a style='color:Blue;' href = '" + ESP.Media.Access.Utilities.Global.Url.PostDetail + "?" + ESP.Media.Access.Utilities.Global.RequestKey.PostID + "=" + e.Row.Cells[0].Text.Trim() + "'>" + Server.HtmlDecode(e.Row.Cells[1].Text) + "</a>";
            e.Row.Cells[2].Text = e.Row.Cells[2].Text;
            e.Row.Cells[3].Text = e.Row.Cells[3].Text;
        }
    }
    protected void dgPost_ItemDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[1].Text = "<a style='color:Blue;' href = '" + ESP.Media.Access.Utilities.Global.Url.PostDetail + "?" + ESP.Media.Access.Utilities.Global.RequestKey.PostID + "=" + e.Row.Cells[0].Text.Trim() + "'>" + Server.HtmlDecode(e.Row.Cells[1].Text) + "</a>";
            e.Row.Cells[2].Text = e.Row.Cells[2].Text;
            e.Row.Cells[3].Text = e.Row.Cells[3].Text;
        }
    }
}
