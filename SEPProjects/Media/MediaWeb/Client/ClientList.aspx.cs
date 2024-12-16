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
using System.Text;
using ESP.Media.BusinessLogic;
//using System.Collections;


public partial class Client_ClientList : ESP.Web.UI.PageBase
{
    /// <summary>
    /// Raises the <see cref="E:Init"/> event.
    /// </summary>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    override protected void OnInit(EventArgs e)
    {
        InitDataGridColumn();
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    /// Initializes the component.
    /// </summary>
    private void InitializeComponent()
    {

    }

    #region 绑定列头
    /// <summary>
    /// 初始化表格
    /// </summary>
    private void InitDataGridColumn()
    {
        string strColumn = "ClientLogo#ClientCFullName#ClientCShortName#ClientEFullName#ClientEShortName#categoryname#ClientID#ClientID";
        string strHeader = "Logo#中文全称#中文简称#英文全称#英文简称#分类#编辑#删除";
        string sort = "#ClientCFullName#ClientCShortName#ClientEFullName#ClientEShortName#categoryname####";
        string strH = "center######center#center";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort,strH, this.dgList);
    }
    #endregion

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        ListBind();
       // dgList.Columns[5].Visible = false;
        Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] = ESP.Media.Access.Utilities.Global.Url.ClientList;

    }

    /// <summary>
    /// Handles the OnClick event of the btnClear control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        this.txtChFullName.Text = string.Empty;
        this.txtChShortName.Text = string.Empty;
        this.txtEnFullName.Text = string.Empty;
        this.txtEnShortName.Text = string.Empty;
        ListBind();
    }

    #region 添加
    /// <summary>
    /// Handles the Click event of the btnAdd control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnAdd_Click(object sender, System.EventArgs e)
    {
        Response.Redirect(string.Format("ClientAddAndEdit.aspx?Operate=ADD&CientOperate={0}", Request["Operate"]));
    }
    #endregion

    #region 绑定列表
    /// <summary>
    /// Lists the bind.
    /// </summary>
    private void ListBind()
    {
        StringBuilder str = new StringBuilder();
        Hashtable ht = new Hashtable();
        if (txtChFullName.Text.Trim() != "")
        {
            str.Append(" and ClientCFullName like '%'+@cfname+'%'");
            ht.Add("@cfname", txtChFullName.Text.Trim());
        }
        if (txtChShortName.Text.Trim() != string.Empty)
        {
            str.Append(" and ClientCShortName like '%'+@csname+'%'");
            ht.Add("@csname", txtChShortName.Text.Trim());
        }
        if (txtEnFullName.Text.Trim() != string.Empty)
        {
            str.Append(" and ClientEFullName like '%'+@efname+'%'");
            ht.Add("@efname", txtEnFullName.Text.Trim());
        }
        if (txtEnShortName.Text.Trim() != string.Empty)
        {
            str.Append(" and ClientEShortName like '%'+@esname+'%'");
            ht.Add("@esname", txtEnShortName.Text.Trim());
        }
        DataTable dt = ClientsManager.GetList(str.ToString(), ht);
        this.dgList.DataSource = dt.DefaultView;
    }

    /// <summary>
    /// Handles the Sorting event of the dgList control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewSortEventArgs"/> instance containing the event data.</param>
    protected void dgList_Sorting(object sender, GridViewSortEventArgs e)
    {
    }

    /// <summary>
    /// Handles the RowDataBound event of the dgList control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
    protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text == "&nbsp;")
            {
                e.Row.Cells[0].Text = string.Format("<a href='ClientDetail.aspx?Cid={0}&CientOperate={1}'><img   src='{2}'class='ThumbnailPhoto' runat='server' /></a>", e.Row.Cells[6].Text, Request["Operate"], ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath);
            }
            else
            {
                e.Row.Cells[0].Text = string.Format("<a href='ClientDetail.aspx?Cid={0}&CientOperate={1}'><img   src='{2}'class='ThumbnailPhoto' runat='server' /></a>", e.Row.Cells[6].Text, Request["Operate"], e.Row.Cells[0].Text.Substring(e.Row.Cells[0].Text.IndexOf("~") + 1));
            }

            e.Row.Cells[0].Width = 120;
            e.Row.Cells[0].Wrap = false;

            e.Row.Cells[1].Text = string.Format("<a href='ClientDetail.aspx?Cid={0}&CientOperate={1}'>{2}</a>", e.Row.Cells[6].Text, Request["Operate"], e.Row.Cells[1].Text);

            e.Row.Cells[5].Width = 200;
            string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "Cid", e.Row.Cells[7].Text);
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", "1");
            param = ESP.Media.Access.Utilities.Global.RemoveParam(param, "backurl");
            
            e.Row.Cells[6].Text = string.Format("<a href='ClientAddAndEdit.aspx?Operate=EDIT&Cid={0}&CientOperate={1}' ><img   runat='server' src='{2}' /></a>", e.Row.Cells[6].Text, Request["Operate"], ESP.Media.Access.Utilities.ConfigManager.EditIconPath);

            e.Row.Cells[7].Text = string.Format("<a href='ClientAddAndEdit.aspx?Operate=DEL&Cid={0}&CientOperate={1}' onclick= \"return confirm( '真的要删除吗?');\" ><img src='{2}' /></a>", e.Row.Cells[7].Text, Request["Operate"], ESP.Media.Access.Utilities.ConfigManager.DelIconPath);

        }
    }
    #endregion
}
