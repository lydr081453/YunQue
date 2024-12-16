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
using ESP.Media.Entity;
public partial class Media_ReporterSelectMediaList : ESP.Web.UI.PageBase
{
    int alertvalue = 0;
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
        string strColumn = "mediaitemid#medianame#MediumSort#IssueRegion#IndustryName#headquarter#TelephoneExchange";
        string strHeader = "选择#媒体名称#形态#发行区域#行业属性#总部所在地#总机";
        string strSort = "#medianame#MediumSort#IssueRegion#IndustryName#headquarter#";
        string strH = "center######";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, strSort, strH, this.dgList);
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
        if (!string.IsNullOrEmpty(Request["alert"]))
        {
            alertvalue = int.Parse(Request["alert"]);
        }
        if (!IsPostBack)
        {

            getIndustry();
            getMediaType();           
            
        }
        if (alertvalue > 0)
        {
            this.btnClose.Visible = true;
            this.btnBack.Visible = false;
        }
        else
        {
            this.btnClose.Visible = false;
            this.btnBack.Visible = true;

        }
       
            ListBind();
        Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] =  ESP.Media.Access.Utilities.Global.Url.ReporterSelectMediaList;
    }
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        this.ddlMediaType.SelectedIndex = 0;
        this.txtCnName.Text = string.Empty;
        this.ddlIndustry.SelectedIndex = 0;

        this.txtIssueRegion.Text = string.Empty;
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


        DataTable dt = ESP.Media.BusinessLogic.MediaitemsManager.GetAuditList(strTerms.ToString(), ht);
        if (dt == null)
        {
            dt = new DataTable();
        }

        this.dgList.DataSource = dt.DefaultView;
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


    #region 选择
    protected void btnSelect_Click(object sender, System.EventArgs e)
    {
        int mid = Convert.ToInt32(hidChecked.Value.Trim());
        string js = "";
        if(!string.IsNullOrEmpty(Request["Operate"]) && Request["Operate"] == "New"){
            //新添加记者ctl00_contentMain_txtMediaName
            MediaitemsInfo media = ESP.Media.BusinessLogic.MediaitemsManager.GetModel(mid);
            js += "<script>opener.document.all.ctl00_contentMain_txtMediaName.value = '" + media.Mediacname + " " + media.Channelname + " " + media.Topicname + "';</script>";
            js += "<script>opener.document.all.ctl00_contentMain_hidMedia.value = '"+media.Mediaitemid+"';</script>";
            js += "<script>window.close();</script>";
            Response.Write(js);
        }
        if (Request["Rid"] != null && mid != 0)
        {
            int rid = Convert.ToInt32(Request["Rid"]);
            ReportersInfo reporter = ESP.Media.BusinessLogic.ReportersManager.GetModel(rid);
            if (reporter != null)
            {
                string errmsg;
                reporter.Media_id = mid;
                int ret = ESP.Media.BusinessLogic.ReportersManager.Update(reporter, null, CurrentUserID, out errmsg);
                string backurl = "";
                if (string.IsNullOrEmpty(Request["backurl"]))
                    backurl = "ReporterDisplay.aspx";
                else
                    backurl = Request["backurl"];
                if (alertvalue > 0)
                    backurl += "?alert=" + alertvalue;
                else
                    backurl += "?";
                if (ret > 0)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.location='{0}&Rid={1}&Mid={2}&Operate=EDIT';alert('保存成功！');",backurl, rid,mid), true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);
                  
                }

            }
        }
        else if (Request["Rid"] != null)
        {
            int rid = Convert.ToInt32(Request["Rid"]);
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.location='ReporterDisplay.aspx?Rid={0}';alert('你没有进行修改操作！');", rid), true);
        }
    }
    #endregion

    #region 添加
    protected void btnAdd_Click(object sender, System.EventArgs e)
    {
        if (Request["Rid"] != "")
        {
            string ss = ESP.Media.Access.Utilities.Global.Url.ReporterSelectMediaList + "?Rid=" + Request["Rid"];
            Response.Redirect("NewMedia.aspx?Source=" + ss);
        }
        else
        {
            Response.Redirect("NewMedia.aspx?Source=" + ESP.Media.Access.Utilities.Global.Url.ReporterSelectMediaList);
        }
    }
    #endregion



    protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int MediaId = int.Parse(dgList.DataKeys[e.Row.RowIndex].Value.ToString());
            e.Row.Cells[0].Text = string.Format("<input type=radio id=radNo onclick='selected(this)' name=radNo value={0}>", e.Row.Cells[0].Text);
            //e.Row.Cells[1].Text = string.Format("<a href='#' onclick=\"window.open('../Media/MediaDisplay.aspx?visible=false&alert=1&Mid={0}','','{2}');\" >{1}</a>", MediaId, e.Row.Cells[1].Text, ESP.Media.Access.Utilities.Global.OpenClass.Common);

        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        string backurl = "";
        if (string.IsNullOrEmpty(Request["backurl"]))
            backurl = "ReporterDisplay.aspx?Rid=" + Request["Rid"];
        else
            backurl = Request["backurl"] + "?Rid=" + Request["Rid"] + "&Operate=EDIT";
        if (alertvalue > 0)
            backurl += "&alert=" + alertvalue;
        Response.Redirect(backurl);
    }
}
