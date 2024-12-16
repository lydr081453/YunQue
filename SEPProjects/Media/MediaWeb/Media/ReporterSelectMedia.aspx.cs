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
public partial class Media_ReporterSelectMedia : ESP.Web.UI.PageBase
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
        string strColumn = "mediaitemid#mediacname#cshortname#createddate#mediatypename#mediaitemid#mediaitemid#mediaitemid";
        string strHeader = "选择#媒体名称#媒体简称#创建日期#媒体类别#查看#编辑#删除";
        string strH = "center###center##center#center#center";
        MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, null,strH, this.dgList);
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
            getCountry();
            getProvince(Convert.ToInt32(ddlCountry.SelectedIndex), ddlProvince);
            getCity(Convert.ToInt32(ddlProvince.SelectedIndex), ddlCity);

            getIndustry();
            getMediaType();
            
        }

        ListBind();
    }
    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        this.ddlMediaType.SelectedIndex = 0;
        this.txtCnName.Text = string.Empty;
        this.ddlIndustry.SelectedIndex = 0;
        this.ddlCountry.SelectedIndex = 0;
        this.ddlProvince.SelectedIndex = 0;
        this.ddlCity.SelectedIndex = 0;

        this.txtAddress.Text = string.Empty;
        ListBind();
    }

    void getCountry()
    {
        ddlCountry.Items.Clear();
        DataTable dtcountry = ESP.Media.BusinessLogic.CountryManager.getAllList();
        if (dtcountry != null && dtcountry.Rows.Count > 0)
        {
            ddlCountry.DataSource = dtcountry;
            ddlCountry.DataTextField = ESP.Media.Access.Utilities.Global.DataTextField.Country;
            ddlCountry.DataValueField = ESP.Media.Access.Utilities.Global.DataValueField.Country;
            ddlCountry.DataBind();
        }
        ddlCountry.Items.Insert(0, new ListItem("请选择", "0"));
        ddlCountry.SelectedIndex = 0;
    }


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


    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvince);
        getCity(Convert.ToInt32(ddlProvince.SelectedValue), ddlCity);

    }

    protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        getCity(Convert.ToInt32(ddlProvince.SelectedValue), ddlCity);
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
        if (ddlCountry.SelectedIndex > 0)
        {
            strTerms.Append(" and a.countryid=@countryid ");
            if (!ht.ContainsKey("@countryid"))
            {
                ht.Add("@countryid", ddlCountry.SelectedValue);
            }
        }

        if (ddlProvince.SelectedIndex > 0)
        {
            strTerms.Append(" and a.provinceid=@provinceid ");
            if (!ht.ContainsKey("@provinceid"))
            {
                ht.Add("@provinceid", ddlProvince.SelectedValue);
            }
        }

        if (ddlCity.SelectedIndex > 0)
        {
            strTerms.Append(" and a.cityid=@cityid ");
            if (!ht.ContainsKey("@cityid"))
            {
                ht.Add("@cityid", ddlCity.SelectedValue);
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


        if (txtAddress.Text.Length > 0)
        {
            strTerms.Append(" and (a.AddressOne like '%'+@Address+'%' or a.AddressTwo like '%'+@Address+'%' ");
            if (!ht.ContainsKey("@Address"))
            {
                ht.Add("@Address", txtAddress.Text);
            }
        }


        if (txtCnName.Text.Length > 0)
        {
            strTerms.Append(" and a.MediaCName like '%'+@MediaCName+'%' ");
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
        this.dgList.Columns[6].Visible = false;
        this.dgList.Columns[7].Visible = false;
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





    protected void dgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = string.Format("<input type=radio id=radNo onclick='selected(this)' name=radNo value={0}>", e.Row.Cells[0].Text+","+e.Row.Cells[1].Text);
            e.Row.Cells[3].Text = e.Row.Cells[3].Text.Split(' ')[0];
            e.Row.Cells[5].Text = string.Format("<a href='MediaDisplay.aspx?Mid={0}' ><img src='{1}' /></a>", e.Row.Cells[5].Text, ESP.Media.Access.Utilities.ConfigManager.DisplayIconPath);
            e.Row.Cells[6].Text = string.Format("<a href='MediaAddAndEdit.aspx?Operate=EDIT&Mid={0}' ><img src='{1}' /></a>", e.Row.Cells[6].Text, ESP.Media.Access.Utilities.ConfigManager.EditIconPath);
            e.Row.Cells[7].Text = string.Format("<a href='MediaAddAndEdit.aspx?Operate=Del&Mid={0}' ><img src='{1}' /></a>", e.Row.Cells[7].Text, ESP.Media.Access.Utilities.ConfigManager.DelIconPath);

        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {

    }
}
