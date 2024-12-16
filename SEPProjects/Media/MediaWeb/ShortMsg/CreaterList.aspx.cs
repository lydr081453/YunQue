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
using System.Xml.Linq;

public partial class ShortMsg_CreaterList : ESP.Web.UI.PageBase
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
        string strColumn = "SysUserID#UserName";
        string strHeader = "选择#创建人名";
        string sort = "##SysUserID#UserName";
        string strH = "center##";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, sort,strH, this.dgList);
    }
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {


        ListBind();
      //  dgList.Columns[5].Visible = false;
     //   Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] = ESP.Media.Access.Utilities.Global.Url.ProductSelectClientList;

    }

    #region 添加
    protected void btnAdd_Click(object sender, System.EventArgs e)
    {
        Response.Redirect(string.Format("ClientAddAndEdit.aspx?Operate=ADD&CientOperate={0}", Request["Operate"]));
    }
    #endregion

    #region 绑定列表
    private void ListBind()
    {
        StringBuilder str = new StringBuilder();
        Hashtable ht = new Hashtable();
        DataTable dt = new DataTable();
        if (txtCreaterName.Text.Trim() != "")
            dt = ESP.Media.BusinessLogic.MailmsgManager.GetCreater(txtCreaterName.Text.Trim());
        else
            dt = ESP.Media.BusinessLogic.MailmsgManager.GetCreaterList();

        this.dgList.DataSource = dt.DefaultView;
    }

    protected void dgList_Sorting(object sender, GridViewSortEventArgs e)
    {


    }

    protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = string.Format("<input type=radio id=radNo name=radNo value={0} runat= 'server'  onclick='selectone(this)'/>", e.Row.Cells[0].Text + "," + e.Row.Cells[1].Text);

        }
    }
    #endregion

    #region 返回总库
    protected void btnClear_OnClick(object sender, EventArgs e)
    {       
        this.txtCreaterName.Text = string.Empty;        

        ListBind();
    }
    #endregion
}
