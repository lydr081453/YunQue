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
public partial class Media_AuditedMediaList : ESP.Web.UI.PageBase
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
        string strColumn = "mediaitemid#medianame#MediumSort#headquarter#IndustryName#TelephoneExchange#mediaitemid#mediaitemid";
        string strHeader = "选择#媒体名称#形态#地域属性#行业属性#总机#编辑#删除";
        string strSort = "#mediacname#MediumSort#headquarter#IndustryName###";
        string strH = "center#######center#center";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, strSort, strH, this.dgList);
    }
    #endregion

    /// <summary>
    /// Gets the industry.
    /// </summary>
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


    /// <summary>
    /// Gets the region attribute.
    /// </summary>
    void getRegionAttribute()
    {
        string[] regionAttributes = ESP.Media.Access.Utilities.Global.RegionAttributeName;
        ddlRegionAttribute.Items.Insert(0, new ListItem("请选择", "0"));
        for (int i = 1; i < regionAttributes.Length; i++)
        {
            ddlRegionAttribute.Items.Insert(i, new ListItem(regionAttributes[i].ToString(), i.ToString()));
        }
    }

    /// <summary>
    /// Gets the country.
    /// </summary>
    /// <param name="regionAttributeID">The region attribute ID.</param>
    void getCountry(int regionAttributeID)
    {
        ddlCountry.Items.Clear();
        DataTable dtcountry = ESP.Media.BusinessLogic.CountryManager.getListByRegionAttributeID(regionAttributeID);
        if (dtcountry != null && dtcountry.Rows.Count > 0)
        {
            ddlCountry.DataSource = dtcountry;
            ddlCountry.DataTextField = ESP.Media.Access.Utilities.Global.DataTextField.Country;
            ddlCountry.DataValueField = ESP.Media.Access.Utilities.Global.DataValueField.Country;
            ddlCountry.DataBind();
        }
    }

    /// <summary>
    /// Gets the province.
    /// </summary>
    /// <param name="countryid">The countryid.</param>
    /// <param name="ddlprov">The ddlprov.</param>
    void getProvince(int countryid, DropDownList ddlprov)
    {
        ddlprov.Items.Clear();
        DataTable dtprovince = ESP.Media.BusinessLogic.ProvinceManager.getAllListByCountry(countryid);
        if (dtprovince != null && dtprovince.Rows.Count > 0)
        {
            ddlprov.DataSource = dtprovince;
            ddlprov.DataTextField = ESP.Media.Access.Utilities.Global.DataTextField.Province;
            ddlprov.DataValueField = ESP.Media.Access.Utilities.Global.DataValueField.Province;
            ddlprov.DataBind();

        }
        ddlprov.Items.Insert(0, new ListItem("请选择", "0"));
        ddlprov.SelectedIndex = 0;
    }

    /// <summary>
    /// Gets the city.
    /// </summary>
    /// <param name="provinceid">The provinceid.</param>
    /// <param name="ddlcity">The ddlcity.</param>
    void getCity(int provinceid, DropDownList ddlcity)
    {
        ddlcity.Items.Clear();
        DataTable dtcity = ESP.Media.BusinessLogic.CityManager.getAllListByProvince(provinceid);
        if (dtcity != null && dtcity.Rows.Count > 0)
        {
            ddlcity.DataSource = dtcity;
            ddlcity.DataTextField = ESP.Media.Access.Utilities.Global.DataTextField.City;
            ddlcity.DataValueField = ESP.Media.Access.Utilities.Global.DataValueField.City;
            ddlcity.DataBind();
        }
        ddlcity.Items.Insert(0, new ListItem("请选择", "0"));
        ddlcity.SelectedIndex = 0;
    }


    /// <summary>
    /// Gets the type of the media.
    /// </summary>
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

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {

        AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.Media.BusinessLogic.CountryManager));
        AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.Media.BusinessLogic.CityManager));
        AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.Media.BusinessLogic.ProvinceManager));

        del();


        if (!IsPostBack)
        {

            getIndustry();
            getRegionAttribute();
            //getCountry(int.Parse(ddlRegionAttribute.SelectedValue));
            //getProvince(ESP.Media.Access.Utilities.Global.DefaultCountry, ddlProvince);
            //getCity(int.Parse(ddlProvince.SelectedValue), ddlCity);

            getMediaType();
            Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] = ESP.Media.Access.Utilities.Global.Url.AuditedMediaList;
        }

        ListBindBySession();

        string filename = string.Format("媒体签到表 {0}.xls", DateTime.Now).Replace(':', '.');
        btnReporterSign.Attributes.Add("onclick", string.Format("return btnReporterSign_ClientClick('{0}');", filename));


        filename = string.Format("联络表 {0}.xls", DateTime.Now).Replace(':', '.');
        btnReporterContact.Attributes.Add("onclick", string.Format("return btnReporterContact_ClientClick('{0}');", filename));

    }

    /// <summary>
    /// Handles the OnClick event of the btnClear control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        this.txtCnName.Text = string.Empty;
        this.ddlMediaType.SelectedIndex = 0;
       // this.txtIssueRegion.Text = string.Empty;

        this.ddlIndustry.SelectedIndex = 0;

        ListBind();
    }


    private void ListBindBySession()
    {
        string str;
        Hashtable ht;
        if (Session["mediaTerms"] == null)
            str = string.Empty;
        else
            str = Session["mediaTerms"].ToString();

        if (Session["mediaTerms"] == null)
            ht = null;
        else
            ht = (Hashtable)Session["mediaHash"];

        DataTable dt = ESP.Media.BusinessLogic.MediaitemsManager.GetAuditList(str, ht);
        if (dt == null)
        {
            dt = new DataTable();
        }

        this.dgList.DataSource = dt.DefaultView;
    }

    #region 绑定列表
    /// <summary>
    /// Lists the bind.
    /// </summary>
    private void ListBind()
    {
            Hashtable ht = new Hashtable();
            StringBuilder strTerms = new StringBuilder();
       
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
        if (ddlRegionAttribute.SelectedIndex > 0)
        {
            strTerms.Append(" and a.RegionAttribute=@RegionAttribute ");
            if (!ht.ContainsKey("@RegionAttribute"))
            {
                ht.Add("@RegionAttribute",ddlRegionAttribute.SelectedValue);
            }

            if (!string.IsNullOrEmpty(hidCountry.Value) && hidCountry.Value != "0")
            {
                strTerms.Append(" and a.countryid=@countryid ");
                if (!ht.ContainsKey("@countryid"))
                {
                    ht.Add("@countryid", Convert.ToInt32(hidCountry.Value));
                }
            }
            if (!string.IsNullOrEmpty(hidPro.Value) && hidPro.Value != "0")
            {
                strTerms.Append(" and a.provinceid=@provinceid ");
                if (!ht.ContainsKey("@provinceid"))
                {
                    ht.Add("@provinceid", Convert.ToInt32(hidPro.Value));
                }

                if (!string.IsNullOrEmpty(hidCity.Value) && hidCity.Value != "0")
                {
                    strTerms.Append(" and a.cityid=@cityid ");
                    if (!ht.ContainsKey("@cityid"))
                    {
                        ht.Add("@cityid", Convert.ToInt32(hidCity.Value));
                    }
                }
            }
        }

        if (txtCnName.Text.Length > 0)
        {
            strTerms.Append(" and (MediacName like '%'+@MediaCName+'%' or a.ChannelName like '%'+@MediaCName+'%' or a.TopicName like '%'+@MediaCName+'%') ");
            if (!ht.ContainsKey("@MediaCName"))
            {
                ht.Add("@MediaCName", txtCnName.Text);
            }
        }
        Session["mediaTerms"] = strTerms.ToString();
        Session["mediaHash"] = ht;

        DataTable dt = ESP.Media.BusinessLogic.MediaitemsManager.GetAuditList(strTerms.ToString(), ht);
        if (dt == null)
        {
            dt = new DataTable();
        }

        this.dgList.DataSource = dt.DefaultView;
       // dgList.HideColumns(new int[] {0});
        
    }
    #endregion

    #region 绑定下拉框
    /// <summary>
    /// DDLs the bind.
    /// </summary>
    private void ddlBind()
    {
        
    }
    #endregion

    #region 查找
    /// <summary>
    /// Handles the Click event of the btnSearch control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSearch_Click(object sender, System.EventArgs e)
    {
        this.ListBind();
    }
    #endregion
    
    
    #region 添加
    protected void btnAdd_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("NewMedia.aspx?listadd=true&backurl=AuditedMediaList.aspx");
    }
    #endregion
    

    /// <summary>
    /// Handles the RowDataBound event of the dgList control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
    protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "<input type='checkbox' id='chkHeader' onclick=selectedcheck('Header','Rep'); value='" + e.Row.Cells[0].Text + "' />选择";
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           e.Row.Cells[0].Text = string.Format("<input type='checkbox' id='chkRep' name='chkRep' value={0} />", e.Row.Cells[0].Text);

            e.Row.Cells[1].Text = string.Format("<a href='MediaDisplay.aspx?Mid={0}&backurl=AuditedMediaList.aspx'>" + e.Row.Cells[1].Text + "</a>", e.Row.Cells[7].Text);

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
            e.Row.Cells[6].Text = string.Format("<a href='MediaAddAndEdit.aspx?backurl=AuditedMediaList.aspx&Source=Audit&Operate=EDIT&Mid={0}' ><img src='{1}' /></a>", e.Row.Cells[6].Text, ESP.Media.Access.Utilities.ConfigManager.EditIconPath);

            e.Row.Cells[7].Text = string.Format("<a href='AuditedMediaList.aspx?Operate=DEL&Mid={0}' ><img src='{1}' /></a>", e.Row.Cells[7].Text, ESP.Media.Access.Utilities.ConfigManager.DelIconPath);
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
    /// Dels this instance.
    /// </summary>
    private void del()
    {
        if (!string.IsNullOrEmpty(Request["Operate"]) && Request["Operate"] == "DEL")
        {
            if (string.IsNullOrEmpty(Request["Mid"]))
                return;
            string errmsg = "";
            int ret = ESP.Media.BusinessLogic.MediaitemsManager.Delete(ESP.Media.BusinessLogic.MediaitemsManager.GetModel(Convert.ToInt32(Request["Mid"])), out errmsg);
            if (ret > 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "window.location='AuditedMediaList.aspx';alert('删除成功！');", true);
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.location='AuditedMediaList.aspx';alert('{0}');", errmsg), true);
            }
        }
    }

    /// <summary>
    /// Handles the Click event of the btnReporterSign control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnReporterSign_Click(object sender, EventArgs e)
    {
        //string media = this.hidChkID.Value;
        //StringBuilder tems = new StringBuilder();
        //Hashtable ht = new Hashtable();
        //string mediastr = " and Media_ID in (";
        //if (media != string.Empty)
        //{
        //    string[] strs = media.Split(',');
        //    for (int i = 0; i < strs.Length; i++)
        //    {
        //        ht.Add("@" + i + "", int.Parse(strs[i].Trim()));
        //        mediastr += "@" + i + ",";

        //    }

        //}
        //mediastr = mediastr.Substring(0, mediastr.Length - 1);
        //mediastr = mediastr + ")";
        //tems.Append(mediastr);
        //DataTable dtreport = ESP.Media.BusinessLogic.ReportersManager.GetList(tems.ToString(), ht);

        //string filename = string.Format("媒体签到表 {0}.xls", DateTime.Now).Replace(':', '.');
        //filename = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.BillPath + filename);
        
        //string errmsg = string.Empty;

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
        //string media = this.hidChkID.Value;
        //StringBuilder tems = new StringBuilder();
        //Hashtable ht = new Hashtable();
        //string mediastr = " and Media_ID in (";
        //if (media != string.Empty)
        //{
        //    string[] strs = media.Split(',');
        //    for (int i = 0; i < strs.Length; i++)
        //    {
        //        ht.Add("@" + i + "", int.Parse(strs[i].Trim()));
        //        mediastr += "@" + i + ",";

        //    }

        //}
        //mediastr = mediastr.Substring(0, mediastr.Length - 1);
        //mediastr = mediastr + ")";
        //tems.Append(mediastr);
        //DataTable dtreport = ESP.Media.BusinessLogic.ReportersManager.GetList(tems.ToString(), ht);

        //string filename = string.Format("联络表 {0}.xls", DateTime.Now).Replace(':', '.');
        //filename = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.BillPath + filename);
        //string errmsg = string.Empty;

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
