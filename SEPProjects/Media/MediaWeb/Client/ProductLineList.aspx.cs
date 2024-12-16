using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Client_ProductLineList : ESP.Web.UI.PageBase
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
        //
    }

    #region 绑定列头
    /// <summary>
    /// 初始化表格
    /// </summary>
    private void InitDataGridColumn()
    {
        string strColumn = "ProductLineTitle#ProductLineName#ProductLineDescription#ProductLineID#ProductLineID";
        string strHeader = "产品线图片#产品线名称#描述#编辑#删除";
        string sort = "#ProductLineName#ProductLineDescription#####";
        string strH = "center###center#center";
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
        Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] = ESP.Media.Access.Utilities.Global.Url.ProductLineList;
    }

    /// <summary>
    /// Handles the OnClick event of the btnClear control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        this.txtProductLineName.Text = string.Empty;
        this.txtDesKey.Text = string.Empty;
        this.txtChFullName.Text = string.Empty;
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
        Response.Redirect("ProductLineAddAndEdit.aspx?Operate=ADD");
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
        if (txtProductLineName.Text.Trim() != "")
        {
            str.Append(" and ProductLineName like '%'+@ProductLineName+'%'");
            ht.Add("@ProductLineName", txtProductLineName.Text.Trim());
        }
        if (txtDesKey.Text.Trim() != string.Empty)
        {
            str.Append(" and ProductLineDescription like '%'+@csname+'%'");
            ht.Add("@csname", txtDesKey.Text.Trim());
        }

        if (txtChFullName.Text.Length > 0)
        {
            str.Append(" and b.clientcfullname like '%'+@clientcfullname+'%'");
            ht.Add("@clientcfullname", txtChFullName.Text);
        }
        DataTable dt = ESP.Media.BusinessLogic.ProductlinesManager.GetListWithClient(str.ToString(), ht);
        this.dgList.DataSource = dt.DefaultView;
    }
    #endregion

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
                e.Row.Cells[0].Text = string.Format("<img src='{1}' class='ThumbnailPhoto'  runat='server' onclick=\"window.location='ProductLineDisplay.aspx?Plid={0}';\"/>", e.Row.Cells[3].Text, ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath);
            }
            else
            {
                e.Row.Cells[0].Text = string.Format("<img src='{1}' class='ThumbnailPhoto'  runat='server' onclick=\"window.location='ProductLineDisplay.aspx?Plid={0}';\"/>", e.Row.Cells[3].Text, e.Row.Cells[0].Text.Substring(e.Row.Cells[0].Text.IndexOf("~") + 1));
            }
        
            e.Row.Cells[0].Wrap = false;

            e.Row.Cells[1].Text = string.Format("<a href='ProductLineDisplay.aspx?Plid={0}' >{1}</a>", e.Row.Cells[3].Text, e.Row.Cells[1].Text);

            string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "Plid", e.Row.Cells[4].Text);
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", "1");
            param = ESP.Media.Access.Utilities.Global.RemoveParam(param, "backurl");
            e.Row.Cells[3].Text = string.Format("<a href='ProductLineAddAndEdit.aspx?Operate=EDIT&Plid={0}' ><img src='{1}' /></a>", e.Row.Cells[3].Text, ESP.Media.Access.Utilities.ConfigManager.EditIconPath);
            e.Row.Cells[4].Text = string.Format("<a href='ProductLineAddAndEdit.aspx?Operate=DEL&Plid={0}' onclick= \"return confirm( '真的要删除吗?');\" ><img src='{1}' /></a>", e.Row.Cells[4].Text, ESP.Media.Access.Utilities.ConfigManager.DelIconPath);
        }
    }
}
