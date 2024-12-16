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
public partial class Media_WebMediaContentsList : ESP.Web.UI.PageBase
{
    int alertvalue = 0;

    /// <summary>
    /// Raises the <see cref="E:Init"/> event.
    /// </summary>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    override protected void OnInit(EventArgs e)
    {
        InitDataGridColumn();
        InitializeComponent();
        base.OnInit(e);
        int userid = CurrentUserID;
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
        string strColumn = "version#medianame#MediumSort#IssueRegion#IndustryName#headquarter#TelephoneExchange";
        string strHeader = "版本号#媒体名称#形态#发行区域#行业属性#总部所在地#总机";
        string strSort = "#mediacname#####";       
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, strSort, this.dgList);
    }
    #endregion

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["alert"]))
        {
            alertvalue = int.Parse(Request["alert"]);
        }
        
        ListBind();
        Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] = ESP.Media.Access.Utilities.Global.Url.WebMediaContentsList + "?Mid=" + Request["Mid"];
    }

    #region 绑定列表
    /// <summary>
    /// Lists the bind.
    /// </summary>
    private void ListBind()
    {
        DataTable dt = new DataTable();
        if (Request["Mid"] != null)
        {

            dt = ESP.Media.BusinessLogic.MediaitemsManager.GetHistListByMediaitemID(Convert.ToInt32(Request["Mid"])); ;
        }

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

            int id = int.Parse(dgList.DataKeys[e.Row.RowIndex].Value.ToString());
            e.Row.Cells[1].Text = string.Format("<a href='MediaContentsDisplay.aspx?backurl=WebMediaContentsList.aspx&LMid={0}&Mid={1}&alert={2}' >" + e.Row.Cells[1].Text + "</a>", id, Request["Mid"], alertvalue);        
        }
    }

    /// <summary>
    /// Handles the Sorting event of the dgList control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewSortEventArgs"/> instance containing the event data.</param>
    protected void dgList_Sorting(object sender, GridViewSortEventArgs e)
    {
        //
    }

    /// <summary>
    /// Handles the Click event of the btnBack control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request["Mid"] != null)
        {
            Response.Redirect(string.Format("MediaDisplay.aspx?Mid={0}", Request["Mid"]));
        }
    }
}
