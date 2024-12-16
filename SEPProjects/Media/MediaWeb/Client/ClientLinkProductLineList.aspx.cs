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
using ESP.Media.BusinessLogic;

public partial class Client_ClientLinkProductLineList : ESP.Web.UI.PageBase
{
    override protected void OnInit(EventArgs e)
    {
        InitDataGridColumn();
        InitializeComponent();
        base.OnInit(e);
    }

    private void InitializeComponent()
    {

    }

    #region 绑定列头
    /// <summary>
    /// 初始化表格 
    /// </summary>
    private void InitDataGridColumn()
    {
        string strColumn = "ProductLineID#ProductLineTitle#ProductLineName#ProductLineDescription";
        string strHeader = "选择#产品线图片#产品线名称#描述";
        string sort = "###ProductLineName#ProductLineDescription";
        string strH = "center#center##";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort,strH, this.dgList);
    }
    #endregion

    int alertvalue = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["alert"]) && Request["alert"].Length > 0)
        {
            alertvalue = Convert.ToInt32(Request["alert"]);
        }
        ListBind();
        string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);

        param = ESP.Media.Access.Utilities.Global.AddParam(param, "Cid", Request["Cid"]);
        Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] = ESP.Media.Access.Utilities.Global.Url.ClientLinkProductLineList + string.Format("?{0}",param);
    //  dgList.Columns[5].Visible = false;
    }

    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        this.txtProductLineName.Text = string.Empty;
        this.txtDesKey.Text = string.Empty;
        ListBind();
    }

    #region 添加
    protected void btnAdd_Click(object sender, System.EventArgs e)
    {
        string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);

        param = ESP.Media.Access.Utilities.Global.AddParam(param, "Operate", "ADD");
        param = ESP.Media.Access.Utilities.Global.AddParam(param, "Cid", Request["Cid"]);
        Response.Redirect(string.Format("ProductLineAddAndEdit.aspx?{0}" , param));
    }
    #endregion

    #region 绑定列表
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
        str.Append(" and clientid=0 ");//sxc 只能显示未被关联的产品线
        DataTable dt = ProductlinesManager.GetList(str.ToString(), ht);
        this.dgList.DataSource = dt.DefaultView;
    }
    #region 关联到产品线
    protected void btnLink_Click(object sender, EventArgs e)
    {
        string[] ss = hidChecked.Value.Trim(',', ' ').Split(',');
        int[] ls = new int[ss.Length];
        for (int i = 0; i < ss.Length; i++)
        {
            ls[i] = Convert.ToInt32(ss[i]);
        }
        if (Request["Cid"] != null)
        {
            string errmsg;
            int ret = ProductlinesManager.LinkToPoductline(ls, Convert.ToInt32(Request["Cid"]), out errmsg);
            if (ret > 0)
            {
               // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("opener.location = opener.location;alert('{0}');", "添加成功"), true);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "opener.location = opener.location;alert('保存成功！');window.close();", true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);
            }
        }
    }
    #endregion
    protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);

            
            int id = Convert.ToInt32(dgList.DataKeys[e.Row.RowIndex].Value.ToString());
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "Plid", id.ToString());
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (alertvalue + 1).ToString());
            e.Row.Cells[0].Text = string.Format("<input type='checkbox' onclick='selected(this)' value='{0}'/>", e.Row.Cells[0].Text);

            if (e.Row.Cells[1].Text == "&nbsp;")
            {
                e.Row.Cells[1].Text = string.Format("<img  src='{0}'style='width:80px;height:60px;'  runat='server' onclick=\"window.location='ProductLineDisplay.aspx?{1}';\"/>", ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath, param);
            }
            else
            {
                e.Row.Cells[1].Text = string.Format("<img  src='{0}'style='width:80px;height:60px;'  runat='server' onclick=\"window.location='ProductLineDisplay.aspx?{1}';\"/>", e.Row.Cells[1].Text.Substring(e.Row.Cells[1].Text.IndexOf("~") + 1),param);
            }
        
            e.Row.Cells[1].Wrap = false;

            e.Row.Cells[2].Text = string.Format("<a href='#' onclick=\"window.location='ProductLineDisplay.aspx?{0}';\" >{1}</a>", param, e.Row.Cells[2].Text);

           
           
        }
    }
}
    #endregion
