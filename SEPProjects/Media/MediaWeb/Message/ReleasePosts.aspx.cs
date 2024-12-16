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

public partial class Message_ReleasePosts : ESP.Web.UI.PageBase
{
    protected override void OnInit(EventArgs e)
    {
        InitDataGridColumn();
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentPage] = ESP.Media.Access.Utilities.Global.Url.ReleasePosts;
        Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] = ESP.Media.Access.Utilities.Global.Url.ReleasePosts;
        databind();

        newPost.UserID = CurrentUserID;

        newPost.PostType = (int)ESP.Media.Access.Utilities.Global.PostType.Issue;

        newPost.IsSystemMsg = (int)ESP.Media.Access.Utilities.Global.IsSystem.SysMsg;

        if (!string.IsNullOrEmpty(Request[ESP.Media.Access.Utilities.Global.RequestKey.PostID]))
        {
            newPost.PostId = Convert.ToInt32(Request[ESP.Media.Access.Utilities.Global.RequestKey.PostID]);
            newPost.OperateType = (int)ESP.Media.Access.Utilities.Global.OperateType.Edit;
            newPost.PostType =-1;
        }
    }

    /// <summary>
    /// 初始化表格
    /// </summary>
    private void InitDataGridColumn()
    {
        string strColumn = "id#subject#body#issuedate#lastreplydate";
        string strHeader = "id#主题#公告内容#发表时间#最后回复时间";
        string strSort = "id#subject#body#issuedate#lastreplydate";
        string strHalign = "left#left#left#center#center";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader,strSort,strHalign, this.dgAdmin);

        TemplateField tf = new TemplateField();
        string clientclick = "return confirm('确认要删除吗？');";
        MyControls.GridViewOperate.ImageButtonItem itmDel = new MyControls.GridViewOperate.ImageButtonItem(ESP.Media.Access.Utilities.ConfigManager.DelIconPath, clientclick, "id", true);
        itmDel.DoSomething += new MyControls.GridViewOperate.DoSomethingHandler(itmDel_DoSomething);
        tf.ItemTemplate = itmDel;
        tf.HeaderText = "删除";
        dgAdmin.Columns.Add(tf);
    }

    void itmDel_DoSomething(object sender, string value)
    {

        string errmsg = string.Empty;
        bool res = ESP.Media.BusinessLogic.PostsManager.del(ESP.Media.BusinessLogic.PostsManager.GetModel(Convert.ToInt32(value)),CurrentUserID);
        if (res)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.location=window.location;alert('{0}');", "删除成功！"), true);
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", "删除失败！"), true);
        }
    }

    void databind()
    {
        DataTable dtAdmin = ESP.Media.BusinessLogic.PostsManager.GetSysMsg();
        DataTable dtCommon = ESP.Media.BusinessLogic.PostsManager.GetBlogMsg();
        dgAdmin.DataSource = dtAdmin.DefaultView;
        dgAdmin.HideColumns(new int[] { 0 });
    }

    protected void dgAdmin_ItemDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dv = (DataRowView)e.Row.DataItem;

            e.Row.Cells[1].Text = "<a style='color:Blue;' href = '" + ESP.Media.Access.Utilities.Global.Url.ReleasePosts + "?" + ESP.Media.Access.Utilities.Global.RequestKey.PostID + "=" + e.Row.Cells[0].Text.Trim()+"&"+ESP.Media.Access.Utilities.Global.RequestKey.OperateType+"="+((int)ESP.Media.Access.Utilities.Global.OperateType.Edit).ToString()+"'>" + Server.HtmlDecode(e.Row.Cells[1].Text) + "</a>";

            e.Row.Cells[2].Text = Server.HtmlDecode(dv["body"].ToString());
            
        }
    }
}
