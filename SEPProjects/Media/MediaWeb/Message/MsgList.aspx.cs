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

public partial class Message_MsgList : ESP.Web.UI.PageBase
{
    protected override void OnInit(EventArgs e)
    {
        InitDataGridColumn();
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentPage] = ESP.Media.Access.Utilities.Global.Url.MsgList;
        Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] = ESP.Media.Access.Utilities.Global.Url.MsgList;
        //if (!IsPostBack)
        //{
        //    Queue q = Queue();
        //    q.Clear();
        //    (Session["PagePath"] as Queue).Enqueue(ESP.Media.Access.Utilities.Global.Url.MsgList);
        //}
        //if (!IsPostBack)
        //{
        //    (Session["PagePath"] as Queue).Enqueue(ESP.Media.Access.Utilities.Global.Url.MsgList);
        //}
        //if (!IsPostBack)
        //{
        //    Queue q = new Queue();
        //    q.Dequeue();
        //}
        databind();
    }

    void InitLeft()
    {
        labMsg.Visible = false;
    }

    /// <summary>
    /// 初始化表格
    /// </summary>
    private void InitDataGridColumn()
    {
        //string strColumn = "id#subject#usernickname#issuedate#lastReplyUserName#lastreplydate";
        //string strHeader = "id#主题#作者#发表时间#最后回复人#最后回复时间";
        //string strSort = "id#subject#usernickname#issuedate#lastReplyUserName#lastreplydate";

        string strColumn = "id#subject#body#issuedate#lastreplydate";
        string strHeader = "id#主题#公告内容#发表时间#最后回复时间";
        string strSort = "id#subject#body#issuedate#lastreplydate";
        string strHalign = "left#left#left#center#center";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader,strSort,strHalign, this.dgAdmin);



         strColumn = "id#subject#userid#userid#issuedate#lastreplydate";
         strHeader = "id#主题#发布人员工编号#发布人#发表时间#最后回复时间";
         strSort = "id#subject#body#userid#userid#issuedate#lastreplydate";
         strHalign = "left#left#left#left#left#center#center";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader,strSort,strHalign, this.dgPost);



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
        dgPost.DataSource = dtCommon.DefaultView;
        dgPost.HideColumns(new int[] { 0 });
        dgAdmin.HideColumns(new int[] { 0 });
    }

    protected void lbtnNewGong_Click(object sender, EventArgs e)
    {
        Response.Redirect(ESP.Media.Access.Utilities.Global.Url.NewPost + "?" + ESP.Media.Access.Utilities.Global.RequestKey.IsSysMsg + "=" + (int)ESP.Media.Access.Utilities.Global.IsSystem.SysMsg);
    }

    /// <summary>
    /// 发布新博客
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnNew_Click(object sender, EventArgs e)
    {
         Response.Redirect(ESP.Media.Access.Utilities.Global.Url.NewPost+"?"+ESP.Media.Access.Utilities.Global.RequestKey.IsSysMsg+"="+(int)ESP.Media.Access.Utilities.Global.IsSystem.Blog);
    }
    protected void dgAdmin_ItemDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dv = (DataRowView)e.Row.DataItem;

            e.Row.Cells[1].Text = "<a style='color:Blue;' href = '" + ESP.Media.Access.Utilities.Global.Url.PostDetail + "?" + ESP.Media.Access.Utilities.Global.RequestKey.PostID + "=" + e.Row.Cells[0].Text.Trim() + "'>" + Server.HtmlDecode(e.Row.Cells[1].Text) + "</a>";
            //string content =Server.HtmlDecode( e.Row.Cells[2].Text);

            e.Row.Cells[2].Text = Server.HtmlDecode(dv["body"].ToString());
            
        }
    }
    protected void dgPost_ItemDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = "<a style='color:Blue;' href = '" + ESP.Media.Access.Utilities.Global.Url.PostDetail + "?" + ESP.Media.Access.Utilities.Global.RequestKey.PostID + "=" + e.Row.Cells[0].Text.Trim() + "'>" + Server.HtmlDecode(e.Row.Cells[1].Text) + "</a>";
            int userid = Convert.ToInt32(e.Row.Cells[2].Text);
            ESP.Compatible.Employee emp = new ESP.Compatible.Employee(userid);
            e.Row.Cells[2].Text = emp.ID;
            e.Row.Cells[3].Text = emp.Name;
        }
    }
}
