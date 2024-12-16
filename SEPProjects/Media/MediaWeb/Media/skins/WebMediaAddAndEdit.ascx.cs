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
using ESP.Media.Entity;
using System.Collections.Generic;
using ESP.Media.Entity;

public partial class Media_skins_WebMediaAddAndEdit : System.Web.UI.UserControl
{
    MediaitemsInfo media = null;
    MediaindustryrelationInfo[] industries;

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        #region AjaxProRegister
        AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.Media.BusinessLogic.MediaitemsManager));
        AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.Media.BusinessLogic.CountryManager));
        AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.Media.BusinessLogic.CityManager));
        AjaxPro.Utility.RegisterTypeForAjax(typeof(ESP.Media.BusinessLogic.ProvinceManager));
        #endregion
        txtMediumSort.SelectedIndex = 2;//形态属性
        getIndustry();
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request["Mid"]))
            {
                setValues();
            }
            else
            {
                getRegionAttribute();
                getCountry(int.Parse(ddlRegionAttribute.SelectedValue));
                //getProvince(int.Parse(ddlCountry.SelectedValue), ddlProvince);
                //getProvince(int.Parse(ddlCountry.SelectedValue), ddlProvinceAddr1);

                getProvince(ESP.Media.Access.Utilities.Global.DefaultCountry, ddlProvince);
                getProvince(ESP.Media.Access.Utilities.Global.DefaultCountry, ddlProvinceAddr1);

                getCity(int.Parse(ddlProvince.SelectedValue), ddlCity);
                getCity(int.Parse(ddlProvinceAddr1.SelectedValue), ddlCityAddr1);
                setCity();
            }
            if (!string.IsNullOrEmpty(Request["Source"]) && Request["Source"] == "Audit")
            {
                txtWebName.Enabled = false;
                //txtWebEngName.Enabled = false;
                //txtWebHTCName.Enabled = false;
                //txtWebEngHTCName.Enabled = false;
            }
        }
    }

    /// <summary>
    /// Handles the Click event of the btnClearSelected control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnClearSelected_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in this.lbxBranchSelected.Items)
        {
            ListItem unItem = new ListItem(item.Text, item.Value);
            this.lbxBranch.Items.Add(unItem);
        }
        this.lbxBranchSelected.Items.Clear();
    }

    /// <summary>
    /// Handles the Click event of the btnSelectBranch control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSelectBranch_Click(object sender, EventArgs e)
    {
        for (int i = this.lbxBranch.Items.Count - 1; i >= 0; i--)
        {
            if (this.lbxBranch.Items[i].Selected)
            {
                this.lbxBranchSelected.Items.Add(this.lbxBranch.Items[i]);
                this.lbxBranch.Items.Remove(this.lbxBranch.Items[i]);
            }
        }
    }

    /// <summary>
    /// Handles the Click event of the btnUnSelectBranch control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnUnSelectBranch_Click(object sender, EventArgs e)
    {

        for (int i = this.lbxBranchSelected.Items.Count - 1; i >= 0; i--)
        {
            if (this.lbxBranchSelected.Items[i].Selected)
            {
                this.lbxBranch.Items.Add(this.lbxBranchSelected.Items[i]);
                this.lbxBranchSelected.Items.Remove(this.lbxBranchSelected.Items[i]);
            }
        }
        if (this.lbxBranchSelected.SelectedIndex > -1)
        {
            ListItem item = this.lbxBranchSelected.SelectedItem;
            this.lbxBranch.Items.Add(item);
            this.lbxBranchSelected.Items.Remove(this.lbxBranchSelected.SelectedItem);
        }
    }

    /// <summary>
    /// Gets the industry.
    /// </summary>
    private void getIndustry()
    {
        DataTable dtindustry = ESP.Media.BusinessLogic.IndustriesManager.GetAllList();
        ddlIndustry.DataSource = dtindustry;
        ddlIndustry.DataTextField = "IndustryName";
        ddlIndustry.DataValueField = "IndustryID";
        ddlIndustry.DataBind();
        ddlIndustry.Items.Insert(0, new ListItem("请选择", "0"));
    }

    /// <summary>
    /// Gets the region attribute.
    /// </summary>
    void getRegionAttribute()
    {
        string[] regionAttributes = ESP.Media.Access.Utilities.Global.RegionAttributeName;
        for (int i = 1; i < regionAttributes.Length; i++)
        {
            ddlRegionAttribute.Items.Insert(i - 1, new ListItem(regionAttributes[i].ToString(), i.ToString()));
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
    /// Handles the SelectedIndexChanged event of the ddlRegionAttribute control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    //protected void ddlRegionAttribute_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    getCountry(int.Parse(ddlRegionAttribute.SelectedValue));
    //    getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvince);
    //    getCity(Convert.ToInt32(ddlProvince.SelectedValue), ddlCity);

    //    getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvinceAddr1);
    //    getCity(Convert.ToInt32(ddlProvinceAddr1.SelectedValue), ddlCityAddr1);
    //}

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlCountry control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    //protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvince);
    //    getCity(Convert.ToInt32(ddlProvince.SelectedValue), ddlCity);

    //    getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvinceAddr1);
    //    getCity(Convert.ToInt32(ddlProvince.SelectedValue), ddlCityAddr1);


    //    //getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvinceAddr2);
    //    //getCity(Convert.ToInt32(ddlProvince.SelectedValue), ddlCityAddr2);
    //}

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlProvince control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    //protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    getCity(Convert.ToInt32(ddlProvince.SelectedValue), ddlCity);
    //}

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlProvinceAddr1 control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    //protected void ddlProvinceAddr1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    getCity(Convert.ToInt32(ddlProvinceAddr1.SelectedValue), ddlCityAddr1);
    //}

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlProvinceAddr2 control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    //protected void ddlProvinceAddr2_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //getCity(Convert.ToInt32(ddlProvinceAddr2.SelectedValue), ddlCityAddr2);
    //}

    /// <summary>
    /// Gets the branch objects.
    /// </summary>
    /// <returns></returns>
    public List<BranchInfo> GetBranchObjects()
    {
        List<BranchInfo> listBranch = new List<BranchInfo>();
        string strCityID = this.RoleColl.Value;
        string[] strCityIDList = strCityID.Split(',');
        for (int i = 0; i < strCityIDList.Length; i++)
        {
            BranchInfo branch = new BranchInfo();
            int mediaid = Convert.ToInt32(Request["Mid"]);
            branch.Mediaitemid = mediaid;

            int cid = 0;
            bool res = int.TryParse(strCityIDList[i], out cid);
            if (res)
            {

                CapitalInfo city = ESP.Media.BusinessLogic.CapitalManager.GetModel(cid);
                branch.Cityid = city.Cityid;
                branch.Cityname = city.Cityname;
            }
            listBranch.Add(branch);

        }
        return listBranch;
    }

    #region 获得对象
    /// <summary>
    /// Gets the object.
    /// </summary>
    /// <returns></returns>
    public MediaitemsInfo GetObject()
    {

        if (Request["Mid"] != null)
        {
            media = ESP.Media.BusinessLogic.MediaitemsManager.GetModel(Convert.ToInt32(Request["Mid"]));

        }
        else
        {
            media = new MediaitemsInfo();
        }
        media.Createddate = media.Createddate == string.Empty ? DateTime.Now.ToString() : media.Createddate;
        media.Createdbyuserid = media.Createdbyuserid == 0 ? 1 : media.Createdbyuserid;
        media.Lastmodifieddate = DateTime.Now.ToString();
        media.Lastmodifiedbyuserid = 1;

        media.Mediacname = txtWebName.Text.Trim();//媒体中文名称
        media.Mediaename = txtWebEngName.Text.Trim();//媒体英文名称
        //media.Cshortname = txtWebHTCName.Text.Trim();//媒体中文简称
        media.Eshortname = txtWebEngHTCName.Text.Trim();//媒体英文简称
        media.Webaddress = txtWebAddress.Text.Trim();//媒体网址
        media.Mediumsort = txtMediumSort.Text.Trim();//形态属性
        media.Channelname = txtWebChannelName.Text.Trim();//频道名称
      //  media.Channelwebaddress = txtChannelAddress.Text.Trim();//频道网址
        media.Mediaitemtype = txtMediumSort.SelectedIndex;
        //行业属性
        media.Industryid = int.Parse(ddlIndustry.SelectedValue);




        media.Governingbody = txtGoverningBody.Text.Trim();//所属单位
        media.Manager = txtMajordomo.Text.Trim();//频道总监


        media.Chiefeditor = txtChiefEditor.Text.Trim();//总编
        media.Admineditor = txtAdminEditor.Text.Trim();//执行总编

        media.Telephoneexchange = txtTelephoneExchange.Text.Trim();//总机
        media.Fax = txtFax.Text.Trim();//传真
        media.Addressone = txtAddress1.Text.Trim();//地址1
        //media.Addresstwo = txtAddress2.Text.Trim() ;//地址2
        media.Phoneone = txtPhoneOne.Text.Trim();//热线1
        media.Phonetwo = txtPhoneTwo.Text.Trim();//热线2
        media.Adsphone = txtAdsPhone.Text.Trim();//广告部电话

        //media.Adsprice = adsUploadPrice.FileName;//广告报价
        //media.Medialogo = mediaUploadLogo.FileName;// 媒体LOGO
        //media.Briefing = briefingUpload.FileName;//剪报

        //地址
        media.Regionattribute = int.Parse(ddlRegionAttribute.SelectedValue);//地域属性 
        //media.Countryid = Convert.ToInt32(ddlCountry.SelectedValue);
        //media.Provinceid = Convert.ToInt32(ddlProvince.SelectedValue);
        //media.Cityid = Convert.ToInt32(ddlCity.SelectedValue);
        if (media.Regionattribute == ESP.Media.Access.Utilities.Global.RegionAttribute_Centrality || media.Regionattribute == ESP.Media.Access.Utilities.Global.RegionAttribute_District)
            media.Countryid = ESP.Media.Access.Utilities.Global.DefaultCountry;
        else
        {
            //media.Countryid = Convert.ToInt32(ddlCountry.SelectedValue);
            media.Countryid = Convert.ToInt32(hidCountry.Value);
        }

        if (media.Regionattribute == ESP.Media.Access.Utilities.Global.RegionAttribute_District)
        {
            //media.Provinceid = Convert.ToInt32(ddlProvince.SelectedValue);
            media.Provinceid = Convert.ToInt32(hidPro.Value);
            //media.Cityid = Convert.ToInt32(ddlCity.SelectedValue);
            media.Cityid = Convert.ToInt32(hidCity.Value);
        }
        else
        {
            media.Provinceid = 0;
            media.Cityid = 0;
        }

        //media.Addr1_countryid = Convert.ToInt32(ddlCountry.SelectedValue);
        //media.Addr1_provinceid = Convert.ToInt32(ddlProvinceAddr1.SelectedValue);
        //media.Addr1_cityid = Convert.ToInt32(ddlCityAddr1.SelectedValue);
        media.Addr1_countryid = ESP.Media.Access.Utilities.Global.DefaultCountry;
        media.Addr1_provinceid = Convert.ToInt32(ddlProvinceAddr1.SelectedValue);
        if (!string.IsNullOrEmpty(hidCityAddr1.Value))
            media.Addr1_cityid = Convert.ToInt32(hidCityAddr1.Value);
        else
            media.Addr1_cityid = 0;

        //media.Addr2_countryid = Convert.ToInt32(ddlCountry.SelectedValue);
        media.Addr2_countryid = ESP.Media.Access.Utilities.Global.DefaultCountry;

        //media.Addr2_provinceid = Convert.ToInt32(ddlProvinceAddr2.SelectedValue);
        //media.Addr2_cityid = Convert.ToInt32(ddlCityAddr2.SelectedValue);

        //邮政编码
        media.Postcode = txtPostcode.Text.Trim();
        //media.Issueregion = txtIssueRegion.Text.Trim();



        #region MultiLine
        if (txtCooperate.Text.Trim().Length > 500)
            media.Cooperate = txtCooperate.Text.Trim().Substring(0, 500);//栏目合作方式
        else
            media.Cooperate = txtCooperate.Text.Trim();

        if (txtRemarks.Text.Trim().Length > 2000)
            media.Remarks = txtRemarks.Text.Trim().Substring(0, 2000);//备注
        else
            media.Remarks = txtRemarks.Text.Trim();

        if (txtMediaIntro.Text.Trim().Length > 2000)
            media.Mediaintro = txtMediaIntro.Text.Trim().Substring(0,2000);//栏目简介
        else
            media.Mediaintro = txtMediaIntro.Text.Trim();

        if (txtReaderSort.Text.Trim().Length > 500)
            media.Readersort = txtReaderSort.Text.Trim().Substring(0, 500);//受众描述(1全部2大众3行业4专业)
        else
            media.Readersort = txtReaderSort.Text.Trim();

        if (txtEngIntro.Text.Trim().Length > 500)
            media.Engintro = txtEngIntro.Text.Trim().Substring(0, 500);//英文简介
        else
            media.Engintro = txtEngIntro.Text.Trim();

        //media.Mediaintro = txtMediaIntro.Text.Trim();//媒体简介
        //media.Remarks = txtRemarks.Text.Trim();//备注
        //media.Cooperate = txtCooperate.Text.Trim();//合作方式

        #endregion

        return media;
    }
    #endregion

    #region 绑定页面信息
    /// <summary>
    /// Inits the page.
    /// </summary>
    /// <param name="oldmedia">The oldmedia.</param>
    public void InitPage(MediaitemsInfo oldmedia)
    {
        this.media = oldmedia;
    }
    #endregion

    /// <summary>
    /// Sets the city.
    /// </summary>
    private void setCity()
    {
        DataTable dtCities = ESP.Media.BusinessLogic.CapitalManager.getAllList();
        foreach (DataRow drCity in dtCities.Rows)
        {
            ListItem newitem = new ListItem(drCity["cityname"].ToString(), drCity["cityid"].ToString());
            this.lbxBranch.Items.Add(newitem);
        }
    }

    /// <summary>
    /// Sets the branch.
    /// </summary>
    private void setBranch()
    {
        DataTable dt = ESP.Media.BusinessLogic.BranchManager.GetListByMediaItemID(media.Mediaitemid);
        if (dt != null && dt.Rows.Count > 0 && this.lbxBranch.Items.Count > 0)
        {
            for (int i = this.lbxBranch.Items.Count - 1; i >= 0; i--)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (this.lbxBranch.Items[i].Text == dr["cityname"].ToString() && this.lbxBranch.Items[i].Value == dr["cityid"].ToString())
                    {
                        ListItem selectItem = new ListItem(this.lbxBranch.Items[i].Text, this.lbxBranch.Items[i].Value);
                        this.lbxBranchSelected.Items.Add(selectItem);
                        this.lbxBranch.Items.Remove(this.lbxBranch.Items[i]);
                        break;
                    }
                }
            }
        }

    }

    /// <summary>
    /// Sets the values.
    /// </summary>
    private void setValues()
    {
        //  getCoverRage();

        //省市下拉框
        //getCountry();
        //getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvince);
        //getCity(Convert.ToInt32(ddlProvince.SelectedValue), ddlCity);

        //getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvinceAddr1);
        //getCity(Convert.ToInt32(ddlProvince.SelectedValue), ddlCityAddr1);

        //getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvinceAddr2);
        //getCity(Convert.ToInt32(ddlProvince.SelectedValue), ddlCityAddr2);


        if (media == null)
            return;
        industries = ESP.Media.BusinessLogic.MediaindustryrelationManager.GetAllIndustryByMedia(media.Mediaitemid);

        setCity();
        setBranch();

        txtWebName.Text = media.Mediacname.Trim();//媒体中文名称
        txtWebEngName.Text = media.Mediaename.Trim();//媒体英文名称
        //txtWebHTCName.Text = media.Cshortname.Trim();//媒体中文简称
        txtWebEngHTCName.Text = media.Eshortname.Trim();
        txtWebAddress.Text = media.Webaddress.Trim();//媒体网址
        txtMediumSort.SelectedIndex = 2;//形态属性
        txtWebChannelName.Text = media.Channelname.Trim();//频道名称
       // txtChannelAddress.Text = media.Channelwebaddress.Trim();//频道网址

        //行业属性
        ddlIndustry.SelectedValue = media.Industryid.ToString();


        //受众描述
        txtReaderSort.Text = media.Readersort.Trim();
        //string TopicTypeText = media.Readersort;

        //if (TopicTypeText.Equals("1"))
        //{
        //    ddlReaderSort.SelectedIndex = 0;//全部
        //}
        //else if (TopicTypeText.Equals("2"))
        //{
        //    ddlReaderSort.SelectedIndex = 1;//大众

        //}
        //else if (TopicTypeText.Equals("3"))
        //{
        //    ddlReaderSort.SelectedIndex = 2;//行业

        //}
        //else if (TopicTypeText.Equals("4"))
        //{
        //    ddlReaderSort.SelectedIndex = 3;//专业

        //}

        txtGoverningBody.Text = media.Governingbody.Trim();//所属单位
        txtMajordomo.Text = media.Manager.Trim();//频道总监
        txtCooperate.Text = media.Cooperate.Trim();//合作方式


        txtChiefEditor.Text = media.Chiefeditor.Trim();//总编
        txtAdminEditor.Text = media.Admineditor.Trim();//执行总编

        txtTelephoneExchange.Text = media.Telephoneexchange.Trim();//总机
        txtFax.Text = media.Fax.Trim();//传真
        txtAddress1.Text = media.Addressone.Trim();//地址1
        //txtAddress2.Text= media.Addresstwo .Trim();//地址2
        txtPhoneOne.Text = media.Phoneone.Trim();//热线1
        txtPhoneTwo.Text = media.Phonetwo.Trim();//热线2
        txtAdsPhone.Text = media.Adsphone.Trim();//广告部电话

        if (!media.Medialogo.Equals(""))
        {
            uploadimage.ImageUrl = media.Medialogo;//媒体LOGO"E:\\zhu.JPG" ;

        }
        else
        {
            this.uploadimage.ImageUrl = ESP.Media.Access.Utilities.ConfigManager.DefauleImgPath;
        }
        uploadimage.Width = 20;
        uploadimage.Height = 20;

        txtMediaIntro.Text = media.Mediaintro.Trim();//媒体简介
        txtEngIntro.Text = media.Engintro.Trim();//英文简介
        txtRemarks.Text = media.Remarks.Trim();//备注

        //地域属性
        getRegionAttribute();
        ddlRegionAttribute.SelectedValue = media.Regionattribute.ToString();
        //省市下拉框
        getCountry(int.Parse(ddlRegionAttribute.SelectedValue));
        ddlCountry.SelectedValue = media.Countryid.ToString();
        hidCountry.Value = media.Countryid.ToString();

        getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvince);
        ddlProvince.SelectedValue = media.Provinceid.ToString();
        hidPro.Value = media.Provinceid.ToString();

        getCity(Convert.ToInt32(ddlProvince.SelectedValue), ddlCity);
        ddlCity.SelectedValue = media.Cityid.ToString();
        hidCity.Value = media.Cityid.ToString();

        //getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvinceAddr1);
        getProvince(ESP.Media.Access.Utilities.Global.DefaultCountry, ddlProvinceAddr1);
        ddlProvinceAddr1.SelectedValue = media.Addr1_provinceid.ToString();
        getCity(Convert.ToInt32(ddlProvinceAddr1.SelectedValue), ddlCityAddr1);
        ddlCityAddr1.SelectedValue = media.Addr1_cityid.ToString();
        hidCityAddr1.Value = media.Addr1_cityid.ToString();

        //getProvince(Convert.ToInt32(ddlCountry.SelectedValue), ddlProvinceAddr2);
        //ddlProvinceAddr2.SelectedValue = media.Addr2_provinceid.ToString();
        //getCity(Convert.ToInt32(ddlProvinceAddr2.SelectedValue), ddlCityAddr2);
        //ddlCityAddr2.SelectedValue = media.Addr2_cityid.ToString();

        //邮政编码
        txtPostcode.Text = media.Postcode;
        //txtIssueRegion.Text = media.Issueregion;
    }

    /// <summary>
    /// Gets or sets the industries.
    /// </summary>
    /// <value>The industries.</value>
    public MediaindustryrelationInfo[] Industries
    {
        get
        {
            return this.industries;
        }
        set
        {
            this.industries = value;
        }
    }

    ///// <summary>
    ///// Gets the adsprice.
    ///// </summary>
    ///// <value>The adsprice.</value>
    //public FileUpload Adsprice
    //{
    //    get
    //    {
    //        return this.adsUploadPrice;
    //    }

    //}

    /// <summary>
    /// Gets the logo.
    /// </summary>
    /// <value>The logo.</value>
    public FileUpload Logo
    {
        get
        {
            return this.mediaUploadLogo;
        }

    }

    /// <summary>
    /// Gets the briefing.
    /// </summary>
    /// <value>The briefing.</value>
    public FileUpload Briefing
    {
        get
        {
            return this.briefingUpload;
        }

    }
}