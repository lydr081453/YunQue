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
using ESP.MediaLinq.Entity;

namespace MediaWeb.NewMedia.BaseData
{
    public partial class AuditMediaList : ESP.Web.UI.PageBase
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

            string strColumn = "mediaitemid#medianame#MediumSort#headquarter#IndustryName#TelephoneExchange#mediaitemid";
            string strHeader = "选择#媒体名称#形态属性#地域属性#行业属性#总机#审核";
            string strSort = "#mediacname#MediumSort#IssueRegion#IndustryName##";
            string strH = "#######center";
            MyControls.GridViewOperate.GridViewHelper.DrawGridView(false, strColumn, strHeader, strSort, strH, this.dgList);


            TemplateField tf = new TemplateField();
            string clientclick = "return confirm('此操作是物理删除,确认要删除吗？');";
            MyControls.GridViewOperate.ImageButtonItem itmDel = new MyControls.GridViewOperate.ImageButtonItem(ESP.MediaLinq.Utilities.ConfigManager.DelIconPath, clientclick, "mediaitemid");
            itmDel.DoSomething += new MyControls.GridViewOperate.DoSomethingHandler(itmDel_DoSomething);
            tf.ItemTemplate = itmDel;
            dgList.Columns.Add(tf);
        }

        void itmDel_DoSomething(object sender, string value)
        {

            string errmsg = string.Empty;
            bool res = ESP.MediaLinq.BusinessLogic.MediaItemManager.PhysicalDel(Convert.ToInt32(value), CurrentUserID, out errmsg);
            if (res)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("window.location=window.location;alert('{0}');", "删除成功！"), true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);
            }
        }

        #endregion

        /// <summary>
        /// Gets the industry.
        /// </summary>
        private void getIndustry()
        {
            DataTable dtindustry = ESP.MediaLinq.BusinessLogic.IndustriesManager.GetDataTable();
            if (dtindustry != null && dtindustry.Rows.Count > 0)
            {
                ddlIndustry.DataSource = dtindustry;
                ddlIndustry.DataTextField = ESP.MediaLinq.Utilities.Global.DataTextField.Industry;
                ddlIndustry.DataValueField = ESP.MediaLinq.Utilities.Global.DataValueField.Industry;
                ddlIndustry.DataBind();
            }
            ddlIndustry.Items.Insert(0, new ListItem("请选择", "0"));
        }



        /// <summary>
        /// Gets the region attribute.
        /// </summary>
        void getRegionAttribute()
        {
            string[] regionAttributes = ESP.MediaLinq.Utilities.Global.RegionAttributeName;
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
            DataTable dtcountry = ESP.MediaLinq.BusinessLogic.CountryManager.getListByRegionAttributeID(regionAttributeID);
            if (dtcountry != null && dtcountry.Rows.Count > 0)
            {
                ddlCountry.DataSource = dtcountry;
                ddlCountry.DataTextField = ESP.MediaLinq.Utilities.Global.DataTextField.Country;
                ddlCountry.DataValueField = ESP.MediaLinq.Utilities.Global.DataValueField.Country;
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
            DataTable dtprovince = ESP.MediaLinq.BusinessLogic.ProvinceManager.getAllListByCountry(countryid);
            if (dtprovince != null && dtprovince.Rows.Count > 0)
            {
                ddlprov.DataSource = dtprovince;
                ddlprov.DataTextField = ESP.MediaLinq.Utilities.Global.DataTextField.Province;
                ddlprov.DataValueField = ESP.MediaLinq.Utilities.Global.DataValueField.Province;
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
            DataTable dtcity = ESP.MediaLinq.BusinessLogic.CityManager.getAllListByProvince(provinceid);
            if (dtcity != null && dtcity.Rows.Count > 0)
            {
                ddlcity.DataSource = dtcity;
                ddlcity.DataTextField = ESP.MediaLinq.Utilities.Global.DataTextField.City;
                ddlcity.DataValueField = ESP.MediaLinq.Utilities.Global.DataValueField.City;
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
            DataTable dttype = ESP.MediaLinq.BusinessLogic.MediaTypeManager.GetDataTable();
            if (dttype != null && dttype.Rows.Count > 0)
            {
                ddlMediaType.DataSource = dttype;
                ddlMediaType.DataTextField = ESP.MediaLinq.Utilities.Global.DataTextField.MediaType;
                ddlMediaType.DataValueField = ESP.MediaLinq.Utilities.Global.DataValueField.MediaType;
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
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.MediaLinq.BusinessLogic.CountryManager));
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.MediaLinq.BusinessLogic.CityManager));
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.MediaLinq.BusinessLogic.ProvinceManager));

            if (!IsPostBack)
            {
                getIndustry();
                getRegionAttribute();
                getMediaType();
                //  Session[ESP.Media.Access.Utilities.Global.SessionKey.CurrentRootPage] = ESP.Media.Access.Utilities.Global.Url.MediaList;

                string errmsg = string.Empty;
                if (Request["Mid"] != null)
                {
                    media_MediaItemsInfo media = ESP.MediaLinq.BusinessLogic.MediaItemManager.GetModel(Convert.ToInt32(Request["Mid"]));

                    int ret = ESP.MediaLinq.BusinessLogic.MediaItemManager.AuditMedia(media, CurrentUserID, out errmsg);
                    if (ret > 0)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('审核成功！');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);
                    }
                }
            }
            ListBind();

            //string filename = string.Format("媒体签到表 {0}.xls", DateTime.Now).Replace(':', '.');
            //btnReporterSign.Attributes.Add("onclick", string.Format("return btnReporterSign_ClientClick('{0}');", filename));


            //filename = string.Format("联络表 {0}.xls", DateTime.Now).Replace(':', '.');
            //btnReporterContact.Attributes.Add("onclick", string.Format("return btnReporterContact_ClientClick('{0}');", filename));
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
            //    this.txtIssueRegion.Text = string.Empty;
            this.ddlIndustry.SelectedIndex = 0;

            ListBind();
        }

        #region 绑定列表
        /// <summary>
        /// Lists the bind.
        /// </summary>
        private void ListBind()
        {
            StringBuilder strTerms = new StringBuilder();
            Hashtable ht = new Hashtable();
            string mediatype = "";
            if (ddlMediaType.SelectedIndex > 0)
            {
                mediatype = ddlMediaType.SelectedValue;
            }

            string industry = "";
            if (ddlIndustry.SelectedIndex > 0)
            {
                industry = ddlIndustry.SelectedValue;

            }
            string regionattribute = "";
            string countryid = "";
            string provinceid = "";
            string cityid = "";
            if (ddlRegionAttribute.SelectedIndex > 0)
            {
                regionattribute = ddlRegionAttribute.SelectedValue;

                if (!string.IsNullOrEmpty(hidCountry.Value) && hidCountry.Value != "0")
                {
                    countryid = hidCountry.Value.Trim();
                }
                if (!string.IsNullOrEmpty(hidPro.Value) && hidPro.Value != "0")
                {
                    provinceid = hidPro.Value;

                    if (!string.IsNullOrEmpty(hidCity.Value) && hidCity.Value != "0")
                    {
                        cityid = hidCity.Value;
                    }
                }
            }
            string mediacname = "";
            if (txtCnName.Text.Length > 0)
            {
                mediacname = txtCnName.Text;
            }

            DataTable dt = ESP.MediaLinq.BusinessLogic.MediaItemManager.GetUnAuditList(mediatype, industry, regionattribute, countryid, provinceid, cityid, mediacname);
            if (dt == null)
            {
                dt = new DataTable();
            }

            this.dgList.DataSource = dt.DefaultView;

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

        }
        #endregion

        #region 添加
        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("NewMedia.aspx");
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
                int MediaId = int.Parse(dgList.DataKeys[e.Row.RowIndex].Value.ToString());
                e.Row.Cells[0].Text = string.Format("<input type='checkbox' id='chkRep' name='chkRep' value={0} />", e.Row.Cells[0].Text);
                e.Row.Cells[1].Text = string.Format("<a href='AuditMediaDisplay.aspx?Mid={0}'>{2}</a>", MediaId, ESP.MediaLinq.Utilities.Global.OpenClass.Common, e.Row.Cells[1].Text);
                e.Row.Cells[6].Text = string.Format("<input type='button' id='btnAudit' onclick=\"window.location.href='AuditMediaList.aspx?Mid={0}'\" value={1} class='widebuttons'/>", MediaId, "审核通过");
            }
        }

        ///// <summary>
        ///// Handles the Click event of the btnReporterSign control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        //protected void btnReporterSign_Click(object sender, EventArgs e)
        //{
        //    string media = this.hidChkID.Value;
        //    StringBuilder tems = new StringBuilder();
        //    Hashtable ht = new Hashtable();
        //    string mediastr = " and Media_ID in (";
        //    if (media != string.Empty)
        //    {
        //        string[] strs = media.Split(',');
        //        for (int i = 0; i < strs.Length; i++)
        //        {
        //            ht.Add("@" + i + "", int.Parse(strs[i].Trim()));
        //            mediastr += "@" + i + ",";

        //        }

        //    }
        //    mediastr = mediastr.Substring(0, mediastr.Length - 1);
        //    mediastr = mediastr + ")";
        //    tems.Append(mediastr);
        //    DataTable dtreport = ESP.Media.BusinessLogic.ReportersManager.GetList(tems.ToString(), ht);

        //    string filename = string.Format("媒体签到表 {0}.xls", DateTime.Now).Replace(':', '.');
        //    filename = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.BillPath + filename);
        //    string errmsg = string.Empty;

        //    //if (ESP.Media.BusinessLogic.ExcelExportManager.SaveSignExcel(Response, dtreport, filename, out errmsg, CurrentUserID))
        //    //{
        //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('生成媒体签到表{0}');", errmsg), true);
        //    //}
        //    //else
        //    //{
        //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);

        //    //}
        //}

        ///// <summary>
        ///// Handles the Click event of the btnReporterContact control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        //protected void btnReporterContact_Click(object sender, EventArgs e)
        //{
        //    string media = this.hidChkID.Value;
        //    StringBuilder tems = new StringBuilder();
        //    Hashtable ht = new Hashtable();
        //    string mediastr = " and Media_ID in (";
        //    if (media != string.Empty)
        //    {
        //        string[] strs = media.Split(',');
        //        for (int i = 0; i < strs.Length; i++)
        //        {
        //            ht.Add("@" + i + "", int.Parse(strs[i].Trim()));
        //            mediastr += "@" + i + ",";

        //        }

        //    }
        //    mediastr = mediastr.Substring(0, mediastr.Length - 1);
        //    mediastr = mediastr + ")";
        //    tems.Append(mediastr);
        //    DataTable dtreport = ESP.Media.BusinessLogic.ReportersManager.GetList(tems.ToString(), ht);

        //    string filename = string.Format("联络表 {0}.xls", DateTime.Now).Replace(':', '.');
        //    filename = Server.MapPath(ESP.Media.Access.Utilities.ConfigManager.BillPath + filename);
        //    string errmsg = string.Empty;

        //    //if (ESP.Media.BusinessLogic.ExcelExportManager.SaveCommunicateExcel(Response, dtreport, filename, out errmsg, CurrentUserID))
        //    //{
        //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('生成联络表{0}');", errmsg), true);
        //    //}
        //    //else
        //    //{
        //    //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), string.Format("alert('{0}');", errmsg), true);

        //    //}
        //}
    }
}
