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
public partial class Media_MediaList : ESP.Web.UI.PageBase
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
        string strColumn = "mediaitemid#medianame#MediumSort#IssueRegion#IndustryName#TelephoneExchange#mediaitemid#mediaitemid";
        string strHeader = "选择#媒体名称#形态#地域属性#行业属性#总机#编辑#删除";
        string strSort = "#mediacname########";
        string strH = "center#######center#center";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, strSort,strH,this.dgList);
    }
    #endregion


    private void getIndustry()
    {
        DataTable dtindustry = ESP.Media.BusinessLogic.IndustriesManager.GetAllList();
        if (dtindustry != null && dtindustry.Rows.Count > 0)
        {
            ddlIndustry.DataSource = dtindustry;
            ddlIndustry.DataTextField = ESP.Media.Access.Utilities.Global.DataTextField.Industry;
            ddlIndustry.DataValueField = ESP.Media.Access.Utilities.Global.DataValueField.Industry;
            ddlIndustry.DataBind();
        }
        ddlIndustry.Items.Insert(0, new ListItem("请选择", "0"));
    }


    private void getMediaType()
    {
        DataTable dttype = ESP.Media.BusinessLogic.MediatypeManager.GetAllList();
        if (dttype != null && dttype.Rows.Count > 0)
        {
            ddlMediaType.DataSource = dttype;
            ddlMediaType.DataTextField = ESP.Media.Access.Utilities.Global.DataTextField.MediaType;
            ddlMediaType.DataValueField = ESP.Media.Access.Utilities.Global.DataValueField.MediaType;
            ddlMediaType.DataBind();
        }
        ddlMediaType.Items.Insert(0, new ListItem("请选择", "0"));
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            getIndustry();
            getMediaType();
            Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] = ESP.Media.Access.Utilities.Global.Url.MediaList;
        }

        ListBind();

        string filename = string.Format("媒体签到表 {0}.xls", DateTime.Now).Replace(':', '.');
        btnReporterSign.Attributes.Add("onclick", string.Format("return btnReporterSign_ClientClick('{0}');", filename));


        filename = string.Format("联络表 {0}.xls", DateTime.Now).Replace(':', '.');
        btnReporterContact.Attributes.Add("onclick", string.Format("return btnReporterContact_ClientClick('{0}');", filename));

    }

    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        this.txtCnName.Text = string.Empty;
        this.ddlMediaType.SelectedIndex = 0;
        this.txtIssueRegion.Text = string.Empty;
        this.ddlIndustry.SelectedIndex = 0;
        ListBind();
    }

    #region 绑定列表
    private void ListBind()
    {
        StringBuilder strTerms = new StringBuilder();
        Hashtable ht = new Hashtable();
        if (ddlMediaType.SelectedIndex > 0)
        {
            strTerms.Append(" and a.MediaItemType=@MediaItemType ");
            if (!ht.ContainsKey("@MediaItemType"))
            {
                ht.Add("@MediaItemType", ddlMediaType.SelectedValue);
            }
        }

        if (ddlIndustry.SelectedIndex > 0)
        {
            strTerms.Append(" and a.industryid=@industryid ");
            if (!ht.ContainsKey("@industryid"))
            {
                ht.Add("@industryid", ddlIndustry.SelectedValue);
            }
        }
        if (txtIssueRegion.Text.Trim() != "")
        {
            strTerms.Append(" and issueregion like '%'+@issueregion+'%'");
            if (!ht.ContainsKey("@issueregion"))
                ht.Add("@issueregion", txtIssueRegion.Text.Trim());
        }

        if (txtCnName.Text.Length > 0)
        {
            strTerms.Append(" and (MediacName like '%'+@MediaCName+'%' or a.ChannelName like '%'+@MediaCName+'%' or a.TopicName like '%'+@MediaCName+'%') ");
            if (!ht.ContainsKey("@MediaCName"))
            {
                ht.Add("@MediaCName", txtCnName.Text);
            }
        }

        strTerms.Append(" and createdbyuserid=@userid");
        ht.Add("@userid", CurrentUserID);
        DataTable dt = ESP.Media.BusinessLogic.MediaitemsManager.GetSaveList(strTerms.ToString(), ht);
        if (dt == null)
        {
            dt = new DataTable();
        }

        this.dgList.DataSource = dt.DefaultView;
      //  dgList.HideColumns(new int[] { 0 });
    }
    #endregion

    #region 绑定下拉框
    private void ddlBind()
    {

    }
    #endregion

    #region 查找
    protected void btnSearch_Click(object sender, System.EventArgs e)
    {

    }
    #endregion

    #region 添加
    protected void btnAdd_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("NewMedia.aspx");
    }
    #endregion



    protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "<input type='checkbox' id='chkHeader' onclick=selectedcheck('Header','Rep'); value='" + e.Row.Cells[0].Text + "' />选择";
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = string.Format("<input type='checkbox' id='chkRep' name='chkRep' value={0} />", e.Row.Cells[0].Text);

            e.Row.Cells[1].Text = string.Format("<a href='MediaDisplay.aspx?Mid={0}'>" + e.Row.Cells[1].Text + "</a>", e.Row.Cells[7].Text);

            //if (e.Row.Cells[2].Text == ESP.Media.Access.Utilities.Global.MediaItemTypeName[1])
            //{
            //    e.Row.Cells[7].Text = string.Format("<a title='查看历史' onclick=\"window.open('PlaneMediaContentsList.aspx?alert=1&Mid={0}','','{1}');\"><img src='{2}' /></a>", e.Row.Cells[8].Text, ESP.Media.Access.Utilities.Global.OpenClass.Common, ESP.Media.Access.Utilities.ConfigManager.DisplayIconPath);
            //}
            //else if (e.Row.Cells[2].Text == ESP.Media.Access.Utilities.Global.MediaItemTypeName[2])
            //{
            //    e.Row.Cells[7].Text = string.Format("<a title='查看历史' onclick=\"window.open('WebMediaContentsList.aspx?alert=1&Mid={0}','','{1}');\"><img src='{2}' /></a>", e.Row.Cells[8].Text, ESP.Media.Access.Utilities.Global.OpenClass.Common, ESP.Media.Access.Utilities.ConfigManager.DisplayIconPath);
            //}
            //else if (e.Row.Cells[2].Text == ESP.Media.Access.Utilities.Global.MediaItemTypeName[3])
            //{
            //    e.Row.Cells[7].Text = string.Format("<a title='查看历史' onclick=\"window.open('TvMediaContentsList.aspx?alert=1&Mid={0}','','{1}');\"><img src='{2}' /></a>", e.Row.Cells[8].Text, ESP.Media.Access.Utilities.Global.OpenClass.Common, ESP.Media.Access.Utilities.ConfigManager.DisplayIconPath);
            //}
            //else if (e.Row.Cells[2].Text == ESP.Media.Access.Utilities.Global.MediaItemTypeName[4])
            //{
            //    e.Row.Cells[7].Text = string.Format("<a title='查看历史' onclick=\"window.open('DABMediaContentsList.aspx?alert=1&Mid={0}','','{1}');\"><img src='{2}' /></a>", e.Row.Cells[8].Text, ESP.Media.Access.Utilities.Global.OpenClass.Common, ESP.Media.Access.Utilities.ConfigManager.DisplayIconPath);
            //}

            e.Row.Cells[6].Text = string.Format("<a href='MediaAddAndEdit.aspx?Operate=EDIT&Mid={0}' ><img src='{1}' /></a>", e.Row.Cells[6].Text, ESP.Media.Access.Utilities.ConfigManager.EditIconPath);

            e.Row.Cells[7].Text = string.Format("<a href='MediaAddAndEdit.aspx?Operate=Del&Mid={0}' ><img src='{1}' /></a>", e.Row.Cells[7].Text, ESP.Media.Access.Utilities.ConfigManager.DelIconPath);

        }
    }


    protected void dgList_Sorting(object sender, GridViewSortEventArgs e)
    {

    }

    /// <summary>
    /// Handles the Click event of the btnReporterSign control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnReporterSign_Click(object sender, EventArgs e)
    {
        string media = this.hidChkID.Value;
        StringBuilder tems = new StringBuilder();
        Hashtable ht = new Hashtable();
        string mediastr = " and Media_ID in (";
        if (media != string.Empty)
        {
            string[] strs = media.Split(',');
            for (int i = 0; i < strs.Length; i++)
            {
                ht.Add("@" + i + "", int.Parse(strs[i].Trim()));
                mediastr += "@" + i + ",";

            }

        }
        mediastr = mediastr.Substring(0, mediastr.Length - 1);
        mediastr = mediastr + ")";
        tems.Append(mediastr);
        DataTable dtreport = ESP.Media.BusinessLogic.ReportersManager.GetList(tems.ToString(), ht);

        string filename = string.Format("媒体签到表 {0}.xls", DateTime.Now).Replace(':', '.');
        filename = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.BillPath + filename);
        string errmsg = string.Empty;

        //if (ESP.Media.BusinessLogic.ExcelExportManager.SaveSignExcel(Response, dtreport, filename, out errmsg, CurrentUserID))
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('生成媒体签到表{0}');", errmsg), true);
        //}
        //else
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);

        //}
    }

    /// <summary>
    /// Handles the Click event of the btnReporterContact control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnReporterContact_Click(object sender, EventArgs e)
    {
        string media = this.hidChkID.Value;
        StringBuilder tems = new StringBuilder();
        Hashtable ht = new Hashtable();
        string mediastr = " and Media_ID in (";
        if (media != string.Empty)
        {
            string[] strs = media.Split(',');
            for (int i = 0; i < strs.Length; i++)
            {
                ht.Add("@" + i + "", int.Parse(strs[i].Trim()));
                mediastr += "@" + i + ",";

            }

        }
        mediastr = mediastr.Substring(0, mediastr.Length - 1);
        mediastr = mediastr + ")";
        tems.Append(mediastr);
        DataTable dtreport = ESP.Media.BusinessLogic.ReportersManager.GetList(tems.ToString(), ht);

        string filename = string.Format("联络表 {0}.xls", DateTime.Now).Replace(':', '.');
        filename = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.BillPath + filename);
        string errmsg = string.Empty;

        //if (ESP.Media.BusinessLogic.ExcelExportManager.SaveCommunicateExcel(Response, dtreport, filename, out errmsg, CurrentUserID))
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('生成联络表{0}');", errmsg), true);
        //}
        //else
        //{
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);

        //}
    }
}
