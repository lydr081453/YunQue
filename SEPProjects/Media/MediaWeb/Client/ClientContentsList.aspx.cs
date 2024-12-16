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

public partial class Client_ClientContentsList : ESP.Web.UI.PageBase
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
        string strColumn = "version#ClientLogo#ClientCFullName#ClientCShortName#ClientEFullName#ClientEShortName#ClientDescription";
        string strHeader = "版本号#Logo#中文全称#中文简称#英文全称#英文简称#描述";
        string sort = "##ClientCFullName#ClientCShortName#ClientEFullName#ClientEShortName#";
        string strH = "#center#####";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader,sort, strH,this.dgList);
    }
    #endregion

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["alert"]) && Request["alert"].Length > 0)
        {
            alertvalue = Convert.ToInt32(Request["alert"]);
        }
        ListBind();
        Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] =  ESP.Media.Access.Utilities.Global.Url.ClientContentsList+"?Cid="+ Request["Cid"];
        if (alertvalue == 1)
        {
            //Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] = ESP.Media.Access.Utilities.Global.Url.ProductLineContentsList + "?alert="+(alertvalue).ToString()+"&Plid=" + Request["Plid"];
            btnBack.Visible = false;
        }
        else
        {
            //Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] = ESP.Media.Access.Utilities.Global.Url.ProductLineContentsList + "?Plid=" + Request["Plid"];
            btnBack.Visible = true;
        }
        if (alertvalue <= 0)
        {
            btnClose.Visible = false;
        }
    }

    #region 绑定列表
    /// <summary>
    /// Lists the bind.
    /// </summary>
    private void ListBind()
    {
       
        if (Request["Cid"] != null)
        {
            DataTable dt = ClientsManager.GetHistListByClientID(Convert.ToInt32(Request["Cid"]));
            this.dgList.DataSource = dt.DefaultView;
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
    /// Handles the RowDataBound event of the dgList control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
    protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int id = Convert.ToInt32(dgList.DataKeys[e.Row.RowIndex].Value.ToString());
            string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
            
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "Cid", Request["Cid"]);
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "Ccid", id.ToString());
             param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (alertvalue + 1).ToString());
            if (e.Row.Cells[1].Text == "&nbsp;")
            {
                if (alertvalue == 0)
                {
                    e.Row.Cells[1].Text = string.Format("<img  style='width:80px;height:60px;' runat='server' src='{1}' onclick=\"window.open('/Client/ClientContentsDetail.aspx?{0}','','{2}');\"/>", param, ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath,ESP.Media.Access.Utilities.Global.OpenClass.Common);
                }
                else
                {
                    e.Row.Cells[1].Text = string.Format("<img  style='width:80px;height:60px;' runat='server' src='{1}' onclick=\"window.location='/Client/ClientContentsDetail.aspx?{0}';\"/>", param, ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath);
                }
            }
            else
            {
                if (alertvalue == 0)
                {
                    e.Row.Cells[1].Text = string.Format("<img style='width:80px;height:60px;'  runat='server'  src='{1}' onclick=\"window.open ( '/Client/ClientContentsDetail.aspx?{0}','','{2}');\"/>", param, e.Row.Cells[1].Text.Substring(e.Row.Cells[1].Text.IndexOf("~") + 1),ESP.Media.Access.Utilities.Global.OpenClass.Common);
                }
                else
                { 
                    e.Row.Cells[1].Text = string.Format("<img style='width:80px;height:60px;'  runat='server'  src='{1}' onclick=\"window.location = '/Client/ClientContentsDetail.aspx?{0}';\"/>", param, e.Row.Cells[1].Text.Substring(e.Row.Cells[1].Text.IndexOf("~") + 1));
                }
            }
            e.Row.Cells[2].Text = string.Format("<a href='#' onclick=\"window.location = '/Client/ClientContentsDetail.aspx?{0}';\" >{1}</a>", param, e.Row.Cells[2].Text);
            e.Row.Cells[1].Width = 120;
            e.Row.Cells[1].Wrap = false;
            e.Row.Cells[6].Width = 200;
        }
    }
    #endregion

    /// <summary>
    /// Handles the Click event of the btnBack control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request["Cid"] != null)
        {
            string param = Request.Url.Query;
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (alertvalue - 1).ToString());
            if (param.Length > 0)
            {
                param = "?" + param;
            }
            Response.Redirect(string.Format("ClientDetail.aspx{0}", param));
        }
    }
}
