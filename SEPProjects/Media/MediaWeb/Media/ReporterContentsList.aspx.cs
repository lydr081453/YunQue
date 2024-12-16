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
public partial class Media_ReporterContentsList : ESP.Web.UI.PageBase
{
    int alertvalue = 0;

    /// <summary>
    /// Raises the <see cref="E:Init"/> event.
    /// </summary>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    override protected void OnInit(EventArgs e)
    {
        InitDataGridColumn();
        base.OnInit(e);
        int userid = CurrentUserID;
        if (!string.IsNullOrEmpty(Request["alert"]))
        {
            if(Request["alert"] != "1")
                btnBack.Visible = true;
            alertvalue = int.Parse(Request["alert"]);
        }
    }

    #region 绑定列头
    /// <summary>
    /// 初始化表格
    /// </summary>
    private void InitDataGridColumn()
    {
        string strColumn = "id#version#reportername#medianame#sex#ReporterPosition#responsibledomain#mobile#tel#email";
        string strHeader = "选择#版本号#姓名#所属媒体#性别#职务#负责领域#手机#固话#邮箱";
        string sort = "##ReporterName#medianame#sex#ReporterPosition#responsibledomain###";
        string strH = "center#########";
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
        Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] = ESP.Media.Access.Utilities.Global.Url.ReporterContentsList;
    }

    #region 绑定列表
    /// <summary>
    /// Lists the bind.
    /// </summary>
    private void ListBind()
    {
        DataTable dt=new DataTable();
        if (Request["Rid"] != null)
        {
            dt = ESP.Media.BusinessLogic.ReportersManager.GetHistListByClientID(Convert.ToInt32(Request["Rid"]));
        }
        
        this.dgList.DataSource = dt.DefaultView;
        for(int i=0;i<dgList.Columns.Count;i++)
        {
            dgList.Columns[i].HeaderStyle.Wrap = false;
        }

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
            e.Row.Cells[0].Visible = false;
            string param = Request.Url.Query.Substring(Request.Url.Query.IndexOf('?') + 1);
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "Hisid", e.Row.Cells[0].Text);
            if (alertvalue > 0)
            {
                param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (alertvalue + 1).ToString());
            }
            e.Row.Cells[2].Text = string.Format("<a href='ReporterContentsDisplay.aspx?{0}' >{1}</a>", param,e.Row.Cells[2].Text);
        }
        else if(e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
    }

    /// <summary>
    /// Gets the work string.
    /// </summary>
    /// <param name="xml">The XML.</param>
    /// <returns></returns>
    private string GetWorkString(string xml)
    {
        xml = Server.HtmlDecode(xml);
        Media_skins_Experience.InitExperienceTable();
        DataTable dt = Media_skins_Experience.ExperienceTable.Clone();
        System.IO.StringReader sr = new System.IO.StringReader(xml);
        dt.ReadXml(sr);
        if (dt.Rows.Count > 0)
            return dt.Rows[0]["单位名称"].ToString();
        else
            return xml;
    }

    /// <summary>
    /// Handles the Sorting event of the dgList control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewSortEventArgs"/> instance containing the event data.</param>
    protected void dgList_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (e.SortDirection == SortDirection.Ascending)
        {
            e.SortDirection = SortDirection.Descending;
        }
        else if (e.SortDirection == SortDirection.Descending)
        {
            e.SortDirection = SortDirection.Ascending;
        }
    }

    /// <summary>
    /// Handles the Click event of the btnBack control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request["Rid"] != null)
        {
            string param = Request.Url.Query;
            param = ESP.Media.Access.Utilities.Global.AddParam(param, "alert", (alertvalue - 1).ToString());
            if (param.Length > 0)
            {
                param = "?" + param;
            }
            Response.Redirect(string.Format("ReporterDisplay.aspx{0}", param));
        }
    }
}
