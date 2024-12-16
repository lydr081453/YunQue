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
using ESP.Compatible;

public partial class Media_IndustryList : ESP.Web.UI.PageBase
{
    override protected void OnInit(EventArgs e)
    {
        InitDataGridColumn();
        InitializeComponent();
        base.OnInit(e);
        int userid = CurrentUserID;
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
        string strColumn = "IndustryName#IndustryID#IndustryID";
        string strHeader = "媒体行业属性名称#编辑#删除";
        string strSort = "##";
        string strH = "#center#center";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, strSort,strH, this.dgList);
    }
    #endregion



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] = ESP.Media.Access.Utilities.Global.Url.MediaList;           
        }

        ListBind();

    }
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        this.txtName.Text = string.Empty;
        ListBind();
    }



    #region 绑定列表
    private void ListBind()
    {
        StringBuilder strTerms = new StringBuilder();
        Hashtable ht = new Hashtable();

        if (txtName.Text.Length > 0)
        {
            strTerms.Append("industryname like '%'+@industryname+'%'");
            ht.Add("@industryname",txtName.Text);
        }

        DataTable dt = ESP.Media.BusinessLogic.IndustriesManager.GetList(strTerms.ToString(),ht);
        if (dt == null)
        {
            dt = new DataTable();
        }

        this.dgList.DataSource = dt.DefaultView;
    }
    #endregion

    protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Cells[1].Text = string.Format("<a href='NewIndustry.aspx?Operate=EDIT&Iid={0}' ><img src='{1}' /></a>", e.Row.Cells[1].Text, ESP.Media.Access.Utilities.ConfigManager.EditIconPath);

            e.Row.Cells[2].Text = string.Format("<a href='NewIndustry.aspx?Operate=Del&Iid={0}' onclick= \"return confirm( '真的要删除吗?');\" ><img src='{1}' /></a>", e.Row.Cells[2].Text, ESP.Media.Access.Utilities.ConfigManager.DelIconPath);

        }
    }


    protected void dgList_Sorting(object sender, GridViewSortEventArgs e)
    {

    }

    #region 查找
    protected void btnSearch_Click(object sender, System.EventArgs e)
    {

    }
    #endregion


    #region 添加
    protected void btnAdd_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("NewIndustry.aspx?Operate=ADD");
    }
    #endregion


}
